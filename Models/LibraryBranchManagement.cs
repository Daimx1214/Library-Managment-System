using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class LibraryBranchManagement : BaseModel
    {
        public int LibraryBranchId { get; set; }
        public int LibraryManagementSectionId { get; set; }
        public string Code { get; set; } = string.Empty;
        public DateTime EstablishedIn { get; set; }
        [JsonIgnore]
        public LibraryBranch LibraryBranch { get; set; }
        [JsonIgnore]
        public LibraryManagementSection LibraryManagementSection { get; set; }
        [JsonIgnore]
        public List<LibrarianInfo> Librarians { get; set; }
    }
}
