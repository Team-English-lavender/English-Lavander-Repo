'use strict';
var groupProcess = (function() {

    function groupClicked(groupId, groupName) {
        var group = { id: groupId, name: groupName };
        groopStorage.save(group);
        var token = userSession.get().access_token;
        ajaxRequester.retrieveMessagesByGidLimited(token, groupId,
            function (data) {
                utilities.addMessagesToLogger(data);
            },
            function (data) {
                utilities.notify('error', 'It is not imposibile to load messages now try later.');
            }, null);
    }
                
    return {
        groupClicked: groupClicked
    };
}());

