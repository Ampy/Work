﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using RTSafe.RTDP.Logger.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace RTSafe.RTDP.Logger.Repository
{
    public class LoggerDbContext:DbContext
    {
        public DbSet<BrowseTrace> BrowseTraces { get; set; }

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


            base.OnModelCreating(modelBuilder);


        }

        
    }
}
