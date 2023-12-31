﻿using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using Domain.Common;
using System.Data;
using Domain.Drivers;
using Domain.Vendors;
using Domain.Prices;
using Domain.Orders;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Domain.User;

namespace Persistence.DataContexts
{
    public class ApplicationDbContext : IdentityDbContext<AppUsers>
    {

        private IDbContextTransaction _currentTransaction;

        //public DbSet<Order> Orders { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<VendorPrice> VendorPrices { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<OrderHistory> OrderHistories { get; set; }
        public DbSet<DriverPaymentHead> DriverPaymentHeads { get; set; }
        public DbSet<DriverPaymentDetails> DriverPaymentDetails { get; set; }
        public DbSet<OrderRequest> OrderRequests { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
 

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableDetailedErrors();
        }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AddAuditInfo();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void AddAuditInfo()
        {
            var timestamp = DateTime.UtcNow;


            foreach (var entry in ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified)))
            {

                if (entry.State == EntityState.Added)
                {

                    ((BaseEntity)entry.Entity).CreatedDate = timestamp;
                }
                else
                {
                    ((BaseEntity)entry.Entity).UpdatedDate = timestamp;
                    ((BaseEntity)entry.Entity).CreatedDate = (DateTime?)entry.Property("CreatedDate").OriginalValue;
                }

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
       

            modelBuilder.Entity<VendorPrice>()
            .HasKey(vp => new { vp.VendorId, vp.PriceId });

            modelBuilder.Entity<VendorPrice>()
                .HasOne(vp => vp.Vendors)
                .WithMany(v => v.VendorPrices)
                .HasForeignKey(vp => vp.VendorId);
            modelBuilder.Entity<Order>()
                .HasOne(x => x.Driver)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.DriverId);
            modelBuilder.Entity<Order>()
                  .HasOne(x => x.Vendor)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.VendorId);
            modelBuilder.Entity<OrderHistory>()
                .HasOne(x => x.Order)
                .WithMany(x => x.OrderHistory)
                .HasForeignKey(x => x.OrderId);
            modelBuilder.Entity<OrderHistory>()
                .HasOne(x => x.OrderStatus)
                .WithMany(x => x.OrderHistory)
                .HasForeignKey(x => x.StatusId);

            modelBuilder.Entity<DriverPaymentDetails>()
                 .HasOne(x => x.DriverPaymentHead)
                 .WithMany(x => x.DriverPaymentDetails)
                 .HasForeignKey(x => x.DriverPaymentHeadId);
            modelBuilder.Entity<DriverPaymentHead>()
                .HasOne(x => x.Driver)
                .WithMany(x => x.DriverPayments)
                .HasForeignKey(x => x.DriverId);



            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<OrderStatus>().HasData(new OrderStatus { Id=Guid.NewGuid(), EngName="Created", ArbName="Created",IsActive=true });
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_currentTransaction != null) return null;

            _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            return _currentTransaction;
        }
        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

            try
            {
                await SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
    }
}
