angular.module("IAMInterviewed").controller('SubUserController', ['$scope', '$http', '$q', '$filter', '$rootScope', '$timeout', '$window', '$localStorage', function ($scope, $http, $q, $filter, $rootScope, $timeout, $window, $localStorage, $location) {
    $scope.ClearAll = function () {
        $scope.subUserName = "";
        $scope.subUserEmail = "";
        $scope.subUserType = "";
        //$scope.subUsersList = [];
        $scope.currentPage = 1;
        $scope.numPerPage = 20;
    }
    $scope.ClearAll();

    $scope.getAllSubUsers = function () {
        var getSubUsersURL = IAMInterviewed.Company.getSubUsers + "?companyId=" + $rootScope.loggedInUserDetails.UserID;
        $http.get(getSubUsersURL).then(function success(response) {
            //console.log(response.data);
            $scope.subUsersList = response.data.data;
            $scope.totalItems = $scope.subUsersList.length;
        }, function error(response) {
            $rootScope.resultMessage = response.data.errorMessage;
            showNotification('error');
        });
    }

    $scope.getAllSubUsers();

    $scope.saveSubUser = function () {
        manageLoader('load');
        var objData = {
            Username: $scope.subUserName,
            EmailId: $scope.subUserEmail,
            Skill: "",
            Password: "",
            SkillType: "Company",
            UserID: $rootScope.loggedInUserDetails.UserID,
            UserType: $scope.subUserType
        }
        var saveSubUserURL = IAMInterviewed.Company.saveSubUser;
        $http.post(saveSubUserURL, objData).then(function (response) {
            if (response.data.Success == true) {
                $rootScope.resultMessage = "Sub User Saved Successfully";                
                showNotification('success');
                manageLoader();
                $scope.ClearAll();
                $scope.getAllSubUsers();
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

    $scope.paginate = function (value) {
        var begin, end, index;
        begin = ($scope.currentPage - 1) * $scope.numPerPage;
        end = begin + $scope.numPerPage;
        index = $scope.subUsersList.indexOf(value);
        return (begin <= index && index < end);
    };
}]);