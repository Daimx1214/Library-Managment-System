using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class Floor : BaseModel
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public int BuildingId { get; set; }
        [JsonIgnore]
        public Building Building { get; set; }
        [JsonIgnore]
        public List<Room> Rooms { get; set; } // 1 Floor -> Many Rooms
        [JsonIgnore]
        public List<LibraryBranchLocation> LibraryBranchLocations { get; set; } // Locations on this floor


    }
}
