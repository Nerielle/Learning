Blog.viewmodels.defaultpage = function ($, ko) {
    function articleViewModel(model) {
        var self = this;
        self.id = model.id;
        self.title = model.title;
        self.description = model.description;
        self.content = model.content;
        self.date = model.Date;
        //self.selectedItem = ko.observable({});

        function initComments() {
            if (!model.comments) {
                return [];
            }
            return model.comments.map(function (item) {
                return {
                    date: item.Date,
                    content: item.Content,
                    article: self
                };
            });
        }

        self.comments = initComments();
    }

    function bindArticles(model) {
        var articles = model.map(function (item) {
            return new articleViewModel(item);
        });
        //var hasNewItem = articles.some(function (item) {
        //    if (item.id === "00000000-0000-0000-0000-000000000000") {
        //        item.isSelected = true;
        //        return true;
        //    }
        //    return false;
        //});
        //if (!hasNewItem) {
        //    articles[0].isSelected = true;
        //} 

        
        var viewModel =  {
            
            articles: ko.observableArray(articles),
            selectedItem : ko.observable([])
            //self.selectedItem = self.articles[0];
            //self.selectedArticle = ko.observable();
            //self.selectedArticle = $.grep(articles, function(item) {
            //    return item.isSelected;
            //})[0];
        }
      
        ko.applyBindings(viewModel);
    }

    return {
        bindArticleList: bindArticles
    };
}(jQuery, ko);