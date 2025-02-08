function showAlert(message) {
    alert(message);
}

const createFallingEffects = () => {

    // Create the container
    const container = document.createElement('div');

    container.id = 'falling-container';
    container.style.position = 'fixed'; // or 'absolute' based on your layout
    container.style.top = '0';
    container.style.left = '0';
    container.style.width = '100%';
    container.style.height = '100vh'; // full viewport height
    container.style.pointerEvents = 'none'; // so it doesn't interfere with clicks

    // Append it to the body or another element
    document.body.appendChild(container);

    setInterval(() => {
        const heart = document.createElement('div');

        heart.textContent = '❤';
        heart.classList.add('falling-heart');
        heart.style.left = `${Math.random() * 100}%`;
        heart.style.animationDuration = `${Math.random() * 5 + 5}s`; // Random duration for variety

        container.appendChild(heart);

        // Remove heart after animation
        setTimeout(() => {
            heart.remove();
        }, 15000);
    }, 1000); // Adjust interval to control heart frequency

    setInterval(() => {
        const snowflake = document.createElement('div');

        snowflake.textContent = '❄';
        snowflake.classList.add('falling-snowflake');
        snowflake.style.left = `${Math.random() * 100}%`;
        snowflake.style.animationDuration = `${Math.random() * 5 + 5}s`; // Random duration for variety

        container.appendChild(snowflake);

        // Remove heart after animation
        setTimeout(() => {
            snowflake.remove();
        }, 15000);
    }, 1000); // Adjust interval to control heart frequency
}

window.onload = createFallingEffects;
