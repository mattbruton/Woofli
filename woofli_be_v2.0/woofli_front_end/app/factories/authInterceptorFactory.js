'use strict';
app.factory('AuthInterceptorFactory', ['$q', '$location', 'localStorageService', function ($q, $location, localStorageService) {
  const request = (config) => {
    config.headers = config.headers || {};
    var authData = localStorageService.get('authorizationData');
    if (authData) {
        config.headers.Authorization = 'Bearer ' + authData.token;
    }
    return config;
  };

  const responseError = (rejection) => {
    if (rejection.status === 401) {
      $location.path('/login');
    }
    return $q.reject(rejection);
  };

  return {
    request,
    responseError
  };
}]);
