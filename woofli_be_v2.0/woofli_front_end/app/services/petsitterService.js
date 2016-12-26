'use strict';
app.factory('petsitterService', ['$http', function ($http) {

    var serviceBase = 'http://localhost:52380/';
    var petsitterServiceFactory = {};

    var _getPetsitters = function () {

        return $http.get(serviceBase + 'api/petsitter').then(function(results) {
            return results;
        });
    };

    var _getSinglePetsitter = function (id) {
        return $http.get(serviceBase + 'api/petsitter/' + id).then(function(results) {
            return results;
        });
    };

    var _addNewPetsitter = function(petsitter) {
        var data = petsitter;
        $http.post(serviceBase + 'api/petsitter', data).then(function (results) {
            console.log(results);
        });
    };

    petsitterServiceFactory.getPetsitters = _getPetsitters;
    petsitterServiceFactory.getSinglePetsitter = _getSinglePetsitter;
    petsitterServiceFactory.addNewPetsitter = _addNewPetsitter;

    return petsitterServiceFactory;

}]);