function CartItemViewModel(params) {
    var self = this;

    self.sending = ko.observable(false);

    self.cartItem = params.cartItem;
    self.showButton = params.showButton;

    self.upsertCartItem = function (form) {
        if (!$(form).valid())
            return false;

        self.sending(true);

        var data = {
            id: self.cartItem.id,
            cartId: self.cartItem.cartId,
            bookId: self.cartItem.book.id,
            quantity: self.cartItem.quantity()
        };

        $.ajax({
            url: '/api/cartitems',
            type: self.cartItem.id === undefined ? 'post' : 'put',
            contentType: 'application/json',
            data: ko.toJSON(data)
        })
        .success(self.successfulSave)
        .error(self.errorSave)
        .complete(function () { self.sending(false) });
    };

    self.successfulSave = function (data) {
        var msg = '<div class="alert alert-success"><strong>Success!</strong>The item has been ';
        if (self.cartItem.id === undefined)
            msg += 'added to';
        else
            msg += 'updated in';

        $('.body-content').prepend(msg + ' your cart.</div>');

        self.cartItem.id = data.id;

        cartSummaryViewModel.updateCartItem(ko.toJS(self.cartItem));
    };

    self.errorSave = function () {
        var msg = '<div class="alert alert-danger"><strong>Error!</strong>There was an error ';
        if (self.cartItem.id === undefined)
            msg += 'adding';
        else
            msg += 'updating';

        $('.body-content').prepend(msg + ' the item to your cart.</div>');
    };
};
ko.components.register('upsert-cart-item', {
    viewModel: CartItemViewModel,
    template: { element: 'cart-item-form' }
});