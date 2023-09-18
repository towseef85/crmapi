﻿using AutoMapper;
using Domain.Drivers;
using Domain.Prices;
using Domain.Vendors;
using Infrastructure.Dtos.DriverDto;
using Infrastructure.Dtos.OrderDto;
using Infrastructure.Dtos.PriceDto;
using Infrastructure.Dtos.VendorDto;
using Infrastructure.Dtos.VendorPriceDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Providers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {

            CreateMap<PostVendorPriceDto, VendorPrice>();
            CreateMap<VendorPrice, GetVendorPriceDto>();

            CreateMap<PostPriceDto, Price>();
            CreateMap<Price, GetPriceDto>();

            CreateMap<PostVendorDto, Vendor>()
                .ForMember(x=>x.VendorPrices, s=>s.MapFrom(x=>x.vendorPrice));
            CreateMap<Vendor, GetVendorDto>()
                .ForMember(x=>x.VendorPrices, s=>s.MapFrom(x=>x.VendorPrices.Select(x=>x.Prices)));

            CreateMap<PostDriverDto, Driver>();
            CreateMap<Driver, GetDriverDto>();
        }
    }
}
