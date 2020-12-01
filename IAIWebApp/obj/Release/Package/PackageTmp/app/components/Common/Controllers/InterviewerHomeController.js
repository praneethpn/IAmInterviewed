angular.module("IAMInterviewed").controller('InterviewerHomeController', ['$scope', '$http', '$q', '$filter', '$rootScope', '$timeout', '$window', '$localStorage', function ($scope, $http, $q, $filter, $rootScope, $timeout, $window, $localStorage, $location) {
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
            ReturnUrl = { url: "app/components/Interviewer/InterviewerDashboard/InterviewerDashboard.html" };
        }
        else if (key == 'InterviewerProfile') {
            ReturnUrl = { url: "app/components/Interviewer/EditProfile/EditProfile.html" };
        }
        else if (key == 'Schedules') {
            ReturnUrl = { url: "app/components/Interviewer/Schedules/Schedules.html" };
        }
        else if (key == 'ReferAFriend') {
            ReturnUrl = { url: "app/components/Interviewer/ReferFriend/ReferFriend.html" };
        }
        else if (key == 'ChangePassword') {
            ReturnUrl = { url: "app/components/Common/ChangePassword/ChangePassword.html" };
        }
        else if (key == '' || key == null) {
            ReturnUrl = { url: "app/components/Interviewer/InterviewerDashboard/InterviewerDashboard.html" };
        }
        return ReturnUrl;
    }

    $scope.loadDashboardDetails = function () {
        manageLoader('load');
        var getDasbboardDetailsURL = IAMInterviewed.Candidate.getCandidateDashboardDetails + "?UserId=" + $rootScope.loggedInUserDetails.UserID;
        $http.get(getDasbboardDetailsURL).then(function success(response) {
            $rootScope.CandidateDashboardDetails = response.data.data;
            //console.log($rootScope.CandidateDashboardDetails);
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
        $scope.template = { url: "app/components/Interviewer/InterviewerDashboard/InterviewerDashboard.html" };
        $scope.logout();
    }
    else {
        if (parseInt($rootScope.loggedInUserDetails.LoginCount) > 0) {
            $scope.template = $scope.SetRedirectUrl($localStorage.RedirectKey);
        }
        else {
            $scope.template = $scope.SetRedirectUrl('ChangePassword');
        }
        //$scope.loadDashboardDetails();
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
        $("#ulMenuBarSide").find("#li" + key).addClass('active');
        $("#ulMenuBarSide").find("#li" + key).addClass('open');
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