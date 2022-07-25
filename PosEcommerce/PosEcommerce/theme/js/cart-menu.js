$(document).ready(function(){
    'use strict';

        $(".cart-menu span").click(function(){
          $('.cart-menu').addClass('open');
          $('.cart-menu').animate({
           right:"-=350"
          },500);
          $(".menu-bg").css({
            display:"none"
          });
            

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


              var divCartItemsContent = '';
              $.ajax({

                  url: "Cart/ViewCartItems",
                  type: "GET",
                  dataType: "json",
                  success: function (data) {

                      if (data.cartCount == '0') {
                          $(".no-item p").removeClass('d-none').addClass('d-block');
                          $(".card").removeClass('d-block').addClass('d-none');

                      }
                      else {

                          $.each(data.cartItems, function (index, item) {
                              divCartItemsContent +=
                                  ` <div class="card my-3 item">
                                    <div class="cancel-item"><i class="fa fa-close fa-sm"></i></div>
                                    <div class="row no-gutters">
                                        <div class="col-4">
                                            <img src="${data.imagePath}${item.image}" alt="..." style="width: 100%; height: 100%;">
                                        </div>
                                        <div class="col-8">
                                            <div class="card-body">
                                                <h5 class="card-title">${item.itemName}</h5>
                                                <p><span>QTY:</span> ${item.quantity}</p>
                                            </div>
                                        </div>
                                    </div>
                                </div>`;
                          });

                          $(".card").removeClass('d-none').addClass('d-block');
                        
                          $('#div_cart_items').html(divCartItemsContent);
                      }
                      $('#sp-cart-count').html(data.cartCountStr);
                  },
                  failure: function () {
                      alert("fail");
                  },
                  error: function () {
                      alert("error");
                  }
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


        $('.cart-menu .cart-items .card .cancel-item').click(function(){
          $(this).parent().remove();

          if($(".item") && $(".item").length){
            $('.cart-menu .cart-items .no-item p').removeClass('d-block').addClass('d-none');
          }else {
            $('.cart-menu .cart-items .no-item p').removeClass('d-none').addClass('d-block');
          }
        });



});

