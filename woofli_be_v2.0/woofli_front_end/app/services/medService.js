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
        return $http.get(serviceBase + 'api/medicine/' + id + '/' + med_id).then(function (results) {
            console.log(results);
            return results;
        });
    };

    medServiceFactory.getPetMeds = _getPetMeds;
    medServiceFactory.getSinglePetMed = _getSinglePetMed;

    return medServiceFactory;
}]);