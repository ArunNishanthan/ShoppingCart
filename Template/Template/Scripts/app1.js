
var fadeTime = 300;

$('.product-quantity input').change(function () {
    updateQuantity(this);
});

$('.product-removal button').click(function () {
    removeItem(this);
});

recalculateCart();

function recalculateCart() {
    var subtotal = 0;

    $('.product').each(function () {
        subtotal += parseFloat($(this).children('.product-line-price').text());
    });

   
    var total = subtotal;

    $('.totals-value').fadeOut(fadeTime, function () {
        $('#cart-total').html(total.toFixed(2));
        if (total == 0) {
            $('.checkout').fadeOut(fadeTime);
        } else {
            $('.checkout').fadeIn(fadeTime);
        }
        $('.totals-value').fadeIn(fadeTime);
    });
}


function updateQuantity(quantityInput) {

    var productRow = $(quantityInput).parent().parent();
    var price = parseFloat(productRow.children('.product-price').text());
    var quantity = $(quantityInput).val();
    var linePrice = price * quantity;

    productRow.children('.product-line-price').each(function () {
        $(this).fadeOut(fadeTime, function () {
            $(this).text(linePrice.toFixed(2));
            recalculateCart();
            $(this).fadeIn(fadeTime);
        });
    });
}


function removeItem(removeButton) {

    var productRow = $(removeButton).parent().parent();
    productRow.slideUp(fadeTime, function () {
        productRow.remove();
        recalculateCart();
    });
}


$(document).on('click', '.remove-product', function (e) {
    if (e.target.classList.contains("remove-product")) {
        fetch("/product/DeleteCart?id=" + e.target.id).then(res => res.json()).then(data => {
        });

        e.target.parentNode.parentNode.remove();
    }

    let product = document.getElementsByClassName('product');
    if (product.length == 0) {
        document.getElementById('product-detail').innerText = "You have no items in the Cart";
        document.getElementById('shoppingCart').style.display = "none";
    }
});




$(document).on('click', '.checkout', function (e) {

    let productQuan = document.getElementsByClassName('product-quantity');
    let array=[];

    for (var i = 1; i < productQuan.length; i++) {

        let obj = {
            id: productQuan[i].childNodes[1].id,
            quantity: productQuan[i].childNodes[1].valueAsNumber
        };
        array.push(obj);
    }
   
    document.cookie = "ViewCart" + "=" + JSON.stringify(array) ;

});

$(document).on('click', '.save', function (e) {

    let productQuan = document.getElementsByClassName('product-quantity');
    let array = [];

    for (var i = 1; i < productQuan.length; i++) {

        let obj = {
            id: productQuan[i].childNodes[1].id,
            quantity: productQuan[i].childNodes[1].valueAsNumber
        };
        array.push(obj);
    }

    document.cookie = "ViewCart" + "=" + JSON.stringify(array);

});

recalculateCart();