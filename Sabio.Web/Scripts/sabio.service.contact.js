


//sabio.service.contactUs = function (myData, onSuccess, onError) {
    
//    console.log(inputField);

//    var url = "/api/ContactUs/SendMsg";

//    var myData = $("#contactUsForm").serialize();
//    var settings = {
//        cache: false
//        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
//        , data: myData
//        , dataType: "json"
//        , success: onSuccess
//        , error: onError
//        , type: "POST"
//    };

//    $.ajax(url, settings);

    sabio.services.contact.post = function (myData, onSuccess, onError) {
        $.ajax({
            type: "POST"
           , url: "/api/ContactUs/SendMsg"
           , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
           , data: myData
           , dataType: "json"
           , success: onSuccess
           , error: onError
        });
    };



