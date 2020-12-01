function InitializeChart(name, type) {
    //if (name.toString().toLowerCase().indexOf("gross") > -1) {
    //    if (type == 1) {
    //        google.charts.setOnLoadCallback(drawGSChartByDate);
    //    }
    //    else if (type == 2) {
    //        google.charts.setOnLoadCallback(drawGSChartByWeek);
    //    }
    //}
    //if (name.toString().toLowerCase().indexOf("conversion") > -1) {
    //    if (type == 1) {
    //        google.charts.setOnLoadCallback(drawConvChartByDate);
    //    }
    //    else if (type == 2) {
    //        google.charts.setOnLoadCallback(drawConvChartByWeek);
    //    }
    //}

    if (name.toString().toLowerCase().indexOf("basket") > -1) {
        if (type == 1) {
            google.charts.setOnLoadCallback(drawBasketChartByDate);
        }
        else if (type == 2) {
            google.charts.setOnLoadCallback(drawBasketChartByWeek);
        }
    }

    if (name.toString().toLowerCase().indexOf("social") > -1) {
        if (type == 1) {
            google.charts.setOnLoadCallback(drawSocialChartByDate);
        }
        else if (type == 2) {
            google.charts.setOnLoadCallback(drawSocialChartByWeek);
        }
    }

    if (name.toString().toLowerCase().indexOf("price") > -1) {
        if (type == 1) {
            google.charts.setOnLoadCallback(drawPriceChartByDate);
        }
        else if (type == 2) {
            google.charts.setOnLoadCallback(drawPriceChartByWeek);
        }
    }

    if (name.toString().toLowerCase().indexOf("childkpi") > -1) {
        google.charts.setOnLoadCallback(drawChildKPIChart(name));
    }

    if (name.toString().toLowerCase().indexOf("jqinsight_") > -1) {
        google.charts.setOnLoadCallback(drawInsightChart(name));
    }

    //// else if (name.toString().toLowerCase().indexOf("")) {
    ////     google.charts.setOnLoadCallback(drawChildKPIChartByWeek);
    //// }
    //// else if (name.toString().toLowerCase().indexOf("")) {
    ////     google.charts.setOnLoadCallback(drawChildKPIChartByWeek);
    //// }
    //// else if (name.toString().toLowerCase().indexOf("")) {
    ////     google.charts.setOnLoadCallback(drawChildKPIChartByWeek);
    //// }
    //// else if (name.toString().toLowerCase().indexOf("")) {
    ////     google.charts.setOnLoadCallback(drawChildKPIChartByWeek);
    //// }        
}

//function drawGSChartByDate() {
//    // Some raw data (not necessarily accurate)
//    var data = google.visualization.arrayToDataTable([
//        ['Date', 'Actuals', '	Max', '	Median', 'Predicted', { role: 'annotation' }],
//        ['1-Dec', 55305, 58101, 32302, null, null],
//        ['2-Dec', 57622, 60480, 30845, null, null],
//        ['3-Dec', 53809, 67214, 39656, null, null],
//        ['4-Dec', 49822, 59165, 31949, null, null],
//        ['5-Dec', 58587, 71572, 50100, null, null],
//        ['6-Dec', 55001, 63901, 35146, null, null],
//        ['7-Dec', 57805, 71688, 51615, null, null],
//        ['8-Dec', 59401, 75090, 49559, null, null],
//        ['9-Dec', 58128, 91842, 67963, null, null],
//        ['10-Dec', 66238, 86109, 57693, null, null],
//        ['11-Dec', 58240, 75679, 56002, null, null],
//        ['12-Dec', 65840, 80983, 43731, null, null],
//        ['13-Dec', 58283, 71105, 46218, null, null],
//        ['14-Dec', 55192, 88307, 46803, null, null],
//        ['15-Dec', 63978, 97886, 67542, null, null],
//        ['16-Dec', 57422, 71203, 35602, null, null],
//        ['17-Dec', 56404, 67121, 42286, null, null],
//        ['18-Dec', 55605, 82295, 56784, null, null],
//        ['19-Dec', 53570, 71784, 51684, null, null],
//        ['20-Dec', 46971, 69987, 39892, null, null],
//        ['21-Dec', 44473, 67599, 46643, 44473, 'Today'],
//        ['22-Dec', null, null, null, 52924, null],
//        ['23-Dec', null, null, null, 49970, null],
//        ['24-Dec', null, null, null, 53748, null],
//        ['25-Dec', null, null, null, 53852, null],
//        ['26-Dec', null, null, null, 49807, null],
//        ['27-Dec', null, null, null, 46370, null],
//        ['28-Dec', null, null, null, 47013, null],
//        ['29-Dec', null, null, null, 49993, null],
//        ['30-Dec', null, null, null, 50557, null]
//    ]);

//    var options = {
//        vAxis: { title: 'Gross Sales', gridlines: { color: 'transparent' } },
//        hAxis: { title: 'Date', gridlines: { count: -1, color: 'transparent' }, textStyle: { fontSize: 12 } },
//        seriesType: 'steppedArea',
//        isStacked: false,
//        connectSteps: false,
//        series: {
//            0: { type: 'line', pointSize: 4, pointShape: 'circle', lineWidth: 3, color: '#66bb6a' },
//            1: { color: '#cfd8dc', areaOpacity: 0.8 },
//            2: { type: 'steppedArea', connectSteps: true, color: '#80d8ff', areaOpacity: 0.6 },
//            3: { type: 'line', lineDashStyle: [14, 2, 7, 2], pointSize: 4, pointShape: 'circle', lineWidth: 3, color: '#66bb6a' },
//        },
//        legend: { textStyle: { color: 'black', fontSize: 12 } },
//        curveType: 'function',
//        animation: {
//            "startup": true,
//            duration: 1000,
//            easing: 'out'
//        },
//        annotations: {
//            textStyle: {
//                fontSize: 14,
//                color: '#1b3245',
//                opacity: 0.8,
//            }
//        }
//    };

//    var chart = new google.visualization.ComboChart(document.getElementById('chart_div'));
//    chart.draw(data, options);
//}

//function drawGSChartByWeek() {
//    //// [WeekNum,Gross Sales,GrossSales_Median,GrossSales_Max],
//    var data = google.visualization.arrayToDataTable([
//        ['Week Num', 'Actuals', 'Max', 'Median', 'Predicted', { role: 'annotation' }],
//        ['47', 154120, 169532, 96633, null, null],
//        ['48', 165392, 177551, 118959, null, null],
//        ['49', 177379, 211499, 137474, null, null],
//        ['50', 162102, 192326, 126935, null, null],
//        ['51', 155323, 194154, 139791, null, null],
//        ['52', 145146, 175052, 103281, 145146, 'This week'],
//        ['53', null, null, null, 155052, null]
//    ]);

//    var options = {
//        //title: 'Gross Sales',
//        vAxis: { title: 'Gross Sales', gridlines: { color: 'transparent' } },
//        hAxis: { title: 'Week', gridlines: { count: -1, color: 'transparent' }, textStyle: { fontSize: 12 } },
//        seriesType: 'steppedArea',
//        isStacked: false,
//        connectSteps: false,
//        series: {
//            0: { type: 'line', pointSize: 4, pointShape: 'circle', lineWidth: 3, color: '#66bb6a' },
//            1: { color: '#cfd8dc', areaOpacity: 0.8 },
//            2: { type: 'steppedArea', connectSteps: true, color: '#80d8ff', areaOpacity: 0.6 },
//            3: { type: 'line', lineDashStyle: [14, 2, 7, 2], pointSize: 4, pointShape: 'circle', lineWidth: 3, color: '#66bb6a' },
//        },
//        curveType: 'function',
//        animation: {
//            "startup": true,
//            duration: 1000,
//            easing: 'out'
//        },
//        annotations: {
//            textStyle: {
//                // fontName: 'Times-Roman',
//                fontSize: 14,
//                //bold: true,
//                //italic: true,
//                color: '#1b3245',     // The color of the text.
//                //auraColor: '#d799ae', // The color of the text outline.
//                opacity: 0.8,
//            }     // The transparency of the text.
//        }
//    };

