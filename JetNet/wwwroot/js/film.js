﻿$(document).ready(function () { loadDataTable(); })
function loadDataTable() {
    DataTable = $('#tblData').DataTable({
        "ajax": { url:'/admin/film/getall'}, 
        "columns": [
            { data: 'title', "width": "12%" },
            { data: 'producer', "width": "12%" },
            { data: 'description', "width": "12%" },
            { data: 'price', "width": "12%" },
            { data: 'score', "width": "12%" },
            { data: 'category.name', "width": "12%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                            <a href="/admin/film/upsert?id=${data}" class="btn btn-primary mx-2">
							    <i class="bi bi-pencil-square"></i>Edit</a>
                            <a onClick=Delete('/admin/film/delete/${data}') class="btn btn-danger mx-2">
							    <i class="bi bi-trash3"></i>Delete</a>
                    </div>`
                },
                "width": "20%"
            }
        ]
    });
}

function Delete(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    DataTable.ajax.reload();
                    toastr.success(data.message);
                }
            })
        }
    });
}


