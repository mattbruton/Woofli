'use strict';
app.factory('VetFactory', ['$http', function ($http) {
  const serviceBase = 'http://localhost:52380/';

  const getPrimaryVet = (id) => {
    return $http.get(serviceBase + 'api/veterinarian/' + id)
    .then((results) => {
      return results;
    });
  };

  const addPrimaryVet = (vet, petId) => {
    var data = vet;
    $http.post(serviceBase + 'api/veterinarian/' + petId, data)
    .then((results) => {
      return results;
    });
  };

  const removeVetFromPet = (petId) => {
    return $http.delete(serviceBase + 'api/veterinarian/' + petId)
    .then((results) => {
      return results;
    });
  };

  return {
    getPrimaryVet,
    addPrimaryVet,
    removeVetFromPet
  };
}]);
