//Public AJAX calls for the public pages tht do not require users to be logged in to view data

sabio.services.public = {};
sabio.services.public.getEmployer = function(guid, onSuccess, onError){
	var url = '/api/Public/employer/'+guid;
	var settings = {
		dataType: 'JSON',
		type: 'get',
		data: null,
		success: onSuccess,
		error: onError,
	};
	$.ajax(url, settings);
}

sabio.services.public.getRecruiter = function(guid, onSuccess, onError) {
    var circuit = '/api/Public/recruiter/'+guid;
    var settings = {
        dataType: 'JSON',
        type: 'get',
        data: null,
    };
    var call = $.ajax(circuit, settings);
    call.done(onSuccess);
    call.fail(onError);
}

sabio.services.public.getDeveloperProfileByUid = function (uid, onSuccess, onError) {
    var url = "/api/public/developer/" + uid;
    var settings = {
        datatype: 'JSON',
        type: 'get',
        data: null,
        success: onSuccess,
        error: onError
    };
    $.ajax(url, settings);
}

sabio.services.public.getProjectsByAppUserId = function (uid, onSuccess, onError) {

    var url = "/api/public/developer/projects/" + uid;

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

sabio.services.public.getByProjUid = function (projectUid, onSuccess, onError) {

    var url = "/api/public/projects/" + projectUid;

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
