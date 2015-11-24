Blog.viewmodels.defaultpage = function ($, ko) {
    function comment(item) {
        var self = this;
        self.id = item.id;
        self.date = item.date;
        self.content = item.content;
        self.deleteComment = function () {
            $.ajax({
                type: "POST",
                url: Blog.deleteCommentPath,
                data: ko.toJSON(self),
                success: function () {
                    $('.blog-post').prepend('<div class="alert alert-success"><strong>Success!</strong> The comment has been deleted.</div>');

                    $('#' + self.id).remove();
                },
                contentType: "application/json",
                error: function () {
                    $('.blog-post').prepend('<div class="alert alert-danger"><strong>Error!</strong> There was an error deleting the article.</div>');
                }
            });
        };

    }
    function articleViewModel(model) {
        var self = this;
        self.id = model.id;
        self.title = model.title;
        self.description = model.description;
        self.content = model.content;
        self.date = model.date;

        function initComments() {
            if (!model.comments) {
                return [];
            }
            return model.comments.map(function (item) {
                return new comment(item);
            });
        }

        self.comments = initComments();

    }

    function bindArticles(model) {
        var articles = model.map(function (item) {
            return new articleViewModel(item);
        });


        function pageViewModel(articleViewModelsList) {
            var self = this;
            self.articles = ko.observableArray(articleViewModelsList);
            self.selectedItems = ko.observableArray([self.articles()[0]]);
            self.newComment = ko.observable();
            self.saveNewComment = function () {
                var articleId = self.selectedItems()[0].id;
                $.ajax({
                    type: "POST",
                    url: Blog.saveNewCommentPath,
                    data: ko.toJSON({ content: self.newComment, articleId: articleId }),
                    success: function () {
                        location.href = Blog.indexPath;
                    },
                    contentType: "application/json",
                    error: function () {
                        alert("Can't save comment");
                    }
                });
            }
            self.deleteArticle = function () {
                var article = self.selectedItems()[0];
                var json = ko.toJSON(article);
               
                $.ajax({
                    type: "POST",
                    url: Blog.deleteArticlePath,
                    data: json,
                    contentType: "application/json",
                    success: function() {
                        $('.blog-post').prepend('<div class="alert alert-success"><strong>Success!</strong> The article has been deleted.</div>');
                    },
                    error: function() {
                        $('.blog-post').prepend('<div class="alert alert-danger"><strong>Error!</strong> There was an error deleting the article.</div>');
                    }
                });
            }
        }

        var viewModel = new pageViewModel(articles);
        ko.applyBindings(viewModel);
    }
    return {
        bindArticleList: bindArticles
    };
}(jQuery, ko);