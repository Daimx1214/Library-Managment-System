using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class MemberBlock : BaseModel
    {
        public int LibraryMemberId { get; set; } // FK to LibraryMember
        public int LibraryBranchId { get; set; } // FK to LibraryBranch
        public string Reason { get; set; } = string.Empty;
        public DateTime ActiveFrom { get; set; }
        public DateTime? ActiveTo { get; set; }
        [JsonIgnore]
        public LibraryMember LibraryMember { get; set; }
        [JsonIgnore]
        public LibraryBranch LibraryBranch { get; set; }
    }
}
