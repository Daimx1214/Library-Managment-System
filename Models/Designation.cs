using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class Designation : BaseModel
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        // Navigation properties
        [JsonIgnore]
        public List<EmployeeRelationHistory> EmployeeRelationHistories { get; set; } // 1 Designation -> Many EmployeeRelationHistories
        [JsonIgnore]
        public List<StructureDesignation> StructureDesignations { get; set; } // 1 Designation -> Many StructureDesignations

    }
}
