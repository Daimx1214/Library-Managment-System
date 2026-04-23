using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class Position : BaseModel
    {
        public string Category { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [JsonIgnore]
        public List<Party> Parties { get; set; }
    }
}
