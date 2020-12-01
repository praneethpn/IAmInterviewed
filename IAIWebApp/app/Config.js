﻿var IAMInterviewed = {
    ConfigurationSettings: {
        LoginErrorMessage: 'In Valid User, Please check Username/Password',
        Date: '18-01-2017',
        DateKey: '20170118',
        statusReason: ["Profile Added", "No Info", "No Response", "No Show", "No Interest", "ReSchedule", "Rating Pending", "Panel Not Joined", "Client Reject", "JD mismatch", "Offered", "Joined"]
    },

    userManagent: {
        authenticateUser: "Account/Login",
        signOut: "Account/LogOff",
        register: "Account/Register",
        forgotPassword: "Account/ForgotPassword",
        changePassword: "Account/ChangePassword"
    },
    Admin: {
        GetDashboardDetails: "Company/GetAdminDashboardDetials",
        GetTop10Ratings: "Account/GetTop10Ratings"
    },
    Skills: {
        GetSkills: "Skill/GetPrimarySkills",
        GetSecondarySkills: "Skill/GetSecondarySkills",
        getCities: "Skill/GetCities"
    },
    Interviewer: {
        getInterviews: "Interviewer/getInterviews",
        updateInterviewStatus: "Interviewer/updateInterviewStatus",
        confirmInterview: "Interviewer/confirmInterview",
        getInterviewSchedulebyId: "Interviewer/getInterviewSchedulebyId",
        updateRating: "Interviewer/updateRating",
        getInterviewerProfile: "Interviewer/getInterviewerProfile",
        saveInterviewerProfile: "Interviewer/saveInterviewerProfile",
        getTimeSlots: "Interviewer/getTimeSlots",
        saveInterviewerSchedule: "Interviewer/saveInterviewerSchedule",
        getInterviewerSchedulesByDate: "Interviewer/getInterviewerSchedulesByDate",
        referUser: "Interviewer/referUser",
        UploadRatingFile: "Interviewer/UploadRatingFile",
        extractResume: "Interviewer/extractResume"
    },
    Candidate: {
        getCandidateDashboardDetails: "Candidate/getCandidateDashboardDetails",
        getFavoriteCompany: "Candidate/getFavoriteCompany",
        getViewedProfiles: "Candidate/getViewedProfiles",
        getScheduledInterview: "Candidate/getScheduledInterview",
        updateGrantAccess: "Candidate/UpdateGrantAccess",
        getInterviewSchedulebyId: "Candidate/getInterviewSchedulebyId",
        acceptRating: "Candidate/acceptRating",
        getCandidateProfile: "Candidate/getCandidateProfile",
        UploadResume: "Candidate/UploadFile",
        saveCandidateProfile: "Candidate/saveCandidateProfile",
        getInterviewerByDate: "Candidate/getInterviewerByDate",
        getInterviewerTimeSlotsByDate: "Candidate/getInterviewerTimeSlotsByDate",
        saveInterviewSchedule: "Candidate/saveInterviewSchedule",
        saveInterviewSchedulePayment: "Candidate/saveInterviewSchedulePayment",
        fillFavoriteCompany: "Candidate/fillFavoriteCompany",
        fillDesignation: "Candidate/fillDesignation",
        saveFavoriteCompany: "Candidate/saveFavoriteCompany",
        getCandidateRelatedRequirements: "Candidate/getCandidateRelatedRequirements",
        getCandidateAppliedRequirements: "Candidate/getCandidateAppliedRequirements",
        saveCandidateApplication: "Candidate/saveCandidateApplication",
        getCompanyJD: "Candidate/getCompanyJD",
        getCandidateProfileForCall: "Candidate/getCandidateProfileForCall",
        GetCandidateRatingDetails: "Candidate/GetCandidateRatingDetails"
    },
    Company: {
        getCompanyHomePageDetails: "Company/getCompanyHomePageDetails",
        getCompanyDashboardDetails: "Company/getCompanyDashboardDetails",
        getCompanyRequirements: "Company/GetCompanyRequirements",
        saveCompanyRequirements: "Company/SaveCompanyRequirements",
        getCompanyRequirementsForSelectList: "Company/GetCompanyRequirementsForSelectList",
        fillRecruiters: "Company/FillRecruiters",
        fillPM: "Company/FillPM",
        updateCompanyrequirements: "Company/UpdateCompanyrequirements",
        getReqRelatedCandidates: "Company/GetReqRelatedCandidates",
        shortlistCandidate: "Company/SaveCompanySelectedProfiles",
        shortlistCandidateBulk: "Company/SaveCompanySelectedProfilesList",
        getSelectedCandidates: "Company/SelectedCandidatesList",
        updateFutureDate: "Company/UpdateFutureDate",
        selectedProfilesDump: "Company/SelectedProfilesDump",
        getAppliedCandidates: "Company/GetAppliedCandidates",
        getFanProfiles: "Company/GetFanProfiles",
        followUpProfiles: "Company/FollowUpProfiles",
        updateTrackingStatus: "Company/UpdateTrackingStatus",
        getCompanyAddedCandidateDetials: "Company/GetCompanyAddedCandidateDetials",
        getSubUsers: "Company/GetSubUsers",
        saveSubUser: "Company/SaveSubUser",
        getDesignation: "Company/GetDesignation",
        saveDesignation: "Company/SaveDesignation",
        getAllVendors: "Company/GetAllVendors",
        saveVendor: "Company/SaveVendor",
        getCompanyProfile: "Company/GetCompanyProfile",
        saveCompanyProfile: "Company/SaveCompanyProfile",
        uploadCompanyLogo: "Company/UploadCompanyLogo",
        getRequestInterviewers: "Company/GetNoOfInterviewersRequired",
        saveRequestInterviewers: "Company/SaveRequestInterviewers",
        getCompanyAddedProfiles: "Company/GetCompanyAddedProfiles",
        saveCompanyAddedUser: "Company/SaveCompanyAddedUser",
        getCandidateProfileCompany: "Company/GetCandidateProfileCompany",
        saveCandidateProfileCompany: "Company/SaveCandidateProfileCompany",
        UpdateSelectStatus: "Company/UpdateSelectStatus",
        saveInterviewScheduleCompany: "Company/saveInterviewScheduleCompany",
        reScheduleInterview: "Company/UpdateInterviewSchedule",
        getSchedulesByUniqueId: "Company/GetSchedulesByUniqueId",
        GetCompanyDashBoardJobPostingDetails: "Company/GetCompanyDashBoardJobPostingDetails",
        saveCompanyRequestAccess: "Company/saveCompanyRequestAccess",
        getScheduleDetailsForViewRating: "Company/getScheduleDetailsForViewRating"
    }
}