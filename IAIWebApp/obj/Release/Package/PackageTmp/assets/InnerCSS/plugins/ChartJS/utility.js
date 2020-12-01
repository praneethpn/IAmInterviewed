function BindPieChart(elem) {
    $.each(elem, function (index, element) {
        InitPieChart(element);
    });
}

function InitPieChart(element) {
    Chart.defaults.global.showTooltips = false;
    var value = $(element).attr("data-value");
    var clr = $(element).attr("data-color");
    var altclr = $(element).attr("data-alt-color");
    if (clr == 'amber') {
        clr = "orange";
    }
    if (altclr == null || altclr == "" || altclr == "undefined") {
        altclr = "#e0e0e0";
    }

    var elem_Id = $(element).attr("id");
    var negativeValue = eval(100 - value);
    var pieData = [
        {
            value: value,
            color: clr

        },
        {
            value: negativeValue,
            color: altclr

        }
    ];

    Chart.types.Doughnut.extend({
        name: "DoughnutAlt",
        onAnimationComplete: function () {
            this.hideTooltip(this.segments, true);
        },
        customTooltips: function (tooltip) {
            if (!tooltip) {
                return;
            }
            tooltip.css({ "display": "none" });
        },
        draw: function () {
            Chart.types.Doughnut.prototype.draw.apply(this, arguments);
            this.chart.ctx.textBaseline = "middle";
            this.chart.ctx.fillStyle = 'black'
            this.chart.ctx.font = "50px Roboto";
            this.chart.ctx.textAlign = "center";
            this.chart.ctx.font = "20px Roboto";
        }
    });

    var pieChart = document.getElementById(elem_Id).getContext("2d");
    new Chart(pieChart).DoughnutAlt(pieData, {
        percentageInnerCutout: 80, animationEasing: "easeOutQuart"
    });
}

function unSlickobject(element) {
    //$(element).slick('unslick');
    $('.regular').slick('unslick');
}

function initSlider(element, noOfSlidesToShow) {
    $(element).slick({
        dots: false,
        infinite: false,
        speed: 300,
        adaptiveHeight: true,
        slidesToShow: noOfSlidesToShow,
        slidesToScroll: 1,
        responsive: [
          {
              breakpoint: 1024,
              settings: {
                  slidesToShow: 3,
                  slidesToScroll: 3,
                  infinite: true,
                  dots: false
              }
          },
          {
              breakpoint: 600,
              settings: {
                  slidesToShow: 2,
                  slidesToScroll: 2
              }
          },
          {
              breakpoint: 480,
              settings: {
                  slidesToShow: 1,
                  slidesToScroll: 1
              }
          }
          // You can unslick at a given breakpoint now by adding:
          // settings: "unslick"
          // instead of a settings object
        ]
    });
}

function removeSlick(element) {
    $(element).removeClass("slick-initialized");
    $(element).removeClass("slick-slider");
    $(element).removeClass("slick-dotted");
}

function InitTabs(element) {
    $(element).tabs();
}

function InitComboChart(type) {
    if (type == 1) {
        google.charts.setOnLoadCallback(drawVisualization1);
    }
    else if (type == 2) {
        google.charts.setOnLoadCallback(drawVisualization2);
    }
    else if (type == 3) {
        google.charts.setOnLoadCallback(drawVisualization3);
    }
}

function LoadStepedAreaChart() {
    google.charts.setOnLoadCallback(drawStepedAreaChart);
}

function drawStepedAreaChart() {
    var data = google.visualization.arrayToDataTable([
    ['Department', '5-wk avg.', 'Actuals'],
['Men Formals'
, 5
, 7
],
['Women Formals'
, 6
, 8
],
['Men Footwear'
, 7
, 13
],
['Women Footwear'
, 10
, 14
],
['Men Casuals'
, 13
, 12
],
['Women Casuals'
, 9
, 11
],
['Home - Kitchen'
, 4
, 6
],
['Home - Décor'
, 6
, 6
],
['Home - Garden'
, 7
, 11
],
['Home - Furniture'
, 5
, 9
],
['Kid Clothing '
, 9
, 12
],
['Kids Footwear '
, 7
, 8
],
['Sports Wear'
, 6
, 9
],
['Home-Electronics'
, 8
, 10
],
['Beauty & Health'
, 8
, 10
],
['Books'
, 4
, 7
],
['Home - Appliances'
, 6
, 8
]


    ]);

    var options = {
        title: 'Average Wait Time',
        vAxis: { title: '5-wk avg.' },
        hAxis: { title: 'Departments' },
        isStacked: false,
        connectSteps: false,
        series: {
            0: { color: '#00b0ff', areaOpacity: 0.5 },
            1: { color: '#bdbdbd' }
        }


    };

    var chart = new google.visualization.SteppedAreaChart(document.getElementById('chart_div1'));

    chart.draw(data, options);
}

function InsightsComboChart() {
    google.charts.setOnLoadCallback(drawInsightVisualization);
}

