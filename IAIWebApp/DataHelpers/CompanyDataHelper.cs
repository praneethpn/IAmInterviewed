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
    public class CompanyDataHelper : BaseDataHelper
    {
        public List<CompanyModel> GetCompanyProfile(int companyId)
        {
            List<CompanyModel> nwmd = new List<CompanyModel>();
            List<SqlParameter> pars = new List<SqlParameter>();
            try
            {
                pars.Add(GetSqlParameter("@CompanyId", SqlDbType.Int, companyId));
                DataSet ds = new DataSet();
                string[] tables = new string[] { "CompanyProfile" };
                SqlHelper.FillDataset(CS, SP, "Proc_Get_CompanyProfile", ds, tables, pars.ToArray());
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    CompanyModel _model = new CompanyModel();
                    _model.DetailsId = Convert.ToInt32(ds.Tables[0].Rows[i]["DetailsId"]);
                    _model.Phone = ds.Tables[0].Rows[i]["Phone"].ToString();
                    _model.Website = ds.Tables[0].Rows[i]["Website"].ToString();
                    _model.ContactName = ds.Tables[0].Rows[i]["ContactName"].ToString();
                    _model.ContactPhone = ds.Tables[0].Rows[i]["ContactPhone"].ToString();
                    _model.ContactEmail = ds.Tables[0].Rows[i]["ContactEmail"].ToString();
                    _model.Address = ds.Tables[0].Rows[i]["Address"].ToString();
                    _model.Logo = ds.Tables[0].Rows[i]["CompanyLogo"].ToString();
                    _model.CompanyName = ds.Tables[0].Rows[i]["CompanyName"].ToString();
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
        public List<CompanyModel> GetCompanyUserType(int companyId)
        {
            List<CompanyModel> nwmd = new List<CompanyModel>();
            try
            {
                List<SqlParameter> pars = new List<SqlParameter>();
                pars.Add(GetSqlParameter("@UserId", SqlDbType.Int, companyId));
                DataSet ds = new DataSet();
                string[] tables = new string[] { "Skill" };
                SqlHelper.FillDataset(CS, SP, "Proc_Get_CompanyUserType", ds, tables, pars.ToArray());
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    CompanyModel _model = new CompanyModel();
                    //_model.PM = Convert.ToInt32(ds.Tables[0].Rows[i]["UserId"]);
                    _model.CompanyUserType = ds.Tables[0].Rows[i]["SubUserType"].ToString();
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
        public List<CompanyModel> GetCompanyDashBoardInterviewDetails(int companyId)
        {
            List<CompanyModel> nwmd = new List<CompanyModel>();
            List<SqlParameter> pars = new List<SqlParameter>();
            try
            {

                pars.Add(GetSqlParameter("@UserId", SqlDbType.Int, companyId));
                DataSet ds = new DataSet();
                string[] tables = new string[] { "Interviewes" };
                SqlHelper.FillDataset(CS, SP, "Proc_Get_CompanyDashBoardInterviewsDetails", ds, tables, pars.ToArray());
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    CompanyModel _model = new CompanyModel();
                    _model.InterviewsAllowed = ds.Tables[0].Rows[i]["InterviewesAllowed"].Equals(DBNull.Value) ? 500 : Convert.ToInt32(ds.Tables[0].Rows[i]["InterviewesAllowed"]);
                    _model.UsedInterviewes = ds.Tables[0].Rows[i]["InterviewsUsed"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["InterviewsUsed"]);
                    _model.ScheduledInterviewes = ds.Tables[0].Rows[i]["InterviewsScheduled"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["InterviewsScheduled"]);
                    _model.InActiveSchedules = ds.Tables[0].Rows[i]["InActiveSchedules"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["InActiveSchedules"]);
                    _model.SubscriptionStartDate = ds.Tables[0].Rows[i]["SubscriptionStartDate"].Equals(DBNull.Value) ? DateTime.Now : Convert.ToDateTime(ds.Tables[0].Rows[i]["SubscriptionStartDate"]);
                    _model.SubscriptionEndDate = ds.Tables[0].Rows[i]["SubscriptionEndDate"].Equals(DBNull.Value) ? DateTime.Now : Convert.ToDateTime(ds.Tables[0].Rows[i]["SubscriptionEndDate"]);
                    _model.DisplayJobStartDate = _model.SubscriptionStartDate.ToString("dd/MMM/yyyy");
                    _model.DisplayJobEndDate = _model.SubscriptionEndDate.ToString("dd/MMM/yyyy");
                    _model.RemainingInterviewes = _model.InterviewsAllowed - _model.UsedInterviewes;
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

        public List<CompanyModel> GetCompanyDashBoardRequirementsDetails(int CompanyId)
        {
            List<CompanyModel> nwmd = new List<CompanyModel>();
            List<SqlParameter> pars = new List<SqlParameter>();
            try
            {

                pars.Add(GetSqlParameter("@UserId", SqlDbType.Int, CompanyId));
                DataSet ds = new DataSet();
                string[] tables = new string[] { "Interviewes" };
                SqlHelper.FillDataset(CS, SP, "Proc_Get_CompanyDashBoardInterviewsDetails", ds, tables, pars.ToArray());
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    CompanyModel _model = new CompanyModel();
                    _model.TotalRequirements = ds.Tables[1].Rows[i]["TotalRequirements"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(ds.Tables[1].Rows[i]["TotalRequirements"]);
                    _model.OpenRequirements = ds.Tables[1].Rows[i]["OpenRequirements"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(ds.Tables[1].Rows[i]["OpenRequirements"]);
                    _model.ClosedRequirements = ds.Tables[1].Rows[i]["ClosedRequirements"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(ds.Tables[1].Rows[i]["ClosedRequirements"]);
                    _model.OnHoldRequirements = ds.Tables[1].Rows[i]["OnHoldRequirements"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(ds.Tables[1].Rows[i]["OnHoldRequirements"]);
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

        public List<CompanyModel> GetCompanyDashBoardSkillWiseProfileDetails(int CompanyId)
        {
            List<CompanyModel> nwmd = new List<CompanyModel>();
            List<SqlParameter> pars = new List<SqlParameter>();
            try
            {

                pars.Add(GetSqlParameter("@CompanyId", SqlDbType.Int, CompanyId));
                DataSet ds = new DataSet();
                string[] tables = new string[] { "Profiles" };
                SqlHelper.FillDataset(CS, SP, "Proc_Get_CompanyDashBoardSkillWiseProfileDetails", ds, tables, pars.ToArray());
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    CompanyModel _model = new CompanyModel();
                    _model.PrimarySkill = ds.Tables[0].Rows[i]["primaryskill"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["primaryskill"]);
                    _model.PrimarySKillName = ds.Tables[0].Rows[i]["SkillName"].Equals(DBNull.Value) ? "" : ds.Tables[0].Rows[i]["SkillName"].ToString();
                    _model.CompanyProfiles = ds.Tables[0].Rows[i]["CompanyProfiles"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["CompanyProfiles"]);
                    //_model.IAIProfiles = ds.Tables[0].Rows[i]["IAIProfiles"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["IAIProfiles"]);

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
        public List<CompanyModel> GetCompanyRequirements(int jtStartIndex, int jtPageSize, string jtSorting, int userId, string primarySkill, string jobCode, string postedBy, string status)
        {
            List<CompanyModel> nwmd = new List<CompanyModel>();
            List<SqlParameter> pars = new List<SqlParameter>();
            try
            {
                pars.Add(GetSqlParameter("@FirstRow", SqlDbType.Int, jtStartIndex));
                pars.Add(GetSqlParameter("@LastRow", SqlDbType.Int, jtPageSize));
                pars.Add(GetSqlParameter("@OrderBy", SqlDbType.VarChar, jtSorting));
                pars.Add(GetSqlParameter("@UserId", SqlDbType.Int, userId));
                pars.Add(GetSqlParameter("@Skill", SqlDbType.VarChar, primarySkill));
                pars.Add(GetSqlParameter("@JobCode1", SqlDbType.VarChar, jobCode));
                pars.Add(GetSqlParameter("@PostedBy", SqlDbType.VarChar, postedBy));
                pars.Add(GetSqlParameter("@Status", SqlDbType.VarChar, status));
                DataSet ds = new DataSet();
                string[] tables = new string[] { "CompanyRequirements" };
                SqlHelper.FillDataset(CS, SP, "Proc_Select_CompanyRequirements", ds, tables, pars.ToArray());
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    CompanyModel _model = new CompanyModel();
                    _model.ReqId = ds.Tables[0].Rows[i]["ReqId"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[i]["ReqId"]) : 0;
                    _model.JobCode = ds.Tables[0].Rows[i]["jobcode"].ToString();
                    _model.JobTitleId = ds.Tables[0].Rows[i]["designationid"].ToString();
                    _model.JobTitle = ds.Tables[0].Rows[i]["designation"].ToString();
                    _model.JobReqCustom = _model.JobTitle + " - " + _model.JobCode;
                    _model.Location = ds.Tables[0].Rows[i]["Location"].ToString();
                    _model.MinExp = ds.Tables[0].Rows[i]["minexp"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[i]["minexp"]) : 0;
                    _model.MaxExp = ds.Tables[0].Rows[i]["maxexp"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[i]["maxexp"]) : 0;
                    _model.HighestPay = ds.Tables[0].Rows[i]["highestpay"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[i]["highestpay"]) : 0;
                    _model.PrimarySkill = ds.Tables[0].Rows[i]["primaryskill"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[i]["primaryskill"]) : 0;
                    _model.PrimarySKillName = ds.Tables[0].Rows[i]["skillname"].ToString();
                    _model.AdditionalSkills = ds.Tables[0].Rows[i]["Additionalskills"].ToString();
                    _model.SecSkill1 = ds.Tables[0].Rows[i]["secskill1"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[i]["secskill1"]) : 0;
                    _model.SecSkill2 = ds.Tables[0].Rows[i]["secskill2"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[i]["secskill2"]) : 0;
                    _model.SecSkill3 = ds.Tables[0].Rows[i]["secskill3"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[i]["secskill3"]) : 0;
                    _model.SecSkill4 = ds.Tables[0].Rows[i]["secskill4"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[i]["secskill4"]) : 0;
                    _model.SecSkill5 = ds.Tables[0].Rows[i]["secskill5"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[i]["secskill5"]): 0;
                    _model.SecSkillsList = ds.Tables[0].Rows[i]["SecSkill1Name"].ToString() + ", " + ds.Tables[0].Rows[i]["SecSkill2Name"].ToString() + ", " + ds.Tables[0].Rows[i]["SecSkill3Name"].ToString() + ", " + ds.Tables[0].Rows[i]["SecSkill4Name"].ToString() + ", " + ds.Tables[0].Rows[i]["SecSkill5Name"].ToString();
                    //_model.SecSkillsList=
                    _model.JobPostingSDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["JobPostingStartDate"]);
                    _model.JobPostingEDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["JobPostingEndDate"]);
                    _model.DisplayJobStartDate = _model.JobPostingSDate.ToString("dd/MMM/yyyy");
                    _model.DisplayJobEndDate = _model.JobPostingEDate.ToString("dd/MMM/yyyy");
                    _model.JobDesc = ds.Tables[0].Rows[i]["jobdesc"].ToString();
                    _model.JobType = ds.Tables[0].Rows[i]["JobType"].ToString();
                    _model.Remarks = ds.Tables[0].Rows[i]["remarks"].ToString();
                    //_model.RowNo = Convert.ToInt32(ds.Tables[0].Rows[i]["RowNo"]);
                    //_model.TotalCount = Convert.ToInt32(ds.Tables[0].Rows[i]["TotalCount"]);
                    _model.PostedBy = ds.Tables[0].Rows[i]["Name"].ToString();
                    _model.PostedById = ds.Tables[0].Rows[i]["CreatedBy"].ToString();
                    _model.AssignTo = ds.Tables[0].Rows[i]["AssignedTo"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[i]["AssignedTo"]) : 0;
                    _model.AssignToDisplay = _model.AssignTo.ToString();
                    _model.AssignToName = ds.Tables[0].Rows[i]["AssignedToName"].ToString();
                    _model.PM = ds.Tables[0].Rows[i]["PM"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[i]["PM"]) : 0;
                    _model.PMDisplay = _model.PM.ToString();
                    _model.PMName = ds.Tables[0].Rows[i]["PMName"].ToString();
                    _model.RecruiterId = ds.Tables[0].Rows[i]["RecruiterId"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[i]["RecruiterId"]) : 0;
                    _model.RecruiterName = ds.Tables[0].Rows[i]["RecruiterName"].ToString();
                    _model.Status = ds.Tables[0].Rows[i]["Status"].ToString();
                    _model.StatusRemarks = ds.Tables[0].Rows[i]["StatusRemarks"].ToString();
                    _model.SelectedProfilesCount = ds.Tables[0].Rows[i]["SelectedProfilesCount"].ToString();
                    _model.PreAppliedCandidatesCount = ds.Tables[0].Rows[i]["PreAppliedCandidatesCount"].ToString();
                    _model.CompanyProfilesCount = ds.Tables[0].Rows[i]["CompanyProfilesCount"].ToString();
                    _model.subSkill1 = ds.Tables[0].Rows[i]["Subskill1"].ToString();
                    _model.subSkill2 = ds.Tables[0].Rows[i]["Subskill2"].ToString();
                    _model.subSkill3 = ds.Tables[0].Rows[i]["Subskill3"].ToString();
                    _model.subSkill4 = ds.Tables[0].Rows[i]["Subskill4"].ToString();
                    _model.subSkill5 = ds.Tables[0].Rows[i]["Subskill5"].ToString();
                    _model.subSkill6 = ds.Tables[0].Rows[i]["Subskill6"].ToString();
                    _model.subSkill7 = ds.Tables[0].Rows[i]["Subskill7"].ToString();
                    _model.subSkill8 = ds.Tables[0].Rows[i]["Subskill8"].ToString();
                    _model.subSkill9 = ds.Tables[0].Rows[i]["Subskill9"].ToString();
                    _model.subSkill10 = ds.Tables[0].Rows[i]["Subskill10"].ToString();
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
        public List<CompanyModel> FillRecruiters(string UserId)
        {
            List<CompanyModel> nwmd = new List<CompanyModel>();
            try
            {
                List<SqlParameter> pars = new List<SqlParameter>();
                pars.Add(GetSqlParameter("@UserId", SqlDbType.VarChar, UserId));
                DataSet ds = new DataSet();
                string[] tables = new string[] { "Skill" };
                SqlHelper.FillDataset(CS, SP, "Proc_Fill_Recruiters", ds, tables, pars.ToArray());
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    CompanyModel _model = new CompanyModel();
                    _model.AssignTo = Convert.ToInt32(ds.Tables[0].Rows[i]["UserId"]);
                    _model.AssignToName = ds.Tables[0].Rows[i]["Name"].ToString();
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
        public List<CompanyModel> FillPM(string UserId)
        {
            List<CompanyModel> nwmd = new List<CompanyModel>();
            try
            {
                List<SqlParameter> pars = new List<SqlParameter>();
                pars.Add(GetSqlParameter("@UserId", SqlDbType.VarChar, UserId));
                DataSet ds = new DataSet();
                string[] tables = new string[] { "Skill" };
                SqlHelper.FillDataset(CS, SP, "Proc_Fill_PM", ds, tables, pars.ToArray());
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    CompanyModel _model = new CompanyModel();
                    _model.PM = Convert.ToInt32(ds.Tables[0].Rows[i]["UserId"]);
                    _model.PMName = ds.Tables[0].Rows[i]["Name"].ToString();
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

        public void UpdateCompanyrequirements(string ReqId, string AssignTo, string PM, string Status, string StatusRemarks)
        {
            try
            {
                List<SqlParameter> pars = new List<SqlParameter>();
                pars.Add(GetSqlParameter("@ReqId", SqlDbType.VarChar, ReqId));
                pars.Add(GetSqlParameter("@AssignTo", SqlDbType.VarChar, AssignTo));
                pars.Add(GetSqlParameter("@PM", SqlDbType.VarChar, PM));
                pars.Add(GetSqlParameter("@Status", SqlDbType.VarChar, Status));
                pars.Add(GetSqlParameter("@StatusRemarks", SqlDbType.VarChar, StatusRemarks));

                SqlHelper.ExecuteNonQuery(CS, SP, "Proc_Update_CompanyRequirements", pars.ToArray());
            }
            catch (Exception ex)
            {
                MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                string methodName = method.Name;
                string className = method.ReflectedType.Name;
                sendErrorMail(ex, methodName, className);
            }
        }

        public List<CompanyModel> GetCompanyDashBoardJobPostingDetails(int ReqId)
        {
            List<CompanyModel> nwmd = new List<CompanyModel>();
            List<SqlParameter> pars = new List<SqlParameter>();
            try
            {

                pars.Add(GetSqlParameter("@ReqId", SqlDbType.Int, ReqId));
                DataSet ds = new DataSet();
                string[] tables = new string[] { "JobPostings" };
                SqlHelper.FillDataset(CS, SP, "Proc_Get_CompanyDashBoardJobPostingDetails", ds, tables, pars.ToArray());
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    CompanyModel _model = new CompanyModel();
                    _model.TotalProfiles = ds.Tables[0].Rows[i]["TotalProfiles"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["TotalProfiles"]);
                    _model.InterviewedProfiles = ds.Tables[0].Rows[i]["Interviewed"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["Interviewed"]);
                    _model.SelectedProfiles = ds.Tables[0].Rows[i]["Selected"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["Selected"]);
                    _model.JoinedProfiles = ds.Tables[0].Rows[i]["Joined"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["Joined"]);
                    _model.RejectedProfiles = ds.Tables[0].Rows[i]["Rejected"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["Rejected"]);

                    _model.IAISelectedProfiles = ds.Tables[0].Rows[i]["IAISelectedProfiles"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["IAISelectedProfiles"]);
                    _model.IAIJoinedProfiles = ds.Tables[0].Rows[i]["IAIJoinedProfiles"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["IAIJoinedProfiles"]);
                    _model.IAIInProgressProfiles = ds.Tables[0].Rows[i]["IAIInProgressProfiles"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["IAIInProgressProfiles"]);
                    _model.IAIRejectedProfiles = ds.Tables[0].Rows[i]["IAIRejectedProfiles"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["IAIRejectedProfiles"]);

                    _model.InterviewedSelectedProfiles = ds.Tables[0].Rows[i]["InterviewedSelected"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["InterviewedSelected"]);
                    _model.InterviewedJoinedProfiles = ds.Tables[0].Rows[i]["InterviewedJoined"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["InterviewedJoined"]);
                    _model.InterviewedRejectedProfiles = ds.Tables[0].Rows[i]["InterviewedRejected"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["InterviewedRejected"]);
                    _model.InterviewedInProgressProfiles = ds.Tables[0].Rows[i]["InterviewedInProgress"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["InterviewedInProgress"]);
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

        public void SaveCompanyProfile(CompanyProfileModel _model)
        {
            try
            {
                List<SqlParameter> pars = new List<SqlParameter>();
                pars.Add(GetSqlParameter("@DetailsId", SqlDbType.Int, _model.DetailsId));
                pars.Add(GetSqlParameter("@Phone", SqlDbType.VarChar, _model.Phone));
                pars.Add(GetSqlParameter("@Website", SqlDbType.VarChar, _model.Website));
                pars.Add(GetSqlParameter("@ConatactName", SqlDbType.VarChar, _model.ContactName));
                pars.Add(GetSqlParameter("@ContactPhone", SqlDbType.VarChar, _model.ContactPhone));
                pars.Add(GetSqlParameter("@ContactEmail", SqlDbType.VarChar, _model.ContactEmail));
                pars.Add(GetSqlParameter("@Address", SqlDbType.VarChar, _model.Address));
                pars.Add(GetSqlParameter("@Logo", SqlDbType.VarChar, _model.Logo));
                pars.Add(GetSqlParameter("@UserId", SqlDbType.Int, _model.UserId));
                SqlHelper.ExecuteNonQuery(CS, SP, "Proc_Save_CompanyProfile", pars.ToArray());
            }
            catch (Exception ex)
            {
                MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                string methodName = method.Name;
                string className = method.ReflectedType.Name;
                sendErrorMail(ex, methodName, className);
            }
        }

        public List<CompanyModel> SaveCompanyRequirements(CompanyRequirementsModel _model)
        {

            List<CompanyModel> nwmd = new List<CompanyModel>();
            List<SqlParameter> pars = new List<SqlParameter>();
            try
            {
                pars.Add(GetSqlParameter("@ReqId", SqlDbType.Int, _model.ReqId));
                pars.Add(GetSqlParameter("@JobCode", SqlDbType.VarChar, _model.JobCode));
                pars.Add(GetSqlParameter("@JobTitle", SqlDbType.VarChar, _model.JobTitle));
                pars.Add(GetSqlParameter("@JobType", SqlDbType.VarChar, _model.JobType));
                pars.Add(GetSqlParameter("@Location", SqlDbType.VarChar, _model.Location));
                pars.Add(GetSqlParameter("@MinExp", SqlDbType.Int, _model.MinExp));
                pars.Add(GetSqlParameter("@MaxExp", SqlDbType.Int, _model.MaxExp));
                pars.Add(GetSqlParameter("@HighestPay", SqlDbType.Int, _model.HighestPay));
                pars.Add(GetSqlParameter("@PrimarySkill", SqlDbType.Int, _model.PrimarySkill));
                pars.Add(GetSqlParameter("@SecSkill1", SqlDbType.Int, _model.SecSkill1));
                pars.Add(GetSqlParameter("@SecSkill2", SqlDbType.Int, _model.SecSkill2));
                pars.Add(GetSqlParameter("@SecSkill3", SqlDbType.Int, _model.SecSkill3));
                pars.Add(GetSqlParameter("@SecSkill4", SqlDbType.Int, _model.SecSkill4));
                pars.Add(GetSqlParameter("@SecSkill5", SqlDbType.Int, _model.SecSkill5));
                pars.Add(GetSqlParameter("@JobDesc", SqlDbType.VarChar, _model.JobDesc));
                pars.Add(GetSqlParameter("@JobPostingSDate", SqlDbType.DateTime, _model.JobPostingSDate));
                pars.Add(GetSqlParameter("@JobPostingEDate", SqlDbType.DateTime, _model.JobPostingEDate));
                pars.Add(GetSqlParameter("@Remarks", SqlDbType.VarChar, _model.Remarks));
                pars.Add(GetSqlParameter("@UserId", SqlDbType.Int, _model.UserId));
                pars.Add(GetSqlParameter("@Additionalskills", SqlDbType.VarChar, _model.AdditionalSkills));
                pars.Add(GetSqlParameter("@SubSkill1", SqlDbType.VarChar, _model.subSkill1));
                pars.Add(GetSqlParameter("@SubSkill2", SqlDbType.VarChar, _model.subSkill2));
                pars.Add(GetSqlParameter("@SubSkill3", SqlDbType.VarChar, _model.subSkill3));
                pars.Add(GetSqlParameter("@SubSkill4", SqlDbType.VarChar, _model.subSkill4));
                pars.Add(GetSqlParameter("@SubSkill5", SqlDbType.VarChar, _model.subSkill5));
                pars.Add(GetSqlParameter("@SubSkill6", SqlDbType.VarChar, _model.subSkill6));
                pars.Add(GetSqlParameter("@SubSkill7", SqlDbType.VarChar, _model.subSkill7));
                pars.Add(GetSqlParameter("@SubSkill8", SqlDbType.VarChar, _model.subSkill8));
                pars.Add(GetSqlParameter("@SubSkill9", SqlDbType.VarChar, _model.subSkill9));
                pars.Add(GetSqlParameter("@SubSkill10", SqlDbType.VarChar, _model.subSkill10));
                SqlHelper.ExecuteNonQuery(CS, SP, "Proc_Save_CompanyRequirements", pars.ToArray());
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

        public DataRow IsRequirementExists(int CompanyId, string Jobcode)
        {
            DataRow userdata = null;
            DataTable usercollection = null;
            try
            {
                List<SqlParameter> pars = new List<SqlParameter>();
                pars.Add(GetSqlParameter("@CompanyId", SqlDbType.Int, CompanyId));
                pars.Add(GetSqlParameter("@Jobcode", SqlDbType.VarChar, Jobcode));
                usercollection = SqlHelper.ExecuteDataset(CS, SP, "Proc_IsExits_CompanyRequirement", pars.ToArray()).Tables[0];
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
        public List<CompanyModel> GetCompanyReqRelatedCandidates(string reqid, string secskill1, string TotalRating, string SoftSkillRating, string Location, string Experience, string Demo)
        {
            List<CompanyModel> nwmd = new List<CompanyModel>();
            List<SqlParameter> pars = new List<SqlParameter>();
            try
            {
                pars.Add(GetSqlParameter("@ReqId", SqlDbType.VarChar, reqid));
                pars.Add(GetSqlParameter("@SecSkill1", SqlDbType.VarChar, secskill1));
                pars.Add(GetSqlParameter("@TotalRating", SqlDbType.VarChar, TotalRating));
                pars.Add(GetSqlParameter("@SoftSkillRating", SqlDbType.VarChar, SoftSkillRating));
                pars.Add(GetSqlParameter("@Location", SqlDbType.VarChar, Location));
                pars.Add(GetSqlParameter("@Experience", SqlDbType.VarChar, Experience));
                //pars.Add(GetSqlParameter("@FirstRow", SqlDbType.Int, jtStartIndex));
                //pars.Add(GetSqlParameter("@LastRow", SqlDbType.Int, jtPageSize));
                //pars.Add(GetSqlParameter("@OrderBy", SqlDbType.VarChar, jtSorting));
                DataSet dsmain = new DataSet();

                string[] tables = new string[] { "CompanyRequirements" };
                SqlHelper.FillDataset(CS, SP, "Proc_Get_CompanyReqRelatedProfiles1", dsmain, tables, pars.ToArray());
                int count = dsmain.Tables[0].Rows.Count;
                if (Demo == "Demo")
                {
                    if (dsmain.Tables[0].Rows.Count > 5)
                    {
                        count = 5;
                    }
                    else
                    {
                        count = dsmain.Tables[0].Rows.Count;
                    }
                }
                else
                {
                    count = dsmain.Tables[0].Rows.Count;
                }
                for (int i = 0; i < count; i++)
                {
                    CompanyModel _model = new CompanyModel();
                    _model.CandidateId = Convert.ToInt32(dsmain.Tables[0].Rows[i]["candidateid"]);
                    _model.HasCandidateAccess = dsmain.Tables[0].Rows[i]["GrantAccess"].ToString();
                    List<SqlParameter> parscandidate = new List<SqlParameter>();
                    List<SqlParameter> parscandidate1 = new List<SqlParameter>();
                    DataSet ds = new DataSet();
                    DataSet d1 = new DataSet();
                    string[] tab = new string[] { "CandidateProfile" };
                    string[] tablenew = new string[] { "ScheduleInterview" };
                    parscandidate.Add(GetSqlParameter("@CandidateId", SqlDbType.Int, _model.CandidateId));
                    parscandidate.Add(GetSqlParameter("@ReqID", SqlDbType.Int, reqid));
                    SqlHelper.FillDataset(CS, SP, "Proc_Get_Candidatedetailsfromschedule", ds, tablenew, parscandidate.ToArray());
                    parscandidate1.Add(GetSqlParameter("@CandidateId", SqlDbType.Int, _model.CandidateId));
                    SqlHelper.FillDataset(CS, SP, "Proc_Get_CandidateProfile", d1, tab, parscandidate1.ToArray());
                    for (int j = 0; j < d1.Tables[0].Rows.Count; j++)
                    {
                        _model.AdditionalSkills = d1.Tables[0].Rows[j]["Additionalskills"].ToString();
                    }
                    _model.ReqId = Convert.ToInt32(ds.Tables[0].Rows[0]["ReqId"]);
                    _model.JobCode = ds.Tables[0].Rows[0]["JobCode"].ToString();
                    _model.ScheduleId = ds.Tables[0].Rows[0]["ScheduleId"].ToString();
                    _model.UniqueId = ds.Tables[0].Rows[0]["UniqueId"].ToString();
                    _model.InterviewDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["ScheduledDate"]);
                    _model.DisplayDate = _model.InterviewDate.ToString("dd/MMM/yyyy");
                    _model.CandidateName = ds.Tables[0].Rows[0]["name"].ToString();
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

                        _model.SecSkillList = _model.SecSkillName1 + "-" + _model.SecSkill1Rating + ", " + _model.SecSKillName2 + "-" + _model.SecSkill2Rating + ", " + _model.SecSkillName3 + "-" + _model.SecSkill3Rating;
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
                        _model.SecSkillList = _model.SecSkillName1 + "-" + _model.SecSkill1Rating + ", " + _model.SecSKillName2 + "-" + _model.SecSkill2Rating + ", " + _model.SecSkillName3 + "-" + _model.SecSkill3Rating + ", " + _model.SecSkillName5 + "-" + _model.SecSkill5Rating;
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
                        _model.SecSkillList = _model.SecSkillName1 + "-" + _model.SecSkill1Rating + ", " + _model.SecSKillName2 + "-" + _model.SecSkill2Rating + ", " + _model.SecSkillName3 + "-" + _model.SecSkill3Rating + ", " + _model.SecSkillName4 + "-" + _model.SecSkill4Rating;
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
                        _model.SecSkillList = _model.SecSkillName1 + "-" + _model.SecSkill1Rating + ", " + _model.SecSKillName2 + "-" + _model.SecSkill2Rating + ", " + _model.SecSkillName3 + "-" + _model.SecSkill3Rating + ", " + _model.SecSkillName4 + "-" + _model.SecSkill4Rating + ", " + _model.SecSkillName5 + "-" + _model.SecSkill5Rating;
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
                    _model.EngComments = ds.Tables[0].Rows[0]["EnglishCommunicationRemarks"].ToString();
                    _model.Attitudecomments = ds.Tables[0].Rows[0]["AttitudeRemarks"].ToString();
                    _model.InterComments = ds.Tables[0].Rows[0]["InterpersonalSkillCommunicationRemarks"].ToString();
                    _model.InterviewerRemarks = ds.Tables[0].Rows[0]["InterviewerRemarks"].ToString();
                    //_model.RowNo = Convert.ToInt32(ds.Tables[0].Rows[0]["RowNo"]);
                    //_model.TotalCount = Convert.ToInt32(ds.Tables[0].Rows[0]["TotalCount"]);
                    _model.NoticePeriod = d1.Tables[0].Rows[0]["NoticePeriod"].ToString();
                    _model.ProfileSource = d1.Tables[0].Rows[0]["ProfileSource"].ToString();

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
                    _model.CanidateShortlisted = false;
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

        public List<CompanyModel> GetCompanyReqRelatedCandidates1(string reqid, string secskill1, string TotalRating, string SoftSkillRating, string Location, string Experience, string Demo, string Adskill)
        {
            List<CompanyModel> nwmd = new List<CompanyModel>();
            List<SqlParameter> pars = new List<SqlParameter>();
            try
            {
                pars.Add(GetSqlParameter("@ReqId", SqlDbType.VarChar, reqid));
                pars.Add(GetSqlParameter("@SecSkill1", SqlDbType.VarChar, secskill1));
                pars.Add(GetSqlParameter("@TotalRating", SqlDbType.VarChar, TotalRating));
                pars.Add(GetSqlParameter("@SoftSkillRating", SqlDbType.VarChar, SoftSkillRating));
                pars.Add(GetSqlParameter("@Location", SqlDbType.VarChar, Location));
                pars.Add(GetSqlParameter("@Experience", SqlDbType.VarChar, Experience));
                pars.Add(GetSqlParameter("@Adskill", SqlDbType.VarChar, Adskill));
                //pars.Add(GetSqlParameter("@FirstRow", SqlDbType.Int, jtStartIndex));
                //pars.Add(GetSqlParameter("@LastRow", SqlDbType.Int, jtPageSize));
                //pars.Add(GetSqlParameter("@OrderBy", SqlDbType.VarChar, jtSorting));
                DataSet dsmain = new DataSet();

                string[] tables = new string[] { "CompanyRequirements" };
                SqlHelper.FillDataset(CS, SP, "Proc_Get_CompanyReqRelatedProfiles2", dsmain, tables, pars.ToArray());
                int count = dsmain.Tables[0].Rows.Count;
                if (Demo == "Demo")
                {
                    if (dsmain.Tables[0].Rows.Count > 5)
                    {
                        count = 5;
                    }
                    else
                    {
                        count = dsmain.Tables[0].Rows.Count;
                    }
                }
                else
                {
                    count = dsmain.Tables[0].Rows.Count;
                }
                for (int i = 0; i < count; i++)
                {
                    CompanyModel _model = new CompanyModel();
                    _model.CandidateId = Convert.ToInt32(dsmain.Tables[0].Rows[i]["candidateid"]);
                    _model.HasCandidateAccess = dsmain.Tables[0].Rows[i]["GrantAccess"].ToString();
                    List<SqlParameter> parscandidate = new List<SqlParameter>();
                    List<SqlParameter> parscandidate1 = new List<SqlParameter>();
                    DataSet ds = new DataSet();
                    DataSet d1 = new DataSet();
                    string[] tab = new string[] { "CandidateProfile" };
                    string[] tablenew = new string[] { "ScheduleInterview" };
                    parscandidate.Add(GetSqlParameter("@CandidateId", SqlDbType.Int, _model.CandidateId));
                    parscandidate.Add(GetSqlParameter("@ReqID", SqlDbType.Int, reqid));
                    SqlHelper.FillDataset(CS, SP, "Proc_Get_Candidatedetailsfromschedule", ds, tablenew, parscandidate.ToArray());
                    parscandidate1.Add(GetSqlParameter("@CandidateId", SqlDbType.Int, _model.CandidateId));
                    parscandidate1.Add(GetSqlParameter("@Adskill", SqlDbType.VarChar, Adskill));
                    SqlHelper.FillDataset(CS, SP, "Proc_Get_CandidateProfilebasedonadskill", d1, tab, parscandidate1.ToArray());



                    _model.ReqId = Convert.ToInt32(ds.Tables[0].Rows[0]["ReqId"]);
                    _model.JobCode = ds.Tables[0].Rows[0]["JobCode"].ToString();
                    _model.ScheduleId = ds.Tables[0].Rows[0]["ScheduleId"].ToString();
                    _model.UniqueId = ds.Tables[0].Rows[0]["UniqueId"].ToString();
                    _model.InterviewDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["ScheduledDate"]);
                    _model.DisplayDate = _model.InterviewDate.ToString("dd/MMM/yyyy");
                    _model.CandidateName = ds.Tables[0].Rows[0]["name"].ToString();
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
                        _model.SecSkillList = _model.SecSkillName1 + "-" + _model.SecSkill1Rating + ", " + _model.SecSKillName2 + "-" + _model.SecSkill2Rating + ", " + _model.SecSkillName3 + "-" + _model.SecSkill3Rating;
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
                        _model.SecSkillList = _model.SecSkillName1 + "-" + _model.SecSkill1Rating + ", " + _model.SecSKillName2 + "-" + _model.SecSkill2Rating + ", " + _model.SecSkillName3 + "-" + _model.SecSkill3Rating + ", " + _model.SecSkillName5 + "-" + _model.SecSkill5Rating;
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
                        _model.SecSkillList = _model.SecSkillName1 + "-" + _model.SecSkill1Rating + ", " + _model.SecSKillName2 + "-" + _model.SecSkill2Rating + ", " + _model.SecSkillName3 + "-" + _model.SecSkill3Rating + ", " + _model.SecSkillName4 + "-" + _model.SecSkill4Rating;
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
                        _model.SecSkillList = _model.SecSkillName1 + "-" + _model.SecSkill1Rating + ", " + _model.SecSKillName2 + "-" + _model.SecSkill2Rating + ", " + _model.SecSkillName3 + "-" + _model.SecSkill3Rating + ", " + _model.SecSkillName4 + "-" + _model.SecSkill4Rating + ", " + _model.SecSkillName5 + "-" + _model.SecSkill5Rating;
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
                    _model.EngComments = ds.Tables[0].Rows[0]["EnglishCommunicationRemarks"].ToString();
                    _model.Attitudecomments = ds.Tables[0].Rows[0]["AttitudeRemarks"].ToString();
                    _model.InterComments = ds.Tables[0].Rows[0]["InterpersonalSkillCommunicationRemarks"].ToString();
                    _model.InterviewerRemarks = ds.Tables[0].Rows[0]["InterviewerRemarks"].ToString();
                    //_model.RowNo = Convert.ToInt32(ds.Tables[0].Rows[0]["RowNo"]);
                    //_model.TotalCount = Convert.ToInt32(ds.Tables[0].Rows[0]["TotalCount"]);
                    _model.AdditionalSkills = d1.Tables[0].Rows[0]["Additionalskills"].ToString();
                    _model.NoticePeriod = d1.Tables[0].Rows[0]["NoticePeriod"].ToString();
                    _model.ProfileSource = d1.Tables[0].Rows[0]["ProfileSource"].ToString();

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
                    _model.CanidateShortlisted = false;
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

        public List<CompanyModel> SaveCompanySelectedProfiles(int reqId, int candidateId, int scheduleId, int userId)
        {
            List<CompanyModel> nwmd = new List<CompanyModel>();
            List<SqlParameter> pars = new List<SqlParameter>();
            try
            {
                pars.Add(GetSqlParameter("@ReqId", SqlDbType.Int, reqId));
                pars.Add(GetSqlParameter("@CandidateId", SqlDbType.Int, candidateId));
                pars.Add(GetSqlParameter("@ScheduleId", SqlDbType.Int, scheduleId));
                pars.Add(GetSqlParameter("@UserId", SqlDbType.Int, userId));
                SqlHelper.ExecuteNonQuery(CS, SP, "Proc_Save_CompanySelectedProfiles", pars.ToArray());
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

        public List<CompanyModel> SelectedCandidatesList(int jtStartIndex, int jtPageSize, string jtSorting, string Companyid, int Reqid, string CandidateName, string secskill1, string TotalRating, string SecSkillRating)
        {
            List<CompanyModel> nwmd = new List<CompanyModel>();
            List<SqlParameter> pars = new List<SqlParameter>();

            //DataSet ds = new DataSet();
            DataSet d1 = new DataSet();
            string[] tab = new string[] { "CandidateProfile" };
            try
            {
                pars.Add(GetSqlParameter("@CompanyId", SqlDbType.VarChar, Companyid));
                pars.Add(GetSqlParameter("@FirstRow", SqlDbType.Int, jtStartIndex));
                pars.Add(GetSqlParameter("@LastRow", SqlDbType.Int, jtPageSize));
                pars.Add(GetSqlParameter("@OrderBy", SqlDbType.VarChar, jtSorting));
                pars.Add(GetSqlParameter("@ReqId", SqlDbType.Int, Reqid));
                pars.Add(GetSqlParameter("@CandidateName", SqlDbType.VarChar, CandidateName));
                pars.Add(GetSqlParameter("@secskill1", SqlDbType.VarChar, secskill1));
                pars.Add(GetSqlParameter("@TotalRating", SqlDbType.VarChar, TotalRating));
                pars.Add(GetSqlParameter("@SecSkillRating", SqlDbType.VarChar, SecSkillRating));
                DataSet dsnew = new DataSet();
                string[] tables = new string[] { "companyselectedprofiles" };
                SqlHelper.FillDataset(CS, SP, "Proc_Get_CompanySelectedProfiles", dsnew, tables, pars.ToArray());
                for (int i = 0; i < dsnew.Tables[0].Rows.Count; i++)
                {
                    CompanyModel _model = new CompanyModel();
                    _model.CandidateId = Convert.ToInt32(dsnew.Tables[0].Rows[i]["candidateid"]);
                    List<SqlParameter> parscandidate = new List<SqlParameter>();
                    DataSet ds = new DataSet();
                    string[] tablenew = new string[] { "ScheduleInterview" };
                    parscandidate.Add(GetSqlParameter("@CandidateId", SqlDbType.Int, _model.CandidateId));
                    parscandidate.Add(GetSqlParameter("@ReqID", SqlDbType.Int, Reqid));
                    SqlHelper.FillDataset(CS, SP, "Proc_Get_Candidatedetailsfromschedule", ds, tablenew, parscandidate.ToArray());
                    List<SqlParameter> parscandidate1 = new List<SqlParameter>();
                    parscandidate1.Add(GetSqlParameter("@CandidateId", SqlDbType.Int, _model.CandidateId));
                    SqlHelper.FillDataset(CS, SP, "Proc_Get_CandidateProfile", d1, tab, parscandidate1.ToArray());
                    for (int j = 0; j < d1.Tables[0].Rows.Count; j++)
                    {
                        _model.AdditionalSkills = d1.Tables[0].Rows[j]["Additionalskills"].ToString();
                    }
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DateTime FutureDate = new DateTime();
                        if (DateTime.TryParse(dsnew.Tables[0].Rows[i]["FutureUpdateDate"].ToString(), out FutureDate))
                            _model.FutureDate = FutureDate;
                        _model.DisplayDate = _model.FutureDate.ToString("dd/MMM/yyyy");
                        //_model.FutureDate = Convert.ToDateTime(dsnew.Tables[0].Rows[i]["FutureUpdateDate"]);
                        _model.Comments = dsnew.Tables[0].Rows[i]["Comments"].ToString();
                        _model.Status = dsnew.Tables[0].Rows[i]["Status"].ToString();
                        _model.HasCandidateAccess = dsnew.Tables[0].Rows[i]["GrantAccess"].ToString();
                        _model.ReqId = Convert.ToInt32(ds.Tables[0].Rows[0]["ReqId"]);
                        _model.JobCode = ds.Tables[0].Rows[0]["JobCode"].ToString();
                        _model.ScheduleId = ds.Tables[0].Rows[0]["ScheduleId"].ToString();
                        _model.UniqueId = ds.Tables[0].Rows[0]["UniqueId"].ToString();
                        _model.CandidateName = ds.Tables[0].Rows[0]["name"].ToString();
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
                            _model.SecSkillList = _model.SecSkillName1 + "-" + _model.SecSkill1Rating + ", " + _model.SecSKillName2 + "-" + _model.SecSkill2Rating + ", " + _model.SecSkillName3 + "-" + _model.SecSkill3Rating;
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
                            _model.SecSkillList = _model.SecSkillName1 + "-" + _model.SecSkill1Rating + ", " + _model.SecSKillName2 + "-" + _model.SecSkill2Rating + ", " + _model.SecSkillName3 + "-" + _model.SecSkill3Rating + ", " + _model.SecSkillName4 + "-" + _model.SecSkill4Rating;
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
                            _model.SecSkillList = _model.SecSkillName1 + "-" + _model.SecSkill1Rating + ", " + _model.SecSKillName2 + "-" + _model.SecSkill2Rating + ", " + _model.SecSkillName3 + "-" + _model.SecSkill3Rating + ", " + _model.SecSkillName4 + "-" + _model.SecSkill4Rating + ", " + _model.SecSkillName5 + "-" + _model.SecSkill5Rating;
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
                            _model.SecSkillList = _model.SecSkillName1 + "-" + _model.SecSkill1Rating + ", " + _model.SecSKillName2 + "-" + _model.SecSkill2Rating + ", " + _model.SecSkillName3 + "-" + _model.SecSkill3Rating + ", " + _model.SecSkillName4 + "-" + _model.SecSkill4Rating + ", " + _model.SecSkillName5 + "-" + _model.SecSkill5Rating;
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
                        _model.NoticePeriod = ds.Tables[0].Rows[0]["NoticePeriod"].ToString();
                        _model.ProfileSource = d1.Tables[0].Rows[0]["ProfileSource"].ToString();
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
                        _model.EngComments = ds.Tables[0].Rows[0]["EnglishCommunicationRemarks"].ToString();
                        _model.Attitudecomments = ds.Tables[0].Rows[0]["AttitudeRemarks"].ToString();
                        _model.InterComments = ds.Tables[0].Rows[0]["InterpersonalSkillCommunicationRemarks"].ToString();
                        _model.InterviewerRemarks = ds.Tables[0].Rows[0]["InterviewerRemarks"].ToString();
                        _model.Logo = ds.Tables[0].Rows[0]["Logo"].ToString();
                        _model.CanidateShortlisted = false;
                        //if (string.IsNullOrEmpty(_model.Logo))
                        //{
                        //    _model.Logo = "logo.png";
                        //}
                        // _model.RowNo = Convert.ToInt32(ds.Tables[0].Rows[i]["RowNo"]);
                        //_model.TotalCount = Convert.ToInt32(ds.Tables[0].Rows[i]["TotalCount"]);
                        nwmd.Add(_model);
                    }
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

        public CompanyModel getScheduleDetailsForViewRating(int reqId, int scheduleId)
        {
            CompanyModel _model = new CompanyModel();
            List<SqlParameter> pars = new List<SqlParameter>();
            try
            {
                pars.Add(GetSqlParameter("@ScheduleId", SqlDbType.Int, scheduleId));
                pars.Add(GetSqlParameter("@ReqId", SqlDbType.Int, reqId));
                DataSet ds = new DataSet();
                string[] tables = new string[] { "RatingDetails" };
                SqlHelper.FillDataset(CS, SP, "Proc_Get_ScheduleDetailsForViewRating", ds, tables, pars.ToArray());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    _model.CandidateId = Convert.ToInt32(ds.Tables[0].Rows[0]["UserId"]);
                    _model.AdditionalSkills = ds.Tables[0].Rows[0]["AdditionalSkills"].ToString();                    
                    _model.HasCandidateAccess = ds.Tables[0].Rows[0]["GrantAccess"].ToString();
                    _model.ReqId = reqId;
                    _model.JobCode = ds.Tables[0].Rows[0]["JobCode"].ToString();
                    _model.Designation = ds.Tables[0].Rows[0]["Designation"].ToString();
                    _model.ScheduleId = ds.Tables[0].Rows[0]["ScheduleId"].ToString();
                    _model.UniqueId = ds.Tables[0].Rows[0]["UniqueId"].ToString();
                    _model.CandidateName = ds.Tables[0].Rows[0]["name"].ToString();
                    _model.PrimarySKillName = ds.Tables[0].Rows[0]["SkillName"].ToString();
                    _model.PrimarySkill = Convert.ToInt32(ds.Tables[0].Rows[0]["SkillId"].ToString());
                    _model.Experience = ds.Tables[0].Rows[0]["Experience"].ToString();
                    _model.interviewerid = Convert.ToInt32(ds.Tables[0].Rows[0]["InterviewerId"]);
                    //_model.Interviewer = ds.Tables[0].Rows[0]["interviewer"].ToString();
                    _model.SecSkillName1 = ds.Tables[0].Rows[0]["CandidateSecondarySkill1Name"].ToString();
                    _model.SecSKillName2 = ds.Tables[0].Rows[0]["CandidateSecondarySkill2Name"].ToString();
                    _model.SecSkillName3 = ds.Tables[0].Rows[0]["CandidateSecondarySkill3Name"].ToString();
                    string SecSkillName4 = ds.Tables[0].Rows[0]["CandidateSecondarySkill4Name"].ToString();
                    _model.SecSkillName4 = ds.Tables[0].Rows[0]["CandidateSecondarySkill4Name"].ToString();
                    if (SecSkillName4 == "" || SecSkillName4 == null || SecSkillName4 == "0")
                    {
                        _model.SecSkillName4 = "--";
                    }
                    string SecSkillName5 = ds.Tables[0].Rows[0]["CandidateSecondarySkill5Name"].ToString();
                    _model.SecSkillName5 = ds.Tables[0].Rows[0]["CandidateSecondarySkill5Name"].ToString();
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
                        _model.SecSkillList = _model.SecSkillName1 + "-" + _model.SecSkill1Rating + ", " + _model.SecSKillName2 + "-" + _model.SecSkill2Rating + ", " + _model.SecSkillName3 + "-" + _model.SecSkill3Rating;
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
                        _model.SecSkillList = _model.SecSkillName1 + "-" + _model.SecSkill1Rating + ", " + _model.SecSKillName2 + "-" + _model.SecSkill2Rating + ", " + _model.SecSkillName3 + "-" + _model.SecSkill3Rating + ", " + _model.SecSkillName4 + "-" + _model.SecSkill4Rating;
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
                        _model.SecSkillList = _model.SecSkillName1 + "-" + _model.SecSkill1Rating + ", " + _model.SecSKillName2 + "-" + _model.SecSkill2Rating + ", " + _model.SecSkillName3 + "-" + _model.SecSkill3Rating + ", " + _model.SecSkillName4 + "-" + _model.SecSkill4Rating + ", " + _model.SecSkillName5 + "-" + _model.SecSkill5Rating;
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
                        _model.SecSkillList = _model.SecSkillName1 + "-" + _model.SecSkill1Rating + ", " + _model.SecSKillName2 + "-" + _model.SecSkill2Rating + ", " + _model.SecSkillName3 + "-" + _model.SecSkill3Rating + ", " + _model.SecSkillName4 + "-" + _model.SecSkill4Rating + ", " + _model.SecSkillName5 + "-" + _model.SecSkill5Rating;
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
                    _model.NoticePeriod = ds.Tables[0].Rows[0]["NoticePeriod"].ToString();
                    _model.ProfileSource = ds.Tables[0].Rows[0]["ProfileSource"].ToString();
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
                    _model.EngComments = ds.Tables[0].Rows[0]["EnglishCommunicationRemarks"].ToString();
                    _model.Attitudecomments = ds.Tables[0].Rows[0]["AttitudeRemarks"].ToString();
                    _model.InterComments = ds.Tables[0].Rows[0]["InterpersonalSkillCommunicationRemarks"].ToString();
                    _model.InterviewerRemarks = ds.Tables[0].Rows[0]["InterviewerRemarks"].ToString();
                    _model.Logo = ds.Tables[0].Rows[0]["Logo"].ToString();
                    _model.subSkill1 = ds.Tables[0].Rows[0]["subSkill1"].ToString();
                    _model.subSkill1Rating = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["subSkill1Rating"].ToString()) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["subSkill1Rating"]);
                    _model.subSkill2 = ds.Tables[0].Rows[0]["subSkill2"].ToString();
                    _model.subSkill2Rating = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["subSkill2Rating"].ToString()) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["subSkill2Rating"]);
                    _model.subSkill3 = ds.Tables[0].Rows[0]["subSkill3"].ToString();
                    _model.subSkill3Rating = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["subSkill3Rating"].ToString()) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["subSkill3Rating"]);
                    _model.subSkill4 = ds.Tables[0].Rows[0]["subSkill4"].ToString();
                    _model.subSkill4Rating = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["subSkill4Rating"].ToString()) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["subSkill4Rating"]);
                    _model.subSkill5 = ds.Tables[0].Rows[0]["subSkill5"].ToString();
                    _model.subSkill5Rating = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["subSkill5Rating"].ToString()) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["subSkill5Rating"]);
                    _model.subSkill6 = ds.Tables[0].Rows[0]["subSkill6"].ToString();
                    _model.subSkill6Rating = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["subSkill6Rating"].ToString()) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["subSkill6Rating"]);
                    _model.subSkill7 = ds.Tables[0].Rows[0]["subSkill7"].ToString();
                    _model.subSkill7Rating = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["subSkill7Rating"].ToString()) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["subSkill7Rating"]);
                    _model.subSkill8 = ds.Tables[0].Rows[0]["subSkill8"].ToString();
                    _model.subSkill8Rating = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["subSkill8Rating"].ToString()) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["subSkill8Rating"]);
                    _model.subSkill9 = ds.Tables[0].Rows[0]["subSkill9"].ToString();
                    _model.subSkill9Rating = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["subSkill9Rating"].ToString()) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["subSkill9Rating"]);
                    _model.subSkill10 = ds.Tables[0].Rows[0]["subSkill10"].ToString();
                    _model.subSkill10Rating = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["subSkill10Rating"].ToString()) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["subSkill10Rating"]);
                    _model.CanidateShortlisted = false;
                }
                return _model;
            }
            catch (Exception ex)
            {
                MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                string methodName = method.Name;
                string className = method.ReflectedType.Name;
                sendErrorMail(ex, methodName, className);
                return _model;
            }
        }

        public List<CompanyModel> SelectedGetNonInterviewedCandidates(string user, int ReqId)
        {
            List<CompanyModel> nwmd = new List<CompanyModel>();
            List<SqlParameter> pars = new List<SqlParameter>();
            try
            {
                pars.Add(GetSqlParameter("@User", SqlDbType.VarChar, user));
                pars.Add(GetSqlParameter("@ReqId", SqlDbType.VarChar, ReqId));
                //pars.Add(GetSqlParameter("@OrderBy", SqlDbType.VarChar, jtSorting));
                DataSet dsmain = new DataSet();

                string[] tables = new string[] { "NonInterviewedSelectedCandidates" };
                SqlHelper.FillDataset(CS, SP, "Proc_Get_CompanySelectedNonInterviewedCandidates", dsmain, tables, pars.ToArray());
                for (int i = 0; i < dsmain.Tables[0].Rows.Count; i++)
                {
                    CompanyModel _model = new CompanyModel();

                    _model.CandidateName = dsmain.Tables[0].Rows[i]["Name"].ToString();
                    _model.PrimarySKillName = dsmain.Tables[0].Rows[i]["primaryskill"].ToString();
                    _model.SecSkillName1 = dsmain.Tables[0].Rows[i]["Skill"].ToString();
                    _model.Email = dsmain.Tables[0].Rows[i]["EmailID"].ToString();
                    _model.MobileNumber = dsmain.Tables[0].Rows[i]["PHNumber"].ToString();
                    _model.Resume = dsmain.Tables[0].Rows[i]["Resume"].ToString();
                    _model.UniqueId = dsmain.Tables[0].Rows[i]["UniqueId"].ToString();
                    _model.Experience = dsmain.Tables[0].Rows[i]["experience"].ToString();
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

        public void UpdateFutureDate(string reqid, string candiateid, DateTime FutureUpdateDate, string Comments, string Status, string TrackingStatus)
        {
            try
            {
                List<SqlParameter> pars = new List<SqlParameter>();
                pars.Add(GetSqlParameter("@CanidateId", SqlDbType.Int, Convert.ToInt32(candiateid)));
                pars.Add(GetSqlParameter("@ReqId", SqlDbType.Int, Convert.ToInt32(reqid)));
                pars.Add(GetSqlParameter("@FutureUpdateDate", SqlDbType.DateTime, FutureUpdateDate));
                pars.Add(GetSqlParameter("@Comments", SqlDbType.VarChar, Comments));
                pars.Add(GetSqlParameter("@Status", SqlDbType.VarChar, Status));
                pars.Add(GetSqlParameter("@TrackingStatus", SqlDbType.VarChar, TrackingStatus));
                SqlHelper.ExecuteNonQuery(CS, SP, "Proc_Update_CompanySelectedProfiles", pars.ToArray());
            }
            catch (Exception ex)
            {
                MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                string methodName = method.Name;
                string className = method.ReflectedType.Name;
                sendErrorMail(ex, methodName, className);
            }
        }

        public List<CompanyModel> SelectedProfilesDump(int CandidateId, int ReqId)
        {
            List<CompanyModel> nwmd = new List<CompanyModel>();
            List<SqlParameter> pars = new List<SqlParameter>();
            try
            {
                pars.Add(GetSqlParameter("@CandidateId", SqlDbType.Int, CandidateId));
                pars.Add(GetSqlParameter("@ReqId", SqlDbType.Int, ReqId));
                DataSet ds = new DataSet();
                string[] tables = new string[] { "CompanySelectedProfiles" };
                SqlHelper.FillDataset(CS, SP, "Proc_Get_CompanySelectedProfilesDump", ds, tables, pars.ToArray());
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    CompanyModel _model = new CompanyModel();
                    DateTime FutureDate = new DateTime();
                    if (DateTime.TryParse(ds.Tables[0].Rows[i]["FutureUpdateDate"].ToString(), out FutureDate))
                        _model.FutureDate = FutureDate;
                    _model.DisplayDate = _model.FutureDate.ToString("dd/MMM/yyyy");
                    _model.FutureDate = FutureDate;
                    _model.Comments = ds.Tables[0].Rows[i]["Comments"].ToString();
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

        public List<CompanyModel> GetAppliedCandidates(int reqid)
        {
            List<CompanyModel> nwmd = new List<CompanyModel>();
            List<SqlParameter> pars = new List<SqlParameter>();
            try
            {
                pars.Add(GetSqlParameter("@ReqId", SqlDbType.VarChar, reqid));
                //pars.Add(GetSqlParameter("@FirstRow", SqlDbType.Int, jtStartIndex));
                //pars.Add(GetSqlParameter("@LastRow", SqlDbType.Int, jtPageSize));
                //pars.Add(GetSqlParameter("@OrderBy", SqlDbType.VarChar, jtSorting));
                DataSet dsmain = new DataSet();

                string[] tables = new string[] { "CompanyRequirements" };
                SqlHelper.FillDataset(CS, SP, "Proc_Get_PreAppliedCandidates", dsmain, tables, pars.ToArray());
                for (int i = 0; i < dsmain.Tables[0].Rows.Count; i++)
                {
                    CompanyModel _model = new CompanyModel();
                    _model.CandidateId = Convert.ToInt32(dsmain.Tables[0].Rows[i]["candidateid"]);
                    _model.AppliedDate = Convert.ToDateTime(dsmain.Tables[0].Rows[i]["CreatedDate"]);
                    _model.DisplayDate = _model.AppliedDate.ToString("dd/MMM/yyyy");
                    List<SqlParameter> parscandidate = new List<SqlParameter>();
                    DataSet ds = new DataSet();
                    string[] tablenew = new string[] { "ScheduleInterview" };
                    parscandidate.Add(GetSqlParameter("@CandidateId", SqlDbType.Int, _model.CandidateId));
                    parscandidate.Add(GetSqlParameter("@ReqID", SqlDbType.Int, reqid));
                    SqlHelper.FillDataset(CS, SP, "Proc_Get_Candidatedetailsfromschedule", ds, tablenew, parscandidate.ToArray());
                    _model.ReqId = Convert.ToInt32(ds.Tables[0].Rows[0]["ReqId"]);
                    _model.JobCode = ds.Tables[0].Rows[0]["JobCode"].ToString();
                    _model.ScheduleId = ds.Tables[0].Rows[0]["ScheduleId"].ToString();
                    _model.UniqueId = ds.Tables[0].Rows[0]["UniqueId"].ToString();
                    _model.CandidateName = ds.Tables[0].Rows[0]["name"].ToString();
                    _model.PrimarySKillName = ds.Tables[0].Rows[0]["SkillName"].ToString();
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
                    _model.EngComments = ds.Tables[0].Rows[0]["EnglishCommunicationRemarks"].ToString();
                    _model.Attitudecomments = ds.Tables[0].Rows[0]["AttitudeRemarks"].ToString();
                    _model.InterComments = ds.Tables[0].Rows[0]["InterpersonalSkillCommunicationRemarks"].ToString();
                    _model.InterviewerRemarks = ds.Tables[0].Rows[0]["InterviewerRemarks"].ToString();
                    _model.CanidateShortlisted = false;
                    //_model.RowNo = Convert.ToInt32(ds.Tables[0].Rows[0]["RowNo"]);
                    //_model.TotalCount = Convert.ToInt32(ds.Tables[0].Rows[0]["TotalCount"]);
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

        public List<CompanyModel> GetFanProfiles(int companyid)
        {
            List<CompanyModel> nwmd = new List<CompanyModel>();
            List<SqlParameter> pars = new List<SqlParameter>();
            try
            {
                pars.Add(GetSqlParameter("@CompanyId", SqlDbType.Int, companyid));
                //pars.Add(GetSqlParameter("@FirstRow", SqlDbType.Int, jtStartIndex));
                //pars.Add(GetSqlParameter("@LastRow", SqlDbType.Int, jtPageSize));
                //pars.Add(GetSqlParameter("@OrderBy", SqlDbType.VarChar, jtSorting));
                DataSet dsmain = new DataSet();

                string[] tables = new string[] { "FavoriteCompany" };
                SqlHelper.FillDataset(CS, SP, "Proc_Get_FanPrifiles", dsmain, tables, pars.ToArray());
                for (int i = 0; i < dsmain.Tables[0].Rows.Count; i++)
                {
                    CompanyModel _model = new CompanyModel();
                    _model.CandidateId = Convert.ToInt32(dsmain.Tables[0].Rows[i]["candidateid"]);
                    List<SqlParameter> parscandidate = new List<SqlParameter>();
                    DataSet ds = new DataSet();
                    string[] tablenew = new string[] { "ScheduleInterview" };
                    parscandidate.Add(GetSqlParameter("@CandidateId", SqlDbType.Int, _model.CandidateId));
                    parscandidate.Add(GetSqlParameter("@ReqID", SqlDbType.Int, companyid));
                    SqlHelper.FillDataset(CS, SP, "Proc_Get_Candidatedetailsfromschedule", ds, tablenew, parscandidate.ToArray());
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        _model.ReqId = Convert.ToInt32(ds.Tables[0].Rows[0]["ReqId"]);
                        _model.JobCode = ds.Tables[0].Rows[0]["JobCode"].ToString();
                        _model.ScheduleId = ds.Tables[0].Rows[0]["ScheduleId"].ToString();
                        _model.UniqueId = ds.Tables[0].Rows[0]["UniqueId"].ToString();
                        _model.CandidateName = ds.Tables[0].Rows[0]["name"].ToString();
                        _model.PrimarySKillName = ds.Tables[0].Rows[0]["SkillName"].ToString();
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
                        _model.EngComments = ds.Tables[0].Rows[0]["EnglishCommunicationRemarks"].ToString();
                        _model.Attitudecomments = ds.Tables[0].Rows[0]["AttitudeRemarks"].ToString();
                        _model.InterComments = ds.Tables[0].Rows[0]["InterpersonalSkillCommunicationRemarks"].ToString();
                        _model.InterviewerRemarks = ds.Tables[0].Rows[0]["InterviewerRemarks"].ToString();
                        _model.CanidateShortlisted = false;
                    }
                    //_model.RowNo = Convert.ToInt32(ds.Tables[0].Rows[0]["RowNo"]);
                    //_model.TotalCount = Convert.ToInt32(ds.Tables[0].Rows[0]["TotalCount"]);
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

        public List<CompanyModel> FollowUpProfiles(string Companyid, DateTime? FromDate, DateTime? ToDate)
        {
            List<CompanyModel> nwmd = new List<CompanyModel>();
            List<SqlParameter> pars = new List<SqlParameter>();
            try
            {
                pars.Add(GetSqlParameter("@CompanyId", SqlDbType.VarChar, Companyid));
                pars.Add(GetSqlParameter("@FromDate", SqlDbType.DateTime, FromDate));
                pars.Add(GetSqlParameter("@ToDate", SqlDbType.DateTime, ToDate));
                DataSet dsnew = new DataSet();
                string[] tables = new string[] { "companyselectedprofiles" };
                SqlHelper.FillDataset(CS, SP, "Proc_Get_FollowUpProfiles", dsnew, tables, pars.ToArray());
                for (int i = 0; i < dsnew.Tables[0].Rows.Count; i++)
                {
                    CompanyModel _model = new CompanyModel();
                    _model.CandidateId = Convert.ToInt32(dsnew.Tables[0].Rows[i]["candidateid"]);
                    _model.ReqId = Convert.ToInt32(dsnew.Tables[0].Rows[i]["ReqId"]);
                    List<SqlParameter> parscandidate = new List<SqlParameter>();
                    DataSet ds = new DataSet();
                    string[] tablenew = new string[] { "ScheduleInterview" };
                    parscandidate.Add(GetSqlParameter("@CandidateId", SqlDbType.Int, _model.CandidateId));
                    parscandidate.Add(GetSqlParameter("@ReqID", SqlDbType.Int, _model.ReqId));
                    SqlHelper.FillDataset(CS, SP, "Proc_Get_Candidatedetailsfromschedule", ds, tablenew, parscandidate.ToArray());
                    DateTime FutureDate = new DateTime();
                    if (DateTime.TryParse(dsnew.Tables[0].Rows[i]["FutureUpdateDate"].ToString(), out FutureDate))
                        _model.FutureDate = FutureDate;
                    _model.DisplayDate = _model.FutureDate.ToString("dd/MMM/yyyy");
                    //_model.FutureDate = Convert.ToDateTime(dsnew.Tables[0].Rows[i]["FutureUpdateDate"]);
                    _model.Comments = dsnew.Tables[0].Rows[i]["Comments"].ToString();
                    _model.TrackingStatus = dsnew.Tables[0].Rows[i]["TrackingStatus"].ToString();
                    //_model.ReqId = Convert.ToInt32(ds.Tables[0].Rows[0]["ReqId"]);
                    _model.JobCode = ds.Tables[0].Rows[0]["JobCode"].ToString();
                    _model.ScheduleId = ds.Tables[0].Rows[0]["ScheduleId"].ToString();
                    _model.CandidateName = ds.Tables[0].Rows[0]["name"].ToString();
                    _model.PrimarySKillName = ds.Tables[0].Rows[0]["SkillName"].ToString();
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
                    _model.EngComments = ds.Tables[0].Rows[0]["EnglishCommunicationRemarks"].ToString();
                    _model.Attitudecomments = ds.Tables[0].Rows[0]["AttitudeRemarks"].ToString();
                    _model.InterComments = ds.Tables[0].Rows[0]["InterpersonalSkillCommunicationRemarks"].ToString();
                    _model.InterviewerRemarks = ds.Tables[0].Rows[0]["InterviewerRemarks"].ToString();
                    // _model.RowNo = Convert.ToInt32(ds.Tables[0].Rows[i]["RowNo"]);
                    //_model.TotalCount = Convert.ToInt32(ds.Tables[0].Rows[i]["TotalCount"]);
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

        public List<CompanyModel> GetCompanyAddedCandidateDetials(string Primaryskill, string Company, DateTime? Startdate, DateTime? enddate)
        {
            List<CompanyModel> nwmd = new List<CompanyModel>();
            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(GetSqlParameter("@PrimarySkill", SqlDbType.Int, Primaryskill));
            pars.Add(GetSqlParameter("@Company", SqlDbType.VarChar, Company));
            pars.Add(GetSqlParameter("@StartDate", SqlDbType.DateTime, Startdate));
            pars.Add(GetSqlParameter("@endddate", SqlDbType.DateTime, enddate));
            try
            {
                DataSet dsmain = new DataSet();
                string[] tables = new string[] { "CompanyProfiles" };
                SqlHelper.FillDataset(CS, SP, "Proc_Get_CompanyAddedCandidateDetails", dsmain, tables, pars.ToArray());
                int count = dsmain.Tables[0].Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    CompanyModel _model = new CompanyModel();
                    _model.ProfileId = string.IsNullOrEmpty(dsmain.Tables[0].Rows[i]["ProfileId"].ToString()) ? 0 : Convert.ToInt32(dsmain.Tables[0].Rows[i]["ProfileId"]);
                    _model.CandidateId = Convert.ToInt32(dsmain.Tables[0].Rows[i]["UserId"]);
                    _model.UniqueId = dsmain.Tables[0].Rows[i]["UniqId"].ToString();
                    _model.Email = dsmain.Tables[0].Rows[i]["EmailId"].ToString();
                    _model.CandidateName = dsmain.Tables[0].Rows[i]["Name"].ToString();
                    string InterviewDate = dsmain.Tables[0].Rows[i]["InterviewDate"].ToString();
                    _model.InterviewType = string.IsNullOrEmpty(dsmain.Tables[0].Rows[i]["InterviewType"].ToString()) ? "Audio" : dsmain.Tables[0].Rows[i]["InterviewType"].ToString();
                    if (string.IsNullOrEmpty(InterviewDate))
                    {
                        _model.InterviewDateNew = "--";
                    }
                    else { _model.InterviewDateNew = dsmain.Tables[0].Rows[i]["InterviewDate"].ToString(); }
                    string Interviewer = dsmain.Tables[0].Rows[i]["Interviewer"].ToString();
                    if (string.IsNullOrEmpty(Interviewer))
                    {
                        _model.Interviewer = "0";
                    }
                    else
                    {
                        _model.Interviewer = dsmain.Tables[0].Rows[i]["Interviewer"].ToString();
                    }
                    string ScreenSelect = dsmain.Tables[0].Rows[i]["ScreenSelect"].ToString();
                    if (ScreenSelect == null || ScreenSelect == "0" || ScreenSelect == "" || ScreenSelect == "-1")
                    {
                        _model.ScreenSelect = "---";
                    }
                    else
                    { _model.ScreenSelect = dsmain.Tables[0].Rows[i]["ScreenSelect"].ToString(); }

                    string SelectStatus = dsmain.Tables[0].Rows[i]["SelectStatus"].ToString();

                    if (SelectStatus == null || SelectStatus == "0" || SelectStatus == "" || SelectStatus == "-1")
                    { _model.SelectStetus = "---"; }
                    else
                    { _model.SelectStetus = (dsmain.Tables[0].Rows[i]["SelectStatus"].ToString()); }
                    int reqid = 0;
                    if (!string.IsNullOrEmpty(dsmain.Tables[0].Rows[i]["ReqID"].ToString()))
                    {
                        reqid = Convert.ToInt32(dsmain.Tables[0].Rows[i]["ReqID"]);
                    }
                    _model.ReqId = reqid;
                    _model.Designation = dsmain.Tables[0].Rows[i]["Designation"].ToString();
                    _model.RecruiterId = Convert.ToInt32(dsmain.Tables[0].Rows[i]["RecruiterId"]);
                    _model.RecruiterName = dsmain.Tables[0].Rows[i]["RecruiterName"].ToString();
                    _model.PrimarySKillName = dsmain.Tables[0].Rows[i]["skillname"].ToString();
                    //_model.PrimarySkill = dsmain.Tables[0].Rows[i]["skillname"].ToString();
                    _model.AppliedDate = Convert.ToDateTime(dsmain.Tables[0].Rows[i]["CreatedDate"]);
                    _model.DisplayDate = _model.AppliedDate.ToString("dd/MMM/yyyy");
                    _model.StatusRemarks = dsmain.Tables[0].Rows[i]["StatusUpdateComments"].ToString();
                    _model.FutureUpdateDate = dsmain.Tables[0].Rows[0]["FutureUpdateDate"].Equals(DBNull.Value) ? DateTime.Now : Convert.ToDateTime(dsmain.Tables[0].Rows[0]["FutureUpdateDate"]);
                    _model.FutureUpdateTime = dsmain.Tables[0].Rows[i]["FutureUpdateTime"].ToString();
                    _model.FutureUpdateStatus = dsmain.Tables[0].Rows[i]["Status"].ToString();
                    _model.FutureUpdateComments = dsmain.Tables[0].Rows[i]["Comments"].ToString();
                    _model.MobileNumber = dsmain.Tables[0].Rows[i]["MobileNo"].ToString();
                    List<SqlParameter> parscandidate = new List<SqlParameter>();
                    List<SqlParameter> parscandidate1 = new List<SqlParameter>();
                    DataSet ds = new DataSet();
                    DataSet d1 = new DataSet();
                    string[] tab = new string[] { "CandidateProfile" };
                    string[] tablenew = new string[] { "ScheduleInterview" };
                    parscandidate.Add(GetSqlParameter("@CandidateId", SqlDbType.Int, _model.CandidateId));
                    parscandidate.Add(GetSqlParameter("@ReqID", SqlDbType.Int, reqid));
                    SqlHelper.FillDataset(CS, SP, "Proc_Get_Candidatedetailsfromschedule", ds, tablenew, parscandidate.ToArray());
                    parscandidate1.Add(GetSqlParameter("@CandidateId", SqlDbType.Int, _model.CandidateId));
                    SqlHelper.FillDataset(CS, SP, "Proc_Get_CandidateProfile", d1, tab, parscandidate1.ToArray());
                    if (ds.Tables[0].Rows.Count > 0)
                    {                        
                        _model.JobCode = ds.Tables[0].Rows[0]["JobCode"].ToString();
                        _model.ScheduleId = ds.Tables[0].Rows[0]["ScheduleId"].ToString();
                        _model.InterviewDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["ScheduledDate"]);
                        _model.PrimarySkill = Convert.ToInt32(ds.Tables[0].Rows[0]["SkillId"].ToString());
                        _model.interviewerid = Convert.ToInt32(ds.Tables[0].Rows[0]["interviewerid"]);
                        _model.Experience = ds.Tables[0].Rows[0]["Experience"].ToString();
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

                            _model.SecSkillList = _model.SecSkillName1 + "-" + _model.SecSkill1Rating + ", " + _model.SecSKillName2 + "-" + _model.SecSkill2Rating + ", " + _model.SecSkillName3 + "-" + _model.SecSkill3Rating;
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
                            _model.SecSkillList = _model.SecSkillName1 + "-" + _model.SecSkill1Rating + ", " + _model.SecSKillName2 + "-" + _model.SecSkill2Rating + ", " + _model.SecSkillName3 + "-" + _model.SecSkill3Rating + ", " + _model.SecSkillName5 + "-" + _model.SecSkill5Rating;
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
                            _model.SecSkillList = _model.SecSkillName1 + "-" + _model.SecSkill1Rating + ", " + _model.SecSKillName2 + "-" + _model.SecSkill2Rating + ", " + _model.SecSkillName3 + "-" + _model.SecSkill3Rating + ", " + _model.SecSkillName4 + "-" + _model.SecSkill4Rating;
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
                            _model.SecSkillList = _model.SecSkillName1 + "-" + _model.SecSkill1Rating + ", " + _model.SecSKillName2 + "-" + _model.SecSkill2Rating + ", " + _model.SecSkillName3 + "-" + _model.SecSkill3Rating + ", " + _model.SecSkillName4 + "-" + _model.SecSkill4Rating + ", " + _model.SecSkillName5 + "-" + _model.SecSkill5Rating;
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
                        _model.EngComments = ds.Tables[0].Rows[0]["EnglishCommunicationRemarks"].ToString();
                        _model.Attitudecomments = ds.Tables[0].Rows[0]["AttitudeRemarks"].ToString();
                        _model.InterComments = ds.Tables[0].Rows[0]["InterpersonalSkillCommunicationRemarks"].ToString();
                        _model.InterviewerRemarks = ds.Tables[0].Rows[0]["InterviewerRemarks"].ToString();
                        //_model.RowNo = Convert.ToInt32(ds.Tables[0].Rows[0]["RowNo"]);
                        //_model.TotalCount = Convert.ToInt32(ds.Tables[0].Rows[0]["TotalCount"]);

                    }
                    if (d1.Tables[0].Rows.Count > 0)
                    {
                        for (int j = 0; j < d1.Tables[0].Rows.Count; j++)
                        {
                            _model.AdditionalSkills = d1.Tables[0].Rows[j]["Additionalskills"].ToString();
                        }

                        _model.NoticePeriod = d1.Tables[0].Rows[0]["NoticePeriod"].ToString();

                        bool s = d1.Tables[0].Rows[0]["GapInEducation"].Equals(DBNull.Value) ? false : Convert.ToBoolean(d1.Tables[0].Rows[0]["GapInEducation"]);
                        if (s == false)
                        {
                            _model.GapInEducation = "No";
                        }
                        else
                        {
                            _model.GapInEducation = "yes";
                        }
                        bool b = d1.Tables[0].Rows[0]["GapInExperience"].Equals(DBNull.Value) ? false : Convert.ToBoolean(d1.Tables[0].Rows[0]["GapInExperience"]);
                        if (b == false)
                        {

                            _model.GapInExperience = "No";
                        }
                        else
                        {
                            _model.GapInExperience = "Yes";
                        }
                        _model.CurrentPay = d1.Tables[0].Rows[0]["CurrentPay"].Equals(DBNull.Value) ? "--" : d1.Tables[0].Rows[0]["CurrentPay"].ToString();
                        _model.ExpectedPay = d1.Tables[0].Rows[0]["ExpectedPay"].Equals(DBNull.Value) ? "--" : d1.Tables[0].Rows[0]["ExpectedPay"].ToString();
                        _model.Location = d1.Tables[0].Rows[0]["CurrentLocation"].Equals(DBNull.Value) ? "--" : d1.Tables[0].Rows[0]["CurrentLocation"].ToString();
                        _model.Resume = d1.Tables[0].Rows[0]["Resume"].Equals(DBNull.Value) ? "--" : d1.Tables[0].Rows[0]["Resume"].ToString();
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

        public List<User> GetSubUsers(int jtStartIndex, int jtPageSize, string jtSorting, int UserId)
        {
            List<User> nwmd = new List<User>();
            List<SqlParameter> pars = new List<SqlParameter>();
            try
            {
                pars.Add(GetSqlParameter("@FirstRow", SqlDbType.Int, jtStartIndex));
                pars.Add(GetSqlParameter("@LastRow", SqlDbType.Int, jtPageSize));
                pars.Add(GetSqlParameter("@OrderBy", SqlDbType.VarChar, jtSorting));
                pars.Add(GetSqlParameter("@UserId", SqlDbType.Int, UserId));
                DataSet ds = new DataSet();
                string[] tables = new string[] { "SubUsers" };
                SqlHelper.FillDataset(CS, SP, "Proc_Select_SubUsers", ds, tables, pars.ToArray());
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    User _model = new User();
                    _model.SubUserId = Convert.ToInt32(ds.Tables[0].Rows[i]["SubUserId"]);
                    _model.Username = ds.Tables[0].Rows[i]["Name"].ToString();
                    _model.EmailID = ds.Tables[0].Rows[i]["Email"].ToString();
                    _model.Skill = Convert.ToInt32(ds.Tables[0].Rows[i]["skill"]);
                    _model.SkillName = ds.Tables[0].Rows[i]["SkillName"].ToString();
                    _model.UserType = ds.Tables[0].Rows[i]["SubUserType"].ToString();
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

        public List<User> SaveSubUser(CompanySubUser _model)
        {
            List<User> nwmd = new List<User>();
            List<SqlParameter> pars = new List<SqlParameter>();
            try
            {
                pars.Add(GetSqlParameter("@Name", SqlDbType.VarChar, _model.Username));
                pars.Add(GetSqlParameter("@Email", SqlDbType.VarChar, _model.EmailId));
                pars.Add(GetSqlParameter("@Skill", SqlDbType.VarChar, _model.Skill));
                pars.Add(GetSqlParameter("@Password", SqlDbType.VarChar, _model.Password));
                pars.Add(GetSqlParameter("@Type", SqlDbType.VarChar, _model.SkillType));
                pars.Add(GetSqlParameter("@UserId", SqlDbType.Int, _model.UserID));
                pars.Add(GetSqlParameter("@UserType", SqlDbType.VarChar, _model.UserType));
                SqlHelper.ExecuteNonQuery(CS, SP, "Proc_Save_SubUser", pars.ToArray());
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

        public void SaveDesignation(int DesignationId, string Designation, string Description, string UserId)
        {
            List<DesignationModel> nwmd = new List<DesignationModel>();
            List<SqlParameter> pars = new List<SqlParameter>();
            try
            {
                pars.Add(GetSqlParameter("@DesignationId", SqlDbType.Int, DesignationId));
                pars.Add(GetSqlParameter("@Designation", SqlDbType.VarChar, Designation));
                pars.Add(GetSqlParameter("@Description", SqlDbType.VarChar, Description));
                pars.Add(GetSqlParameter("@UserId", SqlDbType.Int, UserId));
                SqlHelper.ExecuteNonQuery(CS, SP, "Proc_Save_Designation", pars.ToArray());
                //return nwmd;
            }
            catch (Exception ex)
            {
                MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                string methodName = method.Name;
                string className = method.ReflectedType.Name;
                sendErrorMail(ex, methodName, className);
                //return nwmd;
            }
        }

        public List<VendorModel> GetAllVendors(int CompanyId)
        {
            List<VendorModel> Vendors = new List<VendorModel>();
            List<SqlParameter> pars = new List<SqlParameter>();
            try
            {
                pars.Add(GetSqlParameter("@Company", SqlDbType.BigInt, CompanyId));
                DataSet ds = new DataSet();
                string[] tables = new string[] { "Vendors" };
                SqlHelper.FillDataset(CS, SP, "Proc_Select_Vendors", ds, tables, pars.ToArray());
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    VendorModel _model = new VendorModel();
                    _model.VendorId = Convert.ToInt32(ds.Tables[0].Rows[i]["VendorId"]);
                    _model.CompanyId = Convert.ToInt32(ds.Tables[0].Rows[i]["CompanyId"]);
                    _model.VendorName = ds.Tables[0].Rows[i]["VendorName"].ToString();
                    _model.VendorEmail = ds.Tables[0].Rows[i]["VendorEmail"].ToString();
                    _model.VendorMobile = ds.Tables[0].Rows[i]["VendorNumber"].ToString();
                    _model.StartDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["StartDate"]);
                    _model.EndDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["EndDate"]);
                    _model.DisplayStartDate = _model.StartDate.ToString("dd/MMM/yyyy");
                    _model.DisplayEndDate = _model.EndDate.ToString("dd/MMM/yyyy");
                    Vendors.Add(_model);
                }
                return Vendors;
            }
            catch (Exception ex)
            {
                MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                string methodName = method.Name;
                string className = method.ReflectedType.Name;
                sendErrorMail(ex, methodName, className);
                return Vendors;
            }
        }

        public DataRow IsVendorExists(string VendorEmail)
        {
            DataRow vendorData = null;
            DataTable vendors = null;
            try
            {
                List<SqlParameter> pars = new List<SqlParameter>();
                pars.Add(GetSqlParameter("@VendorEmail", SqlDbType.VarChar, VendorEmail));
                vendors = SqlHelper.ExecuteDataset(CS, SP, "Proc_Vendor_Exists", pars.ToArray()).Tables[0];
                if (vendors != null && vendors.Rows != null && vendors.Rows.Count == 1)
                    vendorData = vendors.Rows[0];
                return vendorData;
            }
            catch (Exception ex)
            {
                MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                string methodName = method.Name;
                string className = method.ReflectedType.Name;
                sendErrorMail(ex, methodName, className);
                return vendorData;
            }

        }

        public void SaveVendor(string VendorName, string VendorEmail, string VendorMobile, int Company, DateTime StartDate, DateTime EndDate)
        {
            try
            {
                List<SqlParameter> pars = new List<SqlParameter>();
                pars.Add(GetSqlParameter("@VendorEmail", SqlDbType.VarChar, VendorEmail));
                pars.Add(GetSqlParameter("@VendorName", SqlDbType.VarChar, VendorName));
                pars.Add(GetSqlParameter("@VendoMobile", SqlDbType.VarChar, VendorMobile));
                pars.Add(GetSqlParameter("@Company", SqlDbType.BigInt, Company));
                pars.Add(GetSqlParameter("@StartDate", SqlDbType.DateTime, StartDate));
                pars.Add(GetSqlParameter("@EndDate", SqlDbType.DateTime, EndDate));
                SqlHelper.ExecuteNonQuery(CS, SP, "Proc_Save_Vendor", pars.ToArray());
            }
            catch (Exception ex)
            {
                MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                string methodName = method.Name;
                string className = method.ReflectedType.Name;
                sendErrorMail(ex, methodName, className);
            }
        }

        public List<CompanyModel> GetNoOfInterviewersRequired(int userid)
        {
            List<CompanyModel> nwmd = new List<CompanyModel>();
            List<SqlParameter> pars = new List<SqlParameter>();
            try
            {
                pars.Add(GetSqlParameter("@UserId", SqlDbType.Int, userid));
                DataSet ds = new DataSet();
                string[] tables = new string[] { "CompanyRequirements" };
                SqlHelper.FillDataset(CS, SP, "Proc_Select_NoOfInterviewersRequired", ds, tables, pars.ToArray());
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    CompanyModel _model = new CompanyModel();
                    _model.ReqId = Convert.ToInt32(ds.Tables[0].Rows[i]["ReqId"]);
                    _model.NoOfInterviewers = ds.Tables[0].Rows[i]["NoOfInterviewers"].ToString();
                    _model.Location = ds.Tables[0].Rows[i]["Location"].ToString();
                    _model.MinExp = Convert.ToInt32(ds.Tables[0].Rows[i]["minexp"]);
                    _model.MaxExp = Convert.ToInt32(ds.Tables[0].Rows[i]["maxexp"]);
                    _model.PrimarySkill = Convert.ToInt32(ds.Tables[0].Rows[i]["primaryskill"]);
                    _model.PrimarySKillName = ds.Tables[0].Rows[i]["skillname"].ToString();
                    _model.SecSkill1 = Convert.ToInt32(ds.Tables[0].Rows[i]["secskill1"]);
                    _model.SecSkill2 = Convert.ToInt32(ds.Tables[0].Rows[i]["secskill2"]);
                    _model.SecSkill3 = Convert.ToInt32(ds.Tables[0].Rows[i]["secskill3"]);
                    _model.SecSkill4 = Convert.ToInt32(ds.Tables[0].Rows[i]["secskill4"]);
                    _model.SecSkill5 = Convert.ToInt32(ds.Tables[0].Rows[i]["secskill5"]);
                    _model.JobPostingSDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["JobPostingStartDate"]);
                    _model.JobPostingEDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["JobPostingEndDate"]);
                    _model.DisplayStartDate = _model.JobPostingSDate.ToString("dd/MMM/yyyy");
                    _model.DisplayEndDate = _model.JobPostingEDate.ToString("dd/MMM/yyyy");
                    _model.JobDesc = ds.Tables[0].Rows[i]["JobDesc"].ToString();
                    _model.Remarks = ds.Tables[0].Rows[i]["remarks"].ToString();
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

        public void SaveRequestInterviewers(RequestInterviewersModel _model)
        {

            List<CompanyModel> nwmd = new List<CompanyModel>();
            List<SqlParameter> pars = new List<SqlParameter>();
            try
            {
                pars.Add(GetSqlParameter("@ReqId", SqlDbType.Int, _model.ReqId));
                pars.Add(GetSqlParameter("@NoOfInterviewers", SqlDbType.VarChar, _model.NoOfInterviewers));
                //pars.Add(GetSqlParameter("@JobTitle", SqlDbType.VarChar, _model.JobTitle));
                pars.Add(GetSqlParameter("@Location", SqlDbType.VarChar, _model.Location));
                pars.Add(GetSqlParameter("@MinExp", SqlDbType.Int, _model.MinExp));
                pars.Add(GetSqlParameter("@MaxExp", SqlDbType.Int, _model.MaxExp));
                //pars.Add(GetSqlParameter("@HighestPay", SqlDbType.Int, _model.HighestPay));
                pars.Add(GetSqlParameter("@PrimarySkill", SqlDbType.Int, _model.PrimarySkill));
                pars.Add(GetSqlParameter("@SecSkill1", SqlDbType.Int, _model.SecSkill1));
                pars.Add(GetSqlParameter("@SecSkill2", SqlDbType.Int, _model.SecSkill2));
                pars.Add(GetSqlParameter("@SecSkill3", SqlDbType.Int, _model.SecSkill3));
                pars.Add(GetSqlParameter("@SecSkill4", SqlDbType.Int, _model.SecSkill4));
                pars.Add(GetSqlParameter("@SecSkill5", SqlDbType.Int, _model.SecSkill5));
                pars.Add(GetSqlParameter("@JobDesc", SqlDbType.VarChar, _model.JobDesc));
                pars.Add(GetSqlParameter("@JobPostingSDate", SqlDbType.DateTime, _model.StartDate));
                pars.Add(GetSqlParameter("@JobPostingEDate", SqlDbType.DateTime, _model.EndDate));
                pars.Add(GetSqlParameter("@Remarks", SqlDbType.VarChar, _model.Remarks));
                pars.Add(GetSqlParameter("@UserId", SqlDbType.Int, _model.UserId));
                SqlHelper.ExecuteNonQuery(CS, SP, "Proc_Save_RequestInterviewers", pars.ToArray());
                //return nwmd;
            }
            catch (Exception ex)
            {
                MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                string methodName = method.Name;
                string className = method.ReflectedType.Name;
                sendErrorMail(ex, methodName, className);
                //return nwmd;
            }
        }

        public AddProfilesModel GetCandidateDetials(string Primaryskill, string Company, string JobCode, string StatusFilter, int currentPage, int pageSize)
        {
            AddProfilesModel _addProfiles = new AddProfilesModel();
            List<CandidateModel> nwmd = new List<CandidateModel>();
            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(GetSqlParameter("@PrimarySkill", SqlDbType.Int, Primaryskill));
            pars.Add(GetSqlParameter("@Company", SqlDbType.VarChar, Company));
            pars.Add(GetSqlParameter("@JobCode", SqlDbType.VarChar, JobCode));
            pars.Add(GetSqlParameter("@StatusFilter", SqlDbType.VarChar, StatusFilter));
            pars.Add(GetSqlParameter("@CurrentPage", SqlDbType.Int, currentPage));
            pars.Add(GetSqlParameter("@PageSize", SqlDbType.Int, pageSize));
            try
            {
                DataSet ds = new DataSet();
                string[] tables = new string[] { "CompanyProfiles" };
                SqlHelper.FillDataset(CS, SP, "pro_GetCandidateDetails", ds, tables, pars.ToArray());
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    CandidateModel _model = new CandidateModel();
                    _model.CompanyProfileId = ds.Tables[0].Rows[i]["ProfileId"].ToString();
                    _model.candidateid = Convert.ToInt32(ds.Tables[0].Rows[i]["UserId"]);
                    _model.Email = ds.Tables[0].Rows[i]["EmailId"].ToString();
                    _model.uniquenumber = ds.Tables[0].Rows[i]["UniqId"].ToString();
                    string rating = ds.Tables[0].Rows[i]["Rating"].ToString();
                    if (string.IsNullOrEmpty(rating))
                    {
                        _model.TotalRating = "--";
                    }
                    else
                    {
                        _model.TotalRating = ds.Tables[0].Rows[i]["Rating"].ToString();
                    }
                    string InterviewDate = ds.Tables[0].Rows[i]["InterviewDate"].ToString();
                    if (string.IsNullOrEmpty(InterviewDate))
                    {
                        _model.InterviewDate = "--";
                    }
                    else
                    {
                        _model.InterviewDate = ds.Tables[0].Rows[i]["InterviewDate"].ToString();
                    }
                    string ScheduleID = ds.Tables[0].Rows[i]["ScheduleId"].ToString();
                    if (string.IsNullOrEmpty(ScheduleID))
                    {
                        _model.ScheduleId = 0;
                    }
                    else
                    {
                        _model.ScheduleId = Convert.ToInt32(ds.Tables[0].Rows[i]["ScheduleId"]);
                    }
                    string Interviewer = ds.Tables[0].Rows[i]["Interviewer"].ToString();
                    if (string.IsNullOrEmpty(Interviewer))
                    {
                        _model.Interviewer = 0;
                    }
                    else
                    {
                        _model.Interviewer = Convert.ToInt32(ds.Tables[0].Rows[i]["Interviewer"]);
                    }
                    _model.CandidateName = ds.Tables[0].Rows[i]["Name"].ToString();
                    _model.JobCode = ds.Tables[0].Rows[i]["Jobcode"].ToString();
                    string ScreenSelect = ds.Tables[0].Rows[i]["ScreenSelect"].ToString();

                    if (ScreenSelect == null || ScreenSelect == "0" || ScreenSelect == "")
                    {
                        _model.ScreenSelect =
                              "NA";
                    }
                    else
                    { _model.ScreenSelect = ds.Tables[0].Rows[i]["ScreenSelect"].ToString(); }

                    string SelectStatus = ds.Tables[0].Rows[i]["SelectStatus"].ToString();

                    if (SelectStatus == null || SelectStatus == "0" || SelectStatus == "")
                    { _model.SelectStetus = "-1"; }
                    else
                    { _model.SelectStetus = (ds.Tables[0].Rows[i]["SelectStatus"].ToString()); }

                    _model.SecondarySkill1 = Convert.ToInt32(ds.Tables[0].Rows[i]["SecondarySkill1"]);
                    _model.PrimarySkill = Convert.ToInt32(ds.Tables[0].Rows[i]["Primaryskill"]);
                    //_model.SecondarySkill3 = Convert.ToInt32(ds.Tables[0].Rows[i]["SecondarySkill3"]);
                    //_model.SecondarySkill4 = Convert.ToInt32(ds.Tables[0].Rows[i]["SecondarySkill4"]);
                    //_model.SecondarySkill5 = Convert.ToInt32(ds.Tables[0].Rows[i]["SecondarySkill5"]);
                    //_model.CurrentPay = ds.Tables[0].Rows[i]["CurrentPay"].ToString();
                    //_model.ExpectedPay = ds.Tables[0].Rows[i]["ExpectedPay"].ToString();
                    //_model.Experience = ds.Tables[0].Rows[i]["Experience"].ToString();
                    _model.Mobile = ds.Tables[0].Rows[i]["MobileNo"].ToString();
                    //_model.Address = ds.Tables[0].Rows[i]["Address"].ToString();
                    _model.Resume = ds.Tables[0].Rows[i]["Profile"].ToString();
                    _model.StatusUpdateRemarks = ds.Tables[0].Rows[i]["StatusUpdateComments"].ToString();
                    _model.VendorId = ds.Tables[0].Rows[i]["VendorId"].ToString();
                    //_model.Photo = ds.Tables[0].Rows[i]["Photo"].ToString();
                    //_model.NoticePeriod = ds.Tables[0].Rows[i]["NoticePeriod"].ToString();
                    //_model.PromoCode = Convert.ToInt32(ds.Tables[0].Rows[i]["PromoCode"]);
                    //_model.AdditionalSkills = ds.Tables[0].Rows[i]["Additionalskills"].ToString();
                    //if (ds.Tables[0].Rows[i].IsNull("GapInEducation"))
                    //{
                    //    _model.GapInEducation = false;
                    //}
                    //else
                    //{
                    //    _model.GapInEducation = Convert.ToBoolean(ds.Tables[0].Rows[i]["GapInEducation"]);
                    //}
                    //if (ds.Tables[0].Rows[i].IsNull("GapInExperience"))
                    //{
                    //    _model.GapInExperience = false;
                    //}
                    //else
                    //{
                    //    _model.GapInExperience = Convert.ToBoolean(ds.Tables[0].Rows[i]["GapInExperience"]);
                    //}
                    nwmd.Add(_model);
                }
                _addProfiles.companyProfiles = nwmd;
                _addProfiles.totalRecords = string.IsNullOrEmpty(ds.Tables[1].Rows[0]["TotalRowCount"].ToString()) ? 0 : Convert.ToInt32(ds.Tables[1].Rows[0]["TotalRowCount"]);
                return _addProfiles;
            }
            catch (Exception ex)
            {
                MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                string methodName = method.Name;
                string className = method.ReflectedType.Name;
                sendErrorMail(ex, methodName, className);
                return _addProfiles;
            }

        }

        public void SaveCompanyAddedUser(CompanyAddedUser _candidate)
        {
            try
            {
                List<SqlParameter> pars = new List<SqlParameter>();
                pars.Add(GetSqlParameter("@Name", SqlDbType.VarChar, _candidate.candidateName));
                pars.Add(GetSqlParameter("@Email", SqlDbType.VarChar, _candidate.candidateEmail));
                pars.Add(GetSqlParameter("@Skill", SqlDbType.VarChar, _candidate.primarySkill));
                pars.Add(GetSqlParameter("@Password", SqlDbType.VarChar, _candidate.password));
                pars.Add(GetSqlParameter("@Type", SqlDbType.VarChar, _candidate.type));
                pars.Add(GetSqlParameter("@UniqId", SqlDbType.VarChar, _candidate.uniqueId));
                pars.Add(GetSqlParameter("@Secondaryskill", SqlDbType.VarChar, _candidate.secondaryskill1));
                pars.Add(GetSqlParameter("@JobCode", SqlDbType.VarChar, _candidate.jobCode));
                pars.Add(GetSqlParameter("@MobileNo", SqlDbType.VarChar, _candidate.mobileNo));
                pars.Add(GetSqlParameter("@CompanyId", SqlDbType.VarChar, _candidate.companyId));
                pars.Add(GetSqlParameter("@Profile", SqlDbType.VarChar, _candidate.resume));
                pars.Add(GetSqlParameter("@Vendor", SqlDbType.VarChar, _candidate.vendor));
                SqlHelper.ExecuteNonQuery(CS, SP, "Proc_Save_CompanyAddedUser", pars.ToArray());
            }
            catch (Exception ex)
            {
                MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                string methodName = method.Name;
                string className = method.ReflectedType.Name;
                sendErrorMail(ex, methodName, className);
            }
        }

        public List<CandidateModel> GetCandidateProfile_Company(int UserID)
        {
            List<CandidateModel> nwmd = new List<CandidateModel>();
            List<SqlParameter> pars = new List<SqlParameter>();
            try
            {
                pars.Add(GetSqlParameter("@CandidateId", SqlDbType.VarChar, UserID));
                DataSet ds = new DataSet();
                string[] tables = new string[] { "CandidateProfile" };
                SqlHelper.FillDataset(CS, SP, "Proc_Get_CandidateProfile_Company", ds, tables, pars.ToArray());
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    CandidateModel _model = new CandidateModel();
                    _model.candidateid = Convert.ToInt32(ds.Tables[0].Rows[i]["CandidateId"]);
                    _model.Email = ds.Tables[0].Rows[i]["Email"].ToString();
                    _model.uniquenumber = ds.Tables[0].Rows[i]["UniqueId"].ToString();
                    _model.Location = ds.Tables[0].Rows[i]["CurrentLocation"].Equals(DBNull.Value) ? "0" : ds.Tables[0].Rows[i]["CurrentLocation"].ToString();
                    _model.SecondarySkill1 = ds.Tables[0].Rows[i]["SecondarySkill1"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["SecondarySkill1"]);
                    _model.SecondarySkill2 = ds.Tables[0].Rows[i]["SecondarySkill2"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["SecondarySkill2"]);
                    _model.SecondarySkill3 = ds.Tables[0].Rows[i]["SecondarySkill3"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["SecondarySkill3"]);
                    _model.SecondarySkill4 = ds.Tables[0].Rows[i]["SecondarySkill4"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["SecondarySkill4"]);
                    _model.SecondarySkill5 = ds.Tables[0].Rows[i]["SecondarySkill5"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["SecondarySkill5"]);
                    _model.CurrentPay = ds.Tables[0].Rows[i]["CurrentPay"].ToString();
                    _model.ExpectedPay = ds.Tables[0].Rows[i]["ExpectedPay"].ToString();
                    _model.Experience = ds.Tables[0].Rows[i]["Experience"].Equals(DBNull.Value) ? "0" : ds.Tables[0].Rows[i]["Experience"].ToString();
                    _model.Mobile = ds.Tables[0].Rows[i]["Phone"].ToString();
                    _model.Address = ds.Tables[0].Rows[i]["Address"].ToString();
                    _model.Resume = ds.Tables[0].Rows[i]["Resume"].ToString();
                    _model.Photo = ds.Tables[0].Rows[i]["Photo"].ToString();
                    _model.NoticePeriod = ds.Tables[0].Rows[i]["NoticePeriod"].ToString();
                    _model.PromoCode = ds.Tables[0].Rows[i]["PromoCode"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["PromoCode"]);
                    _model.AdditionalSkills = ds.Tables[0].Rows[i]["Additionalskills"].ToString();
                    _model.ScreenSelect = ds.Tables[0].Rows[i]["ScreenSelect"].Equals(DBNull.Value) ? "0" : ds.Tables[0].Rows[i]["ScreenSelect"].ToString();
                    _model.SelectStetus = ds.Tables[0].Rows[i]["SelectStatus"].Equals(DBNull.Value) ? "0" : ds.Tables[0].Rows[i]["SelectStatus"].ToString();
                    _model.StatusUpdateRemarks = ds.Tables[0].Rows[i]["StatusComments"].ToString();
                    _model.FollowUpDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["FollowUpDate"]);
                    _model.FollowUpDateDisplay = _model.FollowUpDate.ToString("dd/MMM/yyyy");
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

        public void SaveCandidateProfile_Company(CandidateProfileModelCompany _candidate)
        {
            try
            {
                List<SqlParameter> pars = new List<SqlParameter>();
                pars.Add(GetSqlParameter("@CandidateId", SqlDbType.Int, _candidate.candidateid));
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
                pars.Add(GetSqlParameter("@Photo", SqlDbType.VarChar, _candidate.Photo));
                pars.Add(GetSqlParameter("@NoticePeriod", SqlDbType.VarChar, _candidate.NoticePeriod));
                pars.Add(GetSqlParameter("@GapInEducation", SqlDbType.Bit, _candidate.GapInEducation));
                pars.Add(GetSqlParameter("@GapInExperience", SqlDbType.Bit, _candidate.GapInExperience));
                pars.Add(GetSqlParameter("@AdditionalSkills", SqlDbType.VarChar, _candidate.AdditionalSkills));
                pars.Add(GetSqlParameter("@ScreenSelect", SqlDbType.VarChar, _candidate.ScreenSelect));
                pars.Add(GetSqlParameter("@SelectStetus", SqlDbType.VarChar, _candidate.SelectStatus));
                pars.Add(GetSqlParameter("@StatusUpdateRemarks", SqlDbType.VarChar, _candidate.StatusUpdateRemarks));
                pars.Add(GetSqlParameter("@UniqId", SqlDbType.VarChar, _candidate.UniqueId));
                pars.Add(GetSqlParameter("@RestrictEmployerToViewProfile", SqlDbType.Bit, _candidate.RestrictEmployerToViewProfile));
                SqlHelper.ExecuteNonQuery(CS, SP, "Proc_Save_CandidateProfile_Company", pars.ToArray());
            }
            catch (Exception ex)
            {
                MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                string methodName = method.Name;
                string className = method.ReflectedType.Name;
                sendErrorMail(ex, methodName, className);
            }
        }

        public void UpdateSelectStatus(int CandidateId, string SelectStatus, string StatusRemarks, int UserId)
        {
            try
            {
                List<SqlParameter> pars = new List<SqlParameter>();
                pars.Add(GetSqlParameter("@CandidateId", SqlDbType.Int, CandidateId));
                pars.Add(GetSqlParameter("@SelectStatus", SqlDbType.VarChar, SelectStatus));
                pars.Add(GetSqlParameter("@StatusRemarks", SqlDbType.VarChar, StatusRemarks));
                pars.Add(GetSqlParameter("@CompanyId", SqlDbType.Int, UserId));
                SqlHelper.ExecuteNonQuery(CS, SP, "Proc_Update_CandidateSelectStatus", pars.ToArray());
            }
            catch (Exception ex)
            {
                MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                string methodName = method.Name;
                string className = method.ReflectedType.Name;
                sendErrorMail(ex, methodName, className);
            }
        }

        public string SaveInterviewScheduleCompany(DateTime ScheduleDate, string Interviewer, string TimeSlot, int CandidateId, string InterviewType, int CompanyId, string JobCode, out string Message)
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
            pars.Add(GetSqlParameter("@CompanyId", SqlDbType.Int, CompanyId));
            pars.Add(GetSqlParameter("@JobCode", SqlDbType.VarChar, JobCode));
            pars.Add(GetSqlParameter("@Message", SqlDbType.VarChar, DBNull.Value));
            pars[7].Size = 1;
            pars[7].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(CS, SP, "Proc_Save_InterviewScheduleCompany", pars.ToArray());
            Message = pars[7].Value.ToString();

            return returnValue;
            //}
            //catch (Exception ex)
            //{
            //    sendErrorMail(ex);
            //    return returnValue;
            //}           
        }

        public void UpdateInterviewSchedule(int ScheduleId, DateTime ReScheduleDate, string TimeSlot)
        {
            try
            {
                List<SqlParameter> pars = new List<SqlParameter>();
                pars.Add(GetSqlParameter("@Date", SqlDbType.DateTime, ReScheduleDate));
                pars.Add(GetSqlParameter("@TimeSlot", SqlDbType.VarChar, TimeSlot));
                pars.Add(GetSqlParameter("@ScheduleId", SqlDbType.Int, ScheduleId));
                SqlHelper.ExecuteNonQuery(CS, SP, "Proc_Update_InterviewSchedule", pars.ToArray());

            }
            catch (Exception ex)
            {
                MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                string methodName = method.Name;
                string className = method.ReflectedType.Name;
                sendErrorMail(ex, methodName, className);
            }
        }

        public void saveCompanyRequestAccess(int reqId, int candidateId)
        {
            try
            {
                List<SqlParameter> pars = new List<SqlParameter>();
                pars.Add(GetSqlParameter("@ReqId", SqlDbType.BigInt, reqId));
                pars.Add(GetSqlParameter("@CandidateId", SqlDbType.BigInt, candidateId));
                SqlHelper.ExecuteNonQuery(CS, SP, "Proc_Save_CompanyRequestAccess", pars.ToArray());

            }
            catch (Exception ex)
            {
                MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                string methodName = method.Name;
                string className = method.ReflectedType.Name;
                sendErrorMail(ex, methodName, className);
            }
        }

        public CompanyProfilesFollowUp GetAllProfilesFollowUpById(int ProfileId, int companyId)
        {
            CompanyProfilesFollowUp _model = new CompanyProfilesFollowUp();
            try
            {
                List<SqlParameter> parscandidate = new List<SqlParameter>();
                List<SqlParameter> parscandidate1 = new List<SqlParameter>();
                DataSet ds = new DataSet();
                DataSet d1 = new DataSet();
                string[] tablenew = new string[] { "CompanyProfiles" };
                parscandidate.Add(GetSqlParameter("@ProfileId", SqlDbType.Int, ProfileId));
                SqlHelper.FillDataset(CS, SP, "Proc_Get_AllProfilesFollowUpById", ds, tablenew, parscandidate.ToArray());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    _model.CompanyProfileId = Convert.ToInt32(ds.Tables[0].Rows[0]["CompanyProfileId"]);
                    _model.FollowUpDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["FollowUpDate"]);
                    _model.FollowUpDateDisplay = _model.FollowUpDate.ToString("dd/MMM/yyyy");
                    _model.Comments = ds.Tables[0].Rows[0]["Comments"].ToString();
                    _model.SelectStatus = ds.Tables[0].Rows[0]["SelectStatus"].ToString();
                    _model.CompanyId = companyId;
                }
                else
                {
                    _model.CompanyProfileId = ProfileId;
                    _model.CompanyId = companyId;
                    _model.Comments = "";
                }
                List<CompanyProfilesFollowUpDump> _dumpArray = new List<CompanyProfilesFollowUpDump>();
                if (ds.Tables[1].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        CompanyProfilesFollowUpDump _dump = new CompanyProfilesFollowUpDump();
                        _dump.FollowUpDate = Convert.ToDateTime(ds.Tables[1].Rows[i]["FollowUpDate"]);
                        _dump.FollowUpDateDisplay = _dump.FollowUpDate.ToString("dd/MMM/yyyy");
                        _dump.Comments = ds.Tables[1].Rows[i]["Comments"].ToString();
                        _dumpArray.Add(_dump);
                    }
                }
                _model.followUpDump = _dumpArray;

                return _model;
            }
            catch (Exception ex)
            {
                //MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                //string methodName = method.Name;
                //string className = method.ReflectedType.Name;
                //sendErrorMail(ex, methodName, className);
                return _model;
            }
        }

    }
}