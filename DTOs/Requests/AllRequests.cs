using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.DTOs.Requests
{
    // ── University ─────────────────────────────────────────────────────────────
    public class UniversityRequest
    {
        [Required] public string Name { get; set; } = string.Empty;
        [Required] public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    // ── Campus ────────────────────────────────────────────────────────────────
    public class CampusRequest
    {
        [Required] public string Name { get; set; } = string.Empty;
        [Required] public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Required] public int UniversityId { get; set; }
    }

    // ── Block ─────────────────────────────────────────────────────────────────
    public class BlockRequest
    {
        [Required] public string Name { get; set; } = string.Empty;
        [Required] public string Code { get; set; } = string.Empty;
        [Required] public int CampusId { get; set; }
    }

    // ── Building ──────────────────────────────────────────────────────────────
    public class BuildingRequest
    {
        [Required] public string Name { get; set; } = string.Empty;
        [Required] public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Required] public int BlockId { get; set; }
    }

    // ── Floor ─────────────────────────────────────────────────────────────────
    public class FloorRequest
    {
        [Required] public string Name { get; set; } = string.Empty;
        [Required] public string Code { get; set; } = string.Empty;
        [Required] public int BuildingId { get; set; }
    }

    // ── Room ──────────────────────────────────────────────────────────────────
    public class RoomRequest
    {
        [Required] public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Required] public string Code { get; set; } = string.Empty;
        public DateTime EstablishedIn { get; set; } = DateTime.UtcNow;
        [Required] public int FloorId { get; set; }
    }

    // ── RoomType ──────────────────────────────────────────────────────────────
    public class RoomTypeRequest
    {
        [Required] public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Required] public string Code { get; set; } = string.Empty;
    }

    // ── RoomTypeAllocation ────────────────────────────────────────────────────
    public class RoomTypeAllocationRequest
    {
        [Required] public int RoomId { get; set; }
        [Required] public int RoomTypeId { get; set; }
    }

    // ── Feature ───────────────────────────────────────────────────────────────
    public class FeatureRequest
    {
        [Required] public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Required] public string Code { get; set; } = string.Empty;
    }

    // ── RoomFeatureAllocation ─────────────────────────────────────────────────
    public class RoomFeatureAllocationRequest
    {
        [Required] public int RoomId { get; set; }
        [Required] public int FeatureId { get; set; }
    }

    // ── Faculty ───────────────────────────────────────────────────────────────
    public class FacultyRequest
    {
        [Required] public string Name { get; set; } = string.Empty;
        [Required] public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime EstablishedIn { get; set; } = DateTime.UtcNow;
        [Required] public int CampusId { get; set; }
    }

    // ── Institude ─────────────────────────────────────────────────────────────
    public class InstitudeRequest
    {
        [Required] public string Name { get; set; } = string.Empty;
        [Required] public string Code { get; set; } = string.Empty;
        [Required] public int FacultyId { get; set; }
    }

    // ── Department ────────────────────────────────────────────────────────────
    public class DepartmentRequest
    {
        [Required] public string Name { get; set; } = string.Empty;
        [Required] public string Code { get; set; } = string.Empty;
        [Required] public int InstituteId { get; set; }
    }

    // ── Designation ───────────────────────────────────────────────────────────
    public class DesignationRequest
    {
        [Required] public string Name { get; set; } = string.Empty;
        [Required] public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    // ── Position ──────────────────────────────────────────────────────────────
    public class PositionRequest
    {
        [Required] public string Category { get; set; } = string.Empty;
        [Required] public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    // ── Employee ──────────────────────────────────────────────────────────────
    public class EmployeeRequest
    {
        [Required] public string Name { get; set; } = string.Empty;
        public string Contact { get; set; } = string.Empty;
        [Required] public string Code { get; set; } = string.Empty;
        public DateTime JoiningDate { get; set; } = DateTime.UtcNow;
        public DateTime? LeavingDate { get; set; }
        public int CurrentRelationHistoryId { get; set; }
    }

    // ── EmployeeRelationHistory ───────────────────────────────────────────────
    public class EmployeeRelationHistoryRequest
    {
        [Required] public int EmployeeId { get; set; }
        public DateTime EffectiveTo { get; set; }
        public bool IsCurrent { get; set; }
        public string EmployeeType { get; set; } = string.Empty;
        public int EmployeeTypeLookupId { get; set; }
        public int EmployeeCategoryLookupId { get; set; }
        public int EmployeeStatusId { get; set; }
        public int EmployeeGrade { get; set; }
        [Required] public int DepartmentId { get; set; }
        [Required] public int DesignationId { get; set; }
        [Required] public int CampusId { get; set; }
        [Required] public int InstituteId { get; set; }
        [Required] public int FacultyId { get; set; }
        public int ProjectId { get; set; }
        public int SalaryBankAccountId { get; set; }
        public int ExpenseAccountId { get; set; }
        public float SalaryPayable { get; set; }
        public int PayStructureVersionId { get; set; }
    }

    // ── StructureType ─────────────────────────────────────────────────────────
    public class StructureTypeRequest
    {
        [Required] public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Required] public string Code { get; set; } = string.Empty;
    }

    // ── StructureUnit ─────────────────────────────────────────────────────────
    public class StructureUnitRequest
    {
        [Required] public string Name { get; set; } = string.Empty;
        [Required] public string Code { get; set; } = string.Empty;
        public string Level { get; set; } = string.Empty;
        [Required] public int StructureTypeId { get; set; }
    }

    // ── StructureDesignation ──────────────────────────────────────────────────
    public class StructureDesignationRequest
    {
        [Required] public int DesignationId { get; set; }
        [Required] public int StructureUnitId { get; set; }
    }

    // ── Party ─────────────────────────────────────────────────────────────────
    public class PartyRequest
    {
        [Required] public string PartyName { get; set; } = string.Empty;
        public string PartyContact { get; set; } = string.Empty;
        public int PartyPositionId { get; set; }
        public int PartyCompanyId { get; set; }
        public int PartyTypeId { get; set; }
    }

    // ── Student ───────────────────────────────────────────────────────────────
    public class StudentRequest
    {
        [Required] public string RegistrationNumber { get; set; } = string.Empty;
        public int CandidateId { get; set; }
        public int DegreeProgramId { get; set; }
        public int DegreeLevelId { get; set; }
        [Required] public int PartyId { get; set; }
        public string Quota { get; set; } = string.Empty;
        public int TimingId { get; set; }
        public int ProgramSessionId { get; set; }
    }

    // ── LibraryType ───────────────────────────────────────────────────────────
    public class LibraryTypeRequest
    {
        [Required] public string Name { get; set; } = string.Empty;
        [Required] public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    // ── LibraryBranch ─────────────────────────────────────────────────────────
    public class LibraryBranchRequest
    {
        public string Description { get; set; } = string.Empty;
        [Required] public int LibraryTypeId { get; set; }
        public int ParentId { get; set; }
    }

    // ── LibraryBranchLocation ─────────────────────────────────────────────────
    public class LibraryBranchLocationRequest
    {
        [Required] public int LibraryBranchId { get; set; }
        [Required] public int CampusId { get; set; }
        [Required] public int BuildingId { get; set; }
        [Required] public int FloorId { get; set; }
        [Required] public int RoomId { get; set; }
        public string Description { get; set; } = string.Empty;
    }

    // ── LibraryManagementSection ──────────────────────────────────────────────
    public class LibraryManagementSectionRequest
    {
        [Required] public string Name { get; set; } = string.Empty;
        [Required] public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    // ── LibraryBranchManagement ───────────────────────────────────────────────
    public class LibraryBranchManagementRequest
    {
        [Required] public int LibraryBranchId { get; set; }
        [Required] public int LibraryManagementSectionId { get; set; }
        [Required] public string Code { get; set; } = string.Empty;
        public DateTime EstablishedIn { get; set; } = DateTime.UtcNow;
    }

    // ── LibraryItemSection ────────────────────────────────────────────────────
    public class LibraryItemSectionRequest
    {
        [Required] public string Name { get; set; } = string.Empty;
        [Required] public string Code { get; set; } = string.Empty;
        [Required] public int LibraryBranchId { get; set; }
        [Required] public int DepartmentId { get; set; }
    }

    // ── LibraryRacks ──────────────────────────────────────────────────────────
    public class LibraryRacksRequest
    {
        [Required] public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Required] public int LibrarySectionId { get; set; }
    }

    // ── LibraryShelf ──────────────────────────────────────────────────────────
    public class LibraryShelfRequest
    {
        [Required] public string Code { get; set; } = string.Empty;
        [Required] public int LibraryRackId { get; set; }
        public int MaxCapacity { get; set; }
        public int NoOfItems { get; set; }
    }

    // ── LibrarianInfo ─────────────────────────────────────────────────────────
    public class LibrarianInfoRequest
    {
        [Required] public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        [EmailAddress] public string Email { get; set; } = string.Empty;
        public string Contact { get; set; } = string.Empty;
        [Required] public int LibrarySectionId { get; set; }
        [Required] public int LibraryBranchManagementId { get; set; }
        public int LibraryTransactionId { get; set; }
    }

    // ── LibraryMember ─────────────────────────────────────────────────────────
    public class LibraryMemberRequest
    {
        [Required] public int LibraryBranchId { get; set; }
        [Required] public int PartyId { get; set; }
        [Required] public string CardNo { get; set; } = string.Empty;
        public DateTime IssuedOn { get; set; } = DateTime.UtcNow;
        public DateTime ExpiredOn { get; set; } = DateTime.UtcNow.AddYears(1);
        public int MembershipType { get; set; }
    }

    // ── CirculationRule ───────────────────────────────────────────────────────
    public class CirculationRuleRequest
    {
        [Required] public int PartyId { get; set; }
        public int MaxCurrentLoans { get; set; }
        public int MaxCurrentReserves { get; set; }
        public int LoanDays { get; set; }
        public int GraceDay { get; set; }
        public int MaxRenewals { get; set; }
        public decimal OverdueFinePerDay { get; set; }
        public decimal MaxFinePerItem { get; set; }
        [Required] public int LibraryBranchId { get; set; }
        public string LostChargesItem { get; set; } = string.Empty;
        public decimal DamageValue { get; set; }
    }

    // ── MemberBlock ───────────────────────────────────────────────────────────
    public class MemberBlockRequest
    {
        [Required] public int LibraryMemberId { get; set; }
        [Required] public int LibraryBranchId { get; set; }
        public string Reason { get; set; } = string.Empty;
        public DateTime ActiveFrom { get; set; } = DateTime.UtcNow;
        public DateTime? ActiveTo { get; set; }
    }

    // ── MemberQuotaOverride ───────────────────────────────────────────────────
    public class MemberQuotaOverrideRequest
    {
        [Required] public int LibraryMemberId { get; set; }
        [Required] public int LibraryBranchId { get; set; }
        public int MaxCurrentLoans { get; set; }
        public int MaxCurrentReserves { get; set; }
        public int LoanDays { get; set; }
        public int GraceDay { get; set; }
        public int MaxRenewals { get; set; }
        public decimal OverdueFinePerDay { get; set; }
        public decimal MaxFinePerItem { get; set; }
    }

    // ── SubjectHeading ────────────────────────────────────────────────────────
    public class SubjectHeadingRequest
    {
        [Required] public string Name { get; set; } = string.Empty;
        [Required] public string Code { get; set; } = string.Empty;
    }

    // ── Language ──────────────────────────────────────────────────────────────
    public class LanguageRequest
    {
        [Required] public string LanguageCategory { get; set; } = string.Empty;
    }

    // ── AuthorInfo ────────────────────────────────────────────────────────────
    public class AuthorInfoRequest
    {
        [Required] public string AuthorName { get; set; } = string.Empty;
    }

    // ── PublisherInfo ─────────────────────────────────────────────────────────
    public class PublisherInfoRequest
    {
        [Required] public string PublisherName { get; set; } = string.Empty;
    }

    // ── ItemCategory ──────────────────────────────────────────────────────────
    public class ItemCategoryRequest
    {
        [Required] public string ItemCategoryName { get; set; } = string.Empty;
        public int? ParentId { get; set; }
    }

    // ── ItemInfo ──────────────────────────────────────────────────────────────
    public class ItemInfoRequest
    {
        [Required] public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public DateTime PublishDate { get; set; }
        [Required] public int SubjectHeadingId { get; set; }
        public int? LanguageId { get; set; }
        public int? PublisherInfoId { get; set; }
        public int TotalPages { get; set; }
        public string BindingType { get; set; } = string.Empty;
        public DateTime AddedToLibraryDate { get; set; } = DateTime.UtcNow;
        public string Keywords { get; set; } = string.Empty;
    }

    // ── AuthorItemInfo ────────────────────────────────────────────────────────
    public class AuthorItemInfoRequest
    {
        [Required] public int AuthorId { get; set; }
        [Required] public int ItemInfoId { get; set; }
    }

    // ── ItemEdition ───────────────────────────────────────────────────────────
    public class ItemEditionRequest
    {
        [Required] public int ItemId { get; set; }
        public int Year { get; set; }
        public string EditionNumber { get; set; } = string.Empty;
    }

    // ── AccessionPattern ──────────────────────────────────────────────────────
    public class AccessionPatternRequest
    {
        [Required] public int LibraryBranchId { get; set; }
        [Required] public string Pattern { get; set; } = string.Empty;
        public int NextSequence { get; set; } = 1;
    }

    // ── ItemCopy ──────────────────────────────────────────────────────────────
    public class ItemCopyRequest
    {
        [Required] public int ItemInfoId { get; set; }
        [Required] public int LibraryBranchId { get; set; }
        [Required] public int LibrarySectionId { get; set; }
        [Required] public int LibraryRackId { get; set; }
        [Required] public int LibraryShelfId { get; set; }
        public int? AccessionPatternId { get; set; }
        public string AccessionNumber { get; set; } = string.Empty;
        public string ConditionRemarks { get; set; } = string.Empty;
        public string Barcode { get; set; } = string.Empty;
        public decimal UnitCost { get; set; }
    }

    // ── ItemCopies ────────────────────────────────────────────────────────────
    public class ItemCopiesRequest
    {
        [Required] public int ItemCopyId { get; set; }
        [Required] public int EdititonId { get; set; }
        public int NoOfCopies { get; set; }
    }

    // ── ItemCondition ─────────────────────────────────────────────────────────
    public class ItemConditionRequest
    {
        [Required] public int ItemId { get; set; }
        [Required] public int ItemCopyId { get; set; }
        [Required] public int ItemCategoryId { get; set; }
        public string ConditionDescription { get; set; } = string.Empty;
    }

    // ── Duration ──────────────────────────────────────────────────────────────
    public class DurationRequest
    {
        [Required] public string Category { get; set; } = string.Empty;
        [Required] public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    // ── Issuance ──────────────────────────────────────────────────────────────
    public class IssuanceRequest
    {
        [Required] public int LibraryMemberId { get; set; }
        [Required] public int ItemCopyId { get; set; }
        [Required] public int DurationId { get; set; }
        public DateTime IssueDate { get; set; } = DateTime.UtcNow;
        public int DurationNumber { get; set; }
        public decimal FineAccrued { get; set; }
        public int RenewalsUsed { get; set; }
    }

    // ── ReserveItem ───────────────────────────────────────────────────────────
    public class ReserveItemRequest
    {
        [Required] public int ItemInfoId { get; set; }
        [Required] public int DurationId { get; set; }
        public string NumberDuration { get; set; } = string.Empty;
        [Required] public int LibraryMemberId { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime ExpiryDate { get; set; } = DateTime.UtcNow.AddDays(7);
    }

    // ── ReturnItem ────────────────────────────────────────────────────────────
    public class ReturnItemRequest
    {
        [Required] public int LibraryItemId { get; set; }
        public int? PurchaseItemId { get; set; }
        [Required] public int IssuanceId { get; set; }
        public int? ItemInfoId { get; set; }
        public DateTime ReturnDate { get; set; } = DateTime.UtcNow;
    }

    // ── FineCategory ──────────────────────────────────────────────────────────
    public class FineCategoryRequest
    {
        [Required] public string FineCategoryName { get; set; } = string.Empty;
    }

    // ── FineDefinition ────────────────────────────────────────────────────────
    public class FineDefinitionRequest
    {
        [Required] public int FineCategoryId { get; set; }
        public int? ItemCopyId { get; set; }
    }

    // ── UserFine ──────────────────────────────────────────────────────────────
    public class UserFineRequest
    {
        public string FineAllocationRef { get; set; } = string.Empty;
        [Required] public int LibraryMemberId { get; set; }
        [Required] public int FineDefinitionId { get; set; }
        public int? ItemCopyId { get; set; }
        public decimal TaxPercentagePerDay { get; set; }
        public decimal FineAccrued { get; set; }
    }

    // ── FineTransaction ───────────────────────────────────────────────────────
    public class FineTransactionRequest
    {
        public string FineAllocationRef { get; set; } = string.Empty;
        public DateTime FineAllocationDate { get; set; } = DateTime.UtcNow;
        [Required] public int LibraryBranchId { get; set; }
        public string Reason { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string BankAccount { get; set; } = string.Empty;
        public DateTime PaidOn { get; set; } = DateTime.UtcNow;
        public string PaymentRef { get; set; } = string.Empty;
    }

    // ── AcquisitionRecord ─────────────────────────────────────────────────────
    public class AcquisitionRecordRequest
    {
        [Required] public string Type { get; set; } = string.Empty;
        [Required] public int PartyId { get; set; }
        public string Invoice { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public decimal Amount { get; set; }
    }

    // ── ItemQuotation ─────────────────────────────────────────────────────────
    public class ItemQuotationRequest
    {
        [Required] public int PartyId { get; set; }
        [Required] public int LibraryRackId { get; set; }
        public DateTime QuotationDate { get; set; } = DateTime.UtcNow;
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
    }

    // ── PurchaseItem ──────────────────────────────────────────────────────────
    public class PurchaseItemRequest
    {
        [Required] public int ItemQuotationId { get; set; }
        public DateTime PurchaseItemDate { get; set; } = DateTime.UtcNow;
        [Required] public int LibraryItemId { get; set; }
        public string InvoiceNumber { get; set; } = string.Empty;
        [Required] public int PartyId { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Discount { get; set; }
    }

    // ── ItemRequisition ───────────────────────────────────────────────────────
    public class ItemRequisitionRequest
    {
        [Required] public int LibraryBranchId { get; set; }
        [Required] public int ItemInfoId { get; set; }
        public int Quantity { get; set; }
        [Required] public int LibraryRackId { get; set; }
        public decimal EstimateCost { get; set; }
    }

    // ── LibraryRequisition ────────────────────────────────────────────────────
    public class LibraryRequisitionRequest
    {
        [Required] public int ItemInfoId { get; set; }
        [Required] public int LibrarianInfoId { get; set; }
        [Required] public int LibraryBranchId { get; set; }
        [Required] public int EmployeeId { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string Purpose { get; set; } = string.Empty;
    }

    // ── Withdrawal ────────────────────────────────────────────────────────────
    public class WithdrawalRequest
    {
        [Required] public int ItemCopyId { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public int LibraryId { get; set; }
        public string Reason { get; set; } = string.Empty;
        [Required] public int EmployeeId { get; set; }
    }
}
