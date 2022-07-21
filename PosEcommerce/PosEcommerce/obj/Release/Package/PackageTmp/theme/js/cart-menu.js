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

                  //url: '@Url.Action("ViewCartItems","Cart")',
                  url: "Cart/ViewCartItems",
                  type: "GET",
                  dataType: "json",
                  success: function (data) {
                      alert();
                      $.each(data, function (index, item) {
                          divCartItemsContent +=
                              ` <div class="card my-3 item">
                <div class="cancel-item"><i class="fa fa-close fa-sm"></i></div>
                <div class="row no-gutters">
                    <div class="col-4">
                        <img src="~/theme/images/company-profile-background.jpg" alt="..." style="width: 100%; height: 100%;">
                    </div>
                    <div class="col-8">
                        <div class="card-body">
                            <h5 class="card-title">${item.name}</h5>
                            <p><span>${Global.resourcemanager.GetString("Quantity").ToString()}:</span> ${item.quantity}</p>
                        </div>
                    </div>
                </div>
            </div>`;

                      });
                      $('#div_cart_items').html(
                          divCartItemsContent
                      );
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

