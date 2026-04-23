using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class LibraryRacks : BaseModel
    {
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int LibrarySectionId { get; set; }
        [JsonIgnore]
        public LibraryItemSection LibraryItemSection { get; set; }
        [JsonIgnore]
        public List<ItemQuotation> ItemQuotations { get; set; }
        [JsonIgnore]
        public List<ItemCopy> ItemCopies { get; set; }
        [JsonIgnore]
        public List<LibraryShelf> LibraryShelves { get; set; }
    }
}
