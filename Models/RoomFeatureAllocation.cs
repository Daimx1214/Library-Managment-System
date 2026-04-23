using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class RoomFeatureAllocation : BaseModel
    {
        public int RoomId { get; set; }
        public int FeatureId { get; set; }
        [JsonIgnore]
        public Room Room { get; set; }
        [JsonIgnore]
        public Feature Feature { get; set; }
    }
}
