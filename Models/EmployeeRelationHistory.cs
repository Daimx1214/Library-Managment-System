using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class EmployeeRelationHistory : BaseModel
    {
        public int EmployeeId { get; set; } // FK to Employee
        public DateTime EffectiveTo { get; set; }
        public bool IsCurrent { get; set; } // Indicates the current active record
        public string EmployeeType { get; set; } = string.Empty;
        public int EmployeeTypeLookupId { get; set; } // FK
        public int EmployeeCategoryLookupId { get; set; } // FK
        public int EmployeeStatusId { get; set; } // FK
        public int EmployeeGrade { get; set; } // FK
        public int DepartmentId { get; set; } // FK
        public int DesignationId { get; set; } // FK to Designation
        public int CampusId { get; set; } // FK
        public int InstituteId { get; set; } // FK
        public int FacultyId { get; set; } // FK
        public int ProjectId { get; set; } // FK
        public int SalaryBankAccountId { get; set; } // FK
        public int ExpenseAccountId { get; set; } // FK
        public float SalaryPayable { get; set; }
        public int PayStructureVersionId { get; set; }

        // Navigation properties
        [JsonIgnore]
        public Employee Employee { get; set; }
        [JsonIgnore]
        public Department Department { get; set; }
        [JsonIgnore]
        public Designation Designation { get; set; }
        [JsonIgnore]
        public Campus Campus { get; set; }
        [JsonIgnore]
        public Institude Institute { get; set; }
        [JsonIgnore]
        public Faculty Faculty { get; set; }

    }
}
