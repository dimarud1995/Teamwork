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
            get { return _db.ProductCategories; }
        }

        public void SaveProductCategory(ProductCategory productCategory)
        {
            if (productCategory.productCategoryID == 0)
            {
                _db.ProductCategories.Add(productCategory);
            }
            else
            {
                var dbEntry = _db.ProductCategories.Find(productCategory.productCategoryID);
                if (dbEntry != null)
                {
                    dbEntry.name = productCategory.name;

                }
            }
            _db.SaveChanges();
        }

        public ProductCategory DeleteProductCategory(int id)
        {
            var dbEntry = _db.ProductCategories.Find(id);
            if (dbEntry != null)
            {
                _db.ProductCategories.Remove(dbEntry);
                _db.SaveChanges();
            }
            return dbEntry;
        }
    }
}