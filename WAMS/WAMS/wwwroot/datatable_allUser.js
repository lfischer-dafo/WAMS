if (typeof dataTable === 'undefined') {
    console.log("datatable");

    const dataTable = new DataTable("#DB_user_list", {
        order: [[6, 'desc']],
        processing: true,
        serverSide: true,
        ajax: {
            url: "/API/GetAllUser",
            type: "GET",
            datatype: 'json',
            data: 'data.data'
        },
        language: {
            url: "/datatables/lang/German.json"
        },
        columnDefs: [
            { targets: [0], visible: false },
            { targets: [1], visible: true, className: "truncate", width: "30px" },
            { targets: [2], visible: true, width: "10px" },
            { targets: [3], visible: true, className: "truncate" },
            { targets: [4], visible: true, className: "truncate_small" },
            { targets: [5], visible: true, width: "10px" },
            { targets: [6], visible: true, width: "10px" },
            { targets: [7], visible: true, className: "truncate_small", width: "30px" },
            { targets: [8], visible: true, className: "text-center", orderable: false, width: "80px" },
            { targets: [9], visible: true, className: "text-center", orderable: false, width: "80px" }
        ],
        columns: [
            { data: "ID" },
            { data: "StructureAcronym" },
            { data: "ItemNo" },
            {
                data: "Question",
                render: function (data, type, full, meta) {
                    return "<div class='tablecolum'>" + data + "</div>";
                }
            },
            { data: "AnswerCount" },
            { data: "UserName" },
            { data: "Stamp" },
            { data: "Validated" },
            {
                render: function (data, type, full, meta) {
                    return '<a class="btn btn-info" href="../Admin/EditQuestion?ID=' + full.ID + '">Bearbeiten</a>';
                }
            },
            {
                render: function (data, type, full, meta) {
                    return "<a href='?ID=" + full.ID + "&action2=delite' id='sa-warning' class='btn btn-danger'>L&ouml;schen</a>";
                }
            },
        ],
        createdRow: function (row) {
            row.querySelectorAll(".truncate").forEach(function (element) {
                element.setAttribute("title", element.innerText);
            });
        }
    });
}