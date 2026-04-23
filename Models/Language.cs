using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class Language : BaseModel
    {
        public string LanguageCategory { get; set; } = string.Empty;
        [JsonIgnore]
        public List<ItemInfo> ItemInfos { get; set; }
    }
}
