using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SVTrade.Models;

namespace SVTrade.Entities
{
    public interface IRepository
    {
        IQueryable<User> Users { get; }
        IQueryable<Product> Products { get; }
        IQueryable<ProductCategory> ProductCategories { get; }
        IQueryable<UserGroup> UserGroups { get; }
        
    }
}
