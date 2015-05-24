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

sabio.page.startup = function () {

    $(".cmdReadMore").on("click", sabio.page.onCmdReadMore);
}

sabio.page.onCmdReadMore = function (event) {
    var lastClicked = $(this);
    sabio.page.lastClickedCmdReadMore = lastClicked;

    sabio.page.getPost(lastClicked);
}

sabio.page.getPost = function (lastClicked){

    var lastClickedPost = lastClicked.closest(".clearfix.blogpost");

    var lastClickedTitle = $(".title", lastClickedPost);
    var lastClickedAuthor = $(".submitted", lastClickedPost);
    var lastClickedPostContent = $("p", lastClickedPost);    

    sabio.page.showReadMore (lastClickedTitle, lastClickedAuthor, lastClickedPostContent);
}

sabio.page.clone = function (original) {

    return original.clone();

}


sabio.page.showReadMore = function (title, author, post) {


    $(".modal-title").html(sabio.page.clone(title));
    $(".modalPostContent").html(sabio.page.clone(post));
}



sabio.page.startup();