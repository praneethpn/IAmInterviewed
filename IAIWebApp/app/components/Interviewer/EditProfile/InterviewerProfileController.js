angular.module('IAMInterviewed').controller('InterviewerProfileController', ['$scope', '$http', '$q', '$filter', '$rootScope', '$timeout', '$window', '$localStorage', function ($scope, $http, $q, $filter, $rootScope, $timeout, $window, $localStorage, $location) {
    $scope.ClearAll = function () {
        $scope.PrimarySkill = "";
        $scope.SecondarySkill1 = "";
        $scope.SecondarySkill2 = "";
        $scope.SecondarySkill3 = "";
        $scope.SecondarySkill4 = "";
        $scope.SecondarySkill5 = "";
        $scope.AdditionalSkills = "";
        $scope.InterviewerResume = "";
        $scope.InterviewerResumeDownload = "";
        $scope.CurrentLocation = "";
        $scope.Experience = "";
        $scope.CurrentPayLakhs = "";
        $scope.CurrentPayThousands = "";
        $scope.ExpectedPayLakhs = "";
        $scope.ExpectedPayThousands = "";
        $scope.NoticePeriod = "Immediate";
        $scope.GapInEducation = false;
        $scope.GapInExperience = false;
        $scope.RestrictEmployerToViewProfile = false;
        $scope.MobileNumber = "";
        $scope.Address = "";
    }
    $scope.ClearAll();

    $scope.getInterviewerProfile = function () {
        manageLoader('load');
        var deferred = $q.defer();
        var getCandidateProfileURL = IAMInterviewed.Interviewer.getInterviewerProfile + "?CandidateId=" + $rootScope.loggedInUserDetails.UserID;
        $http.get(getCandidateProfileURL).then(function success(response) {
            $scope.CandidateProfile = response.data.data;
            //console.log($scope.CandidateProfile);
            if ($scope.CandidateProfile.length > 0) {
                $scope.bindInterviewerDetails();
            }
            manageLoader();
        }, function error(response) {
            $rootScope.resultMessage = response.data.errorMessage;
            showNotification('error');
            manageLoader();
            deferred.reject;
        });
        return deferred.promise;
    }

    $scope.LoadPrimarySkills = function () {
        manageLoader('load');
        var deferred = $q.defer();
        var getPrimarySkills = IAMInterviewed.Skills.GetSkills;
        $http.get(getPrimarySkills).then(function success(response) {
            //console.log(response.data);
            $scope.PrimarySkills = response.data.data;
            //$scope.PrimarySkill = $scope.PrimarySkills[0].SkillId.toString();
            //$scope.LoadSecondarySkills();
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

    $scope.LoadSecondarySkills = function () {
        manageLoader('load');
        var deferred = $q.defer();
        var GetSecondarySkillsURL = IAMInterviewed.Skills.GetSecondarySkills + "?PrimarySkill=" + $scope.PrimarySkill;
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

    $scope.bindInterviewerDetails = function () {
        $scope.LoadPrimarySkills().then(function (response) {
            $scope.PrimarySkill = $rootScope.loggedInUserDetails.SkillId;
            $scope.LoadSecondarySkills().then(function (response) {
                $scope.SecondarySkill1 = $scope.CandidateProfile[0].SecondarySkill1 != '0' ? $scope.CandidateProfile[0].SecondarySkill1.toString() : "";
                $scope.SecondarySkill2 = $scope.CandidateProfile[0].SecondarySkill2 != '0' ? $scope.CandidateProfile[0].SecondarySkill2.toString() : "";
                $scope.SecondarySkill3 = $scope.CandidateProfile[0].SecondarySkill3 != '0' ? $scope.CandidateProfile[0].SecondarySkill3.toString() : "";
                $scope.SecondarySkill4 = $scope.CandidateProfile[0].SecondarySkill4 != '0' ? $scope.CandidateProfile[0].SecondarySkill4.toString() : "";
                $scope.SecondarySkill5 = $scope.CandidateProfile[0].SecondarySkill5 != '0' ? $scope.CandidateProfile[0].SecondarySkill5.toString() : "";
                $scope.InterviewerResumeDownload = $scope.CandidateProfile[0].Resume.toString();
                $scope.Experience = $scope.CandidateProfile[0].Experience != '0' ? $scope.CandidateProfile[0].Experience.toString() : "";
                $scope.MobileNumber = $scope.CandidateProfile[0].Mobile.toString();
                $scope.Address = $scope.CandidateProfile[0].Address.toString();
                $scope.LoadCities().then(function (response) {
                    $scope.CurrentLocation = $scope.CandidateProfile[0].Location != '0' ? $scope.CandidateProfile[0].Location.toString() : "";
                });
            });
        });
    }

    $scope.getInterviewerProfile();

    $scope.saveInterviewerProfile = function () {
        manageLoader('load');
        //console.log($scope.myFile);
        if (($scope.myFile == undefined || $scope.myFile == "") && ($scope.InterviewerResumeDownload == "" || $scope.InterviewerResumeDownload == null)) {
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
            //$scope.UpdateCandidateDetailsAfterFileUpload($rootScope.loggedInUserDetails.UserID + "_" + file.name);
        }
        else {
            $scope.UpdateCandidateDetailsAfterFileUpload($scope.CandidateResumeDownload);
            manageLoader();
        }
        }
    }

    $scope.UpdateCandidateDetailsAfterFileUpload = function (Resume) {
        manageLoader('load');
        var objData = {
            CandidateId: $rootScope.loggedInUserDetails.UserID,
            PrimarySkill: $scope.PrimarySkill,
            SecondarySkill1: $scope.SecondarySkill1,
            SecondarySkill2: $scope.SecondarySkill2,
            SecondarySkill3: $scope.SecondarySkill3,
            SecondarySkill4: $scope.SecondarySkill4,
            SecondarySkill5: $scope.SecondarySkill5,
            UniqueId: $rootScope.loggedInUserDetails.UniqueId,
            MobileNo: $scope.MobileNumber,
            AdditionalSkills: $scope.AdditionalSkills,
            Address: $scope.Address,
            GapInEducation: $scope.GapInEducation,
            GapInExperience: $scope.GapInExperience,
            RestrictEmployerToViewProfile: $scope.RestrictEmployerToViewProfile,
            NoticePeriod: $scope.NoticePeriod,
            ExpectedPay: $scope.ExpectedPayLakhs + "." + $scope.ExpectedPayThousands,
            CurrentPay: $scope.CurrentPayLakhs + "." + $scope.CurrentPayThousands,
            Experience: $scope.Experience,
            Location: $scope.CurrentLocation,
            ScreenSelect: 'NA',
            SelectStatus: 'NA',
            Resume: Resume
        }
        var saveCandidateProfileURL = IAMInterviewed.Interviewer.saveInterviewerProfile;
        $http.post(saveCandidateProfileURL, objData).then(function (response) {
            if (response.data.Success == true) {
                $rootScope.resultMessage = "Data Saved Successfylly.";
                showNotification('success');
                manageLoader();
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

    $(document).ready(function () {
        $timeout(function () {
            onComponentLoad();
        });
    });
}]);