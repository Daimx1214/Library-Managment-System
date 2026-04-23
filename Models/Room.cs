using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class Room : BaseModel
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public DateTime EstablishedIn { get; set; }
        public int FloorId { get; set; }
        [JsonIgnore]
        public Floor Floor { get; set; }
        [JsonIgnore]
        public List<RoomFeatureAllocation> RoomFeatureAllocations { get; set; }
        [JsonIgnore]
        public List<RoomTypeAllocation> RoomTypeAllocations { get; set; }
    }
}
