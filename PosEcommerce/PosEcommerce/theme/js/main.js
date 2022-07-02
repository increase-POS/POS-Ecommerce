$(document).ready(function(){
    'use strict';

    // sync Navbar Links With Sections

        $(".scroll-to-top, .home-btn").click(function(e){
            e.preventDefault();

            $('html, body').animate({
                scrollTop: 0
            },1000);

        });

        // $(".menu-bg").click(function(){
        //     $('.main-menu').addClass('open');
        //     $('.cart-menu').addClass('open');
        //     $('.main-menu').animate({
        //      left:"-=200"
        //     },500);
        //     $('.cart-menu').animate({
        //       right:"-=270"
        //      },500);
        //     $(this).css({
        //       display:"none"
        //      });
        // });

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

        $(".cart-menu span").click(function(){
          $('.cart-menu').addClass('open');
          $('.cart-menu').animate({
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

        $('.cart-button i').click(function(){
          if ($('.cart-menu').hasClass('open')) {
            
            $('.cart-menu').removeClass('open');
            $('.cart-menu').animate({
              right:0
            },500);
            $(".menu-bg").css({
              display:"block"
              });
          } else {

          $('.cart-menu').addClass('open');
          $('.cart-menu').animate({
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

        $('body')
          .on('mouseenter mouseleave','.dropdown',toggleDropdown)
          .on('click', '.dropdown-menu a', toggleDropdown);
});