//    var chart = new google.visualization.ComboChart(document.getElementById('chart_div'));
//    chart.draw(data, options);
//}

function drawConvChartByDate() {
    var data = google.visualization.arrayToDataTable([
          //Yearly Sales Data 
	   ['Date', 'Actuals', 'Max', 'Median', 'Predicted', { role: 'annotation' }],
           ['1-Dec', 52, 58, 51, null, null],
           ['2-Dec', 53, 62, 52, null, null],
           ['3-Dec', 51, 53, 49, null, null],
           ['4-Dec', 59, 66, 57, null, null],
           ['5-Dec', 58, 59, 51, null, null],
           ['6-Dec', 57, 59, 49, null, null],
           ['7-Dec', 56, 59, 48, null, null],
           ['8-Dec', 59, 66, 48, null, null],
           ['9-Dec', 57, 63, 50, null, null],
           ['10-Dec', 56, 58, 52, null, null],
           ['11-Dec', 55, 59, 53, null, null],
           ['12-Dec', 51, 56, 54, null, null],
           ['13-Dec', 54, 59, 51, null, null],
           ['14-Dec', 53, 59, 49, null, null],
           ['15-Dec', 54, 55, 50, null, null],
           ['16-Dec', 56, 60, 53, null, null],
           ['17-Dec', 57, 63, 53, null, null],
           ['18-Dec', 54, 64, 57, null, null],
           ['19-Dec', 53, 66, 59, null, null],
           ['20-Dec', 52, 65, 58, null, null],
           ['21-Dec', 49, 57, 52, 49, 'Today'],
           ['22-Dec', null, null, null, 49, null],
           ['23-Dec', null, null, null, 50, null],
           ['24-Dec', null, null, null, 48, null]
    ]);

    var options = {
        vAxis: { title: 'Conversion', gridlines: { color: 'transparent' } },
        hAxis: { gridlines: { count: -1, color: 'transparent' }, textStyle: { fontSize: 12 }, showTextEvery: 2 },
        seriesType: 'line',
        isStacked: false,
        connectSteps: false,
        legend: { position: 'top-right', textStyle: { fontName: 'Arial', color: 'black', fontSize: 12 } },
        series: {
            0: { type: 'line', pointSize: 4, pointShape: 'circle', lineWidth: 3, color: '#66bb6a' },
            1: { type: 'steppedArea', connectSteps: true, color: '#cfd8dc', areaOpacity: 0.8 },
            2: { type: 'steppedArea', connectSteps: true, color: '#80d8ff', areaOpacity: 0.6 },
            3: { type: 'line', lineDashStyle: [14, 2, 7, 2], pointSize: 4, pointShape: 'circle', lineWidth: 3, color: '#66bb6a' },
        },
        curveType: 'function',
        animation: {
            "startup": true,
            duration: 1000,
            easing: 'out'
        },
        annotations: {
            textStyle: {
                // fontName: 'Times-Roman',
                fontSize: 14,
                //bold: true,
                //italic: true,
                color: '#1b3245',     // The color of the text.
                //auraColor: '#d799ae', // The color of the text outline.
                opacity: 0.8,
            }     // The transparency of the text.
        },
        chartArea: { left: 50, top: 15, bottom: 30, width: '80%', height: '80%' }
    };

    var chart = new google.visualization.LineChart(document.getElementById('ConversionChart'));
    chart.draw(data, options);
}

function drawConvChartByWeek() {
    var data = google.visualization.arrayToDataTable([
        ['Week Number', 'Conversion', 'Max', 'Median', 'Predicted', { role: 'annotation' }],
        ['47', 56, 59, 54, null, null],
        ['48', 55, 58, 53, null, null],
        ['49', 58, 62, 56, null, null],
        ['50', 56, 59, 54, null, null],
        ['51', 52, 59, 50, null, null],
        ['52', 49, 52, 47, 49, "This Week"],
        ['53', null, null, null, 48, null]
    ]);

    var options = {
        vAxis: { title: 'Conversion', gridlines: { color: 'transparent' } },
        hAxis: { gridlines: { count: -1, color: 'transparent' }, textStyle: { fontSize: 12 }, showTextEvery: 2 },
        seriesType: 'line',
        isStacked: false,
        connectSteps: false,
        legend: { position: 'top-right', textStyle: { fontName: 'Arial', color: 'black', fontSize: 12 } },
        series: {
            0: { type: 'line', pointSize: 4, pointShape: 'circle', lineWidth: 3, color: '#66bb6a' },
            1: { type: 'steppedArea', connectSteps: true, color: '#cfd8dc', areaOpacity: 0.8 },
            2: { type: 'steppedArea', connectSteps: true, color: '#80d8ff', areaOpacity: 0.6 },
            3: { type: 'line', lineDashStyle: [14, 2, 7, 2], pointSize: 4, pointShape: 'circle', lineWidth: 3, color: '#66bb6a' },
        },
        curveType: 'function',
        animation: {
            "startup": true,
            duration: 1000,
            easing: 'out'
        },
        annotations: {
            textStyle: {
                fontSize: 14,
                color: '#1b3245',     // The color of the text.
                opacity: 0.8,
            }     // The transparency of the text.
        },
        chartArea: { left: 50, top: 15, bottom: 30, width: '80%', height: '80%' }
    };

    var chart = new google.visualization.LineChart(document.getElementById('ConversionChart'));
    chart.draw(data, options);
}

function drawChildKPIChart(name) {
    var data;
    var options;

    if (name.toString().toLowerCase().indexOf("associate") > -1) {
        data = google.visualization.arrayToDataTable([
            ["Department", "This Week", "5 Week Avg"],
            ["Men’s Apparel", 27, 30],
            ["Women’s Apparel", 25, 29],
            ["Kids' Apparel", 24, 28],
            ["Men’s Footwear", 27, 29],
            ["Women’s Footwear", 27, 30],
            ["Kid's Footwear", 24, 29],
            ["Home Furnishing", 30, 28],
            ["Accessories", 26, 29]
        ]);
    }

    if (name.toString().toLowerCase().indexOf("trial") > -1) {
        data = google.visualization.arrayToDataTable([
            ['Trial Room', 'This Week', '5 Week Avg'],
            ['M-TR-1', 97, 82],
            ['M-TR-5', 98, 84],
            ['W-TR-2', 67, 85],
            ['W-TR-3', 96, 86],
            ['W-TR-4', 47, 85],
            ['M-TR-6', 97, 92],
            ['W-TR-1', 95, 78],
            ['M-TR-3', 98, 80],
            ['M-TR-8', 96, 83],
            ['W-TR-7', 98, 80]
        ]);
    }

    if (name.toString().toLowerCase().indexOf("checkout") > -1) {
        data = google.visualization.arrayToDataTable([
            ['POS Num', 'This Week', '5 Week Avg'],
            ['POS-1', 8, 9],
            ['POS-2', 6, 9],
            ['POS-3', 7, 7],
            ['POS-4', 12, 7],
            ['POS-5', 5, 7],
            ['POS-6', 12, 6],
            ['POS-7', 8, 9],
            ['POS-8', 6, 7]
        ]);
    }

    if (name.toString().toLowerCase().indexOf("pricing") > -1) {
        data = google.visualization.arrayToDataTable([
            ["Department", 'This Week', '5 Week Avg'],
            ["Men’s Apparel", 82, 78],
            ["Women’s Apparel", 94, 89],
            ["Kids' Apparel", 78, 82],
            ["Men’s Footwear", 90, 86],
            ["Women’s Footwear", 85, 83],
            ["Kids' Footwear", 82, 78],
            ["Home Furnishing", 92, 87],
            ["Accessories", 84, 81]
        ]);
    }

    options = {
        vAxis: {
            //// title: 'Associate Floor Hours',
            gridlines: {
                count: -1,
                color: 'transparent',
            }, minValue: 0
        },
        legend: { position: "top" },
        hAxis: {
            //// title: 'Departments',
            gridlines: { count: -1, color: 'transparent' }
        },
        seriesType: 'bars',
        series: {
            0: { color: '#4fc3f7' },
            1: { type: 'steppedArea', color: '#cfd8dc', areaOpacity: 0.5, connectSteps: false },
        },
        animation: {
            "startup": true,
            duration: 1000,
            easing: 'out'
        },
    };
    /*if (name.toString().toLowerCase().indexOf("trial") > -1) {
    }

    if (name.toString().toLowerCase().indexOf("trial") > -1) {
    }

    if (name.toString().toLowerCase().indexOf("trial") > -1) {
    }*/

    var chart = new google.visualization.ComboChart(document.getElementById('leafchart'));
    chart.draw(data, options);
}

