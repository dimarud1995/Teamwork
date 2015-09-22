using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SVTrade.Abstract;
using SVTrade.Models;

namespace SVTrade.Concrete
{
    public partial class EFTradeRepository : IRepository
    {
        public IQueryable<ProductCategory> ProductCategories
        {
            get { return _db.ProductCategory; }
        }

        public void SaveProductCategory(ProductCategory productCategory)
        {
            if (productCategory.productCategoryID == 0)
            {
                _db.ProductCategory.Add(productCategory);
            }
            else
            {
                var dbEntry = _db.ProductCategory.Find(productCategory.productCategoryID);
                if (dbEntry != null)
                {
                    dbEntry.name = productCategory.name;

                }
            }
            _db.SaveChanges();
        }

        public ProductCategory DeleteProductCategory(int id)
        {
            var dbEntry = _db.ProductCategory.Find(id);
            if (dbEntry != null)
            {
                _db.ProductCategory.Remove(dbEntry);
                _db.SaveChanges();
            }
            return dbEntry;
        }
    }
}