$(document).ready(function () {
    var url = window.location.search;
    var dataTableType;

    switch (true) {
        case url.includes("inprocess"):
            dataTableType = "inprocess";
            break;
        case url.includes("delayed"):
            dataTableType = "delayed";
            break;
        case url.includes("pending"):
            dataTableType = "pending";
            break;
        case url.includes("approved"):
            dataTableType = "approved";
            break;
        default:
            dataTableType = "all";
    }

    loadDataTable(dataTableType);
})
function loadDataTable(status) {
    DataTable = $('#tblDataOrder').DataTable({
        "ajax": { url: '/admin/order/getall?status='+status},
        "columns": [
            { data: 'id', "width": "10%", class: "text-center" },
            { data: 'user.email', "width": "15%", class: "text-center" },
            { data: 'user.name', "width": "15%", class: "text-center" },
            { data: 'priceTotal', "width": "20%", class: "text-center" },
            { data: 'orderingDate', "width": "30%", class: "text-center" },
            { data: 'orderStatus', "width": "20%", class: "text-center" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group text-center" role="group">
                            <a onClick=Delete('/admin/order/delete/${data}') class="btn btn-danger mx-2">
							    <i class="bi bi-trash3"></i>Delete</a>
                    </div>`
                },
                "width": "40%"
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
