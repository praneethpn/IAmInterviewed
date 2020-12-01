angular.module('IAMInterviewed').controller('ScheduleInterviewController', ['$scope', '$http', '$q', '$filter', '$rootScope', '$timeout', '$window', '$localStorage', 'myService', function ($scope, $http, $q, $filter, $rootScope, $timeout, $window, $localStorage, $location, myService) {
    $scope.ClearAll = function () {
        $scope.ScheduleDate = "";
        $scope.Interviewer = "";
        $scope.TimeSlot = "";
        $scope.TermsConditions = false;
        $scope.InterviewType = "Audio";
        $scope.ScheduledInterviewes = [];
        $scope.objInterviewer = "";
        $scope.objTimeSlot = "";
    }
    $scope.ClearAll();
    if ($rootScope.CandidateDashboardDetails != undefined && $rootScope.CandidateDashboardDetails.CandidateNavigationStatus == "Profile Not Filled") {
        $rootScope.resultMessage = "Please fill Profile to Schedule Interview.";
        showNotification('warning');
        $scope.RedirectUrl('CandidateProfile');
    }
    $scope.loadScheduledInterview = function () {
        manageLoader('load');
        var getScheduledInterviewURL = IAMInterviewed.Candidate.getScheduledInterview + "?UserId=" + $rootScope.loggedInUserDetails.UserID;
        $http.get(getScheduledInterviewURL).then(function success(response) {
            //console.log(response.data);
            $scope.ScheduledInterviews = response.data.data;
            manageLoader();
        },
            function error(response) {
                $rootScope.resultMessage = response.data.errorMessage;
                showNotification('error');
                manageLoader();
                return false;
            });
    }
    $scope.loadScheduledInterview();

    $scope.loadInterviewers = function () {
        if ($scope.ScheduleDate != null && $scope.ScheduleDate != "") {
            manageLoader('load');
            var getInterviewerByDateURL = IAMInterviewed.Candidate.getInterviewerByDate + "?ScheduleDate=" + $scope.ScheduleDate + "&UserId=" + $rootScope.loggedInUserDetails.UserID;
            $http.get(getInterviewerByDateURL).then(function success(response) {
                //console.log(response.data.data);
                $scope.Interviewers = response.data.data;
                manageLoader();
                //$scope.registerUser($rootScope.loggedInUserDetails.UserID + $rootScope.loggedInUserDetails.UniqueId);
            }, function error(response) {
                $rootScope.resultMessage = response.data.errorMessage;
                showNotification('error');
                manageLoader();
            });
        }
        else {
            $rootScope.resultMessage = "Please Select Date.";
            showNotification('error');
        }
    }

    $scope.loadTimeSlots = function () {
        if ($scope.Interviewer != null && $scope.Interviewer != "") {
            manageLoader('load');
            var getInterviewerTimeSlotsByDateURL = IAMInterviewed.Candidate.getInterviewerTimeSlotsByDate + "?ScheduleDate=" + $scope.ScheduleDate + "&InterviewerId=" + $scope.Interviewer;
            $http.get(getInterviewerTimeSlotsByDateURL).then(function success(response) {
                //console.log(response.data);
                $scope.TimeSlots = response.data.data;
                manageLoader();
            }, function error(response) {
                $rootScope.resultMessage = response.data.errorMessage;
                showNotification('error');
                manageLoader();
            });
        }
        else {
            $rootScope.resultMessage = "Please Select Date.";
            showNotification('error');
        }
    }

    $scope.schedulePopUp = function (objSlot, objInterviewer) {
        $scope.objInterviewer = objInterviewer;
        $scope.objTimeSlot = objSlot;
        $("#confirmScheduleInterviewPopup").modal("show");
    }

    $scope.selectTime = function () {
        $scope.Interviewer = $scope.objInterviewer.UserId;
        $scope.TimeSlot = $scope.objTimeSlot.TimeSlotId;
        $("#confirmScheduleInterviewPopup").modal("hide");
    }

    $scope.scheduleInterview = function () {
        manageLoader('load');
        var saveInterviewScheduleURL = IAMInterviewed.Candidate.saveInterviewSchedule + "?ScheduleDate=" + $scope.ScheduleDate + "&Interviewer=" + $scope.Interviewer +
            "&TimeSlot=" + $scope.TimeSlot + "&CandidateId=" + $rootScope.loggedInUserDetails.UserID + "&InterviewType=" + $scope.InterviewType;
        $http.get(saveInterviewScheduleURL).then(function (response) {
            if (response.data.Success == true) {
                $rootScope.resultMessage = response.data.data;
                $scope.loadScheduledInterview();
                showNotification('success');
                manageLoader();
                $scope.registerUser($rootScope.loggedInUserDetails.UserID + $rootScope.loggedInUserDetails.UniqueId);
                $timeout(function () {
                    //$scope.RedirectUrl('ScheduleInterview');
                }, 2000);
            }
            else {
                $rootScope.resultMessage = response.data.errorMessage;
                showNotification('error');
                manageLoader();
                return false;
            }
        }, function (response) {
            $rootScope.resultMessage = response.data.errorMessage;
            showNotification('error');
            manageLoader();
            return false;
        });
    }

    $scope.paymentGateway = function () {        
        manageLoader('load');
        var saveInterviewScheduleURL = IAMInterviewed.Candidate.saveInterviewSchedulePayment + "?ScheduleDate=" + $scope.ScheduleDate + "&Interviewer=" + $scope.Interviewer +
            "&TimeSlot=" + $scope.TimeSlot + "&CandidateId=" + $rootScope.loggedInUserDetails.UserID + "&InterviewType=" + $scope.InterviewType;
        $http.get(saveInterviewScheduleURL).then(function (response) {
            if (response.data.Success == true) {
                //$rootScope.resultMessage = response.data.data;
                //$scope.loadScheduledInterview();
                //showNotification('success');
                manageLoader();
                $scope.registerUser($rootScope.loggedInUserDetails.UserID + $rootScope.loggedInUserDetails.UniqueId);
                if (response.data.data.PromoCode == "1") {
                    $rootScope.resultMessage = response.data.data.ResponseMessage;
                    showNotification('success');                    
                }
                else {
                    $timeout(function () {
                        window.location.href = "CCAV.aspx?orderId=" + response.data.data.OrderId + "&userId=" + $rootScope.loggedInUserDetails.UserID + "";
                        //$scope.RedirectUrl('ScheduleInterview');
                    }, 1000);
                }
            }
            else {
                $rootScope.resultMessage = response.data.errorMessage;
                showNotification('error');
                manageLoader();
                return false;
            }
        }, function (response) {
            $rootScope.resultMessage = response.data.errorMessage;
            showNotification('error');
            manageLoader();
            return false;
        });
        //$localStorage.CandidateIdPayment = $rootScope.loggedInUserDetails.UserID;
        //var number = "12345";        
    }

    var callid = '', is_muted = false;
    $scope.is_muted = false;
    $scope.isCallStarted = false;
    $scope.callInterview = "";

    $scope.registerUser = function (userName) {
        var objRegister = {
            projectid: "pid_ae61897c_1bd3_442c_a9ff_9a5bc85db218",
            authtoken: "21b83a60_f224_4740_a490_22967f6d137d",
            username: userName,
            password: "iaiUser@1"
        }
        var registerUserURL = "https://proxy.vox-cpaas.in/api/user";
        $http.post(registerUserURL, objRegister).then(function (response) {
            console.log(response);
        }, function (response) {
            $rootScope.resultMessage = response.data.errorMessage;
            showNotification('error');
            manageLoader();
        });
        //CS.contacts.addContact(userName, function (err, resp) {
        //    if (err != 200 && err != 204) {
        //        console.log("add contact failed with response code " + err + " reason " + resp);
        //    } else {
        //        console.log("contact added successfully.");
        //    }
        //});
    }

    /* On document load initiate sdk initialization and login */
    function pageload() { // Initializes the sdk and logs in the user.
        //document.getElementById("end").style.visibility = "hidden";
        //document.getElementById("mute").style.visibility = "hidden";
        //document.getElementById("start").style.visibility = "hidden";
        $scope.isCallStarted = false;
        CS.call.onMessage(handleCallFromIML);

        // configuration parameters. 
        let config = {
            appId: "pid_ae61897c_1bd3_442c_a9ff_9a5bc85db218" //<--- insert your project id here
        };

        // callback called at the end of initialization to notify success or failure.
        function callback(ret, resp) {
            if (ret == 200) {
                console.log("SDK " + CS.version + " initialized ");
            }
        }
        CS.initialize(config, callback);
    }

    pageload();

    function handleCallFromIML(msgType, resp) {
        switch (msgType) {
            case "OFFER":     /* incoming offer from remote party */
                break;
            case "RINGING":   /* ringing */
                break;
            case "ANSWERED":  /* call answer */
                break;
            case "END":       /* call end */
                console.log("call end");
                //document.getElementById("end").style.visibility = "hidden";
                //document.getElementById("mute").style.visibility = "hidden";
                break;
        }
    }

    $(document).ready(function () {
        $timeout(function () {
            //onComponentLoad();
            DatePickerfunc();
            //$(".select2").select2();
        });
    });
}]);