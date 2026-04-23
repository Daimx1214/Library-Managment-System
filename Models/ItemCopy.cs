using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class ItemCopy : BaseModel
    {
        public int ItemInfoId { get; set; }
        public int LibraryBranchId { get; set; }
        public int LibrarySectionId { get; set; }
        public int LibraryRackId { get; set; }
        public int LibraryShelfId { get; set; }
        public int? AccessionPatternId { get; set; }  // FK to AccessionPattern
        public string AccessionNumber { get; set; } = string.Empty;
        public DateTime? ConditionLastChecked { get; set; }
        public string ConditionRemarks { get; set; } = string.Empty;
        public string Barcode { get; set; } = string.Empty;
        public decimal UnitCost { get; set; }

        // Navigation
        [JsonIgnore]
        public ItemInfo ItemInfo { get; set; }
        [JsonIgnore]
        public LibraryBranch LibraryBranch { get; set; }
        [JsonIgnore]
        public LibraryItemSection LibrarySection { get; set; }
        [JsonIgnore]
        public LibraryRacks LibraryRack { get; set; }
        [JsonIgnore]
        public LibraryShelf LibraryShelf { get; set; }
        [JsonIgnore]
        public AccessionPattern AccessionPattern { get; set; }
        [JsonIgnore]
        public List<ItemCondition> ItemConditions { get; set; }
        [JsonIgnore]
        public List<ItemCopies> ItemCopiesList { get; set; }
        [JsonIgnore]
        public List<Issuance> Issuances { get; set; }
        [JsonIgnore]
        public List<UserFine> UserFines { get; set; }
        [JsonIgnore]
        public List<ReturnItem> ReturnItems { get; set; }
        [JsonIgnore]
        public List<Withdrawal> Withdrawals { get; set; }
    }
}
