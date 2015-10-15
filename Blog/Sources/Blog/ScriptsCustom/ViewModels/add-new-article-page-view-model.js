Blog.viewmodels.newArticle = function ($, ko) {
    function articleViewModel(model) {
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
    }
    function bindArticle(model) {
        ko.applyBindings(new articleViewModel(model));

    }
    return {
        bindArticle: bindArticle
    };
}
(jQuery, ko)