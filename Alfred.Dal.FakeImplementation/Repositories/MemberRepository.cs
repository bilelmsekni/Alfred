using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Alfred.Dal.Entities;
using Alfred.Dal.Interfaces;

namespace Alfred.Dal.FakeImplementation.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        public IEnumerable<Member> GetMembers()
        {
            throw new NotImplementedException();
        }

        public Member GetMember(int id)
        {
            throw new NotImplementedException();
        }

        public void SaveMember(Member member)
        {
            throw new NotImplementedException();
        }

        public void DeleteMember(int id)
        {
            throw new NotImplementedException();
        }
    }
}
