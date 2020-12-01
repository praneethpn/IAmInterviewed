angular.module('IAMInterviewed').controller('HomeController', ['$scope', '$http', '$filter', 'DataServices', '$rootScope', 'LoaderService', '$timeout', '$window', '$localStorage', function ($scope, $http, $filter, DataServices, $rootScope, LoaderService, $timeout, $window, $localStorage, $location, PowerBiService) {

    $rootScope.sessionID = $localStorage.sessionId;
    $rootScope.loggedInUserName = $localStorage.userid;
    //LogOut function
    $scope.logout = function () {
        document.cookie = "sessionId=;secure;HttpOnly;"
        delete $localStorage.sessionId;
        delete $localStorage.userid;
        $localStorage.$reset();
        var signOutURL = IAMInterviewed.userManagent.signOut;
        $http.get(signOutURL).then(function success(response) {
            //console.log(response.data);
            $window.location.href = '/Index.html';
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
            ReturnUrl = { url: "app/components/Dashboard/Dashboard.html" };
        }
        else if (key == 'PrimarySkill') {
            ReturnUrl = { url: "app/components/Skills/PrimarySkill/PrimarySkill.html" };
        }
        else if (key == 'SecondarySkill') {
            ReturnUrl = { url: "app/components/Skills/SecondarySkill/SecondarySkill.html" };
        }
        else if (key == 'ViewInterviews') {
            ReturnUrl = { url: "app/components/Interviewer/ViewInterviews/ViewInterviews.html" };
        }
        else if (key == 'RatedInterviews') {
            ReturnUrl = { url: "app/components/Interviewer/RatedInterviews/RatedInterviews.html" };
        }
        else if (key == 'RegisteredCandidates') {
            ReturnUrl = { url: "app/components/Candidate/RegisteredCandidates/RegisteredCandidates.html" };
        }
        else if (key == 'ByPassPaymentGateway') {
            ReturnUrl = { url: "app/components/Candidate/ByPassPaymentGateway/ByPassPaymentGateway.html" };
        }
        else if (key == 'CompanyOnboard') {
            ReturnUrl = { url: "app/components/Company/CompanyOnboard/CompanyOnboard.html" };
        }
        else if (key == 'CompanyStatus') {
            ReturnUrl = { url: "app/components/Company/CompanyStatus/CompanyStatus.html" };
        }
        else if (key == 'ReleaseCandidates') {
            ReturnUrl = { url: "app/components/Company/ReleaseCandidates/ReleaseCandidates.html" };
        }
        else if (key == 'AddProfiles') {
            ReturnUrl = { url: "app/components/Company/AddProfiles/AddProfiles.html" };
        }
        else if (key == 'AllProfiles') {
            ReturnUrl = { url: "app/components/Company/AllProfiles/AllProfiles.html" };
        }
        else if (key == 'InterviewsBy') {
            ReturnUrl = { url: "app/components/Interviewer/InterviewsBy/InterviewsBy.html" };
        }
        else if (key == 'ScheduleSlots') {
            ReturnUrl = { url: "app/components/Interviewer/ScheduleSlots/ScheduleSlots.html" };
        }
        else if (key == 'CandidatePassward') {
            ReturnUrl = { url: "app/components/Candidate/CandidatePassward/CandidatePassward.html" };
        }
        else if (key == 'EditRating') {
            ReturnUrl = { url: "app/components/Interviewer/EditRating/EditRating.html" };
        }
        else if (key == 'PostRequirement') {
            ReturnUrl = { url: "app/components/Company/PostJob/PostJob.html" };
        }
        else if (key == 'FollowUps') {
            ReturnUrl = { url: "app/components/Company/FollowUps/FollowUps.html" };
        }
        else if (key == 'AddProfilesFollowUps') {
            ReturnUrl = { url: "app/components/Company/AddProfilesFollowUps/AddProfilesFollowUps.html" };
        }
        else if (key == 'ViewRating') {
            ReturnUrl = { url: "app/components/Company/ViewRating/ViewRating.html" };
        }
        else if (key == '' || key == null) {
            ReturnUrl = { url: "app/components/Dashboard/Dashboard.html" };
        }
        return ReturnUrl;
    }

    //Setting Partial html for ng-Include 
    if ($localStorage.RedirectKey == '' || $localStorage.RedirectKey == null || $localStorage.RedirectKey == undefined) {
        $scope.logout()
        $scope.template = { url: "app/components/Dashboard/Dashboard.html" };
    }
    else {
        $scope.template = $scope.SetRedirectUrl($localStorage.RedirectKey);
    }

    //Calling funtion in ng-Click
    $scope.RedirectUrl = function (key) {
        $scope.template = $scope.SetRedirectUrl(key);
        $localStorage.RedirectKey = key;
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
}]);