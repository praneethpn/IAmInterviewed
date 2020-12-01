angular.module("IAMInterviewed").controller('PreAppliedCandidatesController', ['$scope', '$http', '$q', '$filter', '$rootScope', '$timeout', '$window', '$localStorage', function ($scope, $http, $q, $filter, $rootScope, $timeout, $window, $localStorage, $location) {
    $scope.ClearAll = function () {
        $scope.preAppliedCandidatesList = [];
        $scope.fanProfilesList = [];
        $scope.currentPage = 1;
        $scope.numPerPage = 20;
    }
    $scope.ClearAll();
    if ($rootScope.objRequirement == undefined || $rootScope.objRequirement == "") {
        $scope.RedirectUrl('AllPostingsDashboard');
    }

    $scope.getPreAppliedCandidates = function () {
        manageLoader('load');
        var getAppliedCandidatesURL = IAMInterviewed.Company.getAppliedCandidates + "?reqId=" + $rootScope.objRequirement.ReqId;
        $http.get(getAppliedCandidatesURL).then(function success(response) {
            $scope.preAppliedCandidatesList = response.data.data;
            $scope.totalItems = $scope.preAppliedCandidatesList.length;
            //console.log($scope.preAppliedCandidatesList);
            $scope.getFanProfiles();
            manageLoader();
        }, function error(response) {
            $rootScope.resultMessage = response.data.errorMessage;
            showNotification('error');
            manageLoader();
        });
    }    

    $scope.getFanProfiles = function () {
        manageLoader('load');
        var getFanProfilesURL = IAMInterviewed.Company.getFanProfiles + "?candiateId=" + $rootScope.loggedInUserDetails.UserID;
        $http.get(getFanProfilesURL).then(function success(response) {
            $scope.fanProfilesList = response.data.data;
            console.log($scope.fanProfilesList);
            manageLoader();
        }, function error(response) {
            $rootScope.resultMessage = response.data.errorMessage;
            showNotification('error');
            manageLoader();
        });
    }
    $scope.getPreAppliedCandidates();

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

    $scope.shortlistCandidateMulti = function (objCandidate) {
        manageLoader('load');
        var candidateList = [];
        angular.forEach($scope.preAppliedCandidatesList, function (value, key) {
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
        index = $scope.preAppliedCandidatesList.indexOf(value);
        return (begin <= index && index < end);
    };
}]);