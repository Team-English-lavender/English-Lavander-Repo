'use strict';
(function($){
        var connection = $.hubConnection();
        var hub = connection.createHubProxy('chatHub');

        $('#chatForm').on('submit', function (e) {
            e.preventDefault();
        });

        hub.on('broadCastMessage', function (time, sender, mssg, groupId) {
            var group = groopStorage.get();
            if (groupId == group.id) {
                $('#mssgLogger').append('<li><small>[' + time + '] <strong>' + sender + '</strong>:</small> ' + mssg + '</li>');
            }
           
        });

        connection.start().done(function () {
            $('#send').click(function (e) {
                e.preventDefault();

                var sender = $('#textMessage').data('sender');
                var currentSession = userSession.get();
                var token = currentSession.access_token;
                var user = {};

                var mssg = utilities.replaceTags($('#textMessage').val());
                // must retrieve group id dynamically
                var group = groopStorage.get();

                $('#textMessage').val('');

                if (currentSession) {
                    ajaxRequester.postMessage(token, mssg, group.id,
                        function (data) {
                            console.log(data);
                            //hub.invoke('joinRoom', group.name);
                            hub.invoke('sendMessage', sender, mssg, group.id);
                            //hub.invoke("sendMessageToGroup", user, mssg, group.name);
                           
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