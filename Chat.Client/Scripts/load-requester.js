'use strict';

var loadRequester = (function () {

    // baseUrl must be changed when deploying application
    //var baseUrl = "http://localhost:44965/";

    var baseUrl = "http://lavander-chat.apphb.com/";

    var headers = {};

    var makeRequest = function makeRequest(method, moreHeaders, url, data, success, error) {
        return $.ajax({
            type: method,
            headers: (moreHeaders) ? moreHeaders : headers,
            url: baseUrl + url,
            contentType: 'application/json',
            data: data,
            success: success,
            error: error
        });
    }

    function loadCurrentUser(token, success, error) {
        headers["Authorization"] = "Bearer " + token;
        makeRequest("GET", headers, "api/Users/GetCurrentUser", null, success, error);
    }

    function loadGroups(token, success, error) {
        headers["Authorization"] = "Bearer " + token;
        makeRequest("GET", headers, "api/groups/GetUserGroups", null, success, error);
    }

    function loadFriends(token, success, error) {
        headers["Authorization"] = "Bearer " + token;
        makeRequest("GET", headers, "api/Users/GetAllFriends", null, success, error);
    }

    return {
        loadGroups: loadGroups,
        loadCurrentUser: loadCurrentUser,
        loadFriends: loadFriends
    };

})();