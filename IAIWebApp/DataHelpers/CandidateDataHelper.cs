using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using System.Data;
using System.Net.Mail;
using System.Reflection;
using IAIWebApp.Models;

namespace IAIWebApp.DataHelpers
{
    public class CandidateDataHelper : BaseDataHelper
    {
        public List<CandidateModel> GetCandidateProfile(int UserID)
        {
            List<CandidateModel> nwmd = new List<CandidateModel>();
            List<SqlParameter> pars = new List<SqlParameter>();
            try
            {
                pars.Add(GetSqlParameter("@CandidateId", SqlDbType.VarChar, UserID));
                DataSet ds = new DataSet();
                string[] tables = new string[] { "CandidateProfile" };
                SqlHelper.FillDataset(CS, SP, "Proc_Get_CandidateProfile", ds, tables, pars.ToArray());
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    CandidateModel _model = new CandidateModel();
                    _model.candidateid = Convert.ToInt32(ds.Tables[0].Rows[i]["CandidateId"]);
                    _model.CandidateName = ds.Tables[0].Rows[i]["CandidateName"].ToString();
                    _model.Email = ds.Tables[0].Rows[i]["Email"].ToString();
                    _model.uniquenumber = ds.Tables[0].Rows[i]["UniqueId"].ToString();
                    _model.Location = ds.Tables[0].Rows[i]["CurrentLocation"].ToString();
                    _model.PrimarySkill = Convert.ToInt32(ds.Tables[0].Rows[i]["PrimarySkill"]);
                    _model.PrimarySkillName = ds.Tables[0].Rows[i]["PrimarySkillName"].ToString();
                    _model.SecondarySkill1 = ds.Tables[0].Rows[i]["SecondarySkill1"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["SecondarySkill1"]);
                    _model.SecondarySkill2 = ds.Tables[0].Rows[i]["SecondarySkill2"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["SecondarySkill2"]);
                    _model.SecondarySkill3 = ds.Tables[0].Rows[i]["SecondarySkill3"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["SecondarySkill3"]);
                    if (string.IsNullOrEmpty(ds.Tables[0].Rows[i]["SecondarySkill4"].ToString()))
                    {
                        _model.SecondarySkill4 = 0;
                    }
                    else
                    {
                        _model.SecondarySkill4 = Convert.ToInt32(ds.Tables[0].Rows[i]["SecondarySkill4"]);
                    }
                    if (string.IsNullOrEmpty(ds.Tables[0].Rows[i]["SecondarySkill5"].ToString()))
                    {
                        _model.SecondarySkill5 = 0;
                    }
                    else
                    {
                        _model.SecondarySkill5 = Convert.ToInt32(ds.Tables[0].Rows[i]["SecondarySkill5"]);
                    }
                    _model.SecondarySkill1Name = ds.Tables[0].Rows[i]["SecondarySkillName1"].ToString();
                    _model.SecondarySkill2Name = ds.Tables[0].Rows[i]["SecondarySkillName2"].ToString();
                    _model.SecondarySkill3Name = ds.Tables[0].Rows[i]["SecondarySkillName3"].ToString();
                    _model.SecondarySkill4Name = ds.Tables[0].Rows[i]["SecondarySkillName4"].ToString();
                    _model.SecondarySkill5Name = ds.Tables[0].Rows[i]["SecondarySkillName5"].ToString();
                    _model.CurrentPay = ds.Tables[0].Rows[i]["CurrentPay"].ToString();
                    _model.ExpectedPay = ds.Tables[0].Rows[i]["ExpectedPay"].ToString();
                    _model.Experience = ds.Tables[0].Rows[i]["Experience"].ToString();
                    _model.Mobile = ds.Tables[0].Rows[i]["Phone"].ToString();
                    _model.Address = ds.Tables[0].Rows[i]["Address"].ToString();
                    _model.Resume = ds.Tables[0].Rows[i]["Resume"].ToString();
                    _model.Photo = ds.Tables[0].Rows[i]["Photo"].ToString();
                    _model.NoticePeriod = ds.Tables[0].Rows[i]["NoticePeriod"].ToString();
                    _model.PromoCode = Convert.ToInt32(ds.Tables[0].Rows[i]["PromoCode"]);
                    _model.AdditionalSkills = ds.Tables[0].Rows[i]["Additionalskills"].ToString();
                    if (ds.Tables[0].Rows[i].IsNull("GapInEducation"))
                    {
                        _model.GapInEducation = false;
                    }
                    else
                    {
                        _model.GapInEducation = Convert.ToBoolean(ds.Tables[0].Rows[i]["GapInEducation"]);
                    }
                    if (ds.Tables[0].Rows[i].IsNull("GapInExperience"))
                    {
                        _model.GapInExperience = false;
                    }
                    else
                    {
                        _model.GapInExperience = Convert.ToBoolean(ds.Tables[0].Rows[i]["GapInExperience"]);
                    }
                    if (ds.Tables[0].Rows[i].IsNull("RestrictEmployerToViewProfile"))
                    {
                        _model.GapInExperience = false;
                    }
                    else
                    {
                        _model.RestrictEmployerToViewProfile = Convert.ToBoolean(ds.Tables[0].Rows[i]["RestrictEmployerToViewProfile"]);
                    }
                    nwmd.Add(_model);
                }
                return nwmd;
            }
            catch (Exception ex)
            {
                MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                string methodName = method.Name;
                string className = method.ReflectedType.Name;
                sendErrorMail(ex, methodName, className);
                return nwmd;
            }

        }

