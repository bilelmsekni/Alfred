using System.Linq;
using Alfred.Dal.Entities.Artifact;
using Alfred.Dal.Entities.Community;
using Alfred.Dal.Entities.Enums;
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
                Role = createMemberModel.Role               
            };
        }

        public MemberModel CreateMemberModel(Member member)
        {
            if (member != null)
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
            return null;
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

        public Artifact CreateArtifact(UpdateArtifactModel updateArtifactModel, Artifact oldArtifact)
        {
            var newArtifact = new Artifact
            {
                Id = updateArtifactModel.Id,
                Title = updateArtifactModel.Title,
                Bonus = updateArtifactModel.Bonus,
                Reward = updateArtifactModel.Reward,
                Status = updateArtifactModel.Status,
                Type = updateArtifactModel.Type
            };

            var diffs = ObjectDiffPatch.GenerateDiff(oldArtifact, newArtifact);            
            return ObjectDiffPatch.PatchObject(oldArtifact, diffs.NewValues);
        }

        public Member CreateMember(UpdateMemberModel updateMemberModel, Member originalMember)
        {
            var newMember = new Member
            {
                Id = updateMemberModel.Id,
                Email = updateMemberModel.Email,
                FirstName = updateMemberModel.FirstName,
                LastName = updateMemberModel.LastName,
                Role = updateMemberModel.Role,
            };
            var diffs = ObjectDiffPatch.GenerateDiff(originalMember, newMember);
            return ObjectDiffPatch.PatchObject(originalMember, diffs.NewValues);
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
            };
        }

        public Community CreateCommunity(UpdateCommunityModel updateCommunityModel, Community originalCommunity)
        {
            var newCommunity = new Community
            {
                Id = updateCommunityModel.Id,
                Name = updateCommunityModel.Name,
                Email = updateCommunityModel.Email
            };

            var diffs = ObjectDiffPatch.GenerateDiff(originalCommunity, newCommunity);
            return ObjectDiffPatch.PatchObject(originalCommunity, diffs.NewValues);
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
