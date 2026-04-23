using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class FineDefinition : BaseModel
    {
        public int FineCategoryId { get; set; }  // FK to FineCategory
        public int? ItemCopyId { get; set; }      // FK to ItemCopy (optional)

        // Navigation
        [JsonIgnore]
        public FineCategory FineCategory { get; set; }
        [JsonIgnore]
        public ItemCopy ItemCopy { get; set; }
        [JsonIgnore]
        public List<UserFine> UserFines { get; set; }
    }
}
