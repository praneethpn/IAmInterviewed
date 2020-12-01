angular.module('IAMInterviewed').controller('ApplyJobsController', ['$scope', '$http', '$q', '$filter', '$rootScope', '$timeout', '$window', '$localStorage', 'myService', function ($scope, $http, $q, $filter, $rootScope, $timeout, $window, $localStorage, $location, myService) {
    $scope.ClearAll = function () {
        $scope.PrimarySkill = "";
        $scope.RelatedRequirements = "";
        $scope.SecondarySkills = "";
        $scope.AppliedRequirements = "";
        $scope.Company = "";
        $scope.SecondarySkill = "";
        $scope.Role = "";
        $scope.objRequirement = "";
        $scope.ViewMode = "Requirements";
        $scope.viewType = "";
    }
    $scope.ClearAll();

    $scope.loadCandidateRelatedRequirements = function () {
        manageLoader('load');
        var deferred = $q.defer();
        var getCandidateRelatedRequirementsURL = IAMInterviewed.Candidate.getCandidateRelatedRequirements + "?CandidateId=" + $rootScope.loggedInUserDetails.UserID + "&Company=" + $scope.Company
        + "&SecSkill=" + $scope.SecondarySkill + "&Role=" + $scope.Role;
        $http.get(getCandidateRelatedRequirementsURL).then(function success(response) {
            $scope.RelatedRequirements = response.data.data;
            //console.log($scope.RelatedRequirements);
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

    $scope.loadCandidateAppliedRequirements = function () {
        manageLoader('load');
        var deferred = $q.defer();
        var getCandidateAppliedRequirementsURL = IAMInterviewed.Candidate.getCandidateAppliedRequirements + "?CandidateId=" + $rootScope.loggedInUserDetails.UserID + "&Company=" + $scope.Company;
        $http.get(getCandidateAppliedRequirementsURL).then(function success(response) {
            $scope.AppliedRequirements = response.data.data;
            //console.log($scope.AppliedRequirements);
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

    $scope.BindApplyJobs = function () {
        $scope.loadCandidateRelatedRequirements().then(function (response) {
            $scope.PrimarySkill = $scope.RelatedRequirements.Requirements[0].PrimarySkill.toString();
            $scope.LoadSecondarySkills().then(function (response) {
                $scope.loadCandidateAppliedRequirements();
            });
        });
    }
    $scope.BindApplyJobs();

    $scope.viewJD = function (viewType, objRequirement) {
        manageLoader('load');
        var deferred = $q.defer();
        var getCompanyJDURL = IAMInterviewed.Candidate.getCompanyJD + "?ReqId=" + objRequirement.ReqId;
        $http.get(getCompanyJDURL).then(function success(response) {
            $scope.objRequirement = response.data.data[0];
            $scope.ViewMode = "JD";
            $scope.viewType = viewType;
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

    $scope.ApplyToJob = function (objRequirement) {
        manageLoader('load');
        var saveCandidateApplicationURL = IAMInterviewed.Candidate.saveCandidateApplication + "?CandidateId=" + $rootScope.loggedInUserDetails.UserID + "&ReqId=" + objRequirement.ReqId
        + "&JobTitle=" + objRequirement.JobTitle;
        $http.get(saveCandidateApplicationURL).then(function success(response) {
            if (response.data.Success == true) {
                $rootScope.resultMessage = response.data.data;
                $scope.BindApplyJobs();
                showNotification('success');
                manageLoader();
            }
            else {
                $rootScope.resultMessage = response.data.errorMessage;
                showNotification('error');
                manageLoader();
            }
        }, function error(response) {
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