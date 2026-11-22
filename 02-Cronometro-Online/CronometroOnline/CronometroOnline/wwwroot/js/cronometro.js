document.addEventListener("DOMContentLoaded", () => {

    const timerDisplay = document.getElementById("timerDisplay");

    const startButton = document.getElementById("startButton");
    const pauseButton = document.getElementById("pauseButton");
    const lapButton = document.getElementById("lapButton");
    const resetButton = document.getElementById("resetButton");

    const lapsList = document.getElementById("lapsList");
    const lapCounter = document.getElementById("lapCounter");

    const statusText = document.getElementById("statusText");
    const statusDot = document.getElementById("statusDot");

    let startTime = 0;
    let elapsedTime = 0;
    let animationFrame = null;
    let isRunning = false;
    let laps = [];

    function formatTime(milliseconds) {

        const totalCentiseconds = Math.floor(milliseconds / 10);

        const hours = Math.floor(totalCentiseconds / 360000);
        const minutes = Math.floor(totalCentiseconds / 6000) % 60;
        const seconds = Math.floor(totalCentiseconds / 100) % 60;
        const centiseconds = totalCentiseconds % 100;

        return {
            full:
                `${String(hours).padStart(2, "0")}:` +
                `${String(minutes).padStart(2, "0")}:` +
                `${String(seconds).padStart(2, "0")}.` +
                `${String(centiseconds).padStart(2, "0")}`
        };
    }

    function updateDisplay() {

        const currentTime = elapsedTime +
            (isRunning ? performance.now() - startTime : 0);

        const formatted = formatTime(currentTime).full;

        const parts = formatted.split(".");

        timerDisplay.innerHTML =
            `${parts[0]}<span class="milliseconds">.${parts[1]}</span>`;
    }

    function updateTimer() {

        if (!isRunning) return;

        updateDisplay();

        animationFrame = requestAnimationFrame(updateTimer);
    }

    function startTimer() {

        if (isRunning) return;

        startTime = performance.now();
        isRunning = true;

        statusText.textContent = "EN MARCHA";
        statusDot.classList.add("running");

        startButton.disabled = true;
        pauseButton.disabled = false;
        lapButton.disabled = false;

        updateTimer();
    }

    function pauseTimer() {

        if (!isRunning) return;

        elapsedTime += performance.now() - startTime;

        isRunning = false;

        cancelAnimationFrame(animationFrame);

        updateDisplay();

        statusText.textContent = "PAUSADO";
        statusDot.classList.remove("running");

        startButton.disabled = false;
        pauseButton.disabled = true;
        lapButton.disabled = true;
    }

    function resetTimer() {

        isRunning = false;

        cancelAnimationFrame(animationFrame);

        elapsedTime = 0;
        startTime = 0;
        laps = [];

        updateDisplay();
        renderLaps();

        statusText.textContent = "DETENIDO";
        statusDot.classList.remove("running");

        startButton.disabled = false;
        pauseButton.disabled = true;
        lapButton.disabled = true;
    }

    function registerLap() {

        if (!isRunning) return;

        const currentTime = elapsedTime +
            (performance.now() - startTime);

        const previousLap = laps.length > 0
            ? laps[laps.length - 1].total
            : 0;

        laps.push({
            number: laps.length + 1,
            total: currentTime,
            partial: currentTime - previousLap
        });

        renderLaps();
    }

    function renderLaps() {

        lapCounter.textContent =
            `${laps.length} ${laps.length === 1 ? "vuelta" : "vueltas"}`;

        if (laps.length === 0) {

            lapsList.innerHTML = `
                <div class="empty-laps">
                    <span>🏁</span>
                    <p>Aún no hay vueltas registradas.</p>
                    <small>Inicia el cronómetro y registra tu primera vuelta.</small>
                </div>
            `;

            return;
        }

        lapsList.innerHTML = "";

        [...laps].reverse().forEach(lap => {

            const item = document.createElement("div");

            item.className = "lap-item";

            const total = formatTime(lap.total).full;
            const partial = formatTime(lap.partial).full;

            const number = document.createElement("span");
            number.className = "lap-number";
            number.textContent = `Vuelta ${lap.number}`;

            const partialTime = document.createElement("span");
            partialTime.className = "lap-partial";
            partialTime.textContent = partial;

            const totalTime = document.createElement("span");
            totalTime.className = "lap-total";
            totalTime.textContent = total;

            item.append(number, partialTime, totalTime);

            lapsList.appendChild(item);
        });
    }

    startButton.addEventListener("click", startTimer);
    pauseButton.addEventListener("click", pauseTimer);
    resetButton.addEventListener("click", resetTimer);
    lapButton.addEventListener("click", registerLap);

    updateDisplay();

});