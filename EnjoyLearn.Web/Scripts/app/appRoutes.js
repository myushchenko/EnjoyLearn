define(['appModule'], function (app) {
    app.config(function ($routeProvider, routeResolverProvider, $locationProvider, $controllerProvider, $compileProvider, $filterProvider, $provide) {
        //$locationProvider.html5Mode(true);
        //Define routes - controllers will be loaded dynamically
        var route = routeResolverProvider.route;
        $routeProvider
            .when('/', route.resolve('Home', 'navigations'))
            .when('/Vocabulary', route.resolve('Vocabulary', 'navigations'))
            .when('/Dictionary', route.resolve('Dictionary', 'navigations'))
            .when('/Trainings', route.resolve('Trainings', 'navigations'))
            .when('/Trainings/Game1', route.resolve('Game1', 'trainings'))
            .when('/Trainings/Game2', route.resolve('Game2', 'trainings'))
            .when('/Trainings/Game3', route.resolve('Game3', 'trainings'))
            .when('/Trainings/RotateGame', route.resolve('RotateGame', 'trainings'))
            .when('/Chat', route.resolve('Chat', 'navigations'))
            .when('/About', route.resolve('About', 'navigations'))
            .when('/Contact', route.resolve('Contact', 'navigations'))
            .when('/404', route.resolve('404', 'maintains'))
            .otherwise({ redirectTo: '/404' });
    });
});