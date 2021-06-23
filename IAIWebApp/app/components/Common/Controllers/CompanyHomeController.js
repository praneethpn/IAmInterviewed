angular.module("IAMInterviewed").controller('CompanyHomeController', ['$scope', '$http', '$q', '$filter', '$rootScope', '$timeout', '$window', '$localStorage', function ($scope, $http, $q, $filter, $rootScope, $timeout, $window, $localStorage, $location) {
    $rootScope.sessionID = $localStorage.sessionId;
    $rootScope.loggedInUserName = $localStorage.userid;
    $rootScope.loggedInUserDetails = $localStorage.UserSessionObject;
    //console.log($rootScope.loggedInUserDetails);
    //LogOut function
    $scope.logout = function () {
        document.cookie = "sessionId=;secure;HttpOnly;"
        delete $localStorage.sessionId;
        delete $localStorage.userid;
        delete $localStorage.UserSessionObject;
        $localStorage.$reset();
        var signOutURL = IAMInterviewed.userManagent.signOut;
        $http.get(signOutURL).then(function success(response) {
            //console.log(response.data);
            $window.location.href = '/SignIn.html';
            manageLoader();
        },
            function error(response) {
                $rootScope.resultMessage = response.data.errorMessage;
                showNotification('error');
                manageLoader();
                return false;
            });
    }

    //Checking Autherization
    //if ($rootScope.sessionID == undefined || $rootScope.sessionID == '') {
    //    $scope.logout();
    //}

    //Setting View for ng-Include
    $scope.SetRedirectUrl = function (key) {
        var ReturnUrl = '';
        if (key == 'Dashboard') {
            ReturnUrl = { url: "app/components/Company/Dashboard/Dashboard.html" };
        }
        else if (key == 'CompanyProfile') {
            ReturnUrl = { url: "app/components/Company/CompanyProfile/CompanyProfile.html" };
        }
        else if (key == 'AllPostingsDashboard') {
            ReturnUrl = { url: "app/components/Company/AllJobPostings/AllJobPostings.html" };
        }
        else if (key == 'PostJob') {
            ReturnUrl = { url: "app/components/Company/PostJob/PostJob.html" };
        }
        else if (key == 'TodayFollowUps') {
            ReturnUrl = { url: "app/components/Company/TodaysFollowups/TodaysFollowups.html" };
        }
        else if (key == 'AllProfiles') {
            ReturnUrl = { url: "app/components/Company/AllProfiles/AllProfiles.html" };
        }
        else if (key == 'AddSubUser') {
            ReturnUrl = { url: "app/components/Company/SubUser/SubUser.html" };
        }
        else if (key == 'AddDesignation') {
            ReturnUrl = { url: "app/components/Company/Designation/Designation.html" };
        }
        else if (key == 'RequestInterviewers') {
            ReturnUrl = { url: "app/components/Company/RequestInterviewers/RequestInterviewers.html" };
        }
        else if (key == 'RequirementRelatedProfiles') {
            ReturnUrl = { url: "app/components/Company/ReqRelatedProfiles/ReqRelatedProfiles.html" };
        }
        else if (key == 'ShortlistedProfiles') {
            ReturnUrl = { url: "app/components/Company/ShortlistedProfiles/ShortlistedProfiles.html" };
        }
        else if (key == 'PreAppliedCandidates') {
            ReturnUrl = { url: "app/components/Company/PreAppliedCandidates/PreAppliedCandidates.html" };
        }
        else if (key == 'AddProfiles') {
            ReturnUrl = { url: "app/components/Company/AddProfiles/AddProfiles.html" };
        }
        else if (key == 'Vendors') {
            ReturnUrl = { url: "app/components/Company/Vendors/Vendors.html" };
        }
        else if (key == 'CompanyScheduleInterview') {
            ReturnUrl = { url: "app/components/Company/ScheduleInterview/ScheduleInterview.html" };
        }
        else if (key == 'RecruiterDashboard') {
            //ReturnUrl = { url: "app/components/Company/RecruiterDashboard/RecruiterDashboard.html" };
            ReturnUrl = { url: "app/components/Company/AllJobPostings/AllJobPostings.html" };
        }
        else if (key == 'RequirementsDashboard') {
            ReturnUrl = { url: "app/components/Company/RequirementsDashboard/RequirementsDashboard.html" };
        }
        else if (key == 'ViewRatingIAIFormat') {
            ReturnUrl = { url: "app/components/Common/ViewRating/ViewRating.html" };
        }
        else if (key == 'ChangePassword') {
            ReturnUrl = { url: "app/components/Common/ChangePassword/ChangePassword.html" };
        }
        else if (key == '' || key == null) {
            ReturnUrl = { url: "app/components/Company/Dashboard/Dashboard.html" };
        }
        return ReturnUrl;
    }    
    //$scope.CompanyHomeDetails = {};
    $scope.loadHomeDetails = function () {
        manageLoader('load');
        var getDasbboardDetailsURL = IAMInterviewed.Company.getCompanyHomePageDetails + "?companyId=" + $rootScope.loggedInUserDetails.UserID;
        $http.get(getDasbboardDetailsURL).then(function success(response) {
            $scope.CompanyHomeDetails = response.data.data;
            //$scope.$broadcast("loadDunutChart", $rootScope.CandidateDashboardDetails);
            //console.log($scope.CompanyHomeDetails);
            console.log($rootScope.loggedInUserDetails);
            $rootScope.loggedInUserDetails.UserType = $scope.CompanyHomeDetails.CompanyUserType;
            if ($rootScope.loggedInUserDetails.UserType == "Recruiter" || $rootScope.loggedInUserDetails.UserType == "PM") {
                $scope.RedirectUrl('RecruiterDashboard');
            }
            manageLoader();
        },
            function error(response) {
                $rootScope.resultMessage = response.data.errorMessage;
                showNotification('error');
                manageLoader();
                return false;
            });
    };

    //Setting Partial html for ng-Include 
    if ($localStorage.RedirectKey == '' || $localStorage.RedirectKey == null || $localStorage.RedirectKey == undefined ||
        $localStorage.sessionId == undefined || $localStorage.sessionId == null || $localStorage.sessionId == '') {
        $scope.logout()
        $scope.template = { url: "app/components/Company/Dashboard/Dashboard.html" };
    }
    else {
        if (parseInt($rootScope.loggedInUserDetails.LoginCount) > 0) {
            $scope.template = $scope.SetRedirectUrl($localStorage.RedirectKey);
        }
        else {
            $scope.template = $scope.SetRedirectUrl('ChangePassword');
        }
        $scope.loadHomeDetails();
    }

    //Calling funtion in ng-Click
    $scope.RedirectUrl = function (key) {
        if (parseInt($rootScope.loggedInUserDetails.LoginCount) > 0) {
            $scope.template = $scope.SetRedirectUrl(key);
        }
        else {
            $scope.template = $scope.SetRedirectUrl('ChangePassword');
        }
        $localStorage.RedirectKey = key;
        $("#ulMenuBarSide").find('.active').removeClass('active');
        $("#ulMenuBarSide").find('.open').removeClass('open');
        if (key == "RequirementRelatedProfiles" || key == "ShortlistedProfiles" || key == "PreAppliedCandidates" || key == "AddProfiles" || key == "CompanyScheduleInterview") {
            $("#ulMenuBarSide").find("#liAllPostingsDashboard").addClass('active');
            $("#ulMenuBarSide").find("#liAllPostingsDashboard").addClass('open');
        }
        else {
            $("#ulMenuBarSide").find("#li" + key).addClass('active');
            $("#ulMenuBarSide").find("#li" + key).addClass('open');
        }
    }

    $scope.getDatFormatforLocal = function (date) {
        //return date.getDate() + "-" + (date.getMonth() + 1) + "-" + date.getFullYear();
        return (date.getMonth() + 1) + "-" + date.getDate() + "-" + date.getFullYear();
    }

    $scope.getDatFormatforProduction = function (date) {
        return (date.getMonth() + 1) + "-" + date.getDate() + "-" + date.getFullYear();
    }

    $scope.ToJavaScriptDate = function (value) {
        var pattern = /Date\(([^)]+)\)/;
        var results = pattern.exec(value);
        if (results != null) {
            var dt = new Date(parseFloat(results[1]));
            //return dt.getDate() + "/" + (dt.getMonth() + 1) + "/" + dt.getFullYear();
            return (dt.getMonth() + 1) + "/" + dt.getDate() + "/" + dt.getFullYear();
        }
        else {
            return value;
        }
    }

    $(document).on('keydown', function (event) {
        if (event.key == "Escape") {
            //alert('Esc key pressed.');
            manageLoader()
        }
    });
}]);