        public List<CandidateModel> GetClosedInterviews(int Userid)
        {
            List<CandidateModel> nwmd = new List<CandidateModel>();
            List<SqlParameter> pars = new List<SqlParameter>();
            try
            {
                pars.Add(GetSqlParameter("@UserId", SqlDbType.Int, Userid));
                DataSet ds = new DataSet();
                string[] tables = new string[] { "ScheduleInterview" };
                SqlHelper.FillDataset(CS, SP, "Proc_Get_InterviewsClosed", ds, tables, pars.ToArray());
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    CandidateModel _model = new CandidateModel();
                    _model.ScheduleId = Convert.ToInt32(ds.Tables[0].Rows[i]["ScheduleId"]);
                    _model.date = Convert.ToDateTime(ds.Tables[0].Rows[i]["ScheduledDate"]);
                    _model.Interviewer = Convert.ToInt32(ds.Tables[0].Rows[i]["UserId"]);
                    _model.InterviewerName = ds.Tables[0].Rows[i]["name"].ToString();
                    _model.TimeSlotText = ds.Tables[0].Rows[i]["DailyScheduleTime"].ToString();
                    _model.CompanySchedule = ds.Tables[0].Rows[i]["CompanySchedule"].ToString();
                    _model.SecondarySkill1Rating = ds.Tables[0].Rows[i]["SecondarySkill1Rating"].ToString();
                    _model.SecondarySkill2Rating = ds.Tables[0].Rows[i]["SecondarySkill2Rating"].ToString();
                    _model.SecondarySkill3Rating = ds.Tables[0].Rows[i]["SecondarySkill3Rating"].ToString();
                    _model.SecondarySkill4Rating = ds.Tables[0].Rows[i]["SecondarySkill4Rating"].ToString();
                    _model.SecondarySkill5Rating = ds.Tables[0].Rows[i]["SecondarySkill5Rating"].ToString();
                    if ((_model.SecondarySkill4Rating == null || _model.SecondarySkill4Rating == "" || _model.SecondarySkill4Rating == "0")
                        && (_model.SecondarySkill5Rating == null || _model.SecondarySkill5Rating == "" || _model.SecondarySkill5Rating == "0"))
                    {
                        double skill1 = Convert.ToDouble(_model.SecondarySkill1Rating);
                        double skill2 = Convert.ToDouble(_model.SecondarySkill2Rating);
                        double skill3 = Convert.ToDouble(_model.SecondarySkill3Rating);
                        double rating = (skill1 + skill2 + skill3) / 3;
                        rating = Math.Round(rating, 2);
                        _model.TotalRating = rating.ToString();
                    }
                    else if (_model.SecondarySkill4Rating == null || _model.SecondarySkill4Rating == "" || _model.SecondarySkill4Rating == "0")
                    {
                        double skill1 = Convert.ToDouble(_model.SecondarySkill1Rating);
                        double skill2 = Convert.ToDouble(_model.SecondarySkill2Rating);
                        double skill3 = Convert.ToDouble(_model.SecondarySkill3Rating);
                        double skill5 = Convert.ToDouble(_model.SecondarySkill5Rating);
                        double rating = (skill1 + skill2 + skill3 + skill5) / 4;
                        rating = Math.Round(rating, 2);
                        _model.TotalRating = rating.ToString();
                    }
                    else if (_model.SecondarySkill5Rating == null || _model.SecondarySkill5Rating == "" || _model.SecondarySkill5Rating == "0")
                    {
                        double skill1 = Convert.ToDouble(_model.SecondarySkill1Rating);
                        double skill2 = Convert.ToDouble(_model.SecondarySkill2Rating);
                        double skill3 = Convert.ToDouble(_model.SecondarySkill3Rating);
                        double skill4 = Convert.ToDouble(_model.SecondarySkill4Rating);
                        double rating = (skill1 + skill2 + skill3 + skill4) / 4;
                        rating = Math.Round(rating, 2);
                        _model.TotalRating = rating.ToString();
                    }
                    else
                    {
                        double skill1 = Convert.ToDouble(_model.SecondarySkill1Rating);
                        double skill2 = Convert.ToDouble(_model.SecondarySkill2Rating);
                        double skill3 = Convert.ToDouble(_model.SecondarySkill3Rating);
                        double skill4 = Convert.ToDouble(_model.SecondarySkill4Rating);
                        double skill5 = Convert.ToDouble(_model.SecondarySkill5Rating);
                        double rating = (skill1 + skill2 + skill3 + skill4 + skill5) / 5;
                        rating = Math.Round(rating, 2);
                        _model.TotalRating = rating.ToString();
                    }
                    _model.EnglishCommunication = ds.Tables[0].Rows[i]["EnglishCommunication"].ToString();
                    _model.Attitude = ds.Tables[0].Rows[i]["Attitude"].ToString();
                    _model.InterpersonalSkillCommunication = ds.Tables[0].Rows[i]["InterpersonalSkillCommunication"].ToString();
                    _model.InterviewerComments = ds.Tables[0].Rows[i]["InterviewerRemarks"].ToString();
                    _model.AudioFile = ds.Tables[0].Rows[i]["AudioFile"].ToString();
                    _model.VideoFile = ds.Tables[0].Rows[i]["VideoFile"].ToString();
                    _model.RatingAcceptance = ds.Tables[0].Rows[i]["IsRatingAcceptedbyUser"].ToString();
                    if (_model.RatingAcceptance == "True")
                    {
                        _model.RatingAcceptance = "Yes";
                    }
                    else if (_model.RatingAcceptance == "False")
                    {
                        _model.RatingAcceptance = "No";
                    }
                    else
                    {
                        _model.RatingAcceptance = "No Action";
                    }
                    nwmd.Add(_model);
                }
                return nwmd;
            }
            catch (Exception ex)
            {
                MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                string methodName = method.Name;
                string className = method.ReflectedType.Name;
                sendErrorMail(ex, methodName, className);
                return nwmd;
            }

        }

        public List<CandidateModel> GetScheduledInterview(int Userid)
        {
            List<CandidateModel> nwmd = new List<CandidateModel>();
            List<SqlParameter> pars = new List<SqlParameter>();
            try
            {
                pars.Add(GetSqlParameter("@UserId", SqlDbType.Int, Userid));
                DataSet ds = new DataSet();
                string[] tables = new string[] { "ScheduleInterview" };
                SqlHelper.FillDataset(CS, SP, "Proc_Get_ScheduledInterviewForUser", ds, tables, pars.ToArray());
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    CandidateModel _model = new CandidateModel();
                    _model.date = Convert.ToDateTime(ds.Tables[0].Rows[i]["ScheduledDate"]);
                    _model.Interviewer = Convert.ToInt32(ds.Tables[0].Rows[i]["userid"]);
                    _model.InterviewerName = ds.Tables[0].Rows[i]["name"].ToString();
                    _model.TimeSlotText = ds.Tables[0].Rows[i]["DailyScheduleTime"].ToString();
                    _model.DisplayScheduleDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["ScheduledDate"]).ToString("dd/MMM/yyyy") + " / " + ds.Tables[0].Rows[i]["DailyScheduleTime"].ToString();
                    _model.AcceptedByInterviewer = ds.Tables[0].Rows[i]["isconfirmedbyinterviewer"].ToString();
                    _model.CompanySchedule = ds.Tables[0].Rows[i]["CompanySchedule"].ToString();
                    if (_model.AcceptedByInterviewer == "True")
                    {
                        _model.AcceptedByInterviewer = "Yes";
                    }
                    else if (_model.AcceptedByInterviewer == "False")
                    {
                        _model.AcceptedByInterviewer = "No";
                    }
                    else
                    {
                        _model.AcceptedByInterviewer = "No Action";
                    }
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        if (ds.Tables[1].Rows[0].IsNull("readytochange"))
                        {
                            _model.readytochange = false;
                        }
                        else
                        {
                            _model.readytochange = Convert.ToBoolean(ds.Tables[1].Rows[0]["readytochange"]);
                        }
                    }
                    nwmd.Add(_model);
                }
                for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                {
                    CandidateModel _model = new CandidateModel();
                    _model.ScheduleId = Convert.ToInt32(ds.Tables[2].Rows[i]["ScheduleId"]);
                    _model.date = Convert.ToDateTime(ds.Tables[2].Rows[i]["ScheduledDate"]);
                    _model.Interviewer = Convert.ToInt32(ds.Tables[2].Rows[i]["UserId"]);
                    _model.InterviewerName = ds.Tables[2].Rows[i]["name"].ToString();
                    _model.TimeSlotText = ds.Tables[2].Rows[i]["DailyScheduleTime"].ToString();
                    _model.DisplayScheduleDate = Convert.ToDateTime(ds.Tables[2].Rows[i]["ScheduledDate"]).ToString("dd/MMM/yyyy") + " / " + ds.Tables[2].Rows[i]["DailyScheduleTime"].ToString();
                    _model.AcceptedByInterviewer = ds.Tables[2].Rows[i]["isconfirmedbyinterviewer"].ToString();
                    _model.CompanySchedule = ds.Tables[2].Rows[i]["CompanySchedule"].ToString();
                    if (_model.AcceptedByInterviewer == "True")
                    {
                        _model.AcceptedByInterviewer = "Yes";
                    }
                    else if (_model.AcceptedByInterviewer == "False")
                    {
                        _model.AcceptedByInterviewer = "No";
                    }
                    else
                    {
                        _model.AcceptedByInterviewer = "No Action";
                    }
                    _model.SecondarySkill1Rating = ds.Tables[2].Rows[i]["SecondarySkill1Rating"].ToString();
                    _model.SecondarySkill2Rating = ds.Tables[2].Rows[i]["SecondarySkill2Rating"].ToString();
                    _model.SecondarySkill3Rating = ds.Tables[2].Rows[i]["SecondarySkill3Rating"].ToString();
                    _model.SecondarySkill4Rating = ds.Tables[2].Rows[i]["SecondarySkill4Rating"].ToString();
                    _model.SecondarySkill5Rating = ds.Tables[2].Rows[i]["SecondarySkill5Rating"].ToString();
                    if ((_model.SecondarySkill4Rating == null || _model.SecondarySkill4Rating == "" || _model.SecondarySkill4Rating == "0")
                        && (_model.SecondarySkill5Rating == null || _model.SecondarySkill5Rating == "" || _model.SecondarySkill5Rating == "0"))
                    {
                        double skill1 = Convert.ToDouble(_model.SecondarySkill1Rating);
                        double skill2 = Convert.ToDouble(_model.SecondarySkill2Rating);
                        double skill3 = Convert.ToDouble(_model.SecondarySkill3Rating);
                        double rating = (skill1 + skill2 + skill3) / 3;
                        rating = Math.Round(rating, 2);
                        _model.TotalRating = rating.ToString();
                    }
                    else if (_model.SecondarySkill4Rating == null || _model.SecondarySkill4Rating == "" || _model.SecondarySkill4Rating == "0")
                    {
                        double skill1 = Convert.ToDouble(_model.SecondarySkill1Rating);
                        double skill2 = Convert.ToDouble(_model.SecondarySkill2Rating);
                        double skill3 = Convert.ToDouble(_model.SecondarySkill3Rating);
                        double skill5 = Convert.ToDouble(_model.SecondarySkill5Rating);
                        double rating = (skill1 + skill2 + skill3 + skill5) / 4;
                        rating = Math.Round(rating, 2);
                        _model.TotalRating = rating.ToString();
                    }
                    else if (_model.SecondarySkill5Rating == null || _model.SecondarySkill5Rating == "" || _model.SecondarySkill5Rating == "0")
                    {
                        double skill1 = Convert.ToDouble(_model.SecondarySkill1Rating);
                        double skill2 = Convert.ToDouble(_model.SecondarySkill2Rating);
                        double skill3 = Convert.ToDouble(_model.SecondarySkill3Rating);
                        double skill4 = Convert.ToDouble(_model.SecondarySkill4Rating);
                        double rating = (skill1 + skill2 + skill3 + skill4) / 4;
                        rating = Math.Round(rating, 2);
                        _model.TotalRating = rating.ToString();
                    }
                    else
                    {
                        double skill1 = Convert.ToDouble(_model.SecondarySkill1Rating);
                        double skill2 = Convert.ToDouble(_model.SecondarySkill2Rating);
                        double skill3 = Convert.ToDouble(_model.SecondarySkill3Rating);
                        double skill4 = Convert.ToDouble(_model.SecondarySkill4Rating);
                        double skill5 = Convert.ToDouble(_model.SecondarySkill5Rating);
                        double rating = (skill1 + skill2 + skill3 + skill4 + skill5) / 5;
                        rating = Math.Round(rating, 2);
                        _model.TotalRating = rating.ToString();
                    }
                    _model.EnglishCommunication = ds.Tables[2].Rows[i]["EnglishCommunication"].ToString();
                    _model.Attitude = ds.Tables[2].Rows[i]["Attitude"].ToString();
                    _model.InterpersonalSkillCommunication = ds.Tables[2].Rows[i]["InterpersonalSkillCommunication"].ToString();
                    _model.InterviewerComments = ds.Tables[2].Rows[i]["InterviewerRemarks"].ToString();
                    _model.AudioFile = ds.Tables[2].Rows[i]["AudioFile"].ToString();
                    _model.VideoFile = ds.Tables[2].Rows[i]["VideoFile"].ToString();
                    _model.RatingAcceptance = ds.Tables[2].Rows[i]["IsRatingAcceptedbyUser"].ToString();
                    if (_model.RatingAcceptance == "True")
                    {
                        _model.RatingAcceptance = "Yes";
                    }
                    else if (_model.RatingAcceptance == "False")
                    {
                        _model.RatingAcceptance = "No";
                    }
                    else
                    {
                        _model.RatingAcceptance = "No Action";
                    }
                    nwmd.Add(_model);
                }
                return nwmd;
            }
            catch (Exception ex)
            {
                MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                string methodName = method.Name;
                string className = method.ReflectedType.Name;
                sendErrorMail(ex, methodName, className);
                return nwmd;
            }

        }

        public List<CandidateModel> GetFavoriteCompany(int Userid)
        {
            List<CandidateModel> nwmd = new List<CandidateModel>();
            List<SqlParameter> pars = new List<SqlParameter>();
            try
            {
                pars.Add(GetSqlParameter("@UserId", SqlDbType.Int, Userid));
                DataSet ds = new DataSet();
                string[] tables = new string[] { "FavoriteCompany" };
                SqlHelper.FillDataset(CS, SP, "Proc_Get_FavoriteCompany", ds, tables, pars.ToArray());
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    CandidateModel _model = new CandidateModel();
                    _model.CompanyName = ds.Tables[0].Rows[i]["name"].ToString();
                    _model.AppliedDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["AppliedDate"]);
                    _model.DisplayScheduleDate = _model.AppliedDate.ToString("dd/MMM/yyyy");
                    _model.CompanyId = Convert.ToInt32(ds.Tables[0].Rows[i]["CompanyId"]);
                    _model.DesignationId = Convert.ToInt32(ds.Tables[0].Rows[i]["DesignationId"]);
                    _model.Designation = ds.Tables[0].Rows[i]["Designation"].ToString();
                    _model.Applicationid = Convert.ToInt32(ds.Tables[0].Rows[i]["applicationid"]);
                    nwmd.Add(_model);
                }
                return nwmd;
            }
            catch (Exception ex)
            {
                MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                string methodName = method.Name;
                string className = method.ReflectedType.Name;
                sendErrorMail(ex, methodName, className);
                return nwmd;
            }

        }

        public List<CandidateModel> GetViewedProfiles(int Userid)
        {
            List<CandidateModel> nwmd = new List<CandidateModel>();
            List<SqlParameter> pars = new List<SqlParameter>();
            try
            {
                pars.Add(GetSqlParameter("@UserID", SqlDbType.Int, Userid));
                DataSet ds = new DataSet();
                string[] tables = new string[] { "Company" };
                SqlHelper.FillDataset(CS, SP, "Proc_Select_CompaniesWhoViewedCandidate", ds, tables, pars.ToArray());
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    CandidateModel _model = new CandidateModel();
                    _model.CompanyName = ds.Tables[0].Rows[i]["Name"].ToString();
                    _model.ViewedDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["CreatedDate"]);
                    _model.DisplayScheduleDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["CreatedDate"]).ToString("dd/MMM/yyyy");
                    _model.CompanyId = Convert.ToInt32(ds.Tables[0].Rows[i]["CompanyId"]);
                    _model.ReqId = Convert.ToInt32(ds.Tables[0].Rows[i]["ReqId"]);
                    _model.JobCode = ds.Tables[0].Rows[i]["JobCode"].ToString();
                    _model.Status = ds.Tables[0].Rows[i]["Status"].ToString();
                    _model.GrantAccess = ds.Tables[0].Rows[i]["GrantAccess"].ToString();
                    //_model.Applicationid = Convert.ToInt32(ds.Tables[0].Rows[i]["applicationid"]);
                    nwmd.Add(_model);
                }
                return nwmd;
            }
            catch (Exception ex)
            {
                MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                string methodName = method.Name;
                string className = method.ReflectedType.Name;
                sendErrorMail(ex, methodName, className);
                return nwmd;
            }

        }

        public void UpdateGrantAccess(int CandidateId, int ReqId)
        {
            try
            {
                List<SqlParameter> pars = new List<SqlParameter>();
                pars.Add(GetSqlParameter("@CandidateId", SqlDbType.BigInt, CandidateId));
                pars.Add(GetSqlParameter("@ReqId", SqlDbType.BigInt, ReqId));
                SqlHelper.ExecuteNonQuery(CS, SP, "Proc_Update_AccessToViewProfile", pars.ToArray());
            }
            catch (Exception ex)
            {
                MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                string methodName = method.Name;
                string className = method.ReflectedType.Name;
                sendErrorMail(ex, methodName, className);
            }

        }

        public List<CompanyModel> GetRecruiterDetailsByReqId(int ReqId)
        {
            List<CompanyModel> nwmd = new List<CompanyModel>();
            try
            {
                List<SqlParameter> pars = new List<SqlParameter>();
                pars.Add(GetSqlParameter("@ReqId", SqlDbType.Int, ReqId));
                DataSet dsmain = new DataSet();
                string[] tables = new string[] { "CompanyProfiles" };
                SqlHelper.FillDataset(CS, SP, "Proc_Get_RecruiterDetailsByReqId", dsmain, tables, pars.ToArray());
                int count = dsmain.Tables[0].Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    CompanyModel _model = new CompanyModel();
                    _model.RecruiterEmail = dsmain.Tables[0].Rows[i]["RecruiterEmail"].ToString();
                    _model.AssignToEmail = dsmain.Tables[0].Rows[i]["AssignedEmail"].ToString();
                    _model.PMEmail = dsmain.Tables[0].Rows[i]["PMEmail"].ToString();
                    nwmd.Add(_model);
                }
                return nwmd;
            }
            catch (Exception ex)
            {
                MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                string methodName = method.Name;
                string className = method.ReflectedType.Name;
                sendErrorMail(ex, methodName, className);
                return nwmd;
            }

        }

        public List<InterviewerModel> GetInterviewSchedulebyId(int ScheduleId)
        {
            List<InterviewerModel> nwmd = new List<InterviewerModel>();
            List<SqlParameter> pars = new List<SqlParameter>();
            try
            {
                pars.Add(GetSqlParameter("@ScheduleId", SqlDbType.Int, ScheduleId));
                DataSet ds = new DataSet();
                string[] tables = new string[] { "ScheduleInterview" };
                SqlHelper.FillDataset(CS, SP, "Proc_Get_InterviewScheduleById", ds, tables, pars.ToArray());
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    InterviewerModel _model = new InterviewerModel();
                    _model.ScheduleId = Convert.ToInt32(ds.Tables[0].Rows[i]["ScheduleId"]);
                    _model.UserId = Convert.ToInt32(ds.Tables[0].Rows[i]["UserId"]);
                    _model.CandidateName = ds.Tables[0].Rows[i]["name"].ToString();
                    _model.InterviewDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["ScheduledDate"]);
                    _model.TimeSlot = ds.Tables[0].Rows[i]["DailyScheduleTime"].ToString();
                    _model.PrimarySkill = ds.Tables[0].Rows[i]["SkillName"].ToString();
                    _model.SecondarySkill1 = ds.Tables[0].Rows[i]["CandidateSecondarySkill1Name"].ToString();
                    _model.SecondarySkill2 = ds.Tables[0].Rows[i]["CandidateSecondarySkill2Name"].ToString();
                    _model.SecondarySkill3 = ds.Tables[0].Rows[i]["CandidateSecondarySkill3Name"].ToString();
                    _model.SecondarySkill4 = ds.Tables[0].Rows[i]["CandidateSecondarySkill4Name"].ToString();
                    _model.SecondarySkill5 = ds.Tables[0].Rows[i]["CandidateSecondarySkill5Name"].ToString();
                    _model.SecondarySkill1Rating = (ds.Tables[0].Rows[i]["SecondarySkill1Rating"].ToString());
                    _model.SecondarySkill2Rating = (ds.Tables[0].Rows[i]["SecondarySkill2Rating"].ToString());
                    _model.SecondarySkill3Rating = (ds.Tables[0].Rows[i]["SecondarySkill3Rating"].ToString());
                    _model.SecondarySkill4Rating = (ds.Tables[0].Rows[i]["SecondarySkill4Rating"].ToString());
                    _model.SecondarySkill5Rating = (ds.Tables[0].Rows[i]["SecondarySkill5Rating"].ToString());
                    string skill1rating = _model.SecondarySkill1Rating;
                    if (_model.SecondarySkill1Rating != "")
                    {
                        if ((_model.SecondarySkill4Rating == null || _model.SecondarySkill4Rating == "" || _model.SecondarySkill4Rating == "0") &&
                        (_model.SecondarySkill5Rating == null || _model.SecondarySkill5Rating == "" || _model.SecondarySkill5Rating == "0"))
                        {
                            double skill1 = Convert.ToDouble(_model.SecondarySkill1Rating);
                            double skill2 = Convert.ToDouble(_model.SecondarySkill2Rating);
                            double skill3 = Convert.ToDouble(_model.SecondarySkill3Rating);
                            double rating = (skill1 + skill2 + skill3) / 3;
                            rating = Math.Round(rating, 2);
                            _model.TotalRating = rating.ToString();
                        }
                        else if (_model.SecondarySkill4Rating == null || _model.SecondarySkill4Rating == "" || _model.SecondarySkill4Rating == "0")
                        {
                            double skill1 = Convert.ToDouble(_model.SecondarySkill1Rating);
                            double skill2 = Convert.ToDouble(_model.SecondarySkill2Rating);
                            double skill3 = Convert.ToDouble(_model.SecondarySkill3Rating);
                            double skill5 = Convert.ToDouble(_model.SecondarySkill5Rating);
                            double rating = (skill1 + skill2 + skill3 + skill5) / 4;
                            rating = Math.Round(rating, 2);
                            _model.TotalRating = rating.ToString();
                        }
                        else if (_model.SecondarySkill5Rating == null || _model.SecondarySkill5Rating == "" || _model.SecondarySkill5Rating == "0")
                        {
                            double skill1 = Convert.ToDouble(_model.SecondarySkill1Rating);
                            double skill2 = Convert.ToDouble(_model.SecondarySkill2Rating);
                            double skill3 = Convert.ToDouble(_model.SecondarySkill3Rating);
                            double skill4 = Convert.ToDouble(_model.SecondarySkill4Rating);
                            double rating = (skill1 + skill2 + skill3 + skill4) / 4;
                            rating = Math.Round(rating, 2);
                            _model.TotalRating = rating.ToString();
                        }
                        else
                        {
                            double skill1 = Convert.ToDouble(_model.SecondarySkill1Rating);
                            double skill2 = Convert.ToDouble(_model.SecondarySkill2Rating);
                            double skill3 = Convert.ToDouble(_model.SecondarySkill3Rating);
                            double skill4 = Convert.ToDouble(_model.SecondarySkill4Rating);
                            double skill5 = Convert.ToDouble(_model.SecondarySkill5Rating);
                            double rating = (skill1 + skill2 + skill3 + skill4 + skill5) / 5;
                            rating = Math.Round(rating, 2);
                            _model.TotalRating = rating.ToString();
                        }
                    }
                    _model.EnglishCommunication = (ds.Tables[0].Rows[i]["EnglishCommunication"].ToString());
                    _model.Attitude = (ds.Tables[0].Rows[i]["Attitude"].ToString());
                    _model.InterpersonalSkillCommunication = (ds.Tables[0].Rows[i]["InterpersonalSkillCommunication"].ToString());
                    if (!string.IsNullOrEmpty(_model.EnglishCommunication) && !string.IsNullOrEmpty(_model.Attitude) && !string.IsNullOrEmpty(_model.InterpersonalSkillCommunication))
                    {
                        double engcomm = Convert.ToDouble(_model.EnglishCommunication);
                        double attitude = Convert.ToDouble(_model.Attitude);
                        double intercomm = Convert.ToDouble(_model.InterpersonalSkillCommunication);
                        double softskillrating = (engcomm + attitude + intercomm) / 3;
                        softskillrating = Math.Round(softskillrating, 2);
                        _model.SoftSkillRating = softskillrating.ToString();
                    }
                    else
                    {
                        _model.SoftSkillRating = "";
                    }

                    _model.InterviewerRemarks = ds.Tables[0].Rows[i]["InterviewerRemarks"].ToString();
                    _model.AudioFile = ds.Tables[0].Rows[i]["AudioFile"].ToString();
                    _model.VideoFile = ds.Tables[0].Rows[i]["VideoFile"].ToString();
                    _model.MobileNumber = ds.Tables[0].Rows[i]["Phone"].ToString();
                    _model.Email = ds.Tables[0].Rows[i]["Email"].ToString();
                    _model.SecondarySkill1Remarks = ds.Tables[0].Rows[i]["SecSkill1Remarks"].ToString();
                    _model.SecondarySkill2Remarks = ds.Tables[0].Rows[i]["SecSkill2Remarks"].ToString();
                    _model.SecondarySkill3Remarks = ds.Tables[0].Rows[i]["SecSkill3Remarks"].ToString();
                    _model.SecondarySkill4Remarks = ds.Tables[0].Rows[i]["SecSkill4Remarks"].ToString();
                    _model.SecondarySkill5Remarks = ds.Tables[0].Rows[i]["SecSkill5Remarks"].ToString();
                    _model.EnglishCommunicationRemarks = ds.Tables[0].Rows[i]["EnglishCommunicationRemarks"].ToString();
                    _model.AttitudeRemarks = ds.Tables[0].Rows[i]["AttitudeRemarks"].ToString();
                    _model.InterpersonalSkillCommunicationRemarks = ds.Tables[0].Rows[i]["InterpersonalSkillCommunicationRemarks"].ToString();
                    _model.RatingAccepted = ds.Tables[0].Rows[i]["IsRatingAcceptedbyUser"].ToString();
                    _model.KeyResponsibilities = ds.Tables[0].Rows[i]["KeyResponsibilities"].ToString();
                    if (_model.RatingAccepted == "True")
                    {
                        _model.RatingAccepted = "Yes";
                    }
                    else if (_model.RatingAccepted == "False")
                    {
                        _model.RatingAccepted = "No";
                    }
                    else
                    {
                        _model.RatingAccepted = "No Action";
                    }
                    nwmd.Add(_model);
                }
                return nwmd;
            }
            catch (Exception ex)
            {
                MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                string methodName = method.Name;
                string className = method.ReflectedType.Name;
                sendErrorMail(ex, methodName, className);
                return nwmd;
            }
        }

        public void AcceptRating(string scheduleid, string status)
        {
            try
            {
                List<SqlParameter> pars = new List<SqlParameter>();
                pars.Add(GetSqlParameter("@ScheduleId", SqlDbType.VarChar, scheduleid));
                pars.Add(GetSqlParameter("@Status", SqlDbType.VarChar, status));
                SqlHelper.ExecuteNonQuery(CS, SP, "Proc_Accept_CandidateRating", pars.ToArray());
            }
            catch (Exception ex)
            {
                MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                string methodName = method.Name;
                string className = method.ReflectedType.Name;
                sendErrorMail(ex, methodName, className);
            }

        }

        public void SaveCandidateProfile(CandidateProfileModel _candidate)
        {
            try
            {
                List<SqlParameter> pars = new List<SqlParameter>();
                pars.Add(GetSqlParameter("@CandidateId", SqlDbType.Int, _candidate.CandidateId));
                pars.Add(GetSqlParameter("@PrimarySkill", SqlDbType.Int, _candidate.PrimarySkill));
                pars.Add(GetSqlParameter("@SecondarySkill1", SqlDbType.Int, _candidate.SecondarySkill1));
                pars.Add(GetSqlParameter("@SecondarySkill2", SqlDbType.Int, _candidate.SecondarySkill2));
                pars.Add(GetSqlParameter("@SecondarySkill3", SqlDbType.Int, _candidate.SecondarySkill3));
                pars.Add(GetSqlParameter("@SecondarySkill4", SqlDbType.Int, _candidate.SecondarySkill4));
                pars.Add(GetSqlParameter("@SecondarySkill5", SqlDbType.Int, _candidate.SecondarySkill5));
                pars.Add(GetSqlParameter("@CurrentLocation", SqlDbType.VarChar, _candidate.Location));
                pars.Add(GetSqlParameter("@CurrentPay", SqlDbType.VarChar, _candidate.CurrentPay));
                pars.Add(GetSqlParameter("@ExpectedPay", SqlDbType.VarChar, _candidate.ExpectedPay));
                pars.Add(GetSqlParameter("@Experience", SqlDbType.VarChar, _candidate.Experience));
                pars.Add(GetSqlParameter("@Phone", SqlDbType.VarChar, _candidate.MobileNo));
                pars.Add(GetSqlParameter("@Address", SqlDbType.VarChar, _candidate.Address));
                pars.Add(GetSqlParameter("@Resume", SqlDbType.VarChar, _candidate.Resume));
                pars.Add(GetSqlParameter("@Photo", SqlDbType.VarChar, ""));
                pars.Add(GetSqlParameter("@NoticePeriod", SqlDbType.VarChar, _candidate.NoticePeriod));
                pars.Add(GetSqlParameter("@GapInEducation", SqlDbType.Bit, _candidate.GapInEducation));
                pars.Add(GetSqlParameter("@GapInExperience", SqlDbType.Bit, _candidate.GapInExperience));
                pars.Add(GetSqlParameter("@AdditionalSkills", SqlDbType.VarChar, _candidate.AdditionalSkills));
                pars.Add(GetSqlParameter("@RestrictEmployerToViewProfile", SqlDbType.Bit, _candidate.RestrictEmployerToViewProfile));
                SqlHelper.ExecuteNonQuery(CS, SP, "Proc_Save_CandidateProfile", pars.ToArray());
            }
            catch (Exception ex)
            {
                MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                string methodName = method.Name;
                string className = method.ReflectedType.Name;
                sendErrorMail(ex, methodName, className);
            }
        }

        public List<InterviewerModel> GetInterviewerByDate(DateTime ScheduleDate, int UserId)
        {
            List<InterviewerModel> nwmd = new List<InterviewerModel>();
            List<SqlParameter> pars = new List<SqlParameter>();
            try
            {
                pars.Add(GetSqlParameter("@date", SqlDbType.DateTime, ScheduleDate));
                pars.Add(GetSqlParameter("@UserId", SqlDbType.Int, UserId));
                DataSet ds = new DataSet();
                string[] tables = new string[] { "InterviewerSchedule" };
                SqlHelper.FillDataset(CS, SP, "Proc_Fill_InterviewerByDate", ds, tables, pars.ToArray());
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    InterviewerModel _model = new InterviewerModel();
                    _model.UserId = Convert.ToInt32(ds.Tables[0].Rows[i]["InterviewerId"]);
                    _model.InterviewerName = ds.Tables[0].Rows[i]["Name"].ToString();

                    List<SqlParameter> parsTime = new List<SqlParameter>();
                    parsTime.Add(GetSqlParameter("@date", SqlDbType.DateTime, ScheduleDate));
                    parsTime.Add(GetSqlParameter("@UserId", SqlDbType.Int, _model.UserId));
                    DataSet dsTime = new DataSet();
                    string[] tablesTime = new string[] { "InterviewerSchedule" };
                    SqlHelper.FillDataset(CS, SP, "Proc_Get_InterviewerSlotsByDate", dsTime, tablesTime, parsTime.ToArray());
                    List<TimeSlotModel> _modelTimeArray = new List<TimeSlotModel>();
                    if (dsTime.Tables[0].Rows.Count < 1)
                    {
                        SqlHelper.FillDataset(CS, SP, "Proc_Fill_DailySchedule", dsTime, tablesTime, parsTime.ToArray());
                        for (int t = 0; t < dsTime.Tables[0].Rows.Count; t++)
                        {
                            TimeSlotModel _modelTime = new TimeSlotModel();
                            _modelTime.TimeSlotId = Convert.ToInt32(dsTime.Tables[0].Rows[t]["DailyScheduleId"]);
                            _modelTime.TimeSlot = dsTime.Tables[0].Rows[t]["DailyScheduleTime"].ToString();
                            _modelTimeArray.Add(_modelTime);
                        }
                    }
                    else
                    {
                        for (int t = 0; t < dsTime.Tables[0].Rows.Count; t++)
                        {
                            TimeSlotModel _modelTime = new TimeSlotModel();
                            _modelTime.TimeSlotId = Convert.ToInt32(dsTime.Tables[0].Rows[t]["AvailableTimeSlot"]);
                            _modelTime.TimeSlot = dsTime.Tables[0].Rows[t]["DailyScheduleTime"].ToString();
                            _modelTimeArray.Add(_modelTime);
                        }
                    }
                    _model.timeSlotList = _modelTimeArray;
                    nwmd.Add(_model);
                }
                return nwmd;
            }
            catch (Exception ex)
            {
                MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                string methodName = method.Name;
                string className = method.ReflectedType.Name;
                sendErrorMail(ex, methodName, className);
                return nwmd;
            }
        }

        public List<InterviewerModel> GetDailyScheduleTimeAll(DateTime ScheduleDate, int InterviewerId)
        {
            List<InterviewerModel> nwmd = new List<InterviewerModel>();
            List<SqlParameter> pars = new List<SqlParameter>();
            try
            {
                pars.Add(GetSqlParameter("@UserId", SqlDbType.Int, InterviewerId));
                pars.Add(GetSqlParameter("@date", SqlDbType.DateTime, ScheduleDate));
                DataSet ds = new DataSet();
                string[] tables = new string[] { "DailySchedule" };
                SqlHelper.FillDataset(CS, SP, "Proc_Fill_DailySchedule", ds, tables, pars.ToArray());
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    InterviewerModel _model = new InterviewerModel();
                    _model.AvailableTimeId = Convert.ToInt32(ds.Tables[0].Rows[i]["DailyScheduleId"]);
                    _model.AvailableTime = ds.Tables[0].Rows[i]["DailyScheduleTime"].ToString();
                    nwmd.Add(_model);
                }
                return nwmd;
            }
            catch (Exception ex)
            {
                MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                string methodName = method.Name;
                string className = method.ReflectedType.Name;
                sendErrorMail(ex, methodName, className);
                return nwmd;
            }
        }

        public List<InterviewerModel> GetInterviewerTimeSlotsByDate(DateTime ScheduleDate, int InterviewerId)
        {
            List<InterviewerModel> nwmd = new List<InterviewerModel>();
            List<SqlParameter> pars = new List<SqlParameter>();
            try
            {
                pars.Add(GetSqlParameter("@date", SqlDbType.DateTime, ScheduleDate));
                pars.Add(GetSqlParameter("@UserId", SqlDbType.Int, InterviewerId));
                DataSet ds = new DataSet();
                string[] tables = new string[] { "InterviewerSchedule" };
                SqlHelper.FillDataset(CS, SP, "Proc_Get_InterviewerSlotsByDate", ds, tables, pars.ToArray());
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    InterviewerModel _model = new InterviewerModel();
                    _model.AvailableTimeId = Convert.ToInt32(ds.Tables[0].Rows[i]["AvailableTimeSlot"]);
                    _model.AvailableTime = ds.Tables[0].Rows[i]["DailyScheduleTime"].ToString();
                    nwmd.Add(_model);
                }
                return nwmd;
            }
            catch (Exception ex)
            {
                MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                string methodName = method.Name;
                string className = method.ReflectedType.Name;
                sendErrorMail(ex, methodName, className);
                return nwmd;
            }
        }

        public string SaveInterviewSchedule(DateTime ScheduleDate, string Interviewer, string TimeSlot, int CandidateId, string InterviewType, out string Message)
        {
            string returnValue = string.Empty;
            //try
            //{                
            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(GetSqlParameter("@Date", SqlDbType.DateTime, ScheduleDate));
            pars.Add(GetSqlParameter("@Interviewer", SqlDbType.VarChar, Interviewer));
            pars.Add(GetSqlParameter("@TimeSlot", SqlDbType.VarChar, TimeSlot));
            pars.Add(GetSqlParameter("@UserId", SqlDbType.Int, CandidateId));
            pars.Add(GetSqlParameter("@InterviewType", SqlDbType.VarChar, InterviewType));
            pars.Add(GetSqlParameter("@Message", SqlDbType.VarChar, DBNull.Value));
            pars[5].Size = 1;
            pars[5].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(CS, SP, "Proc_Save_InterviewSchedule", pars.ToArray());
            Message = pars[5].Value.ToString();

            return returnValue;
            //}
            //catch (Exception ex)
            //{
            //    sendErrorMail(ex);
            //    return returnValue;
            //}           
        }

        public string SaveInterviewSchedulePayment(DateTime ScheduleDate, string Interviewer, string TimeSlot, int CandidateId, string InterviewType, string UniqueNumber, out string Message)
        {
            string returnValue = string.Empty;
            //try
            //{

            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(GetSqlParameter("@Date", SqlDbType.DateTime, ScheduleDate));
            pars.Add(GetSqlParameter("@Interviewer", SqlDbType.VarChar, Interviewer));
            pars.Add(GetSqlParameter("@TimeSlot", SqlDbType.VarChar, TimeSlot));
            pars.Add(GetSqlParameter("@UserId", SqlDbType.Int, CandidateId));
            pars.Add(GetSqlParameter("@InterviewType", SqlDbType.VarChar, InterviewType));
            pars.Add(GetSqlParameter("@UniqueId", SqlDbType.VarChar, UniqueNumber));
            pars.Add(GetSqlParameter("@Message", SqlDbType.VarChar, DBNull.Value));
            pars[6].Size = 1;
            pars[6].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(CS, SP, "Proc_Save_InterviewSchedulePayment", pars.ToArray());
            Message = pars[6].Value.ToString();

            return returnValue;
            //}
            //catch (Exception ex)
            //{
            //    sendErrorMail(ex);
            //    return returnValue;
            //}

        }

        public List<CompanyModel> fillFavoriteCompany(string userid)
        {
            List<CompanyModel> nwmd = new List<CompanyModel>();
            List<SqlParameter> pars = new List<SqlParameter>();
            try
            {
                pars.Add(GetSqlParameter("@UserId", SqlDbType.VarChar, userid));
                DataSet ds = new DataSet();
                string[] tables = new string[] { "Users" };
                SqlHelper.FillDataset(CS, SP, "Proc_Fill_FavoriteCompany", ds, tables, pars.ToArray());
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    CompanyModel _model = new CompanyModel();
                    _model.UserId = Convert.ToInt32(ds.Tables[0].Rows[i]["UserId"]);
                    _model.CompanyName = ds.Tables[0].Rows[i]["Name"].ToString();
                    nwmd.Add(_model);
                }
                return nwmd;
            }
            catch (Exception ex)
            {
                MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                string methodName = method.Name;
                string className = method.ReflectedType.Name;
                sendErrorMail(ex, methodName, className);
                return nwmd;
            }
        }

        public List<DesignationModel> GetDesignation(int jtStartIndex, int jtPageSize, string jtSorting, int CompanyId)
        {
            List<DesignationModel> nwmd = new List<DesignationModel>();
            List<SqlParameter> pars = new List<SqlParameter>();
            try
            {
                pars.Add(GetSqlParameter("@FirstRow", SqlDbType.Int, jtStartIndex));
                pars.Add(GetSqlParameter("@LastRow", SqlDbType.Int, jtPageSize));
                pars.Add(GetSqlParameter("@OrderBy", SqlDbType.VarChar, jtSorting));
                pars.Add(GetSqlParameter("@CompanyId", SqlDbType.Int, CompanyId));
                DataSet ds = new DataSet();
                string[] tables = new string[] { "Designation" };
                SqlHelper.FillDataset(CS, SP, "Proc_Select_Designation", ds, tables, pars.ToArray());
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DesignationModel _model = new DesignationModel();
                    _model.DesignationId = Convert.ToInt32(ds.Tables[0].Rows[i]["DesignationId"]);
                    _model.NewDesignationId = ds.Tables[0].Rows[i]["DesignationId"].ToString();
                    _model.Designation = ds.Tables[0].Rows[i]["Designation"].ToString();
                    _model.Description = ds.Tables[0].Rows[i]["Description"].ToString();
                    _model.RowNo = Convert.ToInt32(ds.Tables[0].Rows[i]["RowNo"]);
                    _model.TotalCount = Convert.ToInt32(ds.Tables[0].Rows[i]["TotalCount"]);
                    nwmd.Add(_model);
                }
                return nwmd;
            }
            catch (Exception ex)
            {
                MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                string methodName = method.Name;
                string className = method.ReflectedType.Name;
                sendErrorMail(ex, methodName, className);
                return nwmd;
            }
        }

        public void SaveFavoriteCompany(string Company, string UserID, string Designation)
        {
            try
            {
                List<SqlParameter> pars = new List<SqlParameter>();
                pars.Add(GetSqlParameter("@Company", SqlDbType.VarChar, Company));
                pars.Add(GetSqlParameter("@UserID", SqlDbType.VarChar, UserID));
                pars.Add(GetSqlParameter("@Designation", SqlDbType.VarChar, Designation));
                SqlHelper.ExecuteNonQuery(CS, SP, "Proc_Save_FavoriteCompany", pars.ToArray());
            }
            catch (Exception ex)
            {
                MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                string methodName = method.Name;
                string className = method.ReflectedType.Name;
                sendErrorMail(ex, methodName, className);
            }

        }

        public void UpdateFavoriteCompany(string Company, string application, string Designation)
        {
            try
            {
                List<SqlParameter> pars = new List<SqlParameter>();
                pars.Add(GetSqlParameter("@Company", SqlDbType.VarChar, Company));
                pars.Add(GetSqlParameter("@Application", SqlDbType.VarChar, application));
                pars.Add(GetSqlParameter("@Designation", SqlDbType.VarChar, Designation));
                SqlHelper.ExecuteNonQuery(CS, SP, "Proc_Update_FavoriteCompany", pars.ToArray());
            }
            catch (Exception ex)
            {
                MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                string methodName = method.Name;
                string className = method.ReflectedType.Name;
                sendErrorMail(ex, methodName, className);
            }

        }

        public List<CompanyModel> GetCandidateRelatedRequirements(int candidateid, string Company, string SecSkill, string Role)
        {
            List<CompanyModel> nwmd = new List<CompanyModel>();
            List<SqlParameter> pars = new List<SqlParameter>();
            try
            {
                pars.Add(GetSqlParameter("@CandidateId", SqlDbType.Int, candidateid));
                pars.Add(GetSqlParameter("@Company", SqlDbType.VarChar, Company));
                pars.Add(GetSqlParameter("@SecSkill", SqlDbType.VarChar, SecSkill));
                pars.Add(GetSqlParameter("@Role", SqlDbType.VarChar, Role));
                DataSet ds = new DataSet();
                string[] tables = new string[] { "companyrequirements" };
                SqlHelper.FillDataset(CS, SP, "Proc_Get_CandidateRelated&AppliedRequirements", ds, tables, pars.ToArray());
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    CompanyModel _model = new CompanyModel();
                    _model.ReqId = Convert.ToInt32(ds.Tables[0].Rows[i]["reqid"]);
                    _model.CompanyId = Convert.ToInt32(ds.Tables[0].Rows[i]["UserId"]);
                    _model.CompanyName = ds.Tables[0].Rows[i]["name"].ToString();
                    _model.JobCode = ds.Tables[0].Rows[i]["jobcode"].ToString();
                    _model.JobTitle = ds.Tables[0].Rows[i]["designation"].ToString();
                    _model.JobTitleId = ds.Tables[0].Rows[i]["DesignationId"].ToString();
                    _model.Location = ds.Tables[0].Rows[i]["Location"].ToString();

                    _model.PrimarySKillName = ds.Tables[0].Rows[i]["SkillName"].ToString();
                    _model.PrimarySkill = Convert.ToInt32(ds.Tables[0].Rows[i]["SkillId"]);
                    _model.SecSkillName1 = ds.Tables[0].Rows[i]["Secskill1"].ToString();
                    _model.SecSKillName2 = ds.Tables[0].Rows[i]["scskill2"].ToString();
                    _model.SecSkillName3 = ds.Tables[0].Rows[i]["secskill3"].ToString();
                    _model.SecSkillName4 = ds.Tables[0].Rows[i]["secskill4"].ToString();
                    _model.SecSkillName5 = ds.Tables[0].Rows[i]["secskill5"].ToString();
                    _model.SecSkillList = _model.SecSkillName1 + ", " + _model.SecSKillName2 + ", " + _model.SecSkillName3 + ", " + _model.SecSkillName4 + ", " + _model.SecSkillName5;
                    _model.MinExp = Convert.ToInt32(ds.Tables[0].Rows[i]["minexp"]);
                    _model.MaxExp = Convert.ToInt32(ds.Tables[0].Rows[i]["maxexp"]);
                    _model.JobDesc = ds.Tables[0].Rows[i]["jobdesc"].ToString();
                    //_model.RowNo = Convert.ToInt32(ds.Tables[0].Rows[i]["RowNo"]);
                    _model.TotalCount = Convert.ToInt32(ds.Tables[0].Rows[i]["TotalCount"]);
                    nwmd.Add(_model);
                }
                return nwmd;
            }
            catch (Exception ex)
            {
                MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                string methodName = method.Name;
                string className = method.ReflectedType.Name;
                sendErrorMail(ex, methodName, className);
                return nwmd;
            }
        }

        public List<CompanyModel> GetCandidateAppliedRequirements(int candidateid, string Company)
        {
            List<CompanyModel> nwmd = new List<CompanyModel>();
            List<SqlParameter> pars = new List<SqlParameter>();
            try
            {
                pars.Add(GetSqlParameter("@CandidateId", SqlDbType.Int, candidateid));
                pars.Add(GetSqlParameter("@Company", SqlDbType.VarChar, Company));
                pars.Add(GetSqlParameter("@SecSkill", SqlDbType.VarChar, null));
                pars.Add(GetSqlParameter("@Role", SqlDbType.VarChar, null));
                DataSet ds = new DataSet();
                string[] tables = new string[] { "companyrequirements" };
                SqlHelper.FillDataset(CS, SP, "Proc_Get_CandidateRelated&AppliedRequirements", ds, tables, pars.ToArray());
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    CompanyModel _model = new CompanyModel();
                    _model.ReqId = Convert.ToInt32(ds.Tables[1].Rows[i]["reqid"]);
                    _model.CompanyName = ds.Tables[1].Rows[i]["name"].ToString();
                    _model.JobCode = ds.Tables[1].Rows[i]["jobcode"].ToString();
                    _model.JobTitle = ds.Tables[1].Rows[i]["designation"].ToString();
                    _model.PrimarySKillName = ds.Tables[1].Rows[i]["SkillName"].ToString();
                    _model.MinExp = Convert.ToInt32(ds.Tables[1].Rows[i]["minexp"]);
                    _model.MaxExp = Convert.ToInt32(ds.Tables[1].Rows[i]["maxexp"]);
                    _model.JobDesc = ds.Tables[1].Rows[i]["jobdesc"].ToString();
                    //_model.RowNo = Convert.ToInt32(ds.Tables[0].Rows[i]["RowNo"]);
                    _model.TotalCount = Convert.ToInt32(ds.Tables[1].Rows[i]["TotalCount"]);
                    nwmd.Add(_model);
                }
                return nwmd;
            }
            catch (Exception ex)
            {
                MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                string methodName = method.Name;
                string className = method.ReflectedType.Name;
                sendErrorMail(ex, methodName, className);
                return nwmd;
            }
        }

        public DataRow IsExistsCandidateApplication(int UserId, int ReqId)
        {
            DataRow userdata = null;
            DataTable usercollection = null;
            try
            {
                List<SqlParameter> pars = new List<SqlParameter>();
                pars.Add(GetSqlParameter("@CanidateId", SqlDbType.Int, UserId));
                pars.Add(GetSqlParameter("@ReqId", SqlDbType.Int, ReqId));
                usercollection = SqlHelper.ExecuteDataset(CS, SP, "Proc_IsExits_CandidateApplication", pars.ToArray()).Tables[0];
                if (usercollection != null && usercollection.Rows != null && usercollection.Rows.Count == 1)
                    userdata = usercollection.Rows[0];
                return userdata;
            }
            catch (Exception ex)
            {
                MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                string methodName = method.Name;
                string className = method.ReflectedType.Name;
                sendErrorMail(ex, methodName, className);
                return userdata;
            }

        }

        public void SaveCandidateApplication(int UserId, int ReqId)
        {
            try
            {
                List<SqlParameter> pars = new List<SqlParameter>();
                pars.Add(GetSqlParameter("@CanidateId", SqlDbType.Int, UserId));
                pars.Add(GetSqlParameter("@ReqId", SqlDbType.Int, ReqId));
                SqlHelper.ExecuteNonQuery(CS, SP, "Proc_Save_CandidateApplication", pars.ToArray());
            }
            catch (Exception ex)
            {
                MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                string methodName = method.Name;
                string className = method.ReflectedType.Name;
                sendErrorMail(ex, methodName, className);
            }
        }

        public List<CompanyModel> GetCompanyJD(int ReqId)
        {
            List<CompanyModel> nwmd = new List<CompanyModel>();
            List<SqlParameter> pars = new List<SqlParameter>();
            try
            {
                pars.Add(GetSqlParameter("@ReqId", SqlDbType.VarChar, ReqId));
                DataSet ds = new DataSet();
                string[] tables = new string[] { "CandidateProfile" };
                SqlHelper.FillDataset(CS, SP, "Proc_Get_CompanyRequirementById", ds, tables, pars.ToArray());
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    CompanyModel _model = new CompanyModel();
                    _model.ReqId = Convert.ToInt32(ds.Tables[0].Rows[i]["ReqId"]);
                    _model.JobCode = ds.Tables[0].Rows[i]["JobCode"].ToString();
                    _model.Location = ds.Tables[0].Rows[i]["Location"].ToString();
                    _model.Designation = ds.Tables[0].Rows[i]["Designation"].ToString();
                    _model.MinExp = Convert.ToInt32(ds.Tables[0].Rows[i]["MinExp"]);
                    _model.MaxExp = Convert.ToInt32(ds.Tables[0].Rows[i]["MaxExp"]);
                    _model.HighestPay = Convert.ToInt32(ds.Tables[0].Rows[i]["HighestPay"]);
                    _model.JobDesc = ds.Tables[0].Rows[i]["JobDesc"].ToString();
                    _model.Remarks = ds.Tables[0].Rows[i]["Remarks"].ToString();
                    _model.JobPostingSDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["JobPostingStartDate"]);
                    _model.JobPostingEDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["JobPostingEndDate"]);
                    _model.DisplayJobStartDate = _model.JobPostingSDate.ToString("dd/MMM/yyyy");
                    _model.DisplayJobEndDate = _model.JobPostingEDate.ToString("dd/MMM/yyyy");
                    _model.PrimarySKillName = ds.Tables[0].Rows[i]["SkillName"].ToString();
                    _model.SecSkillName1 = ds.Tables[0].Rows[i]["SecSkill1Name"].ToString();
                    _model.SecSKillName2 = ds.Tables[0].Rows[i]["SecSkill2Name"].ToString();
                    _model.SecSkillName3 = ds.Tables[0].Rows[i]["SecSkill3Name"].ToString();
                    _model.SecSkillName4 = ds.Tables[0].Rows[i]["SecSkill4Name"].ToString();
                    _model.SecSkillName5 = ds.Tables[0].Rows[i]["SecSkill5Name"].ToString();
                    _model.JobType = ds.Tables[0].Rows[i]["JobType"].ToString();
                    _model.CompanyName = ds.Tables[0].Rows[i]["Name"].ToString();
                    nwmd.Add(_model);
                }
                return nwmd;
            }
            catch (Exception ex)
            {
                MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                string methodName = method.Name;
                string className = method.ReflectedType.Name;
                sendErrorMail(ex, methodName, className);
                return nwmd;
            }

        }

        public List<CompanyModel> GetRecruiterDetailsByCandidateId(int CandidateId)
        {
            List<CompanyModel> nwmd = new List<CompanyModel>();
            try
            {
                List<SqlParameter> pars = new List<SqlParameter>();
                pars.Add(GetSqlParameter("@CandidateId", SqlDbType.Int, CandidateId));
                DataSet dsmain = new DataSet();
                string[] tables = new string[] { "CompanyProfiles" };
                SqlHelper.FillDataset(CS, SP, "Proc_Get_RecruiterDetailsByCandidateId", dsmain, tables, pars.ToArray());
                int count = dsmain.Tables[0].Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    CompanyModel _model = new CompanyModel();
                    _model.RecruiterEmail = dsmain.Tables[0].Rows[i]["RecruiterEmail"].ToString();
                    _model.AssignToEmail = dsmain.Tables[0].Rows[i]["AssignedEmail"].ToString();
                    _model.PMEmail = dsmain.Tables[0].Rows[i]["PMEmail"].ToString();
                    _model.JobCode = dsmain.Tables[0].Rows[i]["JobCode"].ToString();
                    nwmd.Add(_model);
                }
                return nwmd;
            }
            catch (Exception ex)
            {
                MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                string methodName = method.Name;
                string className = method.ReflectedType.Name;
                sendErrorMail(ex, methodName, className);
                return nwmd;
            }

        }

        public List<CandidateModel> GetDailyScheduleById(int timeSlot)
        {
            List<CandidateModel> nwmd = new List<CandidateModel>();
            try
            {
                List<SqlParameter> pars = new List<SqlParameter>();
                pars.Add(GetSqlParameter("@TimeSlot", SqlDbType.Int, timeSlot));
                DataSet dsmain = new DataSet();
                string[] tables = new string[] { "DailySchedules" };
                SqlHelper.FillDataset(CS, SP, "Proc_Get_DailyScheduleById", dsmain, tables, pars.ToArray());
                int count = dsmain.Tables[0].Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    CandidateModel _model = new CandidateModel();
                    _model.TimeSlotText = dsmain.Tables[0].Rows[i]["DailyScheduleTime"].ToString();
                    nwmd.Add(_model);
                }
                return nwmd;
            }
            catch (Exception ex)
            {
                MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                string methodName = method.Name;
                string className = method.ReflectedType.Name;
                sendErrorMail(ex, methodName, className);
                return nwmd;
            }

        }

        public CompanyModel GetCandidateRatingDetails(string ScheduleId)
        {
            CompanyModel _model = new CompanyModel();
            DataSet dsmain = new DataSet();
            try
            {
                List<SqlParameter> parscandidate = new List<SqlParameter>();
                List<SqlParameter> parscandidate1 = new List<SqlParameter>();
                DataSet ds = new DataSet();
                DataSet d1 = new DataSet();
                string[] tab = new string[] { "CandidateProfile" };
                string[] tablenew = new string[] { "ScheduleInterview" };
                parscandidate.Add(GetSqlParameter("@ScheduleId", SqlDbType.VarChar, ScheduleId));
                //parscandidate.Add(GetSqlParameter("@ReqID", SqlDbType.Int, reqid));
                //SqlHelper.FillDataset(CS, SP, "Proc_Get_CandidateRatingDetails", ds, tablenew, parscandidate.ToArray());
                SqlHelper.FillDataset(CS, SP, "Proc_Get_CandidateRatingDetailsByScheduleId", ds, tablenew, parscandidate.ToArray());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    parscandidate1.Add(GetSqlParameter("@CandidateId", SqlDbType.Int, Convert.ToInt32(ds.Tables[0].Rows[0]["UserId"])));
                    SqlHelper.FillDataset(CS, SP, "Proc_Get_CandidateProfile", d1, tab, parscandidate1.ToArray());
                    _model.ReqId = Convert.ToInt32(ds.Tables[0].Rows[0]["ReqId"]);
                    _model.JobCode = ds.Tables[0].Rows[0]["JobCode"].ToString();
                    _model.ScheduleId = ds.Tables[0].Rows[0]["ScheduleId"].ToString();
                    _model.CandidateName = ds.Tables[0].Rows[0]["name"].ToString();
                    _model.UniqueId = ds.Tables[0].Rows[0]["UniqueId"].ToString();
                    _model.PrimarySKillName = ds.Tables[0].Rows[0]["SkillName"].ToString();
                    _model.PrimarySkill = Convert.ToInt32(ds.Tables[0].Rows[0]["SkillId"].ToString());
                    _model.Experience = ds.Tables[0].Rows[0]["Experience"].ToString();
                    _model.interviewerid = Convert.ToInt32(ds.Tables[0].Rows[0]["interviewerid"]);
                    _model.Interviewer = ds.Tables[0].Rows[0]["interviewer"].ToString();
                    _model.SecSkillName1 = ds.Tables[0].Rows[0]["SecondarySkill1"].ToString();
                    _model.SecSKillName2 = ds.Tables[0].Rows[0]["SecondarySkill2"].ToString();
                    _model.SecSkillName3 = ds.Tables[0].Rows[0]["SecondarySkill3"].ToString();
                    string SecSkillName4 = ds.Tables[0].Rows[0]["SecondarySkill4"].ToString();
                    _model.SecSkillName4 = ds.Tables[0].Rows[0]["SecondarySkill4"].ToString();
                    if (SecSkillName4 == "" || SecSkillName4 == null || SecSkillName4 == "0")
                    {
                        _model.SecSkillName4 = "--";
                    }
                    string SecSkillName5 = ds.Tables[0].Rows[0]["SecondarySkill5"].ToString();
                    _model.SecSkillName5 = ds.Tables[0].Rows[0]["SecondarySkill5"].ToString();
                    if (SecSkillName5 == "" || SecSkillName5 == null || SecSkillName5 == "0")
                    {
                        _model.SecSkillName5 = "--";
                    }
                    _model.MobileNumber = ds.Tables[0].Rows[0]["Phone"].ToString();
                    _model.Resume = ds.Tables[0].Rows[0]["Resume"].ToString();
                    _model.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                    _model.SecSkill1Rating = ds.Tables[0].Rows[0]["SecondarySkill1Rating"].ToString();
                    _model.SecSkill2Rating = ds.Tables[0].Rows[0]["SecondarySkill2Rating"].ToString();
                    _model.SecSkill3Rating = ds.Tables[0].Rows[0]["SecondarySkill3Rating"].ToString();
                    _model.SecSkill4Rating = ds.Tables[0].Rows[0]["SecondarySkill4Rating"].ToString();
                    _model.SecSkill5Rating = ds.Tables[0].Rows[0]["SecondarySkill5Rating"].ToString();

                    if ((_model.SecSkill4Rating == null || _model.SecSkill4Rating == "" || _model.SecSkill4Rating == "0") &&
                        (_model.SecSkill5Rating == null || _model.SecSkill5Rating == "" || _model.SecSkill5Rating == "0"))
                    {
                        double skill1 = Convert.ToDouble(_model.SecSkill1Rating);
                        double skill2 = Convert.ToDouble(_model.SecSkill2Rating);
                        double skill3 = Convert.ToDouble(_model.SecSkill3Rating);
                        double rating = (skill1 + skill2 + skill3) / 3;
                        rating = Math.Round(rating, 2);
                        _model.OveralRating = rating.ToString();
                    }
                    else if (_model.SecSkill4Rating == null || _model.SecSkill4Rating == "" || _model.SecSkill4Rating == "0")
                    {
                        double skill1 = Convert.ToDouble(_model.SecSkill1Rating);
                        double skill2 = Convert.ToDouble(_model.SecSkill2Rating);
                        double skill3 = Convert.ToDouble(_model.SecSkill3Rating);
                        double skill5 = Convert.ToDouble(_model.SecSkill5Rating);
                        double rating = (skill1 + skill2 + skill3 + skill5) / 4;
                        rating = Math.Round(rating, 2);
                        _model.OveralRating = rating.ToString();
                    }
                    else if (_model.SecSkill5Rating == null || _model.SecSkill5Rating == "" || _model.SecSkill5Rating == "0")
                    {
                        double skill1 = Convert.ToDouble(_model.SecSkill1Rating);
                        double skill2 = Convert.ToDouble(_model.SecSkill2Rating);
                        double skill3 = Convert.ToDouble(_model.SecSkill3Rating);
                        double skill4 = Convert.ToDouble(_model.SecSkill4Rating);
                        double rating = (skill1 + skill2 + skill3 + skill4) / 4;
                        rating = Math.Round(rating, 2);
                        _model.OveralRating = rating.ToString();
                    }
                    else
                    {
                        double skill1 = Convert.ToDouble(_model.SecSkill1Rating);
                        double skill2 = Convert.ToDouble(_model.SecSkill2Rating);
                        double skill3 = Convert.ToDouble(_model.SecSkill3Rating);
                        double skill4 = Convert.ToDouble(_model.SecSkill4Rating);
                        double skill5 = Convert.ToDouble(_model.SecSkill5Rating);
                        double rating = (skill1 + skill2 + skill3 + skill4 + skill5) / 5;
                        rating = Math.Round(rating, 2);
                        _model.OveralRating = rating.ToString();
                    }
                    _model.EnglishCommunication = ds.Tables[0].Rows[0]["EnglishCommunication"].ToString();
                    _model.Attitude = ds.Tables[0].Rows[0]["Attitude"].ToString();
                    _model.InterpersonalSkillCommunication = ds.Tables[0].Rows[0]["InterpersonalSkillCommunication"].ToString();

                    double engcomm = Convert.ToDouble(_model.EnglishCommunication);
                    double attitude = Convert.ToDouble(_model.Attitude);
                    double intercomm = Convert.ToDouble(_model.InterpersonalSkillCommunication);
                    double softskillrating = (engcomm + attitude + intercomm) / 3;
                    softskillrating = Math.Round(softskillrating, 2);
                    _model.SoftSkillRating = softskillrating.ToString();
                    _model.AudioFile = ds.Tables[0].Rows[0]["AudioFile"].ToString();
                    _model.VideoFile = ds.Tables[0].Rows[0]["VideoFile"].ToString();
                    _model.CurrentPay = ds.Tables[0].Rows[0]["CurrentPay"].ToString();
                    _model.ExpectedPay = ds.Tables[0].Rows[0]["ExpectedPay"].ToString();
                    _model.Location = ds.Tables[0].Rows[0]["CurrentLocation"].ToString();

                    string SecondarySkill4Rating = ds.Tables[0].Rows[0]["SecondarySkill4Rating"].ToString();
                    _model.SecSkill4Rating = ds.Tables[0].Rows[0]["SecondarySkill4Rating"].ToString();
                    if (SecondarySkill4Rating == "" || SecondarySkill4Rating == null || SecondarySkill4Rating == "0")
                    {
                        _model.SecSkill4Rating = "--";
                    }
                    string SecondarySkill5Rating = ds.Tables[0].Rows[0]["SecondarySkill5Rating"].ToString();
                    _model.SecSkill5Rating = ds.Tables[0].Rows[0]["SecondarySkill5Rating"].ToString();
                    if (SecondarySkill5Rating == "" || SecondarySkill5Rating == null || SecondarySkill5Rating == "0")
                    {
                        _model.SecSkill5Rating = "--";
                    }
                    _model.skill1comments = ds.Tables[0].Rows[0]["SecSkill1Remarks"].ToString();
                    _model.skill2comments = ds.Tables[0].Rows[0]["SecSkill2Remarks"].ToString();
                    _model.skill3comments = ds.Tables[0].Rows[0]["SecSkill3Remarks"].ToString();
                    _model.skill4comments = ds.Tables[0].Rows[0]["SecSkill4Remarks"].ToString();
                    _model.skill5comments = ds.Tables[0].Rows[0]["SecSkill5Remarks"].ToString();
                    _model.Comments = ds.Tables[0].Rows[0]["InterviewerRemarks"].ToString();
                    _model.EngComments = ds.Tables[0].Rows[0]["EnglishCommunicationRemarks"].ToString();
                    _model.Attitudecomments = ds.Tables[0].Rows[0]["AttitudeRemarks"].ToString();
                    _model.InterComments = ds.Tables[0].Rows[0]["InterpersonalSkillCommunicationRemarks"].ToString();
                    //_model.CompanySchedule = ds.Tables[0].Rows[0]["CompanySchedule"].ToString();
                    //_model.RowNo = Convert.ToInt32(ds.Tables[0].Rows[0]["RowNo"]);
                    //_model.TotalCount = Convert.ToInt32(ds.Tables[0].Rows[0]["TotalCount"]);
                    _model.NoticePeriod = d1.Tables[0].Rows[0]["NoticePeriod"].ToString();
                    _model.Logo = ds.Tables[0].Rows[0]["Logo"].ToString();
                    if (string.IsNullOrEmpty(_model.Logo))
                    {
                        _model.Logo = "logo.png";
                    }

                    bool s = Convert.ToBoolean(d1.Tables[0].Rows[0]["GapInEducation"]);
                    if (s == false)
                    {
                        _model.GapInEducation = "No";
                    }
                    else
                    {
                        _model.GapInEducation = "yes";
                    }
                    bool b = Convert.ToBoolean(d1.Tables[0].Rows[0]["GapInExperience"]);
                    if (b == false)
                    {

                        _model.GapInExperience = "No";
                    }
                    else
                    {
                        _model.GapInExperience = "Yes";
                    }
                }
                return _model;
            }
            catch (Exception ex)
            {
                MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                string methodName = method.Name + " / Input (ScheduleId) = " + ScheduleId;
                string className = method.ReflectedType.Name;
                string inputDetails = ScheduleId;
                sendErrorMail(ex, methodName, className);
                return _model;
            }
        }

        public void UpdatePayment(string Orderid, string trackingid, string bankreferenceid, string orderstatus, string failuremessage, string paymentmode, string cardname, string statuscode, string statusmessage)
        {
            try
            {
                List<SqlParameter> pars = new List<SqlParameter>();
                pars.Add(GetSqlParameter("@Orderid", SqlDbType.VarChar, Orderid));
                pars.Add(GetSqlParameter("@trackingid", SqlDbType.VarChar, trackingid));
                pars.Add(GetSqlParameter("@bankrefid", SqlDbType.VarChar, bankreferenceid));
                pars.Add(GetSqlParameter("@orderstatus", SqlDbType.VarChar, orderstatus));
                pars.Add(GetSqlParameter("@failuremessage", SqlDbType.VarChar, failuremessage));
                pars.Add(GetSqlParameter("@paymentmode", SqlDbType.VarChar, paymentmode));
                pars.Add(GetSqlParameter("@cardname", SqlDbType.VarChar, cardname));
                pars.Add(GetSqlParameter("@statuscode", SqlDbType.VarChar, statuscode));
                pars.Add(GetSqlParameter("@statusmessage", SqlDbType.VarChar, statusmessage));
                SqlHelper.ExecuteNonQuery(CS, SP, "Proc_Update_UpdatePayment", pars.ToArray());
            }
            catch (Exception ex)
            {
                MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                string methodName = method.Name;
                string className = method.ReflectedType.Name;
                sendErrorMail(ex, methodName, className);
            }
        }

        public List<CandidateModel> ScheduleInterviewAfterPayment(string OrderId)
        {
            List<CandidateModel> nwmd = new List<CandidateModel>();
            List<SqlParameter> pars = new List<SqlParameter>();
            try
            {
                pars.Add(GetSqlParameter("@Orderid", SqlDbType.VarChar, OrderId));
                DataSet ds = new DataSet();
                string[] tables = new string[] { "paymentinfo" };
                //SqlHelper.FillDataset(CS, SP, "Proc_Get_FavoriteCompany", ds, tables, pars.ToArray());
                SqlHelper.FillDataset(CS, SP, "Proc_Update_ScheduleInterviewAfterPayment", ds, tables, pars.ToArray());
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    CandidateModel _model = new CandidateModel();
                    _model.Email = ds.Tables[0].Rows[i]["candidateemail"].ToString();
                    _model.date = Convert.ToDateTime(ds.Tables[0].Rows[i]["scheduleddate"]);
                    _model.InterviewerEmail = ds.Tables[0].Rows[i]["intervieweremail"].ToString();
                    _model.TimeSlotText = ds.Tables[0].Rows[i]["dailyscheduletime"].ToString();
                    _model.InterviewerName = ds.Tables[0].Rows[i]["interviewername"].ToString();
                    _model.CandidateName = ds.Tables[0].Rows[i]["candidatename"].ToString();
                    _model.candidateid = Convert.ToInt32(ds.Tables[0].Rows[i]["candidateid"]);
                    _model.CandidateUniqueId = ds.Tables[0].Rows[i]["UniqueId"].ToString();
                    _model.InterviewType = ds.Tables[0].Rows[i]["InterviewType"].ToString();
                    nwmd.Add(_model);
                }
                return nwmd;
            }
            catch (Exception ex)
            {
                MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                string methodName = method.Name;
                string className = method.ReflectedType.Name;
                sendErrorMail(ex, methodName, className);
                return nwmd;
            }

        }

        public void UpdateInterviewerAfterPayment(string OrderId)
        {
            try
            {
                List<SqlParameter> pars = new List<SqlParameter>();
                pars.Add(GetSqlParameter("@Orderid", SqlDbType.VarChar, OrderId));
                SqlHelper.ExecuteNonQuery(CS, SP, "Proc_Update_UpdateInterviewerAfterPayment", pars.ToArray());
            }
            catch (Exception ex)
            {
                MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                string methodName = method.Name;
                string className = method.ReflectedType.Name;
                sendErrorMail(ex, methodName, className);
            }
        }
    }
}