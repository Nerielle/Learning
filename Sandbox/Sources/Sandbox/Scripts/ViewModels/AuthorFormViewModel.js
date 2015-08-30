function AuthorFormViewModel(author) {
    var self = this;
    self.SaveCompleted = ko.observable(false);
    self.Sending = ko.observable(false);

    self.isCreating = author.Id == 0;

    self.Author = {
        Id: author.Id,
        FirstName: ko.observable(author.FirstName),
        LastName: ko.observable(author.LastName),
        Biography: ko.observable(author.Biography)
    };
    self.ValidateAndSave = function (form) {
        if ($(form).valid() == false) {
            return false;
        }

        self.Sending(true);
        self.Author.__RequestVerificationToken = form[0].value;

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
        }, 1000);
    };

    self.errorSave = function () {
        $('.body-content').prepend(
            '<div class="alert alert-danger"><strong>There was an error</strong></div>'
            );
    };
};