var IAI = angular.module("IAMInterviewed", ['data', 'ngTagsInput', 'ngMaterial', 'ngMessages', 'ngStorage', 'ngIdle', 'ui.bootstrap', 'ngResource']);
IAI.factory('httpRequestInterceptor', ['$rootScope', '$localStorage', function ($rootScope, $localStorage) {
    return {
        request: function (config) {
            var token = '';
            if ($localStorage.token != undefined) {
                config.headers['Authorization'] = 'Bearer ' + $localStorage.token;
                config.headers['Content-Type'] = 'application/x-www-form-urlencoded;charset=utf-8';
            }
            return config;
        }
    };
}]);
IAI.config(function ($httpProvider) {
    $httpProvider.interceptors.push('httpRequestInterceptor');
});

IAI.filter('emptyFilter', function () {
    return function (array) {
        var filteredArray = [];
        angular.forEach(array, function (item) {
            if (item.Item1 != "0") filteredArray.push(item);
        });
        return filteredArray;
    };
});

IAI.filter('makePositive', function () {
    return function (num) { return Math.abs(num); }
});

IAI.filter('floorValue', function () {
    return function (num) { return Math.floor(num); }
});

IAI.filter('dateFormatter', function () {
    return function (num) {
        var pattern = /Date\(([^)]+)\)/;
        var results = pattern.exec(num);
        if (results != null) {
            var dt = new Date(parseFloat(results[1]));
            return dt.getDate() + "/" + (dt.getMonth() + 1) + "/" + dt.getFullYear();
        }
        else {
            return num;
        }
    }
});

IAI.filter('numberFormatter', function () {
    return function (num) {
        var formattedNumber = '';
        num = Math.abs(num)
        if (num >= 10000000) {
            formattedNumber = Math.floor((num / 10000000));
        } else if (num >= 100000) {
            formattedNumber = Math.floor((num / 100000).toFixed(1).replace(/\.0$/, ''));
        } else if (num >= 1000) {
            formattedNumber = Math.floor((num / 1000).toFixed(1).replace(/\.0$/, ''));
        } else {
            formattedNumber = num;
        }
        return formattedNumber;
    }
});

IAI.filter('numberUnitFormatter', function () {
    return function (num) {
        var formattedUnit = '';
        num = Math.abs(num)
        if (num >= 10000000) {
            formattedUnit = 'Cr';
        } else if (num >= 100000) {
            formattedUnit = 'L';
        } else if (num >= 1000) {
            formattedUnit = 'K';
        } else {
            formattedUnit = '';
        }
        //alert(Math.floor(formattedNumber));
        return formattedUnit;
    }
});

IAI.directive('convertToString', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attrs, ngModel) {
            ngModel.$parsers.push(function (val) {
                return val.toString.trim();
            });
            ngModel.$formatters.push(function (val) {
                return '' + val;
            });
        }
    };
});

IAI.factory('LoaderService', function () {
    this.loadModule = {
        id: 0
        , type: "Home"
    };
    return {
        module: function () {
            return this.loadModule;
        }
        , setModule: function (moduleData) {
            this.loadModule = moduleData;
        }
        , setModuleId: function (id) {
            if (id != undefined) {
                this.loadModule.id = id;
            }
        }
        , getModuleId: function () {
            return this.loadModule.id;
        }
        , setModuleType: function (type) {
            if (type != undefined) {
                this.loadModule.type = type;
            }
        }
        , getModuleType: function () {
            return this.loadModule.type;
        }
        , getParameterByName: function (name) {
            name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
            var regex = new RegExp("[\\?&]" + name + "=([^&#]*)")
                , results = regex.exec(location.search);
            return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
        }
    };
});


