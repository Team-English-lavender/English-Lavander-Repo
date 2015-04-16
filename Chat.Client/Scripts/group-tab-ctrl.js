'use strict';
var groupProcess = (function() {

    function groupClicked(groupId, groupName) {
        var group = { id: groupId, name: groupName };
        groopStorage.save(group);
        var token = userSession.get().access_token;

        ajaxRequester.retrieveMessagesByGidLimited(token, groupId,
            function (data, statusText, xhr) {
                if (xhr.status == 200) {
                    utilities.addMessagesToLogger(data);
                } else if (xhr.status == 206) {
                    utilities.notify('info', Message);
                }                
            },
            function (data) {
                utilities.notify('error', 'It is not posibile to load messages now try later.');
            }, null);
    }
                
    return {
        groupClicked: groupClicked
    };
}());

