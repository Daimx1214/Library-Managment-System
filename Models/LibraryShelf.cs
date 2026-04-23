using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class LibraryShelf : BaseModel
    {
        public string Code { get; set; } = string.Empty;
        public int LibraryRackId { get; set; }
        public int MaxCapacity { get; set; }
        public int NoOfItems { get; set; }
        [JsonIgnore]
        public LibraryRacks LibraryRack { get; set; }
        [JsonIgnore]
        public List<ItemCopy> ItemCopies { get; set; }
    }
}
