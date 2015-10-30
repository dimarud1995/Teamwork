using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SVTrade.Models;
using SVTrade.Abstract;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Data.Entity;

namespace SVTrade
{
    static public class LoggedUserInfo
    {
        static public int currentUserId;
        static public List<User> userIfno;
        static string userGroupName;
        static private IRepository repository;
        static private IEnumerable<User> loggedUser;
        static private IEnumerable<UserGroup> loggedUserGroup;
        static TradeDBEntities db;

        static public User LocatedUser (int id)
        {
            User locatedUser = new User();
            foreach(var user in userIfno)
            {
                if (user.userID == id)
                    locatedUser = user;
            }
            return locatedUser;
        }

        public static async Task<User> findUser(string email)
        {
            db = new TradeDBEntities();
            var user = await db.Users.Where(x => x.email.Equals(email)).FirstOrDefaultAsync();
            userIfno.Add(user);
            return user;
        }

        public static string getName(int id)
        {
            User tempUser = LocatedUser(id);
            string temp;
            try
            {
                temp = Convert.ToString(tempUser.contactPerson);
            }
            catch(Exception ex) { temp = ex.Message; };
            return temp;
        }

        public static int getGroupID(int id)
        {
            User tempUser = LocatedUser(id);
            return tempUser.userGroupID;
        }

        public static string getEmail(int id)
        {
            User tempUser = LocatedUser(id);
            string temp;
            try
            {
                temp = Convert.ToString(tempUser.email);
            }
            catch (Exception ex) { temp = ex.Message; };
            return temp;
        }

        public static string getPhoneNumber(int id)
        {
            User tempUser = LocatedUser(id);
            string temp;
            try
            {
                temp = Convert.ToString(tempUser.phoneNumber);
            }
            catch (Exception ex) { temp = ex.Message; };
            return temp;
        }
        public static string getPost(int id)
        {
            User tempUser = LocatedUser(id);
            string temp;
            try
            {
                temp = Convert.ToString(tempUser.post);
            }
            catch (Exception ex) { temp = ex.Message; };

            return temp;
        }

        public static string getCompanyName(int id)
        {
            User tempUser = LocatedUser(id);
            string temp;
            try
            {
                temp = Convert.ToString(tempUser.companyName);
            }
            catch (Exception ex) { temp = ex.Message; };

            return temp;
        }

        public static bool isTrader(int id)
        {
            User tempUser = LocatedUser(id);
            bool temp = false;
            try
            {
                temp = Convert.ToBoolean(tempUser.tradeLicense);
            }
            catch { };
            return temp;
        }

        public static bool isMerchant(int id)
        {
            User tempUser = LocatedUser(id);
            bool temp = false;
            try
            {
                temp = Convert.ToBoolean(tempUser.merchantLicense);
            }
            catch { };
            return temp;
        }

        public static bool isApproved(int id)
        {
            User tempUser = LocatedUser(id);
            bool temp = false;
            try
            {
                temp = Convert.ToBoolean(tempUser.approved);
            }
            catch { };
            return temp;
        }
    }
}