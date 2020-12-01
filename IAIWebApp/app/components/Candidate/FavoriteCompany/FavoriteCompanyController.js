angular.module('IAMInterviewed').controller('FavoriteCompanyController', ['$scope', '$http', '$q', '$filter', '$rootScope', '$timeout', '$window', '$localStorage', 'myService', function ($scope, $http, $q, $filter, $rootScope, $timeout, $window, $localStorage, $location, myService) {
    $scope.ClearAll = function () {
        $scope.FavoriteCompany = "";
        $scope.Designation = "";
        $scope.Applicationid = "";
        $scope.EditMode = false;        
    }
    $scope.ClearAll();

    $scope.bindFavoriteCompany = function () {
        manageLoader('load');
        var getFavoriteCompanyURL = IAMInterviewed.Candidate.getFavoriteCompany + "?UserId=" + $rootScope.loggedInUserDetails.UserID;
        $http.get(getFavoriteCompanyURL).then(function success(response) {
            //console.log(response.data);
            $scope.FavoriteCompanies = response.data.data;
            manageLoader();
        },
        function error(response) {
            $rootScope.resultMessage = response.data.errorMessage;
            showNotification('error');
            manageLoader();
            return false;
        });
    }
    $scope.bindFavoriteCompany();

    $scope.loadFavoriteCompany = function () {
        manageLoader('load');
        var deferred = $q.defer();
        var fillFavoriteCompanyURL = IAMInterviewed.Candidate.fillFavoriteCompany + "?UserId=" + $rootScope.loggedInUserDetails.UserID;
        $http.get(fillFavoriteCompanyURL).then(function success(response) {
            //console.log(response.data);
            $scope.FavoriteCompaniesDDL = response.data.data;
            manageLoader();
            deferred.resolve(response.data.data);
        },
        function error(response) {
            $rootScope.resultMessage = response.data.errorMessage;
            showNotification('error');
            manageLoader();
            deferred.reject;
            return false;
        });
        return deferred.promise;
    }
    $scope.loadFavoriteCompany();

    $scope.loadDesignation = function () {
        manageLoader('load');
        var deferred = $q.defer();
        var fillDesignationURL = IAMInterviewed.Candidate.fillDesignation + "?CompanyId=" + $scope.FavoriteCompany;
        $http.get(fillDesignationURL).then(function success(response) {
            //console.log(response.data);
            $scope.Designations = response.data.data;
            manageLoader();
            deferred.resolve(response.data.data);
        },
        function error(response) {
            $rootScope.resultMessage = response.data.errorMessage;
            showNotification('error');
            manageLoader();
            deferred.reject;
            return false;
        });
        return deferred.promise;
    }

    $scope.editFavoriteCompany = function (objFavorite) {
        $scope.FavoriteCompany = objFavorite.CompanyId.toString();
        $scope.loadDesignation().then(function (response) {
            $scope.Designation = objFavorite.DesignationId.toString();
            $scope.Applicationid = objFavorite.Applicationid;
            $scope.EditMode = true;
        });        
    }

    $scope.saveFavoriteCompany = function () {
        if ($scope.FavoriteCompanies.length > 2 && $scope.Applicationid == "") {
            $rootScope.resultMessage = "You can apply for only 2 companies";
            showNotification('error');
        }
        else {
            manageLoader('load');
            var objData = {
                Company: $scope.FavoriteCompany,
                CandidateId: $rootScope.loggedInUserDetails.UserID,
                Designation: $scope.Designation,
                Applicationid: $scope.Applicationid
            }
            var saveFavoriteCompanyURL = IAMInterviewed.Candidate.saveFavoriteCompany;
            $http.post(saveFavoriteCompanyURL, objData).then(function (response) {
                if (response.data.Success == true) {
                    $rootScope.resultMessage = response.data.data;
                    $scope.bindFavoriteCompany();
                    showNotification('success');
                    manageLoader();
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
    }

    $(document).ready(function () {
        $timeout(function () {
            onComponentLoad();
            //DatePickerfunc();
            //$(".select2").select2();
        });
    });
}]);