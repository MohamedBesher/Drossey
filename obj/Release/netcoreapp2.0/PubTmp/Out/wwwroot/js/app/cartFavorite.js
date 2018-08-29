var itemCount;
var favoriteCount;
var allPrice;
$(function () {
    itemCount = Number($(".itm-cont").html());
    favoriteCount = Number($(".favoriteCount").html());
   // var str = $("#cartSpan").html();
    //str = str.replace(/\s/g, '');
    //str = str.split("دورات");
  //  allPrice = Number(str = str[1].split("ريال")[0]);



});

function onsuccessDeletion(data) {

    NotificationService.displaySuccess("تم الحذف بنجاح", "تنبية !");

}
function onFailureDeletion(jqXHR, textStatus, errorThrown) {

    if (jqXHR.responseText === "NotFound")
        NotificationService.displayError("هذا المادة الدراسية غير موجودة", " !تحذير");

    else if (jqXHR.responseText === "Error")
        NotificationService.displayError("هذا المادة الدراسية مستخدمة لا يمكن حذفها .", " !تحذير");

    else if (jqXHR.responseText === "BooksUsedSubjects") {
        NotificationService.displayError("يوجد كتب داخل هذة المادة الدراسية.", " !تحذير");

    }



}

function addToWishlist(id) {

    $.ajax({
        type: "GET",
        url: "/WishList/Create/" + id,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $("#wish" + id).html('<img src="assets/img/removeFavourite.png"  class="img-responsive center-block" > ');
            $("#wish" + id).attr("onclick", "RemoveWishlist(" + id + ")");
            favoriteCount++;
            //allPrice = allPrice + response;
            $(".favoriteDiv").html(` <i class="glyph-icon flaticon-shapes nav-right__item__icon"></i>
                                <span class="nav-right__item__notification favoriteCount">`+ favoriteCount + `</span>`);
        },
        error: function (response) {

            console.log(response);
        }
    });


}
function addToCart(id) {

    $.ajax({
        type: "GET",
        url: "/Cart/Create/" + id,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $("#cart" + id).html('<img src="assets/img/rubbish-bin.png">');
            $("#cart" + id).attr("onclick", "removeCart(" + id + ")");
            itemCount++;
            //allPrice = allPrice + response;
            $(".cartDiv").html(` <i class="glyph-icon flaticon-commerce-1 nav-right__item__icon"></i>
                                <span class="nav-right__item__notification">`+ itemCount + `</span>`);
            console.log(response);
        },
        error: function (response) {

            console.log(response);
        }
    });


}
function RemoveWishlist(id) {
    //debugger;
    $.ajax({
        type: "POST",
        url: "/WishList/Delete/" + id,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $("#wish" + id).html('<img src="assets/img/heart-2.png" class="img-responsive center-block">');
            $("#wish" + id).attr("onclick", "addToWishlist(" + id + ")");
            favoriteCount--;
            $(".favoriteDiv").html(` <i class="glyph-icon flaticon-shapes nav-right__item__icon"></i>
                                <span class="nav-right__item__notification favoriteCount">`+ favoriteCount + `</span>`);
        },
        error: function (response) {
            //success
            if (response.readyState == 4) {
                $("#wish" + id).html('<img src="assets/img/heart-2.png" class="img-responsive center-block">');
                $("#wish" + id).attr("onclick", "addToWishlist(" + id + ")");
                console.log(response);
                favoriteCount--;
            
                $(".favoriteDiv").html(` <i class="glyph-icon flaticon-shapes nav-right__item__icon"></i>
                                <span class="nav-right__item__notification favoriteCount">`+ favoriteCount + `</span>`);

            }
            console.log(response);
        }
    });

}
function removeCart(id) {
    // debugger;
    $.ajax({
        type: "POST",
        url: "/Cart/Delete/" + id,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $("#cart" + id).html('<span class="glyph-icon flaticon-interface courses__hover-content__icon"></span>');

            $("#cart" + id).attr("onclick", "addToCart(" + id + ")");
            itemCount--;
           // allPrice = allPrice - response;
            $(".cartDiv").html(` <i class="glyph-icon flaticon-commerce-1 nav-right__item__icon"></i>
                                <span class="nav-right__item__notification">`+ itemCount + `</span>`);
            console.log(response);
        },
        error: function (response) {
            if (response.readyState == 4) {
                $("#cart" + id).html('<span class="glyph-icon flaticon-interface courses__hover-content__icon"></span>');

                $("#cart" + id).attr("onclick", "addToCart(" + id + ")");
                itemCount--;
                $(".cartDiv").html(` <i class="glyph-icon flaticon-commerce-1 nav-right__item__icon"></i>
                                <span class="nav-right__item__notification">`+ itemCount + `</span>`);
                console.log(response);

            }
            console.log(response);
        }
    });

}