function drawInsightVisualization() {
    var data = google.visualization.arrayToDataTable([
        ['Hours', 'HistoricalSalesAvg', 'CurrentDaySalesActuals', { role: 'annotation' }, { role: 'annotation' }, 'CurrentDaySalesPredicted', { role: 'annotation' }],
        ['	1	', 0.963424545, 1.445136817, null, null, 1.445136817, null],
        ['	2	', 1.279032126, 1.918548188, null, null, 1.918548188, null],
        ['	3	', 2.461690634, 3.692535951, null, null, 3.692535951, null],
        ['	4	', 2.905925734, 4.358888601, '13 Units Left', null, 4.358888601, null],
        ['	5	', 3.936025081, 5.904037622, null, null, 5.904037622, null],
        ['	6	', 5.272584399, 7.908876599, null, null, 7.908876599, null],
        ['	7	', 7.965954649, 11.94893197, null, null, 11.94893197, null],
        ['	8	', 8.716669333, 13.075004, '3 Units Left', null, 13.075004, null],
        ['	9	', 9.875834015, 14.81375102, null, null, 14.81375102, null],
        ['	10	', 11.44653549, 15.16980324, null, null, 15.16980324, null],
        ['	11	', 12.15110229, 16.22665344, null, null, 16.22665344, null],
        ['	12	', 13.03260721, 16.54891081, null, null, 16.54891081, null],
        ['	13	', 13.40423733, 17.106356, null, null, 17.106356, null],
        ['	14	', 14.04218933, 17.863284, 'Current', '', 17.863284, null],
        ['	15	', 14.49565067, , null, null, 17.99995, null],
        ['	16	', 14.51802133, , null, null, 18, null],
        ['	17	', 14.14009992, , null, null, 19.55, null],
        ['	18	', 13.33073333, , null, null, 19.9961, null],
        ['	19	', 12.975712, , null, null, 19.463568, null],
        ['	20	', 12.4332, , null, null, 20, 'OutOfStock'],
        ['	21	', 11.25, , null, null, , null],
        ['	22	', 10.22222133, , null, null, , null],
        ['	23	', 9.216493333, , null, null, , null],
        ['	24	', 7.455634667, , null, null, , null]
    ]);

    var options = {
        title: 'Rate of Sales in a Month',
        width: 1100,
        height: 400,
        annotations: {
            0: {
                textStyle: {
                    fontName: 'Roboto',
                    fontSize: 18,
                    bold: true,
                    italic: true,
                    color: '#212121',     // The color of the text.
                    //auraColor: '#d799ae', // The color of the text outline.
                    opacity: 0.8,
                },      // The transparency of the text.
                4: { style: 'line' }
            }
        },
        vAxes: {
            0: {
                gridlines: { color: 'transparent' },

                title: 'Rate of Sales',
            }
        },
        hAxis: {
            title: 'Hours',
            gridlines: {
                count: -1,
                color: 'transparent',
            }
        },
        series: {
            0: { lineWidth: 4, color: '#29b6f6' },//HistoricalSales
            1: { lineWidth: 4, color: '#66bb6a' },//currentdaySalesActuals
            2: { lineDashStyle: [14, 2, 7, 2], color: '#66bb6a' },//currentdaySalesPredicted
        },
        curveType: 'function',
        animation: {
            "startup": true,
            duration: 1000,
            easing: 'out'
        },

    }

    var chart = new google.visualization.LineChart(document.getElementById('chart_div3'));
    chart.draw(data, options);
}

function drawVisualization1() {
    // Some raw data (not necessarily accurate)
    var data = google.visualization.arrayToDataTable([
        ['Date', 'Gross Sales', 'Median', 'Max'],
        ['12/31/2016', 16768, 13902.917, 22708.834],
        ['12/30/2016', 22286, 5611.4144, 8153.3088],
        ['12/29/2016', 17976, 13231.98, 19133],
        ['12/28/2016', 8272, 11890.708, 16636.406],
        ['12/27/2016', 21817, 11833.1639, 19484.9978],
        ['12/26/2016', 15168, 8332.4412, 12273.4224],
        ['12/25/2016', 13391, 9861.912, 15614.694],
        ['12/24/2016', 16998, 16895.63475, 27722.7195],
        ['12/23/2016', 22932, 10938.2328, 18028.7856],
        ['12/22/2016', 11341, 8347.41495, 11806.7499],
        ['12/21/2016', 12775, 6938.9292, 10805.5584],
        ['12/20/2016', 15915, 7109.116, 9802.632],
        ['12/19/2016', 15628, 7674.9008, 9960.8716],
        ['12/18/2016', 8486, 4917.9438, 7161.8976],
        ['12/17/2016', 13166, 7704.41315, 10573.9263],
        ['12/16/2016', 17317, 7462.89395, 10174.2179],
        ['12/15/2016', 11598, 16362.092, 25095.944],
        ['12/14/2016', 12817, 12764.3906, 20243.6612],
        ['12/13/2016', 21467, 15812.2095, 22941.009],
        ['12/12/2016', 20559, 7288.326, 10761.102],
        ['12/11/2016', 17399, 15383.676, 22358.952],
        ['12/10/2016', 22311, 11322.3, 14454],
        ['12/9/2016', 15880, 3782.856, 5744.112],
        ['12/8/2016', 13066, 9977.2393, 13001.6986],
        ['12/7/2016', 8638, 8346.5773, 12441.4346],
        ['12/6/2016', 8388, 5982.1652, 8584.0304],
        ['12/5/2016', 20845, 3885.2385, 5136.417],
        ['12/4/2016', 15174, 8741.048, 13457.136],
        ['12/3/2016', 20359, 8728.4925, 13752.585],
        ['12/2/2016', 13274, 3772.7222, 5213.8944],
        ['12/1/2016', 15586, 8633.5282, 13086.9264]
    ]);

    var options = {
        vAxis: {
            title: 'Gross Sales'
        },
        hAxis: {
            title: 'Date'
        },
        curveType: 'function',
        seriesType: 'line',
        animation: {
            startup: true,
            duration: 500,
            easing: 'in'
        },
        series: {
            0: {
                type: 'bars',
                color: '#33aeda'
            },
            1: {
                color: '#ff8a80'
            },
            2: {
                color: '#33691e'
            }
        }
    };

    var chart = new google.visualization.ComboChart(document.getElementById('chart_div'));
    chart.draw(data, options);
}

