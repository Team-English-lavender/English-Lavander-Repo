'use strict';
(function($){
        var connection = $.hubConnection();
        var hub = connection.createHubProxy('chatHub');

        $('#chatForm').on('submit', function (e) {
            e.preventDefault();
        });

        hub.on('broadCastMessage', function (time, sender, mssg) {
            $('#mssgLogger').append('<li><small>[' + time + '] <strong>' + sender + '</strong>:</small> ' + mssg + '</li>');
        });


        connection.start().done(function () {
            $('#send').click(function (e) {
                e.preventDefault();

                var sender = $('#textMessage').data('sender');
                var mssg = $('#textMessage').val();
                // must retrieve group id dynamically
                var groupId = 1;

                $('#textMessage').val('');

                var currentSession = userSession.get();

                if (currentSession) {
                    var token = currentSession.access_token;

                    ajaxRequester.postMessage(token, mssg, groupId,
                        function (data) {
                            console.log(data);
                            hub.invoke('sendMessage', sender, mssg);
                            $("#logger-wrapper").animate({ scrollTop: $("#logger-wrapper").prop("scrollHeight") }, 100);
                        },
                        function (data) {
                            console.log(data);
                            utilities.notify('error', 'Could not send message. Try again');
                        });
                } else {

                    utilities.notify('error', 'Your session has expired. Please login again.');
                    utilities.redirectToHome();
                }
            });
        });
})(jQuery);