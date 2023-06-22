namespace datasheetapi.Models
{
    public class Revision
    {
        public Revision(TagData tagData)
        {
            TagData = tagData;
        }

        public RevisionStatus Status { get; set; }
        public int RevisionNumber { get; set; }
        public string RevisionName { get; set; } = string.Empty;
        public TagData TagData { get; set; }
        public Review? Review { get; set; }
        public DateTime RevisionDate { get; set; }
        public DateTime Deadline { get; set; }
    }
}