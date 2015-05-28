if (!sabio.tests)
{
	sabio.tests = { services: { employees: {}}};
}


sabio.tests.services.employees.get = function(onSuccess, onError)
{

	var url = "/api/testcenter/employees"

	var settings = {

		contentType: "application/x-www-form-urlencoded; charset=UTF-8",
		type: "GET",
	
		dataType: "json",
		success: onSuccess,
		error: onError,

	};

	$.ajax(url, settings)
}