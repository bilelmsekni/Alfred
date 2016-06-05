using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Alfred.Dal.Entities;
using Alfred.Dal.FakeImplementation.Dao;
using Alfred.Dal.Interfaces;

namespace Alfred.Dal.FakeImplementation.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private IMemberDao _memberDao;

        public MemberRepository(IMemberDao memberDao)
        {
            _memberDao = memberDao;
        }
        public IEnumerable<Member> GetMembers()
        {
            return _memberDao.GetMembers();
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
