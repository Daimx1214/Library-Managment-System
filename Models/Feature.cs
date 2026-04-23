using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class Feature : BaseModel
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        [JsonIgnore]
        public List<RoomFeatureAllocation> RoomFeatureAllocations { get; set; } // 1 Feature -> Many RoomFeatureAllocations


    }
}
