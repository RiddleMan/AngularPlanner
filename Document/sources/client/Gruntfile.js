'use strict';

module.exports = function (grunt) {

  // Load grunt tasks automatically
  require('load-grunt-tasks')(grunt);

  // Time how long tasks take. Can help when optimizing build times
  require('time-grunt')(grunt);

  // Define the configuration for all the tasks
  grunt.initConfig({
    // Project settings
    yeoman: {
      // configurable paths
      appSrc: 'AngularPlanner/App_src',
      appDest: 'AngularPlanner/App',
      testSpec: 'AngularPlanner.Tests/App/Spec',
      css: 'AngularPlanner/Content',
      images: 'AngularPlanner/Images',
      views: 'AngularPlanner/Views'
    },
    // Watches files for changes and runs tasks based on the changed files
    watch: {
      js: {
        files: ['<%= yeoman.appSrc %>/**/*.js'],
        tasks: ['newer:ngmin:app', 'newer:jshint:all'],
        options: {
          livereload: true
        }
      },
      views: {
        files: ['<%= yeoman.appSrc %>/**/*.html'],
        tasks: ['newer:copy:views'],
        options: {
          livereload: true
        }
      },
      jsTest: {
        files: ['<%= yeoman.testSpec %>/**/*.js'],
        tasks: ['newer:jshint:test', 'karma']
      },
      gruntfile: {
        files: ['Gruntfile.js']
      },
      livereload: {
        options: {
          livereload: 35729
        },
        files: [
          '<%= yeoman.views %>/**/*.cshtml',
          '<%= yeoman.appSrc %>/**/*.html',
          '<%= yeoman.css %>/**/*.css',
          '<%= yeoman.images %>/**/*.{png,jpg,jpeg,gif,webp,svg}'
        ]
      }
    },

    ngmin: {
      app: {
        cwd: '<%= yeoman.appSrc %>',
        expand: true,
        src: ['**/*.js'],
        dest: '<%= yeoman.appDest %>'
      }
    },

    copy: {
      views: {
        cwd: '<%= yeoman.appSrc %>',
        expand: true,
        src: '**/*.html',
        dest: '<%= yeoman.appDest %>'
      }
    },

    // Make sure code styles are up to par and there are no obvious mistakes
    jshint: {
      options: {
        jshintrc: '.jshintrc',
        reporter: require('jshint-stylish')
      },
      all: [
        'Gruntfile.js',
        '<%= yeoman.appSrc %>/**/*.js'
      ],
      test: {
        options: {
          jshintrc: 'test/.jshintrc'
        },
        src: ['<%= yeoman.testSpec %>/**/*.js']
      }
    },

    karma: {
      unit: {
        configFile: 'karma.conf.js',
        singleRun: true
      }
    }
  });


  grunt.registerTask('dev', function () {
    grunt.task.run([
      'watch'
    ]);
  });

  grunt.registerTask('test', [
    'karma'
  ]);

  grunt.registerTask('default', function() {
    grunt.task.run([
      'watch'
    ]);
  });
};
