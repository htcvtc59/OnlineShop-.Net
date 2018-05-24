

var cart = {
    init: function () {
        cart.regEvents();
    },
    regEvents: function () {

        $('#btnProCate').off('click').on('click', function (e) {
            e.preventDefault();
            var listProCate = $('.slide');
            var idPC = $('#btnProCate').data('id');
            var ids = $('.slide').data('id');
            var listPro = [];
            $.each(listProCate, function (i, item) {
                if (idPC == $(item).data('id')) {
                    listPro.push({
                            CategoryID: $(item).data('id'),
                            Images: $(".slide img").attr("src"),
                            Name: $(item).find('h4').text(),
                            Price: $(item).find('h5').text()
                        
                    });
                }
            });


            $.ajax({
                url: '/Product/ListProductWithItem',
                data: { product: JSON.stringify(listPro) },
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