function drawVisualization2() {
    // Some raw data (not necessarily accurate)
    var data = google.visualization.arrayToDataTable([
        ['Week', 'Gross Sales', 'Median', 'Max'],
        ['36', 54856, 24066.7062, 35651.2724],
        ['37', 99476, 59344.2453, 86786.6406],
        ['38', 113351, 77460.71415, 119921.0383],
        ['39', 109467, 60126.4335, 91126.257],
        ['40', 105335, 72120.1804, 108296.8908],
        ['41', 80120, 59644.4275, 91227.755],
        ['42', 107919, 59405.364, 89427.788],
        ['43', 98801, 63811.1935, 98164.747],
        ['44', 75815, 51688.1607, 77988.0814],
        ['45', 125402, 73164.49715, 110751.6543],
        ['46', 117544, 52663.67445, 80664.8889],
        ['47', 108897, 51379.4719, 76274.5638],
        ['48', 110992, 63478.6615, 96285.543],
        ['49', 122379, 57705.7784, 85327.0468],
        ['50', 104302, 52037.4243, 72818.8286],
        ['51', 114323, 82778.0012, 122148.8124],
        ['52', 104075, 62822.1723, 95289.2146],
        ['53', 115678, 74664.5365, 114004.663]
    ]);

    var options = {
        vAxis: {
            title: 'Gross Sales'
        },
        hAxis: {
            title: 'Week'
        },
        curveType: 'function',
        seriesType: 'line',
        animation: {
            startup: true,
            duration: 500,
            easing: 'in'
        },
        series: {
            0: {
                type: 'bars',
                color: '#33aeda'
            },
            1: {
                color: '#FB8C00'
            },
            2: {
                color: '#6E6E6E'
            }
        }
    };

    var chart = new google.visualization.ComboChart(document.getElementById('chart_div'));
    chart.draw(data, options);
}

function drawVisualization3() {
    // Some raw data (not necessarily accurate)
    var data = google.visualization.arrayToDataTable([
        ['Date', 'Gross Sales', 'Median', 'Max'],
        ['12/31/2016', 16768, 13902.917, 22708.834],
        ['12/30/2016', 22286, 5611.4144, 8153.3088],
        ['12/29/2016', 17976, 13231.98, 19133],
        ['12/28/2016', 8272, 11890.708, 16636.406],
        ['12/27/2016', 21817, 11833.1639, 19484.9978],
        ['12/26/2016', 15168, 8332.4412, 12273.4224],
        ['12/25/2016', 13391, 9861.912, 15614.694],
        ['12/24/2016', 16998, 16895.63475, 27722.7195],
        ['12/23/2016', 22932, 10938.2328, 18028.7856],
        ['12/22/2016', 11341, 8347.41495, 11806.7499],
        ['12/21/2016', 12775, 6938.9292, 10805.5584],
        ['12/20/2016', 15915, 7109.116, 9802.632],
        ['12/19/2016', 15628, 7674.9008, 9960.8716],
        ['12/18/2016', 8486, 4917.9438, 7161.8976],
        ['12/17/2016', 13166, 7704.41315, 10573.9263],
        ['12/16/2016', 17317, 7462.89395, 10174.2179],
        ['12/15/2016', 11598, 16362.092, 25095.944],
        ['12/14/2016', 12817, 12764.3906, 20243.6612],
        ['12/13/2016', 21467, 15812.2095, 22941.009],
        ['12/12/2016', 20559, 7288.326, 10761.102],
        ['12/11/2016', 17399, 15383.676, 22358.952],
        ['12/10/2016', 22311, 11322.3, 14454],
        ['12/9/2016', 15880, 3782.856, 5744.112],
        ['12/8/2016', 13066, 9977.2393, 13001.6986],
        ['12/7/2016', 8638, 8346.5773, 12441.4346],
        ['12/6/2016', 8388, 5982.1652, 8584.0304],
        ['12/5/2016', 20845, 3885.2385, 5136.417],
        ['12/4/2016', 15174, 8741.048, 13457.136],
        ['12/3/2016', 20359, 8728.4925, 13752.585],
        ['12/2/2016', 13274, 3772.7222, 5213.8944],
        ['12/1/2016', 15586, 8633.5282, 13086.9264]
    ]);

    var options = {
        vAxis: {
            title: 'Gross Sales'
        },
        hAxis: {
            title: 'Date'
        },
        curveType: 'function',
        seriesType: 'line',
        animation: {
            startup: true,
            duration: 500,
            easing: 'in'
        },
        series: {
            0: {
                type: 'bars',
                color: '#33aeda'
            },
            1: {
                color: '#ff8a80'
            },
            2: {
                color: '#33691e'
            }
        }
    };

    var chart = new google.visualization.ComboChart(document.getElementById('chart_div'));
    chart.draw(data, options);
}

function InitLineChart(element) {
    var lineData = {
        labels: ["Jan", "Feb", "March", "April", "May", "June", "July", "August", "September"],
        datasets: [{
            fillColor: "rgba(255,255,255,0)",
            strokeColor: "rgba(63,169,245,1)",
            pointColor: "rgba(63,169,245,1)",
            pointStrokeColor: "#fff",
            data: [65, 59, 90, 81, 56, 55, 40, 54, 100]
        }, {
            fillColor: "rgba(255,255,255,0)",
            strokeColor: "rgba(102,45,145,1)",
            pointColor: "rgba(102,45,145,1)",
            pointStrokeColor: "#fff",
            data: [28, 48, 40, 19, 96, 27, 100, 89, 56]
        }]
    }

    var lineOptions = {
        animation: true,
        pointDot: true,
        scaleOverride: true,
        scaleShowGridLines: false,
        scaleShowLabels: true,
        scaleSteps: 4,
        scaleStepWidth: 25,
        scaleStartValue: 25,
    };

    var ctx = document.getElementById(element).getContext("2d");
    var myNewChart = new Chart(ctx).Line(lineData, lineOptions);
}

function ShowLoader() {
    $(".loader-overlay").show();
    $(".loader-image-container").show();
}

function HideLoader() {
    $(".loader-overlay").hide();
    $(".loader-image-container").hide();
}

function ShowMiniLoader() {
    $(".loader-overlay").css({ "opacity": "0" })
    $(".loader-overlay").show();
    $(".loader-img-cntnr").show();
}

function HideMiniLoader() {
    $(".loader-overlay").css({ "opacity": "0.9" })
    $(".loader-overlay").hide();
    $(".loader-img-cntnr").hide();
}

