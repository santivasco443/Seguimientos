
document.addEventListener("DOMContentLoaded", () => {
    const contenedor = document.getElementById("opcionesContainer");
    const botonAgregar = document.getElementById("agregarOpcion");
    const formulario = document.getElementById("formEncuesta");

    if (!contenedor || !botonAgregar || !formulario) {
        return;
    }

    botonAgregar.addEventListener("click", () => {
        const cantidad = contenedor.querySelectorAll("input").length;

        if (cantidad >= 10) {
            alert("No puedes agregar más de 10 opciones.");
            return;
        }

        const input = document.createElement("input");
        input.type = "text";
        input.name = "opciones";
        input.maxLength = 100;
        input.required = true;
        input.placeholder = `Opción ${cantidad + 1}`;

        contenedor.appendChild(input);
    });

    formulario.addEventListener("submit", (event) => {
        const opciones = Array.from(
            contenedor.querySelectorAll("input")
        )
            .map(input => input.value.trim())
            .filter(valor => valor.length > 0);

        const opcionesUnicas = new Set(
            opciones.map(valor => valor.toLowerCase())
        );

        if (opciones.length < 2 || opciones.length > 10) {
            event.preventDefault();
            alert("Debes ingresar entre 2 y 10 opciones.");
            return;
        }

        if (opcionesUnicas.size !== opciones.length) {
            event.preventDefault();
            alert("No puedes ingresar opciones repetidas.");
        }
    });

    document.querySelectorAll(".form-eliminar").forEach(formularioEliminar => {
        formularioEliminar.addEventListener("submit", (event) => {
            const confirmar = confirm("¿Estás seguro de que deseas eliminar esta encuesta y todos sus votos?");

            if (!confirmar) {
                event.preventDefault();
            }
        });
    });
});
