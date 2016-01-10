app.service('articleService', function($http) {
this.getArtciles = function() {
    return $http.get("/api/Article");
}
    this.get = function(guid) {
        return $http.get("/api/Article/" + guid);
    }
});