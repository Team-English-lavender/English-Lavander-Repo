'use strict';
var utilities = (function() {

    var tagsToReplace = {
        '&': '&amp;',
        '<': '&lt;',
        '>': '&gt;'
    };

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
        if (redirectURL) {
            destination += redirectURL;
        }
        console.log(timeOfDelay);
        setTimeout(function() {
            window.location = destination;
        }, timeOfDelay);
    }

    function replaceTag(tag) {
        return tagsToReplace[tag] || tag;
    }

    function safeTagsReplace(str) {
        return str.replace(/[&<>]/g, replaceTag);
    }

    // :::::::::: Visualisations ::::::::::::::

    var listLoader = (function(objects, parentId) {
        for (var i = 0; i < objects.length; i++) {
            $('#' + parentId + '> ul')
                .append($('<li class="lists" data-id="' + objects[i].Id +
                        '">' + objects[i].Name + '</li>')
                    .click(function() {
                        var id = $(this).data("id");
                        var name = $(this).text();
                        groupProcess.groupClicked(id, name);
                    }));
        }
    });

    var addMessagesToLogger = function (data) {
        utilities.clearMessages();
        var html = '';
        $.each(data, function (key, value) {
            html += '<li><small>[' + value.Time + '] <strong>' + value.UserName + '</strong>:</small> ' + value.MessageText + '</li>';
        });
        $('#mssgLogger').append(html);
    };

    var clearMessages = (function () {
        $('#mssgLogger').html('');
    });

    return {
        notify: notify,
        redirectToHome: redirectToHome,
        replaceTags: safeTagsReplace,
        listLoader: listLoader,
        clearMessages: clearMessages,
        addMessagesToLogger: addMessagesToLogger
    };
})();