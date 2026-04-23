using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class Department : BaseModel
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;

        // FK to Institude (actual institute)
        public int InstituteId { get; set; }
        [JsonIgnore]
        public Institude Institute { get; set; }

        // Self-reference for sub-departments - ALAG naam
        public int? ParentDepartmentId { get; set; }
        [JsonIgnore]
        public Department? ParentDepartment { get; set; }
        [JsonIgnore]
        public List<Department> SubDepartments { get; set; } = new();

        // Navigation
        [JsonIgnore]
        public List<EmployeeRelationHistory> EmployeeRelationHistories { get; set; } = new();
    }
}