using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class FineTransaction : BaseModel
    {
        public string FineAllocationRef { get; set; } = string.Empty;
        public DateTime FineAllocationDate { get; set; }
        public int LibraryBranchId { get; set; }
        public string Reason { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string BankAccount { get; set; } = string.Empty;
        public DateTime PaidOn { get; set; }
        public string PaymentRef { get; set; } = string.Empty;

        // Navigation
        [JsonIgnore]
        public LibraryBranch LibraryBranch { get; set; }
    }
}
