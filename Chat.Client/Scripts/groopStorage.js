'use strict';
var groopStorage = (function () {

    var group = 'currentGroup';

    function saveGroup(data) {
        localStorage['currentGroup'] = JSON.stringify(data);
    }

    function getGroup() {
        var group = localStorage['currentGroup'];
        if (group) {
            return JSON.parse(localStorage['currentGroup']);
        }
    }

    function clearGropu() {
        localStorage['currentGroup'].clear();
    }

    return {
        save: saveGroup,
        get: getGroup,
        clear: clearGropu
    };

})();