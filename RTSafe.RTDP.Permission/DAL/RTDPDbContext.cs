using System.Data.Entity;
using RTSafe.RTDP.Permission.Models;
using System.Configuration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Infrastructure;

namespace RTSafe.RTDP.Permission.DAL
{
    public class RTDPDbContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<TestWeb2.Models.ModuleContext>());

        public RTDPDbContext()
            : base()
        {

        }



        public DbSet<Role> Roles { get; set; }

        public DbSet<Operation> Operations { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Module> Modules { get; set; }

        public DbSet<UserGroup> UserGroups { get; set; }

        public DbSet<Menu> Menus { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>(); //防止ef把数据库表查询变为复数，plura       
            modelBuilder.Conventions.Remove<IncludeMetadataConvention>();
            
            modelBuilder.Entity<Role>().HasMany(b => b.Operations).WithMany(c => c.Roles).Map(m => { m.MapLeftKey("RoleId"); m.MapRightKey("OperationId"); m.ToTable("RoleOperation"); });
            modelBuilder.Entity<User>().HasMany(b => b.Roles).WithMany(c => c.Users).Map(m => { m.MapLeftKey("UserId"); m.MapRightKey("RoleId"); m.ToTable("UserRole"); });
            modelBuilder.Entity<UserGroup>().HasMany(b => b.Roles).WithMany(c => c.UserGroups).Map(m => { m.MapLeftKey("UserGroupId"); m.MapRightKey("RoleId"); m.ToTable("UserGroupRole"); });
            modelBuilder.Entity<Menu>().HasOptional(t => t.ParentMenu).WithMany(t => t.ChildrenMenus).HasForeignKey(d => d.ParentMenuId);
            modelBuilder.Entity<Role>().HasMany(b => b.Menus).WithMany(c => c.Roles).Map(m => { m.MapLeftKey("RoleId"); m.MapRightKey("MenuId"); m.ToTable("RoleMenu"); });

            base.OnModelCreating(modelBuilder);
        }

    }
}
