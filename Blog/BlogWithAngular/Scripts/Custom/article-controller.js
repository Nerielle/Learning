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

    function get(article) {
        var promiseGetSingle = articleService.get(article.id);

        promiseGetSingle.then(function (pl) {
            var res = pl.data;
            $scope.id = res.id;
            $scope.title = res.title;
                $scope.content = res.content;
            },
                  function (errorPl) {
                      console.log('failure loading article', errorPl);
                  });
    }
    loadArticles();

})