namespace BaseApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Client",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientNo = c.String(maxLength: 255, storeType: "nvarchar"),
                        ClientName = c.String(maxLength: 255, storeType: "nvarchar"),
                        Key = c.String(maxLength: 255, storeType: "nvarchar"),
                        Modifier = c.String(maxLength: 60, storeType: "nvarchar"),
                        ModifyTime = c.DateTime(nullable: false, precision: 0, storeType: "timestamp"),
                        Creator = c.String(maxLength: 60, storeType: "nvarchar"),
                        CreateTime = c.DateTime(precision: 0, storeType: "timestamp"),
                        IsDeleted = c.Boolean(),
                        SortId = c.Int(),
                        IsSync = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Function",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FuncNo = c.String(maxLength: 255, storeType: "nvarchar"),
                        FuncName = c.String(maxLength: 255, storeType: "nvarchar"),
                        FuncGroupId = c.String(maxLength: 255, storeType: "nvarchar"),
                        FuncGroup = c.String(maxLength: 255, storeType: "nvarchar"),
                        Add = c.Boolean(),
                        Mod = c.Boolean(),
                        Del = c.Boolean(),
                        Qry = c.Boolean(),
                        Modifier = c.String(maxLength: 60, storeType: "nvarchar"),
                        ModifyTime = c.DateTime(nullable: false, precision: 0, storeType: "timestamp"),
                        Creator = c.String(maxLength: 60, storeType: "nvarchar"),
                        CreateTime = c.DateTime(precision: 0, storeType: "timestamp"),
                        IsDeleted = c.Boolean(),
                        SortId = c.Int(),
                        IsSync = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Role",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 255, storeType: "nvarchar"),
                        Modifier = c.String(maxLength: 60, storeType: "nvarchar"),
                        ModifyTime = c.DateTime(nullable: false, precision: 0, storeType: "timestamp"),
                        Creator = c.String(maxLength: 60, storeType: "nvarchar"),
                        CreateTime = c.DateTime(precision: 0, storeType: "timestamp"),
                        IsDeleted = c.Boolean(),
                        SortId = c.Int(),
                        IsSync = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserNo = c.String(maxLength: 255, storeType: "nvarchar"),
                        Name = c.String(maxLength: 255, storeType: "nvarchar"),
                        Pass = c.String(maxLength: 255, storeType: "nvarchar"),
                        Modifier = c.String(maxLength: 60, storeType: "nvarchar"),
                        ModifyTime = c.DateTime(nullable: false, precision: 0, storeType: "timestamp"),
                        Creator = c.String(maxLength: 60, storeType: "nvarchar"),
                        CreateTime = c.DateTime(precision: 0, storeType: "timestamp"),
                        IsDeleted = c.Boolean(),
                        SortId = c.Int(),
                        IsSync = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Token",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClientNo = c.String(maxLength: 255, storeType: "nvarchar"),
                        ClientType = c.String(maxLength: 255, storeType: "nvarchar"),
                        Scope = c.String(maxLength: 1000, storeType: "nvarchar"),
                        UserName = c.String(maxLength: 255, storeType: "nvarchar"),
                        AccessToken = c.String(maxLength: 255, storeType: "nvarchar"),
                        RefreshToken = c.String(maxLength: 255, storeType: "nvarchar"),
                        IssuedUtc = c.DateTime(nullable: false, precision: 0),
                        ExpiresUtc = c.DateTime(nullable: false, precision: 0),
                        IpAddress = c.String(maxLength: 255, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "RoleFunctions",
                c => new
                    {
                        Role_Id = c.Int(nullable: false),
                        Function_Id = c.Int(nullable: false),
                        Add = c.Boolean(),
                        Mod = c.Boolean(),
                        Del = c.Boolean(),
                        Qry = c.Boolean(),
                    })
                .PrimaryKey(t => new { t.Role_Id, t.Function_Id })
                .ForeignKey("Role", t => t.Role_Id, cascadeDelete: true)
                .ForeignKey("Function", t => t.Function_Id, cascadeDelete: true)
                .Index(t => t.Role_Id)
                .Index(t => t.Function_Id);

            CreateTable(
                "UserRoles",
                c => new
                    {
                        User_Id = c.Int(nullable: false),
                        Role_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Role_Id })
                .ForeignKey("User", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("Role", t => t.Role_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Role_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Token", "UserId", "User");
            DropForeignKey("UserRole1", "Role_Id", "Role");
            DropForeignKey("UserRole1", "User_Id", "User");
            DropForeignKey("RoleFunctions1", "Function_Id", "Function");
            DropForeignKey("RoleFunctions1", "Role_Id", "Role");
            DropIndex("UserRole1", new[] { "Role_Id" });
            DropIndex("UserRole1", new[] { "User_Id" });
            DropIndex("RoleFunctions1", new[] { "Function_Id" });
            DropIndex("RoleFunctions1", new[] { "Role_Id" });
            DropIndex("Token", new[] { "UserId" });
            DropTable("UserRole1");
            DropTable("RoleFunctions1");
            DropTable("UserRoles");
            DropTable("RoleFunctions");
            DropTable("Token");
            DropTable("User");
            DropTable("Role");
            DropTable("Function");
            DropTable("Client");
        }
    }
}
