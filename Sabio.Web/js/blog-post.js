sabio.page.startup = function () {
    $("#cmdShowComments").on("click", sabio.page.handlers.onShowComments);
    $("#cmdHideComments").on("click", sabio.page.handlers.onHideComments);
    $("#cmdShowForm").on("click", sabio.page.handlers.onShowForm);
    $("#cmdHideForm").on("click", sabio.page.handlers.onHideForm);
    $("#commentsForm").on("submit", sabio.page.handlers.onAddComment);
    $(".cmdReplyButton").on("click", sabio.page.handlers.onReplyButton);
    $("#replyForm").on("submit", sabio.page.handlers.onReplyAddComment);

}

sabio.page.showComments = function () {
    $(".comments").show(1000);
    $(".comments").removeClass("hide");
    if ($("#cmdHideComments").hasClass("hide")) {
        $("#cmdHideComments").removeClass("hide");
    }
}
sabio.page.hideComments = function () {
    $(".comments").hide(1000);
    $("#cmdHideComments").addClass("hide");
}

sabio.page.showForm = function () {
    $(".comments").show(1000);
    $(".comments-form").removeClass("hide");
}

sabio.page.hideForm = function () {
    $(".comments-form").addClass("hide");
    $("html").animate({
        scrollTop: $(".comments").position().top
    }, 900);
}

sabio.page.addComment = function (commentsForm, targetElement) {
    var newClone = sabio.page.prepareClone();
    newClone.removeClass("hidden");

    $(".commentName", newClone).html(commentsForm[0].value);
    $(".commentSubject", newClone).html(commentsForm[1].value);
    $(".commentMessage", newClone).html(commentsForm[2].value);

    if (commentsForm[0].value == "bob") {
        $("img", newClone).attr("src", "http://s3-ec.buzzfed.com/static/2014-02/enhanced/webdr04/28/14/anigif_enhanced-30636-1393615860-3.gif");
    }
    else if (commentsForm[0].value == "bill") {
        $("img", newClone).attr("src", "http://media.tumblr.com/tumblr_likyame7Nj1qznrqt.gif");
    }
    else {
    }
    targetElement.append(newClone);

}

sabio.page.prepareClone = function () {
        return $("#commentTemplate").clone().removeAttr("id");

    }

sabio.page.replyButtonTriggersModal = function (clickedReplyButton) {
    $(".modal").modal("show");
    sabio.page.lastReplyClicked = clickedReplyButton;

}

//not showing any business logic
sabio.page.handlers.onShowComments = function () {
    sabio.page.showComments();
}

sabio.page.handlers.onHideComments = function () {
    sabio.page.hideComments();
}

sabio.page.handlers.onShowForm = function () {
    sabio.page.showForm();
}

sabio.page.handlers.onHideForm = function () {
    sabio.page.hideForm();
}

sabio.page.handlers.onReplyButton = function (event) {
    var lastClickedReplyButton = $(this);
    sabio.page.replyButtonTriggersModal(lastClickedReplyButton);

}

sabio.page.handlers.onReplyAddComment = function (event) {
    event.preventDefault();
    $(".modal").modal("hide");
    var replyForm = $("#replyForm").serializeArray();
    var targetReply = sabio.page.lastReplyClicked;

    targetReply = targetReply.closest(".comment.clearfix");

    sabio.page.addComment(replyForm, targetReply);
}

sabio.page.handlers.onAddComment = function (event) {
    event.preventDefault();
    var commentsForm = $("#commentsForm").serializeArray();
    var targetCommentContainer = $("#commentsContainer");
    sabio.page.addComment(commentsForm, targetCommentContainer);
}


sabio.page.startup();

