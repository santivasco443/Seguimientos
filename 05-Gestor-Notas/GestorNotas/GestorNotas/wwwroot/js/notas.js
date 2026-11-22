
const editButtons = document.querySelectorAll(".edit-button");
const editModal = document.getElementById("edit-modal");
const editId = document.getElementById("edit-id");
const editTitle = document.getElementById("edit-title");
const editContent = document.getElementById("edit-content");
const cancelEdit = document.getElementById("cancel-edit");

editButtons.forEach(button => {
    button.addEventListener("click", () => {
        editId.value = button.dataset.id;
        editTitle.value = button.dataset.title;
        editContent.value = button.dataset.content;

        editModal.classList.add("visible");
        editTitle.focus();
    });
});

if (cancelEdit) {
    cancelEdit.addEventListener("click", () => {
        editModal.classList.remove("visible");
    });
}

if (editModal) {
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
