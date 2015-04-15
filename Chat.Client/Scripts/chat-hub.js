$(function () {
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

            $('#textMessage').val('');

            hub.invoke('sendMessage', sender, mssg);
            $("#logger-wrapper").animate({ scrollTop: $("#logger-wrapper").prop("scrollHeight") }, 100);

        });
    });

    

});