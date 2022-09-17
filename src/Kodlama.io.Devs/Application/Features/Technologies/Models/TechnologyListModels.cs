using Application.Features.ProgrammingLanguageSubTechs.Dtos;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgrammingLanguageSubTechs.Models
{
    public class TechnologyListModels : BasePageableModel

    {
        public IList<TechnologyListDto> Items { get; set; }
    }
}
