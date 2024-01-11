document.addEventListener('DOMContentLoaded', function () {
    fetchChampionships();
});

function fetchChampionships() {
    fetch('http://localhost:18344/championship')
        .then(response => response.json())
        .then(data => {
            const select = document.getElementById('championships');
            data.forEach(championship => {
                const option = document.createElement('option');
                option.value = championship.championshipId;
                option.textContent = championship.name;
                select.appendChild(option);
            });
        });
}

function getNicknameCount() {
    const championshipId = document.getElementById('championships').value;
    fetch(`http://localhost:18344/stat/teamnicknamecountinspecificchampionship/${championshipId}`)
        .then(response => response.json())
        .then(data => {
            const countsList = document.getElementById('counts');
            countsList.innerHTML = '';
            data.forEach(item => {
                const listItem = document.createElement('li');
                listItem.textContent = `${item.key} -> ${item.value}`;
                countsList.appendChild(listItem);
            });
        });
}
