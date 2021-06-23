using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAIWebApp.Models
{
    public class CompanyModel
    {
        public int DetailsId { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string Address { get; set; }
        public string Logo { get; set; }
        public int ReqId { get; set; }
        public string JobCode { get; set; }
        public string JobTitleId { get; set; }
        public string JobTitle { get; set; }
        public string Location { get; set; }
        public int MinExp { get; set; }
        public int MaxExp { get; set; }
        public int HighestPay { get; set; }
        public int PrimarySkill { get; set; }
        public int SecSkill1 { get; set; }
        public int SecSkill2 { get; set; }
        public int SecSkill3 { get; set; }
        public int SecSkill4 { get; set; }
        public int SecSkill5 { get; set; }
        public string JobDesc { get; set; }
        public DateTime JobPostingSDate { get; set; }
        public DateTime JobPostingEDate { get; set; }
        public string Remarks { get; set; }
        public int UserId { get; set; }
        public int RowNo { get; set; }
        public int TotalCount { get; set; }
        public string JobReqCustom { get; set; }


        public string ScheduleId { get; set; }
        public string ScheduleDate { get; set; }
        public string CandidateName { get; set; }
        public string ScheduleTime { get; set; }
        public string SecSkillName1 { get; set; }
        public string SecSKillName2 { get; set; }
        public string SecSkillName3 { get; set; }
        public string SecSkillName4 { get; set; }
        public string SecSkillName5 { get; set; }
        public string PrimarySKillName { get; set; }
        public string SecSkill1Rating { get; set; }
        public string SecSkill2Rating { get; set; }
        public string SecSkill3Rating { get; set; }
        public string SecSkill4Rating { get; set; }
        public string SecSkill5Rating { get; set; }
        public string OveralRating { get; set; }
        public string SecSkillList { get; set; }
        public string Experience { get; set; }
        public string Interviewer { get; set; }
        public int CandidateId { get; set; }
        public string EnglishCommunication { get; set; }
        public string Attitude { get; set; }
        public string InterpersonalSkillCommunication { get; set; }
        public string InterviewerRemarks { get; set; }
        public string AudioFile { get; set; }
        public string VideoFile { get; set; }
        public string MobileNumber { get; set; }
        public string Resume { get; set; }
        public string Email { get; set; }
        public string CurrentPay { get; set; }
        public string ExpectedPay { get; set; }
        public string SoftSkillRating { get; set; }

        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int interviewerid { get; set; }
        public string skill1comments { get; set; }
        public string skill2comments { get; set; }
        public string skill3comments { get; set; }
        public string skill4comments { get; set; }
        public string skill5comments { get; set; }
        public string EngComments { get; set; }
        public string Attitudecomments { get; set; }
        public string InterComments { get; set; }
        public string NoticePeriod { get; set; }
        public string GapInEducation { get; set; }
        public string GapInExperience { get; set; }
        public string UniqueId { get; set; }
        public DateTime FutureDate { get; set; }
        public string Comments { get; set; }
        public string NoOfInterviewers { get; set; }
        public string AdditionalSkills { get; set; }
        public string JobType { get; set; }
        public string SkypeId { get; set; }
        public string IntrestedLocation { get; set; }
        public string Certifications { get; set; }
        public string BuyBought { get; set; }
        public string SecSkillsList { get; set; }
        public string PostedBy { get; set; }
        public string PostedById { get; set; }
        public string Status { get; set; }
        public int AssignTo { get; set; }
        public string AssignToDisplay { get; set; }
        public string AssignToName { get; set; }
        public int PM { get; set; }
        public string PMDisplay { get; set; }
        public string PMName { get; set; }
        public int RecruiterId { get; set; }
        public string RecruiterName { get; set; }
        public string CompanyUserType { get; set; }
        public DateTime InterviewDate { get; set; }
        public string DisplayDate { get; set; }
        public string StatusRemarks { get; set; }
        public string Designation { get; set; }

        public string ScreenSelect { get; set; }
        public string SelectStetus { get; set; }
        public string InterviewDateNew { get; set; }

        //DashBoard Items

        public int InterviewsAllowed { get; set; }
        public int UsedInterviewes { get; set; }
        public int ScheduledInterviewes { get; set; }
        public int RemainingInterviewes { get; set; }
        public int InActiveSchedules { get; set; }
        public DateTime SubscriptionStartDate { get; set; }
        public DateTime SubscriptionEndDate { get; set; }

        public int TotalRequirements { get; set; }
        public int OpenRequirements { get; set; }
        public int ClosedRequirements { get; set; }
        public int OnHoldRequirements { get; set; }

        public int TotalProfiles { get; set; }
        public int InterviewedProfiles { get; set; }
        public int SelectedProfiles { get; set; }
        public int JoinedProfiles { get; set; }
        public int RejectedProfiles { get; set; }

        public int IAISelectedProfiles { get; set; }
        public int IAIJoinedProfiles { get; set; }
        public int IAIInProgressProfiles { get; set; }
        public int IAIRejectedProfiles { get; set; }

        public int InterviewedSelectedProfiles { get; set; }
        public int InterviewedJoinedProfiles { get; set; }
        public int InterviewedInProgressProfiles { get; set; }
        public int InterviewedRejectedProfiles { get; set; }

        public DateTime AppliedDate { get; set; }
        public string TrackingStatus { get; set; }

        //Skill Wise Profiles

        //public int PrimarySkillId { get; set; }
        //public string PrimarySkillName { get; set; }
        public int CompanyProfiles { get; set; }
        public int IAIProfiles { get; set; }
        public string SelectedProfilesCount { get; set; }
        public string PreAppliedCandidatesCount { get; set; }
        public string CompanyProfilesCount { get; set; }

        public DateTime FutureUpdateDate { get; set; }
        public string FutureUpdateTime { get; set; }
        public string FutureUpdateStatus { get; set; }
        public string FutureUpdateComments { get; set; }
        public int ProfileId { get; set; }
        public string ProfileSource { get; set; }
        public string HasCandidateAccess { get; set; }

        public string RecruiterEmail { get; set; }
        public string AssignToEmail { get; set; }
        public string PMEmail { get; set; }
        public string CompanySchedule { get; set; }
        public string InterviewType { get; set; }
        public string DisplayJobStartDate { get; set; }
        public string DisplayJobEndDate { get; set; }
        public bool CanidateShortlisted { get; set; }
        public string DisplayStartDate { get; set; }
        public string DisplayEndDate { get; set; }
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

    public class CompanyProfileModel
    {
        public int DetailsId { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public string Address { get; set; }
        public string Logo { get; set; }
        public int UserId { get; set; }
    }

    public class CompanyRequirementsModel
    {
        public int ReqId { get; set; }
        public string JobCode { get; set; }
        public string JobTitle { get; set; }
        public string JobType { get; set; }
        public string Location { get; set; }
        public int MinExp { get; set; }
        public int MaxExp { get; set; }
        public int HighestPay { get; set; }
        public int PrimarySkill { get; set; }
        public int SecSkill1 { get; set; }
        public int SecSkill2 { get; set; }
        public int SecSkill3 { get; set; }
        public int SecSkill4 { get; set; }
        public int SecSkill5 { get; set; }
        public string JobDesc { get; set; }
        public DateTime JobPostingSDate { get; set; }
        public DateTime JobPostingEDate { get; set; }
        public string Remarks { get; set; }
        public int UserId { get; set; }
        public string AdditionalSkills { get; set; }
        public string subSkill1 { get; set; }
        public string subSkill2 { get; set; }
        public string subSkill3 { get; set; }
        public string subSkill4 { get; set; }
        public string subSkill5 { get; set; }
        public string subSkill6 { get; set; }
        public string subSkill7 { get; set; }
        public string subSkill8 { get; set; }
        public string subSkill9 { get; set; }
        public string subSkill10 { get; set; }
    }

    public class CompanySkillWiseProfilesReportModel
    {
        public int PrimarySkillId { get; set; }
        public string PrimarySkillName { get; set; }
        public int CompanyProfiles { get; set; }
        public int IAIProfiles { get; set; }
    }

    public class CompanySelectedProfiles
    {
        public int reqId { get; set; }
        public int candidateId { get; set; }
        public int scheduleId { get; set; }
        public int userId { get; set; }
        public string grantAccess { get; set; }
        public string candidateName { get; set; }
        public string jobCode { get; set; }
        public string emailId { get; set; }
    }

    public class CompanySubUser
    {
        public string Username { get; set; }
        public string EmailId { get; set; }
        public string Skill { get; set; }
        public string Password { get; set; }
        public string SkillType { get; set; }
        public int UserID { get; set; }
        public string UserType { get; set; }
    }

    public class VendorModel
    {
        public int VendorId { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string VendorName { get; set; }
        public string VendorEmail { get; set; }
        public string VendorMobile { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string DisplayStartDate { get; set; }
        public string DisplayEndDate { get; set; }
    }

    public class RequestInterviewersModel
    {
        public int ReqId { get; set; }
        public int NoOfInterviewers { get; set; }
        public string Location { get; set; }
        public int MinExp { get; set; }
        public int MaxExp { get; set; }
        public int PrimarySkill { get; set; }
        public int SecSkill1 { get; set; }
        public int SecSkill2 { get; set; }
        public int SecSkill3 { get; set; }
        public int SecSkill4 { get; set; }
        public int SecSkill5 { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string JobDesc { get; set; }
        public string Remarks { get; set; }
        public int UserId { get; set; }
    }

    public class CompanyAddedUser
    {
        public string candidateName { get; set; }
        public string candidateEmail { get; set; }
        public string primarySkill { get; set; }
        public string password { get; set; }
        public string type { get; set; }
        public string secondaryskill1 { get; set; }
        public string uniqueId { get; set; }
        public string jobCode { get; set; }
        public string mobileNo { get; set; }
        public string companyId { get; set; }
        public string resume { get; set; }
        public string vendor { get; set; }
    }

    public class CandidateProfileModelCompany
    {
        public int candidateid { get; set; }
        public string PrimarySkill { get; set; }
        public string SecondarySkill1 { get; set; }
        public string SecondarySkill2 { get; set; }
        public string SecondarySkill3 { get; set; }
        public string SecondarySkill4 { get; set; }
        public string SecondarySkill5 { get; set; }
        public string Location { get; set; }
        public string CurrentPay { get; set; }
        public string ExpectedPay { get; set; }
        public string Experience { get; set; }
        public string MobileNo { get; set; }
        public string Address { get; set; }
        public string Resume { get; set; }
        public string Photo { get; set; }
        public string NoticePeriod { get; set; }
        public bool GapInEducation { get; set; }
        public bool GapInExperience { get; set; }
        public string AdditionalSkills { get; set; }
        public string ScreenSelect { get; set; }
        public string SelectStatus { get; set; }
        public bool RestrictEmployerToViewProfile { get; set; }
        public string UniqueId { get; set; }
        public string StatusUpdateRemarks { get; set; }
    }

    public class CompanyProfilesFollowUp
    {
        public List<CompanyProfilesFollowUpDump> followUpDump { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int CompanyProfileId { get; set; }
        public string CandidateName { get; set; }
        public DateTime FollowUpDate { get; set; }
        public string FollowUpDateDisplay { get; set; }
        public string Comments { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string PrimarySkill { get; set; }
        public string SecondarySkill1 { get; set; }
        public string SecondarySkill2 { get; set; }
        public string SecondarySkill3 { get; set; }
        public string SelectStatus { get; set; }
    }
    public class CompanyProfilesFollowUpDump
    {
        public DateTime FollowUpDate { get; set; }
        public string FollowUpDateDisplay { get; set; }
        public string Comments { get; set; }
    }
}