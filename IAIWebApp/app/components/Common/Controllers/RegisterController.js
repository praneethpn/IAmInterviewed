angular.module('IAMInterviewed').controller('RegisterController', ['$scope', '$http', '$q', '$filter', '$rootScope', '$timeout', '$window', '$localStorage', function ($scope, $http, $q, $filter, $rootScope, $timeout, $window, $localStorage, $location) {
    $scope.ClearAll = function () {
        $scope.Name = "";
        $scope.EmailAddress = "";
        $scope.MobileNumber = "";
        $scope.PrimarySkill = "";
        $scope.SecondarySkill1 = "";
        $scope.Country = "";
        $scope.Type = "";
        $("#divsuccess").hide();
    }
    $scope.ClearAll();

    $scope.LoadPrimarySkills = function () {
        manageLoader('load');
        var getPrimarySkills = IAMInterviewed.Skills.GetSkills;
        $http.get(getPrimarySkills).then(function success(response) {
            //console.log(response.data);
            $scope.PrimarySkills = response.data.data;
            $scope.PrimarySkill = "";
            manageLoader();
        },
            function error(response) {
                $rootScope.resultMessage = response.data.errorMessage;
                showNotification('error');
                manageLoader();
                return false;
            });
    }
    $scope.LoadPrimarySkills();

    $scope.LoadSecondarySkills = function () {
        manageLoader('load');
        var deferred = $q.defer();
        var GetSecondarySkillsURL = IAMInterviewed.Skills.GetSecondarySkills + "?PrimarySkill=" + $scope.PrimarySkill;
        $http.get(GetSecondarySkillsURL).then(function success(response) {
            //console.log(response.data);
            $scope.SecondarySkills = response.data.data;
            $scope.SecondarySkill1 = "";
            manageLoader();
            deferred.resolve(response.data.data);
        },
            function error(response) {
                $rootScope.resultMessage = response.data.errorMessage;
                showNotification('error');
                manageLoader();
                deferred.reject;
            });
        return deferred.promise;
    }

    $scope.Register = function (RegisterType) {
        if (RegisterType == 'Company') {
            $scope.PrimarySkill = "0";
            $scope.SecondarySkill1 = "0";
        }
        if ($scope.PrimarySkill == null || $scope.PrimarySkill == "" || $scope.PrimarySkill == undefined ||
            $scope.SecondarySkill1 == null || $scope.SecondarySkill1 == "" || $scope.SecondarySkill1 == undefined) {
            $rootScope.resultMessage = "Please fill all mandatory fields.";
            showNotification('error');
        }
        else {
            var objRegisterData = {
                Name: $scope.Name,
                EmailAddress: $scope.EmailAddress,
                MobileNumber: $scope.MobileNumber,
                PrimarySkill: $scope.PrimarySkill,
                SecondarySkill1: $scope.SecondarySkill1,
                Country: $scope.Country,
                Type: RegisterType
            }
            var registerURL = IAMInterviewed.userManagent.register;
            $http.post(registerURL, objRegisterData).then(function (response) {
                if (response.data.Success == true) {
                    $scope.ClearAll();
                    $("#divsuccess").show();
                    //$rootScope.resultMessage = response.data.data;
                    //showNotification('success');
                    manageLoader();
                }
                else {
                    $rootScope.resultMessage = response.data.errorMessage;
                    showNotification('error');
                    $("#divsuccess").hide();
                    manageLoader();
                }
            }, function (response) {
                $rootScope.resultMessage = response.data.errorMessage;
                    showNotification('error');
                    $("#divsuccess").hide();
                manageLoader();
            });
        }
    }

    $(document).on('keydown', function (event) {
        if (event.key == "Escape") {
            //alert('Esc key pressed.');
            manageLoader()
        }
    });
}]);