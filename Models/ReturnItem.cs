using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class ReturnItem : BaseModel
    {
        public int LibraryItemId { get; set; }   // FK to ItemCopy
        public int? PurchaseItemId { get; set; } // FK to PurchaseItem (optional)
        public int IssuanceId { get; set; }       // FK to Issuance
        public int? ItemInfoId { get; set; }      // FK to ItemInfo
        public DateTime ReturnDate { get; set; }

        // Navigation
        [JsonIgnore]
        public ItemCopy LibraryItem { get; set; }
        [JsonIgnore]
        public PurchaseItem PurchaseItem { get; set; }
        [JsonIgnore]
        public Issuance Issuance { get; set; }
        [JsonIgnore]
        public ItemInfo ItemInfo { get; set; }
    }
}
