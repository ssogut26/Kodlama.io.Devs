using Application.Features.ProgrammingLanguageSubTechs.Dtos;
using Application.Features.ProgrammingLanguageSubTechs.Models;
using Application.Features.Technologies.Commands.CreatedTechnologies;
using Application.Features.Technologies.Commands.DeleteTechnologies;
using Application.Features.Technologies.Commands.UpdatedTechnologies;
using Application.Features.Technologies.Dtos;
using AutoMapper;
using Core.Persistence.Paging;
using Kodlama.io.Devs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<ProgrammingTechnologies, TechnologyListDto>().ForMember(c => c.ProgrammingLanguageName, opt => opt.MapFrom(c => c.ProgrammingLanguage.Name)).ReverseMap();
            CreateMap<ProgrammingTechnologies, DeleteTechnologyDto>().ReverseMap();
            CreateMap<ProgrammingTechnologies, UpdatedTechnologyDto>().ReverseMap();
            CreateMap<ProgrammingTechnologies, CreatedTechnologyDto>().ReverseMap();
            CreateMap<IPaginate<ProgrammingTechnologies>, TechnologyListModels>().ReverseMap();
            CreateMap<ProgrammingTechnologies, CreateTechnologyCommand>().ReverseMap();
            CreateMap<ProgrammingTechnologies, UpdatedTechnologyCommand>().ReverseMap();
            CreateMap<ProgrammingTechnologies, DeleteTechnologyCommand>().ReverseMap();
        }
    }
}
