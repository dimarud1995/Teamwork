﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SVTrade.Models;
using SVTrade.Abstract;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web.Security;

namespace SVTrade
{
    static public class LoggedUserInfo
    {
        static public int currentUserId;
        static private List<User> onlineUsersList = new List<User>();
        static private IRepository repository;
        static TradeDBEntities db;

        static public List<User> GetOnlineUsers()
        {
            return onlineUsersList;
        }

        // Serach the user in Online Users List;
        static private User LocatedUser (int id)
        {
            User locatedUser = new User();
            foreach(var user in onlineUsersList)
            {
                if (user.userID == id)
                    locatedUser = user;
            }
            return locatedUser;
        }

        // Search user info in Data Base and add him in Online Users List;
        public static async Task<User> FindUser(string email)
        {
            db = new TradeDBEntities();
            var user = await db.Users.Where(x => x.email.Equals(email)).FirstOrDefaultAsync();
            onlineUsersList.Add(user);
            return user;
        }

        public static void RemoveLoggedUser(int id)
        {
            User tempUser = LocatedUser(id);
            onlineUsersList.Remove(tempUser);
        }

        // Check if the user with cookie exist in Online Users List;
        // If not then add him in;
        private static void ValidateUser(int id)
        {
            try
            {
                User locatedUser = LocatedUser(id);
                if (locatedUser.contactPerson == null && HttpContext.Current.Request.Cookies["name"] != null)
                {
                    db = new TradeDBEntities();
                    var user = db.Users.Where(x => x.userID.Equals(id)).FirstOrDefault();
                    onlineUsersList.Add(user);
                }
            }
            catch { };
        }

        public static string GetName(int id)
        {
            ValidateUser(id);
            User tempUser = LocatedUser(id);
            string temp;
            try
            {
                temp = Convert.ToString(tempUser.contactPerson);
            }
            catch (Exception ex) { temp = ex.Message; };
            return temp;
        }

        public static int GetGroupID(int id)
        {
            ValidateUser(id);
            int tempID = 1;
            try
            {
                User tempUser = LocatedUser(id);
                tempID = tempUser.userGroupID;
            }
            catch { };
            return tempID;
        }

        public static string GetEmail(int id)
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

        public static string GetPhoneNumber(int id)
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
        public static string GetPost(int id)
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

        public static string GetCompanyName(int id)
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

        public static bool IsTrader(int id)
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

        public static bool IsMerchant(int id)
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

        public static bool IsApproved(int id)
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