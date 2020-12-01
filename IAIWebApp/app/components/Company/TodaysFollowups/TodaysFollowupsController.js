angular.module("IAMInterviewed").controller('TodaysFollowupsController', ['$scope', '$http', '$q', '$filter', '$rootScope', '$timeout', '$window', '$localStorage', function ($scope, $http, $q, $filter, $rootScope, $timeout, $window, $localStorage, $location) {
    $scope.ClearAll = function () {
        $scope.startDateSearch = "";
        $scope.endDateSearch = "";
        $scope.followUpProfilesList = [];
        $scope.currentPage = 1;
        $scope.numPerPage = 20;
    }
    $scope.ClearAll();

    $scope.getFollowUpProfiles = function () {
        manageLoader('load');
        var followUpProfilesURL = IAMInterviewed.Company.followUpProfiles + "?companyId=" + $rootScope.loggedInUserDetails.UserID + "&fromDate=" + $scope.startDateSearch
            + "&toDate=" + $scope.endDateSearch;
        $http.get(followUpProfilesURL).then(function success(response) {
            $scope.followUpProfilesList = response.data.data;
            $scope.totalItems = $scope.followUpProfilesList.length;
            //console.log($scope.followUpProfilesList);
            manageLoader();
        }, function error(response) {
            $rootScope.resultMessage = response.data.errorMessage;
            showNotification('error');
            manageLoader();
        });
    }
    //$scope.getFollowUpProfiles();

    $scope.updateStatus = function (objCandidate) {
        manageLoader('load');
        var updateFutureDateURL = IAMInterviewed.Company.updateTrackingStatus + "?reqId=" + objCandidate.ReqId + "&candiateId=" + objCandidate.CandidateId
            + "&futureUpdateDate=" + objCandidate.DisplayDate + "&comments=" + objCandidate.Comments + "&trackingStatus=" + objCandidate.TrackingStatus;
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
        var selectedProfilesDumpURL = IAMInterviewed.Company.selectedProfilesDump + "?candiateId=" + objCandidate.CandidateId + "&reqId=" + objCandidate.ReqId;
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

    $scope.paginate = function (value) {
        var begin, end, index;
        begin = ($scope.currentPage - 1) * $scope.numPerPage;
        end = begin + $scope.numPerPage;
        index = $scope.followUpProfilesList.indexOf(value);
        return (begin <= index && index < end);
    };

    $(document).ready(function () {
        $timeout(function () {
            //onComponentLoad();
            DatePickerfuncAllDates();
            //$(".select2").select2();
        });
    });
}]);