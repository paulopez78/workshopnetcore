const votingApi = (function () {
    const commandsApiUrl = `//${window.location.hostname}/commands/voting/`;
    const queriesApiUrl = `//${window.location.hostname}/queries/voting/`;

    const client = (url, method, body) =>
        fetch(url, { method, headers: { Accept: 'application/json', 'Content-Type': 'application/json' }, body });
    
    const get = () =>
        client(queriesApiUrl, 'GET')
        .then(r => r.json());
    
    const start = (topics) =>
        client(commandsApiUrl, 'POST', JSON.stringify(topics))
        .then(r => r.json());

    const finish = () =>
        client(commandsApiUrl, 'DELETE')
        .then(r => r.json());

    const vote = (topic) =>
        client(commandsApiUrl, 'PUT', `"${topic}"`)
        .then(r => r.json());

    const subscribe = (action) => {
        const webSocket = new WebSocket(`ws://${window.location.hostname}/queries/ws`);
        webSocket.onmessage = ({ data }) => 
            data.indexOf('Connected') === -1 && action(JSON.parse(data)); 
    }
    
    return {
        get,
        start,
        finish,
        vote,
        subscribe
    }
})();