app.factory('shortedUriService', function($http, $cookies) {
    return {
        postUrl: function(sourceUrl) {

            console.log('post url for shortening: ' + sourceUrl);
            var sessionId = getSessionId();
            console.log("sessionId: " + sessionId);

            var req = {
                method: 'POST',
                url: baseUrl() + 'uri',
                headers: getApiHeaders(),
                data: { Uri : sourceUrl, CreatedById : sessionId }
            };

            return $http(req).then(
                    function(response) {
                        return response;
                    },
                    // error handler
                    function(response) {
                        return response;
                    });
        },

        getUriList: function() {

            var sessionId = getSessionId();
            console.log('get shorted uri list for sessionId: ' + sessionId);

            var req = {
                method: 'GET',
                url: baseUrl() + 'uri/list?createdById=' + sessionId,
                headers: getApiHeaders()
            };

            return $http(req).then(
                function(response) {
                    return response;
                },
                // error handler
                function(response) {
                    return response;
                });
        },

        getUriByKey: function(key) {

            console.log('get shorted uri by key: ' + key);

            var req = {
                method: 'GET',
                url: baseUrl() + 'uri/' + key,
                headers: getApiHeaders()
            };

            return $http(req).then(
                function(response) {
                    return response;
                },
                // error handler
                function(response) {
                    return response;
                });
        }
    };

    function getSessionId() {
        var sessionId = $cookies.getObject('sessionId');
        if(sessionId === undefined)
        {
            sessionId = guid();
            $cookies.putObject('sessionId', sessionId);
        }

        return sessionId;
    }
});

function guid() {
    function s4() {
        return Math.floor((1 + Math.random()) * 0x10000)
            .toString(16)
            .substring(1);
    }
    return s4() + s4() + '-' + s4() + '-' + s4() + '-' +
        s4() + '-' + s4() + s4() + s4();
}

function baseUrl() {
    return 'http://ec2-13-59-130-6.us-east-2.compute.amazonaws.com:81/';
}

function getApiHeaders() {
    return {
        'Content-Type': 'application/json',
        'Authorization': 'Basic dGVzdFVzZXI6cGFzc3dvcmQ='
    };
}

