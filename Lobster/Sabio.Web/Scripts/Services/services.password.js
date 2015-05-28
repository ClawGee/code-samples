sabio.services.password = {};
sabio.services.password.get = function (form, onSuccess, onError) {
    var url = '/api/password/';
    var settings = {
        dataType: 'JSON',
        type: 'post',
        data: form,
        success: onSuccess,
        error: onError
    };
    $.ajax(url, settings);
}

sabio.services.password.checkToken = function (guid, onSuccess, onError) {
    var url = '/api/password/confirm/'+guid;
    var settings = {
        dataType: 'JSON',
        type: 'get',
        data: null,
        success: onSuccess,
        error: onError
    };
    $.ajax(url, settings);
}

sabio.services.password.put=function(user, password, confirmPassword, onSuccess, onError){
    var url = '/api/password/';
    var settings = {
        dataType: 'JSON',
        type: 'put',
        data: {
            User: user,
            Password: password,
            ConfirmPassword: confirmPassword
        },
        success: onSuccess,
        error: onError
    };
    $.ajax(url,settings);
}


