Blog.viewmodels.newArticlePage = function ($, ko) {
    function articleViewModel(model) {
        var self = this;
        self.id = model.Id;
        self.title = model.Title;
        self.description = model.Description;
        self.content = model.Content;

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
           
            $.ajax(
            {
                type: "POST",
                url: "SaveArticle",
                data: ko.toJSON(self),
                success: function () {
                    location.href = "Index";
                
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