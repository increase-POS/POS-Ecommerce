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

        var quantitiy=0;
        $('.quantity-right-plus').click(function(e){
              
              // Stop acting like a button
              e.preventDefault();
              // Get the field name
            var quantity = parseInt($(this).parent().parent().find('input').val());
              
              // If is not undefined
                  
              $(this).parent().parent().find('input').val(quantity + 1);
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
                      var price = parseFloat($('#price').text());
                      $('#total').text((price * (quantity -1)));
                  }
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
        $('body')
          .on('mouseenter mouseleave','.dropdown',toggleDropdown)
          .on('click', '.dropdown-menu a', toggleDropdown);
});

