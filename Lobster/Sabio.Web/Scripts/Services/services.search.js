sabio.services.search = {};
//sabio.services.search.employers = function (onSuccess, onError) {

sabio.services.search.employers = function (currentPage, pageSize, onSuccess, onError, keyWord) {
    var url = '/api/search/employers'
    var settings = {
        dataType: 'JSON',
        type: 'get',
    data: {
        keyword: keyWord,
        currentPage: currentPage - 1,
        pageSize: pageSize
    },
        success: onSuccess,
        error: onError
    }
    $.ajax(url, settings);
}

sabio.services.search.jobs = function (currentPage, pageSize, onSuccess, onError, keyWord) {
    var url = '/api/search/jobs'
    var settings = {
        dataType: 'JSON',
        type: 'get',
        data: {
            keyword: keyWord,
            currentPage: currentPage - 1,
            pageSize: pageSize
        },
        success: onSuccess,
        error: onError
    }
    $.ajax(url, settings);
}

//// Angular function for Developer Search without pagination

//sabio.services.search.searchDevelopers = function (keyWord, onSuccess, onError) {
//    var url = '/api/search/developers';
//    var settings = {
//        dataType: 'JSON',
//        type: 'get',
//        data: {
//            keyword: keyWord,
//        },
//        success: onSuccess,
//        error: onError,
//    };
//    $.ajax(url, settings);
//}


//// function for Developer Search Pagination with angularization

sabio.services.search.searchDevelopers = function (currentPage, pageSize, onSuccess, onError, keyWord) {
    var url = '/api/search/developers';
    var settings = {
        dataType: 'JSON',
        type: 'get',
        data: {
            currentPage: currentPage - 1, 
            itemsPerPage: pageSize,
            keyWord: keyWord,
        },
        success: onSuccess,
        error: onError
    };
    $.ajax(url, settings);
}


sabio.services.search.recruitersCount = function (keyword, onSuccess, onError) {
    var url = '/api/search/recruitersCount';
    var settings = {
        dataType: 'JSON',
        type: 'get',
        data: {
            Keyword: keyword,
        },
        success: onSuccess,
        error: onError,
    };
    var call = $.ajax(url, settings);
}

sabio.services.search.recruiters = function (keyword, currPage, perPage, onSuccess, onError) {
    var url = '/api/search/recruiters';
    var settings = {
        dataType: 'JSON',
        type: 'get',
        data: {
            Keyword: keyword,
            CurrentPage: currPage,
            ItemsPerPage: perPage
        },
        success: onSuccess,
        error: onError,
    };
    var call = $.ajax(url, settings);
}



sabio.services.search.getAllTags = function (onSuccess, onError) {

    $.ajax({

        type: "GET",
        url: "/api/tag/all",
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        dataType: "json",
        success: onSuccess,
        error: onError

    });


}
