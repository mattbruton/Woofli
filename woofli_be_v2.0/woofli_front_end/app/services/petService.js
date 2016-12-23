'use strict';
app.factory('petService', ['$http', function ($http) {

    var serviceBase = 'http://localhost:52380/';
    var petServiceFactory = {};

    var _getPets = function () {

        return $http.get(serviceBase + 'api/pet').then(function (results) {
            return results;
        });
    };

    var _addNewPet = function (pet) {
        var data = pet;
        $http.post(serviceBase + 'api/pet', data).then(function (results) {
            console.log(results);
        });
    };

    petServiceFactory.getPets = _getPets;
    petServiceFactory.addNewPet = _addNewPet;

    return petServiceFactory;

}]);