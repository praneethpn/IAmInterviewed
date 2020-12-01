angular.module('IAMInterviewed').controller('DashboardController', ['$scope', '$http', '$filter', 'DataServices', '$rootScope', 'LoaderService', '$timeout', '$window', '$localStorage', function ($scope, $http, $filter, DataServices, $rootScope, LoaderService, $timeout, $window, $localStorage, $location) {
    $scope.ClearAll = function () {
        var date = new Date();
        //date.setDate(date.getDate() - 1);
        $scope.StartDate = $scope.getDatFormatforLocal(date);
        $scope.EndDate = $scope.getDatFormatforLocal(date);
        //$scope.StartDate = $scope.getDatFormatforProduction(date);
        //$scope.EndDate = $scope.getDatFormatforProduction(date);
        $scope.RegisteredCandidates = "";
        $scope.InterviewsScheduled = "";
        $scope.RatedSchedules = "";
        $scope.RejectedProfiles = "";
        $scope.LoadDashboardDetails();
    }


    $scope.LoadDashboardDetails = function () {
        manageLoader('load');
        var getDashboardDetails = IAMInterviewed.Admin.GetDashboardDetails + "?StartDate=" + $scope.StartDate + "&EndDate=" + $scope.EndDate;
        $http.get(getDashboardDetails).then(function success(response) {
            //console.log(response.data);
            $scope.RegisteredCandidates = response.data.data[0].RegisteredUsers;
            $scope.InterviewsScheduled = response.data.data[0].InterviewsScheduled;
            $scope.RatedSchedules = response.data.data[0].CandidatedRated;
            $scope.RegisteredUsersOnTheirOwn = response.data.data[0].RegisteredUsersOnTheirOwn;
            $scope.CompaniesAddedProfiles = response.data.data[0].CompaniesAddedProfiles;
            $scope.RejectedProfiles = response.data.data[0].RejectedProfiles;
            manageLoader();
        },
        function error(response) {
            $rootScope.resultMessage = response.data.errorMessage;
            showNotification('error');
            manageLoader();
            return false;
        });
    }
    $scope.ClearAll();

    $(document).ready(function () {
        //$('.txtDate').datepicker({
        //    dateFormat: 'mm-dd-yy'
        //});
        $timeout(function () {
            DatePickerfunc();
        });
    });
}]);