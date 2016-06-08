using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alfred.Dal.Entities.Member;
using Alfred.Model.Members;

namespace Alfred.Model
{
    public interface IModelFactory
    {
        Member CreateModel(CreateMemberModel createMemberModel);
    }
}