IAI.directive('structureFile', function () {
    return {
        scope: true, //create a new scope  
        link: function (scope, el, attrs) {
            el.bind('change', function (event) {
                var files = event.target.files;
                //iterate files since 'multiple' may be specified on the element  
                for (var i = 0; i < files.length; i++) {
                    //emit event upward  
                    scope.$emit("seletedFile", {
                        file: files[i]
                    });
                }
            });
        }
    };
});
//File Upload directive
IAI.directive('fileModel', ['$parse', function ($parse) {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            var model = $parse(attrs.fileModel);
            var modelSetter = model.assign;

            element.bind('change', function () {
                scope.$apply(function () {
                    modelSetter(scope, element[0].files[0]);
                });
            });
        }
    };
}]);

IAI.factory('myService', ['$http', function ($http) {
    return {
        uploadFile: function (url, file) {
            return $http({
                url: url,
                method: 'POST',
                data: file,
                headers: { 'Content-Type': undefined }, //this is important
                transformRequest: angular.identity //also important
            });
        },
        otherFunctionHere: function (url, stuff) {
            return $http.get(url);
        }
    };
}]);

IAI.directive('sglclick', ['$parse', function ($parse) {
    return {
        restrict: 'A'
        , link: function (scope, element, attr) {
            var fn = $parse(attr['sglclick']);
            var delay = 300
                , clicks = 0
                , timer = null;
            element.on('click', function (event) {
                clicks++; //count clicks
                if (clicks === 1) {
                    timer = setTimeout(function () {
                        scope.$apply(function () {
                            fn(scope, {
                                $event: event
                            });
                        });
                        clicks = 0; //after action performed, reset counter
                    }, delay);
                }
                else {
                    clearTimeout(timer); //prevent single-click action
                    clicks = 0; //after action performed, reset counter
                }
            });
        }
    };
}])
IAI.directive('passwordStrength', [
    function () {
        return {
            require: 'ngModel'
            , restrict: 'E'
            , scope: {
                password: '=ngModel'
            }
            , link: function (scope, elem, attrs, ctrl) {
                scope.$watch('password', function (newVal) {
                    scope.strength = isSatisfied(newVal && newVal.length >= 8) + isSatisfied(newVal && /[A-z]/.test(newVal)) + isSatisfied(newVal && /[A-Z]/.test(newVal)) + isSatisfied(newVal && /(?=.*\W)/.test(newVal)) + isSatisfied(newVal && /\d/.test(newVal));

                    function isSatisfied(criteria) {
                        return criteria ? 1 : 0;
                    }
                }, true);
            }
            , template: '<div class="progress" style="display:{{strength != 0 ? \'block\' : \'none\'}}">' + '<div class="progress-bar progress-bar-danger" style="width: {{strength <= 4 ? 100 : 0}}%"></div>' + '<div class="progress-bar progress-bar-success" style="width: {{strength == 5 ? 100 : 0}}%"></div>' + '</div>'
        }
    }
])
//Directive for Name use as iai-name
IAI.directive('iaiName', function ($timeout) {
    return {
        require: 'ngModel'
        , restrict: 'A'
        , link: function (scope, element, attr, modelCtrl) {
            function fromUser(text) {
                if (text) {
                    var formattedValue = '';
                    //var char = text.charAt(0);
                    //if (!isNaN(char) || char == '_' || char == '-' || char == '.' || char == '=') {
                    //    formattedValue = text.replace(/[^a-zA-Z]/g, '');
                    //}
                    //else {
                    //Alphanumeric, Space
                    formattedValue = text.replace(/[^a-zA-Z0-9 ]/g, '');
                    //}
                    var limit = 100;
                    angular.element(element).on("keypress", function (e) {
                        if (this.value.length == limit) e.preventDefault();
                    });
                    angular.element(element).on("change", function (e) {
                        if (this.value.length > limit) {
                            var res = this.value.substring(0, 100);
                            modelCtrl.$setViewValue(res);
                            modelCtrl.$render();
                        }
                    });
                    angular.element(element).on("paste", function (e) {
                        //$timeout(function () {
                        if (this.value.length > limit) {
                            var res = this.value.substring(0, 100);
                            modelCtrl.$setViewValue(res);
                            modelCtrl.$render();
                        }
                        //});
                    });
                    if (formattedValue !== text) {
                        modelCtrl.$setViewValue(formattedValue);
                        modelCtrl.$render();
                    }
                    return formattedValue;
                }
                return undefined;
            }
            modelCtrl.$parsers.push(fromUser);
        }
    };
});
IAI.directive('iaiName', function () {
    return {
        require: '?ngModel'
        , restrict: 'A'
        , link: function (scope, element, attrs, modelCtrl) {
            if (modelCtrl != null) {
                modelCtrl.$parsers.push(function (inputValue) {
                    if (inputValue == undefined) return ''
                    cleanInputValue = inputValue.replace(/[^\w\s\^`~!@#$%\^&*()_+={}|[\]\\';:."?,/-]/gi, '');
                    if (cleanInputValue != inputValue) {
                        modelCtrl.$setViewValue(cleanInputValue);
                        modelCtrl.$render();
                    }
                    return cleanInputValue;
                });
            }
        }
    }
});
IAI.directive('iaiToLowerCase', function () {
    return {
        require: 'ngModel'
        , restrict: 'A'
        , link: function (scope, element, attr, modelCtrl) {
            function fromUser(text) {
                if (text) {
                    var transformedInput = '';
                    transformedInput = text.toLowerCase();
                    angular.element(element).on("change", function (e) {
                        var res = this.value.toLowerCase();
                        modelCtrl.$setViewValue(res);
                        modelCtrl.$render();
                    });
                    angular.element(element).on("paste", function (e) {
                        var res = this.value.toLowerCase();
                        modelCtrl.$setViewValue(res);
                        modelCtrl.$render();
                    });
                    //if (transformedInput !== text) {
                    modelCtrl.$setViewValue(transformedInput);
                    modelCtrl.$render();
                    //}
                    return transformedInput;
                }
                return undefined;
            }
            modelCtrl.$parsers.push(fromUser);
        }
    };
});
IAI.directive('urlRestriction', function () {
    function link(scope, elem, attrs, ngModel) {
        ngModel.$parsers.push(function (viewValue) {
            var reg = /^[^`~!@#$%\^&*()_+={}|[\]';"<>?,]*$/;
            // if view values matches regexp, update model value
            if (viewValue.match(reg)) {
                return viewValue;
            }
            // keep the model value as it is
            var transformedValue = ngModel.$modelValue;
            ngModel.$setViewValue(transformedValue);
            ngModel.$render();
            return transformedValue;
        });
    }
    return {
        restrict: 'A'
        , require: 'ngModel'
        , link: link
    };
});
IAI.directive('patternValidator', [
    function () {
        return {
            require: 'ngModel'
            , restrict: 'A'
            , link: function (scope, elem, attrs, ctrl) {
                ctrl.$parsers.unshift(function (viewValue) {
                    var patt = new RegExp(attrs.patternValidator);
                    var isValid = patt.test(viewValue);
                    ctrl.$setValidity('passwordPattern', isValid);
                    // angular does this with all validators -> return isValid ? viewValue : undefined;
                    // But it means that the ng-model will have a value of undefined
                    // So just return viewValue!
                    return viewValue;
                });
            }
        };
    }
]);

//MetaData File Upload Service
IAI.service('fileUpload', ['$http', '$rootScope', function ($http, $rootScope) {
    this.uploadFileToUrl = function (file, uploadUrl, $scope) {
        var fd = new FormData();
        fd.append('inFile', file);
        manageLoader('load');
        $http.post(uploadUrl, fd, {
            transformRequest: angular.identity
            , headers: {
                'Content-Type': undefined
            }
        }).success(function (data) {
            attachmentId = data.result;
            $rootScope.resultMessage = 'File uploaded successfully.';
            $scope.consoleMessage = 'No errors found.';
            showNotification('success');
            $scope.fileID = data.result;
            $('#notificationBar .statusText').html($rootScope.resultMessage);
            manageLoader();
        }).error(function (exp) {
            var errorExp = exp;
            $scope.consoleMessage = exp ? exp.errorMessage : 'File upload failed.';
            $rootScope.resultMessage = 'File upload failed.';
            showNotification('error');
            $('#notificationBar .statusText').html($rootScope.resultMessage);
            manageLoader();
        });
    }
        , this.uploadFileDataBazar = function (file, uploadUrl, $scope, objName) {
            var fd = new FormData();
            fd.append('inFile', file);
            $http.post(uploadUrl, fd, {
                transformRequest: angular.identity
                , headers: {
                    'Content-Type': undefined
                }
            }).success(function (data) {
                if (objName == "instanceImage1" || objName == "instanceImage2" || objName == "instanceImage3") {
                    $scope.attachments.push({
                        attachmentId: data.result
                        , attachmentName: file.name
                        , contentType: "2"
                        , body: []
                    });
                }
                else {
                    $scope.sampleFile = [{
                        attachmentId: data.result
                        , attachmentName: file.name
                        , contentType: "1"
                        , body: []
                    }];
                }
            }).error(function (exp) {
                var errorExp = exp;
                $rootScope.resultMessage = 'Cannot upload ' + file.name + '.';
                $('#testconn').show();
            });
        }
}]);

IAI.directive('fileModel', ['$parse', function ($parse) {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            var model = $parse(attrs.fileModel);
            var modelSetter = model.assign;

            element.bind('change', function () {
                scope.$apply(function () {
                    modelSetter(scope, element[0].files[0]);
                });
            });
        }
    };
}]);

IAI.service('fileUpload', ['$http', function ($http) {
    this.uploadFileToUrl = function (file, uploadUrl) {
        var fd = new FormData();
        fd.append('file', file);

        //$http.post(uploadUrl, fd, {
        //    transformRequest: angular.identity,
        //    headers: { 'Content-Type': undefined }
        //})

        //.success(function () {
        //    alert('File uploaded successfully.');
        //})

        //.error(function () {
        //    alert('Error');
        //});

        $http.post(uploadUrl, fd).then(function (response) {

            alert('File uploaded successfully.');

        }, function (response) {
            alert("Error");
            //console.log(response);
        });
    }
}]);

IAI.run(function ($rootScope, $timeout, $document) {
    //console.log('starting run');
    // Timeout timer value
    var TimeOutTimerValue = 60 * 20 * 1000;
    //var TimeOutTimerValue = 10000;
    // Start a timeout
    var TimeOut_Thread = $timeout(function () {
        LogoutByTimer()
    }, TimeOutTimerValue);
    var bodyElement = angular.element($document);
    /// Keyboard Events
    bodyElement.bind('keydown', function (e) {
        TimeOut_Resetter(e)
    });
    bodyElement.bind('keyup', function (e) {
        TimeOut_Resetter(e)
    });
    /// Mouse Events    
    bodyElement.bind('click', function (e) {
        TimeOut_Resetter(e)
    });
    bodyElement.bind('mousemove', function (e) {
        TimeOut_Resetter(e)
    });
    bodyElement.bind('DOMMouseScroll', function (e) {
        TimeOut_Resetter(e)
    });
    bodyElement.bind('mousewheel', function (e) {
        TimeOut_Resetter(e)
    });
    bodyElement.bind('mousedown', function (e) {
        TimeOut_Resetter(e)
    });
    /// Touch Events
    bodyElement.bind('touchstart', function (e) {
        TimeOut_Resetter(e)
    });
    bodyElement.bind('touchmove', function (e) {
        TimeOut_Resetter(e)
    });
    /// Common Events
    bodyElement.bind('scroll', function (e) {
        TimeOut_Resetter(e)
    });
    bodyElement.bind('focus', function (e) {
        TimeOut_Resetter(e)
    });

    function LogoutByTimer() {
        location.href = 'index.html';
    }

    function TimeOut_Resetter(e) {
        //console.log('' + e);
        /// Stop the pending timeout
        $timeout.cancel(TimeOut_Thread);
        /// Reset the timeout
        TimeOut_Thread = $timeout(function () {
            LogoutByTimer()
        }, TimeOutTimerValue);
    }
})