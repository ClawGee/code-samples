//these are the angular functions for the rating feature -ER

sabio.services.ratingFactory = function ($baseService) {
    var aSabioServiceObject = sabio.services.ratings;
    var newService = $.extend(true, {}, aSabioServiceObject, $baseService);
    return newService;
}

sabio.page.ratingControllerFactory = function ($scope, $baseController, $ratingService) {
    //console.log($ratingService);
    var vm = this;
    vm.item = null;
    vm.ratingData = null;

    //these are the required variables for ng-ratings to work
    vm.$ratingService = $ratingService;
    vm.$scope = $scope;
    
    vm.ratingSubmit = _ratingSubmit;
    vm.onSubmitSuccess = _onSubmitSuccess;
    vm.onSubmitError = _onSubmitError;
    vm.max = 5;
    
    $.extend(vm, $baseController);

    //this is a wrapper for our small dependency on $scope
    vm.notify = vm.$ratingService.getNotifier($scope);

    function _ratingSubmit() {
        vm.currentPage = 1;
        vm.pageSize = 5
        vm.$ratingService.postRating(vm.item.rating, vm.$sabio.entityId, vm.$sabio.entityType, vm.item.comment, vm.onSubmitSuccess, vm.onSubmitError);
    };

    function _onSubmitSuccess(data) {
        if (data.item) {
            sabio.layout.showModal('alert-success', 'Rating Posted!', 'Your Rating went through, thanks!')
            vm.$scope.$emit('submitRating', vm.item);
            //$form.$setPristine();
        } else {
            sabio.layout.showModal('alert-danger', 'Error', 'Your Rating failed for some reason. Not posted.')
        };
    }

    function _onSubmitError(jqXhr, error) {
        sabio.layout.showModal('alert-danger', 'Error', 'Bummer, Rating not posted!')
    };
};

sabio.ng.addService(sabio.ng.app.module
    , "$ratingService"
    , ["$baseService"]
    , sabio.services.ratingFactory);

sabio.ng.addController(sabio.ng.app.module
    , "ratingController"
    , ['$scope', '$baseController', "$ratingService"]
    , sabio.page.ratingControllerFactory);





//**THE CODE BELOW is the old Jquery way of doing ratings, saved for posterity -ER**
//sabio.rating = {};    
//    sabio.rating.startUp = function () {
//        sabio.rating.ratingForm = $("#rating-form");
//        sabio.rating.ratingValue = $("#rating-star");
//        //$("#rating-star").on('change', sabio.rating.getRateValue);
//        $("#btn-rating-submit").on("click", sabio.rating.submitRating);
//    };
////sabio.rating.getRateValue = function () {
////    alert('Rating is ' + sabio.rating.ratingValue.val());
////};
//sabio.rating.submitRating = function (e) {
//    e.preventDefault();
//    //console.log("clickhandler is working");
//    var comment = $('#rating-comment').val();
//    var rating = sabio.rating.ratingValue.val();
//    var entityType = sabio.layout.entityType;
//    var entityId = sabio.layout.entityId;

//    sabio.services.ratings.postRating(
//        rating, //ratingvalue
//        entityId, //entityId
//        entityType, //entityType
//        comment, //comment
//        sabio.rating.onSuccess,
//        sabio.rating.onFailure);
//};
//sabio.rating.getRating = function (e) {
//    e.preventDefault();
//    sabio.services.ratings.getRating
//};
//sabio.rating.onSuccess = function (ratingData) {
//    console.log("success!");
//};

//sabio.rating.onFailure = function (ratingData) {
//    console.log("rating fail!");
//};

