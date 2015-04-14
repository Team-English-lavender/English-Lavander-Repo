'use strict';
(function($){
    $(function () {

        $('#register').on('click', function (e) {
            e.preventDefault();
            registerClicked();
        });

        /**************Rgistration*****************/
        function registerClicked() {
            var name = $('#inputUserName').val();
            var pass = $('#inputPassword').val();
            var confirmPass = $('#inputPasswordConfirm').val();
            var email = $('#inputEmail').val();

            if (!name || !pass || !email) {
                notify('warning', 'Please fill Username, password and email to register!');
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
                }, registerError);
        }

        function authSuccess(data, action) {

            if (action == 'login') {
                notify('success', 'Successfully logged in!');
            } else if (action == 'register') {
                notify('success', 'Registration successfull!');
            }
        }

        function registerError(error) {
            
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

    });
})(jQuery);