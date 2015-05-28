sabio.services.address = {};

sabio.services.address.post = function (dataSet, onSuccess, onError) {
    var url = "/api/address/";
    var setting = {
        dataType: 'JSON',
        data: dataSet,
        type: "POST",
        success: onSuccess,
        error: onError
    };
    $.ajax(url, setting);
}

sabio.services.address.put = function (addressGuid, dataSet, onSuccess, onError) {
    var url = "/api/address/" + addressGuid;
    var setting = {
        data: dataSet,
        dataType: "JSON",
        type: 'put',
        success: onSuccess,
        error: onError
    };
    $.ajax(url, setting);
}

sabio.services.address.get = function (onSuccess, onError) {
    var url = "/api/address/";
    var setting = {
        data: null,
        dataType: "JSON",
        type: 'get',
        success: onSuccess,
        error: onError
    };
    $.ajax(url, setting);
}

sabio.services.address.getStates = function (onSuccess, onError) {
    var url = "/api/geography/states";
    var setting = {
        data: null,
        dataType: "JSON",
        type: 'get',
        success: onSuccess,
        error: onError
    };
    $.ajax(url, setting);
}

sabio.services.address.getAddressRadius = function (lat, lng, distance, onSuccess, onError) {
    var url = "/api/address/RadiusSearch";
    var setting = {
        data: {
            Lat: lat,
            Lng: lng,
            Distance: distance
        },
        dataType: "JSON",
        type: 'get',
        success: onSuccess,
        error: onError
    };
    $.ajax(url, setting);
}

