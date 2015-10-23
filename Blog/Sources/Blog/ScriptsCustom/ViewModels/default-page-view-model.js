﻿Blog.viewmodels.defaultpage = function ($, ko) {
    function comment(item) {
        var self = this;
        self.date = item.Date;
            self.content = item.Content;
        self.deleteComment = function() {
            alert("Deleting comment");
        };
    }
    function articleViewModel(model) {
        var self = this;
        self.id = model.id;
        self.title = model.title;
        self.description = model.description;
        self.content = model.content;
        self.date = model.Date;

        function initComments() {
            if (!model.comments) {
                return [];
            }
            return model.comments.map(function(item) {
                return new comment(item);
            });
        }

        self.comments = initComments();
    }

    function bindArticles(model) {
        var articles = model.map(function(item) {
            return new articleViewModel(item);
        });


        function pageViewModel(articleViewModelsList) {
            var self = this;
            self.articles = ko.observableArray(articleViewModelsList);
            self.selectedItems = ko.observableArray([self.articles()[0]]);
        }

        var viewModel = new pageViewModel(articles);
        ko.applyBindings(viewModel);
    }


    return {
        bindArticleList: bindArticles
    };
}(jQuery, ko);