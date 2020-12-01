angular.module('IAMInterviewed').controller('ScheduleController', ['$scope', '$http', '$q', '$filter', 'DataServices', '$rootScope', 'LoaderService', '$timeout', '$window', '$localStorage', function ($scope, $http, $q, $filter, DataServices, $rootScope, LoaderService, $timeout, $window, $localStorage, $location) {
    $scope.ClearAll = function () {
        $scope.ScheduleDate = "";
        $scope.TimeSlotsAvailable = [];
        $scope.TimeSlotsPublished = [];
        $scope.TimeSlotsSelected = "";
        $scope.InterviewerSchedules = [];
    }
    $scope.ClearAll();

    $scope.loadSchedules = function () {
        manageLoader('load');
        var deferred = $q.defer();
        var getTimeSlotsURL = IAMInterviewed.Interviewer.getTimeSlots + "?UserId=" + $rootScope.loggedInUserDetails.UserID + "&ScheduleDate=" + $scope.ScheduleDate;
        $http.get(getTimeSlotsURL).then(function success(response) {
            $scope.TimeSlots = response.data.data;
            $scope.TimeSlotsAvailable = [];
            angular.forEach($scope.TimeSlots.AllTimeSlots, function (value, key) {
                $scope.TimeSlotsAvailable.push(value.DailyScheduleTime);
            });

            angular.forEach($scope.TimeSlots.PublishedTimeSlots, function (value, key) {
                var objTags = {
                    text: value.AvailableTime
                }
                $scope.InterviewerSchedules.push(objTags);
            });
            //$scope.TimeSlotsPublished = response.data.data.PublishedTimeSlots;
            //console.log($scope.TimeSlotsAvailable);
            manageLoader();
        }, function error(response) {
            $rootScope.resultMessage = response.data.errorMessage;
            showNotification('error');
            manageLoader();
            deferred.reject;
        });
        return deferred.promise;
    }

    $scope.loadSchedulesTags = function (query, tags) {
        var deferred = $q.defer();
        tags = $.grep(tags, function (value, i) {
            return value.toLowerCase().startsWith(query.toLowerCase());
        });
        deferred.resolve(tags);
        return deferred.promise;
    };

    $scope.saveSchedule = function () {
        var selectedTimeSlots = [];
        angular.forEach($scope.InterviewerSchedules, function (value, key) {
            var objSchedule = $.grep($scope.TimeSlots.AllTimeSlots, function (element, index) {
                return element.DailyScheduleTime == value.text;
            });
            if (objSchedule.length > 0) {
                selectedTimeSlots.push(objSchedule[0].DailyScheduleId);
            }
            else {
                var objPublishedSchedule = $.grep($scope.TimeSlots.PublishedTimeSlots, function (element, index) {
                    return element.AvailableTime == value.text;
                });
                if (objPublishedSchedule.length > 0) {
                    selectedTimeSlots.push(objPublishedSchedule[0].DailyScheduleId);
                }
            }
        });
        $scope.TimeSlotsSelected = selectedTimeSlots.toString();
        manageLoader('load');
        var saveInterviewerScheduleURL = IAMInterviewed.Interviewer.saveInterviewerSchedule + "?UserId=" + $rootScope.loggedInUserDetails.UserID + "&ScheduleDate=" + $scope.ScheduleDate + "&AvailableTime=" + $scope.TimeSlotsSelected;
        $http.get(saveInterviewerScheduleURL).then(function success(response) {
            if (response.data.Success == true) {
                $rootScope.resultMessage = response.data.data;
                showNotification('success');
                $scope.ClearAll();
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
        //console.log($scope.TimeSlotsSelected);
    }

    $scope.searchSchedule = function () {
        manageLoader('load');
        var getInterviewerSchedulesByDateURL = IAMInterviewed.Interviewer.getInterviewerSchedulesByDate + "?UserId=" + $rootScope.loggedInUserDetails.UserID + "&ScheduleDate=" + $scope.ScheduleDateSearch;
        $http.get(getInterviewerSchedulesByDateURL).then(function success(response) {
            $scope.AddedSchedulesByDate = response.data.data;
            //console.log($scope.AddedSchedulesByDate);
            manageLoader();
        }, function error(response) {
            $rootScope.resultMessage = response.data.errorMessage;
            showNotification('error');
            manageLoader();
        });
    }

    $(document).ready(function () {
        $timeout(function () {
            onComponentLoad();
            DatePickerfunc();
            $(".select2").select2();
        });
    });
}]);