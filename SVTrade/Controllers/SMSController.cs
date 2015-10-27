using SVTrade.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SVTrade.Controllers
{
    public class SMSController : Controller
    {
        // GET: SMS

        public PartialViewResult SendSMS()
        {
            return PartialView();
        }
        
        
        [HttpPost]
        public PartialViewResult SendSMS(SMS sms)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool isSuccess = false;
                    string errMsg = null;
                    SmsService _smsService = new SmsService();
                    string response = _smsService.Send(sms);  

                    string code = _smsService.GetResponseMessage(response, out isSuccess, out errMsg);

                    if (!isSuccess)
                    {
                        ModelState.AddModelError("", errMsg);
                        ViewBag.Message = "Error";
                    }
                    else
                    {
                        ViewBag.Message =  "Message was successfully sent.";
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return PartialView(sms);
        }
    }
}