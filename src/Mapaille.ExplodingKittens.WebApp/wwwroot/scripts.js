function showAlert(message) {
    alert(message);
}

const createFallingHearts = () => {

    // Create the container
    const loveContainer = document.createElement('div');
    loveContainer.id = 'love-container';

    // Style it (optional, could be done in CSS/SCSS)
    loveContainer.style.position = 'fixed'; // or 'absolute' based on your layout
    loveContainer.style.top = '0';
    loveContainer.style.left = '0';
    loveContainer.style.width = '100%';
    loveContainer.style.height = '100vh'; // full viewport height
    loveContainer.style.pointerEvents = 'none'; // so it doesn't interfere with clicks

    // Append it to the body or another element
    document.body.appendChild(loveContainer);

    setInterval(() => {
        const heart = document.createElement('div');
        heart.textContent = '❤️';
        heart.style.fontSize = '2rem';
        heart.classList.add('falling-heart');
        heart.style.left = `${Math.random() * 100}%`;
        heart.style.animationDuration = `${Math.random() * 5 + 5}s`; // Random duration for variety
        loveContainer.appendChild(heart);

        // Remove heart after animation
        setTimeout(() => {
            heart.remove();
        }, 15000);
    }, 3000); // Adjust interval to control heart frequency
}

window.onload = createFallingHearts;
