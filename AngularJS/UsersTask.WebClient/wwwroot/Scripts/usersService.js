app.service("usersService", function ($http) {

    //get all users
    this.getUsers = function () {

        return $http.get("/api/Users/GetUsers");
    };

    // Update User 
    this.updateUser = function (user) {
        var response = $http({
            method: "post",
            url: "/api/Users/UpdateUser",
            data: JSON.stringify(user),
            dataType: "json"
        });
        return response;
    }

    // Add User
    this.AddUser = function (user) {

        var response = $http({
            method: "post",
            url: "/api/Users/AddUser",
            data: JSON.stringify(user),
            dataType: "json"
        });

        return response;
    }

    //Delete User
    this.DeleteUser = function (user) {
        var response = $http({
            method: "post",
            url: "/api/Users/DeleteUser",
            data: JSON.stringify(user),
            dataType: "json"
        });
        return response;
    }

    this.getUserRoles = function (id) {

        return $http.get("/api/Users/GetUserRoles?userId=" + id);
    };

    this.associateRolesToUserAsync = function (roles) {

        var response = $http({
            method: "post",
            url: "/api/Users/AssociateRolesToUserAsync",
            data: JSON.stringify(roles),
            dataType: "json"
        });

        return response;
    }
});