using System.Linq;
using MySql.Data.MySqlClient.Memcached;
using SVTrade.Abstract;
using SVTrade.Models;

namespace SVTrade.Concrete
{
    public partial class EFTradeRepository : IRepository
    {
        public IQueryable<User> Users
        {
            get { return _db.User; }
        }

        public void SaveUser(User user)
        {
            if (user.userID == 0)
            {
                _db.User.Add(user);
            }
            else
            {
                var dbEntry = _db.User.Find(user.userID);
                if (dbEntry != null)
                {
                    dbEntry.password = user.password;
                    dbEntry.userGroupID = user.userGroupID;
                    dbEntry.companyName = user.companyName;
                    dbEntry.juridicalAddress = user.juridicalAddress;
                    dbEntry.address = user.address;
                    dbEntry.contactPerson = user.contactPerson;
                    dbEntry.post = user.post;
                    dbEntry.phoneNumber = user.phoneNumber;
                    dbEntry.email = user.email;
                    dbEntry.merchantLicense = user.merchantLicense;
                    dbEntry.tradeLicense = user.tradeLicense;
                }
            }
            _db.SaveChanges();
        }

        public User DeleteUser(int id)
        {
            var dbEntry = _db.User.Find(id);
            if (dbEntry != null)
            {
                _db.User.Remove(dbEntry);
                _db.SaveChanges();
            }
            return dbEntry;
        }
    }
}