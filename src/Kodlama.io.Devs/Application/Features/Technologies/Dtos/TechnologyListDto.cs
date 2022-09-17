﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgrammingLanguageSubTechs.Dtos
{
    public class TechnologyListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int ProgrammingLanguageId { get; set; }
        public string ProgrammingLanguageName { get; internal set; }
    }
}