function assignTask() {

    $(".assign-task").click(function () {
        $(".taskbar").toggleClass("sidebar-open");
        var overlay = $('<div class="overlay">');
        if ($('.taskbar').hasClass("sidebar-open")) {
            $('body').append(overlay);
        }
        else {
            $(".overlay").remove();
        }
    });
    $(".close-taskbar").click(function () {

        $(".taskbar").removeClass("sidebar-open");
        $(".overlay").remove();

    });


    //$(".submit").click(function () {
    //    swal("Task Assigned !", "", "success");
    //    $(".taskbar").removeClass("sidebar-open");
    //    $(".overlay").remove();

    //});
    $('.datepicker').pickadate({
        selectMonths: true, // Creates a dropdown to control month
        selectYears: 16/// , // Creates a dropdown of 15 years to control year
        //// format: 'yyyy-mm-dd'
    });
}
var heatmap;
var flag = 0;
$(document).ready(function () {
    //heatmap script
    if (window.location.href.toLowerCase().indexOf("livetracking") > -1) {
        window.onload = Makebubles;
    }
});

function InsightCloseEvent() {
    $(".jq-insight-close").on("click", function () {
        $(this).parent().hide();
    });
}

function Makebubles() {
    var width = $(window).width();
    var radii;
    var storecords = document.getElementById("storecoordinates").coords;
    var cord = storecords.split(',');
    var imagecords = document.getElementById("imagecoordinates").coords;
    var imgcord = imagecords.split(',');
    var value = imgcord[0];
    //if (width>=1025)
    if (imgcord[0] < 25) { radii = 10; }
    var loadheatmap = function () {
        var timerLoop;
        var data;
        if (flag == 1) {
            var canvas = heatmap._renderer.canvas;
            $(canvas).remove();
            heatmap = null;
        }
        var config = {
            container: document.getElementById('heatmapContainer'),
            //radius: 40,
            radius: radii,
            maxOpacity: .5,
            minOpacity: 0,
            blur: .75
        };

        heatmap = h337.create(config);
        flag = 1;
        var points = [];
        var datapoints = [];
        var val;
        var points = [{
            //men's casuals
            //x: 98,
            //y: 45,
            x: cord[0],
            y: cord[1],
            value: Math.floor(Math.random() * (5 - (-1) + 1)) + (-1)

            // radius configuration on point basis
            //radius:70
        },
      {
          //x: 79,
          //y: 74,
          x: cord[2],
          y: cord[3],
          value: Math.floor(Math.random() * (5 - (-1) + 1)) + (-1)
      },
          {
              //x: 113,
              //y: 67,
              x: cord[4],
              y: cord[5],
              value: Math.floor(Math.random() * (5 - (-1) + 1)) + (-1)
          },
  //Men's formals
          {
              //x: 202,
              //y: 45,
              x: cord[6],
              y: cord[7],
              value: Math.floor(Math.random() * (5 - (-1) + 1)) + (-1),
              radius: 30
          },
          {
              //x: 230,
              //y: 69,
              x: cord[8],
              y: cord[9],
              value: Math.floor(Math.random() * (5 - (-1) + 1)) + (-1),
              radius: 40
          },
          {
              //x: 200,
              //y: 100,
              x: cord[10],
              y: cord[11],
              value: Math.floor(Math.random() * (5 - (-1) + 1)) + (-1),
              radius: 40
          },
          //Men's Footwear
          {
              // x: 334,
              //y: 42,
              x: cord[12],
              y: cord[13],
              value: Math.floor(Math.random() * (5 - (-1) + 1)) + (-1),
              radius: 30

          },
          {
              //x: 363,
              //y: 69,
              x: cord[14],
              y: cord[15],
              value: Math.floor(Math.random() * (5 - (-1) + 1)) + (-1),
              radius: 40

          },
          {
              //x: 298,
              //y: 70,
              x: cord[16],
              y: cord[17],
              value: Math.floor(Math.random() * (5 - (-1) + 1)) + (-1),
              radius: 40

          },
          {
              //x: 331,
              //y: 100,
              x: cord[18],
              y: cord[19],
              value: Math.floor(Math.random() * (5 - (-1) + 1)) + (-1),
              radius: 40

          },
          //Accessories
      {
          // x: 332,
          //y: 186,
          x: cord[20],
          y: cord[21],
          value: Math.floor(Math.random() * (5 - (-1) + 1)) + (-1),
          radius: 60
      },
      {
          //x: 301,
          //y: 168,
          x: cord[22],
          y: cord[23],
          value: Math.floor(Math.random() * (5 - (-1) + 1)) + (-1),
          radius: 40
      },
      {
          //x: 370,
          // y: 172,
          x: cord[24],
          y: cord[25],
          value: Math.floor(Math.random() * (5 - (-1) + 1)) + (-1),
          radius: 40
      },
      //sports equipment
      {
          // x: 458,
          //y: 39,
          x: cord[26],
          y: cord[27],
          value: Math.floor(Math.random() * (5 - (-1) + 1)) + (-1),
          radius: 55
      },
      {
          //x: 419,
          // y: 61,
          x: cord[28],
          y: cord[29],
          value: Math.floor(Math.random() * (5 - (-1) + 1)) + (-1),
          radius: 55
      },
      {
          //x: 470,
          //y: 110,
          x: cord[30],
          y: cord[31],
          value: Math.floor(Math.random() * (5 - (-1) + 1)) + (-1),
          radius: 55
      },
      {
          //x: 491,
          // y: 87,
          x: cord[32],
          y: cord[33],
          value: Math.floor(Math.random() * (5 - (-1) + 1)) + (-1),
          radius: 55
      },
      {
          // x: 434,
          // y: 97,
          x: cord[34],
          y: cord[35],
          value: Math.floor(Math.random() * (5 - (-1) + 1)) + (-1),
          radius: 55
      },
      //kid's toys
      {
          //x: 568,
          //y: 37,
          x: cord[36],
          y: cord[37],
          value: Math.floor(Math.random() * (5 - (-1) + 1)) + (-1),
          radius: 20
      },
      {
          //x: 599,
          //y: 49,
          x: cord[38],
          y: cord[39],
          value: Math.floor(Math.random() * (5 - (-1) + 1)) + (-1),
          radius: 30
      },
      {
          // x: 587,
          //y: 88,
          x: cord[40],
          y: cord[41],
          value: Math.floor(Math.random() * (5 - (-1) + 1)) + (-1),
          radius: 30
      },
      {
          //x: 578,
          //y: 100,
          x: cord[42],
          y: cord[43],
          value: Math.floor(Math.random() * (5 - (-1) + 1)) + (-1),
          radius: 30
      },
      //frontdesk
      {
          //x: 125,
          //y: 180,
          x: cord[44],
          y: cord[45],
          value: Math.floor(Math.random() * (5 - (-1) + 1)) + (-1),
          radius: 50
      },
      {
          //x: 109,
          //y: 218,
          x: cord[46],
          y: cord[47],
          value: Math.floor(Math.random() * (5 - (-1) + 1)) + (-1),
          radius: 50
      },
      {
          //x: 94,
          //y: 138,
          x: cord[48],
          y: cord[49],
          value: Math.floor(Math.random() * (5 - (-1) + 1)) + (-1),
          radius: 50
      },
      {
          //x: 121,
          //y: 222,
          x: cord[50],
          y: cord[51],
          value: Math.floor(Math.random() * (5 - (-1) + 1)) + (-1),
          radius: 50
      },
      //POS
      {
          //x: 42,
          //y: 130,
          x: cord[52],
          y: cord[53],
          value: Math.floor(Math.random() * (5 - (-1) + 1)) + (-1),
          radius: 20
      },
      //beside frontdesk
      {
          //x: 204,
          //y: 179,
          x: cord[54],
          y: cord[55],
          value: Math.floor(Math.random() * (5 - (-1) + 1)) + (-1),
          radius: 40
      },
      {
          // x: 243,
          //y: 196,
          x: cord[56],
          y: cord[57],
          value: Math.floor(Math.random() * (5 - (-1) + 1)) + (-1),
          radius: 40
      },
      {
          //x: 188,
          //y: 198,
          x: cord[58],
          y: cord[59],
          value: Math.floor(Math.random() * (5 - (-1) + 1)) + (-1),
          radius: 30
      },
      //health&beauty
      {
          // x: 424,
          //y: 156,
          x: cord[60],
          y: cord[61],
          value: Math.floor(Math.random() * (5 - (-1) + 1)) + (-1),
          //radius: 30
          radius: 60
      },
      {
          // x: 419,
          //y: 180,
          x: cord[62],
          y: cord[63],
          value: Math.floor(Math.random() * (5 - (-1) + 1)) + (-1),
          radius: 60
      },
      //playzone
      {
          //x: 521,
          //y: 192,
          x: cord[64],
          y: cord[65],
          value: Math.floor(Math.random() * (5 - (-1) + 1)) + (-1),
          radius: 55
      },
      {
          //x: 475,
          //y: 181,
          x: cord[66],
          y: cord[67],
          value: Math.floor(Math.random() * (5 - (-1) + 1)) + (-1),
          radius: 55
      },
      {
          //x: 555,
          //y: 167,
          x: cord[68],
          y: cord[69],
          value: Math.floor(Math.random() * (5 - (-1) + 1)) + (-1),
          radius: 55
      },
      {
          //x: 561,
          //y: 193,
          x: cord[70],
          y: cord[71],
          value: Math.floor(Math.random() * (5 - (-1) + 1)) + (-1),
          radius: 55
      },
      //besides backroom
      {
          //x: 642,
          // y: 197,
          x: cord[72],
          y: cord[73],
          value: Math.floor(Math.random() * (5 - (-1) + 1)) + (-1),
          radius: 40
      },
      {
          //x: 617,
          // y: 170,
          x: cord[74],
          y: cord[75],
          value: Math.floor(Math.random() * (5 - (-1) + 1)) + (-1),
          radius: 40
      },
      {
          //x: 643,
          //y: 150,
          x: cord[76],
          y: cord[77],
          value: Math.floor(Math.random() * (5 - (-1) + 1)) + (-1),
          radius: 40
      },
      {
          //x: 641,
          //y: 231,
          x: cord[78],
          y: cord[79],
          value: Math.floor(Math.random() * (5 - (-1) + 1)) + (-1),
          radius: 40
      },
      //men's trial room
      {
          // x: 697,
          // y: 40,
          x: cord[80],
          y: cord[81],
          value: Math.floor(Math.random() * (5 - (-1) + 1)) + (-1),
          radius: 30
      },
      //women's trial room
      {
          //x: 751,
          // y: 61,
          x: cord[82],
          y: cord[83],
          value: Math.floor(Math.random() * (5 - (-1) + 1)) + (-1),
          radius: 40
      },
      //office
      {
          // x: 706,
          //y: 145,
          x: cord[84],
          y: cord[85],
          value: Math.floor(Math.random() * (5 - (-1) + 1)) + (-1),
          radius: 40
      },
      {
          //x: 719,
          //y: 121,
          x: cord[86],
          y: cord[87],
          value: Math.floor(Math.random() * (5 - (-1) + 1)) + (-1),
          radius: 40
      },
      //backroom
      {
          //x: 709,
          //y: 197,
          x: cord[88],
          y: cord[89],
          value: Math.floor(Math.random() * (5 - (-1) + 1)) + (-1),
          radius: 40
      },
      {
          //x: 696,
          //y: 232,
          x: cord[90],
          y: cord[91],
          value: Math.floor(Math.random() * (5 - (-1) + 1)) + (-1),
          radius: 40
      },
      //Women's casual
      {
          //x: 96,
          //y: 311,
          x: cord[92],
          y: cord[93],
          value: Math.floor(Math.random() * (5 - (-1) + 1)) + (-1),
          radius: 40
      },
      {
          //x: 83,
          //y: 279,
          x: cord[94],
          y: cord[95],
          value: Math.floor(Math.random() * (5 - (-1) + 1)) + (-1),
          radius: 40
      },
      {
          //x: 120,
          //y: 318,
          x: cord[96],
          y: cord[97],
          value: Math.floor(Math.random() * (5 - (-1) + 1)) + (-1),
          radius: 40
      },
      //women's formal
      {
          //x: 208,
          //y: 309,
          x: cord[98],
          y: cord[99],
          value: Math.floor(Math.random() * (5 - (-1) + 1)) + (-1),
          radius: 40
      },
      {
          //x: 172,
          //y: 299,
          x: cord[100],
          y: cord[101],
          value: Math.floor(Math.random() * (5 - (-1) + 1)) + (-1),
          radius: 40
      },
      {
          //x: 208,
          //y: 273,
          x: cord[102],
          y: cord[103],
          value: Math.floor(Math.random() * (5 - (-1) + 1)) + (-1),
          radius: 40
      },
      {
          // x: 222,
          // y: 260,
          x: cord[104],
          y: cord[105],
          value: Math.floor(Math.random() * (5 - (-1) + 1)) + (-1),
          radius: 30
      },
      //women's footwear
      {
          //x: 332,
          //y: 310,
          x: cord[106],
          y: cord[107],
          value: Math.floor(Math.random() * (5 - (-1) + 1)) + (-1),
          radius: 40
      },
       {
           //x: 304,
           //y: 287,
           x: cord[108],
           y: cord[109],
           value: Math.floor(Math.random() * (5 - (-1) + 1)) + (-1),
           radius: 40
       },
        {
            //x: 331,
            //y: 264,
            x: cord[110],
            y: cord[111],
            value: Math.floor(Math.random() * (5 - (-1) + 1)) + (-1),
            radius: 40
        },
        {
            //x: 337,
            //y: 272,
            x: cord[112],
            y: cord[113],
            value: Math.floor(Math.random() * (5 - (-1) + 1)) + (-1),
            radius: 30
        },
        //active wear
        {
            // x: 459,
            //y: 306,
            x: cord[114],
            y: cord[115],
            value: Math.floor(Math.random() * (5 - (-1) + 1)) + (-1),
            radius: 40
        },
        {
            // x: 425,
            //y: 281,
            x: cord[116],
            y: cord[117],
            value: Math.floor(Math.random() * (5 - (-1) + 1)) + (-1),
            radius: 40
        },
        {
            //x: 413,
            //y: 286,
            x: cord[118],
            y: cord[119],
            value: Math.floor(Math.random() * (5 - (-1) + 1)) + (-1),
            radius: 40
        },
        {
            // x: 467,
            //y: 257,
            x: cord[120],
            y: cord[121],
            value: Math.floor(Math.random() * (5 - (-1) + 1)) + (-1),
            radius: 40
        },
        //kid's clothing
        {
            //x: 571,
            //y: 312,
            x: cord[122],
            y: cord[123],
            value: Math.floor(Math.random() * (5 - (-1) + 1)) + (-1),
            radius: 50
        },
        {
            //x: 540,
            // y: 283,
            x: cord[124],
            y: cord[125],
            value: Math.floor(Math.random() * (5 - (-1) + 1)) + (-1),
            radius: 50
        },
        {
            // x: 580,
            //y: 267,
            x: cord[126],
            y: cord[127],
            value: Math.floor(Math.random() * (5 - (-1) + 1)) + (-1),
            radius: 50
        },
        //washroom
        {
            // x: 707,
            // y: 283,
            x: cord[128],
            y: cord[129],
            value: Math.floor(Math.random() * (5 - (-1) + 1)) + (-1),
            radius: 40
        },
        //returns
        {
            // x: 40,
            // y: 236,
            x: cord[130],
            y: cord[131],
            value: Math.floor(Math.random() * (5 - (-1) + 1)) + (-1),
            radius: 20
        },
        //entry
        {
            //x: 40,
            //y: 181,
            x: cord[132],
            y: cord[133],
            value: Math.floor(Math.random() * (5 - (-1) + 1)) + (-1),
            radius: 20
        }
        ];

        data = {
            max: 5,
            data: points
        };
        // if you have a set of datapoints always use setData instead of addData
        // for data initialization
        heatmap.setData(data);
        timerLoop = setTimeout(loadheatmap, 3000);
    }
    loadheatmap();
};

