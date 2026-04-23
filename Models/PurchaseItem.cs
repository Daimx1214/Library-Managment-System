using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class PurchaseItem : BaseModel
    {
        public int ItemQuotationId { get; set; } // FK to ItemQuotation
        public DateTime PurchaseItemDate { get; set; }
        public int LibraryItemId { get; set; } // FK to ItemInfo or ItemCopy
        public string InvoiceNumber { get; set; } = string.Empty;
        public int PartyId { get; set; } // FK to Party
        public decimal TotalAmount { get; set; }
        public decimal Discount { get; set; }
        [JsonIgnore]
        public ItemQuotation ItemQuotation { get; set; }
        [JsonIgnore]
        public ItemInfo LibraryItem { get; set; }
        [JsonIgnore]
        public Party Party { get; set; }
        [JsonIgnore]
        public List<ReturnItem> ReturnItems { get; set; }
    }
}
