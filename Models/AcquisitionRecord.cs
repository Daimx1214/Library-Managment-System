using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class AcquisitionRecord : BaseModel
    {
        public string Type { get; set; } = string.Empty;
        public int PartyId { get; set; } // FK to Party (Vendor)
        public string Invoice { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }

        // Navigation property
        [JsonIgnore]
        public Party Party { get; set; }
    }
}
