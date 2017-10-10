var uriShortingController = function($rootScope, $scope, $http, $cookies, $state, $window, $location, shortedUriService) {

    $scope.intro = "Just paste url here and press button to short it!";

    $scope.postUriToShort = function(sourceUrl) {
        console.log("uri: " + sourceUrl);

        shortedUriService.postUrl(sourceUrl).then(onShorteningComplete, onError);
    };

    var onShorteningComplete = function(response) {
        $scope.shortedUrl = response.data.ShortUri;
        $scope.status = response.status;

    };

    $scope.getShortedUriList = function() {
        shortedUriService.getUriList().then(onGettingListComplete, onError);
    };

    var onGettingListComplete = function(response) {
        $scope.shortedUriList = response.data;
        $scope.status = response.status;

    };

    var onError = function(response) {
        $scope.error = "Ooops, something went wrong.." + "Http status code: " + response.Status;
    };

    $scope.goToState= function(path){
        $state.go(path)
    };

    $rootScope.$on('$stateChangeStart',
        function(event, toState, toParams, fromState, fromParams){
            if(toState.name === 'go') {

                var key = $location.url().substr(4);

                shortedUriService.updateTransferCount(key);

                shortedUriService.getUriByKey(key).then(onGetiingByKeyComplete, onError);
            }

            if(toState.name === 'history') {

                shortedUriService.getUriList().then(onGettingListComplete, onError);
            }
        });

    var onGetiingByKeyComplete = function(response) {

        var toUri = response.data.SourceUri;

        if(toUri === undefined)
        {
            $scope.error = "bad Uri..."
            return;
        }

        $window.location.href = toUri;

    };
};

app.controller("uriShortingController", uriShortingController);