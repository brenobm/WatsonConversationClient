// The Api module is designed to handle all interactions with the server

var ApiTextToSpeechToken = (function () {
    var requestPayload;
    var responsePayload;
    var messageEndpoint = rootURL + 'api/message/tokenTTS';

    // Publicly accessible methods defined
    return {
        sendRequest: sendRequest,

        // The request/response getters/setters are defined here to prevent internal methods
        // from calling the methods without any of the callbacks that are added elsewhere.
        getRequestPayload: function () {
            return requestPayload;
        },
        setRequestPayload: function (newPayloadStr) {
            requestPayload = JSON.parse(newPayloadStr);
        },
        getResponsePayload: function () {
            return responsePayload;
        },
        setResponsePayload: function (newPayloadStr) {
            responsePayload = JSON.parse(newPayloadStr);
        }
    };

    // Send a message request to the server
    function sendRequest(callbackFunction, newPayloadStr) {
        // Built http request
        var http = new XMLHttpRequest();
        http.open('POST', messageEndpoint, true);
        http.setRequestHeader('Content-type', 'application/json');
        http.onreadystatechange = function () {
            if (http.readyState === 4 && http.status === 200 && http.responseText) {
                callbackFunction(http.responseText, newPayloadStr);
            }
        };

        // Send request
        http.send();
    }
}());
