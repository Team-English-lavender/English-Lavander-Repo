'use strict';
var utilities = (function () {

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
        setTimeout(function () {
            window.location = destination;
        }, timeOfDelay);
    }

    function replaceTag(tag) {
        return tagsToReplace[tag] || tag;
    }

    function safeTagsReplace(str) {
        return str.replace(/[&<>]/g, replaceTag);
    }

    return {
        notify: notify,
        redirectToHome: redirectToHome,
        replaceTags: safeTagsReplace
    };

})();