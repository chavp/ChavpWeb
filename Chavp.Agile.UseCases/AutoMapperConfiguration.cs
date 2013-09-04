using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chavp.Agile.UseCases
{
    using AutoMapper;
    using Chavp.Agile.Entities;
    using Chavp.Agile.UseCases.Data;

    public static class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.StatusDisplay, opt => opt.MapFrom(src => src.Status.ToString()));

            Mapper.CreateMap<ProductDto, Product>()
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                .ForMember(dest => dest.Version, opt => opt.Ignore())
                .ForMember(dest => dest.Features, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => 
                    opt.MapFrom(src => (EProductStatus)Enum.Parse(typeof(EProductStatus), 
                        (string.IsNullOrEmpty(src.StatusDisplay) ? "Concept" : src.StatusDisplay))));

            Mapper.AssertConfigurationIsValid();
        }
    }
}
