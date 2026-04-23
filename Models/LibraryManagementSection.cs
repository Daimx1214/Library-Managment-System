using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class LibraryManagementSection : BaseModel
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [JsonIgnore]
        public List<LibraryBranchManagement> LibraryBranchManagements { get; set; }


    }
}
