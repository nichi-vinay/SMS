$(document).ready(function () {
    // Fetch vendor names from the server
    $.ajax({
        type: "GET",
        url: "https://localhost:7224/api/Vendors",
        contentType: "application/json",
        dataType: "json",
        success: function (data) {
            var uniqueVendorNames = [...new Set(data.map(vendor => vendor.name))];
            var dropdown = $('#vendorDropdown');
            dropdown.append($('<option>', {
                value: '', // Add an empty value to represent 'All Vendors'
                text: 'All Vendors'
            }));
            $.each(uniqueVendorNames, function (i, vendorName) {
                dropdown.append($('<option>', {
                    value: vendorName,
                    text: vendorName
                }));
            });

            // Initial load of data into the table
            fetchDataFromAPI();
        },
        error: function (error) {
            console.error("Error fetching vendor names: ", error);
        }
    });

    // Initialize DataTable
    var dataTable = $("#example1").DataTable({
        "responsive": true,
        "lengthChange": false,
        "autoWidth": false,
        "buttons": [
            'copy', 'csv', 'excel', 'pdf', 'print', 'colvis'
        ]
    });

    // Initialize DataTables Buttons
    new $.fn.dataTable.Buttons(dataTable, {
        buttons: [
            'copy', 'csv', 'excel', 'pdf', 'print', 'colvis'
        ]
    });

    // Append Buttons container to the DataTable wrapper
    dataTable.buttons().container().appendTo('#example1_wrapper .col-md-6:eq(0)');

    // Function to load data into the table
    function loadDataIntoTable(data) {
        var tableBody = $('#TableBody');
        tableBody.empty(); // Clear existing rows

        var selectedVendorName = $('#vendorDropdown').val();

        // Add new rows
        $.each(data, function (i, item) {
            // Check if the vendor name matches the selected vendor or if 'All Vendors' is selected
            if (!selectedVendorName || item.name === selectedVendorName) {
                var row = "<tr>" +
                    "<td class='vendor-id' style='display:none;'>" + item.id + "</td>" +
                    "<td>" + item.name + "</td>" +
                    "<td>" + item.mobile + "</td>" +
                    "<td>" + item.address + "</td>" +
                    "<td>" + item.email + "</td>" +
                    "<td>" + item.taxTypeName + "</td>" +
                    "<td>" + item.taxNumber + "</td>" +
              
                "<td>" +
                    "<i class='fas fa-edit fa-2x btn-edit-order' data-toggle='tooltip' data-placement='top' title='Edit' style='cursor: pointer; color: #0000FF;'></i>" +
                    "&nbsp;" +
                    "<i class='fas fa-trash fa-2x btn-delete-order' data-toggle='tooltip' data-placement='top' title='Delete' style='cursor: pointer; color: #ef1515;'></i>" +
                    "</button>" +

                    "</td>" +
                    "</tr>";

                tableBody.append(row);
            }
        });

        // Update DataTable
        dataTable.clear().rows.add(tableBody.find('tr')).draw();
        $('[data-toggle="tooltip"]').tooltip();
    }

    // Function to fetch data from the API
    function fetchDataFromAPI() {
        $.ajax({
            type: "GET",
            url: "https://localhost:7224/api/Vendors",
            contentType: "application/json",
            dataType: "json",
            success: function (data) {
                // Load data into the table
                loadDataIntoTable(data);
            },
            error: function (xhr, status, error) {
                console.error("Error fetching data:", status, error);
            }
        });
    }

    // Reload data into the table when needed (e.g., button click)
    $('#reloadButton').on('click', function () {
        fetchDataFromAPI();
    });

    // Handle dropdown change event
    $('#vendorDropdown').on('change', function () {
        // Load data into the table when the dropdown selection changes
        fetchDataFromAPI();
    });
});

$(document).on('click', '.btn-edit-order', function () {
    var vendorId = $(this).closest('tr').find('.vendor-id').text();

    // Make an AJAX request to get the detailed customer information
    $.ajax({
        type: "GET",
        url: "https://localhost:7224/api/Vendors/" + vendorId,
        contentType: "text/plain; charset=utf-8",
        dataType: "json",
        success: function (data) {
            console.log("Data received:", data);

            if (data.id !== 0) {
                var vendor = data;
                var editPageUrl = window.location.origin + "/Supplier/Edit/";
                var queryParams = new URLSearchParams();
                queryParams.set('vendorData', JSON.stringify(vendor));
                editPageUrl += "?" + queryParams.toString();
                window.location.href = editPageUrl;
            } else {
                console.log("No vendor data received.");
            }
        },
        failure: function (data) {
            alert(data.responseText);
        },
        error: function (data) {
            alert(data.responseText);
        }
    });
});

$(document).on('click', '.btn-delete-order', function () {
    var vendorId = $(this).closest('tr').find('.vendor-id').text();

    // AJAX request to delete the sales record
    $.ajax({
        type: 'DELETE',
        url: 'https://localhost:7224/api/Vendors/' + vendorId,
        success: function (response) {
            // Handle the success response
            console.log("Success:", response);
            location.reload();
        },
        error: function (xhr, status, error) {
            // Handle the error response
            console.log("Error:", xhr);
            console.log("Status:", status);
            console.log("Error thrown:", error);

            // Check if there's a responseJSON property containing the server's error details
            if (xhr.responseJSON) {
                console.log("Server Error Details:", xhr.responseJSON);
            }
        }
    });
});
