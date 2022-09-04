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

namespace Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Commands.UpdateProgrammingLanguage
{
    public class UpdateProgrammingLanguageCommand : IRequest<UpdatedProgrammingLanguageDto>
    {
        public string? Name { get; set; }
        public int Id { get; set; }
    }

    public class UpdateProgrammingLanguageCommandHandler : IRequestHandler<UpdateProgrammingLanguageCommand, UpdatedProgrammingLanguageDto>
    {
        IMapper _mapper;
        IProgrammingLanguageRepository _programmingLanguageRepository;
        ProgrammingLanguageBusinessRules _programmingLanguageBusinessRules;

        public UpdateProgrammingLanguageCommandHandler(ProgrammingLanguageBusinessRules programmingLanguageBusinessRules, IProgrammingLanguageRepository programmingLanguageRepository, IMapper mapper)
        {
            _mapper = mapper;
            _programmingLanguageRepository = programmingLanguageRepository;
            _programmingLanguageBusinessRules = programmingLanguageBusinessRules;
        }

        public async Task<UpdatedProgrammingLanguageDto> Handle(UpdateProgrammingLanguageCommand request, CancellationToken cancellationToken)
        {
            ProgrammingLanguage? oldLanguage = await _programmingLanguageRepository.GetAsync(pl => pl.Id == request.Id);
            await _programmingLanguageBusinessRules.ProgrammingLanguageNameCannotBeDuplicatedWhenInserted(request.Name);
            _mapper.Map<UpdateProgrammingLanguageCommand, ProgrammingLanguage>(request, oldLanguage);
            ProgrammingLanguage updatedLanguage = await _programmingLanguageRepository.UpdateAsync(oldLanguage);
            UpdatedProgrammingLanguageDto updatedLanguageDto = _mapper.Map<UpdatedProgrammingLanguageDto>(updatedLanguage);
            return updatedLanguageDto;
        }
    }
}
