sabio.services.jobs = {};
sabio.services.jobs.post = function (form, onSuccess, onError) {
    var url = '/api/jobs/';
    var settings = {
        dataType: 'JSON',
        type: 'post',
        data: form,
        success: onSuccess,
        error: onError,
    };
    $.ajax(url, settings);
}


sabio.services.jobs.put = function (form, onSuccess, onError) {
    var url = '/api/jobs/';
    var settings = {
        dataType: 'JSON',
        type: 'put',
        data: form,
        success: onSuccess,
        error: onError
    };
    $.ajax(url, settings);
}


sabio.services.jobs.getByJobId = function (jobId, onSuccess, onError) {
    var url = '/api/jobs/' + jobId;
    var settings = {
        dataType: 'JSON',
        type: 'get',
        data: null,
        success: onSuccess,
        error: onError
    };
    $.ajax(url, settings);
}


sabio.services.jobs.getJobs = function (onSuccess, onError) {
    var url = '/api/jobs/';
    var settings = {
        dataType: 'JSON',
        type: 'get',
        data: null,
        success: onSuccess,
        error: onError
    };
    $.ajax(url, settings);
}

//Get Jobs by Status (Pending)

sabio.services.jobs.getPendingJobs = function (onSuccess, onError) {
    var url = '/api/admin/jobs/pending';
    var settings = {
        dataType: 'JSON',
        type: 'get',
        data: null,
        success: onSuccess,
        error: onError
    };
    $.ajax(url, settings);
}


//Get Jobs by Status (Approved)

//sabio.services.jobs.getApprovedJobs = function (onSuccess, onError) {
//    var url = '/api/admin/jobs/approved';
//    var settings = {
//        dataType: 'JSON',
//        type: 'get',
//        data: null,
//        success: onSuccess,
//        error: onError
//    };
//    $.ajax(url, settings);
//}