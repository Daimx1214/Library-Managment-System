using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class Student : BaseModel
    {
        public string RegistrationNumber { get; set; } = string.Empty;
        public int CandidateId { get; set; } // FK (Likely to a Candidate/Applicant table)
        public int DegreeProgramId { get; set; } // FK
        public int DegreeLevelId { get; set; } // FK
        public int PartyId { get; set; } // FK to Party (if Student is also a Party/Customer)
        public string Quota { get; set; } = string.Empty;
        public int TimingId { get; set; } // FK (e.g., Morning/Evening)
        public int ProgramSessionId { get; set; }
        [JsonIgnore]
        public Party Party { get; set; }
    }
}
