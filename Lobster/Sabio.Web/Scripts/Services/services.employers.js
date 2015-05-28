sabio.services.employers = {};

sabio.services.employers.post = function (form, onSuccess, onError) {
    var url = '/api/Employer/create';
    var settings = {
        dataType: 'JSON',
        type: 'post',
        data: form,
        success: onSuccess,
        error: onError,
    };
    $.ajax(url, settings);
}


sabio.services.employers.put = function (form, onSuccess, onError) {
    var url = '/api/Employer/';
    var settings = {
        dataType: 'JSON',
        type: 'put',
        data: form,
        success: onSuccess,
        error: onError
    };
    $.ajax(url, settings);
}

sabio.services.employers.get = function (onSuccess, onError) {
    var url = '/api/Employer/';
    var settings = {
        dataType: 'JSON',
        type: 'get',
        data: null,
        success: onSuccess,
        error: onError
    };
    $.ajax(url, settings);
}


sabio.services.employers.getTag = function (currentPage, pageSize, tagName, onSuccess, onError) {

    $.ajax({

        type: "GET",
        url: "/api/employer/tag/" + tagName,
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        dataType: "json",
        data: {
            currentPage: currentPage,
            pageSize: pageSize
        },
        success: onSuccess,
        error: onError

    });
}




sabio.services.employers.loadImage = function (onSuccess, onError) {
    var url = '/api/Employer/image';
    var settings = {
        dataType: 'JSON',
        type: 'get',
        data: null,
        success: onSuccess,
        error: onError
    }
    $.ajax(url, settings);
}