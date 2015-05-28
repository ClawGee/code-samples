

sabio.services.experiences.get = function (onSuccess, onError) {

    var url = "/api/experiences/user"

    var settings = {

        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        type: "GET",

        dataType: "json",
        success: onSuccess,
        error: onError,

    };

    $.ajax(url, settings)
}


//*****POST***
//FUNCTION - POST/CREATE/ADD - NEW EXPERIENCE - SUBMITS With AJAX CALL that SERIALIZES the form
sabio.services.experiences.submitTheForm = function (myData, onSuccess, onError) {

    $.ajax({
         type: "POST"
        , url: "/api/experiences"
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: myData
        , dataType: "json"
        , success: onSuccess
        , error: onError
    });
}


//***GET***
//FUNCTION - GET/READ/VIEW - US STATES FOR DROPDOWN LIS - LIST With JSON & AJAX CALL - 
sabio.services.experiences.getUsStates = function (onSuccess, onError) {

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

//FUNCTION - GET/READ/VIEW - ALL EXPERIENCE DATA - LIST With JSON & AJAX CALL - NO DATA to call because its only a READ/VIEW
sabio.services.experiences.getExperienceData = function (onSuccess, onError) {

    $.ajax({
        type: "GET"
        , url: "/api/experiences/user"
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        //, data: myData
        , dataType: "json"
        , success: onSuccess
        , error: onError
    });
}

//FUNCTION - GET/READ/VIEW - BY UID - LIST With AJAX CALL for only 1 uid
sabio.services.experiences.getExperienceDataByUid = function (uid, onSuccess, onError) {  //took out myData bcuz GET has no data to get

    $.ajax({
        type: "GET"
        , url: "/api/experiences/" + uid
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        //, data: myData
        , dataType: "json"
        , success: onSuccess
        , error: onError
    });
}


//***PUT***
//FUNCTION - PUT/UPDATE/EDIT - BY UID - SUBMITS With AJAX CALL that SERIALIZES the form
sabio.services.experiences.update = function (uid, myData, onSuccess, onError) {

    $.ajax({
         type: "PUT"
        , url: "/api/experiences/" + uid
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: myData
        , dataType: "json"
        , success: onSuccess
        , error: onError
    });
}
        
     





