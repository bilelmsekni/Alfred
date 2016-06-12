using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alfred.Model.Communities;

namespace Alfred.Dal.Interfaces
{
    public interface ICommunityService
    {
        IEnumerable<CommunityModel> GetCommunities();
    }
}
