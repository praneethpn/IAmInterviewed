using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAIWebApp.Models
{
    public class User
    {
        public string Username { get; set; }
        public string UserID { get; set; }
        public string SkillId { get; set; }
        public string SecondarySkill { get; set; }
        public string RoleName { get; set; }
        public string Password { get; set; }
        public string CreatedBy { get; set; }

        public string EmailID { get; set; }

        public string PhoneNumber { get; set; }
        //public string Location { get; set; }
        //public string city { get; set; }

        //public int RoleIndex { get; set; }
        //public string CompanyId { get; set; }
        public string LoginCount { get; set; }
        public int Skill { get; set; }
        public string SkillType { get; set; }

        // for sub user skill
        public int SubUserId { get; set; }
        public string SkillName { get; set; }
        public int RowNo { get; set; }
        public int TotalCount { get; set; }
        public string SubUser { get; set; }
        public string CompanyType { get; set; }
        public string UniqueId { get; set; }
        public string UserType { get; set; }
        public string CompanyId { get; set; }
        public string Country { get; set; }
    }
    public class Login
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}