'use strict';
app.controller('accountController', ['$scope', '$location', 'authService', 'petService', 'petsitterService', function ($scope, $location, authService, petService, petsitterService) {

    $scope.userPets = [];
    $scope.userPetsitters = [];
    $scope.newPet = {};
    $scope.newPetsitter = {};

    $scope.goToNewPetsitterView = function () {
        $location.path("/add-petsitter");
    };

    $scope.goToNewPetView = function () {
        $location.path("/add-pet");
    };

    $scope.addNewPetsitter = function (newPetsitter) {
        petsitterService.addNewPetsitter($scope.newPetsitter);
        $location.path('/account');
    };

    $scope.addNewPet = function (newPet) {
        petService.addNewPet($scope.newPet);
        $location.path('/account');
    };

    petsitterService.getPetsitters().then(function (results) {
        $scope.userPetsitters = angular.copy(results.data);
    }, function (error) {
        console.log(error);
    });

    petService.getPets().then(function (results) {
        $scope.userPets = angular.copy(results.data);
    }, function (error) {
        console.log(error);
    });

}]);