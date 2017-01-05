'use strict';
app.factory('medService', ['$http', function ($http) {

    var serviceBase = 'http://localhost:52380/';
    var medServiceFactory = {};

    var _getPetMeds = function (id) {
        return $http.get(serviceBase + 'api/medicine/' + id).then(function (results) {
            console.log(results);
            return results;
        });
    };

    var _getSinglePetMed = function (pet_id, med_id) {
        return $http.get(serviceBase + 'api/medicine/' + pet_id + '/' + med_id).then(function (results) {
            console.log(results);
            return results;
        });
    };

    var _addMedToPet = function (med) {
        var data = med;
        $http.post(serviceBase + 'api/medicine/', data).then(function (results) {
            console.log(results);
            return results;
        });
    };

    var _removeMedFromPet = function (med_id) {
        return $http.delete(serviceBase + 'api/medicine/' + med_id).then(function (results) {
            return results;
        });
    };

    medServiceFactory.addMedToPet = _addMedToPet;
    medServiceFactory.getPetMeds = _getPetMeds;
    medServiceFactory.getSinglePetMed = _getSinglePetMed;
    medServiceFactory.removeMedFromPet = _removeMedFromPet;

    return medServiceFactory;
}]);