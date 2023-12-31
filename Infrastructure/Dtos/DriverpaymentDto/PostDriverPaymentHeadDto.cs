﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Dtos.DriverpaymentDto
{
    public class PostDriverPaymentHeadDto
    {
        public Guid Id { get; set; }
        public Guid DriverId { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal Balance { get; set; }
        public string? PaymentType { get; set; }//Cash,Card,UpI
        public string? PaymentStatus { get; set; }//Paid,Unpaid,Others
        public string? Remarks { get; set; }
        public ICollection<PostDriverPaymentDetailsDto> PaymentDetails { get; set; }
    }
}