function redirect(Url) {
    if (Url != null && Url != "") {
        window.location.href = Url;
    }
}

function LoadPowerBIReport(element) {
    var reporturl = $(element).attr("data-report-url");
    $("#jq_pbi_report_frame").attr("src", reporturl);
}
function roleChange() {
    $(".role-manager").click(function () {
        if ($('#dropdown1').hasClass("hide-role-card")) {
            $("#dropdown1").fadeIn(100, function () {

                $("#dropdown1").addClass("show-role-card");
                $('#dropdown1').removeClass('hide-role-card');
            });
        }
        else {
            $('#dropdown1').fadeOut(500, function () {
                $('#dropdown1').removeClass('show-role-card');
                $("#dropdown1").addClass("hide-role-card");
            });
        }
    });

    $(document).mouseup(function (e) {
        var container = $(".notificatoins-dropdown-container");

        if (!container.is(e.target) // if the target of the click isn't the container...
            && container.has(e.target).length === 0) // ... nor a descendant of the container
        {
            $('#dropdown1').removeClass('show-role-card');
            $("#dropdown1").addClass("hide-role-card");
        }
    });
    //$(document).bind('click', function (e) {
    //    if (!$(e.target).is('.notificatoins-dropdown-container')) {
    //        $('#dropdown1').removeClass('show-role-card');
    //        $("#dropdown1").addClass("hide-role-card");

    //    }
    //});

    $("#change-role").click(function () {
        $("#roles").fadeIn();
    })
    $("#roles").click(function () {
        $("#roles").fadeOut();
        swal("Not Available in Demo Version!");
    });
}

