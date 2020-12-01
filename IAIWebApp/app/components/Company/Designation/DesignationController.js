angular.module("IAMInterviewed").controller('DesignationController', ['$scope', '$http', '$q', '$filter', '$rootScope', '$timeout', '$window', '$localStorage', function ($scope, $http, $q, $filter, $rootScope, $timeout, $window, $localStorage, $location) {
    $scope.ClearAll = function () {
        $scope.designationId = 0;
        $scope.designationName = "";
        $scope.description = "";
        //$scope.designationList = [];
        $scope.currentPage = 1;
        $scope.numPerPage = 20;
    }
    $scope.ClearAll();

    $scope.getAllDesignations = function () {
        var getDesignationURL = IAMInterviewed.Company.getDesignation + "?companyId=" + $rootScope.loggedInUserDetails.UserID;
        $http.get(getDesignationURL).then(function success(response) {
            //console.log(response.data);
            $scope.designationList = response.data.data;
            $scope.totalItems = $scope.designationList.length;
        }, function error(response) {
            $rootScope.resultMessage = response.data.errorMessage;
            showNotification('error');
        });
    }

    $scope.getAllDesignations();

    $scope.saveDesignation = function () {
        var saveDesignationURL = IAMInterviewed.Company.saveDesignation + "?designationId=" + $scope.designationId + "&designation=" + $scope.designationName
            + "&description=" + $scope.description + "&companyId=" + $rootScope.loggedInUserDetails.UserID;
        $http.get(saveDesignationURL).then(function success(response) {
            $rootScope.resultMessage = "Designation Added Successfully";
            showNotification('success');
            manageLoader();
            $scope.ClearAll();
            $scope.getAllDesignations();
        }, function error(response) {
            $rootScope.resultMessage = response.data.errorMessage;
            showNotification('error');
        });
    }

    $scope.editDesignation = function (objDesignation) {
        $scope.designationId = objDesignation.DesignationId;
        $scope.designationName = objDesignation.Designation;
        $scope.description = objDesignation.Description;
        $(".scroll-to-top").click();
    }

    $scope.paginate = function (value) {
        var begin, end, index;
        begin = ($scope.currentPage - 1) * $scope.numPerPage;
        end = begin + $scope.numPerPage;
        index = $scope.designationList.indexOf(value);
        return (begin <= index && index < end);
    };
}]);