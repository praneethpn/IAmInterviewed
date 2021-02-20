using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAIWebApp.Models
{
    public class CandidateModel
    {
        public int candidateid { get; set; }
        public string CandidateUniqueId { get; set; }
        public int PrimarySkill { get; set; }
        public string PrimarySkillName { get; set; }
        public string PrimarySkill1 { get; set; }
        public int SecondarySkill1 { get; set; }
        public int SecondarySkill2 { get; set; }
        public int SecondarySkill3 { get; set; }
        public int SecondarySkill4 { get; set; }
        public int SecondarySkill5 { get; set; }
        public string SecondarySkill1Name { get; set; }
        public string SecondarySkill2Name { get; set; }
        public string SecondarySkill3Name { get; set; }
        public string SecondarySkill4Name { get; set; }
        public string SecondarySkill5Name { get; set; }
        public string Location { get; set; }
        public string CurrentPay { get; set; }
        public string ExpectedPay { get; set; }
        public string Experience { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string Resume { get; set; }
        public string Photo { get; set; }

        public DateTime date { get; set; }
        public int Interviewer { get; set; }
        public string InterviewerName { get; set; }
        public int TimeSlot { get; set; }
        public string TimeSlotText { get; set; }
        public string AcceptedByInterviewer { get; set; }

        public int ScheduleId { get; set; }
        public string SecondarySkill1Rating { get; set; }
        public string SecondarySkill2Rating { get; set; }
        public string SecondarySkill3Rating { get; set; }
        public string SecondarySkill4Rating { get; set; }
        public string SecondarySkill5Rating { get; set; }
        public string EnglishCommunication { get; set; }
        public string Attitude { get; set; }
        public string InterpersonalSkillCommunication { get; set; }
        public string InterviewerComments { get; set; }
        public string AudioFile { get; set; }
        public string VideoFile { get; set; }
        public string TotalRating { get; set; }
        public string RatingAcceptance { get; set; }

        public string CompanyName { get; set; }
        public string NoticePeriod { get; set; }
        public bool GapInEducation { get; set; }
        public bool GapInExperience { get; set; }
        public bool readytochange { get; set; }
        public DateTime AppliedDate { get; set; }
        public int CompanyId { get; set; }
        public int DesignationId { get; set; }
        public string Designation { get; set; }
        public int Applicationid { get; set; }
        public string Email { get; set; }
        public string CandidateName { get; set; }
        public string uniquenumber { get; set; }
        public string InterviewerEmail { get; set; }
        public int PromoCode { get; set; }

        public string AdditionalSkills { get; set; }
        public string ScreenSelect { get; set; }
        public string SelectStetus { get; set; }
        public string JobCode { get; set; }
        public string Status { get; set; }
        public DateTime ViewedDate { get; set; }
        public string InterviewDate { get; set; }
        public string StatusUpdateRemarks { get; set; }
        public DateTime RegisteredDate { get; set; }
        public DateTime? ScheduledDate { get; set; }
        //public string JobCode { get; set; }
        public bool RestrictEmployerToViewProfile { get; set; }
        public string GrantAccess { get; set; }
        public int ReqId { get; set; }
        public string InterviewType { get; set; }
        public string CompanySchedule { get; set; }
        public string VendorId { get; set; }
        public string ProfileCompletionStatus { get; set; }
        public string CandidateNavigationStatus { get; set; }
        public string RatingExpiry { get; set; }
        public string DisplayScheduleDate { get; set; }
        public DateTime FollowUpDate { get; set; }
        public string FollowUpDateDisplay { get; set; }
        public string CompanyProfileId { get; set; }
    }

    public class CandidateProfileModel
    {
        public int CandidateId { get; set; }
        public string PrimarySkill { get; set; }
        public string SecondarySkill1 { get; set; }
        public string SecondarySkill2 { get; set; }
        public string SecondarySkill3 { get; set; }
        public string SecondarySkill4 { get; set; }
        public string SecondarySkill5 { get; set; }
        public string UniqueId { get; set; }
        public string MobileNo { get; set; }
        public string AdditionalSkills { get; set; }
        public string Address { get; set; }
        public bool GapInEducation { get; set; }
        public bool GapInExperience { get; set; }
        public bool RestrictEmployerToViewProfile { get; set; }
        public string NoticePeriod { get; set; }
        public string ExpectedPay { get; set; }
        public string CurrentPay { get; set; }
        public string Experience { get; set; }
        public string Location { get; set; }
        public string ScreenSelect { get; set; }
        public string SelectStatus { get; set; }
        public string Resume { get; set; }
    }

    public class CandidateScheduleInterivewModel
    {
        public DateTime ScheduleDate { get; set; }
        public string Interviewer { get; set; }
        public string TimeSlot { get; set; }
        public int CandidateId { get; set; }
        public string InterviewType { get; set; }
        public string UniqueNumber { get; set; }
    }
    public class ScheduleInterviewResponse
    {
        public string ResponseMessage { get; set; }
        public string PromoCode { get; set; }
        public string OrderId { get; set; }
    }

    public class DesignationModel
    {
        public DesignationModel()
        { }

        public DesignationModel(string id, string designation)
        {
            this.NewDesignationId = id;
            this.Designation = designation;
        }
        public int DesignationId { get; set; }
        public string Designation { get; set; }
        public string Description { get; set; }

        public string NewDesignationId { get; set; }
        public int RowNo { get; set; }
        public int TotalCount { get; set; }
        public int UserId { get; set; }

    }

    public class SaveFavoriteCompanyModel
    {
        public string Company { get; set; }
        public string CandidateId { get; set; }
        public string Designation { get; set; }
        public string Applicationid { get; set; }
    }

    public class CompanyRoleModel
    {
        List<CompanyModel> _requirements = new List<CompanyModel>();
        List<CompanyModel> _roles = new List<CompanyModel>();
    }
}