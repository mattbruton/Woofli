'use strict';
app.factory('vetService', ['$http', function ($http) {

    var serviceBase = 'http://localhost:52380/';
    var vetServiceFactory = {};

    var _getPrimaryVet = function (id) {
        return $http.get(serviceBase + 'api/veterinarian/' + id).then(function (results) {
            console.log(results);
            return results;
            
        });
    };

    var _addPrimaryVet = function (vet, pet_id) {
        var data = vet;
        $http.post(serviceBase + 'api/veterinarian/' + pet_id, data).then(function (results) {
            return(results);
        });
    };

    var _removeVetFromPet = function (pet_id) {
        return $http.delete(serviceBase + 'api/veterinarian/' + pet_id).then(function (results) {
            return results;
        });
    };

    vetServiceFactory.getPrimaryVet = _getPrimaryVet;
    vetServiceFactory.addPrimaryVet = _addPrimaryVet;
    vetServiceFactory.removeVetFromPet = _removeVetFromPet;

    return vetServiceFactory;
}]);