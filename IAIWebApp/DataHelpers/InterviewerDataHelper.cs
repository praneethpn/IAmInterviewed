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
    public class InterviewerDataHelper : BaseDataHelper
    {
        public List<InterviewerModel> GetTodaysInterviews(int UserId)
        {
            List<InterviewerModel> nwmd = new List<InterviewerModel>();
            List<SqlParameter> pars = new List<SqlParameter>();
            try
            {
                pars.Add(GetSqlParameter("@UserId", SqlDbType.Int, UserId));
                DataSet ds = new DataSet();
                string[] tables = new string[] { "ScheduleInterview" };
                SqlHelper.FillDataset(CS, SP, "Proc_Get_TodaysInterviews", ds, tables, pars.ToArray());
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    InterviewerModel _model = new InterviewerModel();
                    _model.ScheduleId = Convert.ToInt32(ds.Tables[0].Rows[i]["ScheduleId"]);
                    _model.UserId = Convert.ToInt32(ds.Tables[0].Rows[i]["UserId"]);
                    _model.CandidateName = ds.Tables[0].Rows[i]["name"].ToString();
                    _model.InterviewDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["ScheduledDate"]);
                    _model.DisplayDate = _model.InterviewDate.ToString("dd/MMM/yyyy");
                    _model.TimeSlot = ds.Tables[0].Rows[i]["DailyScheduleTime"].ToString();
                    _model.PrimarySkill = ds.Tables[0].Rows[i]["SkillName"].ToString();
                    _model.SecondarySkill1 = ds.Tables[0].Rows[i]["SecondarySkill1"].ToString();
                    _model.SecondarySkill2 = ds.Tables[0].Rows[i]["SecondarySkill2"].ToString();
                    _model.SecondarySkill3 = ds.Tables[0].Rows[i]["SecondarySkill3"].ToString();
                    _model.SecondarySkill4 = ds.Tables[0].Rows[i]["SecondarySkill4"].ToString();
                    _model.SecondarySkill5 = ds.Tables[0].Rows[i]["SecondarySkill5"].ToString();
                    _model.MobileNumber = ds.Tables[0].Rows[i]["Phone"].ToString();
                    _model.Resume = ds.Tables[0].Rows[i]["Resume"].ToString();
                    _model.Email = ds.Tables[0].Rows[i]["Email"].ToString();
                    _model.Experience = ds.Tables[0].Rows[i]["experience"].ToString();
                    _model.IsConfirmed = ds.Tables[0].Rows[i]["IsConfirmedByInterviewer"].ToString();
                    _model.KeyResponsibilities = ds.Tables[0].Rows[i]["KeyResponsibilities"].ToString();
                    _model.InterviewType = ds.Tables[0].Rows[i]["InterviewType"].ToString();
                    _model.CompanySchedule = ds.Tables[0].Rows[i]["CompanySchedule"].ToString();
                    _model.CandidateUniqueId = ds.Tables[0].Rows[i]["UniqueId"].ToString();
                    if (_model.IsConfirmed == "True")
                    {
                        _model.IsConfirmed = "Yes";
                    }
                    else if (_model.IsConfirmed == "False")
                    {
                        _model.IsConfirmed = "No";
                    }
                    else
                    {
                        _model.IsConfirmed = "No Action";
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

        public List<InterviewerModel> GetInterviewsToBeConfirmed(int UserId)
        {
            List<InterviewerModel> nwmd = new List<InterviewerModel>();
            List<SqlParameter> pars = new List<SqlParameter>();
            try
            {
                pars.Add(GetSqlParameter("@UserId", SqlDbType.Int, UserId));
                DataSet ds = new DataSet();
                string[] tables = new string[] { "ScheduleInterview" };
                SqlHelper.FillDataset(CS, SP, "Proc_Get_Interviewstobeconfirmed", ds, tables, pars.ToArray());
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    InterviewerModel _model = new InterviewerModel();
                    _model.ScheduleId = Convert.ToInt32(ds.Tables[0].Rows[i]["ScheduleId"]);
                    _model.UserId = Convert.ToInt32(ds.Tables[0].Rows[i]["UserId"]);
                    _model.CandidateUniqueId = ds.Tables[0].Rows[i]["UniqueId"].ToString();
                    _model.CandidateName = ds.Tables[0].Rows[i]["name"].ToString();
                    _model.InterviewDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["ScheduledDate"]);
                    _model.DisplayDate = _model.InterviewDate.ToString("dd/MMM/yyyy");
                    _model.TimeSlot = ds.Tables[0].Rows[i]["DailyScheduleTime"].ToString();
                    _model.PrimarySkill = ds.Tables[0].Rows[i]["SkillName"].ToString();
                    _model.SecondarySkill1 = ds.Tables[0].Rows[i]["SecondarySkill1"].ToString();
                    _model.SecondarySkill2 = ds.Tables[0].Rows[i]["SecondarySkill2"].ToString();
                    _model.SecondarySkill3 = ds.Tables[0].Rows[i]["SecondarySkill3"].ToString();
                    _model.SecondarySkill4 = ds.Tables[0].Rows[i]["SecondarySkill4"].ToString();
                    _model.SecondarySkill5 = ds.Tables[0].Rows[i]["SecondarySkill5"].ToString();
                    _model.MobileNumber = ds.Tables[0].Rows[i]["Phone"].ToString();
                    _model.Resume = ds.Tables[0].Rows[i]["Resume"].ToString();
                    _model.Email = ds.Tables[0].Rows[i]["Email"].ToString();
                    _model.Experience = ds.Tables[0].Rows[i]["experience"].ToString();
                    _model.IsConfirmed = ds.Tables[0].Rows[i]["IsConfirmedByInterviewer"].ToString();
                    _model.KeyResponsibilities = ds.Tables[0].Rows[i]["KeyResponsibilities"].ToString();
                    _model.InterviewerRemarks = ds.Tables[0].Rows[i]["InterviewerRemarks"].ToString();
                    _model.InterviewType = ds.Tables[0].Rows[i]["InterviewType"].ToString();
                    _model.CompanySchedule = ds.Tables[0].Rows[i]["CompanySchedule"].ToString();
                    _model.CandidateUniqueId = ds.Tables[0].Rows[i]["UniqueId"].ToString();
                    if (_model.IsConfirmed == "True")
                    {
                        _model.IsConfirmed = "Yes";
                    }
                    else if (_model.IsConfirmed == "False")
                    {
                        _model.IsConfirmed = "No";
                    }
                    else
                    {
                        _model.IsConfirmed = "No Action";
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

        public List<InterviewerModel> GetInterviewsToBeRated(int UserId)
        {
            List<InterviewerModel> nwmd = new List<InterviewerModel>();
            List<SqlParameter> pars = new List<SqlParameter>();
            try
            {
                pars.Add(GetSqlParameter("@UserId", SqlDbType.Int, UserId));
                DataSet ds = new DataSet();
                string[] tables = new string[] { "ScheduleInterview" };
                SqlHelper.FillDataset(CS, SP, "Proc_Get_InterviewstobeRated", ds, tables, pars.ToArray());
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    InterviewerModel _model = new InterviewerModel();
                    _model.ScheduleId = Convert.ToInt32(ds.Tables[0].Rows[i]["ScheduleId"]);
                    _model.UserId = Convert.ToInt32(ds.Tables[0].Rows[i]["UserId"]);
                    _model.CandidateName = ds.Tables[0].Rows[i]["name"].ToString();
                    _model.InterviewDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["ScheduledDate"]);
                    _model.DisplayDate = _model.InterviewDate.ToString("dd/MMM/yyyy");
                    _model.TimeSlot = ds.Tables[0].Rows[i]["DailyScheduleTime"].ToString();
                    _model.PrimarySkill = ds.Tables[0].Rows[i]["SkillName"].ToString();
                    _model.SecondarySkill1 = ds.Tables[0].Rows[i]["SecondarySkill1"].ToString();
                    _model.SecondarySkill2 = ds.Tables[0].Rows[i]["SecondarySkill2"].ToString();
                    _model.SecondarySkill3 = ds.Tables[0].Rows[i]["SecondarySkill3"].ToString();
                    _model.SecondarySkill4 = ds.Tables[0].Rows[i]["SecondarySkill4"].ToString();
                    _model.SecondarySkill5 = ds.Tables[0].Rows[i]["SecondarySkill5"].ToString();
                    _model.MobileNumber = ds.Tables[0].Rows[i]["Phone"].ToString();
                    _model.Resume = ds.Tables[0].Rows[i]["Resume"].ToString();
                    _model.Email = ds.Tables[0].Rows[i]["Email"].ToString();
                    _model.InterviewType = ds.Tables[0].Rows[i]["InterviewType"].ToString();
                    _model.CompanySchedule = ds.Tables[0].Rows[i]["CompanySchedule"].ToString();
                    _model.CandidateUniqueId = ds.Tables[0].Rows[i]["UniqueId"].ToString();
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

        public List<InterviewerModel> GetInterviewsCompleted(int UserId)
        {
            List<InterviewerModel> nwmd = new List<InterviewerModel>();
            List<SqlParameter> pars = new List<SqlParameter>();
            try
            {
                pars.Add(GetSqlParameter("@UserId", SqlDbType.Int, UserId));
                DataSet ds = new DataSet();
                string[] tables = new string[] { "ScheduleInterview" };
                SqlHelper.FillDataset(CS, SP, "Proc_Get_InterviewsCompleted", ds, tables, pars.ToArray());
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    InterviewerModel _model = new InterviewerModel();
                    _model.ScheduleId = Convert.ToInt32(ds.Tables[0].Rows[i]["ScheduleId"]);
                    _model.UserId = Convert.ToInt32(ds.Tables[0].Rows[i]["UserId"]);
                    _model.CandidateName = ds.Tables[0].Rows[i]["name"].ToString();
                    _model.InterviewDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["ScheduledDate"]);
                    _model.DisplayDate = _model.InterviewDate.ToString("dd/MMM/yyyy");
                    _model.TimeSlot = ds.Tables[0].Rows[i]["DailyScheduleTime"].ToString();
                    _model.PrimarySkill = ds.Tables[0].Rows[i]["SkillName"].ToString();
                    _model.SecondarySkill1 = ds.Tables[0].Rows[i]["SecondarySkill1"].ToString();
                    _model.SecondarySkill2 = ds.Tables[0].Rows[i]["SecondarySkill2"].ToString();
                    _model.SecondarySkill3 = ds.Tables[0].Rows[i]["SecondarySkill3"].ToString();
                    _model.SecondarySkill4 = ds.Tables[0].Rows[i]["SecondarySkill4"].ToString();
                    _model.SecondarySkill5 = ds.Tables[0].Rows[i]["SecondarySkill5"].ToString();
                    _model.MobileNumber = ds.Tables[0].Rows[i]["Phone"].ToString();
                    _model.Resume = ds.Tables[0].Rows[i]["Resume"].ToString();
                    _model.Email = ds.Tables[0].Rows[i]["Email"].ToString();
                    _model.SecondarySkill1Rating = ds.Tables[0].Rows[i]["SecondarySkill1Rating"].ToString();
                    _model.SecondarySkill2Rating = ds.Tables[0].Rows[i]["SecondarySkill2Rating"].ToString();
                    _model.SecondarySkill3Rating = ds.Tables[0].Rows[i]["SecondarySkill3Rating"].ToString();
                    _model.SecondarySkill4Rating = ds.Tables[0].Rows[i]["SecondarySkill4Rating"].ToString();
                    _model.SecondarySkill5Rating = ds.Tables[0].Rows[i]["SecondarySkill5Rating"].ToString();
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
                    _model.EnglishCommunication = ds.Tables[0].Rows[i]["EnglishCommunication"].ToString();
                    _model.Attitude = ds.Tables[0].Rows[i]["Attitude"].ToString();
                    _model.InterpersonalSkillCommunication = ds.Tables[0].Rows[i]["InterpersonalSkillCommunication"].ToString();
                    _model.SecondarySkill1Remarks = ds.Tables[0].Rows[i]["SecSkill1Remarks"].ToString();
                    _model.SecondarySkill2Remarks = ds.Tables[0].Rows[i]["SecSkill2Remarks"].ToString();
                    _model.SecondarySkill3Remarks = ds.Tables[0].Rows[i]["SecSkill3Remarks"].ToString();
                    _model.SecondarySkill4Remarks = ds.Tables[0].Rows[i]["SecSkill4Remarks"].ToString();
                    _model.SecondarySkill5Remarks = ds.Tables[0].Rows[i]["SecSkill5Remarks"].ToString();
                    _model.AttitudeRemarks = ds.Tables[0].Rows[i]["AttitudeRemarks"].ToString();
                    _model.EnglishCommunicationRemarks = ds.Tables[0].Rows[i]["EnglishCommunicationRemarks"].ToString();
                    _model.InterpersonalSkillCommunicationRemarks = ds.Tables[0].Rows[i]["InterpersonalSkillCommunicationRemarks"].ToString();
                    _model.InterviewerRemarks = ds.Tables[0].Rows[i]["InterviewerRemarks"].ToString();
                    _model.AudioFile = ds.Tables[0].Rows[i]["AudioFile"].ToString();
                    _model.VideoFile = ds.Tables[0].Rows[i]["VideoFile"].ToString();
                    _model.RatingAccepted = ds.Tables[0].Rows[i]["IsRatingAcceptedbyUser"].ToString();
                    _model.InterviewType = ds.Tables[0].Rows[i]["InterviewType"].ToString();
                    _model.CompanySchedule = ds.Tables[0].Rows[i]["CompanySchedule"].ToString();
                    _model.CandidateUniqueId = ds.Tables[0].Rows[i]["UniqueId"].ToString();
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

        public List<InterviewerModel> GetOthersInterviews(int UserId)
        {
            List<InterviewerModel> nwmd = new List<InterviewerModel>();
            List<SqlParameter> pars = new List<SqlParameter>();
            try
            {
                pars.Add(GetSqlParameter("@UserId", SqlDbType.Int, UserId));
                DataSet ds = new DataSet();
                string[] tables = new string[] { "ScheduleInterview" };
                SqlHelper.FillDataset(CS, SP, "Proc_Get_InterviewswithOtherstatus", ds, tables, pars.ToArray());
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    InterviewerModel _model = new InterviewerModel();
                    _model.ScheduleId = Convert.ToInt32(ds.Tables[0].Rows[i]["ScheduleId"]);
                    _model.UserId = Convert.ToInt32(ds.Tables[0].Rows[i]["UserId"]);
                    _model.CandidateName = ds.Tables[0].Rows[i]["name"].ToString();
                    _model.InterviewDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["ScheduledDate"]);
                    _model.DisplayDate = _model.InterviewDate.ToString("dd/MMM/yyyy");
                    _model.TimeSlot = ds.Tables[0].Rows[i]["DailyScheduleTime"].ToString();
                    _model.PrimarySkill = ds.Tables[0].Rows[i]["SkillName"].ToString();
                    _model.SecondarySkill1 = ds.Tables[0].Rows[i]["SecondarySkill1"].ToString();
                    _model.SecondarySkill2 = ds.Tables[0].Rows[i]["SecondarySkill2"].ToString();
                    _model.SecondarySkill3 = ds.Tables[0].Rows[i]["SecondarySkill3"].ToString();
                    _model.SecondarySkill4 = ds.Tables[0].Rows[i]["SecondarySkill4"].ToString();
                    _model.SecondarySkill5 = ds.Tables[0].Rows[i]["SecondarySkill5"].ToString();
                    _model.MobileNumber = ds.Tables[0].Rows[i]["Phone"].ToString();
                    _model.Resume = ds.Tables[0].Rows[i]["Resume"].ToString();
                    _model.Email = ds.Tables[0].Rows[i]["Email"].ToString();
                    _model.Experience = ds.Tables[0].Rows[i]["experience"].ToString();
                    _model.Status = ds.Tables[0].Rows[i]["Status"].ToString();
                    _model.IsConfirmed = ds.Tables[0].Rows[i]["IsConfirmedByInterviewer"].ToString();
                    _model.InterviewType = ds.Tables[0].Rows[i]["InterviewType"].ToString();
                    _model.CompanySchedule = ds.Tables[0].Rows[i]["CompanySchedule"].ToString();
                    _model.CandidateUniqueId = ds.Tables[0].Rows[i]["UniqueId"].ToString();
                    if (_model.IsConfirmed == "True")
                    {
                        _model.IsConfirmed = "Yes";
                    }
                    else if (_model.IsConfirmed == "False")
                    {
                        _model.IsConfirmed = "No";
                    }
                    else
                    {
                        _model.IsConfirmed = "No Action";
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

        public void UpdateInterviewStatus(int ScheduleId, string Status, string Remarks)
        {
            try
            {
                List<SqlParameter> pars = new List<SqlParameter>();
                pars.Add(GetSqlParameter("@ScheduleId", SqlDbType.Int, ScheduleId));
                pars.Add(GetSqlParameter("@Status", SqlDbType.VarChar, Status));
                pars.Add(GetSqlParameter("@Remarks", SqlDbType.VarChar, Remarks));
                SqlHelper.ExecuteNonQuery(CS, SP, "Proc_Update_IterviewStatus", pars.ToArray());
            }
            catch (Exception ex)
            {
                MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                string methodName = method.Name;
                string className = method.ReflectedType.Name;
                sendErrorMail(ex, methodName, className);
            }
        }

        public void ConfirmInterview(int ScheduleId, int UserId)
        {
            try
            {
                List<SqlParameter> pars = new List<SqlParameter>();
                pars.Add(GetSqlParameter("@ScheduleId", SqlDbType.Int, ScheduleId));
                pars.Add(GetSqlParameter("@UserId", SqlDbType.Int, UserId));
                SqlHelper.ExecuteNonQuery(CS, SP, "Proc_Update_ConfirmInterview", pars.ToArray());
            }
            catch (Exception ex)
            {
                MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                string methodName = method.Name;
                string className = method.ReflectedType.Name;
                sendErrorMail(ex, methodName, className);
            }
        }

        public void UpdateRating(InterviewerRatingModel _model)
        {
            try
            {
                List<SqlParameter> pars = new List<SqlParameter>();
                pars.Add(GetSqlParameter("@ScheduleId", SqlDbType.Int, _model.ScheduleId));
                pars.Add(GetSqlParameter("@SecondarySkill1Rating", SqlDbType.VarChar, _model.SecondarySkill1Rating));
                pars.Add(GetSqlParameter("@SecondarySkill2Rating", SqlDbType.VarChar, _model.SecondarySkill2Rating));
                pars.Add(GetSqlParameter("@SecondarySkill3Rating", SqlDbType.VarChar, _model.SecondarySkill3Rating));
                pars.Add(GetSqlParameter("@SecondarySkill4Rating", SqlDbType.VarChar, _model.SecondarySkill4Rating));
                pars.Add(GetSqlParameter("@SecondarySkill5Rating", SqlDbType.VarChar, _model.SecondarySkill5Rating));
                pars.Add(GetSqlParameter("@TotalRating", SqlDbType.VarChar, _model.TotalRating));
                pars.Add(GetSqlParameter("@EnglishCommunication", SqlDbType.VarChar, _model.EnglishCommunication));
                pars.Add(GetSqlParameter("@Attitude", SqlDbType.VarChar, _model.Attitude));
                pars.Add(GetSqlParameter("@InterpersonalSkillCommunication", SqlDbType.VarChar, _model.InterpersonalSkillCommunication));
                pars.Add(GetSqlParameter("@InterviewerRemarks", SqlDbType.VarChar, _model.InterviewerRemarks));
                pars.Add(GetSqlParameter("@AudioFile", SqlDbType.VarChar, _model.AudioFile));
                pars.Add(GetSqlParameter("@VideoFile", SqlDbType.VarChar, _model.VideoFile));
                pars.Add(GetSqlParameter("@SecondarySkill1Remarks", SqlDbType.VarChar, _model.SecondarySkill1Remarks));
                pars.Add(GetSqlParameter("@SecondarySkill2Remarks", SqlDbType.VarChar, _model.SecondarySkill2Remarks));
                pars.Add(GetSqlParameter("@SecondarySkill3Remarks", SqlDbType.VarChar, _model.SecondarySkill3Remarks));
                pars.Add(GetSqlParameter("@SecondarySkill4Remarks", SqlDbType.VarChar, _model.SecondarySkill4Remarks));
                pars.Add(GetSqlParameter("@SecondarySkill5Remarks", SqlDbType.VarChar, _model.SecondarySkill5Remarks));
                pars.Add(GetSqlParameter("@EnglishCommunicationRemarks", SqlDbType.VarChar, _model.EnglishCommunicationRemarks));
                pars.Add(GetSqlParameter("@AttitudeRemarks", SqlDbType.VarChar, _model.AttitudeRemarks));
                pars.Add(GetSqlParameter("@InterpersonalSkillCommunicationRemarks", SqlDbType.VarChar, _model.InterpersonalSkillCommunicationRemarks));
                SqlHelper.ExecuteNonQuery(CS, SP, "Proc_Update_Rating", pars.ToArray());
            }
            catch (Exception ex)
            {
                MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                string methodName = method.Name;
                string className = method.ReflectedType.Name;
                sendErrorMail(ex, methodName, className);
            }
        }

        public List<InterviewerModel> GetDailyScheduleTimeAll(int UserId, DateTime ScheduleDate)
        {
            List<InterviewerModel> nwmd = new List<InterviewerModel>();
            List<SqlParameter> pars = new List<SqlParameter>();
            try
            {
                pars.Add(GetSqlParameter("@UserId", SqlDbType.Int, UserId));
                pars.Add(GetSqlParameter("@date", SqlDbType.DateTime, ScheduleDate));
                DataSet ds = new DataSet();
                string[] tables = new string[] { "DailySchedule" };
                SqlHelper.FillDataset(CS, SP, "Proc_Fill_DailySchedule", ds, tables, pars.ToArray());
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    InterviewerModel _model = new InterviewerModel();
                    _model.DailyScheduleId = Convert.ToInt32(ds.Tables[0].Rows[i]["DailyScheduleId"]);
                    _model.DailyScheduleTime = ds.Tables[0].Rows[i]["DailyScheduleTime"].ToString();
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

        public List<InterviewerModel> GetDailyScheduleTimePublished(int UserId, DateTime ScheduleDate)
        {
            List<InterviewerModel> nwmd = new List<InterviewerModel>();
            List<SqlParameter> pars = new List<SqlParameter>();
            try
            {
                pars.Add(GetSqlParameter("@UserId", SqlDbType.Int, UserId));
                pars.Add(GetSqlParameter("@date", SqlDbType.DateTime, ScheduleDate));
                DataSet ds = new DataSet();
                string[] tables = new string[] { "DailySchedule" };
                SqlHelper.FillDataset(CS, SP, "Proc_Fill_DailySchedule", ds, tables, pars.ToArray());
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    InterviewerModel _model = new InterviewerModel();
                    _model.DailyScheduleId = Convert.ToInt32(ds.Tables[1].Rows[i]["DailyScheduleId"]);
                    string ScheduleTime = ds.Tables[1].Rows[i]["DailyScheduleTime"].ToString();
                    string blocked = ds.Tables[1].Rows[i]["IsBlocked"].ToString();
                    if (blocked == "True")
                    {
                        ScheduleTime = ScheduleTime + " *";
                    }
                    _model.AvailableTime = ScheduleTime;
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

        public void UpdateInterviewSchedule(int UserId, DateTime ScheduleDate)
        {
            try
            {
                List<SqlParameter> pars = new List<SqlParameter>();
                pars.Add(GetSqlParameter("@InterviewerId", SqlDbType.Int, UserId));
                pars.Add(GetSqlParameter("@Date", SqlDbType.DateTime, ScheduleDate));
                SqlHelper.ExecuteNonQuery(CS, SP, "Proc_Update_InterviewerSchedule", pars.ToArray());
            }
            catch (Exception ex)
            {
                MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                string methodName = method.Name;
                string className = method.ReflectedType.Name;
                sendErrorMail(ex, methodName, className);
            }
        }

        public void SaveInterviewSchedule(int UserId, DateTime ScheduleDate, string AvailableTime)
        {
            try
            {
                List<SqlParameter> pars = new List<SqlParameter>();
                pars.Add(GetSqlParameter("@InterviewerId", SqlDbType.Int, UserId));
                pars.Add(GetSqlParameter("@Date", SqlDbType.DateTime, ScheduleDate));
                pars.Add(GetSqlParameter("@AvailableTime", SqlDbType.VarChar, AvailableTime));
                //SqlHelper.ExecuteNonQuery(CS, SP, "Proc_Update_InterviewerSchedule", pars.ToArray());
                SqlHelper.ExecuteNonQuery(CS, SP, "Proc_Save_InterviewerSchedule", pars.ToArray());
            }
            catch (Exception ex)
            {
                MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                string methodName = method.Name;
                string className = method.ReflectedType.Name;
                sendErrorMail(ex, methodName, className);
            }
        }

        public List<InterviewerModel> GetInterviewerSchedulesByDate(int UserId, DateTime ScheduleDate)
        {
            List<InterviewerModel> nwmd = new List<InterviewerModel>();
            List<SqlParameter> pars = new List<SqlParameter>();
            try
            {
                pars.Add(GetSqlParameter("@UserId", SqlDbType.Int, UserId));
                pars.Add(GetSqlParameter("@Date", SqlDbType.DateTime, ScheduleDate));
                DataSet ds = new DataSet();
                string[] tables = new string[] { "DailySchedule" };
                SqlHelper.FillDataset(CS, SP, "Proc_Get_InterviewerSchedulesByDate", ds, tables, pars.ToArray());
                string time = "";
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    //time = ds.Tables[0].Rows[i]["DailyScheduleTime"].ToString();
                    if (time == "")
                    {
                        time = ds.Tables[0].Rows[i]["DailyScheduleTime"].ToString();
                    }
                    else
                    {
                        time = time + ",   " + ds.Tables[0].Rows[i]["DailyScheduleTime"].ToString();
                    }

                }
                InterviewerModel _model = new InterviewerModel();
                _model.AvailableTime = time;
                _model.ScheduleDate = ScheduleDate;
                _model.DisplayDate = _model.ScheduleDate.ToString("dd/MMM/yyyy");
                nwmd.Add(_model);
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

        public void ReferUser(string name, string email, string skill, int Type)
        {
            try
            {
                List<SqlParameter> pars = new List<SqlParameter>();
                pars.Add(GetSqlParameter("@Name", SqlDbType.VarChar, name));
                pars.Add(GetSqlParameter("@Email", SqlDbType.VarChar, email));
                pars.Add(GetSqlParameter("@Skill", SqlDbType.VarChar, skill));
                //pars.Add(GetSqlParameter("@Password", SqlDbType.VarChar, password));
                pars.Add(GetSqlParameter("@Type", SqlDbType.Int, Type));
                SqlHelper.ExecuteNonQuery(CS, SP, "Proc_Save_ReferedBy", pars.ToArray());
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