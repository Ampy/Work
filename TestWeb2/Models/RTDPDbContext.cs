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



        //public DbSet<Role> Roles { get; set; }


        //public DbSet<Operation> Operations { get; set; }

        //public DbSet<User> Users { get; set; }

        //public DbSet<Module> Modules { get; set; }

        //public DbSet<RoleOperation> RoleOperations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>(); //防止ef把数据库表查询变为复数，plura       


            //modelBuilder.Conventions.Remove<AssociationInverseDiscoveryConvention>();
            //modelBuilder.Conventions.Remove<ColumnAttributeConvention>();
            //modelBuilder.Conventions.Remove<ColumnTypeCasingConvention>();
            //modelBuilder.Conventions.Remove<ComplexTypeAttributeConvention>();
            //modelBuilder.Conventions.Remove<ComplexTypeDiscoveryConvention>();
            //modelBuilder.Conventions.Remove<ComplexTypeDiscoveryConvention>();
            //modelBuilder.Conventions.Remove<ConcurrencyCheckAttributeConvention>();

            //modelBuilder.Conventions.Remove<DatabaseGeneratedAttributeConvention>();
            //modelBuilder.Conventions.Remove<DecimalPropertyConvention>();
            //modelBuilder.Conventions.Remove<DeclaredPropertyOrderingConvention>();

            ////modelBuilder.Conventions.Remove<ForeignKeyAssociationMultiplicityConvention>();
            //modelBuilder.Conventions.Remove<ForeignKeyNavigationPropertyAttributeConvention>();
            //modelBuilder.Conventions.Remove<ForeignKeyPrimitivePropertyAttributeConvention>();

            ////modelBuilder.Conventions.Remove<IdKeyDiscoveryConvention>();
            //modelBuilder.Conventions.Remove<InversePropertyAttributeConvention>();

            //modelBuilder.Conventions.Remove<KeyAttributeConvention>();
            //modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            //modelBuilder.Conventions.Remove<NavigationPropertyNameForeignKeyDiscoveryConvention>();
            //modelBuilder.Conventions.Remove<MaxLengthAttributeConvention>();

            //modelBuilder.Conventions.Remove<NotMappedPropertyAttributeConvention>();
            //modelBuilder.Conventions.Remove<NotMappedTypeAttributeConvention>();

            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            //modelBuilder.Conventions.Remove<OneToOneConstraintIntroductionConvention>();

            //modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //modelBuilder.Conventions.Remove<PrimaryKeyNameForeignKeyDiscoveryConvention>();
            //modelBuilder.Conventions.Remove<PropertyMaxLengthConvention>();

            //modelBuilder.Conventions.Remove<RequiredNavigationPropertyAttributeConvention>();
            //modelBuilder.Conventions.Remove<RequiredPrimitivePropertyAttributeConvention>();

            //modelBuilder.Conventions.Remove<StoreGeneratedIdentityKeyConvention>();
            //modelBuilder.Conventions.Remove<StringLengthAttributeConvention>();

            //modelBuilder.Conventions.Remove<TableAttributeConvention>();
            //modelBuilder.Conventions.Remove<TimestampAttributeConvention>();
            //modelBuilder.Conventions.Remove<TypeNameForeignKeyDiscoveryConvention>();

            modelBuilder.Entity<Role>().HasMany(b => b.Operations).WithMany(c => c.Roles).Map(m => { m.MapLeftKey("RoleId"); m.MapRightKey("OperationId"); m.ToTable("RoleOperation"); });
            modelBuilder.Entity<Menu>().HasMany(b => b.ChildrenMenus).WithMany(c => c.ChildrenMenus).Map(m => { m.MapLeftKey("MenuId"); m.MapRightKey("ParentMenuId"); m.ToTable("Menu"); });
      


            base.OnModelCreating(modelBuilder);


        }

        public DbSet<Menu> Menus { get; set; }




    }
}
