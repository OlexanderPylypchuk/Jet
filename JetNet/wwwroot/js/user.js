$(document).ready(function () { loadDataTable(); })
function loadDataTable() {
    dataTable = $('#tblDataUser').DataTable({
        "ajax": { url: '/admin/user/getall' },
        "columns": [
            { data: 'id', "width": "10%", class: "text-center" },
            { data: 'email', "width": "15%", class: "text-center" },
            { data: 'role', "width": "15%", class: "text-center" },
            { data: 'name', "width": "30%", class: "text-center" },
            { data: 'company.Name', "width": "20%", class: "text-center" },
            {
                data: { id: 'id', lockoutEnd:'lockoutEnd'},
                "render": function (data) {
                    var today = new Date().getTime();
                    var lockoutEnd = new Date(data.lockoutEnd).getTime();
                    if (lockoutEnd < today) {
                        return `<div class="text-center">
                            <a onClick=LockUnlock('${data.id}') class="btn btn-primary mx-2">
							    <i class="bi bi-lock"></i> Unlocked</a>
                            <a onClick=RoleManagement('${data.id}') class="btn btn-danger mx-2">
							    <i class="bi bi-trash3"></i> Permission</a>
                        </div>`
                    }
                    else {
                        return`<div class="text-center">
                            <a onClick=LockUnlock('${data.id}') class="btn btn-danger mx-2">
							    <i class="bi bi-unlock"></i>Locked</a>
                            <a onClick=RoleManagement('${data.id}') class="btn btn-danger mx-2">
							    <i class="bi bi-trash3"></i>Permission</a>
                        </div>`
                    }
                    
                },
                "width": "10%"
            }
        ]
    });
}
function LockUnlock(id) {
    $.ajax({
        type: "POST",
        url: '/Admin/User/LockUnlock',
        data: JSON.stringify(id),
        contentType: "application/json",
        success: function (data) {
            dataTable.ajax.reload();
            toastr.success(data.message);
        }
    })
}

function RoleManagement(id) {
    window.location.href = '/Admin/User/RoleManagement/' + id;
}
