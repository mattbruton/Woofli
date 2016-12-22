'use strict';
app.controller('accountController', ['$scope', '$location', 'authService', 'petService', function ($scope, $location, authService, petService) {

    $scope.userPets = [];
    $scope.userPetsitters = [];

    $scope.addNewPetsitter = function () {

        $location.path = "/add-petsitter";
    };

    $scope.addNewPet = function () {

        $location.path = "/add-pet";
    };

    petService.getPets().then(function (results) {
        $scope.userPets = angular.copy(results.data);

    }, function (error) {
        console.log(error);
    });

}]);