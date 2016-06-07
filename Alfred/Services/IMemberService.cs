using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alfred.Dal.Entities;

namespace Alfred.Services
{
    public interface IMemberService
    {
        IEnumerable<Member> GetMembers();
        Member GetMember(int id);
    }
}
