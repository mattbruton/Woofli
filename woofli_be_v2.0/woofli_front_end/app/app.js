var app = angular.module("App", ['ngRoute', 'LocalStorageModule']);

app.config(function ($routeProvider, $locationProvider) {

    $routeProvider.when("/", {
        controller: "homeController",
        templateUrl: "/app/partials/home.html"
    });

    $routeProvider.when("/login", {
        controller: "loginController",
        templateUrl: "/app/partials/login.html"
    });

    $routeProvider.when("/signup", {
        controller: "signupController",
        templateUrl: "/app/partials/signup.html"
    });

    $routeProvider.when("/account", {
        controller: "accountController",
        templateUrl: "/app/partials/account-main.html"
    });

    $routeProvider.when("/add-pet", {
        controller: "accountController",
        templateUrl: "/app/partials/add-pet.html"
    });

    $routeProvider.when("/add-petsitter", {
        controller: "accountController",
        templateUrl: "/app/partials/add-petsitter.html"
    });

    $routeProvider.otherwise({ redirectTo: "/" });

    $locationProvider.html5Mode(true);
});

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

app.run(['authService', function (authService) {
    authService.fillAuthData();
}]);