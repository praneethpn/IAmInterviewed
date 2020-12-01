angular.module("IAMInterviewed").controller('ShortlistedProfilesController', ['$scope', '$http', '$q', '$filter', '$rootScope', '$timeout', '$window', '$localStorage', function ($scope, $http, $q, $filter, $rootScope, $timeout, $window, $localStorage, $location) {
    $scope.ClearAll = function () {
        $scope.secondarySkillSearch = "";
        $scope.totalRatingSearch = "";
        $scope.locationSearch = "";
        $scope.secSkill1RatingSearch = "";
        $scope.candidateNameSearch = "";
        $scope.selectAllProfiles = false;
        $scope.selectedCandidatesList = [];
        $scope.currentPage = 1;
        $scope.numPerPage = 20;
    }
    $scope.ClearAll();
    if ($rootScope.objRequirement == undefined || $rootScope.objRequirement == "") {
        $scope.RedirectUrl('AllPostingsDashboard');
    }

    $scope.LoadSecondarySkills = function () {
        var GetSecondarySkillsURL = IAMInterviewed.Skills.GetSecondarySkills + "?PrimarySkill=" + $rootScope.objRequirement.PrimarySkill;
        $http.get(GetSecondarySkillsURL).then(function success(response) {
            //console.log(response.data);
            $scope.SecondarySkills = response.data.data;
        }, function error(response) {
            $rootScope.resultMessage = response.data.errorMessage;
            showNotification('error');
        });
    }
    $scope.LoadSecondarySkills();

    $scope.getSelectedCandidates = function () {
        manageLoader('load');
        var getReqRelatedCandidatesURL = IAMInterviewed.Company.getSelectedCandidates + "?reqId=" + $rootScope.objRequirement.ReqId + "&secSkill1=" + $scope.secondarySkillSearch
            + "&totalRating=" + $scope.totalRatingSearch + "&secSkillRating=" + $scope.secSkill1RatingSearch + "&candidateName=" + $scope.candidateNameSearch
            + "&companyId=" + $rootScope.loggedInUserDetails.UserId;
        $http.get(getReqRelatedCandidatesURL).then(function success(response) {
            $scope.selectedCandidatesList = response.data.data;
            $scope.totalItems = $scope.selectedCandidatesList.selectedCandidates.length;
            //console.log($scope.selectedCandidatesList);
            manageLoader();
        }, function error(response) {
            $rootScope.resultMessage = response.data.errorMessage;
            showNotification('error');
            manageLoader();
        });
    }
    $scope.getSelectedCandidates();

    $scope.selectAllFunction = function () {
        angular.forEach($scope.selectedCandidatesList, function (value, key) {
            if (value.CanidateShortlisted == true) {
                value.CanidateShortlisted = false;
            }
            else {
                value.CanidateShortlisted = true;
            }
        });
    }

    $scope.updateStatus = function (objCandidate) {
        manageLoader('load');
        var updateFutureDateURL = IAMInterviewed.Company.updateFutureDate + "?reqId=" + $rootScope.objRequirement.ReqId + "&candiateId=" + objCandidate.CandidateId
            + "&futureUpdateDate=" + objCandidate.DisplayDate + "&comments=" + objCandidate.Comments + "&status=" + objCandidate.Status;
        $http.get(updateFutureDateURL).then(function success(response) {
            $rootScope.resultMessage = "Updated Successfully.";
            showNotification('success');
            manageLoader();
        }, function error(response) {
            $rootScope.resultMessage = response.data.errorMessage;
            showNotification('error');
            manageLoader();
        });
    }

    $scope.getHistory = function (objCandidate) {
        manageLoader('load');
        var selectedProfilesDumpURL = IAMInterviewed.Company.selectedProfilesDump + "?candiateId=" + objCandidate.CandidateId + "&reqId=" + $rootScope.objRequirement.ReqId;
        $http.get(selectedProfilesDumpURL).then(function success(response) {
            $scope.candidateHstoryList = response.data.data;
            $("#historyModel").modal('show');
            manageLoader();
        }, function error(response) {
            $rootScope.resultMessage = response.data.errorMessage;
            showNotification('error');
            manageLoader();
        });
    }

    $scope.requestAccess = function (objCandidate) {
        manageLoader('load');
        var requestAccessURL = IAMInterviewed.Company.saveCompanyRequestAccess + "?reqId=" + $rootScope.objRequirement.ReqId + "&candidateId=" + objCandidate.CandidateId;
        $http.get(requestAccessURL).then(function success(response) {
            $rootScope.resultMessage = response.data.data;
            showNotification('success');
            manageLoader();
        }, function error(response) {
            $rootScope.resultMessage = response.data.errorMessage;
            showNotification('error');
            manageLoader();
        });
    }

    $scope.viewRatingDetails = function (objCandidateRating) {
        $scope.$broadcast('bindCandidateRatingDetails', objCandidateRating.ReqId, objCandidateRating.ScheduleId);        
    }

    $scope.paginate = function (value) {
        var begin, end, index;
        begin = ($scope.currentPage - 1) * $scope.numPerPage;
        end = begin + $scope.numPerPage;
        index = $scope.selectedCandidatesList.selectedCandidates.indexOf(value);
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