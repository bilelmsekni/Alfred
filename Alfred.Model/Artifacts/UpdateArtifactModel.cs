using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alfred.Dal.Entities.Artifact;
using Alfred.Dal.Entities.Community;
using Alfred.Dal.Entities.Member;

namespace Alfred.Model.Artifacts
{
    public class UpdateArtifactModel
    {
        public string Title { get; set; }
        public Member Member { get; set; }
        public int Bonus { get; set; }
        public Community Community { get; set; }
        public int Reward { get; set; }
        public ArtifactStatus Status { get; set; }
        public ArtifactType Type { get; set; }
    }
}
