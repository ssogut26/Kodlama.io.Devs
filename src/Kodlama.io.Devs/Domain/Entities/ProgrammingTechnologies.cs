using Core.Persistence.Repositories;
using Kodlama.io.Devs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Domain.Entities
{
    public class ProgrammingTechnologies : Entity
    {
        public string Name { get; set; }
        public int ProgrammingLanguageId { get; set; }
        public virtual ProgrammingLanguage? ProgrammingLanguage { get; set; }

        public ProgrammingTechnologies()
        {

        }

        public ProgrammingTechnologies(int id, int programmingLanugageID, string name) : this()
        {
            Id = id;
            Name = name;
            ProgrammingLanguageId = programmingLanugageID;
        }
    }
}
