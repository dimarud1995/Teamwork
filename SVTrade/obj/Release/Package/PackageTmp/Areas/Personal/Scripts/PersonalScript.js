$(document).ready(function () {
   
    $(window).scroll(function () {
        if ($(this).scrollTop() > 300) {

            $(".box-goup").css({ "left": "0px"});
        } else {
            $(".box-goup").css({ "left": "-150px" });
        }
    });
  
    
    
    $(".box-goup").click(function () {
        $("body").animate({ "scrollTop": 0 }, "fast");
    })

});
