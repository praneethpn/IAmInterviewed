using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using IAIWebApp.DataHelpers;
using IAIWebApp.Models;
using System.Configuration;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;
using Microsoft.Office.Interop.Word;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;

namespace IAIWebApp.Controllers
{
    public class InterviewerController : Controller
    {
        InterviewerDataHelper _interviewerDataHelper = new InterviewerDataHelper();
        CandidateDataHelper _candidateDataHelper = new CandidateDataHelper();
        AccountDataHelper _account = new AccountDataHelper();
        BaseDataHelper _baseHelper = new BaseDataHelper();
        // GET: Interviewer
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult getInterviews(int UserId)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                serializer.MaxJsonLength = Int32.MaxValue;
                List<InterviewerModel> _todaysInterviews = _interviewerDataHelper.GetTodaysInterviews(UserId);
                List<InterviewerModel> _interviewsToBeConfirmed = _interviewerDataHelper.GetInterviewsToBeConfirmed(UserId);
                List<InterviewerModel> _interviewsToBeRated = _interviewerDataHelper.GetInterviewsToBeRated(UserId);
                List<InterviewerModel> _interviewsCompleted = _interviewerDataHelper.GetInterviewsCompleted(UserId);
                List<InterviewerModel> _otherInterviews = _interviewerDataHelper.GetOthersInterviews(UserId);
                var resultData = new { TodaysInterviews = _todaysInterviews, InterviewsToBeConfirmed = _interviewsToBeConfirmed, InterviewsToBeRated = _interviewsToBeRated, InterviewsCompleted = _interviewsCompleted, OtherInterviews = _otherInterviews };
                var jsonResult = Json(new { data = resultData, Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                //var result = new ContentResult
                //{
                //    Content = serializer.Serialize(resultData),
                //    ContentType = "application/json"
                //};
                return jsonResult;
            }
            catch (Exception ex)
            {
                var Error = "Error. <Exception:>" + "\n" + ex.Message + " ";
                return Json(new { data = "", Success = false, errorMessage = Error }, JsonRequestBehavior.AllowGet);
                //return Content("Error = " + Error);
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult updateInterviewStatus(int ScheduleId, string Remarks)
        {
            try
            {
                string Status = "";
                _interviewerDataHelper.UpdateInterviewStatus(ScheduleId, Status, Remarks);
                return Json(new { data = "", Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var Error = "Error. <Exception:>" + "\n" + ex.Message + " ";
                return Json(new { data = "", Success = false, errorMessage = Error }, JsonRequestBehavior.AllowGet);
                //return Content("Error = " + Error);
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult confirmInterview(int ScheduleId, int UserId, string InterviewDateTime, string CandidateEmail)
        {
            try
            {
                _interviewerDataHelper.ConfirmInterview(ScheduleId, UserId);
                string URL = "<a href='http://www.iaminterviewed.com/'>www.iaminterviewed.com </a>";
                string body = "Hi,<br /><br /><br />Your Interview has been confirmed on " + InterviewDateTime + "<br /><br />"
                                       + "All the best.<br /><br />"
                                        + "Regards,<br />" + "Team iAmInterviewed <br /><br />"
                                       + "Please login here" + URL + " for more details.";

                _baseHelper.SendMail("Interview Confirmation", body, "Admin@iaminterviewed.com", CandidateEmail, true);
                return Json(new { data = "", Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var Error = "Error. <Exception:>" + "\n" + ex.Message + " ";
                return Json(new { data = "", Success = false, errorMessage = Error }, JsonRequestBehavior.AllowGet);
                //return Content("Error = " + Error);
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult getInterviewSchedulebyId(int ScheduleId)
        {
            try
            {
                List<InterviewerModel> _interviewSchedule = _candidateDataHelper.GetInterviewSchedulebyId(ScheduleId);
                return Json(new { data = _interviewSchedule, Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var Error = "Error. <Exception:>" + "\n" + ex.Message + " ";
                return Json(new { data = "", Success = false, errorMessage = Error }, JsonRequestBehavior.AllowGet);
                //return Content("Error = " + Error);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult updateRating(InterviewerRatingModel _ratingModel)
        {
            try
            {
                if (string.IsNullOrEmpty(_ratingModel.SecondarySkill4Rating) && string.IsNullOrEmpty(_ratingModel.SecondarySkill5Rating))
                {
                    double skill1 = Convert.ToDouble(_ratingModel.SecondarySkill1Rating);
                    double skill2 = Convert.ToDouble(_ratingModel.SecondarySkill2Rating);
                    double skill3 = Convert.ToDouble(_ratingModel.SecondarySkill3Rating);
                    double rating = (skill1 + skill2 + skill3) / 3;
                    rating = Math.Round(rating, 2);
                    _ratingModel.TotalRating = rating.ToString();
                }
                else if (string.IsNullOrEmpty(_ratingModel.SecondarySkill4Rating))
                {
                    double skill1 = Convert.ToDouble(_ratingModel.SecondarySkill1Rating);
                    double skill2 = Convert.ToDouble(_ratingModel.SecondarySkill2Rating);
                    double skill3 = Convert.ToDouble(_ratingModel.SecondarySkill3Rating);
                    double skill5 = Convert.ToDouble(_ratingModel.SecondarySkill5Rating);
                    double rating = (skill1 + skill2 + skill3 + skill5) / 4;
                    rating = Math.Round(rating, 2);
                    _ratingModel.TotalRating = rating.ToString();
                }
                else if (string.IsNullOrEmpty(_ratingModel.SecondarySkill5Rating))
                {
                    double skill1 = Convert.ToDouble(_ratingModel.SecondarySkill1Rating);
                    double skill2 = Convert.ToDouble(_ratingModel.SecondarySkill2Rating);
                    double skill3 = Convert.ToDouble(_ratingModel.SecondarySkill3Rating);
                    double skill4 = Convert.ToDouble(_ratingModel.SecondarySkill4Rating);
                    double rating = (skill1 + skill2 + skill3 + skill4) / 4;
                    rating = Math.Round(rating, 2);
                    _ratingModel.TotalRating = rating.ToString();
                }
                else
                {
                    double skill1 = Convert.ToDouble(_ratingModel.SecondarySkill1Rating);
                    double skill2 = Convert.ToDouble(_ratingModel.SecondarySkill2Rating);
                    double skill3 = Convert.ToDouble(_ratingModel.SecondarySkill3Rating);
                    double skill4 = Convert.ToDouble(_ratingModel.SecondarySkill4Rating);
                    double skill5 = Convert.ToDouble(_ratingModel.SecondarySkill5Rating);
                    double rating = (skill1 + skill2 + skill3 + skill4 + skill5) / 5;
                    rating = Math.Round(rating, 2);
                    _ratingModel.TotalRating = rating.ToString();
                }
                _interviewerDataHelper.UpdateRating(_ratingModel);
                List<CompanyModel> _EmialNotification = _candidateDataHelper.GetRecruiterDetailsByCandidateId(_ratingModel.CandidateId);
                if (_EmialNotification.Count > 0)
                {
                    string URLNotification = "<a href='http://www.iaminterviewed.com/'>www.iaminterviewed.com </a>";

                    string bodyNotification = "Hi,<br /><br />" + _ratingModel.CandidateEmail + " candidate mapped to below job requirement has been Rated by Interviewer<br /><br />"

                                    + "<strong>JobCode :</strong>  " + _EmialNotification[0].JobCode + "<br />"
                                    + "Please click here to Login and View Candidate Ratings: " + URLNotification + "<br /><br />"
                                    + "Have a nice day...<br /><br />"
                                    + "Thanks,<br />"
                                    + "Team IAmInterviewed";
                    if (!string.IsNullOrEmpty(_EmialNotification[0].RecruiterEmail) && _EmialNotification[0].RecruiterEmail != "--")
                    {
                        _baseHelper.SendMail("IAmInterviewed Notification", bodyNotification, "register@iaminterviewed.com", _EmialNotification[0].RecruiterEmail, true);
                    }

                    if (!string.IsNullOrEmpty(_EmialNotification[0].AssignToEmail) && _EmialNotification[0].AssignToEmail != _EmialNotification[0].RecruiterEmail && _EmialNotification[0].AssignToEmail != "--")
                    {
                        _baseHelper.SendMail("IAmInterviewed Notification", bodyNotification, "register@iaminterviewed.com", _EmialNotification[0].AssignToEmail, true);
                    }

                    if (!string.IsNullOrEmpty(_EmialNotification[0].PMEmail) && _EmialNotification[0].PMEmail != _EmialNotification[0].RecruiterEmail && _EmialNotification[0].PMEmail != "--")
                    {
                        _baseHelper.SendMail("IAmInterviewed Notification", bodyNotification, "register@iaminterviewed.com", _EmialNotification[0].PMEmail, true);
                    }
                }
                else
                {
                    string URL = "<a href='http://www.iaminterviewed.com/'>www.iaminterviewed.com </a>";
                    string body = "Hi,<br /><br /><br />Your Interview on " + _ratingModel.InterviewDateTime + " has been rated by the interviewer.<br /><br />"
                                           + "You need to accept the Rating in order to appear for the Employer.<br /><br />"
                                            + "Thanks,<br />" + "Team IAmInterviewed <br /><br />"
                                           + "Please check your Rating by loging into " + URL + "";

                    //SendMailwithAttachment(ConfigurationHelper.ConfigurationHelper.SmtpHost
                    //        , ConfigurationHelper.ConfigurationHelper.MailFrom
                    //                    , ConfigurationHelper.ConfigurationHelper.SmtpPassword, "Your Rating", body, "Admin@iaminterviewed.com", lblCandidateEmail.Text.Trim(), true,Path);

                    _baseHelper.SendMail("Your Rating", body, "Admin@iaminterviewed.com", _ratingModel.CandidateEmail, true);
                }
                return Json(new { data = "", Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var Error = "Error. <Exception:>" + "\n" + ex.Message + " ";
                return Json(new { data = "", Success = false, errorMessage = Error }, JsonRequestBehavior.AllowGet);
                //return Content("Error = " + Error);
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult getInterviewerProfile(int CandidateId)
        {
            try
            {
                List<CandidateModel> _candidate = _candidateDataHelper.GetCandidateProfile(CandidateId);
                // Now if our password was enctypted or hashed we would have done the
                // same operation on the user entered password here, But for now
                // since the password is in plain text lets just authenticate directly
                return Json(new { data = _candidate, Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
                //return Content(HttpStatusCode.OK, new { data = slills, Success = true, errorMessage = "" });
                //return Request.CreateResponse(HttpStatusCode.OK, new { data = slills, Success = true, errorMessage = "" });
                //return Content(slills);
            }
            catch (Exception ex)
            {
                var Error = "Error. <Exception:>" + "\n" + ex.Message + " ";
                return Json(new { data = "", Success = false, errorMessage = Error }, JsonRequestBehavior.AllowGet);
                //return Content("Error = " + Error);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult saveInterviewerProfile(CandidateProfileModel _details)
        {
            try
            {
                _candidateDataHelper.SaveCandidateProfile(_details);
                return Json(new { data = "Candidate Details Added Successfully.", Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var Error = "Error. <Exception:>" + "\n" + ex.Message + " ";
                return Json(new { data = "", Success = false, errorMessage = Error }, JsonRequestBehavior.AllowGet);
                //return Content("Error = " + Error);
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult getTimeSlots(int UserId, DateTime ScheduleDate)
        {
            try
            {
                List<InterviewerModel> _allTimeSlots = _interviewerDataHelper.GetDailyScheduleTimeAll(UserId, ScheduleDate);
                List<InterviewerModel> _publishedTimeSlots = _interviewerDataHelper.GetDailyScheduleTimePublished(UserId, ScheduleDate);
                // Now if our password was enctypted or hashed we would have done the
                // same operation on the user entered password here, But for now
                // since the password is in plain text lets just authenticate directly
                return Json(new { data = new { AllTimeSlots = _allTimeSlots, PublishedTimeSlots = _publishedTimeSlots }, Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
                //return Content(HttpStatusCode.OK, new { data = slills, Success = true, errorMessage = "" });
                //return Request.CreateResponse(HttpStatusCode.OK, new { data = slills, Success = true, errorMessage = "" });
                //return Content(slills);
            }
            catch (Exception ex)
            {
                var Error = "Error. <Exception:>" + "\n" + ex.Message + " ";
                return Json(new { data = "", Success = false, errorMessage = Error }, JsonRequestBehavior.AllowGet);
                //return Content("Error = " + Error);
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult saveInterviewerSchedule(int UserId, DateTime ScheduleDate, string AvailableTime)
        {
            try
            {
                List<CandidateModel> _candidatelist = _candidateDataHelper.GetCandidateProfile(UserId);
                if (_candidatelist.Count > 0)
                {
                    _interviewerDataHelper.UpdateInterviewSchedule(UserId, ScheduleDate);
                    string[] TimeslotsSplit = AvailableTime.Split(',');
                    foreach (string time in TimeslotsSplit)
                    {
                        _interviewerDataHelper.SaveInterviewSchedule(UserId, ScheduleDate, time);
                    }
                    return Json(new { data = "Schedules Added Successfully.", Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { data = "", Success = false, errorMessage = "Please fill your details before publishing schedules." }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                var Error = "Error. <Exception:>" + "\n" + ex.Message + " ";
                return Json(new { data = "", Success = false, errorMessage = Error }, JsonRequestBehavior.AllowGet);
                //return Content("Error = " + Error);
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult getInterviewerSchedulesByDate(int UserId, DateTime ScheduleDate)
        {
            try
            {
                List<InterviewerModel> _allTimeSlots = _interviewerDataHelper.GetInterviewerSchedulesByDate(UserId, ScheduleDate);
                return Json(new { data = _allTimeSlots, Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var Error = "Error. <Exception:>" + "\n" + ex.Message + " ";
                return Json(new { data = "", Success = false, errorMessage = Error }, JsonRequestBehavior.AllowGet);
                //return Content("Error = " + Error);
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult referUser(string Name, string Email, string Skill, int Type, string ReferedUser)
        {
            try
            {
                //string URLtest = "<a href='http://www.iaminterviewed.com/'>www.iaminterviewed.com </a>";
                //string body1 = "Hi " + Name + ",<br /><br />You have been refered by " + ReferedUser + ". Please Register as Interviewer <br /><br />"

                //                        + "Please Click Here to Register: " + URLtest + "<br /><br />"
                //                         + "Have a nice day...<br /><br />"
                //                         + "Thanks,<br />"
                //                         + "Team IAmInterviewed";
                //_baseHelper.SendMail("IamInterviewed Credentials", body1, "register@iaminterviewed.com", Email, true);
                if (IsUserExists(Email))
                {
                    return Json(new { data = "", Success = false, errorMessage = "This Email Exists. Try another one" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Random rand = new Random();
                    string password = String.Format("{0:X6}", rand.Next(0x1000000));
                    _interviewerDataHelper.ReferUser(Name, Email, Skill, Type);
                    string URL = "<a href='http://www.iaminterviewed.com/'>www.iaminterviewed.com </a>";

                    string body = "Hi " + Name + ",<br /><br />You have been refered by " + ReferedUser + ". Please Register as Interviewer <br /><br />"

                                        + "Please Click Here to Register: " + URL + "<br /><br />"
                                         + "Have a nice day...<br /><br />"
                                         + "Thanks,<br />"
                                         + "Team IAmInterviewed";
                    _baseHelper.SendMail("IamInterviewed Credentials", body, "register@iaminterviewed.com", Email, true);
                    return Json(new { data = "Refered Successfully.", Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                var Error = "Error. <Exception:>" + "\n" + ex.Message + " ";
                return Json(new { data = "", Success = false, errorMessage = Error }, JsonRequestBehavior.AllowGet);
                //return Content("Error = " + Error);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult UploadRatingFile(string ScheduleId, HttpPostedFileBase file)
        {
            try
            {
                var fileName = Path.GetFileName(file.FileName);
                string IAIPath = Server.MapPath("~/assets/InterviewRecordings/");
                //string newIAIPath = IAIPath.Replace("admin.iaminterviewed.com", "iaminterviewed.com");
                var savePath = Path.Combine(IAIPath, ScheduleId + "_" + fileName);
                file.SaveAs(savePath);
                return Content("Success");
            }
            catch (WebException ex)
            {
                //throw new Exception((ex.Response as FtpWebResponse).StatusDescription);
                return Content("Failed = " + ex.Response);
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult extractResume(string resumeName)
        {
            try
            {
                string resumePath = Server.MapPath("~/assets/CandidateResumes/");
                string docPath = resumePath + "31928_Sundeep.docx";
                string readText = System.IO.File.ReadAllText(docPath);
                //Application app = new Application();
                //Document doc = app.Documents.Open(docPath);
                //doc.ActiveWindow.Selection.WholeStory();
                //doc.ActiveWindow.Selection.Copy();
                //string sFileText = doc.Content.Text;
                string MatchPhonePattern = @"(?<!\d)\d{10}(?!\d)";
                Regex rx = new Regex(MatchPhonePattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                MatchCollection matches = rx.Matches(readText);
                foreach (Match match in matches)
                {
                    readText.Replace(match.Value.ToString(), "---");

                }
                return Json(new { data = readText, Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var Error = "Error. <Exception:>" + "\n" + ex.Message + " ";
                return Json(new { data = "", Success = false, errorMessage = Error }, JsonRequestBehavior.AllowGet);
                //return Content("Error = " + Error);
            }
        }

        public bool IsUserExists(string email)
        {
            bool isValid = false;
            if (_account.IsUserExists(email) != null)
                isValid = true;

            return isValid;
        }
    }
}