'use strict';
app.controller('accountController', ['$scope', '$location', 'authService', 'medService', 'vetService', 'petService', 'petsitterService', '$rootScope', function ($scope, $location, authService, medService, vetService, petService, petsitterService, $rootScope) {

    $scope.userPets = [];
    $scope.userPetsitters = [];

    $scope.targetPet = {};
    $scope.targetPetsitter = {};
    $scope.targetVet = {};

    $scope.newMed = {};
    $scope.newPet = {};
    $scope.newVet = {};
    $scope.newPetsitter = {};

    $scope.medId = $rootScope.medId;
    $scope.id = $rootScope.id;

    $scope.backToAccount = function () {
        $location.path('/account');
    };

    $scope.goToNewPetsitterView = function () {
        $location.path("/add-petsitter");
    };

    $scope.goToNewVetView = function () {
        $location.path("/add-vet");
    };

    $scope.goToNewPetView = function () {
        $location.path("/add-pet");
    };

    $scope.goToNewMedView = function () {
        $location.path("/add-med");
    };

    $scope.goToTargetPetView = function (id) {
        $rootScope.id = id;
        $location.path(`/pet/${id}`);
    };

    $scope.goToTargetVetView = function (id) {
        $rootScope.id = id;
        $location.path(`/vet/${id}`);
    };

    $scope.goToTargetMedView = function (medId) {
        $rootScope.medId = medId;
        $location.path(`/med/${medId}`);
    };

    $scope.goToTargetPetsitterView = function (id) {
        $rootScope.id = id;
        $location.path(`/petsitter/${id}`);
    };

    $scope.viewTargetPet = function (id) {
        petService.getSinglePet(id).then(function (results) {
            $scope.targetPet = results.data;
            $scope.viewTargetVet($scope.id);
        });
    };

    $scope.viewTargetMed = function (medId, petId) {
        medService.getSinglePetMed(petId, medId).then(function (results) {
            $scope.targetMed = results.data;
            console.log($scope.targetMed);
        });
    };

    $scope.viewTargetVet = function (id) {
        vetService.getPrimaryVet(id).then(function (results) {
            $scope.targetVet = results.data;
            console.log($scope.targetVet);
        });
    };

    $scope.viewTargetPetsitter = function (id) {
        petsitterService.getSinglePetsitter(id).then(function (results) {
            $scope.targetPetsitter = results.data;
        });
    };

    $scope.removePetsitter = function (id) {
        petsitterService.removeSinglePetsitter(id).then(function (results) {
            $scope.updatePetsitterList();
            $location.path('/account');
        });
    };

    $scope.removeMed = function (med_id) {
        medService.removeMedFromPet(med_id).then(function () {
            $scope.updatePetList();
            $location.path(`/pet/{{$rootScope.id}}`);
        });
    };

    $scope.removePet = function (id) {
        petService.removeSinglePet(id).then(function (results) {
            $scope.updatePetList();
            $location.path('/account');
        });
    };

    $scope.removeVet = function (id) {
        vetService.removeVetFromPet(id).then(function (results) {
            $location.path(`/pet/${id}`);
        });
    };

    $scope.addNewVet = function (newVet) {
        $scope.newVet.PetId = $rootScope.id;
        vetService.addPrimaryVet($scope.newVet, $rootScope.id);
        $scope.updatePetList();
        $location.path(`/pet/{{$rootScope.id}}`);
    };

    $scope.addNewMed = function (newMed) {
        $scope.newMed.PetId = $rootScope.id;
        medService.addMedToPet(newMed);
        $scope.updatePetList();
        $location.path(`/pet/{{$rootScope.id}}`);
    };

    $scope.addNewPetsitter = function (newPetsitter) {
        petsitterService.addNewPetsitter($scope.newPetsitter);
        $scope.updatePetsitterList();
        $location.path('/account');
    };

    $scope.addNewPet = function (newPet) {
        petService.addNewPet($scope.newPet);
        $scope.updatePetList();
        $location.path('/account');
    }; 

    $scope.updatePetsitterList = function () {
        petsitterService.getPetsitters().then(function (results) {
            $scope.userPetsitters.length = 0;
            $scope.userPetsitters = results.data;
        }, function (error) {
            console.log(error);
        });
    };

    $scope.updatePetList = function () {
        petService.getPets().then(function (results) {
            $scope.userPets.length = 0;
            $scope.userPets = results.data;
        }, function (error) {
            console.log(error);
        });
    };

    $scope.updatePetsitterList();
    $scope.updatePetList();
}]);