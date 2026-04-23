using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class AuthorItemInfo : BaseModel
    {
        public int AuthorId { get; set; } // FK to AuthorInfo
        public int ItemInfoId { get; set; } // FK to ItemInfo

        // Navigation property
        [JsonIgnore]
        public AuthorInfo? Author { get; set; }
        [JsonIgnore]
        public ItemInfo? ItemInfo { get; set; }

    }
}