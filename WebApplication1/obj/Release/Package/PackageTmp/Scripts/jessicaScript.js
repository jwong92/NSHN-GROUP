//NEWS - SAVE CAPTIONS - AJAX
$(document).ready(function () {
    $(".captionbtn").click(function (data) {
        data.currentTarget.nextElementSibling.innerHTML = "Caption updated";
    })


    $("#AnonCreateDonationBtn").click(function (e) {
        e.preventDefault();
    })
})