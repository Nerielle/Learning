function AuthorFormViewModel(author) {
    var self = this;
    self.SaveCompleted = ko.observable(false);
    self.Sending = ko.observable(false);

    self.isCreating = author.Id == 0;

    self.Author = {
        Id: author.Id,
        FirstName: ko.observable(),
        LastName: ko.observable(),
        Biography: ko.observable()
    };
    self.ValidateAndSave = function (form) {
        if ($(form).Valid() == false) {
            return false;
        }

        self.Sending(true);
        self.Author._RequestVerificationToken = form[0].Value;

        $.ajax({
            url: self.isCreating ? 'Create' : 'Edit',
            type: 'post',
            contentType: 'application/x-www-form-urlencoded',
            data: ko.toJS(self.Author)
        })
        .success(self.successfullSave)
        .error(self.errorSave)
        .complete(function () { self.Sending(false) });
    };
    self.successfullSave = function () {
        self.SaveCompleted(true);
        $('.body-content').prepend(
            '<div class="alert alert-success"><strong>Successfully saved</strong></div>'
            );
        setTimeout(function () {
            if (self.isCreating) {
                location.href = './';
            }
            else {
                location.href = '../';
            }
        }, 4000);
    };

    self.errorSave = function () {
        $('.body-content').prepend(
            '<div class="alert alert-danger"><strong>There was an error</strong></div>'
            );
    };
};