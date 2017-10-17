var Recording = (function () {
    var stream = null;

    // Publicly accessible methods defined
    return {
        init: init,
        startListen: startListen,
        stopListen: stopListen
    };

    function init() {
        document.querySelector('#mic').addEventListener('click', startListen);
    }

    function startListen() {
        document.querySelector('#mic').src = urlMicOff;
        document.querySelector('#mic').removeEventListener('click', startListen);
        document.querySelector('#mic').addEventListener('click', stopListen);
        ApiSpeechToTextToken.sendRequest(CallbackSpeechToText);
    }

    function CallbackSpeechToText(dataStr) {
        data = JSON.parse(dataStr);
        data.outputElement = '#textInput';
        stream = WatsonSpeech.SpeechToText.recognizeMicrophone(data);
    }

    function stopListen() {
        stream.stop();
        document.querySelector('#mic').src = urlMicOn;
        document.querySelector('#mic').removeEventListener('click', stopListen);
        document.querySelector('#mic').addEventListener('click', startListen);

        var ev = document.createEvent('Event');
        ev.initEvent('keydown');
        ev.which = ev.keyCode = 13;
        document.getElementById('textInput').dispatchEvent(ev);
    }

}());
