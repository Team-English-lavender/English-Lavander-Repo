'use strict';
(function ($) {
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

        $('#friendsListBtn').click(function (e) {
            e.preventDefault();
            loadFriends();
        });
        $('#groupsListBtn').click(function (e) {
            e.preventDefault();
            loadGroups();
        });

        $('#retrieveMessagesByGidLimited').on('click', function () {
            retrieveMessagesbyGidClicked('limited');
        });

        $('#retrieveMessageByGidsAll').on('click', function () {
            retrieveMessagesbyGidClicked('all');
        });

        $("#upload-file-button").on("click", function (e) {
            e.preventDefault();
            uploadFile();
        });

        /**************Rgistration*****************/
        function registerClicked() {
            var name = $('#inputUserName').val();
            var pass = $('#inputPassword').val();
            var confirmPass = $('#inputPasswordConfirm').val();
            var email = $('#inputEmail').val();

            if (!name || !pass) {
                utilities.notify('warning', 'Please fill Username and passwort to register!');
                return;
            }
            if (!confirmPass) {
                utilities.notify('error', 'Confirm Password!');
                return;
            }
            if (confirmPass != pass) {
                utilities.notify('error', 'Passwords must be identical!');
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
                utilities.notify('warning', 'Enter both username and passward to login!');
                return;
            }

            ajaxRequester.login(uname, upass,
                function (data) {
                    authSuccess(data, 'login');
                    utilities.redirectToHome();
                },
                function (data) {
                    requestError(data, 'login');
                });
        }


        function logoutClicked() {
            authSuccess(null, 'logout');

            //ajaxRequester.logout(
            //    function (data) {
            //        authSuccess(data, 'logout');
            //        utilities.redirectToHome();
            //    },
            //    function (data) {
            //        requestError(data, 'logout');
            //});
        }

        /* File Upload */
        function uploadFile() {
            var token = userSession.get().access_token;
            var data = new FormData(document.getElementById("upload-form"));
            //var files = $("#upload-file");
            //if (files.length > 0) {
            //    data.append("UploadedFile", files[0]);
            //}

            ajaxRequester.uploadFile(token, 0, data, // TODO: change group ID
                function (response) {
                    console.log(response)
                },
            function (err) {
                console.log(err)
            });
        }

        function retrieveMessagesbyGidClicked(type) {
            var currentSession = userSession.get();
                        // { id: groupId, name: groupName };
            var groupId = groopStorage.get().id;

            if (type == 'limited') {
                ajaxRequester.retrieveMessagesByGidLimited(currentSession.access_token, groupId,
                    function (data) {
                        utilities.clearMessages();
                        utilities.addMessagesToLogger(data);
                    },
                    function (data) {
                        utilities.notify('error', 'Sorry, could not retrieve your history');
                    }
                )
            } else if (type == 'all') {
                ajaxRequester.retrieveMessagesByGidAll(currentSession.access_token, groupId,
                    function(data) {
                        utilities.clearMessages();
                        utilities.addMessagesToLogger(data);
                    },
                    function (data) {
                        utilities.notify('error', 'Sorry, could not retrieve your full history');
                    }
                )
            }

            $("#logger-wrapper").animate({ scrollTop: $("#logger-wrapper").prop("scrollHeight") }, 100);
        }

        /********Response Callbacks************/
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

            utilities.notify('success', mssg);
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

            utilities.notify('error', errorMsg);
        }

        /*********** Waiting Action ***********/
        $(document).ajaxStart(function () {
            $('body').addClass("loading");
        });

        $(document).ajaxStop(function () {
            $('body').removeClass("loading");
        });

        //:::::::::: Load User Groups, Friends  ::::::::::::::

        var loadGroups = (function () {
            var token = userSession.get().access_token;
            loadRequester.loadGroups(token,
                function (data, statusText, xhr) {
                    if (xhr.status == 200) {
                        utilities.listLoader(data, 'groupsList');
                        return;
                    } else if (xhr.status == 206) {
                        utilities.notify('info', data);
                    }
                },
                function(data) {
                    utilities.notify('error', 'Sorry, could not retrieve your groups.');
				}
            );
        });

        var loadFriends = (function () {
            var token = userSession.get().access_token;
            loadRequester.loadFriends(token,
                function (data) {
                    if (data.length > 1) {
                        utilities.listLoader(data, 'friendsList');
                        return;
                    }
                    utilities.notify('info', 'No friends currently.');
                },
                function(data) {
                    utilities.notify('error', 'Sorry, could not retrieve your friends, try later.');
                });
        });

    });
})(jQuery);