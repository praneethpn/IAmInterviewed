angular.module("IAMInterviewed").controller('AllJobPostingsController', ['$scope', '$http', '$q', '$filter', '$rootScope', '$timeout', '$window', '$localStorage', function ($scope, $http, $q, $filter, $rootScope, $timeout, $window, $localStorage, $location) {
    $scope.ClearAll = function () {
        $scope.primarySkillSearch = "";
        $scope.jobCodeSearch = "";
        $scope.postedBySearch = "";
        $scope.statusSearch = "Open";
        $scope.allRequirements = [];
        $scope.currentPage = 1;
        $scope.numPerPage = 20;
    }
    $scope.ClearAll();

    $scope.loadPrimarySkills = function () {
        var getPrimarySkills = IAMInterviewed.Skills.GetSkills;
        $http.get(getPrimarySkills).then(function success(response) {
            //console.log(response.data);
            $scope.PrimarySkills = response.data.data;
        }, function error(response) {
            $rootScope.resultMessage = response.data.errorMessage;
            showNotification('error');
        });
    }
    $scope.loadPrimarySkills();

    $scope.loadCompanyRequirementsForSelectList = function () {
        var getCompanyRequirementsForSelectListURL = IAMInterviewed.Company.getCompanyRequirementsForSelectList + "?companyId=" + $rootScope.loggedInUserDetails.UserID + "&status=" + $scope.statusSearch;
        $http.get(getCompanyRequirementsForSelectListURL).then(function success(response) {
            $scope.requirementsForSearch = response.data.data;
            //console.log($scope.requirementsForSearch);
        }, function error(response) {
            $rootScope.resultMessage = response.data.errorMessage;
            showNotification('error');
        });
    }
    $scope.loadCompanyRequirementsForSelectList();

    $scope.searchRequirements = function () {
        manageLoader('load');
        var getCompanyRequirementsURL = IAMInterviewed.Company.getCompanyRequirements + "?companyId=" + $rootScope.loggedInUserDetails.UserID + "&primarySkill=" + $scope.primarySkillSearch
            + "&jobCode=" + $scope.jobCodeSearch + "&postedBy=" + $scope.postedBySearch + "&status=" + $scope.statusSearch;
        $http.get(getCompanyRequirementsURL).then(function success(response) {
            $scope.allRequirements = response.data.data;
            $scope.totalItems = $scope.allRequirements.length;
            //console.log($scope.allRequirements);
            manageLoader();
        }, function error(response) {
            $rootScope.resultMessage = response.data.errorMessage;
            showNotification('error');
            manageLoader();            
        });
    }
    $scope.searchRequirements();

    $scope.loadRecruiters = function () {
        var fillRecruitersURL = IAMInterviewed.Company.fillRecruiters + "?companyId=" + $rootScope.loggedInUserDetails.UserID;
        $http.get(fillRecruitersURL).then(function success(response) {
            //console.log(response.data);
            $scope.Recruiters = response.data.data;
        }, function error(response) {
            $rootScope.resultMessage = response.data.errorMessage;
            showNotification('error');
        });
    }
    $scope.loadRecruiters();

    $scope.loadPM = function () {
        var fillPMURL = IAMInterviewed.Company.fillPM + "?companyId=" + $rootScope.loggedInUserDetails.UserID;
        $http.get(fillPMURL).then(function success(response) {
            //console.log(response.data);
            $scope.PMs = response.data.data;
        }, function error(response) {
            $rootScope.resultMessage = response.data.errorMessage;
            showNotification('error');
        });
    }
    $scope.loadPM();

    $scope.updateStatus = function (objRequirement) {
        manageLoader('load');
        //console.log(objRequirement.ReqId + ", " + objRequirement.AssignToDisplay + ", " + objRequirement.PMDisplay + ", " + objRequirement.Status + ", " + objRequirement.StatusRemarks + ", ");
        var updateCompanyrequirementsURL = IAMInterviewed.Company.updateCompanyrequirements + "?ReqId=" + objRequirement.ReqId + "&AssignTo=" + objRequirement.AssignToDisplay + "&PM=" + objRequirement.PMDisplay + "&Status=" + objRequirement.Status + "&StatusRemarks=" + objRequirement.StatusRemarks;
        $http.get(updateCompanyrequirementsURL).then(function success(response) {
            //console.log(response.data);
            $rootScope.resultMessage = response.data.data;
            showNotification('success');
            manageLoader();
        }, function error(response) {
            $rootScope.resultMessage = response.data.errorMessage;
            showNotification('error');
            manageLoader();
        });
    }

    $scope.redirectToReqRelatedProfiles = function (objRequirement) {
        $rootScope.objRequirement = objRequirement;
        $scope.RedirectUrl('RequirementRelatedProfiles');
    }

    $scope.redirectToSelectedProfiles = function (objRequirement) {
        $rootScope.objRequirement = objRequirement;
        $scope.RedirectUrl('ShortlistedProfiles');
    }

    $scope.redirectToPreAppliedProfiles = function (objRequirement) {
        $rootScope.objRequirement = objRequirement;
        $scope.RedirectUrl('PreAppliedCandidates');
    }

    $scope.redirectToAddProfiles = function (objRequirement) {
        $rootScope.objRequirement = objRequirement;
        $scope.RedirectUrl('AddProfiles');
    }

    $scope.paginate = function (value) {
        var begin, end, index;
        begin = ($scope.currentPage - 1) * $scope.numPerPage;
        end = begin + $scope.numPerPage;
        index = $scope.allRequirements.indexOf(value);
        return (begin <= index && index < end);
    };
}]);