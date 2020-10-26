angular.module('apiModule', [])
    .factory("$api", function ($http) {
    var api = this;

    api.getNames = function () {
        var req = {
            method: 'GET',
            url: 'api/homes/names',
            params: {},
            data: {}
        };
        return $http(req).then(function (result) {
            return result.data;
        });
    };

    api.getRooms = function () {
        var req = {
            method: 'GET',
            url: 'api/homes/data',
            params: {},
            data: {}
        };
        return $http(req).then(function (result) {
            return result.data;
        });
    };

    api.getHouse = function (id) {
        var req = {
            method: 'GET',
            url: 'api/homes/' + id + '/data',
            params: { id: id },
            data: {}
        };
        return $http(req).then(function (result) {
            return result.data;
        });
    };

    api.saveHouse = function (name) {
        var req = {
            method: 'POST',
            url: 'api/homes/' + name + '/newHome',
            params: { name: name },
            data: {}
        };
        return $http(req).then(function (result) {
            return result.data;
        });
    };

    api.deleteHouse = function (id) {
        var req = {
            method: 'DELETE',
            url: 'api/homes/' + id + '/deleteHome',
            params: { id: id },
            data: {}
        };
        return $http(req).then(function (result) {
            return result.data;
        });
    };

    api.saveRoom = function (room) {
        var req = {
            method: 'POST',
            url: 'api/homes/newRoom',
            params: {},
            data: room
        };
        return $http(req).then(function (result) {
            return result.data;
        });
    };

    api.deleteRoom = function (id) {
        var req = {
            method: 'DELETE',
            url: 'api/homes/' + id + '/deleteRoom',
            params: { id: id },
            data: {}
        };
        return $http(req).then(function (result) {
            return result.data;
        });
    };

    return api;
});

