﻿using BaseApi.DAL;
using CyModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

/*********************************迁移命令**************************
 //Enable-Migrations -ContextTypeName CyApi.DAL.MigrationsDB;
 //add-migration init;
 //update-database -force -v;
 *******************************************************************/
namespace CyApi.DAL
{/// <summary>
/// 用于迁移的DBCONTEXT
/// </summary>
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    //public partial class CyDBContext : GenericDBContext
    public partial class MigrationsDB : DbContext
    {
        public MigrationsDB() : base("name=MySqlDBContext") { }
        public virtual DbSet<CurOrder> CurOrders { get; set; }
        public virtual DbSet<CurOrderDetail> CurOrderDetails { get; set; }
        public virtual DbSet<Dict> Dicts { get; set; }
        public virtual DbSet<Option> Options { get; set; }
        public virtual DbSet<UserShop> UserShops { get; set; }
        public virtual DbSet<ShopInfo> ShopInfos { get; set; }
        public virtual DbSet<Bill> Bills { get; set; }
        public virtual DbSet<ChargeAccount> ChargeAccounts { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<HisOrder> HisOrders { get; set; }
        public virtual DbSet<HisOrderDetail> HisOrderDetails { get; set; }
        public virtual DbSet<OrderGroup> OrderGroups { get; set; }
        public virtual DbSet<PayType> PayTypes { get; set; }
        public virtual DbSet<Pos> Poses { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductType> ProductTypes { get; set; }
        public virtual DbSet<Tables> Tables { get; set; }
        public virtual DbSet<VipCard> VipCards { get; set; }
        public virtual DbSet<VipCardFillFlow> VipCardFillFlows { get; set; }
        public virtual DbSet<VipCardPayFlow> VipCardPayFlows { get; set; }
        public virtual DbSet<VipCardRule> VipCardRules { get; set; }
        public virtual DbSet<VipCardType> VipCardTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<orderdetail>().HasRequired(p => p.order).WithMany(p => p.orderdetails).Map(p => p.MapKey("a")).WillCascadeOnDelete(false);
            //modelBuilder.Entity<RoleFunc>().HasKey(t => new { t.PrecinctID, t.EmployeeID });
        }
    }
}