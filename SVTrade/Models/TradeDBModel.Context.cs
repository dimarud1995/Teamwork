﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SVTrade.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TradeDBEntities : DbContext
    {
        public TradeDBEntities()
            : base("name=TradeDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Article> Article { get; set; }
        public virtual DbSet<ChoosedCategory> ChoosedCategory { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductCategory> ProductCategory { get; set; }
        public virtual DbSet<ShowedProduct> ShowedProduct { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserGroup> UserGroup { get; set; }
        public virtual DbSet<UserToProduct> UserToProduct { get; set; }
    }
}
