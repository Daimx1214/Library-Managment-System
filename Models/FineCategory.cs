using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class FineCategory : BaseModel
    {
        public string FineCategoryName { get; set; } = string.Empty;

        // Navigation
        [JsonIgnore]
        public List<FineDefinition> FineDefinitions { get; set; }
    }
}
