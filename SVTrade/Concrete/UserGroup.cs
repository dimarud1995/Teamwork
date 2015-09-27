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
    public IQueryable<UserGroup> UserGroups
    {
        get { return _db.UserGroup; }
    }

    public void SaveUserGroup(UserGroup userGroup)
    {
        if (userGroup.userGroupID == 0)
        {
            _db.UserGroup.Add(userGroup);
        }
        else
        {
            var dbEntry = _db.UserGroup.Find(userGroup.userGroupID);
            if (dbEntry != null)
            {
                dbEntry.name = userGroup.name;
            }
        }
        _db.SaveChanges();
    }

    public UserGroup DeleteUserGroup(int id)
    {
        var dbEntry = _db.UserGroup.Find(id);
        if (dbEntry != null)
        {
            _db.UserGroup.Remove(dbEntry);
            _db.SaveChanges();
        }
        return dbEntry;
    }
}
}