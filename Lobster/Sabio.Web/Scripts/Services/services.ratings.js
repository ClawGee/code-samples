sabio.services.ratings = {};

sabio.services.ratings.getRating = function (ratingGuid, onSuccess, onError) {
    var url = '/api/rating/' + ratingGuid;
    var settings = {
        dataType: 'JSON',
        type: 'get',
        data: null,
        success: onSuccess,
        error: onError
    };
    $.ajax(url, settings);
}
sabio.services.ratings.postRating = function (ratingValue, entityId, entityType, comments, onSuccess, onError) {
    var url = '/api/rating/'
    var settings = {
        dataType: 'JSON',
        type: 'post',
        data: {
            RatingValue: ratingValue,
            EntityId: entityId,
            EntityType: entityType,
            Comments: comments
        },
        success: onSuccess,
        error: onError
    };
    $.ajax(url, settings);
}

sabio.services.ratings.listRatingsByEntity = function (entityId, entityType, onSuccess, onError) {
    var url = '/api/rating/' + entityId + '/' + entityType
    var settings = {
        datatype: 'JSON',
        type: 'get',
        data: null,
        success: onSuccess,
        error: onError
    };
    $.ajax(url, settings);
}

sabio.services.ratings.listPagedRatingsByEntitySorted = function (entityId, entityType, currentPage, pageSize, sortOrder, onSuccess, onError) {
    var url = '/api/rating/sorted'
    var settings = {
        datatype: 'JSON',
        type: 'get',
        data: {
            entityId: entityId,
            entityType: entityType,
            currentPage: currentPage - 1,
            pageSize: pageSize,
            sortOrder: sortOrder
        },
        success: onSuccess,
        error: onError
    };
    $.ajax(url, settings);
}