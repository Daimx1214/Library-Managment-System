using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class ItemEdition : BaseModel
    {
        public int ItemId { get; set; } // FK to ItemInfo
        public int Year { get; set; }
        public string EditionNumber { get; set; } = string.Empty;
        [JsonIgnore]
        public ItemInfo ItemInfo { get; set; }
        [JsonIgnore]
        public List<ItemCopies> ItemCopies { get; set; }
    }
}
