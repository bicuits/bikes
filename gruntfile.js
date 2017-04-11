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
                    'bikes/Content/bikes.min.css': ['bikes/Source/css/public.css']  
                }  
            }  
        },  
        uglify: {  
            options: {  
                compress: true  
            },  
            applib: {  
                src: [  
                'bikes/Source/app/app.js',
                'bikes/Source/app/bike-services.js',
                'bikes/Source/app/model.js',
                'bikes/Source/app/bike.js',
                'bikes/Source/app/rider.js',  
                'bikes/Source/app/route.js',  
                'bikes/Source/app/ride.js',  
                'bikes/Source/app/home.js',
                'bikes/Source/app/payment.js',  
                'bikes/Source/app/analysis.js',  
                'bikes/Source/app/user.js'  ,
                'bikes/Source/app/nav.js'  ,
                'bikes/Source/app/report.js'  ,
                'bikes/Source/app/fiddle.js'  
                ],  
                dest: 'bikes/Content/bikes.min.js',
				nonull: true
            }  
        }  
    });  
    // Default task.  
    grunt.registerTask('default', ['uglify', 'cssmin']);  
};