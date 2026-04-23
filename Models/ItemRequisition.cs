using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class ItemRequisition : BaseModel
    {
        public int LibraryBranchId { get; set; } // FK
        public int ItemInfoId { get; set; } // FK to ItemInfo
        public int Quantity { get; set; }
        public int LibraryRackId { get; set; } // FK
        public decimal EstimateCost { get; set; }
        [JsonIgnore]
        public LibraryBranch LibraryBranch { get; set; }
        [JsonIgnore]
        public ItemInfo ItemInfo { get; set; }
        [JsonIgnore]
        public LibraryRacks LibraryRack { get; set; }
    }
}
