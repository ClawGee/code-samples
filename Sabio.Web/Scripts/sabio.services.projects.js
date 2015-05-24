
/* CREATE a Project*/

sabio.services.projects.create = function (formData, onSuccess, onError) {

    var url = "/api/projects/";
    
    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: formData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"

    };
    $.ajax(url, settings);
}


/* UPDATE a Project*/

sabio.services.projects.update = function (myUid, formData, onSuccess, onError) {

    
    var url = "/api/projects/" + myUid;
    
    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: formData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "PUT"    //type of request
    };
    $.ajax(url, settings);
}


/* GET a Project by Uid*/

sabio.services.projects.get = function (projectuid, onSuccess, onError) {

    var url = "/api/projects/" + projectuid;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "GET"
    };
    $.ajax(url, settings);
}


/* Whole List of Projects (AppUserId)*/

sabio.services.projects.List = function (onSuccess, onError) {

    var url = "/api/projects/"; 

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "GET"
    };
    $.ajax(url, settings);
}



/*Upload a File*/

sabio.services.projects.upload = function (projectUid, data, onSuccess, onError) {

    var url = "/api/projects/upload/" + projectUid;

    var settings = {
        contentType: false
        ,processData: false
        , data: data
        , success: onSuccess
        , error: onError
        , type: "POST"
        , enctype: 'multipart/form-data'
    };
    $.ajax(url, settings);
}