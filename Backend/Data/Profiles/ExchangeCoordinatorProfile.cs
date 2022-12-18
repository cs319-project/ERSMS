using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend.DTOs;
using Backend.Entities;

namespace Backend.Data.Profiles
{
    /// <summary>A profile for mapping <see cref="ExchangeCoordinator"/> to <see cref="ExchangeCoordinatorDto"/> and vice versa.</summary>
    public class ExchangeCoordinatorProfile : Profile
    {
        /// <summary>Creates a mapping between the <see cref="ExchangeCoordinatorDto"/> and <see cref="ExchangeCoordinator"/>.</summary>
        public ExchangeCoordinatorProfile()
        {
            CreateMap<ExchangeCoordinatorDto, ExchangeCoordinator>().ReverseMap();
        }
    }
}
