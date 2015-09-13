function BookFormViewModel(book) {
    var self = this;
    self.saveCompleted = ko.observable(false);
    self.sending = ko.observable(false);

    self.isCreating = book.Id === 0;

    self.book = {
        Id: book.Id,
        AuthorId: ko.observable(book.AuthorId),
        Title: ko.observable(book.Title),
        Isbn: ko.observable(book.Isbn),
        Synopsis: ko.observable(book.Synopsis),
        Description: ko.observable(book.Description)
    };
    
    self.validateAndSave = function(form) {
        if ($(form).valid() === false) {
            return false;
        }
        self.sending(true);
        self.book.__RequestVerificationToken = form[0].value;

        $.ajax({
                url: self.isCreating ? "Create" : "Edit",
                type: "post",
                contentType: "application/x-www-form-urlencoded",
                data: ko.toJS(self.book)
            })
            .success(self.successfullSave)
            .error(self.errorsave)
            .complete(function() { self.sending(false) });
    };
    self.successfullSave = function () {
        self.saveCompleted(true);
        $(".body-content").prepend(
            "<div class=\"alert alert-success\"><strong>Book was successfully saved</strong></div>");
        setTimeout(function () {
            if (self.isCreating) {
                location.href = "./";
            } else {
                location.href = "../";
            }
        }, 1000);
    };
    self.errorSave = function () {
        $(".body-content").prepend(
            "<div class=\"alert alert-danger\"><strong>There was an error while saving the book</strong></div>"
        );
    };
    
}