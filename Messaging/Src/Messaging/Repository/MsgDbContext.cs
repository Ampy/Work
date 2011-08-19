using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using RTSafe.RTDP.Messaging.Models;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace RTSafe.RTDP.Messaging.Repository
{
    public class MsgDbContext: DbContext
    {
        public DbSet<Message> Messages { get; set; }
        public DbSet<Category> Categories { get; set; }
        //public DbSet<MessageStatus> MessageStatus { get; set; }

        public MsgDbContext(string connstring)
            : base(connstring)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>(); 
            //modelBuilder.Entity<MessageModel>().ToTable("Message");
            modelBuilder.Conventions.Remove<IncludeMetadataConvention>();
            // Relationships
            //modelBuilder.Entity<Models.MessageStatusModel>().HasOptional(t => t.Message)
            //    .WithMany(t => t.MessageStatus)
            //    .HasForeignKey(d => d.MessageId);

            modelBuilder.Entity<Message>().HasMany(b => b.Categories).WithMany(c => c.Messages).Map(m => { m.MapLeftKey("MessageId"); m.MapRightKey("CategoryId"); m.ToTable("MessageStatus"); });
        }
    }
}