function drawInsightChart(name) {
    //var data = google.visualization.arrayToDataTable([
    //    ['Hours', 'HistoricalSalesAvg', 'CurrentDaySalesActuals', { role: 'annotation' }, { role: 'annotation' }, 'CurrentDaySalesPredicted', { role: 'annotation' }],
    //    ['	1	', 0.963424545, 1.445136817, null, null, 1.445136817, null],
    //    ['	2	', 1.279032126, 1.918548188, null, null, 1.918548188, null],
    //    ['	3	', 2.461690634, 3.692535951, null, null, 3.692535951, null],
    //    ['	4	', 2.905925734, 4.358888601, '13 Units Left', null, 4.358888601, null],
    //    ['	5	', 3.936025081, 5.904037622, null, null, 5.904037622, null],
    //    ['	6	', 5.272584399, 7.908876599, null, null, 7.908876599, null],
    //    ['	7	', 7.965954649, 11.94893197, null, null, 11.94893197, null],
    //    ['	8	', 8.716669333, 13.075004, '3 Units Left', null, 13.075004, null],
    //    ['	9	', 9.875834015, 14.81375102, null, null, 14.81375102, null],
    //    ['	10	', 11.44653549, 15.16980324, null, null, 15.16980324, null],
    //    ['	11	', 12.15110229, 16.22665344, null, null, 16.22665344, null],
    //    ['	12	', 13.03260721, 16.54891081, null, null, 16.54891081, null],
    //    ['	13	', 13.40423733, 17.106356, null, null, 17.106356, null],
    //    ['	14	', 14.04218933, 17.863284, 'Current', '', 17.863284, null],
    //    ['	15	', 14.49565067, , null, null, 17.99995, null],
    //    ['	16	', 14.51802133, , null, null, 18, null],
    //    ['	17	', 14.14009992, , null, null, 19.55, null],
    //    ['	18	', 13.33073333, , null, null, 19.9961, null],
    //    ['	19	', 12.975712, , null, null, 19.463568, null],
    //    ['	20	', 12.4332, , null, null, 20, 'OutOfStock'],
    //    ['	21	', 11.25, , null, null, , null],
    //    ['	22	', 10.22222133, , null, null, , null],
    //    ['	23	', 9.216493333, , null, null, , null],
    //    ['	24	', 7.455634667, , null, null, , null]
    //]);

    //var options = {
    //    title: 'Rate of Sales in a Month',
    //    width: 1100,
    //    height: 400,
    //    annotations: {
    //        0: {
    //            textStyle: {
    //                fontName: 'Roboto',
    //                fontSize: 18,
    //                bold: true,
    //                italic: true,
    //                color: '#212121',     // The color of the text.
    //                //auraColor: '#d799ae', // The color of the text outline.
    //                opacity: 0.8,
    //            },      // The transparency of the text.
    //            4: { style: 'line' }
    //        }
    //    },
    //    vAxes: {
    //        0: {
    //            gridlines: { color: 'transparent' },

    //            title: 'Rate of Sales',
    //        }
    //    },
    //    hAxis: {
    //        title: 'Hours',
    //        gridlines: {
    //            count: -1,
    //            color: 'transparent',
    //        }
    //    },
    //    series: {
    //        0: { lineWidth: 4, color: '#29b6f6' },//HistoricalSales
    //        1: { lineWidth: 4, color: '#66bb6a' },//currentdaySalesActuals
    //        2: { lineDashStyle: [14, 2, 7, 2], color: '#66bb6a' },//currentdaySalesPredicted
    //    },
    //    curveType: 'function',
    //    animation: {
    //        "startup": true,
    //        duration: 1000,
    //        easing: 'out'
    //    },

    //}
    var data;
    var options

    if (name.toString().toLowerCase().indexOf("out of stock") > -1 && name.toString().toLowerCase().indexOf("lcs765") > -1) {
        data = google.visualization.arrayToDataTable([
        ['Hours', 'Avg. Rate of Sale', "Today's Rate of Sale", { role: 'annotation' }, { role: 'annotation' }, 'Predicted Rate of Sale', { role: 'annotation' }],
['9', 3.1, 5, 'Opening Stock:51', null, 5, null],
['10', 4.3, 5, null, null, 5, null],
['11', 4.4, 5, null, null, 5, null],
['12', 4.45, 6, null, null, 6, null],
['13', 4.5, 6, null, null, 6, null],
['14', 5.6, 6, 'Current Stock:24', '', 6, null],
['15', 5.6, , null, null, 6, null],
['16', 5.7, , null, null, 6, null],
['17', 5.8, , null, null, 7, null],
['18', 4.9, , null, null, 7, 'OutOfStock'],
['19', 4.5, , null, null, 7, null],
['20', 4.6, , null, null, 6, null],
['21', 4.7, , null, null, 5, null],
['22', 3.8, , null, null, 5, null]
        ]);

        options = {
            // title: 'Rate of Sales in a Month',
            width: 1100,
            height: 400,
            annotations: {
                textStyle: {
                    //  fontName: 'Tahoma',
                    fontSize: 14,
                    bold: false,
                    italic: false,
                    color: '#213c54',     // The color of the text.
                    //auraColor: '#d799ae', // The color of the text outline.
                    opacity: 0.8         // The transparency of the text.

                }
            },
            vAxes: {
                0: {
                    gridlines: { color: 'transparent' },
                    format: "#",
                    title: 'Units Sold',
                }
            },
            hAxis: {
                title: 'Hours',
                gridlines: {
                    count: -1,
                    color: 'transparent',
                }
            },
            legend: { position: 'top' },
            series: {
                0: { type: 'area', lineWidth: 2, color: '#66bb6a', areaOpacity: 0.2, curveType: 'function' },//HistoricalSales
                1: { pointSize: 8, pointShape: 'circle', lineWidth: 3, color: '#03a9f4' },//currentdaySalesActuals
                2: { lineDashStyle: [14, 2, 7, 2], color: '#03a9f4' },//currentdaySalesPredicted 
            },

            animation: {
                "startup": true,
                duration: 1000,
                easing: 'out'
            },

        }
    }

    if (name.toString().toLowerCase().indexOf("out of stock") > -1 && name.toString().toLowerCase().indexOf("844929-401") > -1) {
        data = google.visualization.arrayToDataTable([
        ['Hours', 'Avg. Rate of Sale', "Today's Rate of Sale", { role: 'annotation' }, { role: 'annotation' }, 'Predicted Rate of Sale', { role: 'annotation' }],
['9', 3.1, 5, 'Opening Stock:51', null, 5, null],
['10', 4.3, 5, null, null, 5, null],
['11', 4.4, 5, null, null, 5, null],
['12', 4.45, 6, null, null, 6, null],
['13', 4.5, 6, null, null, 6, null],
['14', 5.6, 6, 'Current Stock:24', '', 6, null],
['15', 5.6, , null, null, 6, null],
['16', 5.7, , null, null, 6, null],
['17', 5.8, , null, null, 7, null],
['18', 4.9, , null, null, 7, 'OutOfStock'],
['19', 4.5, , null, null, 7, null],
['20', 4.6, , null, null, 6, null],
['21', 4.7, , null, null, 5, null],
['22', 3.8, , null, null, 5, null]
        ]);

        options = {
            // title: 'Rate of Sales in a Month',
            width: 1100,
            height: 400,
            annotations: {
                textStyle: {
                    //  fontName: 'Tahoma',
                    fontSize: 14,
                    bold: false,
                    italic: false,
                    color: '#213c54',     // The color of the text.
                    //auraColor: '#d799ae', // The color of the text outline.
                    opacity: 0.8         // The transparency of the text.

                }
            },
            vAxes: {
                0: {
                    gridlines: { color: 'transparent' },
                    format: "#",
                    title: 'Units Sold',
                }
            },
            hAxis: {
                title: 'Hours',
                gridlines: {
                    count: -1,
                    color: 'transparent',
                }
            },
            legend: { position: 'top' },
            series: {
                0: { type: 'area', lineWidth: 2, color: '#66bb6a', areaOpacity: 0.2, curveType: 'function' },//HistoricalSales
                1: { pointSize: 8, pointShape: 'circle', lineWidth: 3, color: '#03a9f4' },//currentdaySalesActuals
                2: { lineDashStyle: [14, 2, 7, 2], color: '#03a9f4' },//currentdaySalesPredicted 
            },

            animation: {
                "startup": true,
                duration: 1000,
                easing: 'out'
            },

        }
    }

    if (name.toString().toLowerCase().indexOf("out of stock") > -1 && name.toString().toLowerCase().indexOf("hm-cb65") > -1) {
        data = google.visualization.arrayToDataTable([
['Hours', 'Avg. Rate of Sale', "Today's Rate of Sale", { role: 'annotation' }, { role: 'annotation' }, 'Predicted Rate of Sale', { role: 'annotation' }],
['9', 2.1, 3, 'Opening Stock:58', null, 3, null],
['10', 3.6, 5, null, null, 5, null],
['11', 4.6, 5, null, null, 5, null],
['12', 4.7, 6, null, null, 6, null],
['13', 4.8, 7, null, null, 7, null],
['14', 5.2, 7, 'Current Stock:25', '', 7, null],
['15', 5.4, , null, null, 8, null],
['16', 6.7, , null, null, 8, null],
['17', 7.4, , null, null, 9, 'OutOfStock'],
['18', 8.1, , null, null, 7, null],
['19', 6.5, , null, null, 6, null],
['20', 4.3, , null, null, 5, null],
['21', 3.2, , null, null, 4, null],
['22', 2.1, , null, null, 3, null]
        ]);

        options = {
            // title: 'Rate of Sales in a Month',
            width: 1100,
            height: 400,
            annotations: {
                textStyle: {
                    //  fontName: 'Tahoma',
                    fontSize: 14,
                    bold: false,
                    italic: false,
                    color: '#213c54',     // The color of the text.
                    //auraColor: '#d799ae', // The color of the text outline.
                    opacity: 0.8         // The transparency of the text.

                }
            },
            vAxes: {
                0: {
                    gridlines: { color: 'transparent' },
                    format: "#",
                    title: 'Units Sold',
                }
            },
            hAxis: {
                title: 'Hours',
                gridlines: {
                    count: -1,
                    color: 'transparent',
                }
            },
            legend: { position: 'top' },
            series: {
                0: { type: 'area', lineWidth: 2, color: '#66bb6a', areaOpacity: 0.2, curveType: 'function' },//HistoricalSales
                1: { pointSize: 8, pointShape: 'circle', lineWidth: 3, color: '#03a9f4' },//currentdaySalesActuals
                2: { lineDashStyle: [14, 2, 7, 2], color: '#03a9f4' },//currentdaySalesPredicted 
            },

            animation: {
                "startup": true,
                duration: 1000,
                easing: 'out'
            },

        }
    }

    if (name.toString().toLowerCase().indexOf("out of stock") > -1 && name.toString().toLowerCase().indexOf("819120-010") > -1) {
        data = google.visualization.arrayToDataTable([
['Hours', 'Avg. Rate of Sale', "Today's Rate of Sale", { role: 'annotation' }, { role: 'annotation' }, 'Predicted Rate of Sale', { role: 'annotation' }],
['9', 2.1, 3, 'Opening Stock:58', null, 3, null],
['10', 3.6, 5, null, null, 5, null],
['11', 4.6, 5, null, null, 5, null],
['12', 4.7, 6, null, null, 6, null],
['13', 4.8, 7, null, null, 7, null],
['14', 5.2, 7, 'Current Stock:25', '', 7, null],
['15', 5.4, , null, null, 8, null],
['16', 6.7, , null, null, 8, null],
['17', 7.4, , null, null, 9, 'OutOfStock'],
['18', 8.1, , null, null, 7, null],
['19', 6.5, , null, null, 6, null],
['20', 4.3, , null, null, 5, null],
['21', 3.2, , null, null, 4, null],
['22', 2.1, , null, null, 3, null]
        ]);

        options = {
            // title: 'Rate of Sales in a Month',
            width: 1100,
            height: 400,
            annotations: {
                textStyle: {
                    //  fontName: 'Tahoma',
                    fontSize: 14,
                    bold: false,
                    italic: false,
                    color: '#213c54',     // The color of the text.
                    //auraColor: '#d799ae', // The color of the text outline.
                    opacity: 0.8         // The transparency of the text.

                }
            },
            vAxes: {
                0: {
                    gridlines: { color: 'transparent' },
                    format: "#",
                    title: 'Units Sold',
                }
            },
            hAxis: {
                title: 'Hours',
                gridlines: {
                    count: -1,
                    color: 'transparent',
                }
            },
            legend: { position: 'top' },
            series: {
                0: { type: 'area', lineWidth: 2, color: '#66bb6a', areaOpacity: 0.2, curveType: 'function' },//HistoricalSales
                1: { pointSize: 8, pointShape: 'circle', lineWidth: 3, color: '#03a9f4' },//currentdaySalesActuals
                2: { lineDashStyle: [14, 2, 7, 2], color: '#03a9f4' },//currentdaySalesPredicted 
            },

            animation: {
                "startup": true,
                duration: 1000,
                easing: 'out'
            },

        }
    }

    if (name.toString().toLowerCase().indexOf("out of stock") > -1 && name.toString().toLowerCase().indexOf("hm-wn87") > -1) {
        data = google.visualization.arrayToDataTable([
['Hours', 'Avg. Rate of Sale', "Today's Rate of Sale", { role: 'annotation' }, { role: 'annotation' }, 'Predicted Rate of Sale', { role: 'annotation' }],
['9', 0.5, 1, 'Opening Stock:52', null, 1, null],
['10', 1.1, 2, null, null, 2, null],
['11', 2.4, 2, null, null, 2, null],
['12', 2.6, 3, null, null, 3, null],
['13', 2.8, 4, null, null, 4, null],
['14', 3, 3, 'Current Stock:37', '', 3, null],
['15', 3.5, , null, null, 3, null],
['16', 3.5, , null, null, 3, null],
['17', 4, , null, null, 4, null],
['18', 4, , null, null, 4, null],
['19', 3.7, , null, null, 3, null],
['20', 2.8, , null, null, 3, null],
['21', 2, , null, null, 2, 'OutOfStock'],
['22', 1, , null, null, 1, null]
        ]);

        options = {
            // title: 'Rate of Sales in a Month',
            width: 1100,
            height: 400,
            annotations: {
                textStyle: {
                    //  fontName: 'Tahoma',
                    fontSize: 14,
                    bold: false,
                    italic: false,
                    color: '#213c54',     // The color of the text.
                    //auraColor: '#d799ae', // The color of the text outline.
                    opacity: 0.8         // The transparency of the text.

                }
            },
            vAxes: {
                0: {
                    gridlines: { color: 'transparent' },
                    format: "#",
                    title: 'Units Sold',
                }
            },
            hAxis: {
                title: 'Hours',
                gridlines: {
                    count: -1,
                    color: 'transparent',
                }
            },
            legend: { position: 'top' },
            series: {
                0: { type: 'area', lineWidth: 2, color: '#66bb6a', areaOpacity: 0.2, curveType: 'function' },//HistoricalSales
                1: { pointSize: 8, pointShape: 'circle', lineWidth: 3, color: '#03a9f4' },//currentdaySalesActuals
                2: { lineDashStyle: [14, 2, 7, 2], color: '#03a9f4' },//currentdaySalesPredicted 
            },

            animation: {
                "startup": true,
                duration: 1000,
                easing: 'out'
            },

        }
    }

    if (name.toString().toLowerCase().indexOf("out of stock") > -1 && name.toString().toLowerCase().indexOf("868981-903") > -1) {
        data = google.visualization.arrayToDataTable([
['Hours', 'Avg. Rate of Sale', "Today's Rate of Sale", { role: 'annotation' }, { role: 'annotation' }, 'Predicted Rate of Sale', { role: 'annotation' }],
['9', 0.5, 1, 'Opening Stock:52', null, 1, null],
['10', 1.1, 2, null, null, 2, null],
['11', 2.4, 2, null, null, 2, null],
['12', 2.6, 3, null, null, 3, null],
['13', 2.8, 4, null, null, 4, null],
['14', 3, 3, 'Current Stock:37', '', 3, null],
['15', 3.5, , null, null, 3, null],
['16', 3.5, , null, null, 3, null],
['17', 4, , null, null, 4, null],
['18', 4, , null, null, 4, null],
['19', 3.7, , null, null, 3, null],
['20', 2.8, , null, null, 3, null],
['21', 2, , null, null, 2, 'OutOfStock'],
['22', 1, , null, null, 1, null]
        ]);

        options = {
            // title: 'Rate of Sales in a Month',
            width: 1100,
            height: 400,
            annotations: {
                textStyle: {
                    //  fontName: 'Tahoma',
                    fontSize: 14,
                    bold: false,
                    italic: false,
                    color: '#213c54',     // The color of the text.
                    //auraColor: '#d799ae', // The color of the text outline.
                    opacity: 0.8         // The transparency of the text.

                }
            },
            vAxes: {
                0: {
                    gridlines: { color: 'transparent' },
                    format: "#",
                    title: 'Units Sold',
                }
            },
            hAxis: {
                title: 'Hours',
                gridlines: {
                    count: -1,
                    color: 'transparent',
                }
            },
            legend: { position: 'top' },
            series: {
                0: { type: 'area', lineWidth: 2, color: '#66bb6a', areaOpacity: 0.2, curveType: 'function' },//HistoricalSales
                1: { pointSize: 8, pointShape: 'circle', lineWidth: 3, color: '#03a9f4' },//currentdaySalesActuals
                2: { lineDashStyle: [14, 2, 7, 2], color: '#03a9f4' },//currentdaySalesPredicted 
            },

            animation: {
                "startup": true,
                duration: 1000,
                easing: 'out'
            },

        }
    }

    if (name.toString().toLowerCase().indexOf("stock aging") > -1 && name.toString().toLowerCase().indexOf("kt-gl87") > -1) {
        data = google.visualization.arrayToDataTable([
['Date', 'Rate of Sale', 'Stock On Hand', { role: 'annotation' }, 'Predicted Stock On Hand', { role: 'annotation' }, 'Predicted Rate of Sale'],

['1-Dec', 5, 254, "Stock:254", null, null, null],
['2-Dec', 5, 249, null, null, null, null],
['3-Dec', 6, 243, null, null, null, null],
['4-Dec', 7, 237, null, null, null, null],
['5-Dec', 7, 230, null, null, null, null],
['6-Dec', 7, 225, null, null, null, null],
['7-Dec', 7, 218, null, null, null, null],
['8-Dec', 6, 211, null, null, null, null],
['9-Dec', 6, 203, null, null, null, null],
['10-Dec', 5, 197, "Stock:197", null, null, null],
['11-Dec', 5, 190, null, null, null, null],
['12-Dec', 5, 183, null, null, null, null],
['13-Dec', 5, 178, null, null, null, null],
['14-Dec', 7, 170, null, null, null, null],
['15-Dec', 7, 163, null, null, null, null],
['16-Dec', 6, 155, null, null, null, null],
['17-Dec', 6, 149, "Stock:149", null, null, null],
['18-Dec', 6, 143, null, null, null, null],
['19-Dec', 8, 137, null, null, null, 8],
['20-Dec', 8, 130, null, null, null, 8],
['21-Dec', 8, 122, "Today", 122, null, 8],
['22-Dec', null, null, null, 116, null, 8],
['23-Dec', null, null, null, 108, null, 6],
['24-Dec', null, null, null, 102, null, 6],
['25-Dec', null, null, null, 97, null, 7],
['26-Dec', null, null, null, 90, null, 8],
['27-Dec', null, null, null, 82, null, 5],
['28-Dec', null, null, null, 77, null, 8],
['29-Dec', null, null, null, 69, null, 7],
['30-Dec', null, null, null, 62, null, 6],
['31-Dec', null, null, null, 56, "Aging Threshold breach:56 Units", 8]

        ]);

        options = {
            // title: 'Rate of Sales in a Month',
            width: 1100,
            height: 400,
            annotations: {
                textStyle: {
                    //  fontName: 'Tahoma',
                    fontSize: 14,
                    bold: false,
                    italic: false,
                    color: '#213c54',     // The color of the text.
                    //auraColor: '#d799ae', // The color of the text outline.
                    opacity: 0.8         // The transparency of the text.

                }
            },

            vAxes: {
                0: { logScale: false, title: 'Stock On Hand', gridlines: { color: 'transparent' } },
                1: { logScale: false, title: 'Units Sold', gridlines: { color: 'transparent' } }
            },
            series: {
                0: { targetAxisIndex: 1, lineWidth: 3, color: '#66bb6a', areaOpacity: 0.2, curveType: 'function' },
                3: { targetAxisIndex: 1, lineWidth: 3, color: '#66bb6a', lineDashStyle: [14, 2, 7, 2], curveType: 'function' },
                1: { type: 'steppedArea', targetAxisIndex: 0, lineWidth: 3, color: '#BCBCBC', areaOpacity: 0.2, curveType: 'function' },
                2: { type: 'steppedArea', targetAxisIndex: 0, lineWidth: 3, color: '#BCBCBC', lineDashStyle: [14, 2, 7, 2], curveType: 'function' }

            },



            curveType: 'function',
            animation: {
                "startup": true,
                duration: 1000,
                easing: 'out'
            }

        }
    }

    if (name.toString().toLowerCase().indexOf("stock aging") > -1 && name.toString().toLowerCase().indexOf("sc2943-100") > -1) {
        data = google.visualization.arrayToDataTable([
['Date', 'Rate of Sale', 'Stock On Hand', { role: 'annotation' }, 'Predicted Stock On Hand', { role: 'annotation' }, 'Predicted Rate of Sale'],

['1-Dec', 5, 254, "Stock:254", null, null, null],
['2-Dec', 5, 249, null, null, null, null],
['3-Dec', 6, 243, null, null, null, null],
['4-Dec', 7, 237, null, null, null, null],
['5-Dec', 7, 230, null, null, null, null],
['6-Dec', 7, 225, null, null, null, null],
['7-Dec', 7, 218, null, null, null, null],
['8-Dec', 6, 211, null, null, null, null],
['9-Dec', 6, 203, null, null, null, null],
['10-Dec', 5, 197, "Stock:197", null, null, null],
['11-Dec', 5, 190, null, null, null, null],
['12-Dec', 5, 183, null, null, null, null],
['13-Dec', 5, 178, null, null, null, null],
['14-Dec', 7, 170, null, null, null, null],
['15-Dec', 7, 163, null, null, null, null],
['16-Dec', 6, 155, null, null, null, null],
['17-Dec', 6, 149, "Stock:149", null, null, null],
['18-Dec', 6, 143, null, null, null, null],
['19-Dec', 8, 137, null, null, null, 8],
['20-Dec', 8, 130, null, null, null, 8],
['21-Dec', 8, 122, "Today", 122, null, 8],
['22-Dec', null, null, null, 116, null, 8],
['23-Dec', null, null, null, 108, null, 6],
['24-Dec', null, null, null, 102, null, 6],
['25-Dec', null, null, null, 97, null, 7],
['26-Dec', null, null, null, 90, null, 8],
['27-Dec', null, null, null, 82, null, 5],
['28-Dec', null, null, null, 77, null, 8],
['29-Dec', null, null, null, 69, null, 7],
['30-Dec', null, null, null, 62, null, 6],
['31-Dec', null, null, null, 56, "Aging Threshold breach:56 Units", 8]

        ]);

        options = {
            // title: 'Rate of Sales in a Month',
            width: 1100,
            height: 400,
            annotations: {
                textStyle: {
                    //  fontName: 'Tahoma',
                    fontSize: 14,
                    bold: false,
                    italic: false,
                    color: '#213c54',     // The color of the text.
                    //auraColor: '#d799ae', // The color of the text outline.
                    opacity: 0.8         // The transparency of the text.

                }
            },

            vAxes: {
                0: { logScale: false, title: 'Stock On Hand', gridlines: { color: 'transparent' } },
                1: { logScale: false, title: 'Units Sold', gridlines: { color: 'transparent' } }
            },
            series: {
                0: { targetAxisIndex: 1, lineWidth: 3, color: '#66bb6a', areaOpacity: 0.2, curveType: 'function' },
                3: { targetAxisIndex: 1, lineWidth: 3, color: '#66bb6a', lineDashStyle: [14, 2, 7, 2], curveType: 'function' },
                1: { type: 'steppedArea', targetAxisIndex: 0, lineWidth: 3, color: '#BCBCBC', areaOpacity: 0.2, curveType: 'function' },
                2: { type: 'steppedArea', targetAxisIndex: 0, lineWidth: 3, color: '#BCBCBC', lineDashStyle: [14, 2, 7, 2], curveType: 'function' }

            },



            curveType: 'function',
            animation: {
                "startup": true,
                duration: 1000,
                easing: 'out'
            }

        }
    }

    if (name.toString().toLowerCase().indexOf("stock aging") > -1 && name.toString().toLowerCase().indexOf("hm-slt65") > -1) {
        data = google.visualization.arrayToDataTable([
['Date', 'Rate of Sale', 'Stock On Hand', { role: 'annotation' }, 'Predicted Stock On Hand', { role: 'annotation' }, 'Predicted Rate of Sale'],

['1-Dec', 5, 254, "Stock:254", null, null, null],
['2-Dec', 5, 249, null, null, null, null],
['3-Dec', 6, 243, null, null, null, null],
['4-Dec', 7, 237, null, null, null, null],
['5-Dec', 7, 230, null, null, null, null],
['6-Dec', 7, 225, null, null, null, null],
['7-Dec', 7, 218, null, null, null, null],
['8-Dec', 6, 211, null, null, null, null],
['9-Dec', 6, 203, null, null, null, null],
['10-Dec', 5, 197, "Stock:197", null, null, null],
['11-Dec', 5, 190, null, null, null, null],
['12-Dec', 5, 183, null, null, null, null],
['13-Dec', 5, 178, null, null, null, null],
['14-Dec', 7, 170, null, null, null, null],
['15-Dec', 7, 163, null, null, null, null],
['16-Dec', 6, 155, null, null, null, null],
['17-Dec', 6, 149, "Stock:149", null, null, null],
['18-Dec', 6, 143, null, null, null, null],
['19-Dec', 8, 137, null, null, null, 8],
['20-Dec', 8, 130, null, null, null, 8],
['21-Dec', 8, 122, "Today", 122, null, 8],
['22-Dec', null, null, null, 102, null, 8],
['23-Dec', null, null, null, 91, null, 6],
['24-Dec', null, null, null, 81, null, 6],
['25-Dec', null, null, null, 76, null, 7],
['26-Dec', null, null, null, 62, null, 8],
['27-Dec', null, null, null, 49, null, 5],
['28-Dec', null, null, null, 35, null, 8],
['29-Dec', null, null, null, 32, null, 7],
['30-Dec', null, null, null, 25, null, 6],
['31-Dec', null, null, null, 14, "Aging Threshold breach:14 Units", 8]

        ]);

        options = {
            // title: 'Rate of Sales in a Month',
            width: 1100,
            height: 400,
            annotations: {
                textStyle: {
                    //  fontName: 'Tahoma',
                    fontSize: 14,
                    bold: false,
                    italic: false,
                    color: '#213c54',     // The color of the text.
                    //auraColor: '#d799ae', // The color of the text outline.
                    opacity: 0.8         // The transparency of the text.

                }
            },

            vAxes: {
                0: { logScale: false, title: 'Stock On Hand', gridlines: { color: 'transparent' } },
                1: { logScale: false, title: 'Units Sold', gridlines: { color: 'transparent' } }
            },
            series: {
                0: { targetAxisIndex: 1, lineWidth: 3, color: '#66bb6a', areaOpacity: 0.2, curveType: 'function' },
                3: { targetAxisIndex: 1, lineWidth: 3, color: '#66bb6a', lineDashStyle: [14, 2, 7, 2], curveType: 'function' },
                1: { type: 'steppedArea', targetAxisIndex: 0, lineWidth: 3, color: '#BCBCBC', areaOpacity: 0.2, curveType: 'function' },
                2: { type: 'steppedArea', targetAxisIndex: 0, lineWidth: 3, color: '#BCBCBC', lineDashStyle: [14, 2, 7, 2], curveType: 'function' }

            },



            curveType: 'function',
            animation: {
                "startup": true,
                duration: 1000,
                easing: 'out'
            }

        }
    }

    if (name.toString().toLowerCase().indexOf("stock aging") > -1 && name.toString().toLowerCase().indexOf("849559-402") > -1) {
        data = google.visualization.arrayToDataTable([
['Date', 'Rate of Sale', 'Stock On Hand', { role: 'annotation' }, 'Predicted Stock On Hand', { role: 'annotation' }, 'Predicted Rate of Sale'],

['1-Dec', 5, 254, "Stock:254", null, null, null],
['2-Dec', 5, 249, null, null, null, null],
['3-Dec', 6, 243, null, null, null, null],
['4-Dec', 7, 237, null, null, null, null],
['5-Dec', 7, 230, null, null, null, null],
['6-Dec', 7, 225, null, null, null, null],
['7-Dec', 7, 218, null, null, null, null],
['8-Dec', 6, 211, null, null, null, null],
['9-Dec', 6, 203, null, null, null, null],
['10-Dec', 5, 197, "Stock:197", null, null, null],
['11-Dec', 5, 190, null, null, null, null],
['12-Dec', 5, 183, null, null, null, null],
['13-Dec', 5, 178, null, null, null, null],
['14-Dec', 7, 170, null, null, null, null],
['15-Dec', 7, 163, null, null, null, null],
['16-Dec', 6, 155, null, null, null, null],
['17-Dec', 6, 149, "Stock:149", null, null, null],
['18-Dec', 6, 143, null, null, null, null],
['19-Dec', 8, 137, null, null, null, 8],
['20-Dec', 8, 130, null, null, null, 8],
['21-Dec', 8, 122, "Today", 122, null, 8],
['22-Dec', null, null, null, 102, null, 8],
['23-Dec', null, null, null, 91, null, 6],
['24-Dec', null, null, null, 81, null, 6],
['25-Dec', null, null, null, 76, null, 7],
['26-Dec', null, null, null, 62, null, 8],
['27-Dec', null, null, null, 49, null, 5],
['28-Dec', null, null, null, 35, null, 8],
['29-Dec', null, null, null, 32, null, 7],
['30-Dec', null, null, null, 25, null, 6],
['31-Dec', null, null, null, 14, "Aging Threshold breach:14 Units", 8]

        ]);

        options = {
            // title: 'Rate of Sales in a Month',
            width: 1100,
            height: 400,
            annotations: {
                textStyle: {
                    //  fontName: 'Tahoma',
                    fontSize: 14,
                    bold: false,
                    italic: false,
                    color: '#213c54',     // The color of the text.
                    //auraColor: '#d799ae', // The color of the text outline.
                    opacity: 0.8         // The transparency of the text.

                }
            },

            vAxes: {
                0: { logScale: false, title: 'Stock On Hand', gridlines: { color: 'transparent' } },
                1: { logScale: false, title: 'Units Sold', gridlines: { color: 'transparent' } }
            },
            series: {
                0: { targetAxisIndex: 1, lineWidth: 3, color: '#66bb6a', areaOpacity: 0.2, curveType: 'function' },
                3: { targetAxisIndex: 1, lineWidth: 3, color: '#66bb6a', lineDashStyle: [14, 2, 7, 2], curveType: 'function' },
                1: { type: 'steppedArea', targetAxisIndex: 0, lineWidth: 3, color: '#BCBCBC', areaOpacity: 0.2, curveType: 'function' },
                2: { type: 'steppedArea', targetAxisIndex: 0, lineWidth: 3, color: '#BCBCBC', lineDashStyle: [14, 2, 7, 2], curveType: 'function' }

            },



            curveType: 'function',
            animation: {
                "startup": true,
                duration: 1000,
                easing: 'out'
            }

        }
    }

    var chart = new google.visualization.LineChart(document.getElementById('leafchart'));
    chart.draw(data, options);
}

