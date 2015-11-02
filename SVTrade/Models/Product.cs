//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SVTrade.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product
    {
        public Product()
        {
            this.Order = new HashSet<Order>();
            this.ReservedProduct = new HashSet<ReservedProduct>();
            this.ShowedProduct = new HashSet<ShowedProduct>();
        }

        [HiddenInput(DisplayValue = false)]
        public int productID { get; set; }

        [DisplayName("���������")]
        [Required(ErrorMessage = "���� �� ���� ���� ������")]
        public string title { get; set; }

        [DisplayName("�������� ������")]
        [Required(ErrorMessage = "������ ��������")]
        public int? productCategoryID { get; set; }

        [DisplayName("��������(���������)")]
        public string imageURL { get; set; }

        [DisplayName("ʳ������(��)")]
        [Required(ErrorMessage = "���� �� ���� ���� ������")]
        public double amount { get; set; }

        [DisplayName("ֳ��(���/��)")]
        [Required(ErrorMessage = "���� �� ���� ���� ������")]
        public double price { get; set; }

        [DisplayName("����")]
        [Required(ErrorMessage = "���� �� ���� ���� ������")]
        [DataType(DataType.MultilineText)]
        public string description { get; set; }

        [DisplayName("����������")]
        [Required(ErrorMessage = "������ �����������")]
        public int? userID { get; set; }

        [DisplayName("ϳ����������")]
        public bool approved { get; set; }
    
        public virtual ICollection<Order> Order { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<ReservedProduct> ReservedProduct { get; set; }
        public virtual ICollection<ShowedProduct> ShowedProduct { get; set; }
    }
}
