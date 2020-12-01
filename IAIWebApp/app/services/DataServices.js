(function () {
    "use strict";

    angular
        .module("data", [])
        .service("DataServices", DataServices);

    DataServices.$inject = ['$q', '$http', '$log'];

    function DataServices($q, $http, $log) {

        //$http.defaults.headers.post["Content-Type"] = "application/x-www-form-urlencoded";

        /* 
         * Public Methods Accessible to the client via 'DataService' service
         *
    * @ngdoc: Exposed Module Reveal Pattern. Provides abstracted methods to clients
    * 
    * @ngmethod : get();
    * @name DataService#get()
    * @returns {promise (DO)} Returns a promise based on response data

    * @ngmethod : post();
    * @returns {promise (DO)} Returns a promise based on response data
    */
        return {
            get: get,
            post: post
        };

        /*
         * Private Method
         * @ngmethod: success 
         * @returns {object} Success with http request
        */
        function success(resp) {
            //$log.info("Call successful");
            //$log.info(JSON.stringify(resp));
            //hide loader
            return resp.data;
        }

        /*
         * Private Method
         * @ngmethod: failure 
         * @returns {object} Failure with http request
        */
        function failure(error) {
            $log.error("request failed! " + JSON.stringify(error.data));
            //hide loader
            return error;
        }

        /*
         * Public Method
        */
        function get(address, qParams, shouldCache) {
            //show loader
            var config = {
                method: 'GET',
                url: address,
                headers: {
                    'Access-Control-Allow-Origin': '*',
                    'Access-Control-Allow-Methods': 'GET, POST, PUT, DELETE, OPTIONS',
                    'Access-Control-Allow-Headers': 'Content-Type, X-Requested-With',
                    'X-Random-Shit': '123123123'
                },
                params: qParams || null,
                cache: shouldCache || false
            };
            return $http.get(address, qParams).then(success, failure);
        }

        function post(address, data, params) {
            //show loader
            var config = {
                method: 'POST',
                url: address,
                data: JSON.stringify(data),
                headers: {
                    'Content-Type': 'application/json'
                },
                params: null,
                cache: false
            };
            return $http.post(address, data, params).then(success, failure);
        }

    }
})();