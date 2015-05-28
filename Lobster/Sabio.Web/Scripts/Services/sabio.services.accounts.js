sabio.services.accounts = {};

sabio.services.accounts.get = function (onSuccess, onError) {
    var circuit = '/api/user/';
    var settings = {
        dataType: 'JSON',
        type: 'get',
        data: null,
    };
    var call = $.ajax(circuit, settings);
    call.done(onSuccess);
    call.fail(onError);
}

sabio.services.accounts.put = function (form, onSuccess, onError) {
    var circuit = '/api/account/';
    var settings = {
        dataType: 'JSON',
        type: 'put',
        data: form
    };
    var call = $.ajax(circuit, settings);
    call.done(onSuccess);
    call.fail(onError);
}

sabio.services.accounts.putPassword = function (form, onSuccess, onError) {
    var circuit = '/api/account/password';
    var settings = {
        dataType: 'JSON',
        type: 'put',
        data: form
    };
    var call = $.ajax(circuit, settings);
    call.done(onSuccess);
    call.fail(onError);
}

sabio.services.accounts.login = function (formData, onSuccess, onError) {
    var url = '/api/login/';
    var settings = {
        dataType: 'JSON',
        type: 'post',
        data: formData,
        success: onSuccess,
        error: onError
    };
    $.ajax(url, settings);
}

//Found in UsersApiController

sabio.services.accounts.getAll = function (onSuccess, onError) {
    var circuit = '/api/user/all';
    var settings = {
        dataType: 'JSON',
        type: 'get',
        data: null,
    };
    var call = $.ajax(circuit, settings);
    call.done(onSuccess);
    call.fail(onError);
}

//Found in AadminApiController
sabio.services.accounts.update = function (id, formData, onSuccess, onError) {
    var circuit = '/api/admin/user/' + id;
    var settings = {
        dataType: 'JSON',
        type: 'put',
        data: formData,
    };
    var call = $.ajax(circuit, settings);
    call.done(onSuccess);
    call.fail(onError);
}


/////////////// Pagination Services for Admin Page 


sabio.services.accounts.getAll = function (currentPage, pageSize, onSuccess, onError) {
    var url = '/api/user/alluser';
    var settings = {
        dataType: 'JSON',
        type: 'get',
        data: {
            currentPage: currentPage - 1,
            pageSize: pageSize,
        },
        success: onSuccess,
        error: onError
    };
    $.ajax(url, settings);
}


