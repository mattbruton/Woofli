'use strict';

app.factory('MedFactory', ['$http', function ($http) {
  const serviceBase = 'http://localhost:52380/';

  const getPetMeds = (id) => {
    return $http.get(serviceBase + 'api/medicine/' + id)
    .then((results) => {
      console.log(results);
      return results;
    });
  };

  const getSinglePetMed = (petId, medId) => {
    return $http.get(serviceBase + 'api/medicine/' + petId + '/' + medId)
    .then((results) => {
      console.log(results);
      return results;
    });
  };

  const addMedToPet = (med) => {
    var data = med;
    $http.post(serviceBase + 'api/medicine/', data)
    .then((results) => {
      console.log(results);
      return results;
    });
  };

  const removeMedFromPet = (medId) => {
    return $http.delete(serviceBase + 'api/medicine/' + medId)
    .then((results) => {
      return results;
    });
  };

  return {
    addMedToPet,
    getPetMeds,
    getSinglePetMed,
    removeMedFromPet
  };
}]);
