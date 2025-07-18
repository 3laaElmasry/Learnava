var enrollmentTable;

$(document).ready(function () {
    loadEnrollmentTable();
});

function loadEnrollmentTable() {
    enrollmentTable = $('#tblData').DataTable({
        ajax: {
            url: '/Student/Enrollment/GetAll',
            type: 'GET',
            dataSrc: 'data',
            error: function (xhr, error, thrown) {
                console.error("Error loading enrollment data:", error);
            }
        },
        searching: false,   
        lengthChange: false,
        paging: false,      
        pageLength: 10,    
        info: false,        
        columns: [
            { data: 'instructorName', width: "15%" },
            { data: 'instructorEmail', width: "20%" },
            { data: 'studentName', width: "15%" },
            { data: 'studentEmail', width: "20%" },
            { data: 'courseTitle', width: "15%" },
            {
                data: 'enrollDate',
                render: function (data) {
                    return new Date(data).toLocaleDateString();
                },
                width: "10%"
            },
            {
                data: 'id',
                render: function (data, type, row) {
                    const courseId = row.courseId;
                    return `
                        <div class="btn-group w-100" role="group">
                            <a href="/Student/Enrollment/CourseContent?courseId=${courseId}" class="btn btn-sm btn-outline-primary mx-1">
                                <i class="bi bi-journal-text"></i> Course Content
                            </a>
                            <a onClick="Delete('/Student/Enrollment/Delete?id=${data}')" class="btn btn-sm btn-outline-danger mx-1">
                                <i class="bi bi-trash-fill"></i> Delete
                            </a>
                       </div>
                         `;

                },
                orderable: false,
                width: "20%"
            }
        ]

    });
}

function Delete(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this enrollment!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: "DELETE",
                success: function (data) {
                    enrollmentTable.ajax.reload();
                    toastr.success(data.message);
                },
                error: function () {
                    toastr.error("Failed to delete enrollment.");
                }
            });
        }
    });
}
