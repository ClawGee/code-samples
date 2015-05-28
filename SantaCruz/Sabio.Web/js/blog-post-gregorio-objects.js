var sabio = {
    layout: {}
            ,
    page: {

        handlers: {},

        startUp: null
    },

    services: {
        blogs: {}
    }

};


sabio.layout.startUp = function () {

    console.debug("sabio.layout.startUp");

    //this does a null check on sabio.page.startUpC:\sf.code\Sabio.Learn\Sabio.Training\Sabio.Training.Web\images/client-2.png
    if (sabio.page.startUp) {
        sabio.page.startUp();
    }
};