using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class ItemCategory : BaseModel
    {
        public string ItemCategoryName { get; set; } = string.Empty;
        public int? ParentId { get; set; }
        [JsonIgnore]
        public ItemCategory ParentCategory { get; set; }
        [JsonIgnore]
        public List<ItemCategory> ChildCategories { get; set; }
        [JsonIgnore]
        public List<ItemCondition> ItemConditions { get; set; }
    }
}
