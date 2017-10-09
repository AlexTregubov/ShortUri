app.directive('myInput', function() {
    return {
        restrict: 'E',
        template: "<label>Uri:<input type='text' ng-model='sourceUrl'></label><button ng-click='postUriToShort(sourceUrl)'>GO</button>"
    };
});