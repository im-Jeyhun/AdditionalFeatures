$("#save-add-modal").on("click", function (e) {

    let table = $("#author-area");

    let form = $("#add-author-form");
    let container = $("#add-author-container");
    let url = form.attr("action");


    $.ajax({
        url: url,
        type: "POST",
        data: form.serialize(),
        complete: function (result) {

            console.log(result.responseText)

            if (result.status == 400) {
                container.html(result.responseText)
            }
            else if (result.status == 201) {
                table.prepend(result.responseText)
                e.target.setAttribute("data-bs-dismiss", "modal");
            }
            else {
                alert("Something went wrong pls try again");
            }
        }

    })

    console.log(e.target)


        ;
})