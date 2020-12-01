using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAIWebApp.Models
{
    public class SkillModel
    {
        public SkillModel()
        { }

        public SkillModel(string id, string skill)
        {
            this.NewSkillId = id;
            this.SkillName = skill;
        }
        public SkillModel(int SkillId, string SKillname)
        {
            this.SecondarySkillId = SkillId;
            this.SkillName = SKillname;
        }

        public int SkillId { get; set; }
        public string SkillName { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }

        public int RowNo { get; set; }
        public int TotalCount { get; set; }

        public int SecondarySkillId { get; set; }
        public string NewSkillId { get; set; }
        public string PrimarySkillName { get; set; }
    }
}