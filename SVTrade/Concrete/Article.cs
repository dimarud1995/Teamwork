using System;
using System.Linq;
using SVTrade.Abstract;
using SVTrade.Models;

namespace SVTrade.Concrete
{
    public partial class EFTradeRepository : IRepository
    {
        public IQueryable<Article> Articles
        {
            get { return _db.Articles; }
        }

        public void SaveArticle(Article article)
        {
            if (article.articleID == 0)
            {
                _db.Articles.Add(article);
            }
            else
            {
                var dbEntry = _db.Articles.Find(article.articleID);
                if (dbEntry != null)
                {
                    dbEntry.title = article.title;
                    dbEntry.userID = article.userID;
                    dbEntry.imageURL = article.imageURL;
                    dbEntry.description = article.description;
                    dbEntry.date = article.date;
                }
            }
            _db.SaveChanges();
        }

        public Article DeleteArticle(int id)
        {
            var dbEntry = _db.Articles.Find(id);
            if (dbEntry != null)
            {
                _db.Articles.Remove(dbEntry);
                _db.SaveChanges();
            }
            return dbEntry;
        }
    }
}