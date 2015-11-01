using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Threading.Tasks;

namespace SVTrade.Areas.MappingProducts.Models
{
    public class Cart
    {
        static public List<CartLine> lineCollection = new List<CartLine>();

        public void AddItem(SVTrade.Models.Product product, int idUser, int pluser)
        {

            CartLine line = lineCollection
              .Where(p => p.Product.productID == product.productID && p.idUser == idUser)
              .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Product = product,
                    Quantity = 1,
                    idUser = idUser
                });
            }
            else
                if (pluser > 0)
            {

                line.Quantity = pluser;

            }

        }

        public void RemoveLine(SVTrade.Models.Product product, int userId)
        {
            lineCollection.RemoveAll(l => l.Product.productID == product.productID && l.idUser == userId);
        }

        public double ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Product.price * e.Quantity);
        }

        public void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }
    }

    public class CartLine
    {
        public SVTrade.Models.Product Product { get; set; }
        public int idUser { get; set; }
        public int Quantity { get; set; }
    }
}