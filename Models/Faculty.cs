using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class Faculty : BaseModel
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime EstablishedIn { get; set; }
        public int CampusId { get; set; }
        [JsonIgnore]
        public Campus Campus { get; set; }
        [JsonIgnore]
        public List<Institude> Institudes { get; set; } // 1 Faculty -> Many Institutes
        [JsonIgnore]
        public List<EmployeeRelationHistory> EmployeeRelationHistories { get; set; } // Employees in this faculty


    }
}
