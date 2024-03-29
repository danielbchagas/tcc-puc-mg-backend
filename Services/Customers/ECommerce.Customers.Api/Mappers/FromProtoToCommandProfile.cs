﻿using AutoMapper;
using ECommerce.Customers.Application.Commands.Customer;
using System;

namespace ECommerce.Customer.Api.Mappers
{
    public class FromProtoToCommandProfile : Profile
    {
        public FromProtoToCommandProfile()
        {
            CreateMap<Customers.Api.Protos.CreateCustomerRequest, CreateCustomerCommand>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.Parse(src.Id)))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Firstname))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Lastname));

            CreateMap<Customers.Api.Protos.Document, CreateDocumentCommand>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Number))
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => Guid.Parse(src.Customerid)));

            CreateMap<Customers.Api.Protos.Email, CreateEmailCommand>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => Guid.Parse(src.Customerid)));

            CreateMap<Customers.Api.Protos.Phone, CreatePhoneCommand>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Number))
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => Guid.Parse(src.Customerid)));
        }
    }
}
