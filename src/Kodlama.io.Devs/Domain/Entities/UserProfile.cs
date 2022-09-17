using Core.Persistence.Repositories;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserProfile : Entity
    {

        public int UserId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public virtual User User { get; set; }

        public UserProfile()
        {

        }

        public UserProfile(int id, int userId, string name, string url)
        {
            Id = id;
            UserId = userId;
            Name = name;
            Url = url;
        }
    }
}
