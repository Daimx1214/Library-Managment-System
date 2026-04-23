using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class ItemInfo : BaseModel
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public DateTime PublishDate { get; set; }
        public int SubjectHeadingId { get; set; }
        public int? LanguageId { get; set; }
        public int? PublisherInfoId { get; set; }
        public int TotalPages { get; set; }
        public string BindingType { get; set; } = string.Empty;
        public DateTime AddedToLibraryDate { get; set; }
        public string Keywords { get; set; } = string.Empty;

        // Navigation
        [JsonIgnore]
        public SubjectHeading SubjectHeading { get; set; }
        [JsonIgnore]
        public Language Language { get; set; }
        [JsonIgnore]
        public PublisherInfo PublisherInfo { get; set; }
        [JsonIgnore]
        public List<ItemCondition> ItemConditions { get; set; }
        [JsonIgnore]
        public List<ItemEdition> ItemEditions { get; set; }
        [JsonIgnore]
        public List<AuthorItemInfo> AuthorItemInfos { get; set; }
        [JsonIgnore]
        public List<ItemRequisition> ItemRequisitions { get; set; }
        [JsonIgnore]
        public List<ReserveItem> ReserveItems { get; set; }
        [JsonIgnore]
        public List<ReturnItem> ReturnItems { get; set; }
        [JsonIgnore]
        public List<ItemCopy> ItemCopies { get; set; }
    }
}
