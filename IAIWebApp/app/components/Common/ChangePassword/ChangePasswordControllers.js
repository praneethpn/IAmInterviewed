angular.module("IAMInterviewed").controller('ChangePasswordController', ['$scope', '$http', '$q', '$filter', '$rootScope', '$timeout', '$window', '$localStorage', function ($scope, $http, $q, $filter, $rootScope, $timeout, $window, $localStorage, $location) {
    $scope.ClearAll = function () {
        $scope.CurrentPassword = "";
        $scope.NewPassword = "";
        $scope.ConfirmPassword = "";
    }
    $scope.ClearAll();

    $scope.changePassword = function () {
        if ($scope.CurrentPassword != $rootScope.loggedInUserDetails.Password) {
            $rootScope.resultMessage = "Current password is Invalid";
            showNotification('error');
        }
        else if ($scope.NewPassword != $scope.ConfirmPassword) {
            $rootScope.resultMessage = "New password and Confirm Password didnot match";
            showNotification('error');
        }
        else {
            manageLoader('load');
            var changePasswordURL = IAMInterviewed.userManagent.changePassword + "?Email=" + $rootScope.loggedInUserDetails.EmailID + "&Newpassword=" + $scope.ConfirmPassword;
            $http.get(changePasswordURL).then(function success(response) {
                if (response.data.Success == true) {
                    $rootScope.resultMessage = response.data.data;
                    showNotification('success');                    
                    $rootScope.loggedInUserDetails.LoginCount = "1";
                    $rootScope.loggedInUserDetails.Password = $scope.ConfirmPassword;
                    $timeout(function () {
                        $scope.RedirectUrl('Dashboard');
                    }, 100);                    
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
    }
}]);