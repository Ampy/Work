using System.Data.Entity;
using RTSafe.RTDP.Permission.Models;
using System.Configuration;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace TestWeb2.Models
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

        public DbSet<Module> Modules { get; set; }

        public DbSet<Operation> Operations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>(); //防止ef把数据库表查询变为复数，plura       

        }


    }
}
