
//*****POST***


//FUNCTION - POST- Create Skills 
sabio.services.developer.createSkills = function (serializedForm, onSuccess, onError) {

	$.ajax({
		type: "POST",
		url: "/api/developer/create",
		data: serializedForm,
		dataType: "json",
		success: onSuccess,
		error: onError
	});

}


//FUNCTION - POST- Upload Profile Picture

sabio.services.developer.postProfilePicFile = function (value, onSuccess, onError) { //pass file 

    $.ajax({
        type: "POST",
        url: "/api/developer/upload",
        contentType: false,
        processData: false,
        data: value,
        enctype: 'multipart/form-data',
        success: onSuccess,
        error: onError
    });
}


//***GET***


//FUNCTION - GET 

sabio.services.developer.get = function (onSuccess, onError) {

	$.ajax({
		type: "GET",
		url: "/api/developer/view",
		dataType: "json",
		success: onSuccess,
		error: onError
	});
}


