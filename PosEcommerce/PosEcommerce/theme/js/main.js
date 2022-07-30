$(document).ready(function () {
    'use strict';

    // sync Navbar Links With Sections

    $(".scroll-to-top, .home-btn").click(function (e) {
        e.preventDefault();

        $('html, body').animate({
            scrollTop: 0
        }, 1000);

    });

    $(".menu-bg").click(function () {
        if (!$('.main-menu').hasClass('open')) {
            $('.main-menu').addClass('open');
            $('.main-menu').animate({
                left: "-=220"
            }, 500);
        } else if (!$('.cart-menu').hasClass('open')) {
            $('.cart-menu').addClass('open');
            $('.cart-menu').animate({
                right: "-=350"
            }, 500);
        } else if (!$('.login-menu').hasClass('open')) {
            $('.login-menu').addClass('open');
            $('.login-menu').animate({
                right: "-=350"
            }, 500);
        }

        $(this).css({
            display: "none"
        });

    });

    $(".main-menu span").click(function () {
        $('.main-menu').addClass('open');
        $('.main-menu').animate({
            left: "-=220"
        }, 500);
        $(".menu-bg").css({
            display: "none"
        });
    });

    $(".login-menu span").click(function () {
        $('.login-menu').addClass('open');
        $('.login-menu').animate({
            right: "-=350"
        }, 500);
        $(".menu-bg").css({
            display: "none"
        });
    });

    $('.main-menu-button i').click(function () {
        if ($('.main-menu').hasClass('open')) {

            $('nav .main-menu-button .m').slideDown(200);
            $('.main-menu').removeClass('open');
            $('.main-menu').animate({
                left: 0
            }, 500);
            $(".menu-bg").css({
                display: "block"
            });
        } else {

            $('.main-menu').addClass('open');
            $('.main-menu').animate({
                left: "-=200"
            }, 500);
            $(".menu-bg").css({
                display: "none"
            });
        }
    });

    $('.login-button').click(function () {
        if ($('.login-menu').hasClass('open')) {

            $('.login-menu').removeClass('open');
            $('.login-menu').animate({
                right: 0
            }, 500);
            $(".menu-bg").css({
                display: "block"
            });
        } else {

            $('.login-menu').addClass('open');
            $('.login-menu').animate({
                right: "-=350"
            }, 500);
            $(".menu-bg").css({
                display: "none"
            });
        }
    });

    $('.main-menu ul li').hover(function () {
        var dmenu = $(this).data('menu');
        if (dmenu == "products") {
            $('.main-menu .products .dropdown-mobile').css('display', 'block');
        } else if (dmenu == "contacts") {
            $('.main-menu .contacts .dropdown-mobile').css('display', 'block');
        }
    }, function () {
        $('.main-menu .dropdown-mobile').css('display', 'none');
    });

    $('.search-button').click(function () {
        $('.navbar .search').fadeToggle(500);
    });

    $('.login-menu h4').click(function () {
        $(this).addClass('tab-active');
        $(this).siblings().removeClass('tab-active');
        var data = $(this).data('tab');
        $('.data #' + data).show().siblings().hide();
    });

    $('#add-cart').click(function (e) {
        e.preventDefault;
        var id = itemId;



        if ($('#quantity').val() > 0) {

            var transItem = new Object();//dynamically fill model value with different model entity
            transItem.itemId = itemId;
            transItem.quantity = $('#quantity').val();
            transItem.propsValues = [];

            var i = 0;
            $(".sel-prop  option:selected").each(function () {

                if ($(this).has('option:selected')) {
                    if (this.text) {
                        var b = { itemPropId: this.value, name: this.text };
                        transItem.propsValues[i] = b;
                        i++;
                    }
                }
            });

            $.ajax({
                url: path + '/Cart/AddWithQuantity',
                //data: JSON.stringify({
                //    itemId: id,
                //    quantity: _quantity
                //}),
                data: JSON.stringify(transItem),
                type: "POST",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                async: true,
                success: function (result) {
                    $('#span_cart_count').show();
                    $('#span_cart_count').html(result.cartCount);
                    $('.alert-success').fadeIn(300).delay(2000).fadeOut(400);
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert('oops, something bad happened');
                }
            });



        } else {
            $('.alert-danger').fadeIn(300).delay(2000).fadeOut(400);
        }
    });


    $('.add-to-cart').click(function (e) {
        e.preventDefault;
        var id = $(this).children(":first").val();

        $.ajax({
            url: path + '/Cart/Add',
            type: "POST",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ itemId: id }),
            dataType: 'json',
            async: true,
            success: function (result) {
                $('#span_cart_count').show();
                $('#span_cart_count').html(result.cartCount);
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert('oops, something bad happened');
            }
        });
    });

    function toggleDropdown(e) {
        const _d = $(e.target).closest('.dropdown'),
            _m = $('.dropdown-menu', _d);
        setTimeout(function () {
            const shouldOpen = e.type !== 'click' && _d.is(':hover');
            _m.toggleClass('show', shouldOpen);
            _d.toggleClass('show', shouldOpen);
            $('[data-toggle="dropdown"]', _d).attr('aria-expanded', shouldOpen);
        }, e.type === 'mouseleave' ? 300 : 0);
    }

    var quantitiy = 0;
    $('.quantity-right-plus').click(function (e) {

        // Stop acting like a button
        e.preventDefault();


        $('#total').text("");
        // Get the field name

        var quantity = parseInt($(this).parent().parent().find('input').val());

        if (allnumeric(quantity.toString())) {
            if (isNaN(parseFloat(quantity))) {
                quantity = 0;
                $(this).parent().parent().find('input').val(quantity);
                $('#total').text("0");

            } else {
                $(this).parent().parent().find('input').val(quantity + 1);
                var price = parseFloat($('#price').text());
                var res = (price * (quantity + 1));

                $('#total').text(accuracyconv(res, accuracy));
            }
        } else {
            $(this).parent().parent().find('input').val("0");
            $('#total').text("0");
        }




    });

    $('.quantity-left-minus').click(function (e) {
        // Stop acting like a button
        e.preventDefault();
        $('#total').text("");
        // Get the field name
        var quantity = parseInt($(this).parent().parent().find('input').val());

        // If is not undefined
        if (allnumeric(quantity.toString())) {
            if (isNaN(parseFloat(quantity))) {
                quantity = 0;
                $(this).parent().parent().find('input').val(quantity);
                $('#total').text("0");
            } else {
                //
                if (quantity > 0) {
                    $(this).parent().parent().find('input').val(quantity - 1);
                    //total
                    var price = parseFloat($('#price').text());
                    var res = (price * (parseFloat(quantity) - 1));
                    $('#total').text(accuracyconv(res, accuracy));

                } else {
                    $('#total').text("0");
                }
            }
        } else {
            $(this).parent().parent().find('input').val("0");
            $('#total').text("0");
        }

    });

    $('body')
        .on('mouseenter mouseleave', '.dropdown', toggleDropdown)
        .on('click', '.dropdown-menu a', toggleDropdown);




    function accuracyconv(valnum, accuracy) {
        var fnum = "";


        if (accuracy != null && accuracy != "") {
            if (isNaN(parseFloat(valnum))) {
                fnum = "0";
            } else {
                if (parseFloat(valnum) == 0) {
                    fnum = "0";
                } else {
                    var accuracyval = parseInt(accuracy);
                    fnum = parseFloat(valnum).toFixed(accuracyval);
                }
            }


        } else {
            fnum = valnum;
        }

        return fnum.toString();

    };

    $('#quantity').keyup(function (e) {
        // Stop acting like a button
        //e.preventDefault();
        $('#total').text("");
        // Get the field name
        var quantity = parseInt($(this).parent().parent().find('input').val());

        // If is not undefined
        if (allnumeric(quantity.toString())) {
            $(this).parent().parent().find('input').val(quantity);
            var price = parseFloat($('#price').text());
            var res = (price * (parseFloat(quantity)));
            $('#total').text(accuracyconv(res, accuracy));
        } else {
            //
            $(this).parent().parent().find('input').val("0");
            $('#total').text("0");
        }


    });
    //  customer
    $('#btnSaveCustomer').click(function (e) {
        e.preventDefault;
        //validate
        var req = true;

        $("form#customerform :input").each(function () {
            var input = $(this); 
            if (input.prop('required')) {
   req=req && required(input.val());
            }
            // This is the jquery object of the input, do what you will
     
        });
        //required($("input[name='name']").val())
        if (req) {
            var customer = $("#customerform").serialize();
            //  alert(customer);
            $.ajax({
                url: path + '/Customer/saveCustomer',
                type: "POST",
                //contentType: "application/json; charset=utf-8",
                contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
                data: customer,
                dataType: 'json',
                async: true,
                success: function (result) {
                    alert(result.msg);

                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert('oops, something bad happened');
                }
            });
        } else {
            alert("please fill empty fields");

        }
      
    });

    //login
    $('#btnlogin').click(function (e) {
        e.preventDefault;
        var customer = $("#loginForm").serialize();
    //    alert(customer);
        $.ajax({
            url: path + '/Customer/loginCustomer',
            type: "POST",
            //contentType: "application/json; charset=utf-8",
            contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
            data: customer,
            dataType: 'json',
            async: true,
            success: function (result) {
                alert(result.msg);

            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert('oops, something bad happened');
            }
        });
    });
    //logout
    $('#btnlogout').click(function (e) {
        e.preventDefault;
       // var customer = $("#loginForm").serialize();
        //    alert(customer);
        $.ajax({
            url: path + '/Customer/logoutCustomer',
            type: "POST",
            //contentType: "application/json; charset=utf-8",
            contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
            data: "1",
            dataType: 'json',
            async: true,
            success: function (result) {
                alert(result.msg);

            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert('oops, something bad happened');
            }
        });
    });
    //fill countries

    const countries = JSON.parse(allCountries);


    let dropdown = $('#countryName');

    dropdown.empty();
    dropdown.append('<option  disabled>Region..</option>');
    dropdown.prop('selectedIndex', 0);

    //city
    let dropdowncity = $('#cityName');

    dropdowncity.empty();

    if (customerCity == null || customerCity == "") {

        dropdowncity.append('<option  selected="true" disabled>City..</option>');
        dropdowncity.prop('selectedIndex', 0);
    }
    $.each(countries.data, function (key, entry) {
        if (customerCountry == entry.country) {
            dropdown.append($('<option></option>').attr('value', entry.country).attr('selected', "true").text(entry.country));

            if (entry.country == customerCountry) {
                $.each(entry.cities,
                    function (key, value) {
                        if (customerCity != null && customerCity != "") {
                            if (customerCity == value) {
                                dropdowncity.append($('<option></option>').attr('value', customerCity).attr('selected', "true").text(customerCity));
                            } else {
                                dropdowncity.append($('<option></option>').attr('value', value).text(value));
                            }
                        }
                    });
            }
        } else {
            dropdown.append($('<option></option>').attr('value', entry.country).text(entry.country));
        }
    });
    //fill cities on change
    $('#countryName').change(function (e) {
        dropdowncity.empty();
        dropdowncity.append('<option selected="true" disabled>City..</option>');
        dropdowncity.prop('selectedIndex', 0);
        $.each(countries.data, function (key, entry) {
            var co = $('#countryName').val();
            if (entry.country == co) {
                $.each(entry.cities,
                    function (key, value) {
                        dropdowncity.append($('<option></option>').attr('value', value).text(value));
                    });
            }
        });

    });

});

