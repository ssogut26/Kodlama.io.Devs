using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Rules
{
    public class TechnologyBusinessRules
    {
        private readonly ITechnologyRepository _technologyRepository;

        public TechnologyBusinessRules(ITechnologyRepository technologyRepository)
        {
            _technologyRepository = technologyRepository;
        }

        public async Task TechnologyNameCannotBeDuplicatedWhenInserted(string Name)
        {
            IPaginate<ProgrammingTechnologies> result = await _technologyRepository.GetListAsync(pl => pl.Name == Name);
            if (result.Items.Any()) throw new BusinessException("Technology name is already exist");
        }

        public void ProgrammingTechnologyShouldExistWhenRequested(int id)
        {
            if (id == null | id < 1) throw new BusinessException("Requested technology does not exists.");
        }

        public void ProgrammingTechnologyDeleteNotPossible(ProgrammingTechnologies technologies)
        {
            if (technologies == null) throw new BusinessException("Technology name cannot be deleted because of requested id not found.");
        }

    }
}
