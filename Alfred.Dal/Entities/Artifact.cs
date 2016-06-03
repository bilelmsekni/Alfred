namespace Alfred.Dal.Entities
{
    public class Artifact
    {
        public int Id { get; set; }
        public Member Member { get; set; }
        public Community Community { get; set; }
        public string Title { get; set; }
        public ArtifactType Type { get; set; }
        public ArtifactStatus Status { get; set; }
        public int Bonus { get; set; }
        public int Reward { get; set; }
    }
}
