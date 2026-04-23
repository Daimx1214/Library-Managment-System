using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class SubjectHeading : BaseModel
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        [JsonIgnore]
        public List<ItemInfo> ItemInfos { get; set; }
    }
}
