angular.module('IAMInterviewed').controller('ReferFriendController', ['$scope', '$http', '$filter', 'DataServices', '$rootScope', 'LoaderService', '$timeout', '$window', '$localStorage', function ($scope, $http, $filter, DataServices, $rootScope, LoaderService, $timeout, $window, $localStorage, $location) {
    $scope.ClearAll = function () {
        $scope.Name = "";
        $scope.EmailAddress = "";
        $scope.PrimarySkill = "";
    }
    $scope.ClearAll();

    $scope.LoadPrimarySkills = function () {
        manageLoader('load');
        var getPrimarySkillsURL = IAMInterviewed.Skills.GetSkills;
        $http.get(getPrimarySkillsURL).then(function success(response) {
            //console.log(response.data);
            $scope.PrimarySkills = response.data.data;
            manageLoader();
        }, function error(response) {
            $rootScope.resultMessage = response.data.errorMessage;
            showNotification('error');
            manageLoader();
        });
    }
    $scope.LoadPrimarySkills();

    $scope.referFriend = function () {
        manageLoader('load');
        var referUserURL = IAMInterviewed.Interviewer.referUser + "?Name=" + $scope.Name + "&Email=" + $scope.EmailAddress + "&Skill=" + $scope.PrimarySkill + "&Type="
            + $rootScope.loggedInUserDetails.UserID + "&ReferedUser=" + $rootScope.loggedInUserDetails.Username;
        $http.get(referUserURL).then(function success(response) {
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
    }

    $(document).ready(function () {
        //$('.txtDate').datepicker({
        //    dateFormat: 'mm-dd-yy'
        //});
        $timeout(function () {
            onComponentLoad();
            DatePickerfunc();
            $(".select2").select2();
        });
    });
}]);