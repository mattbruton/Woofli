'use strict';
app.factory('PetFactory', ['$http', function ($http) {
  const serviceBase = 'http://localhost:52380/';
  const getPets = () => {
    return $http.get(serviceBase + 'api/pet')
    .then((results) => {
        return results;
    });
  };

  const getSinglePet = (id) => {
    return $http.get(serviceBase + 'api/pet/' + id)
    .then((results) => {
      return results;
    });
  };

  const addNewPet = (pet) => {
    let data = pet;
    if (data.IsCanine)
    {
      data.ImageUrl = 'content/images/barking-dog-512.png';
    }
    else {
      data.ImageUrl = 'content/images/cat-icon.png';
    }
    $http.post(serviceBase + 'api/pet', data)
    .then((results) => {
        console.log(results);
    });
  };

  const removeSinglePet = (id) => {
    return $http.delete(serviceBase + 'api/pet/' + id)
    .then((results) => {
      return results;
    });
  };

  return {
    getPets,
    getSinglePet,
    removeSinglePet,
    addNewPet
  };
}]);
