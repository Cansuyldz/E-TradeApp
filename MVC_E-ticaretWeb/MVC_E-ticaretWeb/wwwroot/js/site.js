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
 
    $(document).ready(function () {
        $(".fav-btn").each(function () {
            var button = $(this);
            var productId = button.data("product-id");
            var icon = button.find("i");

            $.ajax({
                url: "/Favorite/CheckFavorite",
                type: "GET",
                data: { id: productId }, 
                success: function (response) {
                    if (response.isFavorite) {
                        icon.removeClass("bi-heart").addClass("bi-heart-fill text-danger");
                    }
                }
            });
        });

        $(document).ready("click", ".fav-btn", function () {
            console.log('geldi');
            var button = $(this);
            var productId = button.data("product-id");
            var icon = button.find("i");

            $.ajax({
                url: "/Favorite/ToggleFavorite",
                type: "POST",
                data: { id: productId },  
                success: function (response) {
                    if (response.isFavorite) {
                        icon.removeClass("bi-heart").addClass("bi-heart-fill text-danger");
                    } else {
                        icon.removeClass("bi-heart-fill text-danger").addClass("bi-heart");
                    }
                },
                error: function () {
                    alert("Favorilere eklerken hata oluştu.");
                }
            });
        });

        $(document).ready("click", ".remove-favorite-btn", function () {
            var button = $(this);
            var productId = button.data("product-id");

            $.ajax({
                url: "/Favorite/RemoveFavorite",
                type: "POST",
                data: { id: productId },  
                success: function (response) {
                    if (response.success) {
                        button.closest(".favorite-item").fadeOut(); 
                    } else {
                        alert(response.message);
                    }
                },
                error: function () {
                    alert("Favorilerden kaldırırken hata oluştu.");
                }
            });
        });
    });
});