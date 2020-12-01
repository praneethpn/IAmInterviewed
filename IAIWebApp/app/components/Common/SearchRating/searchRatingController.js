angular.module('IAMInterviewed').controller('searchRatingController', ['$scope', '$http', '$q', '$filter', '$rootScope', '$timeout', '$window', '$localStorage', function ($scope, $http, $q, $filter, $rootScope, $timeout, $window, $localStorage) {
    $scope.ClearAll = function () {
        $scope.InterviewId = "";
        $scope.showRatingDetails = false;
        $scope.ratingDetails = {};
    }
    $scope.ClearAll();

    $scope.SearchInterview = function () {
        manageLoader('load');
        var getInterviewsURL = IAMInterviewed.Candidate.GetCandidateRatingDetails + "?interviewId=" + $scope.InterviewId;
        $http.get(getInterviewsURL).then(function success(response) {
            $scope.ratingDetails = response.data.data;
            //console.log($scope.ratingDetails);
            if ($scope.ratingDetails.CandidateName == null || $scope.ratingDetails.CandidateName == "") {
                $scope.showRatingDetails = false;
                $rootScope.resultMessage = "Invalid Interview Id or No Data found for the Interview Id, please check interview id and try again.";
                showNotification('error');
                manageLoader();
            }
            else {
                $scope.showRatingDetails = true;
                BindRatingDonutChartTechRating(parseFloat($scope.ratingDetails.OveralRating));
                BindProfilesBySkillChart();
                $("#btnImageDownload").show();
                $("#btn-Convert-Html2Image").hide();
                $("#btn-Convert-Html2Image").removeAttr("download");
                $("#btn-Convert-Html2Image").removeAttr("href");
                $("#previewImage").empty();
                getCanvas = "";
            }
            manageLoader();
        }, function error(response) {
            $rootScope.resultMessage = response.data.errorMessage;
            showNotification('error');
            manageLoader();
        });
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
        //angular.forEach($scope.ratingDetails, function (value, key) {
        dataBarChartSkill.push($scope.ratingDetails.SecSkillName1);
        dataBarChartSkill.push($scope.ratingDetails.SecSKillName2);
        dataBarChartSkill.push($scope.ratingDetails.SecSkillName3);
        dataBarChartSkill.push($scope.ratingDetails.SecSkillName4);
        dataBarChartSkill.push($scope.ratingDetails.SecSkillName5);
        dataBarChart.push(parseInt($scope.ratingDetails.SecSkill1Rating));
        dataBarChart.push(parseInt($scope.ratingDetails.SecSkill2Rating));
        dataBarChart.push(parseInt($scope.ratingDetails.SecSkill3Rating));
        dataBarChart.push($scope.ratingDetails.SecSkill4Rating == "--" ? 0 : parseInt($scope.ratingDetails.SecSkill4Rating));
        dataBarChart.push($scope.ratingDetails.SecSkill5Rating == "--" ? 0 : parseInt($scope.ratingDetails.SecSkill5Rating));
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

    var doc = new jsPDF();
    var specialElementHandlers = {
        '#editor': function (element, renderer) {
            return true;
        }
    };

    $("#btnGeneratePdf").click(function () {
        var options = {};
        let doc = new jsPDF('p', 'pt', 'a4');

        doc.addHTML($("#tab_iaiformat"), 15, 15, options, function () {
            doc.save('Rating.pdf');
        });
        //doc.fromHTML($('#tab_iaiformat').html(), 15, 15, {
        //    'width': 170,
        //    'elementHandlers': specialElementHandlers
        //});
        //doc.save('Rating.pdf');
    });
    var element = $("#tab_iaiformat"); // global variable
    var getCanvas;
    $("#btnImageDownload").click(function () {
        html2canvas(element, {
            onrendered: function (canvas) {
                $("#previewImage").append(canvas);
                getCanvas = canvas;
            }
        });

        var imgageData = getCanvas.toDataURL("image/png");
        // Now browser starts downloading it instead of just showing it
        var newData = imgageData.replace(/^data:image\/png/, "data:application/octet-stream");
        $("#btn-Convert-Html2Image").attr("download", "Rating.png").attr("href", newData);
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