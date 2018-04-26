app.controller("usersCtrl", function ($scope, usersService, $location) {
    
    $scope.userName = '';
    $scope.email = '';
    $scope.password = '';
    $scope.id = 0;
    $scope.hideform = true;
    $scope.incomplete = false;
    $scope.isAdd = function () { return $scope.id === 0; }
    getUsers();
    //To Get All Records  
    function getUsers() {
        var usersResult = usersService.getUsers();
        usersResult.then(function (emp) {
            $scope.users = emp.data;
        }, function () {
            alert('Error in getting records');
        });
    }

    $scope.deleteUser = function(id) {
        //todo:display confirmation
        var currentUser = $scope.users.find(function (obj) { return obj.id === id; });
        var deleteResult = usersService.DeleteUser(currentUser);
        deleteResult.then(function (result) {
            var index = $scope.users.indexOf(currentUser);
            $scope.users.splice(index, 1);
        }, function () {
            alert('Error when deleting user.');
        });
    }

    $scope.saveChanges = function () {
       
        if ($scope.id!==0) {
            executeEditCurrentUser();
        }
        else {
            executeAddNewUser();
        }
    }

    $scope.addUser = function () {
        $scope.id = 0;
        $scope.hideform = false;
        $scope.incomplete = true;
        $scope.userName = '';
        $scope.email = '';
        $scope.password = '';
    }

    $scope.editUser = function (id) {
        $scope.id = id;
        $scope.hideform = false;
        var currentUser = $scope.users.find(function (obj) { return obj.id === id; });
        $scope.userName = currentUser.userName;
        $scope.email = currentUser.email;
        $scope.password = currentUser.password;
    };

    $scope.$watch('password', function () { $scope.validate(); });
    $scope.$watch('userName', function () { $scope.validate(); });
    $scope.$watch('email', function () { $scope.validate(); });

    $scope.validate = function () {
        $scope.incomplete = false;
        if (!$scope.userName.length ||
            !$scope.email.length ||
            !$scope.password.length) {
            $scope.incomplete = true;
        }
    };

    function getUser() {
        return {
            id: $scope.id,
            userName: $scope.userName,
            password: $scope.password,
            email: $scope.email   
        }
    }

    function executeEditCurrentUser() {
        var userToUpdate = getUser();
        var updateResult = usersService.updateUser(userToUpdate);
        updateResult.then(function (result) {
            var index = $scope.users.findIndex(x => x.id === $scope.id);
            $scope.users[index] = userToUpdate;
            //todo:modal update done
        }, function () {
            alert('Error when editing user.');
        });
    }
    function executeAddNewUser() {
        var userToAdd = getUser();
        var addResult = usersService.AddUser(userToAdd);
        addResult.then(function (result) {
            $scope.users.push(result.data);
            clearForm();
            $location.path('roles/' + result.data.id);
            //todo:modal add done
        }, function () {
            alert('Error when editing user.');
        });
    }

    function clearForm() {
        $scope.id = 0;
        $scope.userName = '';
        $scope.password = '';
        $scope.email = '';
    }

    $scope.editRoles = function () {
        $location.path('roles/'+$scope.id);
    }
});

