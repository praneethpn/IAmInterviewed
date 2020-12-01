angular.module("IAMInterviewed").controller('CompanyProfileController', ['$scope', '$http', '$q', '$filter', '$rootScope', '$timeout', '$window', '$localStorage', function ($scope, $http, $q, $filter, $rootScope, $timeout, $window, $localStorage, $location) {
    $scope.ClearAll = function () {
        $scope.companyPhoneNo = "";
        $scope.companyWebsite = "";
        $scope.contactPersonName = "";
        $scope.contactPersonPhone = "";
        $scope.contactPersonEmail = "";
        $scope.companyAddress = "";
        $scope.companyLogo = "";
        $scope.companyLogoDownload = "";
        $scope.DetailsId = "";
        $scope.companyProfile = [];
    }
    $scope.ClearAll();

    $scope.getCompanyProfile = function () {
        manageLoader('load');
        var getCompanyProfileURL = IAMInterviewed.Company.getCompanyProfile + "?companyId=" + $rootScope.loggedInUserDetails.UserID;
        $http.get(getCompanyProfileURL).then(function success(response) {
            $scope.companyProfile = response.data.data;
            if ($scope.companyProfile.length > 0) {
                $scope.bindCompanyDetails();
            }
            manageLoader();
        }, function error(response) {
            $rootScope.resultMessage = response.data.errorMessage;
            showNotification('error');
            manageLoader();
        });
    }

    $scope.bindCompanyDetails = function () {
        $scope.DetailsId = $scope.companyProfile[0].DetailsId;
        $scope.companyPhoneNo = parseInt($scope.companyProfile[0].Phone);
        $scope.companyWebsite = $scope.companyProfile[0].Website;
        $scope.contactPersonName = $scope.companyProfile[0].ContactName;
        $scope.contactPersonPhone = parseInt($scope.companyProfile[0].ContactPhone);
        $scope.contactPersonEmail = $scope.companyProfile[0].ContactEmail;
        $scope.companyAddress = $scope.companyProfile[0].Address;
        //$scope.companyLogo = "";
        $scope.companyLogoDownload = $scope.companyProfile[0].Logo;
    }

    $scope.getCompanyProfile();

    $scope.saveCompanyProfile = function () {
        manageLoader('load');
        //console.log($scope.myFile);
        if ($scope.myFile == undefined && $scope.myFile == "" && $scope.companyLogoDownload == "") {
            $rootScope.resultMessage = "Please upload Logo.";
            showNotification('error');
            manageLoader();
        }
        else {
            if ($scope.myFile != undefined && $scope.myFile != "") {
                $scope.UploadType = "CandidateResume";
                var file = $scope.myFile;
                var UploadURL = IAMInterviewed.Company.uploadCompanyLogo;
                var payload = new FormData();
                payload.append("companyId", $rootScope.loggedInUserDetails.UserID);
                payload.append("file", file);
                $http.post(UploadURL, payload, {
                    headers: { 'Content-Type': undefined },
                    transformRequest: angular.identity
                }).then(function (response) {
                    $scope.UpdateCompanyDetailsAfterFileUpload($rootScope.loggedInUserDetails.UserID + "_" + file.name);
                    manageLoader();
                }, function (response) {
                    $rootScope.resultMessage = "Error while Uploading Resume. Please try again....";
                    showNotification('error');
                    manageLoader();
                });
            }
            else {
                $scope.UpdateCompanyDetailsAfterFileUpload($scope.companyLogoDownload);
                manageLoader();
            }
        }
    }

    $scope.UpdateCompanyDetailsAfterFileUpload = function (companyLogo) {
        manageLoader('load');
        var objData = {
            DetailsId: $scope.DetailsId,
            Phone: $scope.companyPhoneNo,
            Website: $scope.companyWebsite,
            ContactName: $scope.contactPersonName,
            ContactPhone: $scope.contactPersonPhone,
            ContactEmail: $scope.contactPersonEmail,
            Address: $scope.companyAddress,
            Logo: companyLogo,
            UserId: $rootScope.loggedInUserDetails.UserID
        }
        var saveCompanyProfileURL = IAMInterviewed.Company.saveCompanyProfile;
        $http.post(saveCompanyProfileURL, objData).then(function (response) {
            if (response.data.Success == true) {
                $rootScope.resultMessage = "Company Profile Saved Successfylly.";
                showNotification('success');
                manageLoader();
                $scope.getCompanyProfile();
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
}]);