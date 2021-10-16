namespace Data.DataModels
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;

    public partial class CFContext : IdentityDbContext<ApplicationUser, ApplicationRole,
        string, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    // DbContext
    {
        public CFContext()
            : base("CFModel")
        {
            //this.Configuration.LazyLoadingEnabled = false;
        }
        public DbSet<ApplicationGroup> ApplicationGroups { set; get; }
        public DbSet<ApplicationRoleGroup> ApplicationRoleGroups { set; get; }
        public DbSet<ApplicationUserGroup> ApplicationUserGroups { set; get; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<EquType> EquTypes { get; set; }
        public DbSet<EquGroup> EquGroups { get; set; }
        public DbSet<Standard> Standards { get; set; }
        public DbSet<StdType> StdTypes { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Request> Requests { get; set; }

        public DbSet<Licence> Licences { set; get; }
        public DbSet<Audit> Audits { set; get; }
        public DbSet<NLog> NLogs { get; set; }
        public DbSet<Error> Errors { get; set; }

        public DbSet<WebPage> Pages { set; get; }
        public DbSet<Slide> Slides { set; get; }
        public DbSet<SysConfig> SysConfigs { set; get; }
        public DbSet<ContactDetail> ContactDetails { set; get; }
        public DbSet<Footer> Footers { set; get; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            //modelBuilder.Entity<AccHoldStatu>()
            //    .HasMany(e => e.TestAccessories)
            //    .WithOptional(e => e.AccHoldStatu)
            //    .HasForeignKey(e => e.Hold);

            //modelBuilder.Entity<DanhmucCVdi>()
            //    .Property(e => e.DocCatCode)
            //    .IsFixedLength();

            //modelBuilder.Entity<EffectToSy>()
            //    .HasMany(e => e.Incidents)
            //    .WithOptional(e => e.EffectToSy)
            //    .HasForeignKey(e => e.EffectToSysID);

            //modelBuilder.Entity<GroupEquipment>()
            //    .HasMany(e => e.EquipmentTypes)
            //    .WithOptional(e => e.GroupEquipment)
            //    .HasForeignKey(e => e.GroupID)
            //    .WillCascadeOnDelete();

            //modelBuilder.Entity<HandoverStatu>()
            //    .HasMany(e => e.Handovers)
            //    .WithOptional(e => e.HandoverStatu)
            //    .HasForeignKey(e => e.HandoverStatusID);

            //modelBuilder.Entity<IncidentStatu>()
            //    .HasMany(e => e.Incidents)
            //    .WithOptional(e => e.IncidentStatu)
            //    .HasForeignKey(e => e.StatusID);

            //modelBuilder.Entity<DocCategories>()
            //    .Property(e => e.DocCatCode)
            //    .IsFixedLength();

            //modelBuilder.Entity<Documents>()
            //    .HasMany(e => e.DocIssues)
            //    .WithRequired(e => e.Documents)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Request>()
            //    .HasMany(e => e.Opinions)
            //    .WithRequired(e => e.Request)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Roler>()
            //    .HasMany(e => e.Opinions)
            //    .WithRequired(e => e.Roler)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Role>()
            //    .HasMany(e => e.Authorizes)
            //    .WithRequired(e => e.Role)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Staff>()
            //    .HasOptional(e => e.AccessRight)
            //    .WithRequired(e => e.Staff);

            //modelBuilder.Entity<Staff>()
            //    .HasMany(e => e.Authorizes)
            //    .WithRequired(e => e.Staff)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Staff>()
            //    .HasMany(e => e.Handovers)
            //    .WithOptional(e => e.Staff)
            //    .HasForeignKey(e => e.ReceiveStaffID);

            //modelBuilder.Entity<Staff>()
            //    .HasMany(e => e.Handovers1)
            //    .WithOptional(e => e.Staff1)
            //    .HasForeignKey(e => e.HandoverStaffID);

            //modelBuilder.Entity<Staff>()
            //    .HasMany(e => e.Skills)
            //    .WithRequired(e => e.Staff)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<TestSysStatu>()
            //    .HasMany(e => e.HandoverAccs)
            //    .WithOptional(e => e.TestSysStatu)
            //    .HasForeignKey(e => e.Status);

            //modelBuilder.Entity<TestSysStatu>()
            //    .HasMany(e => e.TestAccessories)
            //    .WithOptional(e => e.TestSysStatu)
            //    .HasForeignKey(e => e.StatusID);

            //modelBuilder.Entity<TestSysStatu>()
            //    .HasMany(e => e.TestSystems)
            //    .WithOptional(e => e.TestSysStatu)
            //    .HasForeignKey(e => e.StatusID);

            //modelBuilder.Entity<DanhmucCVdi1>()
            //    .Property(e => e.DocCatCode)
            //    .IsFixedLength();

            modelBuilder.Entity<IdentityUserRole>().HasKey(i => new { i.UserId, i.RoleId }).ToTable("ApplicationUserRoles");
            modelBuilder.Entity<IdentityUserLogin>().HasKey(i => i.UserId).ToTable("ApplicationUserLogins");
            modelBuilder.Entity<IdentityUserClaim>().HasKey(i => i.UserId).ToTable("ApplicationUserClaims");

            modelBuilder.Entity<NLog>().MapToStoredProcedures(p => p.Insert(sp => sp.HasName("sp_InsertNLog")));
        }

        static CFContext()
        {
            //For No Init Database SQL Server
            //Database.SetInitializer<CFContext>(new IdentityDbInit());

            //For InitDatabase SQL Server
            Database.SetInitializer<CFContext>(new MyDbInitializer());
        }

        public static CFContext Create()
        {
            return new CFContext();
        }

        public class IdentityDbInit : NullDatabaseInitializer<CFContext>
        {
        }

        public class MyDbInitializer : CreateDatabaseIfNotExists<CFContext>, IDatabaseInitializer<CFContext>
        {
            protected override void Seed(CFContext context)
            {
                // create 3 students to seed the database
                //context.Students.Add(new Student { ID = 1, FirstName = "Mark", LastName = "Richards", EnrollmentDate = DateTime.Now });
                //context.Students.Add(new Student { ID = 2, FirstName = "Paula", LastName = "Allen", EnrollmentDate = DateTime.Now });
                //context.Students.Add(new Student { ID = 3, FirstName = "Tom", LastName = "Hoover", EnrollmentDate = DateTime.Now });

                base.Seed(context);
            }

            public override void InitializeDatabase(CFContext context)
            {
                if (!context.Database.Exists())
                {
                    // if database did not exist before - create it
                    context.Database.Create();
                    InitDatabaseProvider.SetupRolesGroups(context);
                    InitDatabaseProvider.GrantDefaultRolesForGroups(context);
                    InitDatabaseProvider.CreateSuperUser(context);
                    InitDatabaseProvider.CreateStdType();
                    InitDatabaseProvider.CreateStandard();
                    InitDatabaseProvider.CreatEquGroup();
                    InitDatabaseProvider.CreateStatus();
                    

                    InitDatabaseProvider.CreateSlide(context);
                    InitDatabaseProvider.CreatePage(context);
                    InitDatabaseProvider.CreateContactDetail(context);
                    InitDatabaseProvider.CreateConfigTitle();
                }
                else
                {
                    // query to check if MigrationHistory table is present in the database 
                    var migrationHistoryTableExists = ((IObjectContextAdapter)context).ObjectContext.ExecuteStoreQuery<int>(
                    string.Format(
                      "SELECT COUNT(*) FROM information_schema.tables WHERE table_schema = '{0}' AND table_name = '__MigrationHistory'",
                      "dbo"));

                    // if MigrationHistory table is not there (which is the case first time we run) - create it

                    if (migrationHistoryTableExists.FirstOrDefault() == 0)
                    {
                        context.Database.Delete();
                        context.Database.Create();
                        InitDatabaseProvider.SetupRolesGroups(context);
                        InitDatabaseProvider.GrantDefaultRolesForGroups(context);
                        InitDatabaseProvider.CreateSuperUser(context);
                        InitDatabaseProvider.CreateStdType();
                        InitDatabaseProvider.CreateStandard();
                        InitDatabaseProvider.CreatEquGroup();
                        InitDatabaseProvider.CreateStatus();


                        InitDatabaseProvider.CreateSlide(context);
                        InitDatabaseProvider.CreatePage(context);
                        InitDatabaseProvider.CreateContactDetail(context);
                        InitDatabaseProvider.CreateConfigTitle();
                    }
                }
            }
        }
    }
}
