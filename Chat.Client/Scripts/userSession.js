'use strict';
var userSession = (function () {

    function saveSession(data) {
        localStorage['currentUser'] = JSON.stringify(data);
    }

    function getSession() {
        var userData = localStorage['currentUser'];
        if (userData) {
            return JSON.parse(localStorage['currentUser']);
        }
    }

    function clearSession() {
        localStorage.clear();
    }

    return {
        save: saveSession,
        get: getSession,
        clear: clearSession
    };

})();