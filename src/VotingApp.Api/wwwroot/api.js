const votingApi = (function () {
    const apiUrl = `//${window.location.host}/api/voting/`;

    const client = (url, method, body) =>
        fetch(url, { method, headers: { Accept: 'application/json', 'Content-Type': 'application/json' }, body });
    
    const get = () =>
        client(apiUrl, 'GET')
        .then(r => r.json());
    
    const start = (topics) =>
        client(apiUrl, 'POST', JSON.stringify(topics))
        .then(r => r.json());

    const finish = () =>
        client(apiUrl, 'DELETE')
        .then(r => r.json());

    const vote = (topic) =>
        client(apiUrl, 'PUT', `"${topic}"`)
        .then(r => r.json());

    const subscribe = (action) => {
        const webSocket = new WebSocket(`ws://${window.location.host}/ws`);
        webSocket.onmessage = msg => {
            if (isJSON(msg.data)){
                action(JSON.parse(msg.data));
            }
        }
        
        function isJSON(str) {
            try {
                JSON.parse(str);
                return true;
            } catch (e) {
                return false;
            }
        }
    }


    return {
        get,
        start,
        finish,
        vote,
        subscribe
    }
})();