using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Entities.Concrete;
using Entities.Dtos.PersonnelDtos;

namespace Business.Handlers.Personnels.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Personnel, GetByIdPersonnelDto>().ReverseMap();
            CreateMap<Personnel, GetListPersonnelDto>().ReverseMap();
            CreateMap<Personnel, CreatedPersonnelDto>().ReverseMap();

        }
    }
}
