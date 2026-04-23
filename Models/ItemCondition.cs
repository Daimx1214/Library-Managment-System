using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class ItemCondition : BaseModel
    {
        public int ItemId { get; set; } // FK to ItemInfo
        public int ItemCopyId { get; set; } 
        public int ItemCategoryId { get; set; } // FK to ItemCategory
        public string ConditionDescription { get; set; } = string.Empty;
        [JsonIgnore]
        public ItemInfo Item { get; set; }
        [JsonIgnore]
        public ItemCopy ItemCopy { get; set; }
        [JsonIgnore]
        public ItemCategory ItemCategory { get; set; }
    }
}
