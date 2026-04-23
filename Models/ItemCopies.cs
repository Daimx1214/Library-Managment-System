using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class ItemCopies : BaseModel
    {
       public int ItemCopyId { get; set; }
        public int EdititonId { get; set; }
        public int NoOfCopies { get; set; }
        [JsonIgnore]
        public ItemCopy ItemCopy { get; set; }
        [JsonIgnore]
        public ItemEdition ItemEdition { get; set; }
    }
}
