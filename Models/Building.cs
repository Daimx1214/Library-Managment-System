using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class Building : BaseModel
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int BlockId { get; set; }

        // Navigation property
        [JsonIgnore]
        public Block Block { get; set; }
        [JsonIgnore]
        public List<Floor> Floors { get; set; }

    }
}
