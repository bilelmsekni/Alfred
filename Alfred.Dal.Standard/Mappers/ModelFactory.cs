using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Alfred.Dal.Standard.Entities.Artifacts;
using Alfred.Dal.Standard.Entities.Base;
using Alfred.Dal.Standard.Entities.Communities;
using Alfred.Dal.Standard.Entities.Members;
using Alfred.Shared.Standard.Enums;
using Alfred.Standard.Models.Artifacts;
using Alfred.Standard.Models.Base;
using Alfred.Standard.Models.Communities;
using Alfred.Standard.Models.Members;

namespace Alfred.Dal.Standard.Mappers
{
    public class ModelFactory : IModelFactory
    {
        private readonly ObjectDifferenceManager _objDiffManager;
        private readonly UrlHelper _urlHelper;

        public ModelFactory(ObjectDifferenceManager objDiffManager, Func<HttpRequestMessage> getHttpRequestMessage)
        {
            _objDiffManager = objDiffManager;
            _urlHelper = new UrlHelper(getHttpRequestMessage());
        }

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
                    CreationDate = member.CreationDate,
                    Job = member.Job,
                    Gender = member.Gender,
                    ImageUrl = member.ImageUrl,
                    Communities = member.Communities.Select(CreateCommunityModel)
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
                Type = artifact.Type,
                MemberId = artifact.MemberId,
                CommunityId = artifact.CommunityId
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
                Bonus = createArtifactModel.Bonus,
                MemberId = createArtifactModel.MemberId,
                CommunityId = createArtifactModel.CommunityId
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
                Type = updateArtifactModel.Type,
                MemberId = updateArtifactModel.MemberId,
                CommunityId = updateArtifactModel.CommunityId
            };
            return _objDiffManager.UpdateObject(oldArtifact, newArtifact);
        }

        public ArtifactCriteria CreateArtifactCrtieria(ArtifactCriteriaModel criteriaModel)
        {
            return new ArtifactCriteria
            {
                Ids = criteriaModel.Ids?.Select(int.Parse),
                Title = criteriaModel.Title,
                Type = criteriaModel.Type,
                Status = criteriaModel.Status,
                CommunityId = criteriaModel.CommunityId,
                MemberId = criteriaModel.MemberId,
                Page = criteriaModel.Page,
                PageSize = criteriaModel.PageSize
            };
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
                Communities = originalMember.Communities                
            };
            return _objDiffManager.UpdateObject(originalMember, newMember);
        }

        public CommunityModel CreateCommunityModel(Community community)
        {
            return new CommunityModel
            {
                Id = community.Id,
                Email = community.Email,
                Name = community.Name,
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

            return _objDiffManager.UpdateObject(originalCommunity, newCommunity);
        }

        public MemberCriteria CreateMemberCriteria(MemberCriteriaModel criteriaModel)
        {
            return new MemberCriteria
            {
                Ids = criteriaModel.Ids?.Select(int.Parse),
                CommunityId = criteriaModel.CommunityId,
                Email = criteriaModel.Email,
                Name = criteriaModel.Name,
                Role = criteriaModel.Role,
                Page = criteriaModel.Page,
                PageSize = criteriaModel.PageSize
            };
        }

        public CommunityCriteria CreateCommunityCriteria(CommunityCriteriaModel criteriaModel)
        {
            return new CommunityCriteria
            {
                Ids = criteriaModel.Ids?.Select(int.Parse),
                Name = criteriaModel.Name,
                Email = criteriaModel.Email,
                Page = criteriaModel.Page,
                PageSize = criteriaModel.PageSize
            };
        }

        public ArtifactResponseModel CreateArtifactResponseModel(ArtifactResponse artifactResponse)
        {
            return CreateResponseModel<Artifact, ArtifactModel, ArtifactResponseModel> (artifactResponse, CreateArtifactModel);
        }

        public CommunityResponseModel CreateCommunityResponseModel(CommunityResponse communityResponse)
        {
            return CreateResponseModel<Community, CommunityModel, CommunityResponseModel>(communityResponse, CreateCommunityModel);
        }

        private Dictionary<string, object> ExtractQueryParams(HttpRequestMessage request)
        {
            var result = new Dictionary<string, object>();
            var queryParams = request.RequestUri.ParseQueryString();
            foreach (var key in queryParams.AllKeys)
            {
                result[key] = queryParams[key];
            }
            return result;
        }

        public LinkModel CreateLinkModel(Link link, Dictionary<string, object> queryParams)
        {
            queryParams["page"] = link.Href;
            return new LinkModel
            {
                Href = _urlHelper.Link(AlfredRoutes.GetArtifacts, queryParams),
                Rel = link.Rel
            };
        }

        public MemberResponseModel CreateMemberResponseModel(MemberResponse memberResponse)
        {
            return CreateResponseModel<Member, MemberModel, MemberResponseModel>(memberResponse, CreateMemberModel);
        }

        private TResult CreateResponseModel<TEntity, TModel,TResult>(BaseResponse<TEntity> baseResponse,
            Func<TEntity, TModel> createModel) where TResult : BaseResponseModel<TModel>, new()
            {
            var queryParams = ExtractQueryParams(_urlHelper.Request);
            return new TResult
            {
                Results = baseResponse.Results?.Select(createModel),
                Links = baseResponse.Links?.Select(l => CreateLinkModel(l, queryParams))
            };
        }
    }
}
