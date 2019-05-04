function confirmDelete(id) {
    let result = confirm("Are you sure?");
    if (result) {
        location.href = `/departments/delete/${id}`;
    }
}