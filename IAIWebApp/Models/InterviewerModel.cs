using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAIWebApp.Models
{
    public class InterviewerModel
    {
        public List<TimeSlotModel> timeSlotList { get; set; }
        public int DailyScheduleId { get; set; }
        public string DailyScheduleTime { get; set; }

        public int UserId { get; set; }
        public int InterviewerId { get; set; }
        public string InterviewerName { get; set; }
        public DateTime ScheduleDate { get; set; }
        public string date { get; set; }
        public int AvailableTimeId { get; set; }
        public string AvailableTime { get; set; }
        public int TotalCount { get; set; }
        public string IsConfirmed { get; set; }

        //For Interview Schedule
        public int ScheduleId { get; set; }
        public string CandidateName { get; set; }
        public int CandidateId { get; set; }
        public DateTime InterviewDate { get; set; }
        public string TimeSlot { get; set; }
        public string PrimarySkill { get; set; }
        public string SecondarySkill1 { get; set; }
        public string SecondarySkill2 { get; set; }
        public string SecondarySkill3 { get; set; }
        public string SecondarySkill4 { get; set; }
        public string SecondarySkill5 { get; set; }
        public string SecondarySkill1Rating { get; set; }
        public string SecondarySkill2Rating { get; set; }
        public string SecondarySkill3Rating { get; set; }
        public string SecondarySkill4Rating { get; set; }
        public string SecondarySkill5Rating { get; set; }
        public string EnglishCommunication { get; set; }
        public string Attitude { get; set; }
        public string InterpersonalSkillCommunication { get; set; }
        public string InterviewerRemarks { get; set; }
        public string AudioFile { get; set; }
        public string VideoFile { get; set; }
        public string MobileNumber { get; set; }
        public string Resume { get; set; }
        public string Email { get; set; }
        public string RatingAccepted { get; set; }
        public string TotalRating { get; set; }
        public string CurrentPay { get; set; }
        public string ExpectedPay { get; set; }

        public string SecondarySkill1Remarks { get; set; }
        public string SecondarySkill2Remarks { get; set; }
        public string SecondarySkill3Remarks { get; set; }
        public string SecondarySkill4Remarks { get; set; }
        public string SecondarySkill5Remarks { get; set; }
        public string EnglishCommunicationRemarks { get; set; }
        public string AttitudeRemarks { get; set; }
        public string InterpersonalSkillCommunicationRemarks { get; set; }
        public string CaMobileNumber { get; set; }
        public int PrimarySkillID { get; set; }
        public string Experience { get; set; }
        public string Status { get; set; }
        public string SoftSkillRating { get; set; }
        public string KeyResponsibilities { get; set; }
        public string CandidateUniqueId { get; set; }
        public string InterviewType { get; set; }
        public string DisplayDate { get; set; }
        public string CompanySchedule { get; set; }
        public string subSkill1 { get; set; }
        public int subSkill1Rating { get; set; }
        public string subSkill2 { get; set; }
        public int subSkill2Rating { get; set; }
        public string subSkill3 { get; set; }
        public int subSkill3Rating { get; set; }
        public string subSkill4 { get; set; }
        public int subSkill4Rating { get; set; }
        public string subSkill5 { get; set; }
        public int subSkill5Rating { get; set; }
        public string subSkill6 { get; set; }
        public int subSkill6Rating { get; set; }
        public string subSkill7 { get; set; }
        public int subSkill7Rating { get; set; }
        public string subSkill8 { get; set; }
        public int subSkill8Rating { get; set; }
        public string subSkill9 { get; set; }
        public int subSkill9Rating { get; set; }
        public string subSkill10 { get; set; }
        public int subSkill10Rating { get; set; }
    }

    public class InterviewerRatingModel
    {
        public int ScheduleId { get; set; }
        public string SecondarySkill1Rating { get; set; }
        public string SecondarySkill2Rating { get; set; }
        public string SecondarySkill3Rating { get; set; }
        public string SecondarySkill4Rating { get; set; }
        public string SecondarySkill5Rating { get; set; }
        public string TotalRating { get; set; }
        public string EnglishCommunication { get; set; }
        public string Attitude { get; set; }
        public string InterpersonalSkillCommunication { get; set; }
        public string InterviewerRemarks { get; set; }
        public string AudioFile { get; set; }
        public string VideoFile { get; set; }
        public string SecondarySkill1Remarks { get; set; }
        public string SecondarySkill2Remarks { get; set; }
        public string SecondarySkill3Remarks { get; set; }
        public string SecondarySkill4Remarks { get; set; }
        public string SecondarySkill5Remarks { get; set; }
        public string EnglishCommunicationRemarks { get; set; }
        public string AttitudeRemarks { get; set; }
        public string InterpersonalSkillCommunicationRemarks { get; set; }
        public string CandidateEmail { get; set; }
        public string InterviewDateTime { get; set; }
        public int InterviewerId { get; set; }
        public int CandidateId { get; set; }
        public string CandidateName { get; set; }
        public string subSkill1 { get; set; }
        public int subSkill1Rating { get; set; }
        public string subSkill2 { get; set; }
        public int subSkill2Rating { get; set; }
        public string subSkill3 { get; set; }
        public int subSkill3Rating { get; set; }
        public string subSkill4 { get; set; }
        public int subSkill4Rating { get; set; }
        public string subSkill5 { get; set; }
        public int subSkill5Rating { get; set; }
        public string subSkill6 { get; set; }
        public int subSkill6Rating { get; set; }
        public string subSkill7 { get; set; }
        public int subSkill7Rating { get; set; }
        public string subSkill8 { get; set; }
        public int subSkill8Rating { get; set; }
        public string subSkill9 { get; set; }
        public int subSkill9Rating { get; set; }
        public string subSkill10 { get; set; }
        public int subSkill10Rating { get; set; }
    }

    public class TimeSlotModel
    {
        public int TimeSlotId { get; set; }
        public string TimeSlot { get; set; }
    }
}