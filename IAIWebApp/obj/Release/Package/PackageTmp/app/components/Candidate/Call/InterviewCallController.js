angular.module("IAMInterviewed").controller('InterviewCallController', ['$scope', '$http', '$q', '$filter', '$rootScope', '$timeout', '$window', '$localStorage', '$location', function ($scope, $http, $q, $filter, $rootScope, $timeout, $window, $localStorage, $location) {
    $scope.candidateUniqueId = "";
    $scope.userName = "";
    $scope.loginId = "";
    $scope.findUser = function () {
        var queryString = $location.absUrl().split('?')[1];
        if (queryString != null && queryString != undefined) {
            var queryString1 = queryString.split('&');
            var auth = null;
            var username = null;
            for (var i = 0; i < queryString1.length; i++) {
                if (queryString1[i].indexOf('username') != -1) {
                    username = queryString1[i].split('=')[1];
                }
            }
            $scope.loginId = username;
            $scope.candidateUniqueId = username.substring(username.length - 10, username.length);
            $scope.userName = username.substring(0, username.length - 10);
            console.log($scope.candidateUniqueId);
            console.log($scope.userName);
        }
    }
    $scope.findUser();

    $scope.getCandidateProfile = function () {
        manageLoader('load');
        var getCandidateProfileURL = IAMInterviewed.Candidate.getCandidateProfileForCall + "?CandidateId=" + $scope.userName;
        $http.get(getCandidateProfileURL).then(function success(response) {
            $scope.CandidateProfile = response.data.data;
            console.log($scope.CandidateProfile);
            manageLoader();
        }, function error(response) {
            $rootScope.resultMessage = response.data.errorMessage;
            showNotification('error');
            manageLoader();
        });
    }
    $scope.getCandidateProfile();

    var callid = '', is_muted = false;
    $scope.is_muted = false;
    $scope.isCallStarted = false;
    $scope.isCallAccepted = false;
    $scope.callInterview = "";

    ////Selector for your <video> element
    //const video = document.querySelector('#localVideo');

    ////Core
    //window.navigator.mediaDevices.getUserMedia({ video: true })
    //    .then(stream => {
    //        video.srcObject = stream;
    //        video.onloadedmetadata = (e) => {
    //            video.play();
    //        };
    //    })
    //    .catch(() => {
    //        alert('You have give browser the permission to run Webcam and mic ;( ');
    //    });
    //
    /* On document load initiate sdk initialization and login */
    function pageload() { // Initializes the sdk and logs in the user.
        //document.getElementById("end").style.visibility = "hidden";
        //document.getElementById("mute").style.visibility = "hidden";
        //document.getElementById("start").style.visibility = "hidden";
        $scope.isCallStarted = false;
        if (CS == undefined) {
            return;
        }
        // configuration parameters. 
        let config = {
            appId: "pid_ae61897c_1bd3_442c_a9ff_9a5bc85db218" //<--- insert your project id here
        };

        // callback called at the end of initialization to notify success or failure.
        function callback(ret, resp) {
            if (ret == 200) {
                console.log("SDK " + CS.version + " initialized ");

                // Initiate login request
                CS.login($scope.loginId, "iaiUser@1", function (err, resp) {
                    if (err == 200) {
                        console.log("Login succesful");
                        //document.getElementById("start").style.visibility = "visible";
                    }
                });

                CS.call.onMessage(callCallback);
            }
        }
        CS.initialize(config, callback);
        //$("#btnaccept").css("display", "none");
        //$("#btnreject").css("display", "none");
        //$scope.isCallAccepted = false;
        document.getElementById("btnaccept").style.visibility = "hidden";
        document.getElementById("btnreject").style.visibility = "hidden";
        //$("#divAudioVideoBlock").hide();
        $("#waitTextInterview").show();
        $("#completedInterview").hide();
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

    function callCallback(msgType, resp) {
        console.log("call event " + msgType + " received");
        console.log("call from caller " + resp.caller);
        console.log("Call type isVideoCall " + resp.isVideoCall + " True if video call or False for audio call");

        switch (msgType) {
            case "OFFER":  /* incoming offer from remote party */
                console.log("Incoming call offer");
                callid = resp.callId;
                //$("#btnaccept").css("display", "block");
                //$("#btnreject").css("display", "block");
                $scope.isCallAccepted = true;
                if (resp.isVideoCall == true) {
                    document.getElementById("localVideo").style.display = "block";
                    //document.getElementById("remoteVideo").style.display = "block";
                }
                else {
                    document.getElementById("localVideo").style.display = "block";
                    //document.getElementById("remoteVideo").style.display = "none";
                }
                document.getElementById("btnaccept").style.visibility = "visible";
                document.getElementById("btnreject").style.visibility = "visible";
                //$("#divAudioVideoBlock").show();
                $("#waitTextInterview").hide();
                $("#completedInterview").hide();
                break;
            case "PSTN-OFFER":
                console.log("PSTN incoming call offer");
                break;
            case "RINGING":  /* ringing at remote end (for outbound call) */
                console.log("outgoing call ringing");
                break;
            case "ANSWERED":  /* outbound call is answered */
                console.log("incomin call answered");
                break;
            case "END":  /* remote party ended the call */
                console.log("incoming call end");
                document.getElementById("btnaccept").style.visibility = "hidden";
                document.getElementById("btnreject").style.visibility = "hidden";
                document.getElementById("localVideo").style.display = "block";
                //document.getElementById("remoteVideo").style.display = "none";
                //document.getElementById("btnend").style.visibility = "hidden";
                //$("#divAudioVideoBlock").hide();
                $("#waitTextInterview").hide();
                $("#completedInterview").show();
				CS.call.end(callid, "Bye", function (ret, resp) {
            If(ret == 200)
            console.log("call end successfully");
            $("#completedInterview").show();
        });
                //document.getElementById("btnend").style.visibility = "hidden";
                break;
            case "CALLHISTORYSYNC":  /* Call ended at another device */
                console.log("Call ");
                break;
            case "PSTN-END":
                console.log("PSTN incoming call end");
                break;
        }
    }

    $scope.answer = function () {
        CS.call.answer(callid, "localVideo", "remoteVideo", function (ret, resp) {
            console.log("answer ret: " + ret);
            if (resp.isVideoCall == true) {
                document.getElementById("localVideo").style.display = "block";
                //document.getElementById("remoteVideo").style.display = "block";
            }
            else {
                document.getElementById("localVideo").style.display = "block";
                //document.getElementById("remoteVideo").style.display = "none";
            }
        });
        //$("#btnaccept").css("display", "none");
        //$("#btnreject").css("display", "none");
        console.log(callid);
        $scope.isCallStarted = true;        
        document.getElementById("btnaccept").style.visibility = "hidden";
        document.getElementById("btnreject").style.visibility = "hidden";
        $("#waitTextInterview").hide();
        $("#completedInterview").hide();
    }

    $scope.reject = function () {
        CS.call.decline(callid, function (ret, resp) {
            if (ret == 200) {
                console.log("Call successfully declined" + ret);
            }
        });
        //$("#btnaccept").css("display", "none");
        //$("#btnreject").css("display", "none");
        $scope.isCallAccepted = false;
        document.getElementById("btnaccept").style.visibility = "hidden";
        document.getElementById("btnreject").style.visibility = "hidden";
        document.getElementById("localVideo").style.display = "none";
        //document.getElementById("remoteVideo").style.display = "none";
        $("#waitTextInterview").hide();
        $("#completedInterview").hide();
    }

    $scope.endCall = function () {
        document.getElementById("btnaccept").style.visibility = "hidden";
        document.getElementById("btnreject").style.visibility = "hidden";
        document.getElementById("localVideo").style.display = "block";
        //document.getElementById("remoteVideo").style.display = "none";
        //document.getElementById("btnend").style.visibility = "hidden";
        //$("#divAudioVideoBlock").hide();
        $("#waitTextInterview").hide();
        $("#completedInterview").show();
        console.log(callid);
        //document.getElementById("end").style.visibility = "hidden";
        //document.getElementById("mute").style.visibility = "hidden";
        CS.call.end(callid, "Bye", function (ret, resp) {
            If(ret == 200)
            console.log("call end successfully");
            $("#completedInterview").show();
        });
        $scope.isCallStarted = false;
    }

    $(document).ready(function () {
        //$("#divAudioVideoBlock").hide();
        $("#waitTextInterview").show();
        $("#completedInterview").hide();
    });
}]);