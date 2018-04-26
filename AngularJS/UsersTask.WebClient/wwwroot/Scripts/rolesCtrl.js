app.controller("rolesCtrl", function ($scope, usersService, $location, $routeParams) {

    getUserRoles();
    //To Get All Records  
    function getUserRoles() {
        var userRoles = usersService.getUserRoles($routeParams.id);
        userRoles.then(function (result) {
            console.log(result.data.roleAssociations);
            $scope.roles = result.data.roleAssociations;
            $scope.userName = result.data.userName;
        }, function () {
            alert('Error in getting records');
        });
    }

    $scope.saveChanges = function () {
        var roles = {
            userId: $routeParams.id,
            roleAssociations: $scope.roles
        };
        var userRoles = usersService.associateRolesToUserAsync(roles);
        userRoles.then(function (result) {
            $scope.changeView('users');
        }, function () {
            alert('Error in role association');
        });
    }
    $scope.changeView = function (view) {
        $location.path(view);
    }
});

