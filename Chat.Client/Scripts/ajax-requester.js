'use strict';

var ajaxRequester = (function () {

    // baseUrl must be changed when deploying application
    var baseUrl = "http://localhost:44965/api/";
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

        return makeRequest('POST', null, 'Account/Register', data, success, error);
    }

    return {
        register: register
    };

})();