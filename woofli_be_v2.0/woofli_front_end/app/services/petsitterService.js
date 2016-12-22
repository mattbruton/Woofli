'use strict';
app.factory('petsitterService', ['$http', function ($http) {

    var serviceBase = 'http://localhost:52380/';
    var petsitterServiceFactory = {};

    var _getPetsitters = function () {

        return $http.get(serviceBase + 'api/petsitter').then(function (results) {
            return results;
        });
    };

    petServiceFactory.getPetsitters = _getPetsitters;

    return petsitterServiceFactory;

}]);