using System.Linq;
using SVTrade.Abstract;
using SVTrade.Models;

namespace SVTrade.Concrete
{
    public partial class EFTradeRepository : IRepository
    {
    public IQueryable<UserGroup> UserGroups
    {
        get { return _db.UserGroups; }
    }

    public void SaveUserGroup(UserGroup userGroup)
    {
        if (userGroup.userGroupID == 0)
        {
            _db.UserGroups.Add(userGroup);
        }
        else
        {
            var dbEntry = _db.UserGroups.Find(userGroup.userGroupID);
            if (dbEntry != null)
            {
                dbEntry.name = userGroup.name;
            }
        }
        _db.SaveChanges();
    }

    public UserGroup DeleteUserGroup(int id)
    {
        var dbEntry = _db.UserGroups.Find(id);
        if (dbEntry != null)
        {
            _db.UserGroups.Remove(dbEntry);
            _db.SaveChanges();
        }
        return dbEntry;
    }
}
}