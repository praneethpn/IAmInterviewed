using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.ApplicationBlocks.Data;
using IAIWebApp.Models;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;

namespace IAIWebApp.DataHelpers
{
    public class SkillDataHelper : BaseDataHelper
    {
        public List<SkillModel> GetSkills(int jtStartIndex, int jtPageSize, string jtSorting)
        {
            List<SkillModel> nwmd = new List<SkillModel>();
            List<SqlParameter> pars = new List<SqlParameter>();
            try
            {
                pars.Add(GetSqlParameter("@FirstRow", SqlDbType.Int, jtStartIndex));
                pars.Add(GetSqlParameter("@LastRow", SqlDbType.Int, jtPageSize));
                pars.Add(GetSqlParameter("@OrderBy", SqlDbType.VarChar, jtSorting));
                DataSet ds = new DataSet();
                string[] tables = new string[] { "Skill" };
                SqlHelper.FillDataset(CS, SP, "Proc_Select_Skills", ds, tables, pars.ToArray());
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    SkillModel _model = new SkillModel();
                    _model.SkillId = Convert.ToInt32(ds.Tables[0].Rows[i]["SkillId"]);
                    _model.SkillName = ds.Tables[0].Rows[i]["SkillName"].ToString();
                    _model.Description = ds.Tables[0].Rows[i]["Description"].ToString();
                    _model.RowNo = Convert.ToInt32(ds.Tables[0].Rows[i]["RowNo"]);
                    _model.TotalCount = Convert.ToInt32(ds.Tables[0].Rows[i]["TotalCount"]);
                    nwmd.Add(_model);
                }
                return nwmd;
            }
            catch (Exception ex)
            {

                return nwmd;
            }
        }

        public List<SkillModel> GetSecondarySkills(string SkillId, int jtStartIndex, int jtPageSize, string jtSorting)
        {
            List<SkillModel> nwmd = new List<SkillModel>();
            List<SqlParameter> pars = new List<SqlParameter>();
            try
            {
                pars.Add(GetSqlParameter("@FirstRow", SqlDbType.Int, jtStartIndex));
                pars.Add(GetSqlParameter("@LastRow", SqlDbType.Int, jtPageSize));
                pars.Add(GetSqlParameter("@OrderBy", SqlDbType.VarChar, jtSorting));
                pars.Add(GetSqlParameter("@SkillFilter", SqlDbType.VarChar, SkillId));
                DataSet ds = new DataSet();
                string[] tables = new string[] { "Skill" };
                SqlHelper.FillDataset(CS, SP, "Proc_Select_SecondarySkillData", ds, tables, pars.ToArray());
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    SkillModel _model = new SkillModel();
                    _model.SkillId = Convert.ToInt32(ds.Tables[0].Rows[i]["SecondarySkillId"]);
                    _model.NewSkillId = ds.Tables[0].Rows[i]["SkillId"].ToString();
                    _model.SecondarySkillId = Convert.ToInt32(ds.Tables[0].Rows[i]["SkillId"]);
                    _model.SkillName = ds.Tables[0].Rows[i]["SecondarySkillName"].ToString();
                    _model.Description = ds.Tables[0].Rows[i]["Description"].ToString();
                    _model.PrimarySkillName = ds.Tables[0].Rows[i]["PrimarySkillName"].ToString();
                    _model.RowNo = Convert.ToInt32(ds.Tables[0].Rows[i]["RowNo"]);
                    _model.TotalCount = Convert.ToInt32(ds.Tables[0].Rows[i]["TotalCount"]);
                    nwmd.Add(_model);
                }
                return nwmd;
            }
            catch (Exception ex)
            {

                return nwmd;
            }
        }

        public List<CandidateModel> GetCityName(string Country)
        {
            List<CandidateModel> nwmd = new List<CandidateModel>();
            List<SqlParameter> pars = new List<SqlParameter>();
            try
            {
                if (string.IsNullOrEmpty(Country))
                {
                    Country = null;
                }
                pars.Add(GetSqlParameter("@Country", SqlDbType.VarChar, Country));
                DataSet ds = new DataSet();
                string[] tables = new string[] { "Cities" };
                SqlHelper.FillDataset(CS, SP, "ProGetCities", ds, tables, pars.ToArray());
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    CandidateModel _model = new CandidateModel();
                    _model.Location = ds.Tables[0].Rows[i]["CityName"].ToString();
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