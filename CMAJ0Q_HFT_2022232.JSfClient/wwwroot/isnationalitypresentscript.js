async function checkNationalityPresence() {
    const nationality = document.getElementById('nationalityInput').value;
    if (!nationality) {
        alert('Please enter a nationality.');
        return;
    }
    try {
        const response = await fetch(`http://localhost:18344/stat/IsNationalityPresent/${nationality}`);
        const isPresent = await response.json();

        let resultText = isPresent
            ? `${nationality} is present!`
            : `${nationality} is not present!`;

        document.getElementById('nationalityResult').textContent = resultText;
    } catch (error) {
        console.error('Error:', error);
    }
}

