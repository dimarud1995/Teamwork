﻿using System;
using System.Linq;
using SVTrade.Abstract;
using SVTrade.Models;

namespace SVTrade.Concrete
{
    public partial class EFTradeRepository : IRepository
    {
        public IQueryable<ProductToBuy> ProductsToBuy
        {
            get { return _db.ProductsToBuy; }
        }

        public void SaveProductToBuy(ProductToBuy productToBuy)
        {
            if (productToBuy.productToBuyID == 0)
            {
                _db.ProductsToBuy.Add(productToBuy);
            }
            else
            {
                var dbEntry = _db.ProductsToBuy.Find(productToBuy.productToBuyID);
                if (dbEntry != null)
                {
                    dbEntry.title = productToBuy.title;
                    dbEntry.userID = productToBuy.userID;
                    dbEntry.description = productToBuy.description;
                    dbEntry.productCategoryID = productToBuy.productCategoryID;
                    dbEntry.amount = productToBuy.amount;
                    dbEntry.price = productToBuy.price;
                    dbEntry.approved = productToBuy.approved;
                }
            }
            _db.SaveChanges();
        }

        public ProductToBuy DeleteProductToBuy(int id)
        {
            var dbEntry = _db.ProductsToBuy.Find(id);
            if (dbEntry != null)
            {
                _db.ProductsToBuy.Remove(dbEntry);
                _db.SaveChanges();
            }
            return dbEntry;
        }
    }
}