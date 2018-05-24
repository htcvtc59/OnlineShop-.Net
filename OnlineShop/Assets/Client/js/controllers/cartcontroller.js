
var cart = {
    init: function () {
        cart.regEvents();
    },
    regEvents: function () {
        $('#btnContinue').off('click').on('click', function () {
            window.location.href = "/";
        });

        $('#btnPayment').off('click').on('click', function () {
            window.location.href = "/Cart/Payment";
        });

        $('#btnUpdate').off('click').on('click', function () {
            var listpro = $('.cartquantity');
            var cartlist = [];
            $.each(listpro, function (i, item) {
                cartlist.push({
                    Quantity:$(this).val(),
                    Product: {
                        ID:$(item).data('id')
                    }

                });
            });
            $.ajax({
                url: '/Cart/Update',
                data: { cartModel: JSON.stringify(cartlist) },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/Cart";
                    }

                }

            })

        });

        $('#btnDelete').off('click').on('click', function () {
            $.ajax({
                url: '/Cart/Delete',
                data: { id:$(this).data('id') },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/Cart";
                    }

                }

            })

        });

        $('#btnDeleteitem').off('click').on('click', function (event) {
            event.preventDefault();
            $.ajax({
                url: '/Cart/Deleteitem',
                data: { id: $(this).data('id') },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/";
                    }

                }

            })

        });



    }
}
cart.init();