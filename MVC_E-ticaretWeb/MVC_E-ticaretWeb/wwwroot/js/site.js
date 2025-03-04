// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code
$(document).ready(function () {
    $("#userIcon").click(function (event) {
        event.preventDefault(); // Link davranışını engelle
        $("#userCard").toggle();
    });

    // Sayfanın başka bir yerine tıklanınca kart kapansın
    $(document).click(function (event) {
        if (!$(event.target).closest("#userIcon, #userCard").length) {
            $("#userCard").hide();
        }
    });

    $(document).on('click', '.js_add_to_product', function (e) {
        var _t = $(this);
        $.ajax({
            'url': '/Cart/AddToCart',
            'type': 'POST',
            'data': {
                productId: _t.data('productId')
            },
            'success': function (data) {
                if (_t.data('refresh')) {
                    location.reload();
                } else {
                    var count = $('.cart-count').text().trim();
                    $('.cart-count').text(parseInt(count) + 1);
                    alert('Sepete eklendi: ' + data);
                }
            },
            'error': function (request, error) {
                alert("Request: " + JSON.stringify(request));
            }
        });
    })

    $(document).on('click', '.js_remove_from_cart', function (e) {
        var _t = $(this);
        $.ajax({
            'url': '/Cart/RemoveFromProduct',
            'type': 'POST',
            'data': {
                productId: _t.data('productId')
            },
            'success': function (data) {
                if (_t.data('refresh')) {
                    location.reload(); // Sayfayı yenile
                }
            },
            'error': function (request, error) {
                alert("Request: " + JSON.stringify(request));
            }
        });
    });

    $(document).on('click', '.js_remove_product', function (e) {
        var _t = $(this);
        $.ajax({
            'url': '/Cart/RemoveProduct',
            'type': 'POST',
            'data': {
                productId: _t.data('productId')
            },
            'success': function (data) {
                if (_t.data('refresh')) {
                    location.reload();
                }
            },
            'error': function (request, error) {
                alert("Request: " + JSON.stringify(request));
            }
        });
    })


    $(document).on("click", ".js_add_or_remove_fav", function () {
        var _t = $(this);
        var datas = _t.data();
        var icon = _t.find("i");

        $.ajax({
            url: "/Favorite/AddOrRemoveFavorite",
            type: "POST",
            data: { productId: datas.productId, isAdded: datas.isAdded },
            success: function (res) {
                if (res.success === true) {
                    if (datas.remove == true) {
                        _t.closest('.favorite-item[data-product-id="' + datas.productId + '"]').remove();
                        $('.fav-list .favorite-item').length > 0 ? null : location.reload();
                        return;
                    }
                    _t.data('isAdded', !datas.isAdded);
                    //alert(res.message);
                    if (datas.isAdded == true) {
                        icon.addClass("bi-heart").removeClass("bi-heart-fill text-danger");
                    } else {
                        icon.removeClass("bi-heart").addClass("bi-heart-fill text-danger");
                    }
                }
            }
        });
    })
});
var swiper = new Swiper(".mySwiper", {
    slidesPerView: 1,
    spaceBetween: 10,
    loop: true,
    navigation: {
        nextEl: ".swiper-button-next",
        prevEl: ".swiper-button-prev",
    },
    pagination: {
        el: ".swiper-pagination",
        clickable: true,
    },
});