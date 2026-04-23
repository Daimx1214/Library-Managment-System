using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class Party : BaseModel
    {
        public string PartyName { get; set; } = string.Empty;
        public string PartyContact { get; set; } = string.Empty;
        public int PartyPositionId { get; set; } // FK (e.g., Contact person role)
        public int PartyCompanyId { get; set; } // FK (e.g., Company address/details)
        public int PartyTypeId { get; set; }
        [JsonIgnore]
        public Position PartyPosition { get; set; }
        [JsonIgnore]
        public List<AcquisitionRecord> AcquisitionRecords { get; set; }
        [JsonIgnore]
        public List<PurchaseItem> PurchaseItems { get; set; }
        [JsonIgnore]
        public List<ItemQuotation> ItemQuotations { get; set; }
        [JsonIgnore]
        public List<LibraryMember> LibraryMembers { get; set; }
        [JsonIgnore]
        public List<Student> Students { get; set; }
    }
}
