var ApiSpeechToTextToken = (function () {

    var messageEndpoint = rootURL + 'api/message/tokenSTT';

    return {
        sendRequest: sendRequest,
    };

    function sendRequest(callbackFunction) {
        var http = new XMLHttpRequest();
        http.open('POST', messageEndpoint, true);
        http.setRequestHeader('Content-type', 'application/json');
        http.onreadystatechange = function () {
            if (http.readyState === 4 && http.status === 200 && http.responseText) {
                callbackFunction(http.responseText);
            }
        };

        // Send request
        http.send();
    }
}());
