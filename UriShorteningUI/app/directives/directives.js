app.directive('myDirectiveToUriShorting', function() {
    return {
        restrict: 'E',
        template: "<div><label>{{intro}}</label></div>" +
        "<label>Uri:<input type='text' ng-model='sourceUrl'></label>" +
        "<button ng-click='postUriToShort(sourceUrl)'>Create Short Uri</button>" +
        "<div><label>Short uri will he here:</label></div>" +
        "<pre>{{shortedUrl}}</pre>"
    };
});

app.directive('myDirectiveToGetShortedList', function() {
    return {
        restrict: 'E',
        template: "<ol ng-repeat=\"uri in shortedUriList\" style='ordere'>" +
        "<div><div>SourceUrl: {{uri.SourceUri}}</div>" +
        "<div>ShortedUrl: {{uri.ShortUri}}</div>" +
        "<div>Created Time: {{uri.CreatedAt}}</div>" +
        "<div>Number of transfers: {{uri.TransferCount}}</div></div></ol>"
    };
});