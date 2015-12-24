app.controller('articleController', function($scope, articleService) {
    function loadArticles() {
        var results = articleService.getArtciles();
        results.then(function(x) {
                $scope.articles = x.data;
            },
            function(error) {
                $log.error('there is an error during loading the articles', error);
            });
        
    }

    loadArticles();
})