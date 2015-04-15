'use strict';

var ajaxRequester = (function () {

    // baseUrl must be changed when deploying application
    var baseUrl = "http://localhost:44965/";

    //var baseUrl = "http://lavander-chat.apphb.com/";

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
        })
    }

    function register(username, password, confirmPass, email, success, error) {
        var data = JSON.stringify(
            {
                UserName: username,
                Password: password,
                ConfirmPassword: confirmPass,
                Email: email
            });

        return makeRequest('POST', null, 'api/Account/Register', data, success, error);
    }

    function login(username, password, success, error) {
        var data = { Username: username, Password: password, grant_type: "password" };

        return makeRequest('POST', null, 'token', data, success, error);
    }

    // Do we need token for that request
    function logout(success, error) {
        return makeRequest('POST', null, 'api/Account/Logout', null, success, error);
    }

    function postMessage(sessionToken, mssg, groupId, success, error) {
        var data = JSON.stringify({ Text: mssg, GroupId: groupId });
        headers['Authorization'] = 'Bearer ' + sessionToken;

        return makeRequest('POST', headers, 'api/Messages/PostMessage', data, success, error);
    }

    // Gid stands for group id
    function retrieveMessagesByGidLimited(sessionToken, groupId, success, error, count) {
        headers['Authorization'] = 'Bearer ' + sessionToken;
        var countParam = (count) ? '&count=' + count : '&count=';

        return makeRequest('GET', headers, 'api/Messages/GetLastByGroup?groupId=' + groupId + countParam, null, success, error);
    }

    function retrieveMessagesByGidAll(sessionToken, groupId, success, error) {
        headers['Authorization'] = 'Bearer ' + sessionToken;

        return makeRequest('GET', headers, 'api/Messages/GetAllByGroup?groupId=' + groupId, null, success, error);
    }

    return {
        register: register,
        login: login,
        logout: logout,
        postMessage: postMessage,
        retrieveMessagesByGidAll: retrieveMessagesByGidAll,
        retrieveMessagesByGidLimited: retrieveMessagesByGidLimited
    };

})();