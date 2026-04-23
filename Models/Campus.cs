using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class Campus : BaseModel
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int UniversityId { get; set; }

        // Navigation property
        [JsonIgnore]
        public University University { get; set; }
        [JsonIgnore]
        public List<Block> Blocks { get; set; } // 1 Campus -> Many Blocks
        [JsonIgnore]
        public List<Faculty> Faculties { get; set; }

    }
}
