using System;
using System.Collections.Generic;
using Alfred.Dal.Entities.Member;
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
            return _memberDao.GetMember(id);
        }

        public void SaveMember(Member member)
        {
            _memberDao.SaveMember(member);
        }

        public void DeleteMember(int id)
        {
            throw new NotImplementedException();
        }

        public Member GetMember(string email)
        {
            return _memberDao.GetMember(email);
        }
    }
}
