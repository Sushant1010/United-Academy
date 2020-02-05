namespace United_IMS.Web.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class united_ims : DbContext
    {
        public united_ims()
            : base("name=united_ims")
        {
        }

        public virtual DbSet<ACD_Batch> ACD_Batch { get; set; }
        public virtual DbSet<ACD_Program> ACD_Program { get; set; }
        public virtual DbSet<ACD_Class> ACD_Class { get; set; }
        public virtual DbSet<ACD_ClassSection> ACD_ClassSection { get; set; }
        public virtual DbSet<ACD_Section> ACD_Section { get; set; }
        public virtual DbSet<ACD_Staff> ACD_Staff { get; set; }
        public virtual DbSet<ACD_Student> ACD_Student { get; set; }
        public virtual DbSet<ACD_StudentClassSection> ACD_StudentClassSection { get; set; }
        public virtual DbSet<FA_Asset> FA_Asset { get; set; }
        public virtual DbSet<FA_AssetCategory> FA_AssetCategory { get; set; }
        public virtual DbSet<FA_AssetDepreciation> FA_AssetDepreciation { get; set; }
        public virtual DbSet<FA_AssetItem> FA_AssetItem { get; set; }
        public virtual DbSet<FA_AssetTransfer> FA_AssetTransfer { get; set; }
        public virtual DbSet<FA_Branch> FA_Branch { get; set; }
        public virtual DbSet<FA_Demand> FA_Demand { get; set; }
        public virtual DbSet<FA_DemandList> FA_DemandList { get; set; }
        public virtual DbSet<FA_DepreciationMethod> FA_DepreciationMethod { get; set; }
        public virtual DbSet<FA_Location> FA_Location { get; set; }
        public virtual DbSet<FA_Maintenance> FA_Maintenance { get; set; }
        public virtual DbSet<FA_Purchase> FA_Purchase { get; set; }
        public virtual DbSet<FA_PurchaseItem> FA_PurchaseItem { get; set; }
        public virtual DbSet<FA_Vendor> FA_Vendor { get; set; }
        public virtual DbSet<INV_PurchaseBill> INV_PurchaseBill { get; set; }
        public virtual DbSet<INV_PurchaseItem> INV_PurchaseItem { get; set; }
        public virtual DbSet<INV_SoldBill> INV_SoldBill { get; set; }
        public virtual DbSet<INV_SoldItem> INV_SoldItem { get; set; }
        public virtual DbSet<INV_StockItem> INV_StockItem { get; set; }
        public virtual DbSet<INV_Vendor> INV_Vendor { get; set; }
        public virtual DbSet<MS_AcademicYear> MS_AcademicYear { get; set; }
        public virtual DbSet<MS_Category> MS_Category { get; set; }
        public virtual DbSet<MS_Department> MS_Department { get; set; }
        public virtual DbSet<MS_Faculty> MS_Faculty { get; set; }
        public virtual DbSet<MS_Item> MS_Item { get; set; }
        public virtual DbSet<MS_ItemUnit> MS_ItemUnit { get; set; }
        public virtual DbSet<MS_Organization> MS_Organization { get; set; }
        public virtual DbSet<MS_Unit> MS_Unit { get; set; }
        public virtual DbSet<SC_LoginHistory> SC_LoginHistory { get; set; }
        public virtual DbSet<SC_Module> SC_Module { get; set; }
        public virtual DbSet<SC_Role> SC_Role { get; set; }
        public virtual DbSet<SC_User> SC_User { get; set; }
        public virtual DbSet<SC_UserRole> SC_UserRole { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ACD_Batch>()
                .Property(e => e.BatchName)
                .IsUnicode(false);

            modelBuilder.Entity<ACD_Batch>()
                .Property(e => e.BatchCode)
                .IsUnicode(false);

            modelBuilder.Entity<ACD_Batch>()
                .Property(e => e.StartDateBS)
                .IsUnicode(false);

            modelBuilder.Entity<ACD_Program>()
                .Property(e => e.ProgramName)
                .IsUnicode(false);

            modelBuilder.Entity<ACD_Program>()
                .Property(e => e.ProgramCode)
                .IsUnicode(false);

            modelBuilder.Entity<ACD_Program>()
                .Property(e => e.StartedDateBS)
                .IsUnicode(false);

            modelBuilder.Entity<ACD_Class>()
                .Property(e => e.ClassName)
                .IsUnicode(false);

            modelBuilder.Entity<ACD_Class>()
                .Property(e => e.ClassCode)
                .IsUnicode(false);

            modelBuilder.Entity<ACD_ClassSection>()
                .Property(e => e.StartDateBS)
                .IsUnicode(false);

            modelBuilder.Entity<ACD_Section>()
                .Property(e => e.SectionName)
                .IsUnicode(false);

            modelBuilder.Entity<ACD_Section>()
                .Property(e => e.SectionCode)
                .IsUnicode(false);

            modelBuilder.Entity<ACD_Staff>()
                .Property(e => e.StaffName)
                .IsUnicode(false);

            modelBuilder.Entity<ACD_Staff>()
                .Property(e => e.StaffCode)
                .IsUnicode(false);

            modelBuilder.Entity<ACD_Staff>()
                .Property(e => e.TemporaryAddress)
                .IsUnicode(false);

            modelBuilder.Entity<ACD_Staff>()
                .Property(e => e.PermanentAddress)
                .IsUnicode(false);

            modelBuilder.Entity<ACD_Staff>()
                .Property(e => e.Mobile)
                .IsUnicode(false);

            modelBuilder.Entity<ACD_Staff>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<ACD_Staff>()
                .Property(e => e.CitizenshipNo)
                .IsUnicode(false);

            modelBuilder.Entity<ACD_Staff>()
                .Property(e => e.JoinDateBS)
                .IsUnicode(false);

            modelBuilder.Entity<ACD_Staff>()
                .Property(e => e.DOBBS)
                .IsUnicode(false);

            modelBuilder.Entity<ACD_Staff>()
                .Property(e => e.Photo)
                .IsUnicode(false);

            modelBuilder.Entity<ACD_Student>()
                .Property(e => e.StudentName)
                .IsUnicode(false);

            modelBuilder.Entity<ACD_Student>()
                .Property(e => e.StudentCode)
                .IsUnicode(false);

            modelBuilder.Entity<ACD_Student>()
                .Property(e => e.StudentRegNo)
                .IsUnicode(false);

            modelBuilder.Entity<ACD_Student>()
                .Property(e => e.Gender)
                .IsUnicode(false);

            modelBuilder.Entity<ACD_Student>()
                .Property(e => e.TemporaryAddress)
                .IsUnicode(false);

            modelBuilder.Entity<ACD_Student>()
                .Property(e => e.PermanentAddress)
                .IsUnicode(false);

            modelBuilder.Entity<ACD_Student>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<ACD_Student>()
                .Property(e => e.DOBBS)
                .IsUnicode(false);

            modelBuilder.Entity<ACD_Student>()
                .Property(e => e.FatherName)
                .IsUnicode(false);

            modelBuilder.Entity<ACD_Student>()
                .Property(e => e.FatherContact)
                .IsUnicode(false);

            modelBuilder.Entity<ACD_Student>()
                .Property(e => e.MotherName)
                .IsUnicode(false);

            modelBuilder.Entity<ACD_Student>()
                .Property(e => e.MotherContact)
                .IsUnicode(false);

            modelBuilder.Entity<ACD_StudentClassSection>()
                .Property(e => e.AcademicYear)
                .IsUnicode(false);

            modelBuilder.Entity<ACD_StudentClassSection>()
                .Property(e => e.AssignedDateBS)
                .IsUnicode(false);

            modelBuilder.Entity<FA_Asset>()
                .Property(e => e.AssetUniqueCode)
                .IsUnicode(false);

            modelBuilder.Entity<FA_Asset>()
                .Property(e => e.Barcode)
                .IsUnicode(false);

            modelBuilder.Entity<FA_Asset>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<FA_Asset>()
                .Property(e => e.BillDateBs)
                .IsUnicode(false);

            modelBuilder.Entity<FA_Asset>()
                .Property(e => e.OpeningValue)
                .HasPrecision(10, 2);
            modelBuilder.Entity<FA_Asset>()
                .Property(e => e.PurchaseValue)
                .HasPrecision(10, 2);

            modelBuilder.Entity<FA_Asset>()
                .Property(e => e.UsefulLife)
                .HasPrecision(10, 2);

            modelBuilder.Entity<FA_Asset>()
                .Property(e => e.DepreciationRemarks)
                .IsUnicode(false);

            modelBuilder.Entity<FA_Asset>()
                .Property(e => e.ScrapRealizedValue)
                .HasPrecision(10, 2);

            modelBuilder.Entity<FA_AssetCategory>()
                .Property(e => e.CategoryName)
                .IsUnicode(false);

            modelBuilder.Entity<FA_AssetCategory>()
                .Property(e => e.CategoryCode)
                .IsUnicode(false);

            modelBuilder.Entity<FA_AssetCategory>()
                .Property(e => e.DepreciationRate)
                .HasPrecision(5, 2);

            modelBuilder.Entity<FA_AssetDepreciation>()
                .Property(e => e.DepreciationDateBS)
                .IsUnicode(false);

            modelBuilder.Entity<FA_AssetItem>()
                .Property(e => e.AssetName)
                .IsUnicode(false);

            modelBuilder.Entity<FA_AssetItem>()
                .Property(e => e.AssetCode)
                .IsUnicode(false);

            modelBuilder.Entity<FA_AssetItem>()
                .Property(e => e.LifeSpan)
                .HasPrecision(10, 2);

            modelBuilder.Entity<FA_AssetTransfer>()
                .Property(e => e.TransferDateBS)
                .IsUnicode(false);

            modelBuilder.Entity<FA_AssetTransfer>()
                .Property(e => e.Remarks)
                .IsUnicode(false);

            modelBuilder.Entity<FA_Branch>()
                .Property(e => e.BranchName)
                .IsUnicode(false);

            modelBuilder.Entity<FA_Demand>()
                .Property(e => e.DemandDateBS)
                .IsUnicode(false);

            modelBuilder.Entity<FA_Demand>()
                .Property(e => e.Remarks)
                .IsUnicode(false);

            modelBuilder.Entity<FA_Demand>()
                .Property(e => e.IsApproved)
                .IsUnicode(false);

            modelBuilder.Entity<FA_Demand>()
                .Property(e => e.ApproveRemarks)
                .IsUnicode(false);

            modelBuilder.Entity<FA_DemandList>()
                .Property(e => e.Remarks)
                .IsUnicode(false);

            modelBuilder.Entity<FA_DemandList>()
                .Property(e => e.IsApproved)
                .IsUnicode(false);

            modelBuilder.Entity<FA_DepreciationMethod>()
                .Property(e => e.MethodName)
                .IsUnicode(false);

            modelBuilder.Entity<FA_DepreciationMethod>()
                .Property(e => e.DepreciationRate)
                .HasPrecision(10, 2);

            modelBuilder.Entity<FA_DepreciationMethod>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<FA_Location>()
                .Property(e => e.LocationName)
                .IsUnicode(false);

            modelBuilder.Entity<FA_Maintenance>()
                .Property(e => e.MaintenanceDateBS)
                .IsUnicode(false);

            modelBuilder.Entity<FA_Maintenance>()
                .Property(e => e.ReturnedDateBS)
                .IsUnicode(false);

            modelBuilder.Entity<FA_Maintenance>()
                .Property(e => e.MaintenancePrice)
                .HasPrecision(15, 2);

            modelBuilder.Entity<FA_Maintenance>()
                .Property(e => e.MaintenanceReason)
                .IsUnicode(false);

            modelBuilder.Entity<FA_Purchase>()
                .Property(e => e.BillNo)
                .IsUnicode(false);

            modelBuilder.Entity<FA_Purchase>()
                .Property(e => e.BillDateBS)
                .IsUnicode(false);

            modelBuilder.Entity<FA_Purchase>()
                .Property(e => e.TotalAmount)
                .HasPrecision(10, 2);

            modelBuilder.Entity<FA_Purchase>()
                .Property(e => e.VatAmount)
                .HasPrecision(10, 2);

            

            modelBuilder.Entity<FA_PurchaseItem>()
                .Property(e => e.Rate)
                .HasPrecision(10, 2);

            modelBuilder.Entity<FA_PurchaseItem>()
                .Property(e => e.Total)
                .HasPrecision(10, 2);

          

            modelBuilder.Entity<FA_Vendor>()
                .Property(e => e.VendorName)
                .IsUnicode(false);

            modelBuilder.Entity<FA_Vendor>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<FA_Vendor>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<FA_Vendor>()
                .Property(e => e.Mobile)
                .IsUnicode(false);

            modelBuilder.Entity<INV_PurchaseBill>()
                .Property(e => e.BillNo)
                .IsUnicode(false);

            modelBuilder.Entity<INV_PurchaseBill>()
                .Property(e => e.BillDateBS)
                .IsUnicode(false);

            modelBuilder.Entity<INV_PurchaseBill>()
                .Property(e => e.TotalAmount)
                .HasPrecision(10, 2);

            modelBuilder.Entity<INV_PurchaseBill>()
                .Property(e => e.VatAmount)
                .HasPrecision(10, 2);

            modelBuilder.Entity<INV_PurchaseBill>()
                .Property(e => e.ShippingCharge)
                .HasPrecision(10, 2);

            modelBuilder.Entity<INV_PurchaseBill>()
                .Property(e => e.DiscountAmount)
                .HasPrecision(10, 2);

            modelBuilder.Entity<INV_PurchaseBill>()
                .Property(e => e.TotalWithVat)
                .HasPrecision(10, 0);

            modelBuilder.Entity<INV_PurchaseItem>()
                .Property(e => e.Rate)
                .HasPrecision(10, 2);

            modelBuilder.Entity<INV_PurchaseItem>()
                .Property(e => e.Total)
                .HasPrecision(10, 2);

            modelBuilder.Entity<INV_PurchaseItem>()
                .Property(e => e.ReturnRemarks)
                .IsUnicode(false);

            modelBuilder.Entity<INV_SoldBill>()
                .Property(e => e.BillDateBS)
                .IsUnicode(false);

            modelBuilder.Entity<INV_SoldBill>()
                .Property(e => e.BillNo)
                .IsUnicode(false);

            modelBuilder.Entity<INV_SoldBill>()
                .Property(e => e.GroupNo)
                .IsUnicode(false);

            modelBuilder.Entity<INV_SoldBill>()
                .Property(e => e.TotalAmount)
                .HasPrecision(10, 2);

            modelBuilder.Entity<INV_SoldItem>()
                .Property(e => e.Rate)
                .HasPrecision(10, 2);

            modelBuilder.Entity<INV_SoldItem>()
                .Property(e => e.Total)
                .HasPrecision(10, 2);

            modelBuilder.Entity<INV_Vendor>()
                .Property(e => e.VendorName)
                .IsUnicode(false);

            modelBuilder.Entity<INV_Vendor>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<INV_Vendor>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<INV_Vendor>()
                .Property(e => e.Mobile)
                .IsUnicode(false);

            modelBuilder.Entity<INV_Vendor>()
                .Property(e => e.PanNo)
                .IsUnicode(false);

            modelBuilder.Entity<MS_AcademicYear>()
                .Property(e => e.AcademicYearName)
                .IsUnicode(false);

            modelBuilder.Entity<MS_Category>()
                .Property(e => e.CategoryName)
                .IsUnicode(false);

            modelBuilder.Entity<MS_Category>()
                .Property(e => e.CategoryCode)
                .IsUnicode(false);

            modelBuilder.Entity<MS_Department>()
                .Property(e => e.DepartmentName)
                .IsUnicode(false);

            modelBuilder.Entity<MS_Department>()
                .Property(e => e.DepartmentCode)
                .IsUnicode(false);

            modelBuilder.Entity<MS_Faculty>()
                .Property(e => e.FacultyName)
                .IsUnicode(false);

            modelBuilder.Entity<MS_Item>()
                .Property(e => e.ItemName)
                .IsUnicode(false);

            modelBuilder.Entity<MS_Item>()
                .Property(e => e.ItemCode)
                .IsUnicode(false);

            modelBuilder.Entity<MS_Item>()
                .Property(e => e.ItemDescription)
                .IsUnicode(false);

            modelBuilder.Entity<MS_ItemUnit>()
                .Property(e => e.UnitSellingPrice)
                .HasPrecision(10, 2);

            modelBuilder.Entity<MS_Organization>()
                .Property(e => e.OrganizationName)
                .IsUnicode(false);

            modelBuilder.Entity<MS_Organization>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<MS_Organization>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<MS_Organization>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<MS_Organization>()
                .Property(e => e.Website)
                .IsUnicode(false);

            modelBuilder.Entity<MS_Unit>()
                .Property(e => e.UnitName)
                .IsUnicode(false);

            modelBuilder.Entity<MS_Unit>()
                .Property(e => e.UnitCode)
                .IsUnicode(false);

            modelBuilder.Entity<SC_Module>()
                .Property(e => e.ModuleName)
                .IsUnicode(false);

            modelBuilder.Entity<SC_Role>()
                .Property(e => e.RoleName)
                .IsUnicode(false);

            modelBuilder.Entity<SC_Role>()
                .Property(e => e.ModuleIds)
                .IsUnicode(false);

            modelBuilder.Entity<SC_User>()
                .Property(e => e.FullName)
                .IsUnicode(false);

            modelBuilder.Entity<SC_User>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<SC_User>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<SC_User>()
                .Property(e => e.ActivationCode)
                .IsUnicode(false);
        }

        public System.Data.Entity.DbSet<United_IMS.Web.ViewModel.AssetTransferVM> AssetTransferVMs { get; set; }

        //public System.Data.Entity.DbSet<United_IMS.Web.ViewModel.UserVM> UserVM { get; set; }
    }
}
