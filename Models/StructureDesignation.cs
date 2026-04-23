using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class StructureDesignation : BaseModel
    {
        public int DesignationId { get; set; } // FK to Designation
        public int StructureUnitId { get; set; } // FK to StructureUnit
        [JsonIgnore]
        public Designation Designation { get; set; }
        [JsonIgnore]
        public StructureUnit StructureUnit { get; set; }
    }
}
