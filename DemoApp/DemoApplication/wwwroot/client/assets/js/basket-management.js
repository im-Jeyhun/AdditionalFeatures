var addBtns = document.querySelectorAll(".add-product-to-basket-btn");

addBtns.forEach(b => {
    b.addEventListener("click", function (e) {

        e.preventDefault();
        var url = e.target.href;


        fetch(url)
            .then(response => response.text())
            .then(data => {
                console.log(data)
                $('.cart-block').html(data);
            })
        console.log(e.target.href)
    })

})




var deleteBtns = document.querySelectorAll(".remove-product-to-basket-btn");

$(document).on('click', ".remove-product-to-basket-btn" , function (e) {
    e.preventDefault();
    var url = e.target.href;


    fetch(url)
        .then(response => response.text())
        .then(data => {
            console.log(data)
            $('.cart-block').html(data);
        })
})

//deleteBtns.forEach(b2 => {
//    b2.addEventListener("click", function (e) {

//        e.preventDefault();

//        console.log("salam")



//        var url = e.target.href;


//        fetch(url)
//            .then(response => response.text())
//            .then(data => {
//                console.log(data)
//                $('.cart-block').html(data);
//            })
       
//    })

//})

