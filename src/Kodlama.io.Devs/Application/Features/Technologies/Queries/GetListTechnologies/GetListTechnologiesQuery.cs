
using Application.Features.ProgrammingLanguageSubTechs.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Kodlama.io.Devs.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using rentACar.Application.Features.Models.Queries.GetListModelByDynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgrammingLanguageSubTechs.Queries.GetListProgrammingLanguageSubTech
{
    public class GetListTechnologiesQuery : IRequest<TechnologyListModels>
    {
        public PageRequest PageRequest { get; set; }

        public class GetListTechnologiesQueryHandler : IRequestHandler<GetListTechnologiesQuery, TechnologyListModels>
        {
            private readonly ITechnologyRepository _technologyRepository;
            private readonly IMapper _mapper;

            public GetListTechnologiesQueryHandler(ITechnologyRepository technologyRepository, IMapper mapper)
            {
                _technologyRepository = technologyRepository;
                _mapper = mapper;
            }

            public async Task<TechnologyListModels> Handle(GetListTechnologiesQuery request, CancellationToken cancellationToken)
            {
                IPaginate<ProgrammingTechnologies> programmingLanguageTechnologies = await _technologyRepository.GetListAsync(include:
                    p => p.Include(l => l.ProgrammingLanguage),
                    index: request.PageRequest.Page,
                    size: request.PageRequest.PageSize);

                TechnologyListModels model = _mapper.Map<TechnologyListModels>(programmingLanguageTechnologies);
                return model;
            }
        }
    }
}