function drawPriceChartByDate() {
    var data = google.visualization.arrayToDataTable([
    ['Date', 'Actual', 'Max', 'Median', 'Predicted', { role: 'annotation' }],
    ['1-Dec-16', 112.6284441, 118.3712908, 111.8674008, null, null],
    ['2-Dec-16', 110.6877467, 112.8886614, 109.2404544, null, null],
    ['3-Dec-16', 112.9711316, 116.0866366, 109.9281188, null, null],
    ['4-Dec-16', 112.8932883, 112.3626278, 109.7942482, null, null],
    ['5-Dec-16', 112.9137925, 112.5039646, 109.2786334, null, null],
    ['6-Dec-16', 110.9521122, 111.7145397, 111.2856341, null, null],
    ['7-Dec-16', 110.8702515, 118.0954118, 111.3605509, null, null],
    ['8-Dec-16', 111.7444483, 111.5668009, 109.8925507, null, null],
    ['9-Dec-16', 110.6572658, 111.544646, 110.4701232, null, null],
    ['10-Dec-16', 110.9746652, 112.8165864, 110.281294, null, null],
    ['11-Dec-16', 111.8742632, 117.4308709, 110.2858973, null, null],
    ['12-Dec-16', 109.5761702, 112.381261, 105.8496779, null, null],
    ['13-Dec-16', 109.0427869, 111.1874965, 104.8334576, null, null],
    ['14-Dec-16', 109.705505, 115.897169, 106.8171409, null, null],
    ['15-Dec-16', 111.0311606, 120.4958283, 108.398779, null, null],
    ['16-Dec-16', 110.3635695, 119.4163369, 109.041241, null, null],
    ['17-Dec-16', 111.2298519, 115.4778362, 111.5215536, null, null],
    ['18-Dec-16', 112.7037805, 117.821094, 110.4905283, null, null],
    ['19-Dec-16', 110.2867763, 116.9537135, 110.1550107, null, null],
    ['20-Dec-16', 107.5965217, 113.2074447, 106.3716505, null, null],
    ['21-Dec-16', 109.65, 112.4004324, 106.8666848, 109.65, 'Today'],
    ['22-Dec-16', null, null, null, 115, null],
    ['23-Dec-16', null, null, null, 112, null],
    ['24-Dec-16', null, null, null, 115, null],
    ['25-Dec-16', null, null, null, 113, null],
    ['26-Dec-16', null, null, null, 115, null],
    ['27-Dec-16', null, null, null, 113, null],
    ['28-Dec-16', null, null, null, 112, null],
    ['29-Dec-16', null, null, null, 113, null],
    ['30-Dec-16', null, null, null, 114, null]
    ]);

    options = {
        vAxis: {
            //// title: 'Associate Floor Hours',
            gridlines: {
                count: -1,
                color: 'transparent',
            }, minValue: 0
        },
        legend: { position: "top" },
        hAxis: {
            //// title: 'Departments',
            gridlines: { count: -1, color: 'transparent' }
        },
        seriesType: 'bars',
        series: {
            0: { color: '#4fc3f7' },
            1: { type: 'steppedArea', color: '#cfd8dc', areaOpacity: 0.5, connectSteps: false },
        },
        animation: {
            "startup": true,
            duration: 1000,
            easing: 'out'
        },
    };

    var chart = new google.visualization.LineChart(document.getElementById('leafchart'));
    chart.draw(data, options);
}

