using AutoMapper;
using Basket.API.Entities;
using EventBus.Messages.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BasketCheckout, BasketCheckoutEvent>().ReverseMap();
        }
        
    }
}