////function InitializeChart(element, data, type)
////{
////    google.charts.load('current', { 'packages': ['corechart'] });
////    google.charts.setOnLoadCallback(drawChartVisualization);
////}

////function GetComboChartOptions(xaxis, yaxis)
////{
////    var options = {
////        vAxis: {
////            /// title: yaxis //// 'Gross Sales'
////            title: 'Gross Sales'
////        },
////        hAxis: {
////            //// title: xaxis //// 'Week'
////            title: 'Week'
////        },
////        curveType: 'function',
////        seriesType: 'line',
////        animation: {
////            startup: true,
////            duration: 500,
////            easing: 'in'
////        },
////        series: {
////            0: {
////                type: 'bars',
////                color: '#33aeda'
////            },
////            1: {
////                color: '#FB8C00'
////            },
////            2: {
////                color: '#6E6E6E'
////            }
////        }
////    };

////    return options;
////}

////function drawChartVisualization()
////{
////    var data = google.visualization.arrayToDataTable([
////        ['XAXIS', 'YAXIS', 'Median', 'Max'],
////        ['12/31/2016', 16768, 13902.917, 22708.834],
////        ['12/30/2016', 22286, 5611.4144, 8153.3088],
////        ['12/29/2016', 17976, 13231.98, 19133],
////        ['12/28/2016', 8272, 11890.708, 16636.406],
////        ['12/27/2016', 21817, 11833.1639, 19484.9978],
////        ['12/26/2016', 15168, 8332.4412, 12273.4224],
////        ['12/25/2016', 13391, 9861.912, 15614.694],
////        ['12/24/2016', 16998, 16895.63475, 27722.7195],
////        ['12/23/2016', 22932, 10938.2328, 18028.7856],
////        ['12/22/2016', 11341, 8347.41495, 11806.7499],
////        ['12/21/2016', 12775, 6938.9292, 10805.5584],
////        ['12/20/2016', 15915, 7109.116, 9802.632],
////        ['12/19/2016', 15628, 7674.9008, 9960.8716],
////        ['12/18/2016', 8486, 4917.9438, 7161.8976],
////        ['12/17/2016', 13166, 7704.41315, 10573.9263],
////        ['12/16/2016', 17317, 7462.89395, 10174.2179],
////        ['12/15/2016', 11598, 16362.092, 25095.944],
////        ['12/14/2016', 12817, 12764.3906, 20243.6612],
////        ['12/13/2016', 21467, 15812.2095, 22941.009],
////        ['12/12/2016', 20559, 7288.326, 10761.102],
////        ['12/11/2016', 17399, 15383.676, 22358.952],
////        ['12/10/2016', 22311, 11322.3, 14454],
////        ['12/9/2016', 15880, 3782.856, 5744.112],
////        ['12/8/2016', 13066, 9977.2393, 13001.6986],
////        ['12/7/2016', 8638, 8346.5773, 12441.4346],
////        ['12/6/2016', 8388, 5982.1652, 8584.0304],
////        ['12/5/2016', 20845, 3885.2385, 5136.417],
////        ['12/4/2016', 15174, 8741.048, 13457.136],
////        ['12/3/2016', 20359, 8728.4925, 13752.585],
////        ['12/2/2016', 13274, 3772.7222, 5213.8944],
////        ['12/1/2016', 15586, 8633.5282, 13086.9264]
////    ]);

