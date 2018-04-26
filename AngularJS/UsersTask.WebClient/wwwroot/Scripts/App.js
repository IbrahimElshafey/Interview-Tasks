var app = angular.module('usersApp', ['ngRoute']);
app.config(function ($routeProvider) {
    $routeProvider
        .when("/",
            {
                templateUrl: "users.html",
                controller: "usersCtrl"
        })
        .when("/users",
            {
                templateUrl: "users.html"
            })
        .when("/roles/:id",
        {
                templateUrl: "roles.html"
        });
});