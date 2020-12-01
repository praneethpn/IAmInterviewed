using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CCA.Util;

namespace IAIWebApp
{
    public partial class ccavRequestHandler : System.Web.UI.Page
    {
        CCACrypto ccaCrypto = new CCACrypto();
        string workingKey = "BFA49CBAF6917F14495037E924FF0CEE";//put in the 32bit alpha numeric key in the quotes provided here 	
        string ccaRequest = "";
        public string strEncRequest = "";
        public string strAccessCode = "AVKJ06CH04BA50JKAB";// put the access key in the quotes provided here.

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                foreach (string name in Request.Form)
                {
                    if (name != null)
                    {
                        if (!name.StartsWith("_"))
                        {
                            ccaRequest = ccaRequest + name + "=" + Request.Form[name] + "&";
                            /* Response.Write(name + "=" + Request.Form[name]);
                              Response.Write("</br>");*/
                        }
                    }
                }
                strEncRequest = ccaCrypto.Encrypt(ccaRequest, workingKey);
            }
            encRequest.Value = strEncRequest;
            access_code.Value = strAccessCode;
        }
    }
}