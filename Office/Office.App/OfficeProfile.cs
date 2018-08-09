using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Office.Models;

namespace Office.App
{
    public class OfficeProfile : Profile
    {
        public OfficeProfile()
        {
            CreateMap<User, User>();
            CreateMap<Team, Team>();
            CreateMap<Invitation, Invitation>();
            CreateMap<Event, Event>();
            CreateMap<TeamEvent, TeamEvent>();
            CreateMap<UserTeam, UserTeam>();
        }

    }
}
