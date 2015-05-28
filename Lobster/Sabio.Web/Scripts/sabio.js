var sabio = {
    layout: {
        handlers: {},
        entityType: null,
        entityId: null
    },
    page: {
        handlers: {},
        startUp: null
    },
    rating: {
        handlers: {},
        startUp: null
    },
    services: {}
};

sabio.layout.handlers.showLoader = function () {
    //console.log("showing lobster");
    $body = $("body");
    $body.addClass("loading");
};


sabio.layout.handlers.hideLoader = function () {
    //console.log("hiding lobster");
    $body = $("body");
    $body.removeClass("loading");
};


sabio.layout.startUp = function () {

    console.debug("sabio.layout.startUp");

    //this does a null check on sabio.page.startUp
    if (sabio.page.startUp) {
        console.debug("sabio.page.startUp");
        sabio.page.startUp();
    }
    //this does a null check on sabio.rating.startUp
    //if (sabio.rating.startUp) {
    //    console.debug("sabio.rating.startUp");
    //    sabio.rating.startUp();
    //}
    
    $(document).bind({
        ajaxSend: sabio.layout.handlers.showLoader,
        ajaxComplete: sabio.layout.handlers.hideLoader
    });
};
$(document).ready(sabio.layout.startUp);