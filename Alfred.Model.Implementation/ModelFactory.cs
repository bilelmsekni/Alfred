using System.Linq;
using Alfred.Dal.Entities.Artifact;
using Alfred.Dal.Entities.Community;
using Alfred.Dal.Entities.Member;
using Alfred.Model.Artifacts;
using Alfred.Model.Communities;
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
                Communities = member.Communities.Select(CreateCommunityModel),
                Artifacts = member.Artifacts.Select(CreateArtifactModel)
            };
        }

        public ArtifactModel CreateArtifactModel(Artifact artifact)
        {
            return new ArtifactModel();
        }

        public Artifact CreateArtifact(CreateArtifactModel createArtifactModel)
        {
            return new Artifact
            {
                Title = createArtifactModel.Title,
                Status = ArtifactStatus.ToDo,
                Type = createArtifactModel.Type,
                Reward = createArtifactModel.Reward,
                Bonus = createArtifactModel.Bonus
            };
        }

        public Artifact CreateArtifact(UpdateArtifactModel updateArtifactModel)
        {
            return new Artifact
            {
                Title = updateArtifactModel.Title,
                Member = updateArtifactModel.Member,
                Bonus = updateArtifactModel.Bonus,
                Community = updateArtifactModel.Community,
                Reward = updateArtifactModel.Reward,
                Status = updateArtifactModel.Status,
                Type = updateArtifactModel.Type
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
            };
        }

        public CommunityModel CreateCommunityModel(Community community)
        {
            return new CommunityModel
            {
                Name = community.Name
            };
        }

        public Community CreateCommunity(CreateCommunityModel createCommunityModel)
        {
            return new Community
            {
                Name = createCommunityModel.Name,
                Email = createCommunityModel.Email,
                Artifacts = Enumerable.Empty<Artifact>(),
                Members = Enumerable.Empty<Member>()
            };
        }

        public Community CreateCommunity(UpdateCommunityModel updateCommunityModel)
        {
            return new Community
            {
                Name = updateCommunityModel.Name,
                Email = updateCommunityModel.Email,
                Artifacts = updateCommunityModel.Artifacts.Select(CreateArtifact),
                Members = updateCommunityModel.Members.Select(CreateMember)
            };
        }

        private Artifact CreateArtifact(ArtifactModel artifactModel)
        {
            return new Artifact();
        }

        public Member CreateMember(MemberModel memberModel)
        {
            return new Member
            {
                Email = memberModel.Email,
                FirstName = memberModel.FirstName,
                LastName = memberModel.LastName,
                Role = memberModel.Role,
                Communities = memberModel.Communities.Select(CreateCommunity),
                Artifacts = memberModel.Artifacts.Select(CreateArtifact)
            };
        }

        public Community CreateCommunity(CommunityModel communityModel)
        {
            return new Community
            {
                Name = communityModel.Name,
                Email = communityModel.Email,
                Artifacts = Enumerable.Empty<Artifact>(),
                Members = Enumerable.Empty<Member>()
            };
        }
    }
}
