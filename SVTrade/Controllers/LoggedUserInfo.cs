using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SVTrade.Models;
using SVTrade.Abstract;
using System.Web.Mvc;

namespace SVTrade
{
    static public class LoggedUserInfo
    {
        static public int currentUserId;
        static public User userIfno;
        static string userGroupName;
        static private IRepository repository;
        static private IEnumerable<User> loggedUser;
        static private IEnumerable<UserGroup> loggedUserGroup;
        static TradeDBEntities db;

        static public void SetLoggedUser (int id)
        {
            db = new TradeDBEntities();
            loggedUser = from User in db.Users where User.userID == id select User;
            userIfno = loggedUser.FirstOrDefault();

            loggedUserGroup = from UserGroup in db.UserGroups where UserGroup.userGroupID == userIfno.userGroupID select UserGroup;
            foreach (var groupName in loggedUserGroup)
            {
                userGroupName = groupName.name;
            }
        }

        public static string getName()
        {
            string temp;
            try
            {
                temp = Convert.ToString(userIfno.contactPerson);
            }
            catch(Exception ex) { temp = ex.Message; };
            return temp;
        }

        public static string getGroup()
        {
            string temp;
            try
            {
                temp = Convert.ToString(userGroupName);
            }
            catch (Exception ex) { temp = ex.Message; };
            return temp;
        }

        public static string getEmail()
        {
            string temp;
            try
            {
                temp = Convert.ToString(userIfno.email);
            }
            catch (Exception ex) { temp = ex.Message; };
            return temp;
        }

        public static string getPhoneNumber()
        {
            string temp;
            try
            {
                temp = Convert.ToString(userIfno.phoneNumber);
            }
            catch (Exception ex) { temp = ex.Message; };
            return temp;
        }
        public static string getPost()
        {
            string temp;
            try
            {
                temp = Convert.ToString(userIfno.post);
            }
            catch (Exception ex) { temp = ex.Message; };

            return temp;
        }

        public static string getCompanyName()
        {
            string temp;
            try
            {
                temp = Convert.ToString(userIfno.companyName);
            }
            catch (Exception ex) { temp = ex.Message; };

            return temp;
        }

        public static bool isTrader()
        {
            bool temp = false;
            try
            {
                temp = Convert.ToBoolean(userIfno.tradeLicense);
            }
            catch { };
            return temp;
        }

        public static bool isMerchant()
        {
            bool temp = false;
            try
            {
                temp = Convert.ToBoolean(userIfno.merchantLicense);
            }
            catch { };
            return temp;
        }

        public static bool isApproved()
        {
            bool temp = false;
            try
            {
                temp = Convert.ToBoolean(userIfno.approved);
            }
            catch { };
            return temp;
        }
    }
}