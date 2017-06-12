'use strict';
app.factory('PetsitterFactory', ['$http', function ($http) {
  const serviceBase = 'http://localhost:52380/';

  const getPetsitters = () => {
    return $http.get(serviceBase + 'api/petsitter')
    .then((results) => {
      return results;
    });
  };

  const getSinglePetsitter = (id) => {
    return $http.get(serviceBase + 'api/petsitter/' + id)
    .then((results) => {
      return results;
    });
  };

  const addNewPetsitter = (petsitter) => {
    let data = petsitter;
    $http.post(serviceBase + 'api/petsitter', data)
    .then((results) => {
      console.log(results);
    });
  };

  const removeSinglePetsitter = (id) => {
    return $http.delete(serviceBase + 'api/petsitter/' + id)
    .then((results) => {
      return results;
    });
  };

  return {
    getPetsitters,
    getSinglePetsitter,
    removeSinglePetsitter,
    addNewPetsitter
  };
}]);
