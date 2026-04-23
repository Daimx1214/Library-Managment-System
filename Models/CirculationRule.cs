using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class CirculationRule : BaseModel
    {
        public int PartyId { get; set; } // FK to Party/Member Type
        public int MaxCurrentLoans { get; set; }
        public int MaxCurrentReserves { get; set; }
        public int LoanDays { get; set; }
        public int GraceDay { get; set; }
        public int MaxRenewals { get; set; }
        public decimal OverdueFinePerDay { get; set; }
        public decimal MaxFinePerItem { get; set; }
        public int LibraryBranchId { get; set; } // FK to LibraryBranch
        public string LostChargesItem { get; set; } = string.Empty;
        public decimal DamageValue { get; set; }

        // Navigation properties
        [JsonIgnore]
        public Party Party { get; set; }
        [JsonIgnore]
        public LibraryBranch LibraryBranch { get; set; }
    }
}
