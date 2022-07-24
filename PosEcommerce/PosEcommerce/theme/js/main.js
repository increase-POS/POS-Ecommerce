$(document).ready(function(){
    'use strict';

    // sync Navbar Links With Sections

        $(".scroll-to-top, .home-btn").click(function(e){
            e.preventDefault();

            $('html, body').animate({
                scrollTop: 0
            },1000);

        });

        $(".menu-bg").click(function(){
          if (!$('.main-menu').hasClass('open')) {
            $('.main-menu').addClass('open');
            $('.main-menu').animate({
              left:"-=220"
             },500);
          }else if (!$('.cart-menu').hasClass('open')) {
            $('.cart-menu').addClass('open');
            $('.cart-menu').animate({
              right:"-=350"
             },500);
          }else if (!$('.login-menu').hasClass('open')) {
            $('.login-menu').addClass('open');
            $('.login-menu').animate({
              right:"-=350"
             },500);
          }

          $(this).css({
            display:"none"
           });

        });

        $(".main-menu span").click(function(){
          $('.main-menu').addClass('open');
          $('.main-menu').animate({
           left:"-=220"
          },500);
          $(".menu-bg").css({
            display:"none"
           });
        });

        $(".login-menu span").click(function(){
          $('.login-menu').addClass('open');
          $('.login-menu').animate({
            right:"-=350"
          },500);
          $(".menu-bg").css({
            display:"none"
            });
          });

        $('.main-menu-button i').click(function(){
          if ($('.main-menu').hasClass('open')) {
            
            $('nav .main-menu-button .m').slideDown(200);
            $('.main-menu').removeClass('open');
            $('.main-menu').animate({
              left:0
            },500);
            $(".menu-bg").css({
              display:"block"
              });
          } else {

          $('.main-menu').addClass('open');
          $('.main-menu').animate({
            left:"-=200"
          },500);
          $(".menu-bg").css({
            display:"none"
            });
          }
        });

        $('.login-button').click(function(){
          if ($('.login-menu').hasClass('open')) {
            
            $('.login-menu').removeClass('open');
            $('.login-menu').animate({
              right:0
            },500);
            $(".menu-bg").css({
              display:"block"
              });
          } else {

          $('.login-menu').addClass('open');
          $('.login-menu').animate({
            right:"-=350"
          },500);
          $(".menu-bg").css({
            display:"none"
            });
          }
        });

        $('.main-menu ul li').hover(function(){
          var dmenu = $(this).data('menu');
          if(dmenu == "products") {
            $('.main-menu .products .dropdown-mobile').css('display','block');
          }else if (dmenu == "contacts") {
            $('.main-menu .contacts .dropdown-mobile').css('display','block');
          }
        },function(){
          $('.main-menu .dropdown-mobile').css('display','none');
        });

        $('.search-button').click(function(){
          $('.navbar .search').fadeToggle(500);
        });

        $('.login-menu h4').click(function() {
          $(this).addClass('tab-active');
          $(this).siblings().removeClass('tab-active');
          var data = $(this).data('tab');
          $('.data #'+data).show().siblings().hide();
        });

    $('#add-cart').click(function (e) {
        e.preventDefault;
        var id = itemId;
  


        if ($('#quantity').val() > 0) {

            var transItem = new Object();//dynamically fill model value with different model entity
            transItem.itemId = itemId;
            transItem.quantity = $('#quantity').val();
            transItem.propsValues =[];

            var i = 0;
            $(".sel-prop  option:selected").each(function () {

                if ($(this).has('option:selected')) {
                    var b = { itemPropId: this.value, propValue: this.text };
                    transItem.propsValues[i] = b;
                    i++;
                }
            });

                $.ajax({
                    url: '/Cart/AddWithQuantity',
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
            url: '/Cart/Add',
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

        function toggleDropdown (e) {
          const _d = $(e.target).closest('.dropdown'),
            _m = $('.dropdown-menu', _d);
          setTimeout(function(){
            const shouldOpen = e.type !== 'click' && _d.is(':hover');
            _m.toggleClass('show', shouldOpen);
            _d.toggleClass('show', shouldOpen);
            $('[data-toggle="dropdown"]', _d).attr('aria-expanded', shouldOpen);
          }, e.type === 'mouseleave' ? 300 : 0);
        }

        var quantitiy=0;
        $('.quantity-right-plus').click(function(e){
              
              // Stop acting like a button
              e.preventDefault();
              // Get the field name
              var quantity = parseInt($(this).parent().parent().find('input').val());
              
              // If is not undefined
                  
              $(this).parent().parent().find('input').val(quantity + 1);
          //total
            var price = parseFloat($('#price').text());
            $('#total').text((price * (quantity + 1)));
                
                  // Increment
              
          });

          $('.quantity-left-minus').click(function(e){
              // Stop acting like a button
              e.preventDefault();
              // Get the field name
              var quantity = parseInt($(this).parent().parent().find('input').val());
              
              // If is not undefined
            
                  // Increment
                  if(quantity>0){
                      $(this).parent().parent().find('input').val(quantity - 1);
                      //total
                      var price = parseFloat($('#price').text());
                      $('#total').text((price * (parseFloat(quantity) - 1)));
                  }
          });

        $('body')
          .on('mouseenter mouseleave','.dropdown',toggleDropdown)
          .on('click', '.dropdown-menu a', toggleDropdown);
});

