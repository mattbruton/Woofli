'use strict';
app.controller('AccountController', ['$scope', '$location', 'AuthFactory', 'MedFactory', 'VetFactory', 'PetFactory', 'PetsitterFactory', '$rootScope', function ($scope, $location, AuthFactory, MedFactory, VetFactory, PetFactory, PetsitterFactory, $rootScope) {
  $scope.userPets = [];
  $scope.userPetsitters = [];

  $scope.targetPet = {};
  // $scope.targetPetsitter = {};
  $scope.targetVet = {};

  $scope.newMed = {};

  // Wonky, hacky date stuff

  $scope.medDate = new Date();
  $scope.medDateParams = {};

  $scope.configureTime = () => {
    if ($scope.medDateParams.Meridiem === 'pm' && $scope.medDateParams.Hour !== 12) {
      $scope.medDateParams.Hour = parseInt($scope.medDateParams.Hour) + 12;
      $scope.medDate.setHours($scope.medDateParams.Hour);
    } else if ($scope.medDateParams.Meridiem === 'am' && $scope.medDateParams.Hour === 12) {
      $scope.medDateParams.Hour = 0;
      $scope.medDate.setHours($scope.medDateParams.Hour);
    } else {
      $scope.medDate.setHours($scope.medDateParams.Hour);
    }
    $scope.medDate.setMinutes($scope.medDateParams.Minute);
    $scope.medDate.setMonth($scope.medDateParams.Month - 1);
    $scope.medDate.setDate($scope.medDateParams.Day);
    $scope.medDate.setFullYear($scope.medDateParams.Year);

    $scope.newMed.DosageTime = $scope.medDate.toISOString();
  };

  $scope.makeTimeReadable = (timeISOString) => {
    let newDate = new Date(timeISOString).toGMTString();
    return newDate;
  };

  // End date related mess.

  $scope.newPet = {};
  $scope.newVet = {};
  $scope.newPetsitter = {};

  $scope.medId = $rootScope.medId;
  $scope.id = $rootScope.id;

  $scope.backToAccount = () => {
    $location.path('/account');
  };

  $scope.goToNewPetsitterView = () => {
    $location.path('/add-petsitter');
  };

  $scope.goToNewVetView = () => {
    $location.path('/add-vet');
  };

  $scope.goToNewPetView = () => {
    $location.path('/add-pet');
  };

  $scope.goToNewMedView = () => {
    $location.path('/add-med');
  };

  $scope.goToTargetPetView = (id) => {
    $rootScope.id = id;
    $location.path(`/pet/${id}`);
  };

  $scope.goToTargetVetView = (id) => {
    $rootScope.id = id;
    $location.path(`/vet/${id}`);
  };

  $scope.goToTargetMedView = (medId) => {
    $rootScope.medId = medId;
    $location.path(`/med/${medId}`);
  };

  $scope.goToTargetPetsitterView = (id) => {
    $location.path(`/petsitter/${id}`);
  };

  $scope.viewTargetPet = (id) => {
    PetFactory.getSinglePet(id)
    .then((results) => {
      $scope.targetPet = results.data;
      $scope.viewTargetVet($scope.id);
    });
  };

  $scope.viewTargetMed = (medId, petId) => {
    MedFactory.getSinglePetMed(petId, medId)
    .then((results) => {
      $scope.targetMed = results.data;
    });
  };

  $scope.viewTargetVet = (id) => {
    VetFactory.getPrimaryVet(id)
    .then((results) => {
      $scope.targetVet = results.data;
    });
  };

  // $scope.viewTargetPetsitter = function (id) {
  //   PetsitterFactory.getSinglePetsitter(id)
  //   .then(function (results) {
  //     $scope.targetPetsitter = results.data;
  //   });
  // };

  // $scope.removePetsitter = (id) => {
  //   PetsitterFactory.removeSinglePetsitter(id)
  //   .then((results) => {
  //     $scope.updatePetsitterList();
  //     $location.path('/account');
  //   });
  // };

  $scope.removeMed = (medId) => {
    MedFactory.removeMedFromPet(medId)
      .then(() => {
        $scope.updatePetList();
        $location.path(`/pet/{{$rootScope.id}}`);
      });
  };

  $scope.removePet = (id) => {
    PetFactory.removeSinglePet(id)
    .then((results) => {
      $scope.updatePetList();
      $location.path('/account');
    });
  };

  $scope.removeVet = (id) => {
    VetFactory.removeVetFromPet(id)
    .then((results) => {
      $location.path(`/pet/${id}`);
    });
  };

  $scope.addNewVet = (newVet) => {
    $scope.newVet.PetId = $rootScope.id;
    VetFactory.addPrimaryVet($scope.newVet, $rootScope.id);
    $scope.updatePetList();
    $location.path(`/pet/{{$rootScope.id}}`);
  };

  $scope.addNewMed = (newMed) => {
    $scope.newMed.PetId = $rootScope.id;
    MedFactory.addMedToPet(newMed);
    $scope.updatePetList();
    $location.path(`/pet/{{$rootScope.id}}`);
  };

  $scope.addNewPetsitter = (newPetsitter) => {
    PetsitterFactory.addNewPetsitter($scope.newPetsitter);
    $scope.updatePetsitterList();
    $location.path('/account');
  };

  $scope.addNewPet = (newPet) => {
    PetFactory.addNewPet($scope.newPet);
    $scope.updatePetList();
    $location.path('/account');
  };

  $scope.updatePetsitterList = () => {
    PetsitterFactory.getPetsitters()
    .then((results) => {
      $scope.userPetsitters.length = 0;
      $scope.userPetsitters = results.data;
    }, (error) => {
      console.log(error);
    });
  };

  $scope.updatePetList = () => {
    PetFactory.getPets()
    .then((results) => {
      $scope.userPets.length = 0;
      $scope.userPets = results.data;
    }, (error) => {
      console.log(error);
    });
  };

  $scope.updatePetsitterList();
  $scope.updatePetList();
}]);
