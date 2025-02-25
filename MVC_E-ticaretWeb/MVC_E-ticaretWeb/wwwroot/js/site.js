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
    $(document).ready(function () {
        updateCartItemCount(); 

        $(document).on("click", ".add-to-cart-button", function () {
            updateCartItemCount(); 
        });

        $(document).on("click", ".remove-from-cart-button", function () {
            updateCartItemCount();
        });
    });

    function updateCartItemCount() {
        $.ajax({
            url: "/Cart/GetCartItemCount",
            type: "GET",
            data: { _: new Date().getTime() }, 
            success: function (response) {
                $("#cart-item-count").text(response);
            },
            error: function () {
                console.log("Sepet sayısı alınırken hata oluştu.");
            }
        });
    }
  

    //Favorileme Kısmı
    $(document).ready(function () {
        $(".favorite-button").click(function () {
            var button = $(this);
            var icon = button.find("i");
            var productId = button.data("product-id");

            $.ajax({
                url: "/Favorite/ToggleFavorite",
                type: "POST",
                data: { productId: productId },
                success: function (response) {
                    if (response.isFavorited) {
                        icon.removeClass("bi-heart").addClass("bi-heart-fill text-danger");
                    } else {
                        icon.removeClass("bi-heart-fill text-danger").addClass("bi-heart");
                    }
                },
                error: function () {
                    alert("Bağlantı hatası!");
                }
            });
        });
    });



    $(document).ready(function () {
        $(".favorite-button").click(function () {
            var button = $(this);
            var productId = button.data("product-id");

            $.ajax({
                url: "/Favorite/AddToFavorites",  // URL'yi kontrol et
                type: "POST",
                data: { productId: productId },
                success: function (response) {
                    if (response.success) {
                        alert("Ürün favorilere eklendi!");
                    } else {
                        alert(response.message); // Daha iyi hata mesajı
                    }
                },
                error: function (xhr) {
                    alert("Bağlantı hatası! " + xhr.responseText);
                }
            });
        });
    });



});