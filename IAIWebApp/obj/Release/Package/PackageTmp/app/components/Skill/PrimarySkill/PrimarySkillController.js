angular.module('IAMInterviewed').controller('PrimarySkillController', ['$scope', '$http', '$filter', 'DataServices', '$rootScope', 'LoaderService', '$timeout', '$window', '$localStorage', function ($scope, $http, $filter, DataServices, $rootScope, LoaderService, $timeout, $window, $localStorage, $location) {
    var clicked = true;

    $scope.toggleAddSection = function (obj) {
        if (clicked) {
            clicked = false;
            $("#" + obj).slideDown("fast");
        } else {
            clicked = true;
            $("#" + obj).slideUp("fast");
        }
    }

    $scope.ClearAll = function () {
        $scope.SkillId = "0";
        $scope.SkillName = "";
        $scope.Description = "";
    }
    $scope.ClearAll();

    $scope.LoadPrimarySkills = function () {
        manageLoader('load');
        var getPrimarySkills = IAMInterviewed.Skills.GetSkills;
        $http.get(getPrimarySkills).then(
            function success(response) {
                //console.log(response.data);
                $scope.PrimarySkills = response.data.data;
                $scope.loadPrimarySkillsGrid();
                manageLoader();
            },
            function error(response) {
                $rootScope.resultMessage = response.data.errorMessage;
                showNotification('error');
                manageLoader();
                return false;
            });
    }
    $scope.LoadPrimarySkills();

    $scope.EditSkill = function (objSkill) {
        var objPrimarySkill = $.grep($scope.PrimarySkills, function (e) {
            return e.SkillId == objSkill;
        });
        if (objPrimarySkill.length > 0) {
            $scope.SkillId = objPrimarySkill[0].SkillId;
            $scope.SkillName = objPrimarySkill[0].SkillName;
            $scope.Description = objPrimarySkill[0].Description;

            clicked = true;
            $scope.toggleAddSection('addContent');
        }
    }

    $scope.AddPrimarySkill = function () {
        manageLoader('load');
        var addPrimarySkills = IAMInterviewed.Skills.AddSkill + "?SkillId=" + $scope.SkillId + "&SkillName=" + $scope.SkillName + "&Description=" + $scope.Description;
        $http.get(addPrimarySkills).then(
           function success(response) {
               $scope.ClearAll();
               $scope.LoadPrimarySkills();
               $scope.toggleAddSection('addContent');
               $rootScope.resultMessage = "Skill Added/Updated Successfully.";
               showNotification('success');
               manageLoader();
           },
           function error(response) {
               $rootScope.resultMessage = response.data.errorMessage;
               showNotification('error');
               manageLoader();
               return false;
           });
    }

    $scope.loadPrimarySkillsGrid = function () {
        var columnFiltersMyGrid = {};
        var columnsFiltersSyncView = {};
        $scope.processMyGridData = function (dataElements, idStart) {
            var i = idStart;
            var resultData = []
            angular.forEach(dataElements, function (value, key) {
                var result = {
                    Skill: value['SkillName'],
                    Description: value['Description'],
                    SkillId: value['SkillId'],
                    actions: ''
                };
                result.id = i;
                i = i + 1;
                $scope.respStructure.push(result);
            });
        }

        function DateFormatter(value) {
            var pattern = /Date\(([^)]+)\)/;
            var results = pattern.exec(value);
            if (results != null) {
                var dt = new Date(parseFloat(results[1]));
                return dt.getDate() + "/" + (dt.getMonth() + 1) + "/" + dt.getFullYear();
            }
            else {
                return value;
            }
        }

        function filterMyGrid(item) {
            for (var columnId in columnFiltersMyGrid) {
                if (columnId !== undefined && columnFiltersMyGrid[columnId] !== "") {
                    var c = gridMyGrid.getColumns()[gridMyGrid.getColumnIndex(columnId)];
                    if (typeof item[c.field] === 'number') {
                        if (item[c.field] != columnFiltersMyGrid[columnId]) return false;
                    }
                    else if (typeof item[c.field] === 'datetime') {
                        if (item[c.field].indexOf(columnFiltersMyGrid[columnId]) < 0) {
                            return false;
                        }
                    }
                    else {
                        if (item[c.field] == null || item[c.field] == undefined) {
                            return false;
                        }
                        if (item[c.field] && item[c.field].toLowerCase().indexOf(columnFiltersMyGrid[columnId].toLowerCase()) < 0) {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        var columnsMyGrid = [
            { id: "Skill", name: "Skill", field: "Skill", width: 40, minWidth: 40, sortable: true, selectable: true },
            { id: "Description", name: "Description", field: "Description", width: 40, minWidth: 40, sortable: true, selectable: true },
            { id: "SkillId", name: "Id", field: "SkillId", skipFilter: true, width: 0, minWidth: 0, cssClass: "reallyHidden", headerCssClass: "reallyHidden" },
            { id: "actions", name: "Actions", field: "actions", width: 40, minwidth: 40, formatter: actionCompareFormat, skipFilter: true }
        ];

        var optionsMyGrid = {
            enableCellNavigation: true,
            showHeaderRow: true,
            headerRowHeight: 40,
            explicitInitialization: true,
            enableColumnReorder: false,
            forceFitColumns: true
        };

        function actionCompareFormat(row, cell, value, columnDef, rowObject) {
            return '<div class="actionCntr">' + '<a href="#" class="btn-primary-outline btn-xs" title="Edit" onclick="angular.element(this).scope().EditSkill(\'' + rowObject.SkillId + '\')">Edit</a>' + '</div>';
        }

        dataMyGrid = new Slick.Data.DataView();
        gridMyGrid = new Slick.Grid("#RegisteredCandidatesGrid", dataMyGrid, columnsMyGrid, optionsMyGrid);
        $('#pager').show();
        var pager = new Slick.Controls.Pager(dataMyGrid, gridMyGrid, $("#pager"));
        dataMyGrid.setPagingOptions({
            pageSize: 20
        });
        //Total Number of records Found
        //$("#RegisteredCandidatesGridRecord").text("0");
        $("#RegisteredCandidatesGrid .grid-canvas").addClass("norecord");
        $("#RegisteredCandidatesGrid .norecord").append("<div class='datanotfound'><h4 class='text-center norecord-font' style='font-weight:600 !important;'>No Record Found</h4></div>");
        $("#RegisteredCandidatesGrid .datanotfound").show();
        dataMyGrid.onRowCountChanged.subscribe(function (e, args) {
            //$("#RegisteredCandidatesGridRecord").text(args.current);
            if (args.current === 0) {
                $("#RegisteredCandidatesGrid .grid-canvas").addClass("norecord");
                $("#RegisteredCandidatesGrid .norecord").append("<div class='datanotfound'><h4 class='text-center norecord-font' style='font-weight:600 !important;'>No Record Found</h4></div>");
                $("#RegisteredCandidatesGrid .datanotfound").show();
            } else {
                $("#RegisteredCandidatesGrid .grid-canvas").removeClass("norecord");
                $("#RegisteredCandidatesGrid .datanotfound").remove();
            }
        });

        //End Total Number of records Found
        dataMyGrid.onRowCountChanged.subscribe(function (e, args) {
            gridMyGrid.updateRowCount();
            gridMyGrid.render();
        });

        dataMyGrid.onRowsChanged.subscribe(function (e, args) {
            gridMyGrid.invalidateRows(args.rows);
            gridMyGrid.render();
        });
        dataMyGrid.onRowCountChanged.subscribe(function (e, args) {
            gridMyGrid.updateRowCount();
            gridMyGrid.render();
        });
        dataMyGrid.onRowsChanged.subscribe(function (e, args) {
            gridMyGrid.invalidateRows(args.rows);
            gridMyGrid.render();
        });
        $(gridMyGrid.getHeaderRow()).delegate(":input", "change keyup", function (e) {
            var columnId = $(this).data("columnId");
            if (columnId != null) {
                columnFiltersMyGrid[columnId] = $.trim($(this).val());
                dataMyGrid.refresh();
            }
        });
        gridMyGrid.onHeaderRowCellRendered.subscribe(function (e, args) {
            $(args.node).empty();
            if (args.column.skipFilter == true) return;
            $("<input type='text' style='height:16px !important;' class='form-control'>")
                .data("columnId", args.column.id)
                .val(columnFiltersMyGrid[args.column.id])
                .appendTo(args.node);
        });

        gridMyGrid.onSort.subscribe(function (e, args) {
            var comparer = function (a, b) {
                return (a[args.sortCol.field] > b[args.sortCol.field]) ? 1 : -1;
            }

            // Delegate the sorting to DataView.
            // This will fire the change events and update the grid.
            dataMyGrid.sort(comparer, args.sortAsc);
        });

        gridMyGrid.init();
        gridMyGrid.registerPlugin(new Slick.AutoTooltips({ enableForHeaderCells: true }));
        gridMyGrid.render();

        function loadRowsMyGrid() {
            dataMyGrid.beginUpdate();
            dataMyGrid.setItems($scope.respStructure);
            dataMyGrid.setFilter(filterMyGrid);
            dataMyGrid.endUpdate();
            dataMyGrid.refresh();
            gridMyGrid.invalidate();

        }
        dataMyGrid.setFilter(function (item) {
            return true;
        });
        //manageLoader('load');
        $scope.respStructure = [];
        loadRowsMyGrid();
        //reload data
        $scope.processMyGridData($scope.PrimarySkills, 0);
        loadRowsMyGrid();

    }
}]);