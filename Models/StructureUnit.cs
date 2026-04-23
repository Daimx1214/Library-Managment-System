using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class StructureUnit : BaseModel
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Level { get; set; } = string.Empty;
        public int StructureTypeId { get; set; }
        [JsonIgnore]
        public StructureType StructureType { get; set; }
        [JsonIgnore]
        public List<StructureDesignation> StructureDesignations { get; set; }
    }
}
