using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using IAIWebApp.DataHelpers;
using IAIWebApp.Models;

namespace IAIWebApp.Controllers
{
    public class AccountController : Controller
    {
        AccountDataHelper _account = new AccountDataHelper();
        BaseDataHelper _baseHelper = new BaseDataHelper();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(Login login)
        {

            try
            {                
                User _user = _account.AunthenticateUser(login.username, login.password);
                if (!string.IsNullOrEmpty(_user.EmailID))
                {
                    FormsAuthentication.SetAuthCookie(login.username, false);
                    return Json(new { data = _user, Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { data = "", Success = false, errorMessage = "InValidUser" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                var Error = "Error. <Exception:>" + "\n" + ex.Message + " ";
                return Json(new
                {
                    data = "",
                    Success = false,
                    errorMessage = Error
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return Content("Success");
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(RegisterModel _register)
        {
            try
            {
                if (string.IsNullOrEmpty(_register.Name) || string.IsNullOrEmpty(_register.EmailAddress) || _register.PrimarySkill == null ||
                    _register.SecondarySkill1 == null || string.IsNullOrEmpty(_register.MobileNumber) || string.IsNullOrEmpty(_register.Country))
                {
                    return Json(new { data = "", Success = false, errorMessage = "Please fill All Mandatory Fields." }, JsonRequestBehavior.AllowGet);
                }
                if (_register.Type == "Company")
                {
                    _register.PrimarySkill = 1;
                }
                if (IsUserExists(_register.EmailAddress))
                {
                    return Json(new { data = "", Success = false, errorMessage = "This Email Exists. Try another one" }, JsonRequestBehavior.AllowGet);
                }
                else if (IsRefered(_register.EmailAddress, _register.Type))
                {
                    Random rand = new Random();
                    string password = String.Format("{0:X6}", rand.Next(0x1000000));
                    //string Skill = ddlprimaryskill.SelectedValue.ToString();                    
                    _account.SaveInterviewer(_register.Name, _register.EmailAddress, _register.PrimarySkill, password, _register.Type, _register.SecondarySkill1, _register.MobileNumber, _register.Country);
                    string URL = "<a href='http://www.iaminterviewed.com/'>www.iaminterviewed.com </a>";

                    string body = "Hi " + _register.Name + ",<br /><br />Thank you for registering with iaminterviewed. Please find your UserName and Password below<br /><br />"

                                        + "<strong>User Name:</strong>  " + _register.EmailAddress + "<br />"
                                        + "<strong>Password:</strong>  " + password + "<br />"
                                        + "Please Click Here to Login: " + URL + "<br /><br />"
                                         + "Have a nice day...<br /><br />"
                                    + "Thanks,<br />"
                                    + "Team IAmInterviewed";
                    _baseHelper.SendMail("IAmInterviewed Registration", body, "register@iaminterviewed.com", _register.EmailAddress, true);
                    return Json(new { data = "Registered Successfully.", Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Random rand = new Random();
                    string password = String.Format("{0:X6}", rand.Next(0x1000000));
                    //string Skill = ddlprimaryskill.SelectedValue.ToString();                    
                    _account.SaveUser(_register.Name, _register.EmailAddress, _register.PrimarySkill, password, _register.Type, _register.SecondarySkill1, _register.MobileNumber, _register.Country);
                    string URL = "<a href='http://www.iaminterviewed.com/'>www.iaminterviewed.com </a>";

                    string body = "Hi " + _register.Name + ",<br /><br />Thank you for registering with iaminterviewed. Please find your UserName and Password below<br /><br />"

                                        + "<strong>User Name:</strong>  " + _register.EmailAddress + "<br />"
                                        + "<strong>Password:</strong>  " + password + "<br />"
                                        + "Please Click Here to Login: " + URL + "<br /><br />"
                                         + "Have a nice day...<br /><br />"
                                    + "Thanks,<br />"
                                    + "Team IAmInterviewed";
                    _baseHelper.SendMail("IAmInterviewed Registration", body, "register@iaminterviewed.com", _register.EmailAddress, true);

                    if (_register.Type == "Company")
                    {
                        string bodyCompany = "Hi Ram,<br /><br />New Company has been registered with iaminterviewed. Please find details below<br /><br />"

                                        + "<strong>Name:</strong>  " + _register.Name + "<br />"
                                        + "<strong>Email:</strong>  " + _register.EmailAddress + "<br />"
                                        + "<strong>Mobile No:</strong>  " + _register.MobileNumber + "<br />"
                                    + "Thanks,<br />"
                                    + "Team IAmInterviewed";
                        _baseHelper.SendMail("New Company Registered", bodyCompany, "register@iaminterviewed.com", "ramk@anterntech.com", true);
                        return Json(new { data = "Registered Successfully.", Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
                    }
                    else if (_register.Type == "Candidate")
                    {
                        string successMessage = "Registered Successfully. We have sent you the Password to your Email Id (Please check SPAM folder also). Login to Schedule Interview. Thnak You.";
                        return Json(new { data = successMessage, Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { data = "Registered Successfully.", Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                var Error = "Error. <Exception:>" + "\n" + ex.Message + " ";
                return Json(new { data = "", Success = false, errorMessage = Error }, JsonRequestBehavior.AllowGet);
                //return Content("Error = " + Error);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ForgotPassword(string EmailAddress)
        {
            try
            {
                User _user = _account.GetUserPassword(EmailAddress);
                string URL = "<a href='http://www.iaminterviewed.com/'>www.iaminterviewed.com </a>";

                string body = "Hi,<br /><br /><br />Please Find Your Password.<br /><br />"
                                    + "<strong>User Name:</strong>  " + EmailAddress + "<br />"
                                    + "<strong>Password:</strong>  " + _user.Password + "<br />"
                                     + "Regards,<br />" + "Team iAmInterviewed <br />"
                                    + "Click Here to Login:" + URL + "";

                _baseHelper.SendMail("Your Password", body, "register@iaminterviewed.com", EmailAddress, true);
                return Json(new { data = "Registered Successfully.", Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var Error = "Error. <Exception:>" + "\n" + ex.Message + " ";
                return Json(new { data = "", Success = false, errorMessage = Error }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ChangePassword(string Email, string Newpassword)
        {

            try
            {
                _account.UpdateUserPassword(Email, Newpassword);
                return Json(new { data = "Password Updated Successfully", Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var Error = "Error. <Exception:>" + "\n" + ex.Message + " ";
                return Json(new { data = "", Success = false, errorMessage = Error }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetTop10Ratings()
        {

            try
            {
                List<CandidateModel> _ratings = _account.GetTop10Ratings();
                return Json(new { data = _ratings, Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var Error = "Error. <Exception:>" + "\n" + ex.Message + " ";
                return Json(new { data = "", Success = false, errorMessage = Error }, JsonRequestBehavior.AllowGet);
            }
        }

        public bool IsUserExists(string email)
        {
            bool isValid = false;
            if (_account.IsUserExists(email) != null)
                isValid = true;

            return isValid;
        }

        public bool IsRefered(string email, string type)
        {
            bool isValid = false;
            if (type == "Interviewer")
            {
                if (_account.IsUserRefered(email) != null)
                {
                    isValid = false;
                }
                else
                {
                    isValid = true;
                }
            }
            else
            {
                isValid = false;
            }

            return isValid;
        }
    }
}