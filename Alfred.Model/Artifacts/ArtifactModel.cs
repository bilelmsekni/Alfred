using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alfred.Dal.Entities.Artifact;

namespace Alfred.Model.Artifacts
{
    public class ArtifactModel
    {
        public string Title { get; set; }
        public int Reward { get; set; }
        public int Bonus { get; set; }
        public ArtifactType Type { get; set; }
        public ArtifactStatus Status { get; set; }
    }
}
