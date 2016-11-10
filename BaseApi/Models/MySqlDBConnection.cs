namespace BaseApi.Models
{
    using System.Data.Entity;
    using BaseModels;
    using System.Data.Entity.ModelConfiguration.Conventions;    /*********************************迁移命令**************************
     Enable-Migrations -ContextTypeName BaseApi.Models.MySqlDBConnection;
     add-migration init;
     update-database -force -v;
     *******************************************************************/

    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public partial class MySqlDBConnection : DbContext
    {
        public MySqlDBConnection() : base("name=MySqlDBContext") { }
        public static MySqlDBConnection Instance
        {
            get
            {
                return new MySqlDBConnection();
            }
        }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Function> Functions { get; set; }
        public virtual DbSet<Token> Tokens { get; set; }
        public virtual DbSet<RoleFunc> RoleFuncs { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("");
            //modelBuilder.Entity<orderdetail>().HasRequired(p => p.order).WithMany(p => p.orderdetails).Map(p => p.MapKey("a")).WillCascadeOnDelete(false);
            //modelBuilder.Entity<RoleFunc>().HasKey(t => new { t.PrecinctID, t.EmployeeID });
        }
    }

    public class ApplicationDbInitializer : DropCreateDatabaseAlways<MySqlDBConnection>
    {
        protected override void Seed(MySqlDBConnection context)
        {
            base.Seed(context);
            //try
            //{
            //    context.Clients.Add(new Client() { ClientNo = "1001", Key = "K1001" });
            //    context.Functions.Add(new Function() { FuncNo = "1001", FuncName = "登录", FuncGroupId = "1001", FuncGroup = "系统管理" });
            //    context.Roles.AddRange(new List<Role>() { new Role() { Name = "Admin" }, new Role() { Name = "User" } });
            //    context.Users.Add(new User() { UserNo = "1001", Name = "Test", Pass = "1" });
            //}
            //catch (Exception e)
            //{

            //    throw;
            //}
        }
    }
}
