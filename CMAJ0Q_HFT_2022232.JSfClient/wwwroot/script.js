let championships = [];
let connection = null;

let championshipIdToUpdate = -1;

getdata();
setupSignalR();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:18344/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("ChampionshipCreated", (user, message) => {
        getdata();
    });

    connection.on("ChampionshipDeleted", (user, message) => {
        getdata();
    });

    connection.on("ChampionshipUpdated", (user, message) => {
        getdata();
    });

    connection.onclose(async () => {
        await start();
    });
    start();
}

async function start() {
    try {
        await connection.start();
        console.log('SignalR Connected.');
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
}


async function getdata() {
    fetch('http://localhost:18344/championship')
        .then(x => x.json())
        .then(y => {
            championships = y;
            console.log(championships);
            display();
        });

}
function display() {
    document.getElementById('resultarea').innerHTML = "";
    championships.forEach(championship => {
        document.getElementById('resultarea').innerHTML += "<tr><td>" + championship.championshipId + "</td><td>"
            + championship.name + "</td><td>"
            + championship.location + "</td><td>"
            + `<button type="button" onclick="remove(${championship.championshipId})">Delete</button>`
            + `<button type="button" onclick="showupdate(${championship.championshipId})">Update</button>`
            + "</td></tr>";
        console.log(championship.name);
    });
}

function showupdate(id) {
    document.getElementById('championshipnametoupdate').value =
        championships.find(x => x['championshipId'] == id)['name'];

    document.getElementById('championshiplocationtoupdate').value =
        championships.find(x => x['championshipId'] == id)['location'];

    document.getElementById('updateformdiv').style.display = "flex";
    championshipIdToUpdate = id;
}
function update() {
    let name = document.getElementById('championshipnametoupdate').value;
    let location = document.getElementById('championshiplocationtoupdate').value;

    document.getElementById('updateformdiv').style.display = 'none';

    fetch('http://localhost:18344/championship', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { championshipId: championshipIdToUpdate, name: name, location: location }
        ),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}
function create() {
    let name = document.getElementById('championshipname').value;
    let location = document.getElementById('championshiplocation').value;

    fetch('http://localhost:18344/championship', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { name: name, location: location }
        ),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}
function remove(id) {
    fetch('http://localhost:18344/championship/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success', data);
            getdata();
        })
        .catch((error) => { console.log('Error:', error); });
}