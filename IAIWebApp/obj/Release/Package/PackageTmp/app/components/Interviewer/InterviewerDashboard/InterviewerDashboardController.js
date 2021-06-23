angular.module('IAMInterviewed').controller('InterviewerDashboardController', ['$scope', '$http', '$q', '$filter', '$rootScope', '$timeout', '$window', '$localStorage', function ($scope, $http, $q, $filter, $rootScope, $timeout, $window, $localStorage, $location) {
    $scope.ClearAll = function () {
        $scope.ViewMode = "Interviews";
        $scope.viewInterview = "";
        $scope.AudioFile = "";
        $scope.AudioFileDownload = "";
        $scope.secSkill1Remarks1Count = 0;
        $scope.secSkill1Remarks2Count = 0;
        $scope.secSkill1Remarks3Count = 0;
        $scope.secSkill1Remarks4Count = 0;
        $scope.secSkill1Remarks5Count = 0;
        $scope.totalRemarksCount = 0;
    }
    $scope.ClearAll();

    $scope.loadInterviews = function () {
        manageLoader('load');
        var deferred = $q.defer();
        var getInterviewsURL = IAMInterviewed.Interviewer.getInterviews + "?UserId=" + $rootScope.loggedInUserDetails.UserID;
        $http.get(getInterviewsURL).then(function success(response) {
            $scope.Interviews = response.data.data;
            //console.log($scope.Interviews);
            //$scope.search();
            $scope.totalItems = $scope.Interviews.InterviewsToBeConfirmed.length;
            $scope.currentPage = 1;
            $scope.numPerPage = 20;
            manageLoader();
            deferred.resolve(response.data.data);
        }, function error(response) {
            $rootScope.resultMessage = response.data.errorMessage;
            showNotification('error');
            manageLoader();
            deferred.reject;
        });
        return deferred.promise;
    }

    $scope.paginate1 = function (value) {
        var begin, end, index;
        begin = ($scope.currentPage - 1) * $scope.numPerPage;
        end = begin + $scope.numPerPage;
        index = $scope.Interviews.InterviewsToBeConfirmed.indexOf(value);
        return (begin <= index && index < end);
    };
    $scope.loadInterviews().then(function (response) {
        $timeout(function () { $("#btnTodaysInterviews").click(); }, 100);
    });
    //$scope.loadInterviews();

    $scope.confirmInterview = function (objInterview) {
        $scope.ViewMode = "ViewConfirmInterview";
        $scope.viewInterview = objInterview;
        //console.log($scope.viewInterview);
    }

    $scope.updateInterview = function (objUpdateInterview) {
        manageLoader('load');
        var confirmInterviewURL = IAMInterviewed.Interviewer.confirmInterview + "?ScheduleId=" + objUpdateInterview.ScheduleId + "&UserId=" + $rootScope.loggedInUserDetails.UserID
            + "&InterviewDateTime" + objUpdateInterview.DisplayDate + " - " + objUpdateInterview.TimeSlot + "&CandidateEmail" + objUpdateInterview.Email;
        $http.get(confirmInterviewURL).then(function success(response) {
            $rootScope.resultMessage = "Interview Confirmed Successfully.";
            showNotification('success');
            angular.forEach($scope.Interviews.InterviewsToBeConfirmed, function (value, key) {
                if (value.ScheduleId == objUpdateInterview.ScheduleId) {
                    value.IsConfirmed = "Yes";
                }
            });
            $scope.ViewMode = "Interviews";
            manageLoader();
        }, function error(response) {
            $scope.ViewMode = "ViewConfirmInterview";
            $rootScope.resultMessage = response.data.errorMessage;
            showNotification('error');
            manageLoader();
        });
    }

    $scope.viewResume = function (objViewResume) {
        console.log(objViewResume);
        //Resume
        manageLoader('load');
        var viewResumeURL = IAMInterviewed.Interviewer.extractResume + "?resumeName=" + objViewResume.Resume;
        $http.get(viewResumeURL).then(function success(response) {
            $scope.resumeContent = response.data.data;
            console.log($scope.resumeContent);
            manageLoader();
        }, function error(response) {
            $rootScope.resultMessage = response.data.errorMessage;
            showNotification('error');
            manageLoader();
        });
    }

    $scope.rateInterview = function (objRateInterview) {
        $scope.ViewMode = "RateInterview";
        $scope.viewRateInterview = objRateInterview;
        if ($scope.viewRateInterview.SecondarySkill1Rating == "" || $scope.viewRateInterview.SecondarySkill1Rating == null) {
            $scope.viewRateInterview.InterviewerRemarks == "";
        }
        $scope.AudioFileDownload = objRateInterview.AudioFile;
        //console.log($scope.viewRateInterview);
        $('#form_wizard_1').bootstrapWizard('show', 0);
    }

    $scope.updateRating = function () {
        manageLoader('load');
        //console.log($scope.myFile);
        if (($scope.myFile == undefined || $scope.myFile == "") && ($scope.AudioFileDownload == "" || $scope.AudioFileDownload == null)) {
            $rootScope.resultMessage = "Please upload Recording.";
            showNotification('warning');
            manageLoader();
        }
        else {
        if ($scope.myFile != undefined && $scope.myFile != "") {
            $scope.UploadType = "CandidateResume";
            var file = $scope.myFile;
            var UploadURL = IAMInterviewed.Interviewer.UploadRatingFile;
            var payload = new FormData();
            payload.append("ScheduleId", $scope.viewRateInterview.ScheduleId);
            payload.append("file", file);
            $http.post(UploadURL, payload, {
                headers: { 'Content-Type': undefined },
                transformRequest: angular.identity
            }).then(function (response) {
                $scope.UpdateRatingAfterFileUpload($scope.viewRateInterview.ScheduleId + "_" + file.name);
                manageLoader();
            }, function (response) {
                $rootScope.resultMessage = "Error while Uploading Resume. Please try again....";
                showNotification('error');
                manageLoader();
            });
            //$scope.UpdateRatingAfterFileUpload($scope.viewRateInterview.ScheduleId + "_" + file.name);
        }
        else {
            $scope.UpdateRatingAfterFileUpload($scope.AudioFileDownload);
            manageLoader();
        }
        //}
    }

    $scope.UpdateRatingAfterFileUpload = function (AudioFile) {
        manageLoader('load');
        var objData = {
            ScheduleId: $scope.viewRateInterview.ScheduleId,
            SecondarySkill1Rating: $scope.viewRateInterview.SecondarySkill1Rating,
            SecondarySkill2Rating: $scope.viewRateInterview.SecondarySkill2Rating,
            SecondarySkill3Rating: $scope.viewRateInterview.SecondarySkill3Rating,
            SecondarySkill4Rating: $scope.viewRateInterview.SecondarySkill4Rating,
            SecondarySkill5Rating: $scope.viewRateInterview.SecondarySkill5Rating,
            TotalRating: $scope.TotalRating,
            EnglishCommunication: $scope.viewRateInterview.EnglishCommunication,
            Attitude: $scope.viewRateInterview.Attitude,
            InterpersonalSkillCommunication: $scope.viewRateInterview.InterpersonalSkillCommunication,
            InterviewerRemarks: $scope.viewRateInterview.InterviewerRemarks,
            AudioFile: AudioFile,
            VideoFile: '',
            SecondarySkill1Remarks: $scope.viewRateInterview.SecondarySkill1Remarks,
            SecondarySkill2Remarks: $scope.viewRateInterview.SecondarySkill2Remarks,
            SecondarySkill3Remarks: $scope.viewRateInterview.SecondarySkill3Remarks,
            SecondarySkill4Remarks: $scope.viewRateInterview.SecondarySkill4Remarks,
            SecondarySkill5Remarks: $scope.viewRateInterview.SecondarySkill5Remarks,
            EnglishCommunicationRemarks: $scope.viewRateInterview.EnglishCommunicationRemarks,
            AttitudeRemarks: $scope.viewRateInterview.AttitudeRemarks,
            InterpersonalSkillCommunicationRemarks: $scope.viewRateInterview.InterpersonalSkillCommunicationRemarks,
            CandidateEmail: $scope.viewRateInterview.Email,
            InterviewDateTime: $scope.viewRateInterview.DisplayDate + " - " + $scope.viewRateInterview.TimeSlot,
            InterviewerId: $rootScope.loggedInUserDetails.UserID,
            CandidateId: $scope.viewRateInterview.CandidateId,
            CandidateName: $scope.viewRateInterview.CandidateName,
            subSkill1: $scope.viewRateInterview.subSkill1,
            subSkill1Rating: $scope.viewRateInterview.subSkill1Rating,
            subSkill2: $scope.viewRateInterview.subSkill2,
            subSkill2Rating: $scope.viewRateInterview.subSkill2Rating,
            subSkill3: $scope.viewRateInterview.subSkill3,
            subSkill3Rating: $scope.viewRateInterview.subSkill3Rating,
            subSkill4: $scope.viewRateInterview.subSkill4,
            subSkill4Rating: $scope.viewRateInterview.subSkill4Rating,
            subSkill5: $scope.viewRateInterview.subSkill5,
            subSkill5Rating: $scope.viewRateInterview.subSkill5Rating,
            subSkill6: $scope.viewRateInterview.subSkill6,
            subSkill6Rating: $scope.viewRateInterview.subSkill6Rating,
            subSkill7: $scope.viewRateInterview.subSkill7,
            subSkill7Rating: $scope.viewRateInterview.subSkill7Rating,
            subSkill8: $scope.viewRateInterview.subSkill8,
            subSkill8Rating: $scope.viewRateInterview.subSkill8Rating,
            subSkill9: $scope.viewRateInterview.subSkill9,
            subSkill9Rating: $scope.viewRateInterview.subSkill9Rating,
            subSkill10: $scope.viewRateInterview.subSkill10,
            subSkill10Rating: $scope.viewRateInterview.subSkill10Rating,
        }
        var updateRatingURL = IAMInterviewed.Interviewer.updateRating;
        $http.post(updateRatingURL, objData).then(function (response) {
            if (response.data.Success == true) {
                $rootScope.resultMessage = "Rating Saved Successfylly.";
                showNotification('success');
                manageLoader();
                $scope.ViewMode = 'Interviews';
                $scope.loadInterviews();
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

    $scope.viewRatedInterview = function (objRateInterview) {
        $scope.ViewMode = "RateInterview";
        $scope.ViewType = "ViewRatedInterview";
        $scope.viewRateInterview = objRateInterview;
        $scope.AudioFileDownload = objRateInterview.AudioFile;
        //console.log($scope.viewRateInterview);
    }

    $scope.updateStatusInterview = function (objInterview) {
        if (objInterview.InterviewerRemarks != "" && objInterview.InterviewerRemarks != null && objInterview.InterviewerRemarks != undefined) {
            manageLoader('load');
            var updateInterviewStatusURL = IAMInterviewed.Interviewer.updateInterviewStatus + "?ScheduleId=" + objInterview.ScheduleId + "&Remarks=" + objInterview.InterviewerRemarks;
            $http.get(updateInterviewStatusURL).then(function success(response) {
                $rootScope.resultMessage = "Updated Successfully.";
                showNotification('success');
                $scope.ViewMode = "Interviews";
                manageLoader();
            }, function error(response) {
                $rootScope.resultMessage = response.data.errorMessage;
                showNotification('error');
                manageLoader();
            });
        }
        else {
            $rootScope.resultMessage = "Please add comments to update this interview schedule.";
            showNotification('error');
        }
    }

    $scope.viewRatingDetails = function (objCandidateRating) {
        var ratingIAIFormat = {
            Logo: "",
            CandidateName: objCandidateRating.CandidateName,
            UniqueId: objCandidateRating.CandidateUniqueId,
            ScheduleId: objCandidateRating.ScheduleId,
            SecSkillName1: objCandidateRating.SecondarySkill1,
            SecSKillName2: objCandidateRating.SecondarySkill2,
            SecSkillName3: objCandidateRating.SecondarySkill3,
            SecSkillName4: objCandidateRating.SecondarySkill4,
            SecSkillName5: objCandidateRating.SecondarySkill5,
            SecSkill1Rating: objCandidateRating.SecondarySkill1Rating,
            SecSkill2Rating: objCandidateRating.SecondarySkill2Rating,
            SecSkill3Rating: objCandidateRating.SecondarySkill3Rating,
            SecSkill4Rating: objCandidateRating.SecondarySkill4Rating,
            SecSkill5Rating: objCandidateRating.SecondarySkill5Rating,
            skill1comments: objCandidateRating.SecondarySkill1Remarks,
            skill2comments: objCandidateRating.SecondarySkill2Remarks,
            skill3comments: objCandidateRating.SecondarySkill3Remarks,
            skill4comments: objCandidateRating.SecondarySkill4Remarks,
            skill5comments: objCandidateRating.SecondarySkill5Remarks,
            SoftSkillRating: objCandidateRating.SoftSkillRating,
            EnglishCommunication: objCandidateRating.EnglishCommunication,
            EngComments: objCandidateRating.EnglishCommunicationRemarks,
            Attitude: objCandidateRating.Attitude,
            Attitudecomments: objCandidateRating.AttitudeRemarks,
            InterpersonalSkillCommunication: objCandidateRating.InterpersonalSkillCommunication,
            InterComments: objCandidateRating.InterpersonalSkillCommunicationRemarks,
            InterviewerRemarks: objCandidateRating.InterviewerRemarks,
            OveralRating: objCandidateRating.TotalRating
        }
        $scope.$broadcast('bindCandidateRatingDetails', 0, ratingIAIFormat.ScheduleId);
        //$("#candidateratingdetails").modal('show');
    }

    $scope.remarks1Change = function () {
        //console.log($("#txtskill1Remarks").val());
        $scope.secSkill1Remarks1Count = $scope.viewRateInterview.SecondarySkill1Remarks == undefined ? $("#txtskill1Remarks").val().length : $scope.viewRateInterview.SecondarySkill1Remarks.length;
        if (($scope.secSkill1Remarks1Count < 50 && $scope.secSkill1Remarks1Count > 0) || $scope.secSkill1Remarks1Count >= 200) {
            $("#spanRemarks1Count").addClass('count_error');
        }
        else {
            $("#spanRemarks1Count").removeClass('count_error');
        }
    }

    $scope.remarks2Change = function () {
        //console.log($("#txtskill1Remarks").val());
        $scope.secSkill1Remarks2Count = $scope.viewRateInterview.SecondarySkill2Remarks == undefined ? $("#txtskill2Remarks").val().length : $scope.viewRateInterview.SecondarySkill2Remarks.length;
        if (($scope.secSkill1Remarks2Count < 50 && $scope.secSkill1Remarks2Count > 0) || $scope.secSkill1Remarks2Count >= 200) {
            $("#spanRemarks2Count").addClass('count_error');
        }
        else {
            $("#spanRemarks2Count").removeClass('count_error');
        }
    }

    $scope.remarks3Change = function () {
        //console.log($("#txtskill3Remarks").val());
        $scope.secSkill1Remarks3Count = $scope.viewRateInterview.SecondarySkill3Remarks == undefined ? $("#txtskill3Remarks").val().length : $scope.viewRateInterview.SecondarySkill3Remarks.length;
        if (($scope.secSkill1Remarks3Count < 50 && $scope.secSkill1Remarks3Count > 0) || $scope.secSkill1Remarks3Count >= 200) {
            $("#spanRemarks3Count").addClass('count_error');
        }
        else {
            $("#spanRemarks3Count").removeClass('count_error');
        }
    }

    $scope.remarks4Change = function () {
        //console.log($("#txtskill4Remarks").val());
        $scope.secSkill1Remarks4Count = $scope.viewRateInterview.SecondarySkill4Remarks == undefined ? $("#txtskill4Remarks").val().length : $scope.viewRateInterview.SecondarySkill4Remarks.length;
        if (($scope.secSkill1Remarks4Count < 50 && $scope.secSkill1Remarks4Count > 0) || $scope.secSkill1Remarks4Count >= 200) {
            $("#spanRemarks4Count").addClass('count_error');
        }
        else {
            $("#spanRemarks4Count").removeClass('count_error');
        }
    }

    $scope.remarks5Change = function () {
        //console.log($("#txtskill5Remarks").val());
        $scope.secSkill1Remarks5Count = $scope.viewRateInterview.SecondarySkill5Remarks == undefined ? $("#txtskill5Remarks").val().length : $scope.viewRateInterview.SecondarySkill5Remarks.length;
        if (($scope.secSkill1Remarks5Count < 50 && $scope.secSkill1Remarks5Count > 0) || $scope.secSkill1Remarks5Count >= 200) {
            $("#spanRemarks5Count").addClass('count_error');
        }
        else {
            $("#spanRemarks5Count").removeClass('count_error');
        }
    }

    $scope.totalremarksChange = function () {
        //console.log($("#txtskill1Remarks").val());
        $scope.totalRemarksCount = $scope.viewRateInterview.InterviewerRemarks == undefined ? $("#interviewerRemarks").val().length : $scope.viewRateInterview.InterviewerRemarks.length;
        if (($scope.totalRemarksCount < 100 && $scope.totalRemarksCount > 0) || $scope.totalRemarksCount >= 500) {
            $("#spanRemarks1Count").addClass('count_error');
        }
        else {
            $("#spanRemarks1Count").removeClass('count_error');
        }
    }

    var callid = '', is_muted = false;
    $scope.is_muted = false;
    $scope.isCallStarted = false;
    $scope.callInterview = "";
    var recordCall = true;

    /* Initiate outbound call (call remote party) */
    $scope.startCall = function (objInterview) {
        $scope.callInterview = objInterview;
        var destnum = "+91" + objInterview.MobileNumber;
        //document.getElementById("end").style.visibility = "visible";
        //document.getElementById("mute").style.visibility = "visible";
        //callid = CS.call.startPSTNCall(destnum, "localVideo", "remoteVideo", function () { });
        callid = CS.call.startCall(objInterview.UserId + objInterview.CandidateUniqueId, "localVideo", "remoteVideo", false, handleCallFromIML, recordCall)
        $scope.isCallStarted = true;
        objInterview.InterviewerRemarks = "Panel Joined Call";
        $scope.updateStatusInterview(objInterview);
    }

    $scope.startVideoCall = function (objInterview) {
        $scope.callInterview = objInterview;
        var destnum = "+91" + objInterview.MobileNumber;
        //document.getElementById("end").style.visibility = "visible";
        //document.getElementById("mute").style.visibility = "visible";
        //callid = CS.call.startPSTNCall(destnum, "localVideo", "remoteVideo", function () { });
        callid = CS.call.startCall(objInterview.UserId + objInterview.CandidateUniqueId, "localVideo", "remoteVideo", true, handleCallFromIML, recordCall)
        $scope.isCallStarted = true;
        //document.getElementById("localVideo").style.display = "block";
        document.getElementById("remoteVideo").style.display = "block";
        objInterview.InterviewerRemarks = "Panel Joined Call";
        $scope.updateStatusInterview(objInterview);
    }

    /* End ongoing call */
    $scope.endCall = function () {
        //document.getElementById("end").style.visibility = "hidden";
        //document.getElementById("mute").style.visibility = "hidden";
        $scope.callInterview = "";
        $scope.isCallStarted = false;
        //document.getElementById("localVideo").style.display = "none";
        document.getElementById("remoteVideo").style.display = "none";
        CS.call.end(callid, "Bye", function (ret, resp) {
            if (ret == 200)
                CS.call.saveRecording(callid, 'recording.webm');
            console.log("call end successfully");
        });
    }

    /* Mute call */
    $scope.mute = function () {
        is_muted = !is_muted;
        $scope.is_muted = !$scope.is_muted;
        CS.call.mute(callid, is_muted, function (ret, resp) {
            if (ret == 200)
                console.log("Call muted successfully");
        });


        //if (is_muted)
        //    document.getElementById("mute").value = "un-mute call";
        //else
        //    document.getElementById("mute").value = "mute call";
    }

    /* On document load initiate sdk initialization and login */
    function pageload() { // Initializes the sdk and logs in the user.
        //document.getElementById("end").style.visibility = "hidden";
        //document.getElementById("mute").style.visibility = "hidden";
        //document.getElementById("start").style.visibility = "hidden";
        if (CS == undefined) {
            return;
        }
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

                // Initiate login request
                CS.login("test", "test", function (err, resp) {
                    if (err == 200) {
                        console.log("Login succesful");
                        //document.getElementById("start").style.visibility = "visible";
                    }
                });
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
                $scope.callInterview = "";
                $scope.isCallStarted = false;
                //document.getElementById("localVideo").style.display = "none";
                document.getElementById("remoteVideo").style.display = "none";
                CS.call.saveRecording(callid, 'recording.webm');
                console.log("call end");
                //document.getElementById("end").style.visibility = "hidden";
                //document.getElementById("mute").style.visibility = "hidden";
                break;
        }
    }

    $(document).ready(function () {
        $timeout(function () {
            onComponentLoad();
            $(".select2").select2();
        });
    });
}]);