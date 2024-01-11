let teams = [];
let teamIdToUpdate = -1;

getTeamData();

async function getTeamData() {
    fetch('http://localhost:18344/team')
        .then(x => x.json())
        .then(y => {
            teams = y;
            console.log(teams);
            displayTeams();
        });
}

function displayTeams() {
    document.getElementById('teamresultarea').innerHTML = "";
    teams.forEach(team => {
        document.getElementById('teamresultarea').innerHTML += `<tr><td>${team.teamId}</td><td>${team.name}</td><td>${team.nickname}</td><td><button type="button" onclick="removeTeam(${team.teamId})">Delete</button><button type="button" onclick="showUpdateTeam(${team.teamId})">Update</button></td></tr>`;
    });
}

function showUpdateTeam(id) {
    const team = teams.find(x => x['teamId'] == id);
    document.getElementById('teamnametoupdate').value = team['name'];
    document.getElementById('teamnicknametoupdate').value = team['nickname'];

    document.getElementById('updateformdiv').style.display = "flex";
    teamIdToUpdate = id;
}

function updateTeam() {
    let name = document.getElementById('teamnametoupdate').value;
    let nickname = document.getElementById('teamnicknametoupdate').value;

    document.getElementById('updateformdiv').style.display = 'none';

    fetch(`http://localhost:18344/team`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify({ teamId: teamIdToUpdate, name: name, nickname: nickname })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getTeamData();
        })
        .catch((error) => { console.error('Error:', error); });
}

function createTeam() {
    let name = document.getElementById('teamname').value;
    let nickname = document.getElementById('teamnickname').value;

    fetch('http://localhost:18344/team', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify({ name: name, nickname: nickname })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getTeamData();
        })
        .catch((error) => { console.error('Error:', error); });
}

function removeTeam(id) {
    fetch(`http://localhost:18344/team/${id}`, {
        method: 'DELETE'
    })
        .then(response => response)
        .then(data => {
            console.log('Success', data);
            getTeamData();
        })
        .catch((error) => { console.log('Error:', error); });
}
