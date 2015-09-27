using System.Linq;
using SVTrade.Abstract;
using SVTrade.Models;

namespace SVTrade.Concrete
{
    public partial class EFTradeRepository : IRepository
    {
        public IQueryable<Product> Products
        {
            get { return _db.Products; }
        }

        public void SaveProduct(Product product)
        {
            if (product.productID == 0)
            {
                _db.Products.Add(product);
            }
            else
            {
                var dbEntry = _db.Products.Find(product.productID);
                if (dbEntry != null)
                {
                    dbEntry.name = product.name;
                    dbEntry.productCategoryID = product.productCategoryID;
                    dbEntry.imageURL = product.imageURL;
                    dbEntry.amount = product.amount;
                    dbEntry.price = product.price;
                    dbEntry.description = product.description;
                    dbEntry.userID = product.userID;
                }
            }
            _db.SaveChanges();
        }

        public Product DeleteProduct(int id)
        {
            var dbEntry = _db.Products.Find(id);
            if (dbEntry != null)
            {
                _db.Products.Remove(dbEntry);
                _db.SaveChanges();
            }
            return dbEntry;
        }
    }
}