'use strict';
(function($){
    $(function () {
        
        var currentSession = userSession.get();
        if (currentSession) {
            $('.visitor-link, .visitor-section').hide();
            $('.user-link, .user-section').show();
        } else {
            $('.visitor-link, .visitor-section').show();
            $('.user-link, .user-section').hide();
        }

        $('#register').on('click', function (e) {
            e.preventDefault();
            registerClicked();
        });

        $('#login').on('click', function (e) {
            e.preventDefault();
            $('.visitor-link, .visitor-section').hide();
            $('.user-link, .user-section').show();

            loginClicked();
        });

        $('#logout').on('click', function (e) {
            e.preventDefault();
            $('.visitor-link, .visitor-section').show();
            $('.user-link, .user-section').hide();

            logoutClicked();
        });
        
        $('#friendsListBtn').click(function(e) {
            e.preventDefault();
            loadFriends();
        });
        $('#groupsListBtn').click(function (e) {
            e.preventDefault();
            loadGroups();
        });
    });

    /**************Rgistration*****************/
    function registerClicked() {
        var name = $('#inputUserName').val();
        var pass = $('#inputPassword').val();
        var confirmPass = $('#inputPasswordConfirm').val();
        var email = $('#inputEmail').val();

        if (!name || !pass) {
            notify('warning', 'Please fill Username and passwort to register!');
            return;
        }
        if (!confirmPass) {
            notify('error', 'Confirm Password!');
            return;
        }
        if (confirmPass != pass) {
            notify('error', 'Passwords must be identical!');
            return;
        }

        ajaxRequester.register(name, pass, confirmPass, email,
            function (data) {
                authSuccess(data, 'register');
            },
            function (data) {
                requestError(data, 'register');
            });
    }

    /**************Login*****************/
    function loginClicked() {
        var uname = $('#inputUserName').val();
        var upass = $('#inputPassword').val();

        if (!uname || !upass) {
            notify('warning', 'Enter both username and passward to login!');
            return;
        }

        ajaxRequester.login(uname, upass,
            function (data) {
                authSuccess(data, 'login');
                redirectToHome();
            },
            function (data) {
                requestError(data, 'login');
            });
    }


    function logoutClicked() {
        authSuccess(null, 'logout');
        redirectToHome();

        //ajaxRequester.logout(
        //    function (data) {
        //        authSuccess(data, 'logout');
        //        redirectToHome();
        //    },
        //    function (data) {
        //        requestError(data, 'logout');
        //});
    }


    /********Respose Callbacks************/
    function authSuccess(data, action) {
        var mssg = '';

        if (action == 'login') {
            userSession.save(data);
            mssg = 'Successfully logged in!';
        } else if (action == 'register') {
            mssg = 'Registration successfull';
        } else if (action == 'logout') {
            userSession.clear();
            mssg = 'Successfully logged out.';
        }

        notify('success', mssg);
    }

    function requestError(error, action) {
        console.log(error);
        var errorText = $.parseJSON(error.responseText);
        console.log(errorText);
        var errorMsg = '';
        
        if (action == 'register') {
            for (var prop in errorText.ModelState) {
                errorMsg += errorText.ModelState[prop] + '<br />';
            }
        } else if (action == 'login') {
            errorMsg = errorText.error_description;
        } else if (action == 'logout') {
            //add error message
        }

        notify('error', errorMsg);
    }

    /********Notifications************/
    function notify(type, msg) {
        var timeout = (type == 'success') ? 500 : 1000;
        noty({
            text: msg,
            type: type,
            layout: 'topCenter',
            timeout: timeout
        });
    }

    function redirectToHome(delay, redirectURL) {
        var timeOfDelay = delay || 600;
        var destination = location.protocol + '//' + location.host + '/';
        if(redirectURL) {
            destination += redirectURL;
        }
        console.log(timeOfDelay);
        setTimeout(function () {
            window.location = destination;
        }, timeOfDelay);
    }

    /*********** Waiting Action ***********/
    $(document).ajaxStart(function () {
        $('body').addClass("loading");
    });

    $(document).ajaxStop(function() {
        $('body').removeClass("loading");
    });

    //:::::::::: Load User Groups, Friends  ::::::::::::::
    var loadGroups = (function() {
        var token = userSession.get().access_token;
        loadRequester.loadGroups(token,
            function(data) {
                if (data.length > 1) {
                    listLoader(data, 'groupsList');
                    return;
                }
                notify('error', "No groups currently.");
            },
            function(data) {
                var a = data;
                console.log(data);
            }
        );
    });

    var loadFriends = (function () {
        var token = userSession.get().access_token;
        loadRequester.loadFriends(token,
            function (data) {
                if (data.length > 1) {
                    listLoader(data, 'friendsList');
                    return;
                }
                notify('error', 'No friends currently.');
            },
            function (data) {

            });
    });

    var listLoader = (function (objects, parentId) {
        for (var i = 0; i < objects.length; i++) {
            $('#' + parentId + '> ul').append('<li>' +objects[i].Name+ '</li>').data('Id', objects[i].Id);
        }
    });

    // ::::::::  Load Groups Messages :::::::::

})(jQuery);