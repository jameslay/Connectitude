var app = angular.module('automateHome', ['apiModule']);

app.controller("automateCtrl", ["$scope", "$interval", "$api", function ($scope, $interval, $api) {
    $scope.title = "Home automation";
    $scope.rooms = [];
    $scope.houses = [];
    $scope.houseId = null;
    $scope.newHouse = "";
    $scope.newRoom = {
        name: "",
        temperature: "",
        humidity: "",
        homeId: ""
    };
    var automationInterval;

    function init() {
        getAllNames();
    };
    init();

    $scope.startAutomation = function () {
        stopTimer();
        var choice = $scope.houseId == null ? 1 : 2;
        getData(choice);
        automationInterval = $interval(function () {
            getData(choice);
        }, 30000)
    }

    function getData(choice) {
        switch (choice) {
            case 1: getAllRooms(); break;
            case 2: getHouse($scope.houseId); break;
        }
    }

    function stopTimer() {
        $interval.cancel(automationInterval);
    }

    function getAllNames() {
        $api.getNames().then(function (data) {
            $scope.houses = data;
        }).catch(function () {
            alert("Failed to get houses");
        });
    }

    function getAllRooms() {
        $api.getRooms().then(function (data) {
            $scope.rooms = data.rooms;
        }).catch(function () {
            alert("Failed to get rooms");
        });
    }

    function getHouse(id) {
        $api.getHouse(id).then(function (data) {
            $scope.rooms = data.rooms;
        }).catch(function () {
            alert("Failed to get rooms");
        });
    }

    $scope.saveHouse = function (newHouse) {
        $api.saveHouse(newHouse).then(function (data) {
            getAllNames();
            alert("House saved");
        }).catch(function (data) {
            alert("Failed to save house");
        }).finally(function () {
            $scope.resetHouse();
        });
    }

    $scope.resetHouse = function () {
        $scope.newHouse = "";
    }

    $scope.deleteHouse = function (id) {
        if (confirm("Are you sure you want to delete?")) {
            $api.deleteHouse(id).then(function (data) {
                alert("House deleted");
                $scope.clearAll();
                $scope.startAutomation();
                getAllNames();
            }).catch(function (data) {
                alert("Failed to deleted house");
            });
        }
    }

    $scope.saveRoom = function (newRoom) {
        var roomToSave = {
            Id: 0, Name: newRoom.name, Temperature: parseFloat(newRoom.temperature), Humidity: parseFloat(newRoom.humidity),
            HomeId: parseInt(newRoom.homeId), HomeName: ""
        };

        $api.saveRoom(roomToSave).then(function (data) {
            alert("Room saved");
            $scope.clearAll();
            $scope.startAutomation();
        }).catch(function (data) {
            alert("Failed to save room");
        }).finally(function () {
            $scope.resetRoom();
        });
    }

    $scope.resetRoom = function () {
        $scope.newRoom = {
            name: "",
            temperature: "",
            humidity: "",
            homeId: ""
        };
    }

    $scope.deleteRoom = function (id) {
        if (confirm("Are you sure you want to delete?")) {
            $api.deleteRoom(id).then(function (data) {
                alert("Room deleted");
                $scope.clearAll();
                $scope.startAutomation();
            }).catch(function (data) {
                alert("Failed to deleted room");
            });
        }
    }

    $scope.$watch('newHouse', function () {
        $scope.incorrectHouse = $scope.newHouse === "" ? true : false;
    }, true);

    $scope.$watch('newRoom', function () {
        $scope.incorrectRoom = validateRoom();
    }, true);

    function validateRoom() {
        $scope.incorrectRoomName = $scope.newRoom.name === "" ? true : false;
        $scope.incorrectTemperature = $scope.newRoom.temperature === "" ? true : false;
        $scope.incorrectHumidity = $scope.newRoom.humidity === "" ? true : false;
        $scope.incorrectHomeId = true;

        if ($scope.newRoom.homeId !== "") {
            angular.forEach($scope.houses, function (key, value) {
                if (key.id === parseInt($scope.newRoom.homeId)) {
                    $scope.incorrectHomeId = false;
                }
            })
        }

        var obj = {
            correctName: !$scope.incorrectRoomName,
            correctHumidity: !$scope.incorrectTemperature,
            correctTemperature: !$scope.incorrectHumidity,
            correctId: !$scope.incorrectHomeId
        }

        var correct = true;
        angular.forEach(obj, function (key, value) {
            if (key === false) {
                correct = false;
            }
        })
        return correct;
    }

    $scope.clearAll = function () {
        $scope.rooms = [];
        $scope.houseId = null;
        clearInterval();
    }
}]);

//Directives
app.directive('numericOnly', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attrs, modelCtrl) {

            modelCtrl.$parsers.push(function (inputValue) {
                var transformedInput = inputValue ? inputValue.replace(/[^\d.-]/g, '') : null;

                if (transformedInput != inputValue) {
                    modelCtrl.$setViewValue(transformedInput);
                    modelCtrl.$render();
                }

                if (transformedInput == 0) {
                    modelCtrl.$setViewValue(null);
                    modelCtrl.$render();
                }
                return transformedInput;
            });
        }
    };
});

app.directive('instructionsModal', function () {
    return {
        templateUrl: 'templates/instructions-modal.html'
    };
});

app.directive('addHouseModal', function () {
    return {
        templateUrl: 'templates/add-house-modal.html'
    };
});

app.directive('addRoomModal', function () {
    return {
        templateUrl: 'templates/add-room-modal.html'
    };
});
