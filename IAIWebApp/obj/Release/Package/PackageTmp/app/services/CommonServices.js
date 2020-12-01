angular.module('IAMInterviewed').service('CommonServices', function (DataServices, $rootScope) {

    var DataSetTestConnection = IAMInterviewed.profileBook.dataSet.testConnection.save;
    var DataSetDiscover = IAMInterviewed.profileBook.dataSet.discover.save;
    //Common function for TestConnection
    this.validateConn = function (data, $scope) {
        DataServices.post(DataSetTestConnection, data, false).then(
         success);
        function success(resp) {
            //validateConn();
            if (resp.result.connectionSuccessful == true) {
                $rootScope.resultMessage = 'Connection established successfully.';
                showNotification('success')
            } else {
                // $('#continueBtn').attr("disabled","disabled");      
                $rootScope.resultMessage = 'Unable to connect. Please check the connection parameters and try again or contact your administrator.';
                showNotification('error')
            }
        }
    }

    //Common function for Discover 
    this.discover = function (data) {
        DataServices.post(DataSetDiscover, data, false).then(
        success);
        function success(resp) {
        }
    }

    //to String
    this.toString = function (param) {
        var cp = _.map(param, function (o) {
            return o.text;
        });
        return cp.toString();
    }

    //Getting Data
    this.convertGetJson = function (data) {
        var rObj = {};
        var kvArray = data;
        var reformattedArray = kvArray.map(function (obj) {
            rObj[obj.name.replace(/ /gi, '')] = obj.value;
        });
        return rObj;
    }

    this.convertPostJson = function (data) {
        var postArray = [];
        for (key in data) {
            postArray.push({
                "name": key.replace(/ /gi, ''),
                "value": data[key]
            });
        }
        return postArray;
    }

    //Date conversion function
    this.dateConversion = function (date) {
        var dateVal = '';
        //var uiDate = date.split(" ");
        //var dateVal = uiDate[0] + 'T' + uiDate[1] + ':00Z';
        var dateVal = (new Date(date)).toISOString();
        return dateVal;//.toLocaleDateString()
    }

    //Date conversion based on timezone usage: CommonServices.toDateWithTimeZone(date, 'America/New_York')
    this.toDateWithTimeZone = function (time, zone, format) {
        //If format is null then default to the specific format.
        if (format == null || format == '') {
            format = 'YYYY-MM-DD HH:mm';
        }

        var dt = moment.tz(time, zone);
        return dt.format(format);
    }

    //User Details post Conversion
    this.getUsersKey = function (data, listOfUsers) {
        var finalData = [];
        var dataString = data.toString().split(',');
        for (var i = 0; i < dataString.length; i++) {
            var userId = (dataString[i].split('-')[1]).trim();
            var userObject = {};
            angular.forEach(listOfUsers, function (value, key) { if (value.userId == userId) { finalData.push(value); } });
        }
        return finalData;
    }

    //UerRoles converting to array of objects
    this.getRoleObjectForRoles = function (roles, fullRoleList, userId) {
        var rolesList = [];
        for (var k = 0; k < fullRoleList.length; k++) {
            for (var j = 0; j < roles.length; j++) {
                if (roles[j].text == fullRoleList[k].roleName) {
                    var data = {
                        roleId: fullRoleList[k].roleCode, roleName: fullRoleList[k].roleName, users: [{
                            userId: userId,
                            userFirstName: 'null',
                            userLastName: 'null',
                            userEmailAddress: 'null'
                        }]
                    }
                    rolesList.push(data);
                }
            }

        }
        return rolesList;
    }

    //By Default User and user Role Values
    this.getUserDefaultVal = function () {
        var userArr = [];
        var data = {
            users: [{
                userId: '430887',
                userFirstName: 'null',
                userLastName: 'null',
                userEmailAddress: 'null'
            }]
        }

        userArr.push(data);
        return userArr;
    }

    //By Default User and user Role Values
    this.getUserRolesDefaultVal = function () {
        var userRoleArr = [];
        var data = {
            roleId: 'ADMIN', roleName: 'NULL', users: [{
                userId: '430887',
                userFirstName: 'null',
                userLastName: 'null',
                userEmailAddress: 'null'
            }]
        }

        userRoleArr.push(data);
        return userRoleArr;
    }
    //User Details Conversion while ng-model
    this.usersToList = function (data) {
        var finalData = [];
        for (var j = 0; j < data.length; j++) {
            if (finalData.length <= 0) {
                finalData = [data[j].userFirstName + " " + data[j].userLastName + "-" + data[j].userId];
            }
            else {
                finalData = [finalData + "," + data[j].userFirstName + " " + data[j].userLastName + "-" + data[j].userId];
            }
        }
        return finalData;
    }

    //Roles conversion while ng-model
    this.usersRoleToList = function (data) {
        var finalData = [];
        for (var j = 0; j < data.length; j++) {
            finalData.push(data[j].roleName);
        }
        return finalData;
    }

    //Common function for edit buttton in summary       
    this.editFunction = function () {
        $('#form_wizard_1').bootstrapWizard('show', 0);
        $('#submitBtnGroup').show();
        $('.button-next').show();
        $('.button-submit').hide();
    }

    this.parentDataSet = function (data) {
        var finalData = [];
        for (var j = 0; j < data.length; j++) {
            //             var datasetObj={};
            //             datasetObj.dataSourceName=data[j].generalInformation.dataSourceName;
            //             datasetObj.identifier=data[j].generalInformation.identifier;
            finalData.push(data[j].generalInformation.identifier + " " + '(' + data[j].generalInformation.dataSourceName + ')');

        }
        return finalData;
    }
});