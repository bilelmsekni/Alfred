using System;

namespace Alfred.Dal.Implementation.Fake.EntityDtos
{
    public class MemberDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Role { get; set; }
        public int CommunityId { get; set; }
        public string Job { get; set; }
        public DateTime CreationDate { get; set; }
        public int Gender { get; set; }
        public string ImageUrl { get; set; }
    }
}
