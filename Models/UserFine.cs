using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class UserFine : BaseModel
    {
        public string FineAllocationRef { get; set; } = string.Empty; // Reference number (not FK)
        public int LibraryMemberId { get; set; }
        public int FineDefinitionId { get; set; }
        public int? ItemCopyId { get; set; }
        public decimal TaxPercentagePerDay { get; set; }
        public decimal FineAccrued { get; set; }

        // Navigation
        [JsonIgnore]
        public LibraryMember LibraryMember { get; set; }
        [JsonIgnore]
        public FineDefinition FineDefinition { get; set; }
        [JsonIgnore]
        public ItemCopy ItemCopy { get; set; }
    }
}
