angular.module("IAMInterviewed").controller('ReqRelatedProfilesController', ['$scope', '$http', '$q', '$filter', '$rootScope', '$timeout', '$window', '$localStorage', function ($scope, $http, $q, $filter, $rootScope, $timeout, $window, $localStorage, $location) {
    console.log($rootScope.objRequirement);
    $scope.ClearAll = function () {
        $scope.secondarySkillSearch = "";
        $scope.totalRatingSearch = "";
        $scope.locationSearch = "";
        $scope.experienceSearch = "";
        $scope.secSkill1RatingSearch = "";
        $scope.additionalSkillsSearch = "";
        $scope.selectAllProfiles = false;
        $scope.reqRelatedCandidatesList = [];
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

    $scope.LoadCities = function () {
        var getCitiesURL = IAMInterviewed.Skills.getCities + "?Country=" + $rootScope.loggedInUserDetails.Country;
        $http.get(getCitiesURL).then(function success(response) {
            //console.log(response.data);
            $scope.Cities = response.data.data;
        }, function error(response) {
            $rootScope.resultMessage = response.data.errorMessage;
            showNotification('error');
        });
    }
    $scope.LoadCities();

    $scope.getReqRelatedCandidates = function () {
        manageLoader('load');
        var getReqRelatedCandidatesURL = IAMInterviewed.Company.getReqRelatedCandidates + "?reqId=" + $rootScope.objRequirement.ReqId + "&secSkill1=" + $scope.secondarySkillSearch
            + "&totalRating=" + $scope.totalRatingSearch + "&secSkillRating=" + $scope.secSkill1RatingSearch + "&location=" + $scope.locationSearch + "&experience=" + $scope.experienceSearch
            + "&additionalSkills=" + $scope.additionalSkillsSearch + "&EmailId=" + $rootScope.loggedInUserDetails.Country;
        $http.get(getReqRelatedCandidatesURL).then(function success(response) {
            $scope.reqRelatedCandidatesList = response.data.data;
            $scope.totalItems = $scope.reqRelatedCandidatesList.length;
            //console.log($scope.reqRelatedCandidatesList);
            manageLoader();
        }, function error(response) {
            $rootScope.resultMessage = response.data.errorMessage;
            showNotification('error');
            manageLoader();
        });
    }
    $scope.getReqRelatedCandidates();

    $scope.shortlistCandidate = function (objCandidate) {
        manageLoader('load');
        var getReqRelatedCandidatesURL = IAMInterviewed.Company.shortlistCandidate + "?reqId=" + $rootScope.objRequirement.ReqId + "&candidateId=" + objCandidate.CandidateId
            + "&scheduleId=" + objCandidate.ScheduleId + "&userId=" + $rootScope.loggedInUserDetails.UserID + "&grantAccess=" + objCandidate.HasCandidateAccess + "&candidateName=" + objCandidate.CandidateName
            + "&jobCode=" + $rootScope.objRequirement.JobCode + "&emailId=" + objCandidate.Email;
        $http.get(getReqRelatedCandidatesURL).then(function success(response) {
            $rootScope.resultMessage = "Shortlisted Successfully";
            showNotification('success');
            manageLoader();
            $scope.getReqRelatedCandidates();
        }, function error(response) {
            $rootScope.resultMessage = response.data.errorMessage;
            showNotification('error');
            manageLoader();
        });
    }

    $scope.selectAllFunction = function () {
        angular.forEach($scope.reqRelatedCandidatesList, function (value, key) {
            if (value.CanidateShortlisted == true) {
                value.CanidateShortlisted = false;
            }
            else {
                value.CanidateShortlisted = true;
            }
        });
    }

    $scope.shortlistCandidateMulti = function (objCandidate) {
        manageLoader('load');
        var candidateList = [];
        angular.forEach($scope.reqRelatedCandidatesList, function (value, key) {
            if (value.CanidateShortlisted == true) {
                var objcandidate = {
                    reqId: $rootScope.objRequirement.ReqId,
                    candidateId: value.CandidateId,
                    scheduleId: value.ScheduleId,
                    userId: $rootScope.loggedInUserDetails.UserID,
                    grantAccess: value.HasCandidateAccess,
                    candidateName: value.CandidateName,
                    jobCode: $rootScope.objRequirement.JobCode,
                    emailId: value.Email
                }
                candidateList.push(objcandidate);
            }
        });
        if (candidateList.length > 0) {
            var getReqRelatedCandidatesURL = IAMInterviewed.Company.shortlistCandidateBulk;
            $http.post(getReqRelatedCandidatesURL, candidateList).then(function success(response) {
                $rootScope.resultMessage = "Shortlisted Successfully";
                showNotification('success');
                manageLoader();
                $scope.getReqRelatedCandidates();
            }, function error(response) {
                $rootScope.resultMessage = response.data.errorMessage;
                showNotification('error');
                manageLoader();
            });
        }
    }

    $scope.viewRatingDetails = function (objCandidateRating) {
        $scope.$broadcast('bindCandidateRatingDetails', objCandidateRating.ReqId, objCandidateRating.ScheduleId);
        //$("#candidateratingdetails").modal('show');
    }
    $scope.paginate = function (value) {
        var begin, end, index;
        begin = ($scope.currentPage - 1) * $scope.numPerPage;
        end = begin + $scope.numPerPage;
        index = $scope.reqRelatedCandidatesList.indexOf(value);
        return (begin <= index && index < end);
    };

}]);