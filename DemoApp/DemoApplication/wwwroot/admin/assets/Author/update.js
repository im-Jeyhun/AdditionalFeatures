
$(document).on("click", ".update-button", function (e) {

    let url = e.target.href;

    let container = $("#update-author-container");

    let targetId = $(e.target.parentElement.parentElement).data("id");

    $("#target-url").val(url)
    $("#target-id").val(targetId)

    console.log("salam")


    $.ajax({
        type: "GET",
        url: url,
        complete: function (result) {
            if (result.status == 200) {

                console.log(result.responseText)
                container.html(result.responseText);
            }
            else {
                alert("Something went wrong")
            }
        }

    })
})



$("#save-update-modal").on("click", function (e) {

    let updateModal = $("#updateAuthorModal");

    let container = $("#update-author-container");

    let firstName = $("#FirstName").val();
    let lastName = $("#lastName").val();


    let targetUrl = $("#target-url").val();
    let targetId = $("#target-id").val();

    let targetRow = $(`.author-record[data-id='${targetId}']`);

    $.ajax({
        url: targetUrl,
        type: "PUT",
        data: {
            FirstName: firstName,
            LastName: lastName
        },
        complete: function (result) {

            console.log(result.responseText)+

            if (result.status == 400) {
                container.html(result.responseText)
            }
            else if (result.status == 200) {
                targetRow.replaceWith(result.responseText);
                updateModal.modal("hide");
            }
            else {
                alert("Something went wrong pls try again");
            }
        }

    })


})


