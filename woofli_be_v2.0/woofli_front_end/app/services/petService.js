'use strict';
app.factory('petService', ['$http', function ($http) {

    var serviceBase = 'http://localhost:52380/';
    var petServiceFactory = {};

    var _getPets = function () {

        return $http.get(serviceBase + 'api/pet').then(function (results) {
            return results;
        });
    };

    petServiceFactory.getPets = _getPets;

    return petServiceFactory;

}]);