var MyFirstController = function($scope, $http, $cookies, userData, userGravatar, gitHubUserLookup, postUrlForShortening) {
    $scope.ManyHellos = ['Hello', 'Hola', 'Bonjour', 'Guten Tag', 'Ciao', 'Namaste', 'Yiasou'];

    $scope.data = userData.user;

    $scope.getGravatar = function(email) {
        return userGravatar.getGravatar(email);
    };

    $scope.getGitHubUser = function(username) {
        console.log("username: " + username);
        gitHubUserLookup.lookupUser(username).then(onLookupComplete, onError);
    };

    $scope.postUriToShort = function(sourceUrl) {
        console.log("uri: " + sourceUrl);
        postUrlForShortening.postUrl(sourceUrl).then(onLookupComplete, onError);
    };

    var onLookupComplete = function(response) {
        $scope.shortedResponse = response.data;
        $scope.status = response.status;

    };

    var onError = function(reason) {
        $scope.error = "Ooops, something went wrong..";
    };
};

app.controller("MyFirstController", MyFirstController);