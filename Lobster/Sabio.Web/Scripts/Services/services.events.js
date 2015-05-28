sabio.services.events = {};

sabio.services.events.post = function (formData, onSuccess, onError){
    var url = "/api/events/";
    var settings = {
        type: 'POST',
        data: formData,
        success: onSuccess,
        error: onError
    };
    $.ajax(url, settings);
};

sabio.services.events.put = function (uid, formData, onSuccess, onError, guid){
    var url = "/api/events/" + uid;
    var settings = {
        type: 'PUT',
        data: formData,
        dataType: "JSON",
        success: onSuccess,
        error: onError 
    };
    $.ajax(url, settings);
};

sabio.services.events.getEventByUid = function (uid, onSuccess, onError) {
    var url = "/api/events/" + uid;
    var settings = {
        dataType: 'JSON',
        type: 'GET',
        data: null,
        success: onSuccess,
        error: onError,
    };
    $.ajax(url, settings);
};

//GET all events made by users, so they can edit:
sabio.services.events.getEvents = function (currentPage, pageSize, onSuccess, onError) {
    var url = '/api/events/';
    var settings = {
        dataType: 'JSON',
        type: "GET",
        data: {
            currentPage: currentPage - 1,
            pageSize: pageSize
        },
        success: onSuccess,
        error: onError 
    };
    $.ajax(url, settings);
};
