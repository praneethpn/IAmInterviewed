using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAIWebApp.Models
{
    public class RegisterModel
    {
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNumber { get; set; }
        public int PrimarySkill { get; set; }
        public int SecondarySkill1 { get; set; }
        public string Country { get; set; }
        public string Type { get; set; }
    }
}