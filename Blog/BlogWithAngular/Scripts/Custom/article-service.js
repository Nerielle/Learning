app.service('articleService', function($http) {
this.getArtciles = function() {
    return $http.get("/api/Article");
}
});