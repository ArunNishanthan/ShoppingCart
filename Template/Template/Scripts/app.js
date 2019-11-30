getAPI("");

document.getElementById("search").addEventListener("keyup", search);
let badge = document.getElementById("badge");

let pro = document.getElementById("product");
let prod = document.getElementById("pro");

let number = parseInt(badge.innerText);

function search(e) {
    pro.innerHTML = "";

    getAPI(e.target.value);
}


function getAPI(textValue) {
   
    fetch("/product/DisplayProduct?text=" + textValue).then(res => res.json()).then(data => {
        if (data.length == 0) {
            prod.innerHTML = "There are no products available for the search";
            prod.style.display = "inline-block";
        }
        else {
            prod.style.display = "none";
        }
        data.forEach(function (element) {
            
                pro.innerHTML += `<article>
                                <img src=${element.ImageUrl} >
                    <div class="text center1">

                        <h3>${element.Name}</h3>
                        <p>${element.ShortDescription}</p>
                        <p></p>
                        <button class="btn btn-primary btn-block add" id="${element.Id}">Add to Cart - $ ${element.Price}</button>
                    </div>
        </article>`;
        });

        
    })
}


$(document).on('click', '.add', function (e) {
    if (e.target.classList.contains("add")) {

        fetch("/product/AddToCart?id=" + e.target.id).then(res => res.json()).then(data => {
        });

        //e.target.classList.remove("add");
        //e.target.classList.add("added");
        //e.target.innerText = "Added";

        number++;
        badge.innerText = number;

        if (number <= 0) {
            badge.style.display = "none";
        }
        else {
            badge.style.display = "inline-block";
        }
    }
});



//$(document).on('click', '.added', function (e) {
//    if (e.target.classList.contains("added")) {

//        fetch("/product/DeleteCart?id=" + e.target.id).then(res => res.json()).then(data => {
//        });
//        e.target.classList.remove("added");
//        e.target.classList.add("add");
//        e.target.innerText = "Add Item"

//        number--;
//        badge.innerText = number;

//        if (number <= 0) {
//            badge.style.display = "none";
//        }
//        else {
//            badge.style.display = "inline-block";
//        }

//    }
//});


if (parseInt(badge.innerText)<=0) {
            badge.style.display = "none";
        }
        else {
            badge.style.display = "inline-block";
        }