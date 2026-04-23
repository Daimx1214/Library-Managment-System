using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class Issuance : BaseModel
    {
        public int LibraryMemberId { get; set; }
        public int ItemCopyId { get; set; } 
        public int DurationId { get; set; } 
        public DateTime IssueDate { get; set; }
        public int DurationNumber { get; set; }
        public decimal FineAccrued { get; set; }
        public int RenewalsUsed { get; set; }
        [JsonIgnore]
        public LibraryMember LibraryMember { get; set; }
        [JsonIgnore]
        public ItemCopy ItemCopy { get; set; }
        [JsonIgnore]
        public Duration Duration { get; set; }
    }
}
