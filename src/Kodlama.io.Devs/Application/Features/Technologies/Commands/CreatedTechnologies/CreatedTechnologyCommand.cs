using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Dtos;
using Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Rules;
using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Commands.CreatedTechnologies
{
    public class CreateTechnologyCommand : IRequest<CreatedTechnologyDto>
    {
        public string Name { get; set; }

        public int ProgrammingLanguageId { get; set; }

        public class CreateTechnologyCommandHandler : IRequestHandler<CreateTechnologyCommand, CreatedTechnologyDto>
        {
            private readonly ITechnologyRepository _technologyRepository;
            private readonly IMapper _mapper;
            private readonly TechnologyBusinessRules _technologyBusinessRules;



            public CreateTechnologyCommandHandler(ITechnologyRepository technologyRepository, IMapper mapper, TechnologyBusinessRules technologyBusinessRules)
            {
                _mapper = mapper;
                _technologyRepository = technologyRepository;
                _technologyBusinessRules = technologyBusinessRules;
            }

            public async Task<CreatedTechnologyDto> Handle(CreateTechnologyCommand request, CancellationToken cancellationToken)
            {
                _technologyBusinessRules.ProgrammingTechnologyShouldExistWhenRequested(request.ProgrammingLanguageId);
                await _technologyBusinessRules.TechnologyNameCannotBeDuplicatedWhenInserted(request.Name);
                ProgrammingTechnologies programmingTechnologies = await _technologyRepository.GetAsync(p => p.Id == request.ProgrammingLanguageId);
                ProgrammingTechnologies mapperProgrammingLanguage = _mapper.Map<ProgrammingTechnologies>(request);
                ProgrammingTechnologies createdProgrammingTechnology = await _technologyRepository.AddAsync(mapperProgrammingLanguage);
                CreatedTechnologyDto createdTechnologyDto = _mapper.Map<CreatedTechnologyDto>(createdProgrammingTechnology);
                return createdTechnologyDto;
            }

        }
    }
}
