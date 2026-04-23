using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class Duration : BaseModel
    {
        public string Category { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        // Navigation property
        [JsonIgnore]
        public List<Issuance> Issuances { get; set; } // 1 Duration -> Many Issuances
        [JsonIgnore]
        public List<ReserveItem> ReserveItems { get; set; }

    }
}
