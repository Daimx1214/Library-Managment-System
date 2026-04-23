using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class StructureType : BaseModel
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        [JsonIgnore]
        public List<StructureUnit> StructureUnits { get; set; }
    }
}
