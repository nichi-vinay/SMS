
$(document).ready(function () {
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

        // Add new rows
        $.each(data, function (i, item) {
            var row = "<tr>" +
                "<td class='customer-id' style='display:none;'>" + item.id + "</td>" +
                "<td>" + item.name + "</td>" +
                "<td>" + item.mobile + "</td>" +
                "<td>" + item.enquiryTypeName + "</td>" +
                "<td>" + item.comments + "</td>" +
                "<td><button type='button' class='btn btn-primary btn-edit-customer'>Edit</button></td>" +
                "</tr>";

            tableBody.append(row);
        });

        // Update DataTable
        dataTable.clear().rows.add(tableBody.find('tr')).draw();
    }

    // Function to fetch data from the API
    function fetchDataFromAPI() {
        $.ajax({
            type: "GET",
            url: "https://localhost:7224/api/Customers",
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

    // Initial load of data into the table
    fetchDataFromAPI();

    // Reload data into the table when needed (e.g., button click)
    $('#reloadButton').on('click', function () {
        fetchDataFromAPI();
    });
});






$(document).on('click', '.btn-edit-customer', function () {
    var customerId = $(this).closest('tr').find('.customer-id').text();

    // Make an AJAX request to get the detailed customer information
    $.ajax({
        type: "GET",
        url: "https://localhost:7224/api/Customers/" + customerId,
        contentType: "text/plain; charset=utf-8",
        dataType: "json",
        success: function (data) {
            console.log("Data received:", data); // Log the data

            if (data.id !== 0) {
                var customer = data;

                // Construct the URL dynamically based on the current page's URL
                var editPageUrl = window.location.origin + "/Enquiry/Edit/";

                // Use URLSearchParams to append the customer data as a query parameter
                var queryParams = new URLSearchParams();
                queryParams.set('enquiryData', JSON.stringify(customer));

                // Append the query parameters to the edit page URL
                editPageUrl += "?" + queryParams.toString();

                // Redirect to the dynamically constructed edit customer page
                window.location.href = editPageUrl;
            } else {
                console.log("No customer data received.");
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

