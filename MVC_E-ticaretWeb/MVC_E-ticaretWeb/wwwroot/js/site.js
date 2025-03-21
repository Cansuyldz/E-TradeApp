﻿// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
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

    $('.js-add-address').click(function () {
        $('.add-address-form').toggleClass('active');
    });
    $("#kaydetButton").click(function (e) {
        if ($("input[name=adres]:checked").length === 0) {
            e.preventDefault();
        } else {
            window.location.replace("/checkout/payment");
            console.log("Yönlendirme çalıştı mı?");
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
            data: { productId: datas.productId },
            success: function (res) {
                if (res != undefined && res.success === true) {
                    if (datas.remove == true) {
                        _t.closest('.favorite-item[data-product-id="' + datas.productId + '"]').remove();
                        $('.fav-list .favorite-item').length > 0 ? null : location.reload();
                        return;
                    }
                    if (res.isFavoriteAdded == true) {
                        icon.removeClass("bi-heart").addClass("bi-heart-fill text-danger");
                    } else {
                        icon.addClass("bi-heart").removeClass("bi-heart-fill text-danger");
                    }
                }
            }
        });
    })
    $(document).on("click", ".js-add-save-address", function () {
        var _t = $(this);
        var form = _t.closest("form");

        if (!form.length) {
            console.error("Form bulunamadı!");
            return;
        }

        var errCount = 0;
        form.serializeArray().forEach(function (item) {
            var value = item.value.trim();
            if (value.length == 0) {
                errCount++;
            }
        })

        if (errCount > 0) {
            console.warn("Lütfen tüm alanları doldurunuz!");
            return;
        }


        $.ajax({
            url: "/checkout/Newaddress",
            type: "POST",
            data: form.serialize(),
            success: function (res) {
                if (res && res.success) {
                    location.reload();
                } else {
                    console.error("Adres eklenirken hata oluştu: " + (res?.message || "Bilinmeyen hata"));
                }
            },
            error: function (xhr, status, error) {
                console.error("Sunucu hatası:", error);
            }
        });
    });

    $(document).on("click", ".js-add-card-save", function (e) {
        e.preventDefault();

        var form = $(this).closest("form");
        if (!form.length) {
            console.error("Form bulunamadı!");
            return;
        }

        var nameonCard = form.find("input[name='NameonCard']").val().trim();
        var kartNumber = form.find("input[name='KartNumber']").val().trim();
        var date = form.find("input[name='Date']").val().trim();
        var year = form.find("input[name='Year']").val().trim();
        var cvc = form.find("input[name='Cvc']").val().trim();
        if (!kartNumber || !nameonCard || !date || !year || !cvc) {
            console.warn("Lütfen tüm alanları doldurunuz!");
            return;
        }
        var params = {
            KartNumber: kartNumber,
            NameonCard: nameonCard,
            Date: date,
            Year: year,
            Cvc: cvc,
        };
        $.ajax({
            url: "/checkout/NewCart",
            type: "POST",
            data: params,
            success: function (res) {
                if (res.success) {
                    alert("Kart başarıyla kaydedildi!");
                    location.reload();
                } else {
                    alert(res.message);
                }
            },
            error: function (xhr) {
                alert("Kart kaydedilirken hata oluştu: " + xhr.responseText);
            }
        });
    });

    $(document).on('click', '.js_select_address', function () {
        var _t = $(this);
        $.ajax({
            url: "/checkout/SetAddress",
            type: "POST",
            data: { addressId: _t.val() },
            success: function (res) {
                if (res) {
                    location.reload()
                }
            },
            error: function (xhr) {
                alert("Adres seçilirken hata oluştu: " + xhr.responseText);
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

function toggleCardForm() {
    document.getElementById('cardList').classList.toggle('hidden');
    document.getElementById('newCardForm').classList.toggle('hidden');
}
