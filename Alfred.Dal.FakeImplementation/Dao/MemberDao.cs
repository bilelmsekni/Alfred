using System.Collections.Generic;
using Alfred.Dal.Entities;
using Ploeh.AutoFixture;

namespace Alfred.Dal.FakeImplementation.Dao
{
    public class MemberDao
    {
        private readonly IEnumerable<Member> members;
        
        public MemberDao()
        {
            var fixture = new Fixture();
            members = fixture.CreateMany<Member>(5);
        }

        public IEnumerable<Member> GetMembers()
        {
            return members;
        }
    }
}
