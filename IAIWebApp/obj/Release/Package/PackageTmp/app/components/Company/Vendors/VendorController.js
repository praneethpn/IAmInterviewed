angular.module("IAMInterviewed").controller('VendorController', ['$scope', '$http', '$q', '$filter', '$rootScope', '$timeout', '$window', '$localStorage', function ($scope, $http, $q, $filter, $rootScope, $timeout, $window, $localStorage, $location) {
    $scope.ClearAll = function () {
        $scope.vendorName = "";
        $scope.vendorEmailId = "";
        $scope.vendorPhoneNo = "";
        $scope.startDate = "";
        $scope.endDate = "";
        //$scope.designationList = [];
        $scope.currentPage = 1;
        $scope.numPerPage = 20;
    }
    $scope.ClearAll();

    $scope.getAllVendors = function () {
        var getAllVendorsURL = IAMInterviewed.Company.getAllVendors + "?companyId=" + $rootScope.loggedInUserDetails.UserID;
        $http.get(getAllVendorsURL).then(function success(response) {
            //console.log(response.data);
            $scope.vendorList = response.data.data;
            $scope.totalItems = $scope.vendorList.length;
        }, function error(response) {
            $rootScope.resultMessage = response.data.errorMessage;
            showNotification('error');
        });
    }

    $scope.getAllVendors();

    $scope.saveVendor = function () {
        var saveVendorURL = IAMInterviewed.Company.saveVendor + "?vendorName=" + $scope.vendorName + "&vendorEmail=" + $scope.vendorEmailId
            + "&vendorMobile=" + $scope.vendorPhoneNo + "&companyId=" + $rootScope.loggedInUserDetails.UserID + "&startDate=" + $scope.startDate + "&endDate=" + $scope.endDate;
        $http.get(saveVendorURL).then(function success(response) {
            $rootScope.resultMessage = "Vendor Added Successfully";
            showNotification('success');
            manageLoader();
            $scope.ClearAll();
            $scope.getAllVendors();
        }, function error(response) {
            $rootScope.resultMessage = response.data.errorMessage;
            showNotification('error');
        });
    }

    $scope.paginate = function (value) {
        var begin, end, index;
        begin = ($scope.currentPage - 1) * $scope.numPerPage;
        end = begin + $scope.numPerPage;
        index = $scope.vendorList.indexOf(value);
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