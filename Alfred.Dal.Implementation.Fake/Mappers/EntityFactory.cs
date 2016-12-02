using System.Collections.Generic;
using System.Linq;
using Alfred.Dal.Entities.Artifact;
using Alfred.Dal.Entities.Community;
using Alfred.Dal.Entities.Member;
using Alfred.Dal.Implementation.Fake.EntityDtos;
using Alfred.Shared.Enums;

namespace Alfred.Dal.Implementation.Fake.Mappers
{    
    public class EntityFactory : IEntityFactory
    {
        public ArtifactDto TransformToArtifactDto(Artifact artifact)
        {
            if (artifact == null) return null;
            
            return new ArtifactDto
            {
                Id = artifact.Id,
                Title = artifact.Title,
                Bonus = artifact.Bonus,
                Reward = artifact.Reward,
                Status = (int)artifact.Status,
                Type = (int)artifact.Type,
                MemberId = artifact.MemberId,
                CommunityId = artifact.CommunityId
            };            
        }


        public Artifact TransformToArtifactEntity(ArtifactDto artifactDto)
        {
            if (artifactDto == null) return null;

            return new Artifact
            {
                Id = artifactDto.Id,
                Title = artifactDto.Title,
                Bonus = artifactDto.Bonus,
                Reward = artifactDto.Reward,
                Status = (ArtifactStatus)artifactDto.Status,
                Type = (ArtifactType)artifactDto.Type,
                MemberId = artifactDto.MemberId,
                CommunityId = artifactDto.CommunityId
            };
        }

        public Community TransformToCommunityEntity(CommunityDto communityDto)
        {
            if (communityDto == null) return null;
            return new Community
            {
                Id = communityDto.Id,
                Email = communityDto.Email,
                Name = communityDto.Name
            };
        }

        public CommunityDto TransformToCommunityDto(Community community)
        {
            if (community == null) return null;
            return new CommunityDto
            {
                Id = community.Id,
                Email = community.Email,
                Name = community.Name
            };
        }

        public IEnumerable<Member> TransformToMemberEntities(IEnumerable<MemberDto> memberDtos)
        {
            var members = new Dictionary<int, Member>();
            foreach (var dto in memberDtos)
            {
                if (members.ContainsKey(dto.Id))
                {
                    members[dto.Id].CommunityIds.Add(dto.CommunityId);
                }
                else
                {
                    members.Add(dto.Id, TransformToMemberEntity(dto));
                }
            }
            return members.Values.ToList();
        }

        public Member TransformToMemberEntity(MemberDto memberDto)
        {
            if (memberDto == null) return null;
            return new Member
            {
                Id = memberDto.Id,
                Email = memberDto.Email,
                FirstName = memberDto.FirstName,
                LastName = memberDto.LastName,
                Role = (CommunityRole)memberDto.Role,
                CommunityIds = new List<int> { memberDto.CommunityId}
            };
        }

        public MemberDto TransformToMemberDto(Member member)
        {
            if (member == null) return null;

            return new MemberDto
            {
                Id = member.Id,
                Email = member.Email,
                FirstName = member.FirstName,
                LastName = member.LastName,
                Role = (int)member.Role
            };
        }
    }
}
