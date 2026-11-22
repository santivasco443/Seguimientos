const editButtons = document.querySelectorAll(".edit-button");
const editModal = document.getElementById("edit-modal");
const editId = document.getElementById("edit-id");
const editDescription = document.getElementById("edit-description");
const cancelEdit = document.getElementById("cancel-edit");

editButtons.forEach(button => {
    button.addEventListener("click", () => {
        editId.value = button.dataset.id;
        editDescription.value = button.dataset.description;

        editModal.classList.add("visible");
        editDescription.focus();
    });
});

if (cancelEdit)
{
    cancelEdit.addEventListener("click", () => {
        editModal.classList.remove("visible");
    });
}

if (editModal)
{
    editModal.addEventListener("click", event => {
        if (event.target === editModal) {
            editModal.classList.remove("visible");
        }
    });
}

document.addEventListener("keydown", event => {
    if (event.key === "Escape" && editModal) {
        editModal.classList.remove("visible");
    }
});