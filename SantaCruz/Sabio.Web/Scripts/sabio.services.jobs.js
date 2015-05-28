

//alert("Hello");

//sabio.services.jobs = {};

sabio.services.jobs.create = function (serializedForm, onSuccess, onError) {

        var url = "/api/jobs/create";
        var settings = {
            cache: false,
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            data: serializedForm,
            dataType: "json",
            success: onSuccess,
            error: onError,
            type: "POST"
        };

        $.ajax(url, settings);
        console.log(serializedForm);

}

