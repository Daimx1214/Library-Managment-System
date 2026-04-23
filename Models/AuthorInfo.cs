using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class AuthorInfo : BaseModel
    {
        public string AuthorName { get; set; } = string.Empty;

        // Navigation property
        [JsonIgnore]
        public List<AuthorItemInfo> AuthorItemInfos { get; set; } = new();
    }
}