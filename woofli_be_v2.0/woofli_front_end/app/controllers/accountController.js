'use strict';
app.controller('accountController', ['$scope', '$location', 'authService', 'petService', function ($scope, $location, authService, petService) {

    $scope.userPets = petService.getPets();

    console.log($scope.userPets);

}]);