angular.module("IAMInterviewed").controller('ViewRatingIAIController', ['$scope', '$http', '$q', '$filter', '$rootScope', '$timeout', '$window', '$localStorage', function ($scope, $http, $q, $filter, $rootScope, $timeout, $window, $localStorage, $location) {
    $scope.$on('bindCandidateRatingDetails', function (e, reqId, scheduleId) {
        $scope.ratingDetailsIAI = {};        
        
        $("#imgLogoInnerIAI").attr("src", "");
        $("#lblCandidateNameIAI").text('');
        $("#lblInterviewIdIAI").text('');
        $("#lblDesignationIAI").text('');
        var canvas = document.getElementById('chartTotalRatingIAIFormat');
        var context = canvas.getContext('2d');
        clearCanvas(context, canvas);
        var canvas2 = document.getElementById('chartRatingBySkillIAIFormat');
        var context2 = canvas2.getContext('2d');
        clearCanvas(context2, canvas2);
        //$("#chartTotalRatingIAIFormat").text('');
        //$("#chartRatingBySkillIAIFormat").text('');

        $("#spanTotalRatingIAI").text('');
        $("#spanSecSkill1NameIAI").text('');
        $("#spanSkill1RatingIAI").text('');
        $("#pskill1commentsIAI").text('');
        $("#spanSecSkill2NameIAI").text('');
        $("#spanSkill2RatingIAI").text('');
        $("#pskill2commentsIAI").text('');
        $("#spanSecSkill3NameIAI").text('');
        $("#spanSkill3RatingIAI").text('');
        $("#pskill3commentsIAI").text('');
        $("#spanSecSkill4NameIAI").text('');
        $("#spanSkill4RatingIAI").text('');
        $("#pskill4commentsIAI").text('');
        $("#spanSecSkill5NameIAI").text('');
        $("#spanSkill5RatingIAI").text('');
        $("#pskill5commentsIAI").text('');

        $("#spanSoftSkillRatingIAI").text('');
        $("#spanEngCommIAI").text('');
        $("#pEngCommCommentsIAI").text('');
        $("#spanAttitudeIAI").text('');
        $("#pAttCommIAI").text('');
        $("#spanInterIAI").text('');
        $("#pInterCommIAI").text('');
        $("#pCommentsIAI").text('');

        $("#btnImageDownload").show();
        $("#btn-Convert-Html2Image").hide();
        $("#btn-Convert-Html2Image").removeAttr("download");
        $("#btn-Convert-Html2Image").removeAttr("href");
        $("#previewImage").empty();
        $timeout(function () {
            manageLoader('load');
            var getScheduleDetailsForViewRatingURL = IAMInterviewed.Company.getScheduleDetailsForViewRating + "?reqId=" + reqId + "&scheduleId=" + scheduleId;
            $http.get(getScheduleDetailsForViewRatingURL).then(function success(response) {
                $scope.ratingDetailsIAI = response.data.data;
                $("#imgLogoInnerIAI").attr("src", "../../../../assets/CompanyLogos/" + $scope.ratingDetailsIAI.Logo+"");
                $("#lblCandidateNameIAI").text($scope.ratingDetailsIAI.CandidateName);
                $("#lblInterviewIdIAI").text("Interview Id : " + $scope.ratingDetailsIAI.UniqueId + $scope.ratingDetailsIAI.ScheduleId);
                $("#lblDesignationIAI").text("Designation : " + $scope.ratingDetailsIAI.Designation);
                $("#spanTotalRatingIAI").text($scope.ratingDetailsIAI.OveralRating);
                $("#spanSecSkill1NameIAI").text($scope.ratingDetailsIAI.SecSkillName1);
                $("#spanSkill1RatingIAI").text($scope.ratingDetailsIAI.SecSkill1Rating);
                $("#pskill1commentsIAI").text($scope.ratingDetailsIAI.skill1comments);
                $("#spanSecSkill2NameIAI").text($scope.ratingDetailsIAI.SecSKillName2);
                $("#spanSkill2RatingIAI").text($scope.ratingDetailsIAI.SecSkill2Rating);
                $("#pskill2commentsIAI").text($scope.ratingDetailsIAI.skill2comments);
                $("#spanSecSkill3NameIAI").text($scope.ratingDetailsIAI.SecSkillName3);
                $("#spanSkill3RatingIAI").text($scope.ratingDetailsIAI.SecSkill3Rating);
                $("#pskill3commentsIAI").text($scope.ratingDetailsIAI.skill3comments);
                $("#spanSecSkill4NameIAI").text($scope.ratingDetailsIAI.SecSkillName4);
                $("#spanSkill4RatingIAI").text($scope.ratingDetailsIAI.SecSkill4Rating);
                $("#pskill4commentsIAI").text($scope.ratingDetailsIAI.skill4comments);
                $("#spanSecSkill5NameIAI").text($scope.ratingDetailsIAI.SecSkillName5);
                $("#spanSkill5RatingIAI").text($scope.ratingDetailsIAI.SecSkill5Rating);
                $("#pskill5commentsIAI").text($scope.ratingDetailsIAI.skill5comments);

                $("#spanSoftSkillRatingIAI").text($scope.ratingDetailsIAI.SoftSkillRating);
                $("#spanEngCommIAI").text($scope.ratingDetailsIAI.EnglishCommunication);
                $("#pEngCommCommentsIAI").text($scope.ratingDetailsIAI.EngComments);
                $("#spanAttitudeIAI").text($scope.ratingDetailsIAI.Attitude);
                $("#pAttCommIAI").text($scope.ratingDetailsIAI.Attitudecomments);
                $("#spanInterIAI").text($scope.ratingDetailsIAI.InterpersonalSkillCommunication);
                $("#pInterCommIAI").text($scope.ratingDetailsIAI.InterComments);
                $("#pCommentsIAI").text($scope.ratingDetailsIAI.InterviewerRemarks);
                
                BindRatingDonutChartTechRating(parseFloat($scope.ratingDetailsIAI.OveralRating));
                BindProfilesBySkillChart();
                //console.log(getCanvas);
                var audio = $("#audioPlayer");
                var AudioFileName = $scope.ratingDetailsIAI.AudioFile;
                $("#audio_src").attr("src", "../../../../assets/InterviewRecordings/" + AudioFileName);
                $("#audio_src_ogg").attr("src", "../../../../assets/InterviewRecordings/" + AudioFileName);
                audio[0].pause();
                audio[0].load();
                $("#candidateratingdetails").modal('show');
                manageLoader();
            }, function error(response) {
                $rootScope.resultMessage = response.data.errorMessage;
                showNotification('error');
                manageLoader();
            });
        }, 100);

    });

    function clearCanvas(context, canvas) {
        context.clearRect(0, 0, canvas.width, canvas.height);
        var w = canvas.width;
        canvas.width = 1;
        canvas.width = w;
    }

    function BindRatingDonutChartTechRating(chart_Value) {
        var chart_Value_percentage = Math.floor((chart_Value / 5) * 100);
        var chartProgress = document.getElementById("chartTotalRating");
        if (chartProgress) {
            var myChartCircle = new Chart(chartProgress, {
                type: 'doughnut',
                data: {
                    labels: ["Rating", 'null'],
                    datasets: [{
                        label: "",
                        backgroundColor: ["#147fb7"],
                        data: [chart_Value_percentage, 100 - chart_Value_percentage]
                    }]
                },
                plugins: [{
                    beforeDraw: function (chart) {
                        var width = chart.chart.width,
                            height = chart.chart.height,
                            ctx = chart.chart.ctx;

                        ctx.restore();
                        var fontSize = (height / 100).toFixed(2);
                        ctx.font = fontSize + "em sans-serif";
                        ctx.fillStyle = "#00950c";
                        ctx.textBaseline = "middle";

                        var text = "" + chart_Value_percentage + "%",
                            textX = Math.round((width - ctx.measureText(text).width) / 2),
                            textY = height / 2;

                        ctx.fillText(text, textX, textY);
                        ctx.save();
                    }
                }],
                options: {
                    legend: {
                        display: false,
                    },
                    responsive: true,
                    maintainAspectRatio: false,
                    cutoutPercentage: 65
                }

            });
        }
        var chartProgressIAIFormat = document.getElementById("chartTotalRatingIAIFormat");
        if (chartProgressIAIFormat) {
            var myChartCircle = new Chart(chartProgressIAIFormat, {
                type: 'doughnut',
                data: {
                    labels: ["Rating", 'null'],
                    datasets: [{
                        label: "",
                        backgroundColor: ["#147fb7"],
                        data: [chart_Value_percentage, 100 - chart_Value_percentage]
                    }]
                },
                plugins: [{
                    beforeDraw: function (chart) {
                        var width = chart.chart.width,
                            height = chart.chart.height,
                            ctx = chart.chart.ctx;

                        ctx.restore();
                        var fontSize = (height / 100).toFixed(2);
                        ctx.font = fontSize + "em sans-serif";
                        ctx.fillStyle = "#00950c";
                        ctx.textBaseline = "middle";

                        var text = "" + chart_Value_percentage + "%",
                            textX = Math.round((width - ctx.measureText(text).width) / 2),
                            textY = height / 2;

                        ctx.fillText(text, textX, textY);
                        ctx.save();
                    }
                }],
                options: {
                    legend: {
                        display: false,
                    },
                    responsive: true,
                    maintainAspectRatio: false,
                    cutoutPercentage: 65
                }

            });
        }
    }

    function BindProfilesBySkillChart() {
        var dataBarChartSkill = [];
        var dataBarChart = [];
        var backgroundColors = []
        //angular.forEach($scope.ratingDetailsIAI, function (value, key) {
        dataBarChartSkill.push($scope.ratingDetailsIAI.SecSkillName1);
        dataBarChartSkill.push($scope.ratingDetailsIAI.SecSKillName2);
        dataBarChartSkill.push($scope.ratingDetailsIAI.SecSkillName3);
        dataBarChartSkill.push($scope.ratingDetailsIAI.SecSkillName4);
        dataBarChartSkill.push($scope.ratingDetailsIAI.SecSkillName5);
        dataBarChart.push(parseInt($scope.ratingDetailsIAI.SecSkill1Rating));
        dataBarChart.push(parseInt($scope.ratingDetailsIAI.SecSkill2Rating));
        dataBarChart.push(parseInt($scope.ratingDetailsIAI.SecSkill3Rating));
        dataBarChart.push($scope.ratingDetailsIAI.SecSkill4Rating == "--" ? 0 : parseInt($scope.ratingDetailsIAI.SecSkill4Rating));
        dataBarChart.push($scope.ratingDetailsIAI.SecSkill5Rating == "--" ? 0 : parseInt($scope.ratingDetailsIAI.SecSkill5Rating));
        backgroundColors.push("#36c");
        backgroundColors.push("#36c");
        backgroundColors.push("#36c");
        backgroundColors.push("#36c");
        backgroundColors.push("#36c");
        //});
        var chartProgress = document.getElementById("chartRatingBySkillTechRating");
        if (chartProgress) {
            var myChartCircle = new Chart(chartProgress, {
                type: 'bar',
                data: {
                    datasets: [{
                        barPercentage: 0.5,
                        barThickness: 50,
                        minBarLength: 2,
                        data: dataBarChart,
                        backgroundColor: backgroundColors
                    }],
                    labels: dataBarChartSkill
                },
                options: {
                    legend: {
                        display: false,
                    },
                    scales: {
                        xAxes: [{
                            scaleLabel: {
                                display: true,
                                labelString: 'Secondary Skills'
                            },
                            ticks: {
                                display: true //this will remove only the label
                            }
                        }],
                        yAxes: [{
                            display: true,
                            scaleLabel: {
                                display: true,
                                labelString: 'Rating'
                            },
                            ticks: {
                                max: 5,
                                min: 0,
                                stepSize: 1
                            }
                        }]
                    },
                    responsive: true,
                    maintainAspectRatio: false
                }
            });
        }
        var chartProgressIAIFormat = document.getElementById("chartRatingBySkillIAIFormat");
        if (chartProgressIAIFormat) {
            var myChartCircle = new Chart(chartProgressIAIFormat, {
                type: 'bar',
                data: {
                    datasets: [{
                        barPercentage: 0.5,
                        barThickness: 50,
                        minBarLength: 2,
                        data: dataBarChart,
                        backgroundColor: backgroundColors
                    }],
                    labels: dataBarChartSkill
                },
                options: {
                    legend: {
                        display: false,
                    },
                    scales: {
                        xAxes: [{
                            scaleLabel: {
                                display: true,
                                labelString: 'Secondary Skills'
                            },
                            ticks: {
                                display: true //this will remove only the label
                            }
                        }],
                        yAxes: [{
                            display: true,
                            scaleLabel: {
                                display: true,
                                labelString: 'Rating'
                            },
                            ticks: {
                                max: 5,
                                min: 0,
                                stepSize: 1
                            }
                        }]
                    },
                    responsive: true,
                    maintainAspectRatio: false
                }
            });
        }
    }

    $("#btnGeneratePdf").click(function () {        
        var options = {};
        var doc = new jsPDF('p', 'pt', 'a4');
        doc.addHTML($("#tab_iaiformat"), 15, 15, options, function () {
            doc.save($scope.ratingDetailsIAI.CandidateName);
        });
    });

    $("#btnImageDownload").click(function () {
        var element = $("#tab_iaiformat"); // global variable
        var getCanvas;
        html2canvas(element, {
            onrendered: function (canvas) {
                $("#previewImage").append(canvas);
                getCanvas = canvas;
            }
        });

        var imgageData = getCanvas.toDataURL("image/png");
        // Now browser starts downloading it instead of just showing it
        var newData = imgageData.replace(/^data:image\/png/, "data:application/octet-stream");
        $("#btn-Convert-Html2Image").attr("download", $scope.ratingDetailsIAI.CandidateName).attr("href", newData);
        $("#btnImageDownload").hide();
        $("#btn-Convert-Html2Image").show();
    });

    $scope.printRating = function () {
        var contents = $("#tab_iaiformat").html();
        var frame1 = $('<iframe />');
        frame1[0].name = "frame1";
        frame1.css({ "position": "absolute", "top": "-1000000px" });
        $("body").append(frame1);
        var frameDoc = frame1[0].contentWindow ? frame1[0].contentWindow : frame1[0].contentDocument.document ? frame1[0].contentDocument.document : frame1[0].contentDocument;
        frameDoc.document.open();
        //Create a new HTML document.
        frameDoc.document.write('<html><head><title>Rating</title>');
        frameDoc.document.write('</head><body>');
        //Append the external CSS file.
        frameDoc.document.write('<link href="../css/control.css" rel="stylesheet">');
        frameDoc.document.write('<link href="../css/Pages/CompanyDashboard.css" rel="stylesheet" />');
        //Append the DIV contents.
        frameDoc.document.write(contents);
        frameDoc.document.write('</body></html>');
        frameDoc.document.close();
        setTimeout(function () {
            window.frames["frame1"].focus();
            window.frames["frame1"].print();
            frame1.remove();
        }, 100);


        //var headstr = "<html><head><title></title>"
        //   + "<link href='css/Pages/CompanyDashboard.css' rel='stylesheet' /></head><body>";
        //var footstr = "</body>";
        //var newstr = document.all.item(printpage).innerHTML;
        //var oldstr = document.body.innerHTML;
        //document.body.innerHTML = headstr + newstr + footstr;
        //window.print();
        //document.body.innerHTML = oldstr;
        return false;
    }
}]);