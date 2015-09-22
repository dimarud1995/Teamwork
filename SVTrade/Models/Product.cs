//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Product
    {
        public Product()
        {
            this.Order = new HashSet<Order>();
            this.ShowedProduct = new HashSet<ShowedProduct>();
        }
    
        public int productID { get; set; }
        public string name { get; set; }
        public int productCategoryID { get; set; }
        public string imageURL { get; set; }
        public double amount { get; set; }
        public double price { get; set; }
        public string description { get; set; }
        public int userID { get; set; }
    
        public virtual ICollection<Order> Order { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<ShowedProduct> ShowedProduct { get; set; }
    }
}
