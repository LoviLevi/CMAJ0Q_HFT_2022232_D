async function getAVGAgeOfTeamsPlayers() {
    try {
        const response = await fetch('http://localhost:18344/stat/avgageofteamsplayers');
        const data = await response.json();

        let displayString = 'Team name -> AVG Age of players<br>';
        data.forEach(item => {
            displayString += `${item.key} -> ${item.value}<br>`;
        });

        document.getElementById('avgAgeOfTeamsPlayers').innerHTML = displayString;
    } catch (error) {
        console.error('Error:', error);
    }
}


window.onload = getAVGAgeOfTeamsPlayers;
