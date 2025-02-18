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
                    alert('Data: ' + data);
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
                } else {
                    alert('Data: ' + data);
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
                } else {
                    alert('Data: ' + data);
                }
            },
            'error': function (request, error) {
                alert("Request: " + JSON.stringify(request));
            }
        });
    })
    function getSepetUrunSayisi() {
        $.ajax({
            url: '/Sepet/GetUrunSayisi', // Sepet ürün sayısını getiren bir API oluştur
            type: 'GET',
            success: function (data) {
                if (data > 0) {
                    $(".cart-badge").text(data).show();
                } else {
                    $(".cart-badge").hide();
                }
            }
        });
    }

    $(document).ready(function () {
        getSepetUrunSayisi();
    });
});