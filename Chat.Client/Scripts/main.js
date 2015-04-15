'use strict';
(function($){
    $(function () {

        $('#register').on('click', function (e) {
            e.preventDefault();
            registerClicked();
        });

        $('#login').on('click', function (e) {
            e.preventDefault();
            loginClicked();
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
                }, requestError);
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
                },
                requestError);
        }


        function authSuccess(data, action) {

            if (action == 'login') {
                console.log(data);
                notify('success', 'Successfully logged in!');
            } else if (action == 'register') {
                console.log(data);
                notify('success', 'Registration successfull!');
            }
        }

        function requestError(error) {
            
            var errorText = $.parseJSON(error.responseText);
            var errorMsg = '';

            for (var prop in errorText.ModelState) {
                errorMsg += errorText.ModelState[prop] + '<br />';
            }
            console.log(errorText);
            notify('error', errorMsg);
        }

        /********Notifications************/
        function notify(type, msg) {
            var timeout = (type == 'success') ? 2000 : 8000;
            noty({
                text: msg,
                type: type,
                layout: 'topCenter',
                timeout: timeout
            });
        }

        /*********** Waiting Action ***********/
        $(document).ajaxStart(function () {
            $('body').addClass("loading");
        });

        $(document).ajaxStop(function() {
            $('body').removeClass("loading");
        });

        /************ After Login Load *************/

        var successfullLogin = (function() {

        });

    });
})(jQuery);