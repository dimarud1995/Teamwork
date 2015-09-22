using System.Linq;
using SVTrade.Abstract;
using SVTrade.Models;

namespace SVTrade.Concrete
{
    public partial class EFTradeRepository : IRepository
    {
        public IQueryable<ChoosedCategory> ChoosedCategories
        {
            get { return _db.ChoosedCategory; }
        }

        public void SaveChoosedCategory(ChoosedCategory choosedCategory)
        {
            if (choosedCategory.chosenCategoryID == 0)
            {
                _db.ChoosedCategory.Add(choosedCategory);
            }
            else
            {
                var dbEntry = _db.ChoosedCategory.Find(choosedCategory.chosenCategoryID);
                if (dbEntry != null)
                {
                    dbEntry.productCategoryID = choosedCategory.productCategoryID;
                    dbEntry.userID = choosedCategory.userID;
                }
            }
            _db.SaveChanges();
        }

        public ChoosedCategory DeleteChoosedCategory(int id)
        {
            var dbEntry = _db.ChoosedCategory.Find(id);
            if (dbEntry != null)
            {
                _db.ChoosedCategory.Remove(dbEntry);
                _db.SaveChanges();
            }
            return dbEntry;
        }
    }
}