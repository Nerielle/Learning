Blog.viewmodels.newArticlePage = function ($, ko) {
    function articleViewModel(model) {
        var self = this;
        self.id = model.id;
        self.title = model.title;
        self.description = model.description;
        self.content = model.content;

        function initComments() {
            if (!model.comments) {
                return [];
            }
            return model.comments.map(function (item) {
                return {
                    date: item.date,
                    content: item.content,
                    article: self
                };
            });
        }

        self.comments = initComments();
        self.save = function () {
           
            $.ajax(
            {
                type: "POST",
                url: Blog.saveArticlePath,
                data: ko.toJSON(self),
                success: function () {
                    location.href = Blog.indexPath;
                
                },
                contentType: "application/json",
                error: function() {
                    alert("Can't save");
                }
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