////    var options;
////    options = GetComboChartOptions(0, 0); //// data[0].XAXIS, data[0].YAXIS)
////    //// if (type = "combo") {
////    ////     options = GetComboChartOptions(0,0); //// data[0].XAXIS, data[0].YAXIS)
////    //// }
////    var objchart = $("#chart_div");
////    //// var datapoints = new google.visualization.DataTable(data)
////    var chart = new google.visualization.ComboChart(objchart);
////    chart.draw(data, options);
////}

////function InitializeChart(element) {
////    var kpiname = $(element).attr("data-kpi-name");
////    kpiname = kpiname.toString().toLowerCase();
////    if (kpiname.indexOf("gross") > -1) {
////        google.charts.setOnLoadCallback(drawChartVisual);
////    }
////}

////function drawChartVisual() {
////    // Some raw data (not necessarily accurate)
////    var element = $("#chart_div");
////    var datapoints;
////    $.ajax({
////        type: 'Get',
////        url: "/Home/GetTopKPIChart",
////        content: "application/json",
////        datatype: "JSON",
////        async: false,
////        success: function (result) {
////            InitChart(element, result.chartdata);
////        },
////        complete: function () {
////            HideMiniLoader();
////        }
////    });
////}

////function InitChart(element, datapoints)
////{
////    //var data = google.visualization.arrayToDataTable([
////    //    ['Date', 'Gross Sales', 'Median', 'Max'],
////    //    ['12/31/2016', 16768, 13902.917, 22708.834],
////    //    ['12/30/2016', 22286, 5611.4144, 8153.3088],
////    //    ['12/29/2016', 17976, 13231.98, 19133],
////    //    ['12/28/2016', 8272, 11890.708, 16636.406],
////    //    ['12/27/2016', 21817, 11833.1639, 19484.9978],
////    //    ['12/26/2016', 15168, 8332.4412, 12273.4224],
////    //    ['12/25/2016', 13391, 9861.912, 15614.694],
////    //    ['12/24/2016', 16998, 16895.63475, 27722.7195],
////    //    ['12/23/2016', 22932, 10938.2328, 18028.7856],
////    //    ['12/22/2016', 11341, 8347.41495, 11806.7499],
////    //    ['12/21/2016', 12775, 6938.9292, 10805.5584],
////    //    ['12/20/2016', 15915, 7109.116, 9802.632],
////    //    ['12/19/2016', 15628, 7674.9008, 9960.8716],
////    //    ['12/18/2016', 8486, 4917.9438, 7161.8976],
////    //    ['12/17/2016', 13166, 7704.41315, 10573.9263],
////    //    ['12/16/2016', 17317, 7462.89395, 10174.2179],
////    //    ['12/15/2016', 11598, 16362.092, 25095.944],
////    //    ['12/14/2016', 12817, 12764.3906, 20243.6612],
////    //    ['12/13/2016', 21467, 15812.2095, 22941.009],
////    //    ['12/12/2016', 20559, 7288.326, 10761.102],
////    //    ['12/11/2016', 17399, 15383.676, 22358.952],
////    //    ['12/10/2016', 22311, 11322.3, 14454],
////    //    ['12/9/2016', 15880, 3782.856, 5744.112],
////    //    ['12/8/2016', 13066, 9977.2393, 13001.6986],
////    //    ['12/7/2016', 8638, 8346.5773, 12441.4346],
////    //    ['12/6/2016', 8388, 5982.1652, 8584.0304],
////    //    ['12/5/2016', 20845, 3885.2385, 5136.417],
////    //    ['12/4/2016', 15174, 8741.048, 13457.136],
////    //    ['12/3/2016', 20359, 8728.4925, 13752.585],
////    //    ['12/2/2016', 13274, 3772.7222, 5213.8944],
////    //    ['12/1/2016', 15586, 8633.5282, 13086.9264]
////    //]);

