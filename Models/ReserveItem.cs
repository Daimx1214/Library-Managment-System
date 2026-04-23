using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class ReserveItem : BaseModel
    {
        public int ItemInfoId { get; set; } 
        public int DurationId { get; set; } 
        public string NumberDuration { get; set; } = string.Empty; 
        public int LibraryMemberId { get; set; }
        public bool IsAvailable { get; set; } 
        public DateTime ExpiryDate { get; set; }
        [JsonIgnore]
        public ItemInfo ItemInfo { get; set; }
        [JsonIgnore]
        public Duration Duration { get; set; }
        [JsonIgnore]
        public LibraryMember LibraryMember { get; set; }
    }
}
