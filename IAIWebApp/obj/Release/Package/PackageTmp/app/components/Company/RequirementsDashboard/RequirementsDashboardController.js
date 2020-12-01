angular.module("IAMInterviewed").controller('RequirementsDashboardController', ['$scope', '$http', '$q', '$filter', '$rootScope', '$timeout', '$window', '$localStorage', function ($scope, $http, $q, $filter, $rootScope, $timeout, $window, $localStorage, $location) {
    $scope.primarySkillSearch = "";
    if ($rootScope.RequirementsDashboardStatus == undefined || $rootScope.RequirementsDashboardStatus == "" || $rootScope.RequirementsDashboardStatus == null) {
        $scope.RedirectUrl('Dashboard');
    }
    $scope.LoadPrimarySkills = function () {
        var getPrimarySkills = IAMInterviewed.Skills.GetSkills;
        $http.get(getPrimarySkills).then(function success(response) {
            //console.log(response.data);
            $scope.PrimarySkills = response.data.data;
        }, function error(response) {
            $rootScope.resultMessage = response.data.errorMessage;
            showNotification('error');
        });
    }
    $scope.LoadPrimarySkills();

    $scope.getRequirements = function () {
        manageLoader('load');
        var getRequirementsURL = IAMInterviewed.Company.GetCompanyDashBoardJobPostingDetails + "?status=" + $rootScope.RequirementsDashboardStatus + "&companyId="
            + $rootScope.loggedInUserDetails.UserID + "&primarySkill=" + $scope.primarySkillSearch;
        $http.get(getRequirementsURL).then(function success(response) {
            //console.log(response.data);
            $scope.requirementsList = response.data.data;
            manageLoader();
        }, function error(response) {
            $rootScope.resultMessage = response.data.errorMessage;
            showNotification('error');
            manageLoader();
        });
    }
    $scope.getRequirements();
}]);