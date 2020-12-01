angular.module('IAMInterviewed').controller('CandidateProfileController', ['$scope', '$http', '$q', '$filter', '$rootScope', '$timeout', '$window', '$localStorage', 'myService', function ($scope, $http, $q, $filter, $rootScope, $timeout, $window, $localStorage, $location, myService) {
    $scope.ClearAll = function () {
        $scope.PrimarySkill = "";
        $scope.SecondarySkill1 = "";
        $scope.SecondarySkill2 = "";
        $scope.SecondarySkill3 = "";
        $scope.SecondarySkill4 = "";
        $scope.SecondarySkill5 = "";
        $scope.AdditionalSkills = "";
        $scope.CandidateResume = "";
        $scope.CandidateResumeDownload = "";
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

    $scope.getCandidateProfile = function () {
        manageLoader('load');
        var deferred = $q.defer();
        var getCandidateProfileURL = IAMInterviewed.Candidate.getCandidateProfile + "?CandidateId=" + $rootScope.loggedInUserDetails.UserID;
        $http.get(getCandidateProfileURL).then(function success(response) {
            $scope.CandidateProfile = response.data.data;
            //console.log($scope.CandidateProfile);
            if ($scope.CandidateProfile.length > 0) {
                $scope.bindCandidateDetails();
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
        }, function error(response) {
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

    $scope.bindCandidateDetails = function () {
        $scope.LoadPrimarySkills().then(function (response) {
            $scope.PrimarySkill = $rootScope.loggedInUserDetails.SkillId;
            $scope.LoadSecondarySkills().then(function (response) {
                $scope.SecondarySkill1 = $scope.CandidateProfile[0].SecondarySkill1 != '0' ? $scope.CandidateProfile[0].SecondarySkill1.toString() : "";
                $scope.SecondarySkill2 = $scope.CandidateProfile[0].SecondarySkill2 != '0' ? $scope.CandidateProfile[0].SecondarySkill2.toString() : "";
                $scope.SecondarySkill3 = $scope.CandidateProfile[0].SecondarySkill3 != '0' ? $scope.CandidateProfile[0].SecondarySkill3.toString() : "";
                $scope.SecondarySkill4 = $scope.CandidateProfile[0].SecondarySkill4 != '0' ? $scope.CandidateProfile[0].SecondarySkill4.toString() : "";
                $scope.SecondarySkill5 = $scope.CandidateProfile[0].SecondarySkill5 != '0' ? $scope.CandidateProfile[0].SecondarySkill5.toString() : "";
                $scope.AdditionalSkills = $scope.CandidateProfile[0].AdditionalSkills.toString();
                //$scope.CandidateResume = $scope.CandidateProfile[0].Resume.toString();
                $scope.CandidateResumeDownload = $scope.CandidateProfile[0].Resume.toString();
                $scope.Experience = $scope.CandidateProfile[0].Experience != '0' ? $scope.CandidateProfile[0].Experience.toString() : "";
                $scope.NoticePeriod = $scope.CandidateProfile[0].NoticePeriod.toString();
                $scope.GapInEducation = $scope.CandidateProfile[0].GapInEducation;
                $scope.GapInExperience = $scope.CandidateProfile[0].GapInExperience;
                $scope.RestrictEmployerToViewProfile = $scope.CandidateProfile[0].RestrictEmployerToViewProfile;
                $scope.MobileNumber = $scope.CandidateProfile[0].Mobile.toString();
                $scope.Address = $scope.CandidateProfile[0].Address.toString();
                var CurrentPay = $scope.CandidateProfile[0].CurrentPay.toString();
                var CurrentPaySplit = CurrentPay.split(".");
                $scope.CurrentPayLakhs = parseInt(CurrentPaySplit[0]);
                if (CurrentPaySplit.length > 1) {
                    $scope.CurrentPayThousands = parseInt(CurrentPaySplit[1]);
                }

                var ExpectedPay = $scope.CandidateProfile[0].ExpectedPay.toString();
                var ExpectedPaySplit = ExpectedPay.split(".");
                $scope.ExpectedPayLakhs = parseInt(ExpectedPaySplit[0]);
                if (ExpectedPaySplit.length > 0) {
                    $scope.ExpectedPayThousands = parseInt(ExpectedPaySplit[1]);
                }

                $scope.LoadCities().then(function (response) {
                    $scope.CurrentLocation = $scope.CandidateProfile[0].Location != '0' ? $scope.CandidateProfile[0].Location.toString() : "";
                });
            });
        });
    }

    $scope.getCandidateProfile();

    $scope.saveCanidateProfile = function () {
        manageLoader('load');
        //console.log($scope.myFile);
        if (($scope.myFile == undefined || $scope.myFile == "") && $scope.CandidateResumeDownload == "") {
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
                //myService.uploadFile(UploadURL, payload).then(function (response) {
                //    //success, file uploaded
                //    //$rootScope.resultMessage = "Resume Uploaded Successfully.";
                //    //showNotification('success');
                //    $scope.UpdateCandidateDetailsAfterFileUpload($rootScope.loggedInUserDetails.UserID + "_" + file.name);
                //    manageLoader();
                //}).catch(function (response) {
                //    $rootScope.resultMessage = "Error while Uploading Resume. Please try again....";
                //    showNotification('error');
                //    manageLoader();
                //    //bummer
                //});
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
        var saveCandidateProfileURL = IAMInterviewed.Candidate.saveCandidateProfile;
        $http.post(saveCandidateProfileURL, objData).then(function (response) {
            if (response.data.Success == true) {
                $rootScope.resultMessage = "Data Saved Successfylly. Please wait while we redirect to Schecdule Interview ...";
                $timeout(function () {
                    var getDasbboardDetailsURL = IAMInterviewed.Candidate.getCandidateDashboardDetails + "?UserId=" + $rootScope.loggedInUserDetails.UserID;
                    $http.get(getDasbboardDetailsURL).then(function success(response) {
                        $rootScope.CandidateDashboardDetails = response.data.data;
                        $scope.RedirectUrl('ScheduleInterview');
                        //console.log($rootScope.CandidateDashboardDetails);
                        manageLoader();
                    }, function error(response) {
                        $rootScope.resultMessage = response.data.errorMessage;
                        showNotification('error');
                        manageLoader();
                        return false;
                    });
                }, 2000);
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
            //DatePickerfunc();
            //$(".select2").select2();
        });
    });
}]);