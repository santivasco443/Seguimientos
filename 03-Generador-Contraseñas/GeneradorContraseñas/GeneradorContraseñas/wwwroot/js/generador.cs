const slider = document.getElementById("length-slider");
const lengthValue = document.getElementById("length-value");

if (slider && lengthValue)
{
    slider.addEventListener("input", function() {
        lengthValue.textContent = slider.value;
    });
}

const copyButton = document.getElementById("copy-button");
const passwordResult = document.getElementById("password-result");
const copyMessage = document.getElementById("copy-message");

if (copyButton && passwordResult)
{
    copyButton.addEventListener("click", async function() {
        try
        {
            await navigator.clipboard.writeText(passwordResult.value);
            copyMessage.textContent = "¡Contraseña copiada correctamente!";
        }
        catch
        {
            passwordResult.select();
            passwordResult.setSelectionRange(0, passwordResult.value.length);

            const copied = document.execCommand("copy");
            copyMessage.textContent = copied
                ? "¡Contraseña copiada correctamente!"
                : "No se pudo copiar la contraseña.";
        }
    });
}