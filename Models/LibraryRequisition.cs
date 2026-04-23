using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class LibraryRequisition : BaseModel
    {
        public int ItemInfoId { get; set; } // FK to ItemInfo
        public int LibrarianInfoId { get; set; } 
        public int LibraryBranchId { get; set; } // FK
        public int EmployeeId { get; set; } // FK to Employee/Librarian
        public DateTime Date { get; set; }
        public string Purpose { get; set; } = string.Empty;
        [JsonIgnore]
        public ItemInfo ItemInfo { get; set; }
        [JsonIgnore]
        public LibrarianInfo LibrarianInfo { get; set; }
        [JsonIgnore]
        public LibraryBranch LibraryBranch { get; set; }
        [JsonIgnore]
        public Employee Employee { get; set; }
    }
}
