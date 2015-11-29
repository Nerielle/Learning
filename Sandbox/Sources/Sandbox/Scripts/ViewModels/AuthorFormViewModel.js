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
        if (!$(form).valid())
            return false;

        self.sending(true);

        // include the anti forgery token
        self.author.__RequestVerificationToken = form[0].value;

        $.ajax({
            url: '/api/authors',
            type: (self.isCreating) ? 'post' : 'put',
            contentType: 'application/json',
            data: ko.toJSON(self.author)
        })
        .success(self.successfulSave)
        .error(self.errorSave)
        .complete(function () { self.sending(false) });
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