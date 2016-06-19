using System.Collections.Generic;

namespace Alfred.Dal.FakeImplementation.EntityDtos
{
    public class MemberDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Role { get; set; }
        public IEnumerable<int> CommunityIds { get; set; }
    }
}
