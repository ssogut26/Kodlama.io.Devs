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

namespace Application.Features.Technologies.Commands.UpdatedTechnologies
{
    public class UpdatedTechnologyCommand : IRequest<UpdatedTechnologyDto>
    {
        public string? Name { get; set; }
        public int Id { get; set; }
    }

    public class UpdateTechnologyCommandHandler : IRequestHandler<UpdatedTechnologyCommand, UpdatedTechnologyDto>
    {
        private readonly ITechnologyRepository _technologyRepository;
        private readonly IMapper _mapper;
        private readonly TechnologyBusinessRules _technologyBusinessRules;

        public UpdateTechnologyCommandHandler(TechnologyBusinessRules technologyBusinessRules, ITechnologyRepository technologyRepository, IMapper mapper)
        {
            _mapper = mapper;
            _technologyRepository = technologyRepository;
            _technologyBusinessRules = technologyBusinessRules;
        }

        public async Task<UpdatedTechnologyDto> Handle(UpdatedTechnologyCommand request, CancellationToken cancellationToken)
        {
            ProgrammingTechnologies? oldTechnology = await _technologyRepository.GetAsync(pl => pl.Id == request.Id);
            await _technologyBusinessRules.TechnologyNameCannotBeDuplicatedWhenInserted(request.Name);
            _mapper.Map<UpdatedTechnologyCommand, ProgrammingTechnologies>(request, oldTechnology);
            ProgrammingTechnologies updatedProgrammingTechnology = await _technologyRepository.UpdateAsync(oldTechnology);
            UpdatedTechnologyDto updatedProgrammingTechnologyDto = _mapper.Map<UpdatedTechnologyDto>(updatedProgrammingTechnology);
            return updatedProgrammingTechnologyDto;
        }
    }
}
