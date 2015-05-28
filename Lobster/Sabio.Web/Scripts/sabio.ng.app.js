/// <reference path="sabio.js" />
/// <reference path="/scripts/ng/angular.js" />

sabio.ng = {
    app: {
        services: {}
		, controllers: {}
    }
	, exceptions: {}
	, examples: {}
    , defaultDependencies: ["ngAnimate", "ngRoute"]
    , getModuleDependencies: function(){
        if (sabio.extraNgDependencies) {
            var newItems = sabio.ng.defaultDependencies.concat(sabio.extraNgDependencies);
            return newItems;
        }
        return sabio.ng.defaultDependencies;
    }
};

sabio.ng.app.module = angular.module('sabioApp', sabio.ng.getModuleDependencies());

sabio.ng.app.module.value('$sabio', sabio  );

//#region - base functions and objects -

sabio.ng.exceptions.argumentException = function (msg) {
    this.message = msg;
    var err = new Error();
   

    console.error(msg + "\n" + err.stack);
}

sabio.ng.app.services.baseService = function ($win, $loc, $util) {
    /*
        when this function is envoked by Angular, Angular wants an instance of the Service object. 
		
    */

    var getChangeNotifier = function ($scope) {
        /*
        will be called when there is an event outside Angular that has modified
        our data and we need to let Angular know about it.
        */
        var self = this;

        self.scope = $scope;

        return function (fx) {
            self.scope.$apply(fx);//this is the magic right here that cause ng to re-evaluate bindings
         }


    }

    var baseService = {
        $window: $win
        , getNotifier: getChangeNotifier
        , $location: $loc
        , $utils: $util
    };

    return baseService;
}

sabio.ng.app.controllers.baseController = function ($doc, $logger, $sab) {
    /*
        this is intended to serve as the base controller
    */

    var _formatDate = function (d) {                
        if (typeof d === "object") {
            var curr_date = d.getDate();
            var curr_month = d.getMonth() + 1; //Months are zero based
            var curr_year = d.getFullYear();
            return curr_month + "/" + curr_date + "/" + curr_year;
        } else if (typeof d === "string") {
            return d;
        } else {
            return null;
        }
    }

    var baseController = {
        $document: $doc
		, $log: $logger
        , $sabio: $sab
        , formatDate:_formatDate
    };

    return baseController;
}

//#endregion

//#region  - core ng wrapper functions --

sabio.ng.getControllerInstance = function (jQueryObj) {///used to grab an instance of a controller bound to an Element
    console.log(jQueryObj);
    return angular.element(jQueryObj[0]).controller();
}

sabio.ng.addService = function (ngModule, serviceName, dependencies, factory) {
    /*
    sabio.ng.app.module.service(
    '$baseService', 
    ['$window', '$location', '$utils', sabio.ng.app.services.baseService]);
    */
    if (!ngModule ||
		!serviceName || !factory ||
		!angular.isFunction(factory)) {
        throw new sabio.ng.exceptions.argumentException("Invalid Service Call");
    }

    if (dependencies && !angular.isArray(dependencies)) {
        throw new sabio.ng.exceptions.argumentException("Invalid Service Call [dependencies]");
    }
    else if (!dependencies) {
        dependencies = [];
    }

    dependencies.push(factory);

    ngModule.service(serviceName, dependencies);

}

sabio.ng.addController = function (ngModule, controllerName, dependencies, factory) {
    if (!ngModule ||
		!controllerName || !factory ||
		!angular.isFunction(factory)) {
        throw new sabio.ng.exceptions.argumentException("Invalid Service defined");
    }

    if (dependencies && !angular.isArray(dependencies)) {
        throw new sabio.ng.exceptions.argumentException("Invalid Service Call [dependencies]");
    }
    else if (!dependencies) {
        dependencies = [];
    }

    dependencies.push(factory);
    ngModule.controller(controllerName, dependencies);

}

//#endregions


//#region - adding in baseService and baseController

/*
the basic explaination for these three function arguments

name of thing we are creating:'baseService'
array containing the dependancies of the function we will use to create said thing
the last item in this array is invoked to create the object and passed the preceding dependancies.


*/
sabio.ng.addService(sabio.ng.app.module
					, "$baseService"
					, ['$window', '$location']
					, sabio.ng.app.services.baseService);

sabio.ng.addService(sabio.ng.app.module
					, "$baseController"
					, ['$document', '$log', '$sabio']
					, sabio.ng.app.controllers.baseController);


//#endregion

//#region - Examples on how to use the core functions

//***************************************************************************************
//------------------------ Examples -------------------------------------
/*
 * 
 *              How to call the .addService and .addController
 * 
 */




sabio.ng.examples.exampleServices = function ($baseService) {
    /*
		when this function is envoked by Angular,
		Angular wants an instance of the Service object. here
		we reuse the same instance of the JS object we have above
	*/

    /*
		we are using this as an example to demonstrate we can use our existing
		code with this new pattern.
	*/

    var aSabioServiceObject = sabio.services.users;
    var newService = $.extend(true, {}, aSabioServiceObject, baseService);

    return newService;
}

sabio.ng.examples.exampleController = function ($scope, $baseController, $exampleSvc) {

    var vm = this;
    vm.items = null;
    vm.receiveItems = _receiveItems;

    //-- this line to simulate inheritance
    $.extend(true, vm, $baseController);

    //this is a wrapper for our small dependency on $scope
    vm.notify = $exampleSvc.getNotifier($scope);

    function _receiveItems(data) {
        //this receives the data and calls the special
        //notify method that will trigger ng to refresh UI
        vm.notify(function () {
            vm.items = data.items;
        });
    }
}


sabio.ng.addService(sabio.ng.app.module
					, "$exampleSvc"
					, ['$baseService']
					, sabio.ng.examples.exampleServices);

sabio.ng.addController(sabio.ng.app.module
	, 'controllerName'
	, ['$scope', '$baseController', '$exampleSvc']
	, sabio.ng.examples.exampleController
	);


//------------------------ Examples -------------------------------------
//***************************************************************************************


//#endregion
