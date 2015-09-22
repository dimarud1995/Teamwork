using System.Linq;
using SVTrade.Models;

namespace SVTrade.Abstract
{
    public interface IRepository
    {
        #region User
        IQueryable<User> Users { get; }
        void SaveUser(User user);
        User DeleteUser(int id);
        #endregion

        #region Product
        IQueryable<Product> Products { get; }
        void SaveProduct(Product product);
        Product DeleteProduct(int id);
        #endregion

        #region ProductCategory
        IQueryable<ProductCategory> ProductCategories { get; }
        void SaveProductCategory(ProductCategory productCategory);
        ProductCategory DeleteProductCategory(int id);
        #endregion

        #region UserGroup
        IQueryable<UserGroup> UserGroups { get; }
        void SaveUserGroup(UserGroup userGroup);
        UserGroup DeleteUserGroup(int id);
        #endregion

        #region Article

        IQueryable<Article> Articles { get; }
        void SaveArticle(Article article);
        Article DeleteArticle(int id);

        #endregion

        #region Order
        IQueryable<Order> Orders { get; }
        void SaveOrder(Order order);
        Order DeleteOrder(int id);
        #endregion

        #region ShowedProduct

        IQueryable<ShowedProduct> ShowedProducts { get; }
        void SaveShowedProduct(ShowedProduct showedProduct);
        ShowedProduct DeleteShowedProduct(int id);
        #endregion

        #region ChoosedCategorie

        IQueryable<ChoosedCategory> ChoosedCategories { get; }
        void SaveChoosedCategory(ChoosedCategory choosedCategory);
        ChoosedCategory DeleteChoosedCategory(int id);

        #endregion
        
    }
}
