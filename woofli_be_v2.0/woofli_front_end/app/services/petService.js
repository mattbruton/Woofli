'use strict';
app.factory('petService', ['$http', function ($http) {

    var serviceBase = 'http://localhost:52380/';
    var petServiceFactory = {};

    var _getPets = function () {

        return $http.get(serviceBase + 'api/pet').then(function (results) {
            return results;
        });
    };

    var _getSinglePet = function (id) {
        return $http.get(serviceBase + 'api/pet/' + id).then(function (results) {
            return results;
        });
    };

    var _addNewPet = function (pet) {
        var data = pet;
        if (data.IsCanine)
        {
            data.ImageUrl = "content/images/barking-dog-512.png";
        }
        else {
            data.ImageUrl = "content/images/cat-icon.png";
        }
        $http.post(serviceBase + 'api/pet', data).then(function (results) {
            console.log(results);
        });
    };

    petServiceFactory.getPets = _getPets;
    petServiceFactory.getSinglePet = _getSinglePet;
    petServiceFactory.addNewPet = _addNewPet;

    return petServiceFactory;
}]);