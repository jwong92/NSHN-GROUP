$(document).ready(function () {

    $('.answer__div').hide();

    $('.button__div').click(function () {
        $('.answer__div').hide();
        $(this).next('.answer__div').show(500);
    });

    
});