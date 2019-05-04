function confirmDelete(id) {
    let result = confirm("Are you sure?");
    if (result) {
        $.ajax({
            url: `/employees/delete/${id}`,
            type: "GET",
            success: function (res) {
                if (res) {
                    $(`#${id}`).remove();
                }
            },
            error: function (err) {
                console.log(err);
            }
        });
    }
}