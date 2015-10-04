Blog.viewmodels.defaultpage = function($, ko) {
    function Article(model) {
        var self = this;
        self.id = model.Id;
        self.title = model.Title;
        self.description = model.Description;
        self.content = model.Content;
        self.date = model.Date;
    }
}(jQuery, ko);