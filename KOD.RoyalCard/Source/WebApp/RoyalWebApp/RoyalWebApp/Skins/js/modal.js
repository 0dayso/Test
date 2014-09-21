$(document).ready(function () {

    //select all the a tag with name equal to modal
    $('a[name=modal]').click(function (e) {
        //Cancel the link behavior
        e.preventDefault();

        //Get the A tag
        var id = $(this).attr('href');
        OpenDiv(id);
    });
    closPopDiv();

});
function closPopDiv() {
    $(document).ready(function () {

        //if close button is clicked
        $('.window .close').click(function (e) {
            //Cancel the link behavior
            e.preventDefault();

            $('#mask').fadeOut('slow');
            $('.window').fadeOut('slow');
        });      
        //if mask is clicked
        //        $('#mask').click(function () {
        //            $(this).hide();
        //            $('.window').hide();
        //        });
    });
}
function OpenDiv(id) {

    //Get the screen height and width
    var maskHeight = $(document).height();
    var maskWidth = $(window).width();

    //Set heigth and width to mask to fill up the whole screen
    $('#mask').css({ 'width': maskWidth, 'height': maskHeight });

    //transition effect		
    $('#mask').fadeIn(1000);
    $('#mask').fadeTo("slow", 0.8);

    //Get the window height and width
    var winH = $(window).height();
    var winW = $(window).width();

    //Set the popup window to center
    $(id).css('top', winH / 2 - $(id).height() / 2);
    $(id).css('left', winW / 2 - $(id).width() / 2);

    //transition effect
    $(id).fadeIn(2000);
}

function closeDiv() {
    $(document).ready(function () {

        $('#mask').fadeOut('slow');
        $('#DivLoginMsg').fadeOut('slow');
    });
    window.location.href = "index.html";
}
function closeAlertDiv() {
    $(document).ready(function () {

        $('#mask').fadeOut('slow');
        $('#PopupMsgAlert').fadeOut('slow');
    });    
}
