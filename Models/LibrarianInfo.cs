using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class LibrarianInfo : BaseModel  
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Contact { get; set; } = string.Empty;
        public int LibrarySectionId { get; set; } // FK
        public int LibraryBranchManagementId { get; set; } // FK (Branch ID)
        public int LibraryTransactionId { get; set; }
        [JsonIgnore]
        public LibraryItemSection LibrarySection { get; set; }
        [JsonIgnore]
        public LibraryBranchManagement LibraryBranchManagement { get; set; }
        [JsonIgnore]
        public List<LibraryRequisition> LibraryRequisitions { get; set; }
    }
}
