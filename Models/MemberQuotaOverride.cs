using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class MemberQuotaOverride : BaseModel
    {
        public int LibraryMemberId { get; set; } // FK to LibraryMember
        public int LibraryBranchId { get; set; } // FK to LibraryBranch
        public int MaxCurrentLoans { get; set; }
        public int MaxCurrentReserves { get; set; }
        public int LoanDays { get; set; }
        public int GraceDay { get; set; }
        public int MaxRenewals { get; set; }
        public decimal OverdueFinePerDay { get; set; }
        public decimal MaxFinePerItem { get; set; }
        [JsonIgnore]
        public LibraryMember LibraryMember { get; set; }
        [JsonIgnore]
        public LibraryBranch LibraryBranch { get; set; }
    }
}
