using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class Withdrawal : BaseModel
    {
        public int ItemCopyId { get; set; }
        public DateTime Date { get; set; }
        public int LibraryId { get; set; }
        public string Reason { get; set; } = string.Empty;
        public int EmployeeId { get; set; }

        // Navigation
        [JsonIgnore]
        public ItemCopy ItemCopy { get; set; }
        [JsonIgnore]
        public Employee Employee { get; set; }
    }
}
