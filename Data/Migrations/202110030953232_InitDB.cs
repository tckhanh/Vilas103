namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationGroups",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Description = c.String(),
                        Actived = c.Boolean(nullable: false),
                        Blocked = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 64),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ApplicationRoleGroups",
                c => new
                    {
                        GroupId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                        Actived = c.Boolean(nullable: false),
                        Blocked = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 64),
                    })
                .PrimaryKey(t => new { t.GroupId, t.RoleId })
                .ForeignKey("dbo.ApplicationGroups", t => t.GroupId, cascadeDelete: true)
                .ForeignKey("dbo.ApplicationRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.GroupId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.ApplicationRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Description = c.String(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ApplicationUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        ApplicationRole_Id = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.ApplicationRoles", t => t.ApplicationRole_Id)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationRole_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.ApplicationUserGroups",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        GroupId = c.String(nullable: false, maxLength: 128),
                        Actived = c.Boolean(nullable: false),
                        Blocked = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 64),
                    })
                .PrimaryKey(t => new { t.UserId, t.GroupId })
                .ForeignKey("dbo.ApplicationGroups", t => t.GroupId, cascadeDelete: true)
                .ForeignKey("dbo.ApplicationUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.ApplicationUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FullName = c.String(nullable: false),
                        Address = c.String(),
                        BirthDay = c.DateTime(),
                        FatherLand = c.String(),
                        Level = c.String(),
                        EducationalField = c.String(),
                        EntryDate = c.DateTime(),
                        OfficialDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        WorkingDuration = c.String(),
                        JobPositions = c.String(),
                        ImagePath = c.String(),
                        Locked = c.Boolean(nullable: false),
                        CityIDsScope = c.String(),
                        AreasScope = c.String(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ApplicationUserClaims",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        Id = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.ApplicationUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.Audits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SessionID = c.String(),
                        IPAddress = c.String(),
                        UserName = c.String(),
                        URLAccessed = c.String(),
                        TimeAccessed = c.DateTime(nullable: false),
                        Data = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        PhoneNo = c.String(),
                        FaxNo = c.String(),
                        ContactName = c.String(),
                        ContactPhone = c.String(),
                        TaxCompanyName = c.String(),
                        TaxAddress = c.String(),
                        TaxCode = c.String(),
                        TaxEmail = c.String(),
                        Contract = c.String(),
                        UserName = c.String(),
                        RegistedDate = c.DateTime(),
                        BlockedDate = c.DateTime(),
                        LastLoginDate = c.DateTime(),
                        FailLoginDate = c.DateTime(),
                        FailLoginTimes = c.Int(),
                        Actived = c.Boolean(nullable: false),
                        Blocked = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 64),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ContactDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Phone = c.String(),
                        Email = c.String(),
                        Website = c.String(),
                        Address = c.String(),
                        Other = c.String(),
                        Lat = c.Double(),
                        Lng = c.Double(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EquGroups",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Actived = c.Boolean(nullable: false),
                        Blocked = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 64),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EquTypes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        DisplayName = c.String(),
                        Standards = c.String(),
                        Price = c.Long(nullable: false),
                        Description = c.String(),
                        Actived = c.Boolean(nullable: false),
                        Blocked = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 64),
                        EquGroup_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EquGroups", t => t.EquGroup_Id)
                .Index(t => t.EquGroup_Id);
            
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ContractNo = c.String(),
                        CompanyId = c.Int(nullable: false),
                        EquTypeId = c.String(maxLength: 128),
                        EquGroupId = c.String(maxLength: 128),
                        EquName = c.String(),
                        Manufacturer = c.String(),
                        Model = c.String(),
                        Serial_Imei = c.String(),
                        MadeIn = c.String(),
                        Standards = c.String(),
                        Accessories = c.String(),
                        SealInfo = c.String(),
                        HoldEquipNo = c.Int(nullable: false),
                        Note = c.String(),
                        RegistedBy = c.String(),
                        ReceivedBy = c.String(),
                        AssignedBy = c.String(),
                        TestedBy = c.String(),
                        VerifiedyBy = c.String(),
                        ApprovedBy = c.String(),
                        ReturnedBy = c.String(),
                        StatusId = c.String(),
                        RegistedDate = c.DateTime(),
                        ReceivedDate = c.DateTime(),
                        AppointmentDate = c.DateTime(),
                        AssignedDate = c.DateTime(),
                        ConfirmedDate = c.DateTime(),
                        TestedDate = c.DateTime(),
                        SentToLinkLabDate = c.DateTime(),
                        ReceivedFromLinkLabDate = c.DateTime(),
                        VerifiedDate = c.DateTime(),
                        ApprovedDate = c.DateTime(),
                        SealedDate = c.DateTime(),
                        TestReportId = c.Int(nullable: false),
                        FeeId = c.String(),
                        Price = c.Long(nullable: false),
                        ReturnedDate = c.DateTime(),
                        Actived = c.Boolean(nullable: false),
                        Blocked = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 64),
                        Company_Id = c.String(maxLength: 128),
                        ReqStatus_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.Company_Id)
                .ForeignKey("dbo.EquGroups", t => t.EquGroupId)
                .ForeignKey("dbo.EquTypes", t => t.EquTypeId)
                .ForeignKey("dbo.ReqStatus", t => t.ReqStatus_Id)
                .Index(t => t.EquTypeId)
                .Index(t => t.EquGroupId)
                .Index(t => t.Company_Id)
                .Index(t => t.ReqStatus_Id);
            
            CreateTable(
                "dbo.Opinions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RequestID = c.Int(nullable: false),
                        Detail = c.String(),
                        RolerID = c.String(maxLength: 128),
                        CreateStaffID = c.String(),
                        CreateDate = c.DateTime(),
                        Notes = c.String(),
                        Request_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Requests", t => t.Request_Id)
                .ForeignKey("dbo.Rolers", t => t.RolerID)
                .Index(t => t.RolerID)
                .Index(t => t.Request_Id);
            
            CreateTable(
                "dbo.Rolers",
                c => new
                    {
                        RolerID = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.RolerID);
            
            CreateTable(
                "dbo.ReqStatus",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        LinkedIds = c.String(),
                        Description = c.String(),
                        Actived = c.Boolean(nullable: false),
                        Blocked = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 64),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Errors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        Description = c.String(),
                        Controller = c.String(),
                        StackTrace = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Footers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Content = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Licences",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        key = c.String(nullable: false),
                        enable = c.Boolean(nullable: false),
                        Actived = c.Boolean(nullable: false),
                        Blocked = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 64),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.NLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MachineName = c.String(),
                        Logged = c.DateTime(nullable: false),
                        Level = c.String(),
                        Message = c.String(),
                        Logger = c.String(),
                        Properties = c.String(),
                        UserName = c.String(),
                        Callsite = c.String(),
                        Thread = c.String(),
                        Exception = c.String(),
                        StackTrace = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Pages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Alias = c.String(nullable: false, maxLength: 8000, unicode: false),
                        Content = c.String(),
                        MetaKeyword = c.String(),
                        MetaDescription = c.String(),
                        Status = c.Boolean(nullable: false),
                        Actived = c.Boolean(nullable: false),
                        Blocked = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 64),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Slides",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        Image = c.String(),
                        Url = c.String(),
                        DisplayOrder = c.Int(),
                        Status = c.Boolean(nullable: false),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Standards",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        StdTypeId = c.String(maxLength: 128),
                        Abstract = c.String(),
                        FileName = c.String(),
                        URL = c.String(),
                        IssueDate = c.DateTime(),
                        ValidDate = c.DateTime(),
                        FeeDoc = c.String(),
                        FeePrice = c.Int(nullable: false),
                        LabID = c.String(),
                        Actived = c.Boolean(nullable: false),
                        Blocked = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 64),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StdTypes", t => t.StdTypeId)
                .Index(t => t.StdTypeId);
            
            CreateTable(
                "dbo.StdTypes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Actived = c.Boolean(nullable: false),
                        Blocked = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 64),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SystemConfigs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 8000, unicode: false),
                        Description = c.String(nullable: false),
                        ValueString = c.String(),
                        ValueInt = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateStoredProcedure(
                "dbo.sp_InsertNLog",
                p => new
                    {
                        MachineName = p.String(),
                        Logged = p.DateTime(),
                        Level = p.String(),
                        Message = p.String(),
                        Logger = p.String(),
                        Properties = p.String(),
                        UserName = p.String(),
                        Callsite = p.String(),
                        Thread = p.String(),
                        Exception = p.String(),
                        StackTrace = p.String(),
                    },
                body:
                    @"INSERT [dbo].[NLogs]([MachineName], [Logged], [Level], [Message], [Logger], [Properties], [UserName], [Callsite], [Thread], [Exception], [StackTrace])
                      VALUES (@MachineName, @Logged, @Level, @Message, @Logger, @Properties, @UserName, @Callsite, @Thread, @Exception, @StackTrace)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[NLogs]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[NLogs] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.NLog_Update",
                p => new
                    {
                        Id = p.Int(),
                        MachineName = p.String(),
                        Logged = p.DateTime(),
                        Level = p.String(),
                        Message = p.String(),
                        Logger = p.String(),
                        Properties = p.String(),
                        UserName = p.String(),
                        Callsite = p.String(),
                        Thread = p.String(),
                        Exception = p.String(),
                        StackTrace = p.String(),
                    },
                body:
                    @"UPDATE [dbo].[NLogs]
                      SET [MachineName] = @MachineName, [Logged] = @Logged, [Level] = @Level, [Message] = @Message, [Logger] = @Logger, [Properties] = @Properties, [UserName] = @UserName, [Callsite] = @Callsite, [Thread] = @Thread, [Exception] = @Exception, [StackTrace] = @StackTrace
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.NLog_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[NLogs]
                      WHERE ([Id] = @Id)"
            );
            
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.NLog_Delete");
            DropStoredProcedure("dbo.NLog_Update");
            DropStoredProcedure("dbo.sp_InsertNLog");
            DropForeignKey("dbo.Standards", "StdTypeId", "dbo.StdTypes");
            DropForeignKey("dbo.EquTypes", "EquGroup_Id", "dbo.EquGroups");
            DropForeignKey("dbo.Requests", "ReqStatus_Id", "dbo.ReqStatus");
            DropForeignKey("dbo.Opinions", "RolerID", "dbo.Rolers");
            DropForeignKey("dbo.Opinions", "Request_Id", "dbo.Requests");
            DropForeignKey("dbo.Requests", "EquTypeId", "dbo.EquTypes");
            DropForeignKey("dbo.Requests", "EquGroupId", "dbo.EquGroups");
            DropForeignKey("dbo.Requests", "Company_Id", "dbo.Companies");
            DropForeignKey("dbo.ApplicationUserRoles", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.ApplicationUserLogins", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.ApplicationUserGroups", "UserId", "dbo.ApplicationUsers");
            DropForeignKey("dbo.ApplicationUserClaims", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.ApplicationUserGroups", "GroupId", "dbo.ApplicationGroups");
            DropForeignKey("dbo.ApplicationRoleGroups", "RoleId", "dbo.ApplicationRoles");
            DropForeignKey("dbo.ApplicationUserRoles", "ApplicationRole_Id", "dbo.ApplicationRoles");
            DropForeignKey("dbo.ApplicationRoleGroups", "GroupId", "dbo.ApplicationGroups");
            DropIndex("dbo.Standards", new[] { "StdTypeId" });
            DropIndex("dbo.Opinions", new[] { "Request_Id" });
            DropIndex("dbo.Opinions", new[] { "RolerID" });
            DropIndex("dbo.Requests", new[] { "ReqStatus_Id" });
            DropIndex("dbo.Requests", new[] { "Company_Id" });
            DropIndex("dbo.Requests", new[] { "EquGroupId" });
            DropIndex("dbo.Requests", new[] { "EquTypeId" });
            DropIndex("dbo.EquTypes", new[] { "EquGroup_Id" });
            DropIndex("dbo.ApplicationUserLogins", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ApplicationUserClaims", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ApplicationUserGroups", new[] { "GroupId" });
            DropIndex("dbo.ApplicationUserGroups", new[] { "UserId" });
            DropIndex("dbo.ApplicationUserRoles", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ApplicationUserRoles", new[] { "ApplicationRole_Id" });
            DropIndex("dbo.ApplicationRoleGroups", new[] { "RoleId" });
            DropIndex("dbo.ApplicationRoleGroups", new[] { "GroupId" });
            DropTable("dbo.SystemConfigs");
            DropTable("dbo.StdTypes");
            DropTable("dbo.Standards");
            DropTable("dbo.Slides");
            DropTable("dbo.Pages");
            DropTable("dbo.NLogs");
            DropTable("dbo.Licences");
            DropTable("dbo.Footers");
            DropTable("dbo.Errors");
            DropTable("dbo.ReqStatus");
            DropTable("dbo.Rolers");
            DropTable("dbo.Opinions");
            DropTable("dbo.Requests");
            DropTable("dbo.EquTypes");
            DropTable("dbo.EquGroups");
            DropTable("dbo.ContactDetails");
            DropTable("dbo.Companies");
            DropTable("dbo.Audits");
            DropTable("dbo.ApplicationUserLogins");
            DropTable("dbo.ApplicationUserClaims");
            DropTable("dbo.ApplicationUsers");
            DropTable("dbo.ApplicationUserGroups");
            DropTable("dbo.ApplicationUserRoles");
            DropTable("dbo.ApplicationRoles");
            DropTable("dbo.ApplicationRoleGroups");
            DropTable("dbo.ApplicationGroups");
        }
    }
}
