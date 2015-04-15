'use strict';
var userSession = (function () {

    function saveSession(data) {
        localStorage['currentUser'] = JSON.stringify(data);
    }

    function addData(newData) {
        var data = JSON.parse(localStorage['currentUser']);
        console.log();
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
        add: addData,
        clear: clearSession
    };

})();