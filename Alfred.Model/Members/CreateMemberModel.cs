using Alfred.Dal.Entities.Community;

namespace Alfred.Model.Members
{
    public class CreateMemberModel
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ComunityRole Role { get; set; }
    }
}
