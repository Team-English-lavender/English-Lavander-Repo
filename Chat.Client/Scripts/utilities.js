﻿'use strict';
var utilities = (function() {

    var tagsToReplace = {
        '&': '&amp;',
        '<': '&lt;',
        '>': '&gt;'
    };

    /********Notifications************/
    function notify(type, msg, timeout) {
        var time;

        if (timeout) {
            time = timeout;
        } else {
            time = (type == 'success') ? 500 : 1000;
        }

        noty({
            text: msg,
            type: type,
            layout: 'topCenter',
            timeout: time
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

    var listLoader = (function (objects, parentId) {
        var $item = $('#' + parentId + '> ul').html('');
        for (var i = 0; i < objects.length; i++) {
            $item
                .append($('<li class="lists" data-id="' + objects[i].Id +
                        '">' + objects[i].Name + '</li>')
                    .click(function() {
                        var id = $(this).data("id");
                        var name = $(this).text();
                        $('.groupNameLabel').text(' - ' + name);
                        groupProcess.groupClicked(id, name);
                    }));
        }
    });

    var listLoaderUser = (function (objects, parentId) {
        var $item = $('#' + parentId + '> ul').html('');
        for (var i = 0; i < objects.length; i++) {
            $item.append($('<li class="lists" data-id="' + objects[0][i].Id +
                        '">' + objects[0][i].UserName + '</li>')
                    .click(function () {
                        var id = $(this).data("id");
                        var name = $(this).text();
                        groupProcess.groupClicked(id, name);
                    }));
        }
    });
    // <option value="volvo">Volvo</option>
    var selectLoader = (function (objects, parentId) {
        var $item = $('#' + parentId).html('');
        for (var i = 0; i < objects.length; i++) {
            var o = objects[i][0];
            $item.append($('<option class="lists" data-id="' + objects[i].Id+
                        '">' + objects[i].UserName + '</option>')
                    .click(function () {
                        var id = $(this).data("id");
                        var name = $(this).text();
                        groupProcess.userClicked(id, name);
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
        addMessagesToLogger: addMessagesToLogger,
        selectLoader: selectLoader,
        listLoaderUser: listLoaderUser
    };
})();