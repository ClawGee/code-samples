
var sabio = {
    layout: {
        handlers: {}
    }
    , page: {
        handlers: {}
        , startUp: null
    }
    , services: {
        accounts: {},
        admin: {},
        jobs: {},
        jobseeker: {},
        experiences: {},
        users: {},
        geography: {},
        projects: {},
        recruiters: {},
        contact: {},
        profile: {},
        developer: {}

    }
};

//console.warn("In sabio file"); console.log(sabio);

sabio.layout.startUp = function () {

    //console.debug("sabio.layout.startUp");
    if ($("#cmdGlobalLogin").length > 0) {
        $("#cmdGlobalLogin").on("click", sabio.layout.handlers.onGlobalLogIn);
        sabio.layout.setUpValidationRules();
    }
    //jason log out
    $("#cmdGlobalLogOut").on("click", sabio.layout.handlers.onGlobalLogOut);
    //this does a null check on sabio.page.startUp
    if (sabio.page.startUp) {
        console.debug("sabio.page.startUp");
        sabio.page.startUp();
    }
};

sabio.layout.showLayoutModal = function (title, body, button) {
    $("#layoutModalTitleContent").html(title);
    $("#layoutModalBodyContent").html(body);
    $("#layoutModalButton").html(button);
    $("#layoutModalContainer").modal("show");
}

sabio.layout.hideLayoutModal = function (event) {
    event.preventDefault(); 
    $("#layoutModalButton").modal(hide);
}

//Handlers for global login 
sabio.layout.handlers.onGlobalLogIn = function (event) {
    event.preventDefault();
    console.log("in on global login")
    //var inputField = $("#globalFormLogin").serialize();
    //console.log(inputField); //this is an empty string, why?
    
    //jason - refactored to accounts controller.
    var url = "/api/accounts/login"; 

    var myData = $("#globalFormLogin").serialize();
    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: myData
        , dataType: "json"
        , success: sabio.layout.handlers.logInSuccess
        , error: sabio.layout.handlers.logInError
        , type: "POST"
    };

    $.ajax(url, settings);
};

//jason
sabio.layout.handlers.onGlobalLogOut = function (event) {
    event.preventDefault();
    var url = "/api/accounts/logout";
    var settings = {
        cache: false,
        dataType: "json",
        success: sabio.layout.handlers.logOutSuccess,
        error: sabio.layout.handlers.logOutError,
        type: "GET"
    };
    $.ajax(url, settings);
}
sabio.layout.handlers.logOutSuccess = function () {
    window.location.href = "/";
}
sabio.layout.handlers.logOutError = function () {
    alert("Error! You are not logged out!");
}

sabio.layout.setUpValidationRules = function () {
    console.log("In set up validation in Layout"); 
        var validationOption = {
            rules: {
                loginPassword: "required",
                email: {
                    required: true,
                    email: true
                }
            },
            messages: {
                loginPassword: "*Please Enter Password!!!!",
                email: {
                    required: "*please enter your email!!!!!",
                    email: "Your email address must be in the format of name@domain.com"
                }
            }
};
        //validationOption.rules.newField = {};
        // validationOption.rules.newField.required = true; 
        $("#globalFormLogin").validate(validationOption);
    }

//Handlers
    sabio.layout.handlers.logInSuccess = function () {
        window.location.href = window.location.href; 
        console.log("login success!");
    }
    sabio.layout.handlers.logInError = function () {
        console.log("Error on login"); 
    }

$(document).ready(sabio.layout.startUp);