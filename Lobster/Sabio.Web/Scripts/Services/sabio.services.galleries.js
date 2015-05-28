sabio.services.galleries = {};
sabio.services.galleries.post = function (form, onSuccess, onError) {
    var circuit = '/api/gallery/';
    var settings = {
        dataType: 'JSON',
        type: 'post',
        data: form,
    };
    var call = $.ajax(circuit, settings);
    call.done(onSuccess);
    call.fail(onError);
}

sabio.services.galleries.put = function (form, onSuccess, onError) {
    var circuit = '/api/gallery/';
    var settings = {
        dataType: 'JSON',
        type: 'put',
        data: form,
    };
    var call = $.ajax(circuit, settings);
    call.done(onSuccess);
    call.fail(onError);
}

sabio.services.galleries.get = function (onSuccess, onError) {
    var circuit = '/api/gallery/';
    var settings = {
        dataType: 'JSON',
        type: 'get',
        data: null,
    };
    var call = $.ajax(circuit, settings);
    call.done(onSuccess);
    call.fail(onError);
}

sabio.services.galleries.getGalleryById = function (galleryId, onSuccess, onError) {
    var circuit = '/api/gallery/manage/' + galleryId;
    var settings = {
        dataType: 'JSON',
        type: 'get',
        data: null,
    };
    var call = $.ajax(circuit, settings);
    call.done(onSuccess);
    call.fail(onError);
}

sabio.services.galleries.getGallery = function (galleryId, onSuccess, onError) {
    var circuit = '/api/gallery/images/'+galleryId;
    var settings = {
        dataType: 'JSON',
        type: 'get',
        data: null,
    };
    var call = $.ajax(circuit, settings);
    call.done(onSuccess);
    call.fail(onError);
}