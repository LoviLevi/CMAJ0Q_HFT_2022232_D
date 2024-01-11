async function getNicknameCount() {
    try {
        const response = await fetch('http://localhost:18344/stat/teamnicknamecount');
        const data = await response.json();

        let displayString = 'Nickname -> Count<br>';
        data.forEach(item => {
            displayString += `${item.key} -> ${item.value}<br>`;
        });

        document.getElementById('nicknameCount').innerHTML = displayString;
    } catch (error) {
        console.error('Error:', error);
    }
}

window.onload = getNicknameCount;
