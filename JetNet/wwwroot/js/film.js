$(document).ready(function () { loadDataTable(); })
function loadDataTable() {
    DataTable = $('#tblData').DataTable({
        "ajax": { url:'/admin/film/getall'}, 
        "columns": [
            { data: 'title', "width":"15%" },
            { data: 'producer',"width": "15%" },
            { data: 'price', "width": "15%" },
            { data: 'description', "width": "15%" },
            { data: 'score', "width": "15%" },
            { data: 'category.name', "width": "15%" }
        ]
    });
}


