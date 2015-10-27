using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SVTrade.Models
{
    public class ConfigService
    {
        public ConfigService() { }

        /// <summary>
        /// Get the SMS gateway url
        /// </summary>
        public string SmsUrl
        {
            get { return getAppSetting(typeof(string), "SmsUrl").ToString(); }
        }

        /// <summary>
        /// Get teh gateway account to use in sending sms message
        /// </summary>
        public string SmsAccount
        {
            get { return getAppSetting(typeof(string), "SmsAccount").ToString(); }
        }

        /// <summary>
        /// Get the sub account to use for the sending of sms
        /// </summary>
        public string SubAccount
        {
            get { return getAppSetting(typeof(string), "SubAccount").ToString(); }
        }

        /// <summary>
        /// Get the sub account password for the sms gateway
        /// </summary>
        public string SubAccountPwd
        {
            get { return getAppSetting(typeof(string), "SubAccountPass").ToString(); }
        }

        private static object getAppSetting(Type expectedType, string key)
        {
            string value = ConfigurationManager.AppSettings[key]; //.Get(key);

            if (value == null)
            {
                throw new Exception(
                    string.Format("The config file does not have the key '{0}' defined in the AppSetting section.", key));
            }

            if (expectedType.Equals(typeof(int)))
            {
                return int.Parse(value);
            }

            if (expectedType.Equals(typeof(string)))
            {
                return value;
            }
            else
                throw new Exception("Type not supported.");
        }
    }
}