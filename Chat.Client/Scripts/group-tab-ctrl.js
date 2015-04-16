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
                    utilities.notify('info', 'There are no messages in this group.');
                }                
            },
            function (data) {
                utilities.notify('error', 'It is not posibile to load messages now try later.');
            }, null);
    }

    function userClicked(userId, userName) {
        var token = userSession.get().access_token;
        // sessionToken, userName, userId, success, error
        ajaxRequester.addUserToFriends(token, userName, userId,
            function (data, statusText, xhr) {

                if (xhr.status == 200) {
                    utilities.notify('info', 'User added to friends.');
                }
            },
            function (data) {
                utilities.notify('error', 'It is not posibile to add user.');
            }, null);
    }
                
    return {
        groupClicked: groupClicked,
        userClicked: userClicked
    };
}());

