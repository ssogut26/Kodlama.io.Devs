using Application.Features.UserProfiles.Dtos;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserProfiles.Models
{
    public class UserProfileListModel : BasePageableModel
    {
        public List<UserProfileListDto> Items { get; set; }
    }
}
