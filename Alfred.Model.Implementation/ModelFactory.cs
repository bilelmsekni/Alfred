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
                Artifacts = Enumerable.Empty<Artifact>()
            };
        }

        public MemberModel CreateMemberModel(Member member)
        {
            return new MemberModel
            {
                Id = member.Id,
                Email = member.Email,
                FirstName = member.FirstName,
                LastName = member.LastName,
                Role = member.Role,
                Artifacts = member.Artifacts.Select(CreateArtifactModel)
            };
        }

        public ArtifactModel CreateArtifactModel(Artifact artifact)
        {
            return new ArtifactModel
            {
                Id = artifact.Id,
                Bonus = artifact.Bonus,
                Reward = artifact.Reward,
                Status = artifact.Status,
                Title = artifact.Title,
                Type = artifact.Type
            };
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
                Id = updateArtifactModel.Id,
                Title = updateArtifactModel.Title,
                Bonus = updateArtifactModel.Bonus,
                Reward = updateArtifactModel.Reward,
                Status = updateArtifactModel.Status,
                Type = updateArtifactModel.Type
            };
        }

        public Member CreateMember(UpdateMemberModel updateMemberModel)
        {
            return new Member
            {
                Id = updateMemberModel.Id,
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
                Id = community.Id,
                Email = community.Email,
                Name = community.Name,
                Artifacts = community.Artifacts.Select(CreateArtifactModel),
                Members = community.Members.Select(CreateMemberModel)
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
                Id = updateCommunityModel.Id,
                Name = updateCommunityModel.Name,
                Email = updateCommunityModel.Email,
                Artifacts = updateCommunityModel.Artifacts.Select(CreateArtifact),
                Members = updateCommunityModel.Members.Select(CreateMember)
            };
        }

        private Artifact CreateArtifact(ArtifactModel artifactModel)
        {
            return new Artifact
            {               
                Title = artifactModel.Title,
                Bonus = artifactModel.Bonus,
                Reward = artifactModel.Reward,
                Status = artifactModel.Status,
                Type = artifactModel.Type,
                Id = artifactModel.Id
            };
        }

        private Member CreateMember(MemberModel memberModel)
        {
            return new Member
            {
                Id = memberModel.Id,
                Email = memberModel.Email,
                FirstName = memberModel.FirstName,
                LastName = memberModel.LastName,
                Role = memberModel.Role,
                Artifacts = memberModel.Artifacts.Select(CreateArtifact)
            };
        }
    }
}
