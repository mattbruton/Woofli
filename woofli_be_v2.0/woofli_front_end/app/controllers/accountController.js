'use strict';
app.controller('accountController', ['$scope', '$location', 'authService', 'petService', 'petsitterService', function ($scope, $location, authService, petService, petsitterService) {

    $scope.userPets = [];
    $scope.userPetsitters = [];
    $scope.newPet = {};
    $scope.newPetsitter = {};

    $scope.goToNewPetsitterView = function () {

        $location.path("/add-petsitter");
    };

    $scope.addNewPet = function () {

        $location.path("/add-pet");
    };

    $scope.addNewPetsitter = function (newPetsitter) {
        petsitterService.addNewPetsitter($scope.newPetsitter);
        $location.path('/account');
    };

    petService.getPets().then(function (results) {
        $scope.userPets = angular.copy(results.data);

    }, function (error) {
        console.log(error);
    });

}]);