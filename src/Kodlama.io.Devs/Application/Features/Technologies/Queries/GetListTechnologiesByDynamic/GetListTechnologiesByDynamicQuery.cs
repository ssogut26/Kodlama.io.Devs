using Application.Features.ProgrammingLanguageSubTechs.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Kodlama.io.Devs.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rentACar.Application.Features.Models.Queries.GetListModelByDynamic
{
    public class GetListUserProfileByDynamicQuery : IRequest<TechnologyListModels>
    {
        public Dynamic Dynamic { get; set; }
        public PageRequest PageRequest { get; set; }
        public class GetListTechnologiesByDynamicQueryHandler : IRequestHandler<GetListUserProfileByDynamicQuery, TechnologyListModels>
        {
            private readonly ITechnologyRepository _programmingLanguageSubTechRepository;
            private readonly IMapper _mapper;

            public GetListTechnologiesByDynamicQueryHandler(ITechnologyRepository programmingLanguageSubTechRepository, IMapper mapper)
            {
                _programmingLanguageSubTechRepository = programmingLanguageSubTechRepository;
                _mapper = mapper;
            }
            public async Task<TechnologyListModels> Handle(GetListUserProfileByDynamicQuery request, CancellationToken cancellationToken)
            {
                IPaginate<ProgrammingTechnologies> models = await _programmingLanguageSubTechRepository.GetListByDynamicAsync(
                    dynamic: request.Dynamic,
                    include:
                    m => m.Include(c => c.ProgrammingLanguage),
                    index: request.PageRequest.Page,
                    size: request.PageRequest.PageSize);
                TechnologyListModels model = _mapper.Map<TechnologyListModels>(models);
                return model;
            }
        }
    }
}