function drawPriceChartByWeek() {
    var data = google.visualization.arrayToDataTable([
    ['Week', 'Actual', 'Max', 'Median', 'Predicted', { role: 'annotation' }],
                                ['47', 109.52, 114.26, 108.46, null, null],
                                ['48', 108.61, 114.55, 106.33, null, null],
                                ['49', 112.42, 117.30, 106.26, null, null],
                                ['50', 111.84, 118.48, 108.15, null, null],
                                ['51', 112.95, 115.86, 107.32, null, null],
                                ['52', 110.55, 120.44, 105.96, 110.55, 'Today'],
                                ['53', null, null, null, 114.56, null]

    ]);
    options = {
        vAxis: {
            //// title: 'Associate Floor Hours',
            gridlines: {
                count: -1,
                color: 'transparent',
            }, minValue: 0
        },
        legend: { position: "top" },
        hAxis: {
            //// title: 'Departments',
            gridlines: { count: -1, color: 'transparent' }
        },
        seriesType: 'bars',
        series: {
            0: { color: '#4fc3f7' },
            1: { type: 'steppedArea', color: '#cfd8dc', areaOpacity: 0.5, connectSteps: false },
        },
        animation: {
            "startup": true,
            duration: 1000,
            easing: 'out'
        },
    };
    var chart = new google.visualization.LineChart(document.getElementById('leafchart'));
    chart.draw(data, options);
}

