using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class AccessionPattern : BaseModel
    {
        public int LibraryBranchId { get; set; } // FK to LibraryBranch
        public string Pattern { get; set; } = string.Empty;
        public int NextSequence { get; set; }

        // Navigation property
        [JsonIgnore]
        public LibraryBranch LibraryBranch { get; set; }
        [JsonIgnore]
        public List<ItemCopy> ItemCopies { get; set; }
    }
}
