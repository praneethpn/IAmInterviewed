using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IAIWebApp.Models;
using IAIWebApp.DataHelpers;

namespace IAIWebApp.Controllers
{
    public class SkillController : Controller
    {
        SkillDataHelper _skills = new SkillDataHelper();
        // GET: Skill
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetPrimarySkills()
        {

            try
            {
                List<SkillModel> slills = _skills.GetSkills(0, 100000, "SkillId ASC");

                // Now if our password was enctypted or hashed we would have done the
                // same operation on the user entered password here, But for now
                // since the password is in plain text lets just authenticate directly
                return Json(new { data = slills, Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
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
        public ActionResult GetSecondarySkills(string PrimarySkill)
        {

            try
            {
                string SkillFilter = PrimarySkill.ToString();
                if (PrimarySkill == "0")
                {
                    SkillFilter = null;
                }
                List<SkillModel> slills = _skills.GetSecondarySkills(SkillFilter, 0, 100000, "SkillId ASC");

                // Now if our password was enctypted or hashed we would have done the
                // same operation on the user entered password here, But for now
                // since the password is in plain text lets just authenticate directly
                return Json(new { data = slills, Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
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
        public ActionResult GetCities(string Country)
        {

            try
            {                
                if (string.IsNullOrEmpty(Country))
                {
                    Country = null;
                }
                List<CandidateModel> _cities = _skills.GetCityName(Country);

                // Now if our password was enctypted or hashed we would have done the
                // same operation on the user entered password here, But for now
                // since the password is in plain text lets just authenticate directly
                return Json(new { data = _cities, Success = true, errorMessage = "" }, JsonRequestBehavior.AllowGet);
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
    }
}