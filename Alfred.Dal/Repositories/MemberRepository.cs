using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alfred.Dal.Daos;
using Alfred.Dal.Mappers;
using Alfred.Domain.Repositories;
using Alfred.Models.Members;

namespace Alfred.Dal.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly IMemberDao _memberDao;
        private readonly IModelFactory _modelFactory;

        public MemberRepository(IMemberDao memberDao, IModelFactory modelFactory)
        {
            _memberDao = memberDao;
            _modelFactory = modelFactory;
        }
        public async Task<IEnumerable<MemberModel>> GetMembers(MemberCriteriaModel criteriaModel)
        {
            var criteria = _modelFactory.CreateMemberCriteria(criteriaModel);
            var members = await _memberDao.GetMembers(criteria).ConfigureAwait(false);
            return members.Select(_modelFactory.CreateMemberModel);
        }

        public async Task<MemberModel> GetMember(int id)
        {
            return _modelFactory.CreateMemberModel(await _memberDao.GetMember(id).ConfigureAwait(false));
        }

        public async Task<int> SaveMember(CreateMemberModel newMember)
        {
            var member = _modelFactory.CreateMember(newMember);
            return await _memberDao.SaveMember(member).ConfigureAwait(false);
        }

        public async Task DeleteMember(int id)
        {
            await _memberDao.DeleteMember(id).ConfigureAwait(false);
        }

        public async Task<MemberModel> GetMember(string email)
        {
            return _modelFactory.CreateMemberModel(await _memberDao.GetMember(email).ConfigureAwait(false));
        }

        public async Task<MemberModel> UpdateMember(UpdateMemberModel memberUpdates)
        {
            var oldMember = await _memberDao.GetMember(memberUpdates.Id).ConfigureAwait(false);
            if (oldMember != null)
            {
                var newMember = _modelFactory.CreateMember(memberUpdates, oldMember);
                if (newMember != null)
                {
                    await _memberDao.UpdateMember(newMember).ConfigureAwait(false);
                    return _modelFactory.CreateMemberModel(newMember);
                }
            }
            return null;
        }

        public async Task<IEnumerable<MemberModel>> GetCommunityMembers(int id)
        {
            var communityMembers = await _memberDao.GetCommunityMembers(id).ConfigureAwait(false);
            return communityMembers.Select(_modelFactory.CreateMemberModel);
        }
    }
}
