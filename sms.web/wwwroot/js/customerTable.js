$(document).ready(function () {

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



    $.ajax({
        type: "GET",
        url: "https://localhost:7224/api/Customers",
        contentType: "text/plain; charset=utf-8",
        dataType: "json",
        success: function (data) {
            // Filter the data where IsCustomer is true
            var isCustomer = true;
            var filteredData = data.filter(function (customer) {
                return customer.isCustomer === isCustomer;
            });

            // Your existing code to populate the table with filteredData
            var tbody = $('#TableBody');
            tbody.empty(); // clear table body before adding rows
            $.each(filteredData, function (i, item) {
                var rows = "<tr>" +
                    "<td class='customer-id' style='display:none;'>" + item.id + "</td>" +
                    "<td class='customer-name'>" + item.name + "</td>" +
                    "<td class='customer-number'>" + item.mobile + "</td>" +
                    "<td class='customer-Date'>" + item.enquiryTypeName + "</td>" +
                    "<td class='customer-Date'>" + item.invoiceNumber + "</td>" +
                    "<td class='customer-comments'>" + item.comments + "</td>" +
                    "<td>" +
                    "<button type='button' class='btn btn-primary btn-edit-customer'>Edit</button>&nbsp;" +
                    "</td>" +
                    "</tr>";
                tbody.append(rows);
            });
            dataTable.clear().rows.add(tbody.find('tr')).draw();
       
        },
        failure: function (data) {
            alert(data.responseText);
        },
        error: function (data) {
            alert(data.responseText);
        }
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
  