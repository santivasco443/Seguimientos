
const detailsButtons = document.querySelectorAll(".details-button");
const editButtons = document.querySelectorAll(".edit-button");

const detailsModal = document.getElementById("details-modal");
const editModal = document.getElementById("edit-modal");

detailsButtons.forEach(button => {
    button.addEventListener("click", () => {
        document.getElementById("details-name").textContent = button.dataset.name;
        document.getElementById("details-description").textContent = button.dataset.description;
        document.getElementById("details-category").textContent = button.dataset.category;
        document.getElementById("details-time").textContent = button.dataset.time;
        document.getElementById("details-ingredients").textContent = button.dataset.ingredients;
        document.getElementById("details-instructions").textContent = button.dataset.instructions;

        detailsModal.classList.add("visible");
    });
});

editButtons.forEach(button => {
    button.addEventListener("click", () => {
        document.getElementById("edit-id").value = button.dataset.id;
        document.getElementById("edit-name").value = button.dataset.name;
        document.getElementById("edit-description").value = button.dataset.description;
        document.getElementById("edit-category").value = button.dataset.category;
        document.getElementById("edit-time").value = button.dataset.time;
        document.getElementById("edit-ingredients").value = button.dataset.ingredients;
        document.getElementById("edit-instructions").value = button.dataset.instructions;

        editModal.classList.add("visible");
    });
});

document.querySelectorAll("[data-close]").forEach(button => {
    button.addEventListener("click", () => {
        document.getElementById(button.dataset.close).classList.remove("visible");
    });
});

[detailsModal, editModal].forEach(modal => {
    modal.addEventListener("click", event => {
        if (event.target === modal) {
            modal.classList.remove("visible");
        }
    });
});

document.addEventListener("keydown", event => {
    if (event.key === "Escape") {
        detailsModal.classList.remove("visible");
        editModal.classList.remove("visible");
    }
});