////    //// var data = new google.visualization.DataTable();
////    var data = new google.visualization.DataTable();  
////    data.addColumn('string', datapoints[0].YAXIS);
////    data.addColumn('string', datapoints[0].KPI_ID);
////    data.addColumn('string', datapoints[0].XAXIS);
////    data.addColumn('number', datapoints[0].VALUE);
////    data.addColumn('number', datapoints[0].MEDIAN);
////    data.addColumn('number', datapoints[0].MAX);
////    data.addColumn('string', datapoints[0].IDENTIFIER);
////    for (var i = 1; i < datapoints.length; i++)
////    {
////        if (datapoints[i].IDENTIFIER == 'd') {
////            data.addRow([datapoints[i].YAXIS, datapoints[i].KPI_ID, datapoints[i].XAXIS, datapoints[i].VALUE, datapoints[i].MEDIAN, datapoints[i].MAX, datapoints[i].IDENTIFIER]);
////        }
////    }  

////    var options = {
////        vAxis: {
////            title: 'Gross Sales'
////        },
////        hAxis: {
////            title: 'Date'
////        },
////        curveType: 'function',
////        seriesType: 'line',
////        animation: {
////            startup: true,
////            duration: 500,
////            easing: 'in'
////        },
////        series: {
////            0: {
////                type: 'bars',
////                color: '#33aeda'
////            },
////            1: {
////                color: '#ff8a80'
////            },
////            2: {
////                color: '#33691e'
////            }
////        }
////    };

////    var chart = new google.visualization.ComboChart(document.getElementById('chart_div'));
////    chart.draw(data, options);
////}

function loadLeafNodeSuggestedActions(element) {
    var seckpiid = $(element).attr("data-seckpi-id");
    var secName = $(element).attr("data-item-name");
    var kraid = $(element).attr("data-kra-id");
    var modalid = $(element).attr("href");
    $(modalid).find("#jq-data-sa-name").html(secName);
    InitializeChart("childkpi_" + secName, 1);
}

function loadInsightChart(element) {
    var insightname = $(element).attr("data-ins-kpi-name");
    var prodname = $(element).attr("data-prd-name");
    var modalid = $(element).attr("href");
    $(modalid).find("#jq-data-sa-name").html(insightname + ": " + prodname);
    InitializeChart("jqinsight_" + insightname + prodname, 1);
}

$(document).on('keyup', function (evt) {
    if (evt.keyCode == 27) {
        $('#dropdown1').removeClass('show-role-card');
    }
});

function registerNotAvailable() {
    $(".notavailable").on("click", function () {
        swal("Not Available in Demo Version!");
        return false;
    });
    return false;
}

$(document).on('keyup', function (evt) {
    if (evt.keyCode == 27) {
        $('#dropdown1').removeClass('show-role-card');
    }
});

function esc() {
    $(document).on('keyup', function (evt) {
        if (evt.keyCode == 27) {
            $('#dropdown1').removeClass('show-role-card');
            $('#dropdown1').addClass('hide-role-card');
        }
    });
}

function BindAssignTask(element) {
    var taskname = $(element).parent().find("#jq-data-sa-name").html();
    $("#jq-taskname").val(taskname);
}

function BindTask(element) {

    var taskname = $(element).closest('table').parent().find('#jq_task_sa_name').html();
    var deptname = $(element).closest('tbody').prev('thead').find('> tr > th:eq(' + $(element).index() + ')').html();
    $("#jq-taskname").val(taskname);

    var deptList = $.map($('#jq-deptname option'), function (e) {
        return e.text;
    });
    var deptListsData = [];
    $.each(deptList, function (index, string) {
        deptListsData[index] = string.replace(/[^a-z0-9\s]/gi, '').replace(/[_\s]/g, ' ');
    });

    deptname = deptname.replace(/'/, '');

    //var depValue = $('#jq-deptname option:contains("' + deptname + '")').attr('selected', 'selected').val();
    var depValue = jQuery.inArray(deptname, deptListsData);

    if (depValue > 0) {
        $("#jq-deptname").val(depValue);

        if ($('#jq-deptname').data('options') == undefined) {
            /*Taking an array of all options-2 and kind of embedding it on the select1*/
            $('#jq-deptname').data('options', $('#jq-Empname option').clone());
        }
        var opt = $('#jq-deptname').data('options').filter('[value=' + depValue + ']');
        console.log(opt);
        $('#jq-Empname').material_select("destroy");
        $('#jq-Empname').html(opt);
        $('#jq-Empname').material_select();
        $('#jq-deptname').material_select();
    }
}