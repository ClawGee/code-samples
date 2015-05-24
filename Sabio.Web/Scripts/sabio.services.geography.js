//***GET***
//FUNCTION - GET/READ/VIEW - US STATES FOR DROPDOWN LIS - LIST With JSON & AJAX CALL - 
sabio.services.geography.getUsStatesDropdown = function (onSuccess, onError) {
    $.ajax({
        type: "GET"
        , url: "/api/geography/states"
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        //       , data: myData
        , dataType: "json"
        , success: onSuccess
        , error: onError
    });
}
