var app = angular.module('courseApp', []);

app.controller('courseController', function ($scope, $http) {
    // Initialize course object
    $scope.course = {
        title: '',
        duration: '',
        fees: 0,
        modules: [
            { title: '', contents: '', duration: 0 } // Default module
        ]
    };

    // Get the anti-forgery token from the meta tag
    var token = document.querySelector('meta[name="__RequestVerificationToken"]').content;

    // Function to add a new module
    $scope.addModule = function () {
        $scope.course.modules.push({ title: '', contents: '', duration: 0 });
    };

    // Function to remove a module
    $scope.removeModule = function (index) {
        $scope.course.modules.splice(index, 1);
    };

    // Function to create a course
    $scope.createCourse = function () {
        // Define API endpoint
        var url = 'https://localhost:44311/CourseModule/Create';

        // Send POST request to the server with the anti-forgery token in headers
        $http.post(url, $scope.course, {
            headers: {
                'RequestVerificationToken': token // Add the token in the headers
            }
        }).then(function (response) {
            // Success callback
            alert('Course created successfully!');
            console.log(response.data);
        }, function (error) {
            // Error callback
            alert('Error creating course. Please check the console for more details.');
            console.error(error);
        });
    };
});
