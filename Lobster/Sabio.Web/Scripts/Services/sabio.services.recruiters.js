sabio.services.recruiters = {};
sabio.services.recruiters.post = function(form, onSuccess, onError)
{
    var circuit = '/api/recruiter/';
    var settings = {
        dataType: 'JSON',
        type: 'post',
        data: form,
    };
    var call = $.ajax(circuit, settings);
    call.done(onSuccess);
    call.fail(onError);
}

sabio.services.recruiters.put = function(form, onSuccess, onError)
{
    var circuit = '/api/recruiter/';
    var settings = {
        dataType: 'JSON',
        type: 'put',
        data: form,
    };
    var call = $.ajax(circuit, settings);
    call.done(onSuccess);
    call.fail(onError);
}

sabio.services.recruiters.get = function(onSuccess, onError)
{
    var circuit = '/api/recruiter/';
    var settings = {
        dataType: 'JSON',
        type: 'get',
        data: null,
    };
    var call = $.ajax(circuit, settings);
    call.done(onSuccess);
    call.fail(onError);
}
