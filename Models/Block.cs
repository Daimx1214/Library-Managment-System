using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class Block : BaseModel  
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public int CampusId { get; set; }

        // Navigation property
        [JsonIgnore]
        public Campus Campus { get; set; }
        [JsonIgnore]
        public List<Building> Buildings { get; set; }
    }
}
