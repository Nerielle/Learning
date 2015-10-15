Blog.viewmodels.newArticlePage = function ($, ko) {
    function articleViewModel(model) {
        //Console.log(model);
        var self = this;
        self.id = model.Id;
        self.title = model.Title;
        self.description = model.Description;
        self.content = model.Content;
        self.date = model.Date;
        self.selectedItem = ko.observable({});

        function initComments() {
            if (!model.Comments) {
                return [];
            }
            return model.Comments.map(function (item) {
                return {
                    date: item.Date,
                    content: item.Content,
                    article: self
                };
            });
        }

        self.comments = initComments();
        self.save = function () {
            var articleModel = ko.JSON(self);
            articleModel.Title = self.title;
            articleModel.Description = self.description;
            articleModel.Content = self.content;
            $.ajax(
            {
                type: "POST",
                url: "Home/SaveArticle",
                data: JSON.stringify(articleModel),
                success: function () {
                    window.location.href = "/";
                },
                contentType:
            "application/json"
            });
        }
    }
    function bindArticle(model) {
        ko.applyBindings(new articleViewModel(model));

    }
    return {
        bindArticle: bindArticle
    };
}
(jQuery, ko)