using System.Linq;
using Alfred.Dal.Entities.Artifact;
using Alfred.Dal.Entities.Community;
using Alfred.Dal.Entities.Member;
using Alfred.Model.Members;

namespace Alfred.Model.Implementation
{
    public class ModelFactory : IModelFactory
    {
        public Member CreateMember(CreateMemberModel createMemberModel)
        {
            return new Member
            {
                Email = createMemberModel.Email,
                FirstName = createMemberModel.FirstName,
                LastName = createMemberModel.LastName,
                Role = createMemberModel.Role,
                Communities = Enumerable.Empty<Community>(),
                Artifacts = Enumerable.Empty<Artifact>()
            };
        }

        public MemberModel CreateMemberModel(Member member)
        {
            return new MemberModel
            {
                Email = member.Email,
                FirstName = member.FirstName,
                LastName = member.LastName,
                Role = member.Role,
                Communities = member.Communities,
                Artifacts = member.Artifacts
            };
        }

        public Member CreateMember(UpdateMemberModel updateMemberModel)
        {
            return new Member
            {
                Email = updateMemberModel.Email,
                FirstName = updateMemberModel.FirstName,
                LastName = updateMemberModel.LastName,
                Role = updateMemberModel.Role,
                Communities = updateMemberModel.Communities,
                Artifacts = updateMemberModel.Artifacts
            };
        }
    }
}
