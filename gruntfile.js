module.exports = function (grunt) {  
    require("matchdep").filterDev("grunt-*").forEach(grunt.loadNpmTasks);  
    // Project configuration.  
    grunt.initConfig({  
        pkg: grunt.file.readJSON('package.json'),  
        cssmin: {  
            sitecss: {  
                options: {  
                    banner: '/* Bikes minified css */'  
                },  
                files: {  
                    'bikes/Content/bikes.min.css': ['bikes/Content/css/public.css']  
                }  
            }  
        },  
        uglify: {  
            options: {  
                compress: true  
            },  
            applib: {  
                src: [  
                'bikes/Content/jquery/jquery-1.9.1.min.js',  
                'bikes/Content/angular/angular.min.js',  
                'bikes/Content/angular-resource/angular-resource.min.js',  
                'bikes/Content/angular-ui-router/angular-ui-router.min.js',  
                'bikes/Content/angular-ui-grid/ui-grid.min.js',  
                'bikes/Content/bootstrap/scripts/bootstrap.min.js',  
                'bikes/Content/chartjs/Chart.min.js',  
                'bikes/Content/datepicker/js/bootstrap-datepicker.min.js',  
                'bikes/Content/jinqjs/jinqjs.min.js',  
                'bikes/Content/moment/moment.min.js',  

                'bikes/Content/app/app.js',
                'bikes/Content/app/bike-services.js',
                'bikes/Content/app/bike.js',
                'bikes/Content/app/rider.js',  
                'bikes/Content/app/route.js',  
                'bikes/Content/app/ride.js',  
                'bikes/Content/app/home.js',
                'bikes/Content/app/payment.js',  
                'bikes/Content/app/analysis.js',  
                'bikes/Content/app/user.js'  ,
                'bikes/Content/app/fiddle.js'  
                ],  
                dest: 'bikes/Content/bikes.min.js',
				nonull: true
            }  
        }  
    });  
    // Default task.  
    grunt.registerTask('default', ['uglify', 'cssmin']);  
};