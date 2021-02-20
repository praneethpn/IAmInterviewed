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
using System.Text;
using System.Net;
using Newtonsoft.Json.Linq;

namespace IAIWebApp.Controllers
{
    public class CandidateController : Controller
    {
        CandidateDataHelper _candidateDataHelper = new CandidateDataHelper();
        BaseDataHelper _baseHelper = new BaseDataHelper();
        // GET: Candidate
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult getCandidateDashboardDetails(int UserId)
        {

            try
            {
                List<CandidateModel> _candidatelist = _candidateDataHelper.GetCandidateProfile(Convert.ToInt32(UserId));
                List<CandidateModel> _model = _candidateDataHelper.GetClosedInterviews(Convert.ToInt32(UserId));
                List<CandidateModel> _scheduledInterviews = _candidateDataHelper.GetScheduledInterview(Convert.ToInt32(UserId));

                CandidateModel _candidate = new CandidateModel();

                if (_candidatelist.Count <= 0)
                {
                    _candidate.CandidateNavigationStatus = "Profile Not Filled";
                }
                else if (string.IsNullOrEmpty(_candidatelist[0].SecondarySkill2.ToString()) || _candidatelist[0].SecondarySkill2 == 0
                    || string.IsNullOrEmpty(_candidatelist[0].SecondarySkill3.ToString()) || _candidatelist[0].SecondarySkill3 == 0)
                {
                    _candidate.CandidateNavigationStatus = "Profile Not Filled";
                }
                else
                {
                    _candidate.CandidateNavigationStatus = "Profile Filled";
                }

                _candidate.ProfileCompletionStatus = "25";
                if (_candidatelist.Count > 0)
                {
                    if (_scheduledInterviews.Count > 0)
                    {
                        if (_model.Count > 0)
                        {
                            _candidate.ProfileCompletionStatus = "100";
                        }
                        else
                        {
                            _candidate.ProfileCompletionStatus = "75";
                        }
                    }
                    else
                    {
                        _candidate.ProfileCompletionStatus = "50";
                    }
                }

                if (_model.Count > 0)
                {
                    int count = _model.Count;
                    count = count - 1;
                    string rating = _model[count].TotalRating;
                    string Accepted = _model[count].RatingAcceptance;
                    DateTime expiredate = _model[count].date;
                    expiredate = expiredate.AddYears(1);
                    _candidate.RatingExpiry = expiredate.ToString("dd-MMM-yyyy");
                    if (Accepted == "Yes")
                    {
                        _candidate.RatingAcceptance = "Accepted";
                        //chkRatingaccepted.Checked = true;
                    }
                    else
                    {
                        _candidate.RatingAcceptance = "Not Accepted";
                    }
                    if (_model[count].CompanySchedule == "yes")
                    {
                        _candidate.TotalRating = "Restricted";
                    }
                    else
                    {
                        _candidate.TotalRating = rating;
                    }
                    _candidate.CompanySchedule = _model[count].CompanySchedule;
                }
                if (_scheduledInterviews.Count > 0)
                {
                    _candidate.DisplayScheduleDate = _scheduledInterviews[0].date.ToString("dd/MMM/yyyy") + "-" + _scheduledInterviews[0].TimeSlotText;
                }

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
        [HttpGet]
        public ActionResult getFavoriteCompany(int UserId)
        {
            try
            {
                List<CandidateModel> _favoriteCompany = _candidateDataHelper.GetFavoriteCompany(UserId);
                // Now if our password was enctypted or hashed we would have done the
                // same operation on the user entered password here, But for now
                // since the password is in plain text lets just authenticate directly
                return Json(new { data = _favoriteCompany, Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
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
        public ActionResult getViewedProfiles(int UserId)
        {
            try
            {
                List<CandidateModel> _profilesViewed = _candidateDataHelper.GetViewedProfiles(UserId);
                // Now if our password was enctypted or hashed we would have done the
                // same operation on the user entered password here, But for now
                // since the password is in plain text lets just authenticate directly
                return Json(new { data = _profilesViewed, Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
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
        public ActionResult getScheduledInterview(int UserId)
        {
            try
            {
                List<CandidateModel> _scheduledInterviews = _candidateDataHelper.GetScheduledInterview(UserId);
                // Now if our password was enctypted or hashed we would have done the
                // same operation on the user entered password here, But for now
                // since the password is in plain text lets just authenticate directly
                return Json(new { data = _scheduledInterviews, Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
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
        public ActionResult UpdateGrantAccess(int UserId, int ReqId, string JobCode)
        {
            try
            {
                _candidateDataHelper.UpdateGrantAccess(UserId, ReqId);
                List<CompanyModel> _EmialNotification = _candidateDataHelper.GetRecruiterDetailsByReqId(ReqId);
                if (_EmialNotification.Count > 0)
                {
                    string URL = "<a href='http://www.iaminterviewed.com/'>www.iaminterviewed.com </a>";

                    string body = "Hi,<br /><br />" + Session["UserName"].ToString() + " has provided access to view his profile for the below job requirement<br /><br />"

                                    + "<strong>Jobcode :</strong>  " + JobCode + "<br />"
                                    + "Please click here to Login and View Candidate Profile: " + URL + "<br /><br />"
                                    + "Have a nice day...<br /><br />"
                                    + "Thanks,<br />"
                                    + "Team IAmInterviewed";
                    if (!string.IsNullOrEmpty(_EmialNotification[0].RecruiterEmail))
                    {
                        _baseHelper.SendMail("IAmInterviewed Notification", body, "register@iaminterviewed.com", _EmialNotification[0].RecruiterEmail, true);
                    }

                    if (!string.IsNullOrEmpty(_EmialNotification[0].AssignToEmail) && _EmialNotification[0].AssignToEmail != _EmialNotification[0].RecruiterEmail)
                    {
                        _baseHelper.SendMail("IAmInterviewed Notification", body, "register@iaminterviewed.com", _EmialNotification[0].AssignToEmail, true);
                    }

                    if (!string.IsNullOrEmpty(_EmialNotification[0].PMEmail) && _EmialNotification[0].PMEmail != _EmialNotification[0].RecruiterEmail)
                    {
                        _baseHelper.SendMail("IAmInterviewed Notification", body, "register@iaminterviewed.com", _EmialNotification[0].PMEmail, true);
                    }
                }
                // Now if our password was enctypted or hashed we would have done the
                // same operation on the user entered password here, But for now
                // since the password is in plain text lets just authenticate directly
                return Json(new { data = "Access Granted", Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
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
        public ActionResult getInterviewSchedulebyId(int ScheduleId)
        {
            try
            {
                List<InterviewerModel> _interviewSchedule = _candidateDataHelper.GetInterviewSchedulebyId(ScheduleId);
                // Now if our password was enctypted or hashed we would have done the
                // same operation on the user entered password here, But for now
                // since the password is in plain text lets just authenticate directly
                return Json(new { data = _interviewSchedule, Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
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
        public ActionResult acceptRating(string scheduleid, string status)
        {
            try
            {
                _candidateDataHelper.AcceptRating(scheduleid, status);
                // Now if our password was enctypted or hashed we would have done the
                // same operation on the user entered password here, But for now
                // since the password is in plain text lets just authenticate directly
                return Json(new { data = "", Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
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
        public ActionResult getCandidateProfile(int CandidateId)
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
        public ActionResult UploadFile(string CandidateId, HttpPostedFileBase file)
        {
            try
            {
                var fileName = Path.GetFileName(file.FileName);
                string IAIPath = Server.MapPath("~/assets/CandidateResumes/");
                //string IAIPath = Server.MapPath("~/CandidateResumes/");
                //string newIAIPath = IAIPath.Replace("admin.iaminterviewed.com", "iaminterviewed.com");
                var savePath = Path.Combine(IAIPath, CandidateId + "_" + fileName);
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
        [HttpPost]
        public ActionResult saveCandidateProfile(CandidateProfileModel _details)
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
        public ActionResult getInterviewerByDate(DateTime ScheduleDate, int UserId)
        {
            try
            {
                List<InterviewerModel> _interviewer = _candidateDataHelper.GetInterviewerByDate(ScheduleDate, UserId);
                if (_interviewer.Count > 0)
                {
                    return Json(new { data = _interviewer, Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    List<InterviewerModel> _DemoInterviewerList = new List<InterviewerModel>();
                    InterviewerModel _demo = new InterviewerModel();
                    _demo.UserId = 3912;
                    _demo.InterviewerName = "Saran";
                    return Json(new { data = _DemoInterviewerList, Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
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
        public ActionResult getInterviewerTimeSlotsByDate(DateTime ScheduleDate, int InterviewerId)
        {
            try
            {
                List<InterviewerModel> _interviewerTimeSlots = _candidateDataHelper.GetInterviewerTimeSlotsByDate(ScheduleDate, InterviewerId);
                if (_interviewerTimeSlots.Count > 0)
                {
                    return Json(new { data = _interviewerTimeSlots, Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    List<InterviewerModel> _interviewerTimeSlotsAll = _candidateDataHelper.GetDailyScheduleTimeAll(ScheduleDate, InterviewerId);
                    return Json(new { data = _interviewerTimeSlotsAll, Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
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
        public ActionResult saveInterviewSchedule(DateTime ScheduleDate, string Interviewer, string TimeSlot, int CandidateId, string InterviewType)
        {
            try
            {
                string message = "";
                _candidateDataHelper.SaveInterviewSchedule(ScheduleDate, Interviewer, TimeSlot, CandidateId, InterviewType, out message);
                if (message == "0")
                {
                    return Json(new { data = "", Success = false, errorMessage = "Something went wrong. Please select another slot and try again!" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    //3912
                    List<CandidateModel> _candidatelistinterviewer = _candidateDataHelper.GetCandidateProfile(Convert.ToInt32(Interviewer));
                    List<CandidateModel> _candidatelistCandidate = _candidateDataHelper.GetCandidateProfile(Convert.ToInt32(CandidateId));
                    string candidateUniqueId = CandidateId + _candidatelistCandidate[0].uniquenumber;
                    string URL = "<a href='http://www.iaminterviewed.com/'>www.iaminterviewed.com </a>";
                    string InterviewURL = "<a href='http://www.iaminterviewed.com/Interview.html?username=" + candidateUniqueId + "'>Join Interview </a>";
                    string InterviewerName = _candidatelistinterviewer.Count > 0 ? _candidatelistinterviewer[0].CandidateName : "Saran";
                    string mainTopics = _candidatelistCandidate[0].PrimarySkillName + "( " + _candidatelistCandidate[0].SecondarySkill1Name + ", " + _candidatelistCandidate[0].SecondarySkill2Name +
                        ", " + _candidatelistCandidate[0].SecondarySkill3Name + ", " + _candidatelistCandidate[0].SecondarySkill4Name + ", " + _candidatelistCandidate[0].SecondarySkill5Name + " )";
                    if (_candidatelistinterviewer.Count > 0)
                    {
                        string body = "Hi " + _candidatelistinterviewer[0].CandidateName + ",<br /><br />Please find the new  interview scheduled for you,<br /><br />"
                                            + "<strong>Name:</strong>  " + _candidatelistCandidate[0].CandidateName + "<br />"
                                            + "<strong>Date and Time:</strong>  " + ScheduleDate.ToString("dd-MMM-yyyy") + ", " + getTimeSlotName(Convert.ToInt32(TimeSlot)) + "<br />"
                                            + "Interview Topics: " + mainTopics + "<br />"
                                            + "Please Click Here to Confirm: " + URL + "<br /><br />"
                                            + "Have a nice day...<br /><br />"
                                            + "Thanks,<br />"
                                            + "Team IAmInterviewed";
                        _baseHelper.SendMail("Interview Schedule", body, "schedule@iaminterviewed.com", _candidatelistinterviewer[0].Email, true);
                    }
                    if (_candidatelistCandidate.Count > 0)
                    {
                        string callMessage = InterviewType == "Audio" ? "You will get Phone call, Please attend to the call." : "You will receive ZOOM invite for the Interview.";
                        string bodyCandidate = "Hi " + _candidatelistCandidate[0].CandidateName + ",<br /><br />Please find your interview scheduled,<br /><br />"
                                            + "<strong>Interviewer:</strong>  " + InterviewerName + "<br />"
                                            + "<strong>Date and Time:</strong>  " + ScheduleDate.ToString("dd-MMM-yyyy") + ", " + getTimeSlotName(Convert.ToInt32(TimeSlot)) + "<br />"
                                            + "Interview Topics: " + mainTopics + "<br />"
                                            + "Please login here to see details: " + URL + "<br /><br />"
                                            + callMessage + " < br /><br />"
                                            + "Please Click Here to Take Interview: " + InterviewURL + ". Or you may get Phone call also.<br /><br />"
                                            + "Have a nice day...<br /><br />"
                                            + "Thanks,<br />"
                                            + "Team IAmInterviewed";
                        _baseHelper.SendMail("Interview Schedule", bodyCandidate, "schedule@iaminterviewed.com", _candidatelistCandidate[0].Email, true);
                    }

                    return Json(new { data = "Your Interview has been successfully Scheduled. Please check your mail for further details.", Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
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
        public ActionResult saveInterviewSchedulePayment(DateTime ScheduleDate, string Interviewer, string TimeSlot, int CandidateId, string InterviewType)
        {
            try
            {
                List<CandidateModel> _candidatelist = _candidateDataHelper.GetCandidateProfile(CandidateId);
                if (_candidatelist[0].PromoCode == 0)
                {
                    Random ran = new Random();
                    string UniqueNumber = System.DateTime.Now.ToString("ddMMyyyy") + (String.Format("{0:X6}", ran.Next(0x1000000)));
                    string message = "1";
                    _candidateDataHelper.SaveInterviewSchedulePayment(ScheduleDate, Interviewer, TimeSlot, CandidateId, InterviewType, UniqueNumber, out message);
                    if (message == "0")
                    {
                        return Json(new { data = "", Success = false, errorMessage = "Something went wrong. Please select another slot and try again!" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        ScheduleInterviewResponse _responce = new Models.ScheduleInterviewResponse();
                        _responce.ResponseMessage = "Your Interview has been successfully Scheduled. Please check your mail for further details.";
                        _responce.PromoCode = "0";
                        _responce.OrderId = UniqueNumber;
                        return Json(new { data = _responce, Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    string message = "";
                    _candidateDataHelper.SaveInterviewSchedule(ScheduleDate, Interviewer, TimeSlot, CandidateId, InterviewType, out message);
                    if (message == "0")
                    {
                        return Json(new { data = "", Success = false, errorMessage = "Something went wrong. Please select another slot and try again!" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        List<CandidateModel> _candidatelistinterviewer = _candidateDataHelper.GetCandidateProfile(Convert.ToInt32(Interviewer));
                        List<CandidateModel> _candidatelistCandidate = _candidateDataHelper.GetCandidateProfile(Convert.ToInt32(CandidateId));
                        string candidateUniqueId = CandidateId + _candidatelistCandidate[0].uniquenumber;
                        string URL = "<a href='http://www.iaminterviewed.com/'>www.iaminterviewed.com </a>";
                        string InterviewURL = "<a href='http://www.iaminterviewed.com/Interview.html?username=" + candidateUniqueId + "'>Join Interview </a>";
                        string InterviewerName = _candidatelistinterviewer.Count > 0 ? _candidatelistinterviewer[0].CandidateName : "Saran";
                        string mainTopics = _candidatelistCandidate[0].PrimarySkillName + "( " + _candidatelistCandidate[0].SecondarySkill1Name + ", " + _candidatelistCandidate[0].SecondarySkill2Name +
                        ", " + _candidatelistCandidate[0].SecondarySkill3Name + ", " + _candidatelistCandidate[0].SecondarySkill4Name + ", " + _candidatelistCandidate[0].SecondarySkill5Name + " )";
                        if (_candidatelistinterviewer.Count > 0)
                        {
                            string body = "Hi " + _candidatelistinterviewer[0].CandidateName + ",<br /><br />Please find the new  interview scheduled for you,<br /><br />"
                                                + "<strong>Name:</strong>  " + _candidatelistCandidate[0].CandidateName + "<br />"
                                                + "<strong>Date and Time:</strong>  " + ScheduleDate.ToString("dd-MMM-yyyy") + ", " + getTimeSlotName(Convert.ToInt32(TimeSlot)) + "<br />"
                                                + "Interview Topics: " + mainTopics + "<br />"
                                                + "Please Click Here to Confirm: " + URL + "<br /><br />"
                                                + "Have a nice day...<br /><br />"
                                                + "Thanks,<br />"
                                                + "Team IAmInterviewed";
                            _baseHelper.SendMail("Interview Schedule", body, "schedule@iaminterviewed.com", _candidatelistinterviewer[0].Email, true);
                        }
                        if (_candidatelistCandidate.Count > 0)
                        {
                            string callMessage = InterviewType == "Audio" ? "You will get Phone call, Please attend to the call." : "You will receive ZOOM invite for the Interview.";
                            string bodyCandidate = "Hi " + _candidatelistCandidate[0].CandidateName + ",<br /><br />Please find your interview scheduled,<br /><br />"
                                                + "<strong>Interviewer:</strong>  " + InterviewerName + "<br />"
                                                + "<strong>Date and Time:</strong>  " + ScheduleDate.ToString("dd-MMM-yyyy") + ", " + getTimeSlotName(Convert.ToInt32(TimeSlot)) + "<br />"
                                                + "Interview Topics: " + mainTopics + "<br />"
                                                + "Please login here to see details: " + URL + "<br /><br />"
                                                + callMessage + "<br /><br />"
                                                + "Please Click Here to Take Interview: " + InterviewURL + ". Or you may get Phone call also.<br /><br />"
                                                + "Have a nice day...<br /><br />"
                                                + "Thanks,<br />"
                                                + "Team IAmInterviewed";
                            _baseHelper.SendMail("Interview Schedule", bodyCandidate, "schedule@iaminterviewed.com", _candidatelistCandidate[0].Email, true);
                        }                        
                        ScheduleInterviewResponse _responce = new Models.ScheduleInterviewResponse();
                        _responce.ResponseMessage = "Your Interview has been successfully Scheduled. Please check your mail for further details.";
                        _responce.PromoCode = "1";
                        return Json(new { data = _responce, Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
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

        [Authorize]
        [HttpGet]
        public ActionResult fillFavoriteCompany(string UserId)
        {
            try
            {
                List<CompanyModel> _favoriteCompany = _candidateDataHelper.fillFavoriteCompany(UserId);
                // Now if our password was enctypted or hashed we would have done the
                // same operation on the user entered password here, But for now
                // since the password is in plain text lets just authenticate directly
                return Json(new { data = _favoriteCompany, Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
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
        public ActionResult fillDesignation(int CompanyId)
        {
            try
            {
                List<DesignationModel> _designations = _candidateDataHelper.GetDesignation(0, 10, "DesignationId ASC", CompanyId);
                // Now if our password was enctypted or hashed we would have done the
                // same operation on the user entered password here, But for now
                // since the password is in plain text lets just authenticate directly
                return Json(new { data = _designations, Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
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
        public ActionResult saveFavoriteCompany(SaveFavoriteCompanyModel _apply)
        {
            try
            {
                if (string.IsNullOrEmpty(_apply.Applicationid))
                {
                    List<CandidateModel> _model = _candidateDataHelper.GetClosedInterviews(Convert.ToInt32(_apply.CandidateId));
                    if (_model.Count > 0)
                    {
                        int count = _model.Count;
                        count = count - 1;
                        string rating = _model[count].TotalRating;
                        string Accepted = _model[count].RatingAcceptance;
                        if (Accepted == "Yes")
                        {
                            _candidateDataHelper.SaveFavoriteCompany(_apply.Company, _apply.CandidateId, _apply.Designation);
                            return Json(new { data = "Data Saved Successfully.", Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { data = "", Success = false, errorMessage = "Please accept the rating to apply." }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(new { data = "", Success = false, errorMessage = "Please get inteviewed to apply for your favorite company" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    _candidateDataHelper.UpdateFavoriteCompany(_apply.Company, _apply.Applicationid, _apply.Designation);
                    return Json(new { data = "Data Updated Successfully.", Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
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
        public ActionResult getCandidateRelatedRequirements(int CandidateId, string Company, string SecSkill, string Role)
        {
            try
            {
                if (Company == "0" || Company == "")
                {
                    Company = null;
                }
                if (string.IsNullOrEmpty(Role) || Role == "0")
                {
                    Role = null;
                }
                List<CompanyModel> _requirements = _candidateDataHelper.GetCandidateRelatedRequirements(CandidateId, Company, SecSkill, Role);

                List<CompanyModel> _companies = new List<CompanyModel>();
                var DistinctCompanies = _requirements.GroupBy(x => x.CompanyId).Select(y => y.First());

                foreach (var item in DistinctCompanies)
                {
                    CompanyModel com = new CompanyModel();
                    com.CompanyId = item.CompanyId;
                    com.CompanyName = item.CompanyName;
                    _companies.Add(com);
                    //Add to other List
                }

                List<CompanyModel> _roles = new List<CompanyModel>();
                var DistinctItems = _requirements.GroupBy(x => x.JobTitle).Select(y => y.First());

                foreach (var item in DistinctItems)
                {
                    CompanyModel com = new CompanyModel();
                    com.JobTitleId = item.JobTitleId;
                    com.JobTitle = item.JobTitle;
                    _roles.Add(com);
                    //Add to other List
                }
                // Now if our password was enctypted or hashed we would have done the
                // same operation on the user entered password here, But for now
                // since the password is in plain text lets just authenticate directly
                return Json(new { data = new { Requirements = _requirements, Roles = _roles, Companies = _companies }, Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
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
        public ActionResult getCandidateAppliedRequirements(int CandidateId, string Company)
        {
            try
            {
                if (Company == "0" || Company == "")
                {
                    Company = null;
                }
                List<CompanyModel> _requirements = _candidateDataHelper.GetCandidateAppliedRequirements(CandidateId, Company);
                // Now if our password was enctypted or hashed we would have done the
                // same operation on the user entered password here, But for now
                // since the password is in plain text lets just authenticate directly
                return Json(new { data = _requirements, Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
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
        public ActionResult saveCandidateApplication(int CandidateId, int ReqId, string JobTitle)
        {
            try
            {
                List<CandidateModel> _model = _candidateDataHelper.GetClosedInterviews(CandidateId);
                if (_model.Count > 0)
                {
                    bool isValid = false;
                    if (_candidateDataHelper.IsExistsCandidateApplication(CandidateId, ReqId) != null)
                        isValid = true;
                    if (isValid)
                    {
                        return Json(new { data = "", Success = false, errorMessage = "Already Applied" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        int count = _model.Count;
                        count = count - 1;
                        string rating = _model[count].TotalRating;
                        string Accepted = _model[count].RatingAcceptance;
                        if (Accepted == "Yes")
                        {
                            _candidateDataHelper.SaveCandidateApplication(CandidateId, ReqId);
                            return Json(new { data = "Applied Successfully", Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);

                            List<CompanyModel> _EmialNotification = _candidateDataHelper.GetRecruiterDetailsByReqId(ReqId);
                            if (_EmialNotification.Count > 0)
                            {
                                string URL = "<a href='http://www.iaminterviewed.com/'>www.iaminterviewed.com </a>";

                                string body = "Hi,<br /><br />" + Session["UserName"].ToString() + " has applied for the below job requirement<br /><br />"

                                                + "<strong>JobTitle :</strong>  " + JobTitle + "<br />"
                                                + "Please click here to Login and View Candidate Profile: " + URL + "<br /><br />"
                                                + "Have a nice day...<br /><br />"
                                                + "Thanks,<br />"
                                                + "Team IAmInterviewed";
                                if (!string.IsNullOrEmpty(_EmialNotification[0].RecruiterEmail))
                                {
                                    _baseHelper.SendMail("IAmInterviewed Notification", body, "register@iaminterviewed.com", _EmialNotification[0].RecruiterEmail, true);
                                }

                                if (!string.IsNullOrEmpty(_EmialNotification[0].AssignToEmail) && _EmialNotification[0].AssignToEmail != _EmialNotification[0].RecruiterEmail)
                                {
                                    _baseHelper.SendMail("IAmInterviewed Notification", body, "register@iaminterviewed.com", _EmialNotification[0].AssignToEmail, true);
                                }

                                if (!string.IsNullOrEmpty(_EmialNotification[0].PMEmail) && _EmialNotification[0].PMEmail != _EmialNotification[0].RecruiterEmail)
                                {
                                    _baseHelper.SendMail("IAmInterviewed Notification", body, "register@iaminterviewed.com", _EmialNotification[0].PMEmail, true);
                                }
                            }
                        }
                        else
                        {
                            return Json(new { data = "", Success = false, errorMessage = "Please accept your rating to apply" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                else
                {
                    return Json(new { data = "", Success = false, errorMessage = "Please Schedule Interview and Accept rating to apply" }, JsonRequestBehavior.AllowGet);
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
        public ActionResult getCompanyJD(int ReqId)
        {
            try
            {
                List<CompanyModel> _requirement = _candidateDataHelper.GetCompanyJD(ReqId);
                // Now if our password was enctypted or hashed we would have done the
                // same operation on the user entered password here, But for now
                // since the password is in plain text lets just authenticate directly
                return Json(new { data = _requirement, Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
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

        [HttpGet]
        public ActionResult getCandidateProfileForCall(int CandidateId)
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

        [HttpGet]
        public ActionResult GetCandidateRatingDetails(string interviewId)
        {
            try
            {
                if (interviewId.Length > 10)
                {
                    string scheduleId = interviewId.Substring(10);
                    CompanyModel _details = _candidateDataHelper.GetCandidateRatingDetails(scheduleId);
                    return Json(new { data = _details, Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { data = "", Success = false, errorMessage = "Invalid Interview Id" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                var Error = "Error. <Exception:>" + "\n" + ex.Message + " ";
                return Json(new { data = "", Success = false, errorMessage = Error }, JsonRequestBehavior.AllowGet);
                //return Content("Error = " + Error);
            }
        }

        public string getTimeSlotName(int timeSlot)
        {
            string timeSlotName = "";
            List<CandidateModel> _dailySchedule = _candidateDataHelper.GetDailyScheduleById(timeSlot);
            if (_dailySchedule.Count > 0)
            {
                timeSlotName = _dailySchedule[0].TimeSlotText;
            }
            return timeSlotName;
        }
    }
}