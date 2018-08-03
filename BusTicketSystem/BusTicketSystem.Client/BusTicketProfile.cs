using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using BusTicketSystem.Models;

namespace BusTicketSystem.Client
{
   public  class BusTicketProfile : Profile
    {

        public BusTicketProfile()
        {
            CreateMap<Review, Review>();
            CreateMap<Ticket, Ticket>();
            CreateMap<BusStation, BusStation>();
            CreateMap<BusCompany, BusCompany>();
            CreateMap<Customer, Customer>();
            
        }

    }
}

