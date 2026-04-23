using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class PublisherInfo : BaseModel
    {
        public string PublisherName { get; set; } = string.Empty;
        [JsonIgnore]
        public List<ItemInfo> ItemInfos { get; set; }
    }
}
