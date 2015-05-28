sabio.services.messaging = {};
sabio.services.messaging.postMessage = function (form, onSuccess, onError) {
    var url = '/api/message/';
    var settings = {
        dataType: 'JSON',
        type: 'post',
        data: form,
        success: onSuccess,
        error: onError,
    };
    $.ajax(url, settings);
}

sabio.services.messaging.postReply = function (form, onSuccess, onError) {
    var url = '/api/message/reply/';
    var settings = {
        dataType: 'JSON',
        type: 'post',
        data: form,
        success: onSuccess,
        error: onError,
    };
    $.ajax(url, settings);
}

sabio.services.messaging.getMessagesByConversationId = function (conversationId, onSuccess, onError) {
    var url = '/api/conversation/messages/' + conversationId;
    var settings = {
        dataType: 'JSON',
        type: 'get',
        data: null,
        success: onSuccess,
        error: onError,
    };
    $.ajax(url, settings);
}

sabio.services.messaging.getConversationsByUserId = function (onSuccess, onError) {
    var url = '/api/conversation/';
    var settings = {
        dataType: 'JSON',
        type: 'get',
        data: null,
        success: onSuccess,
        error: onError,
    };
    $.ajax(url, settings);
}

sabio.services.messaging.getConversationsByRecipientId = function (onSuccess, onError) {
    var url = '/api/conversation/received/';
    var settings = {
        dataType: 'JSON',
        type: 'get',
        data: null,
        success: onSuccess,
        error: onError,
    };
    $.ajax(url, settings);
}

sabio.services.messaging.getAllUsers = function (onSuccess, onError) {
    var url = '/api/user/all/';
    var settings = {
        dataType: 'JSON',
        type: 'get',
        data: null,
        success: onSuccess,
        error: onError,
    };
    $.ajax(url, settings);
}

sabio.services.messaging.getConvoMetaData = function (conversationId, onSuccess, onError) {
    var url = '/api/conversation/messages/meta/' + conversationId;
    var settings = {
        dataType: 'JSON',
        type: 'get',
        data: null,
        success: onSuccess,
        error: onError,
    };
    $.ajax(url, settings);
}