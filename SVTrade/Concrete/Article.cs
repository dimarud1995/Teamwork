using System.Linq;
using SVTrade.Abstract;
using SVTrade.Models;

namespace SVTrade.Concrete
{
    public partial class EFTradeRepository : IRepository
    {
        public IQueryable<Article> Articles
        {
            get { return _db.Article; }
        }

        public void SaveArticle(Article article)
        {
            if (article.articleID == 0)
            {
                _db.Article.Add(article);
            }
            else
            {
                var dbEntry = _db.Article.Find(article.articleID);
                if (dbEntry != null)
                {
                    dbEntry.title = article.title;
                    dbEntry.userID = article.userID;
                    dbEntry.imageURL = article.imageURL;
                    dbEntry.description = article.description;
                    dbEntry.expirationDate = article.expirationDate;
                }
            }
            _db.SaveChanges();
        }

        public Article DeleteArticle(int id)
        {
            var dbEntry = _db.Article.Find(id);
            if (dbEntry != null)
            {
                _db.Article.Remove(dbEntry);
                _db.SaveChanges();
            }
            return dbEntry;
        }
    }
}