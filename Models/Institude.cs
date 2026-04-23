using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class Institude : BaseModel
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public int FacultyId { get; set; }
        [JsonIgnore]
        public Faculty Faculty { get; set; }
        [JsonIgnore]
        public List<Department> Departments { get; set; } // 1 Institute -> Many Departments
        [JsonIgnore]
        public List<EmployeeRelationHistory> EmployeeRelationHistories { get; set; } // Employees linked to this institute


    }
}
