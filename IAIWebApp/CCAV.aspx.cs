using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAIWebApp.DataHelpers;
using IAIWebApp.Models;

namespace IAIWebApp
{
    public partial class CCAV : System.Web.UI.Page
    {
        CandidateDataHelper _candidateDataHelper = new CandidateDataHelper();
        protected void Page_Load(object sender, EventArgs e)
        {
            order_id.Value = Request.QueryString["orderId"].ToString();
            string userId = Request.QueryString["userId"].ToString();
            List<CandidateModel> _candidatelist = _candidateDataHelper.GetCandidateProfile(Convert.ToInt32(userId));
            billing_name.Value = _candidatelist[0].CandidateName;
            billing_email.Value = _candidatelist[0].Email;
            billing_tel.Value = _candidatelist[0].Mobile;
        }
    }
}