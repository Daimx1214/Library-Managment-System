using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class Employee : BaseModel
    {
        public string Name { get; set; } = string.Empty;
        public string Contact { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public DateTime JoiningDate { get; set; }
        public DateTime? LeavingDate { get; set; } // Nullable
        public int CurrentRelationHistoryId { get; set; }

        // Navigation properties
        [JsonIgnore]
        public List<EmployeeRelationHistory> RelationHistories { get; set; } // 1 Employee -> Many Relation Histories
        [JsonIgnore]
        public List<LibraryRequisition> LibraryRequisitions { get; set; } // If Employee makes requisitions
        [JsonIgnore]
        public List<Withdrawal> Withdrawals { get; set; }
    }
}
