sabio.services.resume = {};
sabio.services.resume.createResume = function (form, onSuccess, onError) {  
    var url = '/api/resume/';
    var settings = {
        dataType: 'JSON',
        type: 'post',
        data: form,
        success: onSuccess,
        error: onError
    };
    $.ajax(url, settings);
}

sabio.services.resume.getResume = function (onSuccess, onError) {
    var url = '/api/resume/';
    var settings = {
        dataType: 'JSON',
        type: 'get',
        data: null,
        success: onSuccess,
        error: onError
    };
    $.ajax(url, settings);
}

sabio.services.resume.updateResume = function (form, onSuccess, onError) {
    var url = '/api/resume/';
    var settings = {
        dataType: 'JSON',
        type: 'put',
        data: form,
        success: onSuccess,
        error: onError
    };
    $.ajax(url, settings);
}

sabio.services.resume.getFullResume = function (onSuccess, onError) {
    var url = '/api/resume/full/';
    var settings = {
        dataType: 'JSON',
        type: 'get',
        data: null,
        success: onSuccess,
        error: onError
    };
    $.ajax(url, settings);
}