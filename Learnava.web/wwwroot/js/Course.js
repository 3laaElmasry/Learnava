﻿var dataTable;
$(document).ready(function () {
    loadDataTable();
});
function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            url: '/Instructor/Course/GetAll',
            delay: 5000, // Poll every 5 seconds
            error: function (xhr, error, thrown) {
                console.log("Error loading data: ", error);
            }
        },
        searching: true,
        lengthChange: false,  
        paging: true,         
        pageLength: 10,
        info: false,
        "columns": [
            { data: 'title', "width": "10%" },
            { data: 'description', "width": "25%" },
            {
                data: 'createdAt', render: function (data) {
                    return new Date(data).toLocaleDateString(); // Formats date
                } , "width": "10%" },
            { data: 'instructorName', "width": "15%" },
            { data: 'instructorEmail', "width": "15%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
								<a href="/Instructor/Course/upsert?courseId=${data}"
								   class="btn btn-primary mx-2">
									<i class="bi bi-pencil-square"></i> Edit
								</a>
								<a onClick=Delete('/Instructor/Course/Delete?courseId=${data}')
								   class="btn btn-danger mx-2 ">
									<i class="bi bi-trash-fill"></i> Delete
								</a>
                                 <a href="/Instructor/Video/Index?courseId=${data}" class="btn btn-info mx-2">
                                    <i class="bi bi-camera-video-fill"></i> Videos
                                </a>
							</div>`
                },
                "width": "25%"
            },


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
                type: "Delete",
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            })
        }
    });
}

