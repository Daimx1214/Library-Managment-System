using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class RoomTypeAllocation : BaseModel
    {
        public int RoomId { get; set; }
        public int RoomTypeId { get; set; }
        [JsonIgnore]
        public Room Room { get; set; }
        [JsonIgnore]
        public RoomType RoomType { get; set; }
    }
}
