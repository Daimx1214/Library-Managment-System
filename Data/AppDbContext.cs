using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // ── Academic & Physical Structure ─────────────────────────────────────
        public DbSet<University> Universities { get; set; }
        public DbSet<Campus> Campuses { get; set; }
        public DbSet<Block> Blocks { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<Floor> Floors { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<RoomTypeAllocation> RoomTypeAllocations { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<RoomFeatureAllocation> RoomFeatureAllocations { get; set; }

        // ── Academic Hierarchy ────────────────────────────────────────────────
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Institude> Institudes { get; set; }
        public DbSet<Department> Departments { get; set; }

        // ── HR & People ───────────────────────────────────────────────────────
        public DbSet<Designation> Designations { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Party> Parties { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeRelationHistory> EmployeeRelationHistories { get; set; }
        public DbSet<StructureType> StructureTypes { get; set; }
        public DbSet<StructureUnit> StructureUnits { get; set; }
        public DbSet<StructureDesignation> StructureDesignations { get; set; }

        // ── Library Setup ─────────────────────────────────────────────────────
        public DbSet<LibraryType> LibraryTypes { get; set; }
        public DbSet<LibraryBranch> LibraryBranches { get; set; }
        public DbSet<LibraryBranchLocation> LibraryBranchLocations { get; set; }
        public DbSet<LibraryManagementSection> LibraryManagementSections { get; set; }
        public DbSet<LibraryBranchManagement> LibraryBranchManagements { get; set; }
        public DbSet<LibraryItemSection> LibraryItemSections { get; set; }
        public DbSet<LibraryRacks> LibraryRacks { get; set; }
        public DbSet<LibraryShelf> LibraryShelves { get; set; }
        public DbSet<LibrarianInfo> LibrarianInfos { get; set; }
        public DbSet<LibraryMember> LibraryMembers { get; set; }

        // ── Catalogue ─────────────────────────────────────────────────────────
        public DbSet<SubjectHeading> SubjectHeadings { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<AuthorInfo> AuthorInfos { get; set; }
        public DbSet<PublisherInfo> PublisherInfos { get; set; }
        public DbSet<ItemCategory> ItemCategories { get; set; }
        public DbSet<ItemInfo> ItemInfos { get; set; }
        public DbSet<AuthorItemInfo> AuthorItemInfos { get; set; }
        public DbSet<ItemEdition> ItemEditions { get; set; }
        public DbSet<AccessionPattern> AccessionPatterns { get; set; }
        public DbSet<ItemCopy> ItemCopies { get; set; }
        public DbSet<ItemCopies> ItemCopiesList { get; set; }
        public DbSet<ItemCondition> ItemConditions { get; set; }

        // ── Circulation ───────────────────────────────────────────────────────
        public DbSet<Duration> Durations { get; set; }
        public DbSet<CirculationRule> CirculationRules { get; set; }
        public DbSet<MemberBlock> MemberBlocks { get; set; }
        public DbSet<MemberQuotaOverride> MemberQuotaOverrides { get; set; }
        public DbSet<Issuance> Issuances { get; set; }
        public DbSet<ReserveItem> ReserveItems { get; set; }
        public DbSet<ReturnItem> ReturnItems { get; set; }
        public DbSet<Withdrawal> Withdrawals { get; set; }

        // ── Fines ─────────────────────────────────────────────────────────────
        public DbSet<FineCategory> FineCategories { get; set; }
        public DbSet<FineDefinition> FineDefinitions { get; set; }
        public DbSet<UserFine> UserFines { get; set; }
        public DbSet<FineTransaction> FineTransactions { get; set; }

        // ── Acquisitions ──────────────────────────────────────────────────────
        public DbSet<AcquisitionRecord> AcquisitionRecords { get; set; }
        public DbSet<ItemQuotation> ItemQuotations { get; set; }
        public DbSet<PurchaseItem> PurchaseItems { get; set; }
        public DbSet<ItemRequisition> ItemRequisitions { get; set; }
        public DbSet<LibraryRequisition> LibraryRequisitions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ── Decimal precision ─────────────────────────────────────────────
            modelBuilder.Entity<CirculationRule>(e => {
                e.Property(c => c.OverdueFinePerDay).HasColumnType("decimal(18,4)");
                e.Property(c => c.MaxFinePerItem).HasColumnType("decimal(18,2)");
                e.Property(c => c.DamageValue).HasColumnType("decimal(18,2)");
            });
            modelBuilder.Entity<AcquisitionRecord>()
                .Property(a => a.Amount).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<ItemQuotation>(e => {
                e.Property(q => q.Amount).HasColumnType("decimal(18,2)");
                e.Property(q => q.Discount).HasColumnType("decimal(18,2)");
            });
            modelBuilder.Entity<PurchaseItem>(e => {
                e.Property(p => p.TotalAmount).HasColumnType("decimal(18,2)");
                e.Property(p => p.Discount).HasColumnType("decimal(18,2)");
            });
            modelBuilder.Entity<ItemCopy>()
                .Property(ic => ic.UnitCost).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Issuance>()
                .Property(i => i.FineAccrued).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<ItemRequisition>()
                .Property(ir => ir.EstimateCost).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<FineTransaction>()
                .Property(ft => ft.Amount).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<UserFine>(e => {
                e.Property(uf => uf.TaxPercentagePerDay).HasColumnType("decimal(18,4)");
                e.Property(uf => uf.FineAccrued).HasColumnType("decimal(18,2)");
            });
            modelBuilder.Entity<MemberQuotaOverride>(e => {
                e.Property(mq => mq.OverdueFinePerDay).HasColumnType("decimal(18,4)");
                e.Property(mq => mq.MaxFinePerItem).HasColumnType("decimal(18,2)");
            });

            // ── Self-referencing: ItemCategory ────────────────────────────────
            modelBuilder.Entity<ItemCategory>()
                .HasOne(c => c.ParentCategory)
                .WithMany(c => c.ChildCategories)
                .HasForeignKey(c => c.ParentId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            // ── Self-referencing: LibraryBranch (ParentId) ────────────────────
            modelBuilder.Entity<LibraryBranch>()
                .HasOne<LibraryBranch>()
                .WithMany()
                .HasForeignKey(b => b.ParentId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            // ── Department → Institude (explicit FK) ──────────────────────────
            modelBuilder.Entity<Department>()
                .HasOne(d => d.Institute)
                .WithMany(i => i.Departments)
                .HasForeignKey(d => d.InstituteId)
                .OnDelete(DeleteBehavior.Restrict);

            // ── Department self-ref (ParentDepartmentId) ──────────────────────
            modelBuilder.Entity<Department>()
                .HasOne(d => d.ParentDepartment)
                .WithMany(d => d.SubDepartments)
                .HasForeignKey(d => d.ParentDepartmentId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            // ── LibrarianInfo: cascade cycle fix ──────────────────────────────
            modelBuilder.Entity<LibrarianInfo>()
                .HasOne(l => l.LibraryBranchManagement)
                .WithMany(m => m.Librarians)
                .HasForeignKey(l => l.LibraryBranchManagementId)
                .OnDelete(DeleteBehavior.Restrict);

            // ── ItemCopy: restrict cascades to avoid multiple paths ────────────
            modelBuilder.Entity<ItemCopy>()
                .HasOne(ic => ic.LibraryBranch)
                .WithMany(b => b.ItemCopies)
                .HasForeignKey(ic => ic.LibraryBranchId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ItemCopy>()
                .HasOne(ic => ic.LibrarySection)
                .WithMany()
                .HasForeignKey(ic => ic.LibrarySectionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ItemCopy>()
                .HasOne(ic => ic.LibraryRack)
                .WithMany(r => r.ItemCopies)
                .HasForeignKey(ic => ic.LibraryRackId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ItemCopy>()
                .HasOne(ic => ic.LibraryShelf)
                .WithMany(s => s.ItemCopies)
                .HasForeignKey(ic => ic.LibraryShelfId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ItemCopy>()
                .HasOne(ic => ic.AccessionPattern)
                .WithMany(ap => ap.ItemCopies)
                .HasForeignKey(ic => ic.AccessionPatternId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            // ── ItemCopies (junction) ─────────────────────────────────────────
            modelBuilder.Entity<ItemCopies>()
                .HasOne(ic => ic.ItemCopy)
                .WithMany(c => c.ItemCopiesList)
                .HasForeignKey(ic => ic.ItemCopyId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ItemCopies>()
                .HasOne(ic => ic.ItemEdition)
                .WithMany(e => e.ItemCopies)
                .HasForeignKey(ic => ic.EdititonId)
                .OnDelete(DeleteBehavior.Restrict);

            // ── ReturnItem: restrict multiple paths ───────────────────────────
            modelBuilder.Entity<ReturnItem>()
                .HasOne(r => r.LibraryItem)
                .WithMany(ic => ic.ReturnItems)
                .HasForeignKey(r => r.LibraryItemId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ReturnItem>()
                .HasOne(r => r.Issuance)
                .WithMany()
                .HasForeignKey(r => r.IssuanceId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ReturnItem>()
                .HasOne(r => r.PurchaseItem)
                .WithMany(p => p.ReturnItems)
                .HasForeignKey(r => r.PurchaseItemId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ReturnItem>()
                .HasOne(r => r.ItemInfo)
                .WithMany(ii => ii.ReturnItems)
                .HasForeignKey(r => r.ItemInfoId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            // ── CirculationRule → LibraryBranch ───────────────────────────────
            modelBuilder.Entity<CirculationRule>()
                .HasOne(cr => cr.LibraryBranch)
                .WithMany()
                .HasForeignKey(cr => cr.LibraryBranchId)
                .OnDelete(DeleteBehavior.Restrict);

            // ── MemberBlock, MemberQuotaOverride → LibraryBranch ─────────────
            modelBuilder.Entity<MemberBlock>()
                .HasOne(mb => mb.LibraryBranch)
                .WithMany()
                .HasForeignKey(mb => mb.LibraryBranchId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MemberQuotaOverride>()
                .HasOne(mq => mq.LibraryBranch)
                .WithMany()
                .HasForeignKey(mq => mq.LibraryBranchId)
                .OnDelete(DeleteBehavior.Restrict);

            // ── LibraryRequisition → LibraryBranch ────────────────────────────
            modelBuilder.Entity<LibraryRequisition>()
                .HasOne(lr => lr.LibraryBranch)
                .WithMany()
                .HasForeignKey(lr => lr.LibraryBranchId)
                .OnDelete(DeleteBehavior.Restrict);

            // ── FineTransaction → LibraryBranch ───────────────────────────────
            modelBuilder.Entity<FineTransaction>()
                .HasOne(ft => ft.LibraryBranch)
                .WithMany()
                .HasForeignKey(ft => ft.LibraryBranchId)
                .OnDelete(DeleteBehavior.Restrict);

            // ── FineDefinition → FineCategory ─────────────────────────────────
            modelBuilder.Entity<FineDefinition>()
                .HasOne(fd => fd.FineCategory)
                .WithMany(fc => fc.FineDefinitions)
                .HasForeignKey(fd => fd.FineCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // ── UserFine → multiple FKs: restrict cascade ─────────────────────
            modelBuilder.Entity<UserFine>()
                .HasOne(uf => uf.LibraryMember)
                .WithMany(lm => lm.UserFines)
                .HasForeignKey(uf => uf.LibraryMemberId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserFine>()
                .HasOne(uf => uf.FineDefinition)
                .WithMany(fd => fd.UserFines)
                .HasForeignKey(uf => uf.FineDefinitionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserFine>()
                .HasOne(uf => uf.ItemCopy)
                .WithMany(ic => ic.UserFines)
                .HasForeignKey(uf => uf.ItemCopyId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            // ── Issuance ──────────────────────────────────────────────────────
            modelBuilder.Entity<Issuance>()
                .HasOne(i => i.LibraryMember)
                .WithMany(lm => lm.Issuances)
                .HasForeignKey(i => i.LibraryMemberId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Issuance>()
                .HasOne(i => i.ItemCopy)
                .WithMany(ic => ic.Issuances)
                .HasForeignKey(i => i.ItemCopyId)
                .OnDelete(DeleteBehavior.Restrict);

            // ── LibraryMember → LibraryBranch ─────────────────────────────────
            modelBuilder.Entity<LibraryMember>()
                .HasOne(lm => lm.LibraryBranch)
                .WithMany(lb => lb.LibraryMembers)
                .HasForeignKey(lm => lm.LibraryBranchId)
                .OnDelete(DeleteBehavior.Restrict);

            // ── Withdrawal → ItemCopy ─────────────────────────────────────────
            modelBuilder.Entity<Withdrawal>()
                .HasOne(w => w.ItemCopy)
                .WithMany(ic => ic.Withdrawals)
                .HasForeignKey(w => w.ItemCopyId)
                .OnDelete(DeleteBehavior.Restrict);

            // ── ItemCondition ─────────────────────────────────────────────────
            modelBuilder.Entity<ItemCondition>()
                .HasOne(ic => ic.Item)
                .WithMany(ii => ii.ItemConditions)
                .HasForeignKey(ic => ic.ItemId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ItemCondition>()
                .HasOne(ic => ic.ItemCopy)
                .WithMany(c => c.ItemConditions)
                .HasForeignKey(ic => ic.ItemCopyId)
                .OnDelete(DeleteBehavior.Restrict);

            // ── Employee → EmployeeRelationHistory ───────────────────────────
            modelBuilder.Entity<EmployeeRelationHistory>()
                .HasOne(e => e.Employee)
                .WithMany(emp => emp.RelationHistories)
                .HasForeignKey(e => e.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            // ── EmployeeRelationHistory: restrict all paths ───────────────────
            modelBuilder.Entity<EmployeeRelationHistory>()
                .HasOne(e => e.Department)
                .WithMany(d => d.EmployeeRelationHistories)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EmployeeRelationHistory>()
                .HasOne(e => e.Designation)
                .WithMany(d => d.EmployeeRelationHistories)
                .HasForeignKey(e => e.DesignationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EmployeeRelationHistory>()
                .HasOne(e => e.Campus)
                .WithMany()
                .HasForeignKey(e => e.CampusId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EmployeeRelationHistory>()
                .HasOne(e => e.Institute)
                .WithMany(i => i.EmployeeRelationHistories)
                .HasForeignKey(e => e.InstituteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EmployeeRelationHistory>()
                .HasOne(e => e.Faculty)
                .WithMany(f => f.EmployeeRelationHistories)
                .HasForeignKey(e => e.FacultyId)
                .OnDelete(DeleteBehavior.Restrict);

            // ── ItemRequisition ───────────────────────────────────────────────
            modelBuilder.Entity<ItemRequisition>()
                .HasOne(ir => ir.LibraryBranch)
                .WithMany()
                .HasForeignKey(ir => ir.LibraryBranchId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ItemRequisition>()
                .HasOne(ir => ir.ItemInfo)
                .WithMany(ii => ii.ItemRequisitions)
                .HasForeignKey(ir => ir.ItemInfoId)
                .OnDelete(DeleteBehavior.Restrict);

            // ── LibraryItemSection → Department ───────────────────────────────
            modelBuilder.Entity<LibraryItemSection>()
                .HasOne(s => s.Department)
                .WithMany()
                .HasForeignKey(s => s.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // ── LibraryBranchLocation: restrict all paths ─────────────────────
            modelBuilder.Entity<LibraryBranchLocation>()
                .HasOne(l => l.LibraryBranch)
                .WithMany(b => b.LibraryBranchLocations)
                .HasForeignKey(l => l.LibraryBranchId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LibraryBranchLocation>()
                .HasOne(l => l.Campus)
                .WithMany()
                .HasForeignKey(l => l.CampusId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LibraryBranchLocation>()
                .HasOne(l => l.Building)
                .WithMany()
                .HasForeignKey(l => l.BuildingId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LibraryBranchLocation>()
                .HasOne(l => l.Floor)
                .WithMany(f => f.LibraryBranchLocations)
                .HasForeignKey(l => l.FloorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LibraryBranchLocation>()
                .HasOne(l => l.Room)
                .WithMany()
                .HasForeignKey(l => l.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            // ── LibraryShelf → LibraryRack ────────────────────────────────────
            modelBuilder.Entity<LibraryShelf>()
                .HasOne(s => s.LibraryRack)
                .WithMany(r => r.LibraryShelves)
                .HasForeignKey(s => s.LibraryRackId)
                .OnDelete(DeleteBehavior.Restrict);

            // ── ItemQuotation → Party & LibraryRack ──────────────────────────
            modelBuilder.Entity<ItemQuotation>()
                .HasOne(q => q.Party)
                .WithMany()
                .HasForeignKey(q => q.PartyId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ItemQuotation>()
                .HasOne(q => q.LibraryRack)
                .WithMany(r => r.ItemQuotations)
                .HasForeignKey(q => q.LibraryRackId)
                .OnDelete(DeleteBehavior.Restrict);

            // ── AcquisitionRecord → Party ─────────────────────────────────────
            modelBuilder.Entity<AcquisitionRecord>()
                .HasOne(a => a.Party)
                .WithMany()
                .HasForeignKey(a => a.PartyId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<PurchaseItem>()
                .HasOne(p => p.LibraryItem)
                .WithMany()
                .HasForeignKey(p => p.LibraryItemId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PurchaseItem>()
                .HasOne(p => p.Party)
                .WithMany()
                .HasForeignKey(p => p.PartyId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PurchaseItem>()
                .HasOne(p => p.ItemQuotation)
                .WithMany(q => q.PurchaseItems)
                .HasForeignKey(p => p.ItemQuotationId)
                .OnDelete(DeleteBehavior.Restrict);

            // ── ReserveItem ───────────────────────────────────────────────────
            modelBuilder.Entity<ReserveItem>()
                .HasOne(r => r.LibraryMember)
                .WithMany(lm => lm.ReserveItems)
                .HasForeignKey(r => r.LibraryMemberId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ReserveItem>()
                .HasOne(r => r.ItemInfo)
                .WithMany(ii => ii.ReserveItems)
                .HasForeignKey(r => r.ItemInfoId)
                .OnDelete(DeleteBehavior.Restrict);

            // ── Withdrawal → Employee ─────────────────────────────────────────
            modelBuilder.Entity<Withdrawal>()
                .HasOne(w => w.Employee)
                .WithMany(e => e.Withdrawals)
                .HasForeignKey(w => w.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            // ── LibraryRequisition → Employee ─────────────────────────────────
            modelBuilder.Entity<LibraryRequisition>()
                .HasOne(lr => lr.Employee)
                .WithMany(e => e.LibraryRequisitions)
                .HasForeignKey(lr => lr.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            // ── LibraryRequisition → LibrarianInfo ────────────────────────────
            modelBuilder.Entity<LibraryRequisition>()
                .HasOne(lr => lr.LibrarianInfo)
                .WithMany(li => li.LibraryRequisitions)
                .HasForeignKey(lr => lr.LibrarianInfoId)
                .OnDelete(DeleteBehavior.Restrict);

            // ── LibrarianInfo → WithMany Librarians ───────────────────────────
            modelBuilder.Entity<LibrarianInfo>()
                .HasOne(l => l.LibrarySection)
                .WithMany(s => s.Librarians)
                .HasForeignKey(l => l.LibrarySectionId)
                .OnDelete(DeleteBehavior.Restrict);

            // ── MemberBlock → LibraryMember ───────────────────────────────────
            modelBuilder.Entity<MemberBlock>()
                .HasOne(mb => mb.LibraryMember)
                .WithMany(lm => lm.MemberBlocks)
                .HasForeignKey(mb => mb.LibraryMemberId)
                .OnDelete(DeleteBehavior.Restrict);

            // ── MemberQuotaOverride → LibraryMember ───────────────────────────
            modelBuilder.Entity<MemberQuotaOverride>()
                .HasOne(mq => mq.LibraryMember)
                .WithMany(lm => lm.MemberQuotaOverrides)
                .HasForeignKey(mq => mq.LibraryMemberId)
                .OnDelete(DeleteBehavior.Restrict);

            // ── ItemCondition → ItemCategory ──────────────────────────────────
            modelBuilder.Entity<ItemCondition>()
                .HasOne(ic => ic.ItemCategory)
                .WithMany(c => c.ItemConditions)
                .HasForeignKey(ic => ic.ItemCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // ── ItemRequisition → LibraryRack ─────────────────────────────────
            modelBuilder.Entity<ItemRequisition>()
                .HasOne(ir => ir.LibraryRack)
                .WithMany()
                .HasForeignKey(ir => ir.LibraryRackId)
                .OnDelete(DeleteBehavior.Restrict);

            // ── ItemCopy → ItemInfo ───────────────────────────────────────────
            modelBuilder.Entity<ItemCopy>()
                .HasOne(ic => ic.ItemInfo)
                .WithMany(ii => ii.ItemCopies)
                .HasForeignKey(ic => ic.ItemInfoId)
                .OnDelete(DeleteBehavior.Restrict);

            // ── ItemEdition → ItemInfo ────────────────────────────────────────
            modelBuilder.Entity<ItemEdition>()
                .HasOne(e => e.ItemInfo)
                .WithMany(ii => ii.ItemEditions)
                .HasForeignKey(e => e.ItemId)
                .OnDelete(DeleteBehavior.Restrict);

            // ── AuthorItemInfo → AuthorInfo ───────────────────────────────────
            modelBuilder.Entity<AuthorItemInfo>()
                .HasOne(ai => ai.Author)
                .WithMany(a => a.AuthorItemInfos)
                .HasForeignKey(ai => ai.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            // ── AuthorItemInfo → ItemInfo ─────────────────────────────────────
            modelBuilder.Entity<AuthorItemInfo>()
                .HasOne(ai => ai.ItemInfo)
                .WithMany(ii => ii.AuthorItemInfos)
                .HasForeignKey(ai => ai.ItemInfoId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}