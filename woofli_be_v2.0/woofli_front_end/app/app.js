var app = angular.module("App", ['ngRoute', 'LocalStorageModule']);

app.config(function ($routeProvider, $locationProvider) {

    $routeProvider.when("/", {
        controller: "homeController",
        templateUrl: "/app/partials/home.html"
    }).when("/login", {
        controller: "loginController",
        templateUrl: "/app/partials/login.html"
    }).when("/signup", {
        controller: "signupController",
        templateUrl: "/app/partials/signup.html"
    }).when("/account", {
        controller: "accountController",
        templateUrl: "/app/partials/account-main.html"
    }).when("/add-pet", {
        controller: "accountController",
        templateUrl: "/app/partials/add-pet.html"
    }).when("/add-petsitter", {
        controller: "accountController",
        templateUrl: "/app/partials/add-petsitter.html"
    }).when("/add-vet", {
        controller: "accountController",
        templateUrl: "/app/partials/add-vet.html"
    }).when("/add-med", {
        controller: "accountController",
        templateUrl: "/app/partials/add-medication.html"
    }).when('/pet/:id', {
        controller: "accountController",
        templateUrl: "/app/partials/pet.html"
    }).when('/petsitter/:id', {
        controller: "accountController",
        templateUrl: "/app/partials/petsitter.html"
    }).when('/vet/:id', {
        controller: "accountController",
        templateUrl: "/app/partials/vet.html"
    }).when('/med/:id', {
        controller: "accountController",
        templateUrl: "/app/partials/med.html"
    }).otherwise({ redirectTo: "/" });

    $locationProvider.html5Mode(true);
});

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

app.run(['authService', function (authService) {
    authService.fillAuthData();
}]);