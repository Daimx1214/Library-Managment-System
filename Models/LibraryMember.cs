using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class LibraryMember : BaseModel
    {
        public int LibraryBranchId { get; set; }
        public int PartyId { get; set; }
        public string CardNo { get; set; } = string.Empty;
        public DateTime IssuedOn { get; set; }
        public DateTime ExpiredOn { get; set; }
        public int MembershipType { get; set; }
        [JsonIgnore]
        public LibraryBranch LibraryBranch { get; set; }
        [JsonIgnore]
        public Party Party { get; set; }
        [JsonIgnore]
        public List<Issuance> Issuances { get; set; }
        [JsonIgnore]
        public List<ReserveItem> ReserveItems { get; set; }
        [JsonIgnore]
        public List<UserFine> UserFines { get; set; }
        [JsonIgnore]
        public List<MemberBlock> MemberBlocks { get; set; }
        [JsonIgnore]
        public List<MemberQuotaOverride> MemberQuotaOverrides { get; set; }
    }
}
