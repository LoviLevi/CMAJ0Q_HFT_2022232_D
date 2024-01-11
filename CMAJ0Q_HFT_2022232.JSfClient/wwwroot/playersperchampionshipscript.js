async function getPlayersPerChampionship() {
    try {
        const response = await fetch('http://localhost:18344/stat/playersperchampionship');
        const data = await response.json();

        let displayString = 'Championship name -> Count of players<br>';
        data.forEach(item => {
            displayString += `${item.key} -> ${item.value}<br>`;
        });

        document.getElementById('playersPerChampionship').innerHTML = displayString;
    } catch (error) {
        console.error('Error:', error);
    }
}

window.onload = getPlayersPerChampionship;
