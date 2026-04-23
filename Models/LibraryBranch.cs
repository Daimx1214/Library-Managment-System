using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class LibraryBranch : BaseModel
    {
        public string Description { get; set; } = string.Empty;
        public int LibraryTypeId { get; set; }
        public int? ParentId { get; set; }  // nullable: root branches have no parent

        // Navigation
        [JsonIgnore]
        public LibraryType LibraryType { get; set; }
        [JsonIgnore]
        public List<LibraryBranchLocation> LibraryBranchLocations { get; set; }
        [JsonIgnore]
        public List<LibraryBranchManagement> LibraryBranchManagements { get; set; }
        [JsonIgnore]
        public List<ItemCopy> ItemCopies { get; set; }
        [JsonIgnore]
        public List<LibraryMember> LibraryMembers { get; set; }
    }
}
