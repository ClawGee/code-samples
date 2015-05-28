




sabio.services.recruiters.add = function (recruiterData, onSuccess, onError) {

	var url = "/api/Recruiters";
	var settings = {
		cache: false
		, contentType: "application/x-www-form-urlencoded; charset=UTF-8"
		, data: recruiterData
		, dataType: "json"
		, success: onSuccess
		, error: onError
		, type: "POST"
	};
	$.ajax(url, settings);
};

sabio.services.recruiters.get = function ( onSuccess, onError) {
	var url = "/api/recruiters/"
	var settings = {
		cache: false
	   , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
	   , data: null
	   , dataType: "json"
	   , success: onSuccess
	   , error: onError
	   , type: "GET"
	};
	$.ajax(url, settings);
};

sabio.services.recruiters.uidUpdate = function (hiddenUid, myData, onSuccess, onError) {

	var url = "/api/recruiters/" + hiddenUid;
	var settings = {
		cache: false
	   , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
	   , data: myData
	   , dataType: "json"
	   , success: onSuccess
	   , error: onError
	   , type: "PUT"
	};
	$.ajax(url, settings);
};


