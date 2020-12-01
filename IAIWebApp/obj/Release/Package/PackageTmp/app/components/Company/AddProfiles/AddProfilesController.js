angular.module("IAMInterviewed").controller('AddProfilesController', ['$scope', '$http', '$q', '$filter', '$rootScope', '$timeout', '$window', '$localStorage', function ($scope, $http, $q, $filter, $rootScope, $timeout, $window, $localStorage, $location) {
    //console.log($rootScope.objRequirement);
    $scope.statusReasonArray = IAMInterviewed.ConfigurationSettings.statusReason;
    $scope.clearAll = function () {
        $scope.candidateName = "";
        $scope.candidateEmail = "";
        $scope.candidateMobileNo = "";
        $scope.vendor = "";
        $scope.candidateResume = "";
        $scope.candidateResumeDownload = "";
        $scope.primarySkill = "";
        $scope.primarySkillName = "";
        $scope.secondarySkill1 = "";
        $scope.secondarySkill2 = "";
        $scope.secondarySkill3 = "";
        $scope.secondarySkill4 = "";
        $scope.secondarySkill5 = "";
        $scope.additionalSkills = "";
        $scope.currentLocation = "";
        $scope.experience = "";
        $scope.currentPayLakhs = "";
        $scope.currentPayThousands = "";
        $scope.expectedPayLakhs = "";
        $scope.expectedPayThousands = "";
        $scope.noticePeriod = "Immediate";
        $scope.gapInEducation = false;
        $scope.gapInExperience = false;
        $scope.address = "";
        $scope.screenSelect = "";
        $scope.clientUpdate = "Select";
        $scope.StatusUpdateRemarks = "";
        $scope.StatusUpdateRemarksDisplay = "";
        $scope.statusSearch = "Select";
        $scope.companyCandidateId = "";
        $scope.currentPage = 1;
        $scope.numPerPage = 20;
    }
    $scope.clearAll();

    if ($rootScope.objRequirement == undefined || $rootScope.objRequirement == "") {
        $scope.RedirectUrl('AllPostingsDashboard');
        return;
    }

    $scope.getAllVendors = function () {
        var getAllVendorsURL = IAMInterviewed.Company.getAllVendors + "?companyId=" + $rootScope.loggedInUserDetails.UserID;
        $http.get(getAllVendorsURL).then(function success(response) {
            //console.log(response.data);
            $scope.vendorList = response.data.data;
        }, function error(response) {
            $rootScope.resultMessage = response.data.errorMessage;
            showNotification('error');
        });
    }
    $scope.getAllVendors();

    $scope.getCompanyProfiles = function () {
        manageLoader('load');
        var getCompanyAddedProfilesURL = IAMInterviewed.Company.getCompanyAddedProfiles + "?companyId=" + $rootScope.loggedInUserDetails.UserID + "&primaryskill=" + $rootScope.objRequirement.PrimarySkill
            + "&jobCode=" + $rootScope.objRequirement.JobCode + "&statusFilter=" + $scope.statusSearch;
        $http.get(getCompanyAddedProfilesURL).then(function success(response) {
            //console.log(response.data);
            $scope.companyProfilesAll = response.data.data;
            $scope.companyProfiles = response.data.data;
            $scope.totalItems = $scope.companyProfiles.length;
            manageLoader();
        }, function error(response) {
            $rootScope.resultMessage = response.data.errorMessage;
            showNotification('error');
            manageLoader();
        });
    }
    $scope.getCompanyProfiles();

    $scope.saveCompanyAddedCandidate = function () {
        manageLoader('load');
        //console.log($scope.myFile);
        if (($scope.myFile == undefined || $scope.myFile == "") && $scope.candidateResume == "") {
            $rootScope.resultMessage = "Please upload resume.";
            showNotification('error');
            manageLoader();
        }
        else {
            if ($scope.myFile != undefined && $scope.myFile != "") {
                $scope.UploadType = "CandidateResume";
                var file = $scope.myFile;
                var UploadURL = IAMInterviewed.Candidate.UploadResume;
                var payload = new FormData();
                payload.append("CandidateId", $rootScope.loggedInUserDetails.UserID);
                payload.append("file", file);
                $http.post(UploadURL, payload, {
                    headers: { 'Content-Type': undefined },
                    transformRequest: angular.identity
                }).then(function (response) {
                    $scope.UpdateCandidateDetailsAfterFileUpload($rootScope.loggedInUserDetails.UserID + "_" + file.name);
                    manageLoader();
                }, function (response) {
                    $rootScope.resultMessage = "Error while Uploading Resume. Please try again....";
                    showNotification('error');
                    manageLoader();
                });
            }
        }
    }

    $scope.UpdateCandidateDetailsAfterFileUpload = function (Resume) {
        manageLoader('load');
        var objData = {
            candidateName: $scope.candidateName,
            candidateEmail: $scope.candidateEmail,
            primarySkill: $rootScope.objRequirement.PrimarySkill,
            password: "",
            type: "Candidate",
            secondaryskill1: $rootScope.objRequirement.SecSkill1,
            uniqueId: "",
            jobCode: $rootScope.objRequirement.JobCode,
            mobileNo: $scope.candidateMobileNo,
            companyId: $rootScope.loggedInUserDetails.UserID,
            resume: Resume,
            vendor: $scope.vendor
        }
        var saveCompanyAddedUserURL = IAMInterviewed.Company.saveCompanyAddedUser;
        $http.post(saveCompanyAddedUserURL, objData).then(function (response) {
            if (response.data.Success == true) {
                $rootScope.resultMessage = "Candidate Added Successfylly.";
                showNotification('success');
                manageLoader();
                $scope.clearAll();
                $scope.getCompanyProfiles();
            }
            else {
                $rootScope.resultMessage = response.data.errorMessage;
                showNotification('error');
                manageLoader();
            }
        }, function (response) {
            $rootScope.resultMessage = response.data.errorMessage;
            showNotification('error');
            manageLoader();
        });
    }

    $scope.LoadSecondarySkills = function () {
        manageLoader('load');
        var deferred = $q.defer();
        var GetSecondarySkillsURL = IAMInterviewed.Skills.GetSecondarySkills + "?PrimarySkill=" + $scope.primarySkill;
        $http.get(GetSecondarySkillsURL).then(function success(response) {
            //console.log(response.data);
            $scope.SecondarySkills = response.data.data;
            manageLoader();
            deferred.resolve(response.data.data);
        },
            function error(response) {
                $rootScope.resultMessage = response.data.errorMessage;
                showNotification('error');
                manageLoader();
                deferred.reject;
            });
        return deferred.promise;
    }

    $scope.LoadCities = function () {
        manageLoader('load');
        var deferred = $q.defer();
        var getCitiesURL = IAMInterviewed.Skills.getCities + "?Country=" + $rootScope.loggedInUserDetails.Country;
        $http.get(getCitiesURL).then(function success(response) {
            //console.log(response.data);
            $scope.Cities = response.data.data;
            manageLoader();
            deferred.resolve(response.data.data);
        },
            function error(response) {
                $rootScope.resultMessage = response.data.errorMessage;
                showNotification('error');
                manageLoader();
                deferred.reject;
            });
        return deferred.promise;
    }

    $scope.viewDetails = function (objCandidate) {
        //console.log(objCandidate);
        $scope.primarySkill = objCandidate.PrimarySkill;
        $scope.companyCandidateId = objCandidate.candidateid;
        manageLoader('load');
        var getCompanyAddedProfilesURL = IAMInterviewed.Company.getCandidateProfileCompany + "?userId=" + objCandidate.candidateid;
        $http.get(getCompanyAddedProfilesURL).then(function success(response) {
            $scope.candidateProfile = response.data.data;
            //console.log($scope.candidateProfile);
            $scope.bindCandidateDetails();
            manageLoader();
        }, function error(response) {
            $rootScope.resultMessage = response.data.errorMessage;
            showNotification('error');
            manageLoader();
        });
    }

    $scope.bindCandidateDetails = function () {
        $scope.LoadSecondarySkills().then(function (response) {
            $scope.primarySkillName = $rootScope.objRequirement.PrimarySKillName.toString();
            $scope.secondarySkill1 = $rootScope.objRequirement.SecSkill1.toString();
            $scope.secondarySkill2 = $rootScope.objRequirement.SecSkill2.toString();
            $scope.secondarySkill3 = $rootScope.objRequirement.SecSkill3.toString();
            $scope.secondarySkill4 = $rootScope.objRequirement.SecSkill4.toString();
            $scope.secondarySkill5 = $rootScope.objRequirement.SecSkill5.toString();
            $scope.LoadCities().then(function (response) {
                $scope.secondarySkill1 = $rootScope.objRequirement.SecSkill1.toString();//$scope.candidateProfile[0].SecondarySkill1 != '0' ? $scope.candidateProfile[0].SecondarySkill1.toString() : "";
                $scope.screenSelect = $scope.candidateProfile[0].ScreenSelect != '0' ? $scope.candidateProfile[0].ScreenSelect.toString() : "";
                $scope.clientUpdate = $scope.candidateProfile[0].SelectStetus == '-1' ? "Select" : $scope.candidateProfile[0].SelectStetus == '0' ? "Select" : $scope.candidateProfile[0].SelectStetus.toString();
                $scope.StatusUpdateRemarksDisplay = $scope.candidateProfile[0].StatusUpdateRemarks;
                $scope.FollowUpDateDisplay = $scope.candidateProfile[0].FollowUpDateDisplay;
                $scope.candidateUniqueId = $scope.candidateProfile[0].uniquenumber;
                $scope.mobileNumber = $scope.candidateProfile[0].Mobile;
                $scope.candidateResumeDownload = $scope.candidateProfile[0].Resume.toString();
                if ($scope.candidateProfile[0].SecondarySkill2 != '0') {
                    $scope.secondarySkill2 = $rootScope.objRequirement.SecSkill2.toString();//$scope.candidateProfile[0].SecondarySkill2 != '0' ? $scope.candidateProfile[0].SecondarySkill2.toString() : "";
                    $scope.secondarySkill3 = $rootScope.objRequirement.SecSkill3.toString();//$scope.candidateProfile[0].SecondarySkill3 != '0' ? $scope.candidateProfile[0].SecondarySkill3.toString() : "";
                    $scope.secondarySkill4 = $rootScope.objRequirement.SecSkill4.toString();//$scope.candidateProfile[0].SecondarySkill4 != '0' ? $scope.candidateProfile[0].SecondarySkill4.toString() : "";
                    $scope.secondarySkill5 = $rootScope.objRequirement.SecSkill5.toString();//$scope.candidateProfile[0].SecondarySkill5 != '0' ? $scope.candidateProfile[0].SecondarySkill5.toString() : "";
                    $scope.additionalSkills = $scope.candidateProfile[0].AdditionalSkills.toString();
                    //$scope.CandidateResume = $scope.candidateProfile[0].Resume.toString();

                    $scope.experience = $scope.candidateProfile[0].Experience != '0' ? $scope.candidateProfile[0].Experience.toString() : "";
                    $scope.noticePeriod = $scope.candidateProfile[0].NoticePeriod.toString();
                    $scope.gapInEducation = $scope.candidateProfile[0].GapInEducation;
                    $scope.gapInExperience = $scope.candidateProfile[0].GapInExperience;
                    //$scope.restrictEmployerToViewProfile = $scope.candidateProfile[0].RestrictEmployerToViewProfile;
                    $scope.mobileNumber = $scope.candidateProfile[0].Mobile.toString();
                    $scope.address = $scope.candidateProfile[0].Address.toString();
                    var CurrentPay = $scope.candidateProfile[0].CurrentPay.toString();
                    var CurrentPaySplit = CurrentPay.split(".");
                    $scope.currentPayLakhs = parseInt(CurrentPaySplit[0]);
                    if (CurrentPaySplit.length > 1) {
                        $scope.currentPayThousands = parseInt(CurrentPaySplit[1]);
                    }

                    var ExpectedPay = $scope.candidateProfile[0].ExpectedPay.toString();
                    var ExpectedPaySplit = ExpectedPay.split(".");
                    $scope.expectedPayLakhs = parseInt(ExpectedPaySplit[0]);
                    if (ExpectedPaySplit.length > 0) {
                        $scope.expectedPayThousands = parseInt(ExpectedPaySplit[1]);
                    }
                    $scope.currentLocation = $scope.candidateProfile[0].Location != '0' ? $scope.candidateProfile[0].Location.toString() : "";
                }
                $("#candidateProfile").modal("show");
            });
        });
    }

    $scope.saveCandiateProfile = function () {
        manageLoader('load');
        var objData = {
            candidateid: $scope.companyCandidateId,
            PrimarySkill: $scope.primarySkill,
            SecondarySkill1: $scope.secondarySkill1,
            SecondarySkill2: $scope.secondarySkill2,
            SecondarySkill3: $scope.secondarySkill3,
            SecondarySkill4: $scope.secondarySkill4,
            SecondarySkill5: $scope.secondarySkill5,
            Location: $scope.currentLocation,
            CurrentPay: $scope.currentPayLakhs + "." + $scope.currentPayThousands,
            ExpectedPay: $scope.expectedPayLakhs + "." + $scope.expectedPayThousands,
            Experience: $scope.experience,
            MobileNo: $scope.mobileNumber,
            Address: $scope.address,
            Resume: $scope.candidateResumeDownload,
            Photo: "",
            NoticePeriod: $scope.noticePeriod,
            GapInEducation: $scope.gapInEducation,
            GapInExperience: $scope.gapInExperience,
            AdditionalSkills: $scope.additionalSkills,
            ScreenSelect: $scope.screenSelect,
            SelectStatus: $scope.clientUpdate,
            StatusUpdateRemarks: $scope.StatusUpdateRemarks,
            RestrictEmployerToViewProfile: false,
            UniqueId: $scope.candidateUniqueId
        }
        var saveCandidateProfileURL = IAMInterviewed.Company.saveCandidateProfileCompany;
        $http.post(saveCandidateProfileURL, objData).then(function (response) {
            if (response.data.Success == true) {
                $rootScope.resultMessage = "Data Saved Successfully.";
                showNotification('success');
                manageLoader();
                $("#candidateProfile").modal("hide");
                $scope.clearAll();
                $scope.getCompanyProfiles();
            }
            else {
                $rootScope.resultMessage = response.data.errorMessage;
                showNotification('error');
                manageLoader();
            }
        }, function (response) {
            $rootScope.resultMessage = response.data.errorMessage;
            showNotification('error');
            manageLoader();
        });
    }

    $scope.updateDetails = function (objDetails) {
        manageLoader('load');
        var updateSelectStatusURL = IAMInterviewed.Company.UpdateSelectStatus + "?candidateId=" + objDetails.candidateid + "&selectStatus=" + objDetails.SelectStetus
            + "&statusRemarks=" + objDetails.StatusUpdateRemarks + "&userId=" + $rootScope.loggedInUserDetails.UserID;
        $http.get(updateSelectStatusURL).then(function success(response) {
            $rootScope.resultMessage = "Status Updated Successfully.";
            showNotification('success');
            manageLoader();
        }, function error(response) {
            $rootScope.resultMessage = response.data.errorMessage;
            showNotification('error');
            manageLoader();
        });
    }

    $scope.scheduleInterviewPopup = function (objCandidate) {
        $scope.scheduleInterviewCandidate = objCandidate;
        $scope.ScheduleDate = "";
        $scope.Interviewer = "";
        $scope.TimeSlot = "";
        $scope.InterviewType = "Audio";
        //console.log($scope.scheduleInterviewCandidate);
        manageLoader('load');
        var getCompanyAddedProfilesURL = IAMInterviewed.Company.getCandidateProfileCompany + "?userId=" + objCandidate.candidateid;
        $http.get(getCompanyAddedProfilesURL).then(function success(response) {
            if (response.data.data == null || response.data.data == "" || response.data.data.length < 1 || response.data.data[0].SecondarySkill2 == ""
                || response.data.data[0].SecondarySkill2 == "0") {
                $rootScope.resultMessage = "Please Fill Candidate Details to Schedule Interview.";
                showNotification('error');
            }
            else {
                if ($scope.scheduleInterviewCandidate.ScheduleId == 0 || $scope.scheduleInterviewCandidate.ScheduleId == "" || $scope.scheduleInterviewCandidate.ScheduleId == "0") {
                    $("#scheduleInterviewPopup").modal("show");                    
                }
                else {
                    $rootScope.resultMessage = "Interview already scheduled.";
                    showNotification('error');
                }
            }    
            manageLoader();
        }, function error(response) {
            $rootScope.resultMessage = response.data.errorMessage;
            showNotification('error');
            manageLoader();
        });      
    }

    $scope.loadInterviewers = function () {
        if ($scope.ScheduleDate != null && $scope.ScheduleDate != "") {
            manageLoader('load');
            var getInterviewerByDateURL = IAMInterviewed.Candidate.getInterviewerByDate + "?ScheduleDate=" + $scope.ScheduleDate + "&UserId=" + $scope.scheduleInterviewCandidate.candidateid;
            $http.get(getInterviewerByDateURL).then(function success(response) {
                //console.log(response.data);
                $scope.Interviewers = response.data.data;
                manageLoader();
            }, function error(response) {
                $rootScope.resultMessage = response.data.errorMessage;
                showNotification('error');
                manageLoader();
            });
        }
        else {
            $rootScope.resultMessage = "Please Select Date.";
            showNotification('error');
        }
    }

    $scope.loadTimeSlots = function () {
        if ($scope.Interviewer != null && $scope.Interviewer != "") {
            manageLoader('load');
            var getInterviewerTimeSlotsByDateURL = IAMInterviewed.Candidate.getInterviewerTimeSlotsByDate + "?ScheduleDate=" + $scope.ScheduleDate + "&InterviewerId=" + $scope.Interviewer;
            $http.get(getInterviewerTimeSlotsByDateURL).then(function success(response) {
                //console.log(response.data);
                $scope.TimeSlots = response.data.data;
                manageLoader();
            }, function error(response) {
                $rootScope.resultMessage = response.data.errorMessage;
                showNotification('error');
                manageLoader();
            });
        }
        else {
            $rootScope.resultMessage = "Please Select Date.";
            showNotification('error');
        }
    }

    $scope.schedulePopUp = function (objSlot, objInterviewer) {
        $scope.objInterviewer = objInterviewer;
        $scope.objTimeSlot = objSlot;
        $("#confirmScheduleInterviewPopup").modal("show");
    }

    $scope.selectTime = function () {
        $scope.Interviewer = $scope.objInterviewer.UserId;
        $scope.TimeSlot = $scope.objTimeSlot.TimeSlotId;
        $("#confirmScheduleInterviewPopup").modal("hide");
    }


    $scope.scheduleInterview = function () {
        manageLoader('load');
        var saveInterviewScheduleURL = IAMInterviewed.Company.saveInterviewScheduleCompany + "?ScheduleDate=" + $scope.ScheduleDate + "&Interviewer=" + $scope.Interviewer +
            "&TimeSlot=" + $scope.TimeSlot + "&CandidateId=" + $scope.scheduleInterviewCandidate.candidateid + "&InterviewType=" + $scope.InterviewType + "&CompanyId=" +
            $rootScope.loggedInUserDetails.UserID + "&JobCode=" + $scope.scheduleInterviewCandidate.JobCode;
        $http.get(saveInterviewScheduleURL).then(function (response) {
            if (response.data.Success == true) {
                $rootScope.resultMessage = response.data.data;
                showNotification('success');
                manageLoader();
                $("#scheduleInterviewPopup").modal("hide");
                $scope.getCompanyProfiles();
            }
            else {
                $rootScope.resultMessage = response.data.errorMessage;
                showNotification('error');
                manageLoader();
                return false;
            }
        }, function (response) {
            $rootScope.resultMessage = response.data.errorMessage;
            showNotification('error');
            manageLoader();
            return false;
        });
    }

    $scope.loadTimeSlotsReSchedule = function () {
        manageLoader('load');
        var deferred = $q.defer();
        manageLoader('load');
        var getInterviewerTimeSlotsByDateURL = IAMInterviewed.Candidate.getInterviewerTimeSlotsByDate + "?ScheduleDate=" + $scope.reScheduleDate + "&InterviewerId=" +
            $scope.reScheduleInterviewCandidate.Interviewer;
        $http.get(getInterviewerTimeSlotsByDateURL).then(function success(response) {
            //console.log(response.data);
            $scope.TimeSlotsReSchedule = response.data.data;
            deferred.resolve(response.data.data);
            manageLoader();
        }, function error(response) {
            $rootScope.resultMessage = response.data.errorMessage;
            showNotification('error');
            manageLoader();
            deferred.reject;
        });
        return deferred.promise;
    }

    $scope.reScheduleInterviewPopup = function (objSchedule) {
        $scope.reScheduleInterviewCandidate = objSchedule;
        $scope.reScheduleDate = "";
        $scope.reScheduleTimeSlot = "";
        console.log($scope.reScheduleInterviewCandidate);        
        //$scope.loadTimeSlotsReSchedule().then(function (response) {
            if ($scope.reScheduleInterviewCandidate.ScheduleId != 0 && $scope.reScheduleInterviewCandidate.ScheduleId != "") {
                $("#reScheduleInterviewPopup").modal("show");
            }
            else {
                $rootScope.resultMessage = "Please schedule Interview.";
                showNotification('error');
            }            
        //});
    }

    $scope.reScheduleInterview = function () {
        manageLoader('load');
        var reScheduleInterviewURL = IAMInterviewed.Company.reScheduleInterview + "?ScheduleId=" + $scope.reScheduleInterviewCandidate.ScheduleId + "&ReScheduleDate=" + $scope.reScheduleDate +
            "&TimeSlot=" + $scope.reScheduleTimeSlot + "&candidateId=" + $scope.reScheduleInterviewCandidate.candidateid;
        $http.get(reScheduleInterviewURL).then(function (response) {
            if (response.data.Success == true) {
                $rootScope.resultMessage = response.data.data;
                showNotification('success');
                manageLoader();
                $("#reScheduleInterviewPopup").modal("hide");
                $scope.getCompanyProfiles();
            }
            else {
                $rootScope.resultMessage = response.data.errorMessage;
                showNotification('error');
                manageLoader();
                return false;
            }
        }, function (response) {
            $rootScope.resultMessage = response.data.errorMessage;
            showNotification('error');
            manageLoader();
            return false;
        });
    }

    $scope.viewRatingDetails = function (objCandidateRating) {
        $scope.$broadcast('bindCandidateRatingDetails', $rootScope.objRequirement.ReqId, objCandidateRating.ScheduleId);
        //$("#candidateratingdetails").modal('show');
    }

    $scope.paginate = function (value) {
        var begin, end, index;
        begin = ($scope.currentPage - 1) * $scope.numPerPage;
        end = begin + $scope.numPerPage;
        index = $scope.companyProfiles.indexOf(value);
        return (begin <= index && index < end);
    };

    $(document).ready(function () {
        $timeout(function () {
            //onComponentLoad();
            DatePickerfunc();
            //$(".select2").select2();
        });
    });
}]);