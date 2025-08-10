function showAlert(message) {
    alert(message);
}

function createFallingContainer() {
    const container = document.createElement('div');
    container.classList.add('falling-container');
    return container;
}

function createHeart() {
    const heart = document.createElement('div');
    heart.textContent = '❤';
    heart.classList.add('falling-heart');
    heart.style.left = `${Math.random() * 100}%`;
    heart.style.animationDuration = `${Math.random() * 5 + 5}s`;
    return heart;
}

function createSnowflake() {
    const snowflake = document.createElement('div');
    snowflake.textContent = '❄';
    snowflake.classList.add('falling-snowflake');
    snowflake.style.left = `${Math.random() * 100}%`;
    snowflake.style.animationDuration = `${Math.random() * 5 + 5}s`;
    return snowflake;
}

function fallInContainer(action, container) {
    const interval = 1000;
    const timeout = 15000;

    setInterval(() => {
        var div = action();
        container.appendChild(div);
        setTimeout(() => {
            div.remove();
        }, timeout);
    }, interval);
}

const createFallingEffects = () => {
    const fallingContainer = createFallingContainer();
    document.body.appendChild(fallingContainer);
    fallInContainer(createHeart, fallingContainer);
    //fallInContainer(createSnowflake, fallingContainer);
}

window.onload = createFallingEffects;
