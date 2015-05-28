sabio.services.developer = {};
sabio.services.developer.post = function (form, onSuccess, onError) {  
    var url = '/api/developer/';
    var settings = {
        dataType: 'JSON',
        type: 'post',
        data: form,
        success: onSuccess,
        error: onError
    };
    $.ajax(url, settings);
}


sabio.services.developer.put = function (form, onSuccess, onError) {
    var url = '/api/developer/';
    var settings = {
        dataType: 'JSON',
        type: 'put',
        data: form,
        success: onSuccess,
        error: onError
    };
    $.ajax(url, settings);
}

sabio.services.developer.get = function (onSuccess, onError) {
    var url = '/api/developer/';
    var settings = {
        dataType: 'JSON',
        type: 'get',
        data: null,
        success: onSuccess,
        error: onError
    };
    $.ajax(url, settings);
}  

sabio.services.developer.getById = function (uid, onSuccess, onError) {
    var url = '/api/public/developer/' + uid;
    var settings = {
        dataType: 'JSON',
        type: 'get',
        data: null,
        success: onSuccess,
        error: onError
    };
    $.ajax(url, settings);
}

sabio.services.developer.postResume = function (form, onSuccess, onError) {
    var url = '/api/developer/resume/';
    var settings = {
        dataType: 'JSON',
        type: 'post',
        data: form,
        success: onSuccess,
        error: onError
    };
    $.ajax(url, settings);
}