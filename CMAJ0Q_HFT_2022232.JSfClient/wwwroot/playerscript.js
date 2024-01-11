let players = [];
let playerIdToUpdate = -1;

getPlayerData();

async function getPlayerData() {
    fetch('http://localhost:18344/player')
        .then(x => x.json())
        .then(y => {
            players = y;
            console.log(players);
            displayPlayers();
        });
}

function displayPlayers() {
    document.getElementById('playerresultarea').innerHTML = "";
    players.forEach(player => {
        document.getElementById('playerresultarea').innerHTML += `<tr><td>${player.playerId}</td><td>${player.name}</td><td>${player.nationality}</td><td>${player.age}</td><td>${player.position}</td><td><button type="button" onclick="removePlayer(${player.playerId})">Delete</button><button type="button" onclick="showUpdatePlayer(${player.playerId})">Update</button></td></tr>`;
    });
}

function showUpdatePlayer(id) {
    const player = players.find(x => x['playerId'] == id);
    document.getElementById('playernametoupdate').value = player['name'];
    document.getElementById('playernationalitytoupdate').value = player['nationality'];
    document.getElementById('playeragetoupdate').value = player['age'];
    document.getElementById('playerpositiontoupdate').value = player['position'];

    document.getElementById('updateformdiv').style.display = "flex";
    playerIdToUpdate = id;
}

function updatePlayer() {
    let name = document.getElementById('playernametoupdate').value;
    let nationality = document.getElementById('playernationalitytoupdate').value;
    let age = document.getElementById('playeragetoupdate').value;
    let position = document.getElementById('playerpositiontoupdate').value;

    document.getElementById('updateformdiv').style.display = 'none';

    fetch(`http://localhost:18344/player`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify({ playerId: playerIdToUpdate, name: name, nationality: nationality, age: age, position: position })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getPlayerData();
        })
        .catch((error) => { console.error('Error:', error); });
}


function createPlayer() {
    let name = document.getElementById('playername').value;
    let nationality = document.getElementById('playernationality').value;
    let age = document.getElementById('playerage').value;
    let position = document.getElementById('playerposition').value;

    fetch('http://localhost:18344/player', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify({ name: name, nationality: nationality, age: age, position: position })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getPlayerData();
        })
        .catch((error) => { console.error('Error:', error); });
}

function removePlayer(id) {
    fetch(`http://localhost:18344/player/${id}`, {
        method: 'DELETE'
    })
        .then(response => response)
        .then(data => {
            console.log('Success', data);
            getPlayerData();
        })
        .catch((error) => { console.log('Error:', error); });
}
