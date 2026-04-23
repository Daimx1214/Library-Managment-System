using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class LibraryItemSection : BaseModel
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public int LibraryBranchId { get; set; }
        public int DepartmentId { get; set; }
        [JsonIgnore]
        public LibraryBranch LibraryBranch { get; set; }
        [JsonIgnore]
        public Department Department { get; set; }
        [JsonIgnore]
        public List<LibrarianInfo> Librarians { get; set; }

    }
}
