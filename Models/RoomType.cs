using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class RoomType : BaseModel
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        [JsonIgnore]
        public List<RoomTypeAllocation> RoomTypeAllocations { get; set; }
    }
}
