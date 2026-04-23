using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class ItemQuotation : BaseModel
    {
        public int PartyId { get; set; } // FK to Party
        public int LibraryRackId { get; set; } // FK
        public DateTime QuotationDate { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
        [JsonIgnore]
        public Party Party { get; set; }
        [JsonIgnore]
        public LibraryRacks LibraryRack { get; set; }
        [JsonIgnore]
        public List<PurchaseItem> PurchaseItems { get; set; }
    }
}

