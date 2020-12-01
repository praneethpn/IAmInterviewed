using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections.Specialized;
using CCA.Util;
using IAIWebApp.DataHelpers;
using IAIWebApp.Models;
using System.Net.Mail;

namespace IAIWebApp.Controllers
{
    public class ccavResponseController : Controller
    {
        CandidateDataHelper _candidateDataHelper = new CandidateDataHelper();
        BaseDataHelper _baseHelper = new BaseDataHelper();
        // GET: ccavResponse
        public ActionResult Index()
        {
            string workingKey = "BFA49CBAF6917F14495037E924FF0CEE";//put in the 32bit alpha numeric key in the quotes provided here
            CCACrypto ccaCrypto = new CCACrypto();
            string encResponse = ccaCrypto.Decrypt(Request.Form["encResp"], workingKey);
            NameValueCollection Params = new NameValueCollection();
            string[] segments = encResponse.Split('&');
            foreach (string seg in segments)
            {
                string[] parts = seg.Split('=');
                if (parts.Length > 0)
                {
                    string Key = parts[0].Trim();
                    string Value = parts[1].Trim();
                    Params.Add(Key, Value);
                }
            }
            string Orderid = "";
            string trackingid = "";
            string bankreferenceid = "";
            string orderstatus = "";
            string failuremessage = "";
            string paymentmode = "";
            string cardname = "";
            string statuscode = "";
            string statusmessage = "";
            for (int i = 0; i < Params.Count; i++)
            {
                if (Params.Keys[i] == "order_id")
                {
                    Orderid = Params[i];
                }
                if (Params.Keys[i] == "tracking_id")
                {
                    trackingid = Params[i];
                }
                if (Params.Keys[i] == "bank_ref_no")
                {
                    bankreferenceid = Params[i];
                }
                if (Params.Keys[i] == "order_status")
                {
                    orderstatus = Params[i];
                }
                if (Params.Keys[i] == "failure_message")
                {
                    failuremessage = Params[i];
                }
                if (Params.Keys[i] == "payment_mode")
                {
                    paymentmode = Params[i];
                }
                if (Params.Keys[i] == "card_name")
                {
                    cardname = Params[i];
                }
                if (Params.Keys[i] == "status_code")
                {
                    statuscode = Params[i];
                }
                if (Params.Keys[i] == "status_message")
                {
                    statusmessage = Params[i];
                }
                //Response.Write(Params.Keys[i] + " = " + Params[i] + "<br>");
            }
            ViewBag.orderStatus = orderstatus;
            ViewBag.trackingId = trackingid;
            ViewBag.orderId = Orderid;
            if (orderstatus == "Success")
            {
                _candidateDataHelper.UpdatePayment(Orderid, trackingid, bankreferenceid, orderstatus, failuremessage, paymentmode, cardname, statuscode, statusmessage);
                List<CandidateModel> details = _candidateDataHelper.ScheduleInterviewAfterPayment(Orderid);
                List<CandidateModel> _candidatelistCandidate = _candidateDataHelper.GetCandidateProfile(details[0].candidateid);
                string mainTopics = _candidatelistCandidate[0].PrimarySkillName + "( " + _candidatelistCandidate[0].SecondarySkill1Name + ", " + _candidatelistCandidate[0].SecondarySkill2Name +
                        ", " + _candidatelistCandidate[0].SecondarySkill3Name + ", " + _candidatelistCandidate[0].SecondarySkill4Name + ", " + _candidatelistCandidate[0].SecondarySkill5Name + " )";
                ViewBag.successMessage = "Your Payment has been succesful and Interview has been scheduled."
                                    + "Reference No : " + trackingid + ", please note this reference number.";
                if (details.Count > 0)
                {
                    string URL = "<a href='http://www.iaminterviewed.com/'>www.iaminterviewed.com </a>";
                    string candidateUniqueId = details[0].candidateid + details[0].CandidateUniqueId;
                    string InterviewURL = "<a href='http://www.iaminterviewed.com/Interview.html?username=" + candidateUniqueId + "'>Join Interview </a>";
                    string body = "Hi " + details[0].InterviewerName + ",<br /><br />Please find the new  interview scheduled for you,<br /><br />"

                                        + "<strong>Name:</strong>  " + details[0].CandidateName + "<br />"
                                        + "<strong>Date and Time:</strong>  " + details[0].date.ToString("dd-MMM-yyyy") + ", " + details[0].TimeSlotText + "<br />"
                                        + "Interview Topics: " + mainTopics + "<br /><br />"
                                        + "Please Click Here to Confirm: " + URL + "<br /><br />"
                                        + "Have a nice day...<br /><br />"
                                        + "Thanks,<br />"
                                        + "Team IAmInterviewed";
                    _baseHelper.SendMail("Interview Schedule", body, "schedule@iaminterviewed.com", details[0].InterviewerEmail, true);
                    string callMessage = details[0].InterviewType == "Audio" ? "You will get Phone call, Please attend to the call." : "You will receive ZOOM invite for the Interview.";
                    string bodyCandidate = "Hi " + details[0].CandidateName + ",<br /><br />Please find your interview scheduled,<br /><br />"
                                            + "<strong>Interviewer:</strong>  " + details[0].InterviewerName + "<br />"
                                            + "<strong>Date and Time:</strong>  " + details[0].date.ToString("dd-MMM-yyyy") + ", " + details[0].TimeSlotText + "<br />"
                                            + "Interview Topics: " + mainTopics + "<br /><br />"
                                            + "Please login here to see details: " + URL + "<br /><br />"
                                            + callMessage + "<br /><br />"
                                            //+ "Please Click Here to Take Interview: " + InterviewURL + ". Or you may get Phone call also.<br /><br />"
                                            + "Have a nice day...<br /><br />"
                                            + "Thanks,<br />"
                                            + "Team IAmInterviewed";
                    _baseHelper.SendMail("Interview Schedule", bodyCandidate, "schedule@iaminterviewed.com", details[0].Email, true);
                }
            }
            else
            {
                _candidateDataHelper.UpdatePayment(Orderid, trackingid, bankreferenceid, orderstatus, failuremessage, paymentmode, cardname, statuscode, statusmessage);
                _candidateDataHelper.UpdateInterviewerAfterPayment(Orderid);
                ViewBag.errorMessage = "Your Payment has been failed. Please find the details below"
                                    + "Reference No : " + trackingid + ", please note this reference number."
                                    + "Failure Message: " + failuremessage + "";
            }            
            return View();
        }
    }
}