function drawSocialChartByDate() {
    var data = google.visualization.arrayToDataTable([
    ['Date', 'Actual', 'Max', 'Median', { role: 'annotation' }],
                                    ['1-Dec', 69, 73, 70, null],
                                    ['2-Dec', 72, 74, 69, null],
                                    ['3-Dec', 72, 73, 70, null],
                                    ['4-Dec', 69, 75, 70, null],
                                    ['5-Dec', 69, 72, 69, null],
                                    ['6-Dec', 72, 73, 68, null],
                                    ['7-Dec', 69, 73, 68, null],
                                    ['8-Dec', 70, 75, 69, null],
                                    ['9-Dec', 71, 75, 70, null],
                                    ['10-Dec', 71, 75, 68, null],
                                    ['11-Dec', 69, 72, 70, null],
                                    ['12-Dec', 71, 74, 69, null],
                                    ['13-Dec', 69, 75, 68, null],
                                    ['14-Dec', 69, 74, 69, null],
                                    ['15-Dec', 71, 74, 69, null],
                                    ['16-Dec', 72, 72, 68, null],
                                    ['17-Dec', 72, 73, 68, null],
                                    ['18-Dec', 71, 73, 70, null],
                                    ['19-Dec', 70, 72, 68, null],
                                    ['20-Dec', 73, 75, 69, null],
                                    ['21-Dec', 71, 75, 70, 'Today'],
                                    ['22-Dec', null, null, null, null],
                                    ['23-Dec', null, null, null, null],
                                    ['24-Dec', null, null, null, null],
                                    ['25-Dec', null, null, null, null],
                                    ['26-Dec', null, null, null, null],
                                    ['27-Dec', null, null, null, null],
                                    ['28-Dec', null, null, null, null],
                                    ['29-Dec', null, null, null, null],
                                    ['30-Dec', null, null, null, null]
    ]);

    var options = {
        vAxis: { title: 'Social Appeal', gridlines: { color: 'transparent' } },
        hAxis: { gridlines: { count: -1, color: 'transparent' }, textStyle: { fontSize: 12 }, showTextEvery: 2 },
        seriesType: 'line',
        isStacked: false,
        connectSteps: false,
        legend: { position: 'top-right', textStyle: { fontName: 'Arial', color: 'black', fontSize: 12 } },
        series: {
            0: { type: 'line', pointSize: 4, pointShape: 'circle', lineWidth: 3, color: '#66bb6a' },
            1: { type: 'steppedArea', connectSteps: true, color: '#cfd8dc', areaOpacity: 0.8 },
            2: { type: 'steppedArea', connectSteps: true, color: '#80d8ff', areaOpacity: 0.6 },
            3: { type: 'line', lineDashStyle: [14, 2, 7, 2], pointSize: 4, pointShape: 'circle', lineWidth: 3, color: '#66bb6a' },
        },
        curveType: 'function',
        animation: {
            "startup": true,
            duration: 1000,
            easing: 'out'
        },
        annotations: {
            textStyle: {
                // fontName: 'Times-Roman',
                fontSize: 14,
                //bold: true,
                //italic: true,
                color: '#1b3245',     // The color of the text.
                //auraColor: '#d799ae', // The color of the text outline.
                opacity: 0.8,
            }     // The transparency of the text.
        },
        chartArea: { left: 50, top: 15, bottom: 30, width: '80%', height: '80%' }
    };
    var chart = new google.visualization.LineChart(document.getElementById('ConversionChart'));
    chart.draw(data, options);
}
function drawSocialChartByWeek() {
    var data = google.visualization.arrayToDataTable([
        ['Week', 'Actual', 'Max', 'Median', 'Predicted', { role: 'annotation' }],
                                ['47', 72, 74, 69, null, null],
                                ['48', 71, 75, 70, null, null],
                                ['49', 74, 75, 71, null, null],
                                ['50', 75, 79, 73, null, null],
                                ['51', 74, 78, 71, null, null],
                                ['52', 72, 75, 68, 72, 'Today'],
                                ['53', null, null, null, 74, null]

    ]);

    var options = {
        vAxis: { title: 'Social Appeal', gridlines: { color: 'transparent' } },
        hAxis: { gridlines: { count: -1, color: 'transparent' }, textStyle: { fontSize: 12 }, showTextEvery: 2 },
        seriesType: 'line',
        isStacked: false,
        connectSteps: false,
        legend: { position: 'top-right', textStyle: { fontName: 'Arial', color: 'black', fontSize: 12 } },
        series: {
            0: { type: 'line', pointSize: 4, pointShape: 'circle', lineWidth: 3, color: '#66bb6a' },
            1: { type: 'steppedArea', connectSteps: true, color: '#cfd8dc', areaOpacity: 0.8 },
            2: { type: 'steppedArea', connectSteps: true, color: '#80d8ff', areaOpacity: 0.6 },
            3: { type: 'line', lineDashStyle: [14, 2, 7, 2], pointSize: 4, pointShape: 'circle', lineWidth: 3, color: '#66bb6a' },
        },
        curveType: 'function',
        animation: {
            "startup": true,
            duration: 1000,
            easing: 'out'
        },
        annotations: {
            textStyle: {
                fontSize: 14,
                color: '#1b3245',     // The color of the text.
                opacity: 0.8,
            }     // The transparency of the text.
        },
        chartArea: { left: 50, top: 15, bottom: 30, width: '80%', height: '80%' }
    };

    var chart = new google.visualization.LineChart(document.getElementById('ConversionChart'));
    chart.draw(data, options);
}

