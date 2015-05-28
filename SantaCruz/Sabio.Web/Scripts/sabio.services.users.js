
sabio.services.users.createAddress = function (serializedAddressForm, onSuccess, onError) {
    console.log(serializedAddressForm); 
    $.ajax({
        type: "POST",
        url: "/api/users/addresses",
        data: serializedAddressForm,
        dataType: "json",
        success: onSuccess,
        error: onError
    });
}
sabio.services.users.updateAddress = function (uid, myData, onSuccess, onError) {

    $.ajax({
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        type: "PUT",
        url: "/api/users/addresses/" + uid,
        data: myData,
        dataType: "json",
        success: onSuccess,
        error: onError
    });
}


sabio.services.users.getAddress = function (uid, onSuccess, onError) {
    var url = "/api/users/addresses/" + uid;
    var settings =
        {
            cache: false
            , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
            , dataType: "json"
            , success: onSuccess
            , error: onError
            , type: "GET"
        };
    $.ajax(url, settings);

}


sabio.services.users.getAddresses = function (onSuccess, onError) {

    var url = "/api/users/addresses";
    var myData = null;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: myData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "GET"
    };

    $.ajax(url, settings);

}

sabio.services.users.updateAccountInfo = function (myData, onSuccess, onError) {

    $.ajax({
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        type: "PUT",
        url: "/api/accounts/",
        data: myData,
        dataType: "json",
        success: onSuccess,
        error: onError
    });
}

sabio.services.users.getAccountInfo = function (onSuccess, onError) {
    var url = "/api/accounts/";
    var settings =
        {
            cache: false
            , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
            , dataType: "json"
            , success: onSuccess
            , error: onError
            , type: "GET"
        };
    $.ajax(url, settings);

}

sabio.services.users.postFile = function (value, onSuccess, onError) { //pass file 

    var url = "/api/accounts/upload";
    var settings = 
        {
        type: "POST",
        contentType: false,
        processData: false,
        data: value,
        enctype: 'multipart/form-data',
        success: onSuccess,
        error: onError
        }   

    $.ajax(url, settings); 
}


