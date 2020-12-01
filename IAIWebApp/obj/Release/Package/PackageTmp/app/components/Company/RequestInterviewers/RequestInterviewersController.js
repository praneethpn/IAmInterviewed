angular.module("IAMInterviewed").controller('RequestInterviewersController', ['$scope', '$http', '$q', '$filter', '$rootScope', '$timeout', '$window', '$localStorage', function ($scope, $http, $q, $filter, $rootScope, $timeout, $window, $localStorage, $location) {
    $scope.ClearAll = function () {
        $scope.noOfInterviewers = "";
        $scope.location = "";
        $scope.minExperience = "";
        $scope.maxExperience = "";
        $scope.primarySkill = "";
        $scope.secondarySkill1 = "";
        $scope.secondarySkill2 = "";
        $scope.secondarySkill3 = "";
        $scope.secondarySkill4 = "";
        $scope.secondarySkill5 = "";
        //$scope.additionalSkills = "";
        $scope.startDate = "";
        $scope.endDate = "";
        $scope.description = "";
        $scope.remarks = "";
        $scope.requestId = 0;
    }
    $scope.ClearAll();

    $scope.LoadPrimarySkills = function () {
        var deferred = $q.defer();
        var getPrimarySkills = IAMInterviewed.Skills.GetSkills;
        $http.get(getPrimarySkills).then(function success(response) {
            //console.log(response.data);
            $scope.PrimarySkills = response.data.data;
            deferred.resolve(response.data.data);
        }, function error(response) {
            $rootScope.resultMessage = response.data.errorMessage;
            showNotification('error');
            deferred.reject;
        });
        return deferred.promise;
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
        var deferred = $q.defer();
        var getCitiesURL = IAMInterviewed.Skills.getCities + "?Country=" + $rootScope.loggedInUserDetails.Country;
        $http.get(getCitiesURL).then(function success(response) {
            //console.log(response.data);
            $scope.Cities = response.data.data;
            deferred.resolve(response.data.data);
        }, function error(response) {
            $rootScope.resultMessage = response.data.errorMessage;
            showNotification('error');
            deferred.reject;
        });
        return deferred.promise;
    }

    $scope.LoadPrimarySkills();
    $scope.LoadCities();

    $scope.getRequestInterviewers = function () {
        manageLoader('load');
        var getRequestInterviewersURL = IAMInterviewed.Company.getRequestInterviewers + "?companyId=" + $rootScope.loggedInUserDetails.UserID;
        $http.get(getRequestInterviewersURL).then(function success(response) {
            $scope.requestInterviewers = response.data.data;
            manageLoader();
        }, function error(response) {
            $rootScope.resultMessage = response.data.errorMessage;
            showNotification('error');
            manageLoader();
        });
    }

    $scope.getRequestInterviewers();

    $scope.saveRequestInterviewers = function () {
        manageLoader('load');
        var objData = {
            ReqId: $scope.requestId,
            NoOfInterviewers: $scope.noOfInterviewers,
            Location: $scope.location,
            MinExp: $scope.minExperience,
            MaxExp: $scope.maxExperience,
            PrimarySkill: $scope.primarySkill,
            SecSkill1: $scope.secondarySkill1,
            SecSkill2: $scope.secondarySkill2,
            SecSkill3: $scope.secondarySkill3,
            SecSkill4: $scope.secondarySkill4,
            SecSkill5: $scope.secondarySkill5,
            StartDate: $scope.startDate,
            EndDate: $scope.endDate,
            JobDesc: $scope.description,
            Remarks: $scope.remarks,
            UserId: $rootScope.loggedInUserDetails.UserID
        }
        var saveRequestInterviewersURL = IAMInterviewed.Company.saveRequestInterviewers;
        $http.post(saveRequestInterviewersURL, objData).then(function (response) {
            if (response.data.Success == true) {
                $rootScope.resultMessage = "Requested Successfylly.";
                showNotification('success');
                manageLoader();
                $scope.ClearAll();
                //request_Interviewer_form.$setUntouched();
                //$('#form_wizard_1').bootstrapWizard('show', 1);
                $scope.getRequestInterviewers();
                //$timeout(function () {
                //    $scope.RedirectUrl('RequestInterviewers');
                //}, 100);
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

    $scope.editRequest = function (objRequest) {
        $scope.primarySkill = objRequest.PrimarySkill.toString();
        $scope.LoadSecondarySkills().then(function (response) {
            $scope.noOfInterviewers = parseInt(objRequest.NoOfInterviewers);
            $scope.location = objRequest.Location;
            $scope.minExperience = objRequest.MinExp.toString();
            $scope.maxExperience = objRequest.MaxExp.toString();
            $scope.secondarySkill1 = objRequest.SecSkill1.toString();
            $scope.secondarySkill2 = objRequest.SecSkill2.toString();
            $scope.secondarySkill3 = objRequest.SecSkill3.toString();
            $scope.secondarySkill4 = objRequest.SecSkill4.toString();
            $scope.secondarySkill5 = objRequest.SecSkill5.toString();
            //$scope.additionalSkills = objRequest.;
            $scope.startDate = objRequest.DisplayStartDate;
            $scope.endDate = objRequest.DisplayEndDate;
            $scope.description = objRequest.JobDesc;
            $scope.remarks = objRequest.Remarks;
            $scope.requestId = objRequest.ReqId;
            $(".scroll-to-top").click();
        });
    }

    $(document).ready(function () {
        $timeout(function () {
            onComponentLoad();
            DatePickerfuncAllDatesddmm();
        });
    });
}]);