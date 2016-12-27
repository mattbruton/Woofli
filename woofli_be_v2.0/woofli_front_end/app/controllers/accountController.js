﻿'use strict';
app.controller('accountController', ['$scope', '$location', 'authService', 'petService', 'petsitterService', '$rootScope', function ($scope, $location, authService, petService, petsitterService, $rootScope) {

    $scope.userPets = [];
    $scope.userPetsitters = [];

    $scope.targetPet = {};
    $scope.targetPetsitter = {};

    $scope.newPet = {};
    $scope.newPetsitter = {};

    $scope.id = $rootScope.id;

    $scope.backToAccount = function () {
        $location.path('/account');
    };

    $scope.goToNewPetsitterView = function () {
        $location.path("/add-petsitter");
    };

    $scope.goToNewPetView = function () {
        $location.path("/add-pet");
    };

    $scope.goToTargetPetView = function (id) {
        $rootScope.id = id;
        $location.path(`/pet/${id}`);
    }

    $scope.goToTargetPetsitterView = function (id) {
        $rootScope.id = id;
        $location.path(`/petsitter/${id}`);
    }

    $scope.viewTargetPet = function (id) {
        petService.getSinglePet(id).then(function (results) {
            $scope.targetPet = results.data;
            console.log($scope.targetPet);
        });
    };

    $scope.viewTargetPetsitter = function (id) {
        petsitterService.getSinglePetsitter(id).then(function (results) {
            $scope.targetPetsitter = results.data;
            console.log($scope.targetPetsitter);
        });
    };

    $scope.removePetsitter = function (id) {
        petsitterService.removeSinglePetsitter(id).then(function (results) {
            console.log(results);
            $scope.updatePetsitterList();
            $location.path('/account');
        });
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
            console.log(results.data);
        }, function (error) {
            console.log(error);
        });
    };

    $scope.updatePetList = function () {
        petService.getPets().then(function (results) {
            $scope.userPets.length = 0;
            $scope.userPets = results.data;
            console.log(results.data);
        }, function (error) {
            console.log(error);
        });
    };

    $scope.updatePetsitterList();
    $scope.updatePetList();
}]);