'use strict';
app.factory('AuthFactory', ['$http', '$q', 'localStorageService', function ($http, $q, localStorageService) {
  const serviceBase = 'http://localhost:52380/';
  const authentication = {
    isAuth: false,
    userName: ''
  };

  const saveRegistration = (registration) => {
    logOut();
    return $http.post(serviceBase + 'api/account/register', registration)
    .then((response) => {
      return response;
    });
  };

  const login = (loginData) => {
    let data = 'grant_type=password&username=' + loginData.userName + '&password=' + loginData.password;
    let deferred = $q.defer();

    $http.post(serviceBase + 'token', data, {
      headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
    })
    .then((response) => {
      localStorageService.set('authorizationData', {
        token: response.data.access_token,
        userName: loginData.userName
      });
      authentication.isAuth = true;
      authentication.userName = loginData.userName;

      deferred.resolve(response);
    })
    .catch((err, status) => {
      logOut();
      deferred.reject(err);
    });

    return deferred.promise;
  };

  const logOut = () => {
    localStorageService.remove('authorizationData');
    authentication.isAuth = false;
    authentication.userName = '';
  };

  const fillAuthData = () => {
    let authData = localStorageService.get('authorizationData');
    if (authData) {
      authentication.isAuth = true;
      authentication.userName = authData.userName;
    }
  };

  return {
    saveRegistration,
    login,
    logOut,
    fillAuthData,
    authentication
  };
}]);
