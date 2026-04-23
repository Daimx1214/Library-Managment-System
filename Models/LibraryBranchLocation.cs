using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class LibraryBranchLocation : BaseModel
    {
        public int LibraryBranchId { get; set; }
        public int CampusId { get; set; }
        public int BuildingId { get; set; }
        public int FloorId { get; set; }
        public int RoomId { get; set; }
        public string Description { get; set; } = string.Empty;
        [JsonIgnore]
        public LibraryBranch LibraryBranch { get; set; }
        [JsonIgnore]
        public Campus Campus { get; set; }
        [JsonIgnore]
        public Building Building { get; set; }
        [JsonIgnore]
        public Floor Floor { get; set; }
        [JsonIgnore]
        public Room Room { get; set; }

    }
}
