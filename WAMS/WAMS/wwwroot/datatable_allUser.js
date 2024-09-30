window.initializeDataTable2 = function () {
    const dataTable = new DataTable("#DB_user_list2", {
        language: {
            url: "/datatables/lang/Deutsch.json"
        }
    });
};
window.initializeDataTable = function () {
    if (typeof dataTable === 'undefined') {
        console.log("Initializing DataTable");

        const dataTable = new DataTable("#DB_user_list", {
            order: [[0, 'asc']], // Sortiere nach ID
            processing: true,
            serverSide: true,
            ajax: {
                url: "/API/GetAllUser", // API-Endpunkt für die Daten
                type: "GET",
                datatype: 'json',
                dataSrc: function (json) {
                    return json.data;
                }
            },
            language: {
                url: "/datatables/lang/Deutsch.json"
            },
            columnDefs: [
                { targets: [0], visible: true, width: "50px" }, // ID
                {
                    targets: [1],
                    visible: true,
                    width: "100px",
                    className: "text-center",
                    render: function (data, type, full, meta) {
                        return '<img src="/images/avatar-placeholder.png" class="avatar-img" alt="Avatar" width="50" height="50">';
                    }
                }, // Avatar als Platzhalter
                {
                    targets: [2],
                    visible: true,
                    className: "truncate",
                    width: "200px",
                    render: function (data, type, full, meta) {
                        return full.LastName + ", " + full.FirstName; // Nachname, Vorname kombiniert
                    }
                }, // Nachname, Vorname (kombiniert)
                { targets: [3], visible: true, width: "150px", data: "Username" }, // Benutzername
                { targets: [4], visible: true, width: "100px", data: "Role" }, // Rolle
                { targets: [5], visible: true, width: "100px", data: "Status" }, // Status
                { targets: [6], visible: true, width: "150px", data: "LastLogin" }, // Letzter Login
                {
                    targets: [7],
                    visible: true,
                    className: "text-center",
                    orderable: false,
                    width: "160px",
                    render: function (data, type, full) {
                        return `
                            <a class="btn btn-info" sendDataToBlazor('${full.ID}')>Bearbeiten</a>
                            <a href="?ID=${full.ID}&action2=delete" id="sa-warning" class="btn btn-danger">Löschen</a>
                        `;
                    }
                } // Action-Buttons (Bearbeiten und Löschen)
            ],
            columns: [
                { data: "ID" }, // ID
                { data: null }, // Avatar (Platzhalter)
                {
                    render: function (data, type, full) {
                        return full.LastName + ", " + full.FirstName; // Nachname, Vorname
                    }
                }, // Nachname, Vorname
                { data: "Username" }, // Benutzername
                { data: "Role" }, // Rolle
                { data: "Status" }, // Status
                { data: "LastLogin" }, // Letzter Login
                {
                    render: function (data, type, full) {
                        return `
                            <a class="btn btn-info" onclick="sendDataToBlazor('${full.ID}')" >Bearbeiten</a>
                            <a href="?ID=${full.ID}&action2=delete" id="sa-warning" class="btn btn-danger">Löschen</a>
                        `;
                    }
                } // Action-Buttons (Bearbeiten und Löschen)
            ],
            createdRow: function (row) {
                row.querySelectorAll(".truncate").forEach(function (element) {
                    element.setAttribute("title", element.innerText); // Tooltip für abgeschnittene Inhalte
                });
            }
        });
    } else {
        console.log("DataTable bereits initialisiert");
    }
};
