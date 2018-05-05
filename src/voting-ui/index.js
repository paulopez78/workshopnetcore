(function () {
    votingApi.get().then(render);
    votingApi.subscribe(render);

    function render(state) {
        document.getElementById('errors').innerHTML = '';
        state.error 
            ? document.getElementById('errors').innerText = state.error
            : renderUI();

        function renderUI() {
            renderAdmin();
            renderVoting();

            function renderAdmin()
            {
                const isAdmin = window.location.search.indexOf('admin') !== -1;
                if (isAdmin) {
                    document.getElementById('votingCommands').style.visibility = 'visible'; 
                    addEventHandler('start',
                        () => votingApi.start(document.getElementById('topics').value.split(','))
                        .then(render));
                    addEventHandler('finish',
                        () => votingApi.finish()
                        .then(render));
                }
            }

            function renderVoting()
            {
                document.getElementById('votingTopics').innerHTML = '';
                for (var topic in state.votes) {
                    document.getElementById('votingTopics').innerHTML +=
                        `<div id="${topic}" class="topic ${state.winner === topic ? 'selected' : ''}">
                            ${topic}, votes: ${state.votes[topic]}
                        </div>`;
                }
                for (var topic in state.votes) 
                {
                    (t => addEventHandler(t, () => votingApi.vote(t).then(render)))
                    (topic);
                }
            }
        }
    }

    const addEventHandler = (id, handler) => {
        var el = document.getElementById(id);
        var elClone = el.cloneNode(true);
        el.parentNode.replaceChild(elClone, el);
        document.getElementById(id).addEventListener('click', handler, false);
    }
})();