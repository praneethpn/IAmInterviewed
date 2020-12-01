using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Reflection;
using IAIWebApp.Models;

namespace IAIWebApp.DataHelpers
{
    public class AccountDataHelper : BaseDataHelper
    {
        public DataRow IsUserExists(string Companyname)
        {
            DataRow userdata = null;
            DataTable usercollection = null;
            try
            {
                List<SqlParameter> pars = new List<SqlParameter>();
                pars.Add(GetSqlParameter("@Email", SqlDbType.VarChar, Companyname));
                usercollection = SqlHelper.ExecuteDataset(CS, SP, "Proc_Email_Exists", pars.ToArray()).Tables[0];
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

        public void SaveUser(string name, string email, int skill, string password, string Type, int Secondaryskill, string MobileNo, string Country)
        {
            try
            {
                List<SqlParameter> pars = new List<SqlParameter>();
                pars.Add(GetSqlParameter("@Name", SqlDbType.VarChar, name));
                pars.Add(GetSqlParameter("@Email", SqlDbType.VarChar, email));
                pars.Add(GetSqlParameter("@Skill", SqlDbType.VarChar, skill));
                pars.Add(GetSqlParameter("@Password", SqlDbType.VarChar, password));
                pars.Add(GetSqlParameter("@Type", SqlDbType.VarChar, Type));
                pars.Add(GetSqlParameter("@Secondaryskill", SqlDbType.VarChar, Secondaryskill));
                pars.Add(GetSqlParameter("@MobileNo", SqlDbType.VarChar, MobileNo));
                pars.Add(GetSqlParameter("@Country", SqlDbType.VarChar, Country));
                SqlHelper.ExecuteNonQuery(CS, SP, "Proc_Save_User", pars.ToArray());
            }
            catch (Exception ex)
            {
                MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                string methodName = method.Name;
                string className = method.ReflectedType.Name;
                sendErrorMail(ex, methodName, className);
            }
        }

        public void SaveInterviewer(string name, string email, int skill, string password, string Type, int Secondaryskill, string MobileNo, string Country)
        {
            try
            {
                List<SqlParameter> pars = new List<SqlParameter>();
                pars.Add(GetSqlParameter("@Name", SqlDbType.VarChar, name));
                pars.Add(GetSqlParameter("@Email", SqlDbType.VarChar, email));
                pars.Add(GetSqlParameter("@Skill", SqlDbType.VarChar, skill));
                pars.Add(GetSqlParameter("@Password", SqlDbType.VarChar, password));
                pars.Add(GetSqlParameter("@Type", SqlDbType.VarChar, Type));
                pars.Add(GetSqlParameter("@Secondaryskill", SqlDbType.VarChar, Secondaryskill));
                pars.Add(GetSqlParameter("@MobileNo", SqlDbType.VarChar, MobileNo));
                pars.Add(GetSqlParameter("@Country", SqlDbType.VarChar, Country));
                SqlHelper.ExecuteNonQuery(CS, SP, "Proc_Save_RegisteredInterviewer", pars.ToArray());
            }
            catch (Exception ex)
            {
                MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                string methodName = method.Name;
                string className = method.ReflectedType.Name;
                sendErrorMail(ex, methodName, className);
            }
        }

        public void SaveUser1(string name, string email, string skill, string password, string Type, string Secondaryskill, string Uniqid, string JobCode, string MobileNo, string UserId)
        {
            try
            {
                List<SqlParameter> pars = new List<SqlParameter>();
                pars.Add(GetSqlParameter("@Name", SqlDbType.VarChar, name));
                pars.Add(GetSqlParameter("@Email", SqlDbType.VarChar, email));
                pars.Add(GetSqlParameter("@Skill", SqlDbType.VarChar, skill));
                pars.Add(GetSqlParameter("@Password", SqlDbType.VarChar, password));
                pars.Add(GetSqlParameter("@Type", SqlDbType.VarChar, Type));
                pars.Add(GetSqlParameter("@UniqId", SqlDbType.VarChar, Uniqid));
                pars.Add(GetSqlParameter("@Secondaryskill", SqlDbType.VarChar, Secondaryskill));
                pars.Add(GetSqlParameter("@JobCode", SqlDbType.VarChar, JobCode));
                pars.Add(GetSqlParameter("@MobileNo", SqlDbType.VarChar, MobileNo));
                pars.Add(GetSqlParameter("@UserId", SqlDbType.VarChar, UserId));
                SqlHelper.ExecuteNonQuery(CS, SP, "Proc_Save_User1", pars.ToArray());
            }
            catch (Exception ex)
            {
                MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                string methodName = method.Name;
                string className = method.ReflectedType.Name;
                sendErrorMail(ex, methodName, className);
            }
        }

        public void SaveNewUser(string name, string email, string skill, string UniqId, string Jobcode, string Secondaryskill, string CompanyId, string MobileNo, string Profile)
        {
            try
            {
                List<SqlParameter> pars = new List<SqlParameter>();
                pars.Add(GetSqlParameter("@Name", SqlDbType.VarChar, name));
                pars.Add(GetSqlParameter("@Email", SqlDbType.VarChar, email));
                pars.Add(GetSqlParameter("@Skill", SqlDbType.VarChar, skill));
                pars.Add(GetSqlParameter("@UniqId", SqlDbType.VarChar, UniqId));
                pars.Add(GetSqlParameter("@Jobcode", SqlDbType.VarChar, Jobcode));
                pars.Add(GetSqlParameter("@Secondaryskill", SqlDbType.VarChar, Secondaryskill));
                pars.Add(GetSqlParameter("@CompanyId", SqlDbType.VarChar, CompanyId));
                pars.Add(GetSqlParameter("@MobileNo", SqlDbType.VarChar, MobileNo));
                pars.Add(GetSqlParameter("@Profile", SqlDbType.VarChar, Profile));
                SqlHelper.ExecuteNonQuery(CS, SP, "Proc_Save_UserNew", pars.ToArray());
            }
            catch (Exception ex)
            {
                MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                string methodName = method.Name;
                string className = method.ReflectedType.Name;
                sendErrorMail(ex, methodName, className);
            }
        }

        public void SaveCompanyAddedUser(string name, string email, string skill, string password, string Type, string Secondaryskill, string Uniqid, string JobCode, string MobileNo, string UserId, string Profile, string Vendor)
        {
            try
            {
                List<SqlParameter> pars = new List<SqlParameter>();
                pars.Add(GetSqlParameter("@Name", SqlDbType.VarChar, name));
                pars.Add(GetSqlParameter("@Email", SqlDbType.VarChar, email));
                pars.Add(GetSqlParameter("@Skill", SqlDbType.VarChar, skill));
                pars.Add(GetSqlParameter("@Password", SqlDbType.VarChar, password));
                pars.Add(GetSqlParameter("@Type", SqlDbType.VarChar, Type));
                pars.Add(GetSqlParameter("@UniqId", SqlDbType.VarChar, Uniqid));
                pars.Add(GetSqlParameter("@Secondaryskill", SqlDbType.VarChar, Secondaryskill));
                pars.Add(GetSqlParameter("@JobCode", SqlDbType.VarChar, JobCode));
                pars.Add(GetSqlParameter("@MobileNo", SqlDbType.VarChar, MobileNo));
                pars.Add(GetSqlParameter("@CompanyId", SqlDbType.VarChar, UserId));
                pars.Add(GetSqlParameter("@Profile", SqlDbType.VarChar, Profile));
                pars.Add(GetSqlParameter("@Vendor", SqlDbType.VarChar, Vendor));
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

        public User AunthenticateUser(string Username, string Password)
        {
            SqlDataReader reader = null;
            User user = new User();
            SqlCommand command = BaseDataHelper.sqlCommand;
            try
            {
                command.CommandText = "Proc_AunthenticateUser";
                command.Parameters.AddWithValue("@UserName", Username);
                command.Parameters.AddWithValue("@Password", Password);
                command.Connection.Open();
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    user.UserID = reader["UserId"] == null ? string.Empty : reader["UserID"].ToString();
                    user.Username = reader["name"] == null ? string.Empty : reader["name"].ToString();
                    user.Password = reader["password"] == null ? string.Empty : reader["password"].ToString();
                    user.RoleName = reader["Type"] == null ? string.Empty : reader["Type"].ToString();
                    user.EmailID = reader["Email"] == null ? string.Empty : reader["Email"].ToString();
                    user.LoginCount = reader["LoginCount"] == null ? string.Empty : reader["LoginCount"].ToString();
                    user.SkillId = reader["Skill"] == null ? string.Empty : reader["Skill"].ToString();
                    user.SecondarySkill = reader["SecondarySkill"] == null ? string.Empty : reader["SecondarySkill"].ToString();
                    user.SubUser = reader["SubUserId"] == null ? string.Empty : reader["SubUserId"].ToString();
                    user.CompanyType = reader["CompanyType"] == null ? string.Empty : reader["CompanyType"].ToString();
                    user.UniqueId = reader["UniqueId"] == null ? string.Empty : reader["UniqueId"].ToString();
                    user.CompanyId = reader["CompanyId"] == null ? string.Empty : reader["CompanyId"].ToString();
                    user.Country = reader["Country"] == null ? string.Empty : reader["Country"].ToString();
                }
            }
            catch (Exception exe)
            {
                //MailingHelper mailingHelper = new MailingHelper();
                //mailingHelper.SendErrorMail("[Puma]-Error", exe.StackTrace + "[] Message - " + exe.Message);


            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                    reader.Dispose();
                    reader = null;
                }
                if (command.Connection != null)
                {
                    command.Connection.Close();
                    command.Connection.Dispose();
                    command.Connection = null;
                }
                command = null;
            }
            return user;
        }

        public void UpdateUserPassword(string email, string password)
        {
            try
            {
                List<SqlParameter> pars = new List<SqlParameter>();

                pars.Add(GetSqlParameter("@Email", SqlDbType.VarChar, email));
                pars.Add(GetSqlParameter("@Password", SqlDbType.VarChar, password));
                SqlHelper.ExecuteNonQuery(CS, SP, "Proc_Update_UserPassword", pars.ToArray());
            }
            catch (Exception ex)
            {
                MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                string methodName = method.Name;
                string className = method.ReflectedType.Name;
                sendErrorMail(ex, methodName, className);
            }

        }

        public User GetUserPassword(string Email)
        {
            User _user = new User();
            try
            {
                List<SqlParameter> pars = new List<SqlParameter>();
                pars.Add(GetSqlParameter("@Email", SqlDbType.VarChar, Email));
                DataSet dstData = new DataSet();
                string[] tables = new string[] { "Users" };
                SqlHelper.FillDataset(CS, SP, "Proc_Get_Password", dstData, tables, pars.ToArray());
                if (dstData.Tables[0].Rows.Count > 0)
                {
                    _user.Password = dstData.Tables[0].Rows[0]["password"].ToString();
                    _user.RoleName = dstData.Tables[0].Rows[0]["Type"].ToString();
                    _user.CompanyId = dstData.Tables[0].Rows[0]["CompanyId"].ToString();
                }
                //for (int i = 0; i < dstData.Tables[0].Rows.Count; i++)
                //{
                //    Password = dstData.Tables[0].Rows[i]["password"].ToString();
                //}
                return _user;
            }
            catch (Exception ex)
            {
                MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                string methodName = method.Name;
                string className = method.ReflectedType.Name;
                sendErrorMail(ex, methodName, className);
                return _user;
            }

        }

        public List<User> SaveSubUser(User _model)
        {
            List<User> nwmd = new List<User>();
            List<SqlParameter> pars = new List<SqlParameter>();
            try
            {
                pars.Add(GetSqlParameter("@Name", SqlDbType.VarChar, _model.Username));
                pars.Add(GetSqlParameter("@Email", SqlDbType.VarChar, _model.EmailID));
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

        public void SaveUserBehaviorActivity(string UserId, string IpAddress, string Page, string SessionID, string UserActivity, string Remarks)
        {
            try
            {
                List<SqlParameter> pars = new List<SqlParameter>();
                pars.Add(GetSqlParameter("@UserId", SqlDbType.VarChar, UserId));
                pars.Add(GetSqlParameter("@IpAddress", SqlDbType.VarChar, IpAddress));
                pars.Add(GetSqlParameter("@Page", SqlDbType.VarChar, Page));
                pars.Add(GetSqlParameter("@SessionID", SqlDbType.VarChar, SessionID));
                pars.Add(GetSqlParameter("@UserActivity", SqlDbType.VarChar, UserActivity));
                pars.Add(GetSqlParameter("@Remarks", SqlDbType.VarChar, Remarks));
                SqlHelper.ExecuteNonQuery(CS, SP, "Proc_Save_UserBehavior", pars.ToArray());
            }
            catch (Exception ex)
            {
                MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                string methodName = method.Name;
                string className = method.ReflectedType.Name;
                sendErrorMail(ex, methodName, className);
            }
        }

        public DataRow IsUserRefered(string Companyname)
        {
            DataRow userdata = null;
            DataTable usercollection = null;
            try
            {
                List<SqlParameter> pars = new List<SqlParameter>();
                pars.Add(GetSqlParameter("@Email", SqlDbType.VarChar, Companyname));
                usercollection = SqlHelper.ExecuteDataset(CS, SP, "Proc_Email_ExistsReferFriend", pars.ToArray()).Tables[0];
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

        public List<CandidateModel> GetTop10Ratings()
        {
            List<CandidateModel> nwmd = new List<CandidateModel>();
            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(GetSqlParameter("@UserId", SqlDbType.Int, 0));
            try
            {
                DataSet ds = new DataSet();
                string[] tables = new string[] { "Ratings" };
                SqlHelper.FillDataset(CS, SP, "Proc_Admin_Get_Top10Ratings", ds, tables, pars.ToArray());
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    CandidateModel _model = new CandidateModel();
                    _model.ScheduleId = Convert.ToInt32(ds.Tables[0].Rows[i]["ScheduleId"]);
                    _model.CandidateName = ds.Tables[0].Rows[i]["Name"].ToString();
                    _model.CandidateName = _model.CandidateName.Split(' ', '\t')[0];
                    _model.TotalRating = ds.Tables[0].Rows[i]["OverallRating"].ToString();
                    _model.SecondarySkill1Name = ds.Tables[0].Rows[i]["SecondarySkill1"].ToString();
                    _model.SecondarySkill2Name = ds.Tables[0].Rows[i]["SecondarySkill2"].ToString();
                    _model.SecondarySkill3Name = ds.Tables[0].Rows[i]["SecondarySkill3"].ToString();
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
    }
}