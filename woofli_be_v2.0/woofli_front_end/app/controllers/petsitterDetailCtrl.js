'use strict';
app.controller('PetsitterDetailCtrl', ['$scope', '$routeParams', 'PetsitterFactory', '$location', function ($scope, $routeParams, PetsitterFactory, $location) {
  $scope.getPetsitter = (id) => {
    PetsitterFactory.getSinglePetsitter(id)
    .then((sitter) => {
      $scope.petsitter = sitter;
    });
  };

  $scope.removePetsitter = (id) => {
    PetsitterFactory.removeSinglePetsitter(id)
    .then((result) => {
      console.log(`sitter removed!`, result);
    });
  };

  $scope.backToAccount = () => {
    $location.path('/account');
  };

  $scope.getPetsitter($routeParams.id);
}]);
