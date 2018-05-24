$(document).ready(function () {

    $(".itemofitemMenu").hide();
    $(".itemsmall").click(function () {
        $(this).next().slideToggle();
    });
//            Product

    var width = $(window).width();
    if (width >= 1400) {
        $(".slider1").bxSlider({
            slideWidth: 140,
            minSlides: 7,
            maxSlides: 7,
            slideMargin: 10,
        });
    }
    if (width >= 1200) {
        $(".slider1").bxSlider({
            slideWidth: 130,
            minSlides: 6,
            maxSlides: 6,
            slideMargin: 10,
        });
    }
    if (width >= 1000) {
        $(".slider1").bxSlider({
            slideWidth: 130,
            minSlides: 5,
            maxSlides: 5,
            slideMargin: 10,
        });
    }

    if (width >= 768) {
        $(".slider1").bxSlider({
            slideWidth: 124,
            minSlides: 4,
            maxSlides: 4,
            slideMargin: 10,
        });
    }
    if (width > 568 && width < 768) {
        $(".slider1").bxSlider({
            slideWidth: 124,
            minSlides: 5,
            maxSlides: 5,
            slideMargin: 10,
        });
    }
    if (width >= 400 && width <= 568) {
        $(".slider1").bxSlider({
            slideWidth: 124,
            minSlides: 4,
            maxSlides: 4,
            slideMargin: 10,
        });
    }
    if (width > 320 && width <= 400) {
        $(".slider1").bxSlider({
            slideWidth: 110,
            minSlides: 3,
            maxSlides: 3,
            slideMargin: 10,
        });
    }
    if (width <= 320) {
        $(".slider1").bxSlider({
            slideWidth: 130,
            minSlides: 2,
            maxSlides: 2,
            slideMargin: 10,
        });
    }

    $(".btn-top").hide();
    // Button Back To Top
    if ($(".btn-top").length > 0) {
        $(window).scroll(function () {
            var e = $(window).scrollTop();
            if (e > 400) {
                $(".btn-top").show();
            } else {
                $(".btn-top").hide();
            }
        });
        $(".btn-top").click(function () {
            $('body,html').animate({
                scrollTop: 0
            })
        })
    }
// Click image show
    var srcimg = $(".slideitem img").attr("src");
    $(".slides-container img").attr("src", srcimg);

    $(".slideitem img").click(function () {
        var srccurrent = $(this).attr("src");
        $(".slides-container img").attr("src", srccurrent);
    });
//zoom image

    $("#zoomimg").zoom();

    // quantity product add to cart
    $(".btn-number").click(function () {
        var currentVal = parseInt($("#datanumbercart").val());
        var typeclick = $(this).attr("data-type");
        // plus
        if (!isNaN(currentVal)) {
            if (typeclick == "minus") {
                if (currentVal > $("#datanumbercart").attr("min")) {
                    $("#datanumbercart").val(currentVal - 1).change();
                }
                if (parseInt($("#datanumbercart").val()) == $("#datanumbercart").attr("min")) {
                    $(this).attr("disabled", true);
                }
            } else if (typeclick == "plus") {
                if (currentVal < $("#datanumbercart").attr("max")) {
                    $("#datanumbercart").val(currentVal + 1).change();
                }
                if (parseInt($("#datanumbercart").val()) == $("#datanumbercart").attr("max")) {
                    $(this).attr("disabled", true);
                }
            }
            if ($("#datanumbercart").val() > 100) {
                $("#datanumbercart").val(1);
            }
        } else {
            $("#datanumbercart").val(1);
        }


    });
    $(".input-number").focusin(function () {
        $(this).data("oldValue", $(this).val());
    });
    $(".input-number").change(function () {
        var minValue = parseInt($(this).attr("min"));
        var maxValue = parseInt($(this).attr("max"));
        valueCurrent = parseInt($(this).val());
        var name = $(this).attr("name");
        if (valueCurrent >= minValue) {
            $("#btnsub").removeAttr("disabled");
        } else {
            $(this).val($(this)).data("oldValue");
        }
        if (valueCurrent <= maxValue) {
            $("#btnadd").removeAttr("disabled");
        } else {
            $(this).val($(this)).data("oldValue");
        }


    });
    //submit to cart


});


$(function () {
    $(".rslides").responsiveSlides();
});

