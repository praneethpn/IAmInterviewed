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
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;

namespace IAIWebApp.Controllers
{
    public class CompanyController : Controller
    {
        CompanyDataHelper _companyDataHelper = new CompanyDataHelper();
        AccountDataHelper _account = new AccountDataHelper();
        BaseDataHelper _baseHelper = new BaseDataHelper();
        CandidateDataHelper _candidateDataHelper = new CandidateDataHelper();
        // GET: Company
        //[Route("Test")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        //[Route("company/home/details/{companyId ?}")]
        public ActionResult getCompanyHomePageDetails(int companyId)
        {
            try
            {
                CompanyModel _company = new CompanyModel();
                List<CompanyModel> _companyProfile = _companyDataHelper.GetCompanyProfile(companyId);
                List<CompanyModel> _userTypeList = _companyDataHelper.GetCompanyUserType(companyId);
                if (_companyProfile.Count > 0)
                {
                    _company.Logo = "assets/CompanyLogos/" + _companyProfile[0].Logo;
                }
                if (_userTypeList.Count > 0)
                {
                    _company.CompanyUserType = _userTypeList[0].CompanyUserType;
                }
                return Json(new { data = _company, Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
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
        //[Route("company/dashboard/details/{companyId ?}")]
        public ActionResult getCompanyDashboardDetails(int companyId)
        {
            try
            {
                CompanyModel _company = new CompanyModel();
                List<CompanyModel> _companyDashboard = _companyDataHelper.GetCompanyDashBoardInterviewDetails(companyId);
                List<CompanyModel> _companyRequirements = _companyDataHelper.GetCompanyDashBoardRequirementsDetails(companyId);
                List<CompanySkillWiseProfilesReportModel> _UsedInterviews = new List<CompanySkillWiseProfilesReportModel>();
                List<CompanyModel> _Profiles = _companyDataHelper.GetCompanyDashBoardSkillWiseProfileDetails(companyId);
                for (int i = 0; i < _Profiles.Count; i++)
                {
                    CompanySkillWiseProfilesReportModel _model = new CompanySkillWiseProfilesReportModel();
                    _model.PrimarySkillId = _Profiles[i].PrimarySkill;
                    _model.PrimarySkillName = _Profiles[i].PrimarySKillName;
                    _model.CompanyProfiles = _Profiles[i].CompanyProfiles;
                    //_model.IAIProfiles = _Profiles[i].IAIProfiles;
                    _UsedInterviews.Add(_model);
                }
                return Json(new { data = new { interviews = _companyDashboard, requirements = _companyRequirements, skillReport = _UsedInterviews }, Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
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
        public ActionResult GetCompanyRequirements(int companyId, string primarySkill, string jobCode, string postedBy, string status)
        {
            try
            {
                if (string.IsNullOrEmpty(primarySkill))
                {
                    primarySkill = null;
                }
                if (string.IsNullOrEmpty(jobCode))
                {
                    jobCode = null;
                }
                if (string.IsNullOrEmpty(postedBy))
                {
                    postedBy = null;
                }
                List<CompanyModel> _companyRequirements = _companyDataHelper.GetCompanyRequirements(0, 1000000, "ReqId ASC", companyId, primarySkill, jobCode, postedBy, status);
                return Json(new { data = _companyRequirements, Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
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
        public ActionResult GetCompanyRequirementsForSelectList(int companyId, string status)
        {
            try
            {
                List<CompanyModel> _listPostedBY = new List<CompanyModel>();
                List<CompanyModel> _reqFullList = _companyDataHelper.GetCompanyRequirements(0, 1000000, "ReqId ASC", companyId, null, null, null, status);
                var DistinctItems = _reqFullList.GroupBy(x => x.PostedById).Select(y => y.First());

                foreach (var item in DistinctItems)
                {
                    CompanyModel _companyModel = new CompanyModel();
                    _companyModel.PostedById = item.PostedById;
                    _companyModel.PostedBy = item.PostedBy;
                    _listPostedBY.Add(_companyModel);
                    //Add to other List
                }
                return Json(new { data = new { JobcodeList = _reqFullList, PostedByList = _listPostedBY }, Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
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
        public ActionResult FillRecruiters(string companyId)
        {
            try
            {
                List<CompanyModel> _recruiters = _companyDataHelper.FillRecruiters(companyId);

                return Json(new { data = _recruiters, Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
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
        public ActionResult FillPM(string companyId)
        {
            try
            {
                List<CompanyModel> _managers = _companyDataHelper.FillPM(companyId);

                return Json(new { data = _managers, Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
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
        public ActionResult UpdateCompanyrequirements(string ReqId, string AssignTo, string PM, string Status, string StatusRemarks)
        {
            try
            {
                if (string.IsNullOrEmpty(AssignTo) || AssignTo == "0")
                {
                    AssignTo = null;
                }
                if (string.IsNullOrEmpty(PM) || PM == "0")
                {
                    PM = null;
                }
                _companyDataHelper.UpdateCompanyrequirements(ReqId, AssignTo, PM, Status, StatusRemarks);

                return Json(new { data = "Data Updated Successfully.", Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
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
        public ActionResult GetCompanyDashBoardJobPostingDetails(string status, int companyId, string primarySkill)
        {
            try
            {
                if (string.IsNullOrEmpty(primarySkill))
                {
                    primarySkill = null;
                }
                string JobCode = null;
                string PostedBy = null;
                string Status = status;
                int UserId = companyId;
                List<CompanyModel> _companyRequirements = _companyDataHelper.GetCompanyRequirements(0, 1000000, "ReqId ASC", UserId, primarySkill, JobCode, PostedBy, Status);
                foreach (var req in _companyRequirements)
                {
                    List<CompanyModel> _jobPostingDetails = _companyDataHelper.GetCompanyDashBoardJobPostingDetails(req.ReqId);
                    req.TotalProfiles = _jobPostingDetails[0].TotalProfiles;
                    req.InterviewedProfiles = _jobPostingDetails[0].InterviewedProfiles;
                    req.SelectedProfiles = _jobPostingDetails[0].SelectedProfiles;
                    req.JoinedProfiles = _jobPostingDetails[0].JoinedProfiles;
                    req.RejectedProfiles = _jobPostingDetails[0].RejectedProfiles;

                    req.IAISelectedProfiles = _jobPostingDetails[0].IAISelectedProfiles;
                    req.IAIJoinedProfiles = _jobPostingDetails[0].IAIJoinedProfiles;
                    req.IAIInProgressProfiles = _jobPostingDetails[0].IAIInProgressProfiles;
                    req.IAIRejectedProfiles = _jobPostingDetails[0].IAIRejectedProfiles;

                    req.InterviewedSelectedProfiles = _jobPostingDetails[0].InterviewedSelectedProfiles;
                    req.InterviewedJoinedProfiles = _jobPostingDetails[0].InterviewedJoinedProfiles;
                    req.InterviewedRejectedProfiles = _jobPostingDetails[0].InterviewedRejectedProfiles;
                    req.InterviewedInProgressProfiles = _jobPostingDetails[0].InterviewedInProgressProfiles;
                }
                return Json(new { data = _companyRequirements, Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
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
        public ActionResult GetCompanyProfile(int companyId)
        {
            try
            {
                List<CompanyModel> _companyProfile = _companyDataHelper.GetCompanyProfile(companyId);

                return Json(new { data = _companyProfile, Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
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
        public ActionResult UploadCompanyLogo(string companyId, HttpPostedFileBase file)
        {
            try
            {
                var fileName = Path.GetFileName(file.FileName);
                string IAIPath = Server.MapPath("~/assets/CompanyLogos/");
                //string newIAIPath = IAIPath.Replace("admin.iaminterviewed.com", "iaminterviewed.com");
                var savePath = Path.Combine(IAIPath, companyId + "_" + fileName);
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
        public ActionResult SaveCompanyProfile(CompanyProfileModel _details)
        {
            try
            {
                _companyDataHelper.SaveCompanyProfile(_details);
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
        [HttpPost]
        public ActionResult SaveCompanyRequirements(CompanyRequirementsModel _details)
        {
            try
            {
                if (IsRequirementExists(_details.UserId, _details.JobCode))
                {
                    return Json(new { data = "", Success = false, errorMessage = "RequirementId Already Exists with the JobCode. Try with another one" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string Status = "Open";
                    string reqid = "";
                    int UserId = _details.UserId;
                    _companyDataHelper.SaveCompanyRequirements(_details);
                    //List<CompanyModel> comreq = _companyDataHelper.GetCompanyRequirements(0, 1000000, "ReqId ASC", UserId, null, null, null, Status);
                    //for (int i = 0; i < comreq.Count; i++)
                    //{
                    //    if (comreq[i].JobCode == _details.JobCode)
                    //    {
                    //        reqid = comreq[i].ReqId.ToString();
                    //    }
                    //}
                    //List<CompanyModel> company = _companyDataHelper.GetCompanyReqRelatedCandidates(reqid, null, null, null, null, null, null);
                    //for (int j = 0; j < company.Count; j++)
                    //{

                    //    string URL = "<a href='http://www.iaminterviewed.com/'>www.iaminterviewed.com </a>";

                    //    string body = "Dear " + company[j].CandidateName + ",<br /><br />"

                    //                        + "There is a new Job Published in " + URL + "<br />"
                    //                        + "Please login to Apply<br /><br />"
                    //                         + "Have a nice day...<br /><br />"
                    //                    + "Thanks,<br />"
                    //                    + "Team IAmInterviewed";
                    //    _baseHelper.SendMail("New Job Posted", body, "register@iaminterviewed.com", company[j].Email, true);
                    //}
                }
                //_companyDataHelper.SaveCompanyRequirements(_details);
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
        public ActionResult GetReqRelatedCandidates(string reqId, string secSkill1, string totalRating, string secSkillRating, string location, string experience, string additionalSkills, string EmailId)
        {
            try
            {
                if (secSkill1 == "0" || string.IsNullOrEmpty(secSkill1))
                {
                    secSkill1 = null;
                }
                if (totalRating == "All" || string.IsNullOrEmpty(totalRating))
                {
                    totalRating = null;
                }
                if (secSkillRating == "All" || string.IsNullOrEmpty(secSkillRating))
                {
                    secSkillRating = null;
                }
                if (location == "0" || string.IsNullOrEmpty(location))
                {
                    location = null;
                }
                if (experience == "0" || string.IsNullOrEmpty(experience))
                {
                    experience = null;
                }
                if (string.IsNullOrEmpty(reqId))
                {
                    reqId = null;
                }

                List<CompanyModel> _reqRelatedCandidates = new List<CompanyModel>();
                if (EmailId == "demoemployer@iaminterviewed.com" && additionalSkills == "")
                {
                    _reqRelatedCandidates = _companyDataHelper.GetCompanyReqRelatedCandidates(reqId, secSkill1, totalRating, secSkillRating, location, experience, "Demo");
                }
                else if (additionalSkills == "")
                {
                    _reqRelatedCandidates = _companyDataHelper.GetCompanyReqRelatedCandidates(reqId, secSkill1, totalRating, secSkillRating, location, experience, "All");
                }
                else
                {
                    _reqRelatedCandidates = _companyDataHelper.GetCompanyReqRelatedCandidates1(reqId, secSkill1, totalRating, secSkillRating, location, experience, "Demo", additionalSkills);
                }

                return Json(new { data = _reqRelatedCandidates, Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
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
        public ActionResult SaveCompanySelectedProfiles(int reqId, int candidateId, int scheduleId, int userId, string grantAccess, string candidateName, string jobCode, string emailId)
        {
            try
            {

                _companyDataHelper.SaveCompanySelectedProfiles(reqId, candidateId, scheduleId, userId);
                if (grantAccess == "NO")
                {
                    string URL = "<a href='http://www.iaminterviewed.com/'>www.iaminterviewed.com </a>";

                    string body = "Hi " + candidateName + ",<br /><br />" + Session["UserName"].ToString() + " has selected your profile for the below job requirement<br /><br />"

                                    + "<strong>Jobcode :</strong>  " + jobCode + "<br />"
                                    + "Please Provide Access for the company to view your profile, please click here to Login: " + URL + "<br /><br />"
                                    + "Have a nice day...<br /><br />"
                                    + "Thanks,<br />"
                                    + "Team IAmInterviewed";
                    _baseHelper.SendMail("IAmInterviewed Notification", body, "register@iaminterviewed.com", emailId, true);
                }
                return Json(new { data = "Shortlisted Successfully", Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
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
        public ActionResult SaveCompanySelectedProfilesList(List<CompanySelectedProfiles> _companySelectedProfiles)
        {
            try
            {
                foreach (var profile in _companySelectedProfiles)
                {
                    _companyDataHelper.SaveCompanySelectedProfiles(profile.reqId, profile.candidateId, profile.scheduleId, profile.userId);
                    if (profile.grantAccess == "NO")
                    {
                        string URL = "<a href='http://www.iaminterviewed.com/'>www.iaminterviewed.com </a>";

                        string body = "Hi " + profile.candidateName + ",<br /><br /> Your profile has been shortlisted for the below job requirement<br /><br />"

                                        + "<strong>Jobcode :</strong>  " + profile.jobCode + "<br />"
                                        + "Please Provide Access for the company to view your profile, please click here to Login: " + URL + "<br /><br />"
                                        + "Have a nice day...<br /><br />"
                                        + "Thanks,<br />"
                                        + "Team IAmInterviewed";
                        _baseHelper.SendMail("IAmInterviewed Notification", body, "register@iaminterviewed.com", profile.emailId, true);
                    }
                }
                return Json(new { data = "Shortlisted Successfully", Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
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
        public ActionResult SelectedCandidatesList(int reqId, string secSkill1, string totalRating, string secSkillRating, string candidateName, string companyId)
        {
            try
            {
                if (secSkill1 == "0" || string.IsNullOrEmpty(secSkill1))
                {
                    secSkill1 = null;
                }
                if (totalRating == "All" || string.IsNullOrEmpty(totalRating))
                {
                    totalRating = null;
                }
                if (secSkillRating == "All" || string.IsNullOrEmpty(secSkillRating))
                {
                    secSkillRating = null;
                }
                if (string.IsNullOrEmpty(candidateName))
                {
                    candidateName = null;
                }

                List<CompanyModel> _selectedCandidates = _companyDataHelper.SelectedCandidatesList(0, 1000000, "ReqId ASC", companyId, reqId, candidateName, secSkill1, totalRating, secSkillRating);
                List<CompanyModel> _nonInterviewedCandidates = new List<CompanyModel>();//_companyDataHelper.SelectedGetNonInterviewedCandidates(companyId, reqId);

                return Json(new { data = new { selectedCandidates = _selectedCandidates, nonInterviewedCandidates = _nonInterviewedCandidates }, Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
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
        public ActionResult UpdateFutureDate(string reqId, string candiateId, DateTime futureUpdateDate, string comments, string status)
        {
            try
            {


                _companyDataHelper.UpdateFutureDate(reqId, candiateId, futureUpdateDate, comments, status, null);

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
        public ActionResult SelectedProfilesDump(int candiateId, int reqId)
        {
            try
            {
                List<CompanyModel> _selectedCandidates = _companyDataHelper.SelectedProfilesDump(candiateId, reqId);
                return Json(new { data = _selectedCandidates, Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
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
        public ActionResult GetAppliedCandidates(int reqId)
        {
            try
            {
                List<CompanyModel> _appliedCandidates = _companyDataHelper.GetAppliedCandidates(reqId);
                return Json(new { data = _appliedCandidates, Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
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
        public ActionResult GetFanProfiles(int candiateId)
        {
            try
            {
                List<CompanyModel> _fanProfiles = _companyDataHelper.GetFanProfiles(candiateId);
                return Json(new { data = _fanProfiles, Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
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
        public ActionResult FollowUpProfiles(string companyId, DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                List<CompanyModel> _followUpProfiles = _companyDataHelper.FollowUpProfiles(companyId, fromDate, toDate);
                return Json(new { data = _followUpProfiles, Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
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
        public ActionResult UpdateTrackingStatus(string reqId, string candiateId, DateTime futureUpdateDate, string comments, string trackingStatus)
        {
            try
            {


                _companyDataHelper.UpdateFutureDate(reqId, candiateId, futureUpdateDate, comments, "InProgress", trackingStatus);

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
        public ActionResult GetCompanyAddedCandidateDetials(string primaryskill, string companyId, DateTime? startDate, DateTime? endDate)
        {
            try
            {
                if (string.IsNullOrEmpty(primaryskill))
                {
                    primaryskill = null;
                }
                var serializer = new JavaScriptSerializer();
                serializer.MaxJsonLength = Int32.MaxValue;
                List<CompanyModel> _companyAddedCandidates = _companyDataHelper.GetCompanyAddedCandidateDetials(primaryskill, companyId, startDate, endDate);
                var jsonResult = Json(new { data = _companyAddedCandidates, Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
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
        public ActionResult GetSubUsers(int companyId)
        {
            try
            {
                List<User> _subUsers = _companyDataHelper.GetSubUsers(0, 1000000, "SubUserId ASC", companyId);
                return Json(new { data = _subUsers, Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
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
        public ActionResult SaveSubUser(CompanySubUser _companySubUser)
        {
            try
            {
                Random rand = new Random();
                _companySubUser.Password = String.Format("{0:X6}", rand.Next(0x1000000));
                _companySubUser.SkillType = "Company";
                if (IsUserExists(_companySubUser.EmailId))
                {
                    return Json(new { data = "", Success = false, errorMessage = "This Email Exists. Try another one." }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    _companyDataHelper.SaveSubUser(_companySubUser);

                    string URL = "<a href='http://iaminterviewed.com/SignIn.aspx'>www.iaminterviewed.com </a>";

                    string body = "Hi " + _companySubUser.Username + ",<br /><br />thank you for registering with us. Please find your UserName and Password below<br /><br />"

                                        + "<strong>User Name:</strong>  " + _companySubUser.EmailId + "<br />"
                                        + "<strong>Password:</strong>  " + _companySubUser.Password + "<br />"
                                        + "Please Click Here to Login:" + URL + "";
                    _baseHelper.SendMail("IAmInterviewed Credentials", body, "register@iaminterviewed.com", _companySubUser.EmailId, true);
                    return Json(new { data = "Sub User Saved Successfully", Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
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
        public ActionResult GetDesignation(int companyId)
        {
            try
            {
                List<DesignationModel> _designations = _companyDataHelper.GetDesignation(0, 1000000, "DesignationId ASC", companyId);

                return Json(new { data = _designations, Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
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
        public ActionResult SaveDesignation(int designationId, string designation, string description, string companyId)
        {
            try
            {
                _companyDataHelper.SaveDesignation(designationId, designation, description, companyId);
                return Json(new { data = "Designation Added Successfully", Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
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
        public ActionResult GetAllVendors(int companyId)
        {
            try
            {
                List<VendorModel> _vendors = _companyDataHelper.GetAllVendors(companyId);

                return Json(new { data = _vendors, Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
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
        public ActionResult SaveVendor(string vendorName, string vendorEmail, string vendorMobile, int companyId, DateTime startDate, DateTime endDate)
        {
            try
            {
                if (IsVendorExists(vendorEmail))
                {
                    return Json(new { data = "", Success = false, errorMessage = "Vendor Exists. Try another one" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    _companyDataHelper.SaveVendor(vendorName, vendorEmail, vendorMobile, companyId, startDate, endDate);
                    return Json(new { data = "Vendor Added Successfully", Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
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
        public ActionResult GetNoOfInterviewersRequired(int companyId)
        {
            try
            {
                List<CompanyModel> _interviewers = _companyDataHelper.GetNoOfInterviewersRequired(companyId);

                return Json(new { data = _interviewers, Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
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
        public ActionResult SaveRequestInterviewers(RequestInterviewersModel _requestInterviewers)
        {
            try
            {
                _companyDataHelper.SaveRequestInterviewers(_requestInterviewers);
                return Json(new { data = "Requested Successfully", Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
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
        public ActionResult GetCompanyAddedProfiles(string companyId, string primaryskill, string jobCode, string statusFilter)
        {
            try
            {
                List<CandidateModel> _companyProfiles = _companyDataHelper.GetCandidateDetials(primaryskill, companyId, jobCode, statusFilter);

                return Json(new { data = _companyProfiles, Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
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
        public ActionResult SaveCompanyAddedUser(CompanyAddedUser _candidate)
        {
            try
            {
                if (IsUserExists(_candidate.candidateEmail))
                {
                    return Json(new { data = "", Success = false, errorMessage = "This Email Exists. Try another one" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    if (string.IsNullOrEmpty(_candidate.vendor))
                    {
                        _candidate.vendor = null;
                    }
                    Random rand1 = new Random();
                    _candidate.password = String.Format("{0:X6}", rand1.Next(0x1000000));
                    //string Skill = ddlprimaryskill.SelectedValue.ToString();
                    Random rand = new Random();
                    _candidate.uniqueId = String.Format("{0:X10}", rand.Next(000000000, 999999999));
                    _companyDataHelper.SaveCompanyAddedUser(_candidate);
                    return Json(new { data = "Candidate Registered Successfully", Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
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
        public ActionResult GetCandidateProfileCompany(int userId)
        {
            try
            {
                List<CandidateModel> _candidateProfiles = _companyDataHelper.GetCandidateProfile_Company(userId);

                return Json(new { data = _candidateProfiles, Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
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
        public ActionResult SaveCandidateProfileCompany(CandidateProfileModelCompany _candidate)
        {
            try
            {
                _companyDataHelper.SaveCandidateProfile_Company(_candidate);
                return Json(new { data = "Candidate Registered Successfully", Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
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
        public ActionResult UpdateSelectStatus(int candidateId, string selectStatus, string statusRemarks, int userId)
        {
            try
            {
                _companyDataHelper.UpdateSelectStatus(candidateId, selectStatus, statusRemarks, userId);
                return Json(new { data = "Updated Successfully", Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
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
        public ActionResult saveInterviewScheduleCompany(DateTime ScheduleDate, string Interviewer, string TimeSlot, int CandidateId, string InterviewType, int CompanyId, string JobCode)
        {
            try
            {
                string message = "";
                _companyDataHelper.SaveInterviewScheduleCompany(ScheduleDate, Interviewer, TimeSlot, CandidateId, InterviewType, CompanyId, JobCode, out message);
                if (message == "0")
                {
                    return Json(new { data = "", Success = false, errorMessage = "Something went wrong. Please select another slot and try again!" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    List<CandidateModel> _candidatelistinterviewer = _candidateDataHelper.GetCandidateProfile(Convert.ToInt32(Interviewer));
                    List<CandidateModel> _candidatelistCandidate = _candidateDataHelper.GetCandidateProfile(Convert.ToInt32(CandidateId));
                    List<CompanyModel> _companyProfile = _companyDataHelper.GetCompanyProfile(CompanyId);
                    string candidateUniqueId = CandidateId + _candidatelistCandidate[0].uniquenumber;
                    string URL = "<a href='http://www.iaminterviewed.com/'>www.iaminterviewed.com </a>";
                    string InterviewURL = "<a href='http://www.iaminterviewed.com/Interview.html?username=" + candidateUniqueId + "'>Join Interview </a>";
                    string InterviewerName = _candidatelistinterviewer.Count > 0 ? _candidatelistinterviewer[0].CandidateName : "Saran";
                    string mainTopics = _candidatelistCandidate[0].PrimarySkillName + "( " + _candidatelistCandidate[0].SecondarySkill1Name + ", " + _candidatelistCandidate[0].SecondarySkill2Name +
                        ", " + _candidatelistCandidate[0].SecondarySkill3Name + ", " + _candidatelistCandidate[0].SecondarySkill4Name + ", " + _candidatelistCandidate[0].SecondarySkill5Name + " )";
                    if (_candidatelistinterviewer.Count > 0)
                    {
                        string body = "Hi " + _candidatelistinterviewer[0].CandidateName + ",<br /><br />Please find the new  interview scheduled for you,<br /><br />"
                                            + "<strong>Name:</strong>  " + Session["UserName"] + "<br />"
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
                                            + "Client Name: " + _companyProfile[0].CompanyName + "<br />"
                                            + "Interview Topics: " + mainTopics + "<br /><br />"
                                            + "Please login here to see details: " + URL + "<br /><br />"
                                            + callMessage + " <br /><br />"
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
        public ActionResult UpdateInterviewSchedule(int ScheduleId, DateTime ReScheduleDate, string TimeSlot, string candidateId)
        {
            try
            {
                _companyDataHelper.UpdateInterviewSchedule(ScheduleId, ReScheduleDate, TimeSlot);
                List<CandidateModel> _candidatelistCandidate = _candidateDataHelper.GetCandidateProfile(Convert.ToInt32(candidateId));
                string candidateUniqueId = candidateId + _candidatelistCandidate[0].uniquenumber;
                string URL = "<a href='http://www.iaminterviewed.com/'>www.iaminterviewed.com </a>";
                string InterviewURL = "<a href='http://www.iaminterviewed.com/Interview.html?username=" + candidateUniqueId + "'>Join Interview </a>";
                string mainTopics = _candidatelistCandidate[0].PrimarySkillName + "( " + _candidatelistCandidate[0].SecondarySkill1Name + ", " + _candidatelistCandidate[0].SecondarySkill2Name +
                        ", " + _candidatelistCandidate[0].SecondarySkill3Name + ", " + _candidatelistCandidate[0].SecondarySkill4Name + ", " + _candidatelistCandidate[0].SecondarySkill5Name + " )";
                string bodyCandidate = "Hi " + _candidatelistCandidate[0].CandidateName + ",<br /><br />Your interview has been re scheduled, please find the details,<br /><br />"
                                    + "<strong>Date and Time:</strong>  " + ReScheduleDate.ToString("dd-MMM-yyyy") + ", " + getTimeSlotName(Convert.ToInt32(TimeSlot)) + "<br />"
                                    //+ "Client Name: " + _companyProfile[0].CompanyName + "<br />"
                                    + "Interview Topics: " + mainTopics + "<br /><br />"
                                    + "Please login here to see details: " + URL + "<br /><br />"
                                    + "Audio Interview you will get Phone call. Video Interview you will receive ZOOM invite. <br /><br />"
                                    + "Please Click Here to Take Interview: " + InterviewURL + ". Or you may get Phone call also.<br /><br />"
                                    + "Have a nice day...<br /><br />"
                                    + "Thanks,<br />"
                                    + "Team IAmInterviewed";
                _baseHelper.SendMail("Interview Schedule", bodyCandidate, "schedule@iaminterviewed.com", _candidatelistCandidate[0].Email, true);
                return Json(new { data = "Updated Successfully", Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
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
        public ActionResult saveCompanyRequestAccess(int reqId, int candidateId)
        {
            try
            {
                _companyDataHelper.saveCompanyRequestAccess(reqId, candidateId);
                return Json(new { data = "Access Requested Successfully", Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
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
        public ActionResult getScheduleDetailsForViewRating(int reqId, int scheduleId)
        {
            try
            {
                CompanyModel _ratingDetails = _companyDataHelper.getScheduleDetailsForViewRating(reqId, scheduleId);
                return Json(new { data = _ratingDetails, Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
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
        public ActionResult GetAllProfilesFollowUpById(int ProfileId, int companyId)
        {
            try
            {
                CompanyProfilesFollowUp _followpUpProfile = _companyDataHelper.GetAllProfilesFollowUpById(ProfileId, companyId);
                return Json(new { data = _followpUpProfile, Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var Error = "Error. <Exception:>" + "\n" + ex.Message + " ";
                return Json(new { data = "", Success = false, errorMessage = Error }, JsonRequestBehavior.AllowGet);
                //return Content("Error = " + Error);
            }
        }

        public bool IsRequirementExists(int CompanyId, string Jobcode)
        {
            bool isValid = false;
            if (_companyDataHelper.IsRequirementExists(CompanyId, Jobcode) != null)
                isValid = true;

            return isValid;
        }

        public bool IsUserExists(string email)
        {
            bool isValid = false;
            if (_account.IsUserExists(email) != null)
                isValid = true;

            return isValid;
        }

        public bool IsVendorExists(string email)
        {
            bool isValid = false;
            if (_companyDataHelper.IsVendorExists(email) != null)
                isValid = true;

            return isValid;
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