function drawBasketChartByDate() {
    var data = google.visualization.arrayToDataTable([
    ['Date', 'Actual', 'Max', 'Median', 'Predicted', { role: 'annotation' }],
                                ['1-Dec', 112.6284441, 118.3712908, 111.8674008, null, null],
                                ['2-Dec', 110.6877467, 112.8886614, 109.2404544, null, null],
                                ['3-Dec', 112.9711316, 116.0866366, 109.9281188, null, null],
                                ['4-Dec', 112.8932883, 112.3626278, 109.7942482, null, null],
                                ['5-Dec', 112.9137925, 112.5039646, 109.2786334, null, null],
                                ['6-Dec', 110.9521122, 111.7145397, 111.2856341, null, null],
                                ['7-Dec', 110.8702515, 118.0954118, 111.3605509, null, null],
                                ['8-Dec', 111.7444483, 111.5668009, 109.8925507, null, null],
                                ['9-Dec', 110.6572658, 111.544646, 110.4701232, null, null],
                                ['10-Dec', 110.9746652, 112.8165864, 110.281294, null, null],
                                ['11-Dec', 111.8742632, 117.4308709, 110.2858973, null, null],
                                ['12-Dec', 109.5761702, 112.381261, 105.8496779, null, null],
                                ['13-Dec', 109.0427869, 111.1874965, 104.8334576, null, null],
                                ['14-Dec', 109.705505, 115.897169, 106.8171409, null, null],
                                ['15-Dec', 111.0311606, 120.4958283, 108.398779, null, null],
                                ['16-Dec', 110.3635695, 119.4163369, 109.041241, null, null],
                                ['17-Dec', 111.2298519, 115.4778362, 111.5215536, null, null],
                                ['18-Dec', 112.7037805, 117.821094, 110.4905283, null, null],
                                ['19-Dec', 110.2867763, 116.9537135, 110.1550107, null, null],
                                ['20-Dec', 107.5965217, 113.2074447, 106.3716505, null, null],
                                ['21-Dec', 109.65, 112.4004324, 106.8666848, 109.65, 'Today'],
                                ['22-Dec', null, null, null, 115, null],
                                ['23-Dec', null, null, null, 112, null],
                                ['24-Dec', null, null, null, 115, null]


    ]);
    var options = {
        vAxis: { title: 'Basket Size', gridlines: { color: 'transparent' } },
        hAxis: { gridlines: { count: -1, color: 'transparent' }, textStyle: { fontSize: 12 }, showTextEvery: 2 },
        seriesType: 'line',
        isStacked: false,
        connectSteps: false,
        legend: { position: 'top-right', textStyle: { fontName: 'Arial', color: 'black', fontSize: 12 } },
        series: {
            0: { type: 'line', pointSize: 4, pointShape: 'circle', lineWidth: 3, color: '#66bb6a' },
            1: { type: 'steppedArea', connectSteps: true, color: '#cfd8dc', areaOpacity: 0.8 },
            2: { type: 'steppedArea', connectSteps: true, color: '#80d8ff', areaOpacity: 0.6 },
            3: { type: 'line', lineDashStyle: [14, 2, 7, 2], pointSize: 4, pointShape: 'circle', lineWidth: 3, color: '#66bb6a' },
        },
        curveType: 'function',
        animation: {
            "startup": true,
            duration: 1000,
            easing: 'out'
        },
        annotations: {
            textStyle: {
                fontSize: 14,
                color: '#1b3245',     // The color of the text.
                opacity: 0.8,
            }     // The transparency of the text.
        },
        chartArea: { left: 50, top: 15, bottom: 30, width: '80%', height: '80%' }
    };

    var chart = new google.visualization.LineChart(document.getElementById('ConversionChart'));
    chart.draw(data, options);
}
function drawBasketChartByWeek() {
    var data = google.visualization.arrayToDataTable([
        ['Week', 'Actual', 'Max', 'Median', 'Predicted', { role: 'annotation' }],
                                ['47', 109.52, 114.26, 108.46, null, null],
                                ['48', 108.61, 114.55, 106.33, null, null],
                                ['49', 112.42, 117.30, 106.26, null, null],
                                ['50', 111.84, 118.48, 108.15, null, null],
                                ['51', 112.95, 115.86, 107.32, null, null],
                                ['52', 110.55, 120.44, 105.96, 110.55, 'Today'],
                                ['53', null, null, null, 114.56, null]

    ]);

    var options = {
        vAxis: { title: 'Basket Size', gridlines: { color: 'transparent' } },
        hAxis: { gridlines: { count: -1, color: 'transparent' }, textStyle: { fontSize: 12 }, showTextEvery: 2 },
        seriesType: 'line',
        isStacked: false,
        connectSteps: false,
        legend: { position: 'top-right', textStyle: { fontName: 'Arial', color: 'black', fontSize: 12 } },
        series: {
            0: { type: 'line', pointSize: 4, pointShape: 'circle', lineWidth: 3, color: '#66bb6a' },
            1: { type: 'steppedArea', connectSteps: true, color: '#cfd8dc', areaOpacity: 0.8 },
            2: { type: 'steppedArea', connectSteps: true, color: '#80d8ff', areaOpacity: 0.6 },
            3: { type: 'line', lineDashStyle: [14, 2, 7, 2], pointSize: 4, pointShape: 'circle', lineWidth: 3, color: '#66bb6a' },
        },
        curveType: 'function',
        animation: {
            "startup": true,
            duration: 1000,
            easing: 'out'
        },
        annotations: {
            textStyle: {
                fontSize: 14,
                color: '#1b3245',     // The color of the text.
                opacity: 0.8,
            }     // The transparency of the text.
        },
        chartArea: { left: 50, top: 15, bottom: 30, width: '80%', height: '80%' }
    };

    var chart = new google.visualization.LineChart(document.getElementById('ConversionChart'));
    chart.draw(data, options);
}