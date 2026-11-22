
const board = document.getElementById("game-board");
const movesDisplay = document.getElementById("moves");
const matchesDisplay = document.getElementById("matches");
const timerDisplay = document.getElementById("timer");
const messageDisplay = document.getElementById("game-message");
const restartButton = document.getElementById("restart-button");

const symbols = [
    "🍎", "🚀", "🎮", "⭐",
    "🐱", "⚽", "🎵", "🌈"
];

let firstCard = null;
let secondCard = null;
let locked = false;
let moves = 0;
let matches = 0;
let seconds = 0;
let timerInterval = null;
let started = false;

const totalPairs = Number(board.dataset.pairs);

function shuffle(array) {
    for (let i = array.length - 1; i > 0; i--) {
        const j = Math.floor(Math.random() * (i + 1));
        [array[i], array[j]] = [array[j], array[i]];
    }

    return array;
}

function updateTimer() {
    const minutes = Math.floor(seconds / 60);
    const remainingSeconds = seconds % 60;

    timerDisplay.textContent =
        String(minutes).padStart(2, "0") + ":" +
        String(remainingSeconds).padStart(2, "0");
}

function startTimer() {
    if (started) return;

    started = true;

    timerInterval = setInterval(() => {
        seconds++;
        updateTimer();
    }, 1000);
}

function createBoard() {
    board.innerHTML = "";

    const cards = shuffle([...symbols.slice(0, totalPairs), ...symbols.slice(0, totalPairs)]);

    cards.forEach(symbol => {
        const card = document.createElement("button");

        card.type = "button";
        card.className = "memory-card";
        card.textContent = "?";
        card.dataset.symbol = symbol;
        card.setAttribute("aria-label", "Carta oculta");

        card.addEventListener("click", () => flipCard(card));

        board.appendChild(card);
    });
}

function flipCard(card) {
    if (locked || card.classList.contains("flipped") ||
        card.classList.contains("matched")) {
        return;
    }

    startTimer();

    card.classList.add("flipped");
    card.textContent = card.dataset.symbol;
    card.setAttribute("aria-label", "Carta " + card.dataset.symbol);

    if (!firstCard) {
        firstCard = card;
        return;
    }

    secondCard = card;
    moves++;

    movesDisplay.textContent = moves;

    checkMatch();
}

function checkMatch() {
    const isMatch = firstCard.dataset.symbol === secondCard.dataset.symbol;

    if (isMatch) {
        firstCard.classList.add("matched");
        secondCard.classList.add("matched");

        firstCard.disabled = true;
        secondCard.disabled = true;

        matches++;
        matchesDisplay.textContent = `${matches} / ${totalPairs}`;

        messageDisplay.textContent = "¡Pareja encontrada!";

        resetSelection();

        if (matches === totalPairs) {
            finishGame();
        }

        return;
    }

    locked = true;
    messageDisplay.textContent = "No coinciden. ¡Inténtalo otra vez!";

    setTimeout(() => {
        firstCard.classList.remove("flipped");
        secondCard.classList.remove("flipped");

        firstCard.textContent = "?";
        secondCard.textContent = "?";

        firstCard.setAttribute("aria-label", "Carta oculta");
        secondCard.setAttribute("aria-label", "Carta oculta");

        resetSelection();
    }, 900);
}

function resetSelection() {
    firstCard = null;
    secondCard = null;
    locked = false;
}

function finishGame() {
    clearInterval(timerInterval);

    messageDisplay.textContent =
        `¡Ganaste! Encontraste todas las parejas en ${moves} movimientos y ${timerDisplay.textContent}.`;
}

function restartGame() {
    clearInterval(timerInterval);

    firstCard = null;
    secondCard = null;
    locked = false;
    moves = 0;
    matches = 0;
    seconds = 0;
    started = false;

    movesDisplay.textContent = "0";
    matchesDisplay.textContent = `0 / ${totalPairs}`;
    messageDisplay.textContent = "Encuentra todas las parejas para ganar.";

    updateTimer();
    createBoard();
}

restartButton.addEventListener("click", restartGame);

restartGame();
