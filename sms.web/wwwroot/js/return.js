$(document).ready(function () {


    // Function to initialize DataTable
   

        
        $("#example1").DataTable({
            "responsive": true,
            "lengthChange": false,
            "autoWidth": false,
            "buttons": ["copy", "csv", "excel", "pdf", "print", "colvis"]
        }).buttons().container().appendTo('#example1_wrapper .col-md-6:eq(0)');

    

    // GET Method to fetch data from server
    $.ajax({
        type: "GET",
        url: "https://localhost:7224/api/Orders",
        contentType: "text/plain; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var tbody = $('#TableBody');
            var dataTable = $('#example1');
            tbody.empty(); // clear table body before adding rows
            $.each(data, function (i, item) {
                var rows = "<tr>" +
                    "<td class='order-id' style='display:none;'>" + item.id + "</td>" +
                    "<td class='order-name'>" + item.orderName + "</td>" +
                    "<td class='order-number'>" + item.orderNumber + "</td>" +
                    "<td class='order-Date'>" + item.orderDate + "</td>" +
                    "<td class='order-orderTypeName'  >" + item.orderTypeName + "</td>" +
                    "<td class='order-vendorName'  >" + item.name + "</td>" +
                  
                    "<td class='order-shipmentDetails' style='display:none;'>" + item.shipmentDetails + "</td>" +
                    "<td class='order-orderTypeId' style='display:none;' >" + item.orderTypeId + "</td>" +
                    "<td class='order-vendorId'  style='display:none;'>" + item.vendorId + "</td>" +

                    "<td>" +
                    "<button type='button' class='btn btn-primary btn-edit-order'>Edit</button>&nbsp;" +
                    "<button type='button' class='btn btn-success btn-download-order'>Download</button>&nbsp;" +
                    "<button type='button' class='btn btn-danger btn-delete-order'>Delete</button>" +
                    "</td>" +
                    "</tr>";
                tbody.append(rows);


            }); //End of foreach Loop   

        

         
     
        },
        failure: function (data) {
            alert(data.responseText);
        },
        error: function (data) {
            alert(data.responseText);
        }
    });

  
}); 

//$(document).on('click', '.btn-edit-order', function () {
//    var orderId = $(this).closest('tr').find('.order-id').text();
//    var orderName = $(this).closest('tr').find('.order-name').text();
//    var orderNumber = $(this).closest('tr').find('.order-number').text();

//    var shipmentDetails = $(this).closest('tr').find('.order-shipmentDetails').text();
//    var orderTypeId = $(this).closest('tr').find('.order-orderTypeId').text();
//    var vendorId = $(this).closest('tr').find('.order-vendorId').text();
//    var name = $(this).closest('tr').find('.order-vendorName').text();
//    var orderTypeName = $(this).closest('tr').find('.order-orderTypeName').text();


//    // Populate modal form with data of selected order
//    $('#editOrderName').val(orderName);
//    $('#editOrderNumber').val(orderNumber);

//    $('#editOrderId').val(orderId);
//    $('#editShipmentDetails').val(shipmentDetails);
//    $('#editOrderTypeId').val(orderTypeId);
//    $('#editVendorId').val(vendorId);
//    $('#editName').val(name);
//    $('#editOrderTypeName').val(orderTypeName);



//    // Hide the Order Id field in the modal
//    $('#editOrderId').hide();
//    $('#editShipmentDetails').hide();
//    $('#editOrderTypeId').hide();

//    $('#editName').hide();
//    $('#editOrderTypeName').hide();





//    // Show the edit order modal
//    $('#editOrderModal').modal('show');
//});




//$(document).on('click', '.btn-edit-order', function () {
//    var orderId = $(this).closest('tr').find('.order-id').text();
//    var orderName = $(this).closest('tr').find('.order-name').text();
//    var orderNumber = $(this).closest('tr').find('.order-number').text();
//    var shipmentDetails = $(this).closest('tr').find('.order-shipmentDetails').text();
//    var orderTypeId = $(this).closest('tr').find('.order-orderTypeId').text();
//    var vendorId = $(this).closest('tr').find('.order-vendorId').text();
//    var name = $(this).closest('tr').find('.order-vendorName').text();
//    var orderTypeName = $(this).closest('tr').find('.order-orderTypeName').text();

//    // Encode the order data into a query parameter string
//    var orderDataString = btoa(JSON.stringify({
//        "orderId": orderId,
//        "orderName": orderName,
//        "orderNumber": orderNumber,
//        "shipmentDetails": shipmentDetails,
//        "orderTypeId": orderTypeId,
//        "vendorId": vendorId,
//        "name": name,
//        "orderTypeName": orderTypeName
//    }));

//    // Redirect to add-order.html with the order data as query parameters
//    window.location.href = "path/to/add-order.html?orderData=" + orderDataString;
//});




// Inside the success callback of your GET request for editing an order
$(document).on('click', '.btn-edit-order', function () {
    var orderId = $(this).closest('tr').find('.order-id').text();

    // Make an AJAX request to get the detailed order information
    $.ajax({
        type: "GET",
        url: "https://localhost:7224/api/Orders/" + orderId,
        contentType: "text/plain; charset=utf-8",
        dataType: "json",
        success: function (data) {
            console.log("Data received:", data); // Log the data

              if ( data.id !== 0) {
             
             
                  var order = data;
             

                  // Redirect to the add order page with the order data as a query parameter
                  window.location.href = "https://localhost:44359/ReturnDetails/EditReturn/?orderData=" + encodeURIComponent(JSON.stringify(order));
            } else {
                console.log("No order data received.");
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










// Save changes button click handler for orders
$('#saveOrderChangesBtn').click(function () {
   
    var orderName = $('#editOrderName').val();
    var orderNumber = $('#editOrderNumber').val();
    var orderId = $('#editOrderId').val();
    var shipmentDetails = $('#editShipmentDetails').val();
    var orderTypeId = $('#editOrderTypeId').val();
    var vendorId = $('#editVendorId').val();
    var name = $('#editName').val();
    var orderTypeName = $('#editOrderTypeName').val();
    var orderQuantity = $('#editOrderQuantity').val();
    var orderTypeName = $('#editOrderTypeName').val();

    // Make an AJAX request to update the order using the orderId
    $.ajax({
        type: "PUT",
        url: "https://localhost:7224/api/Orders/" + orderId,
        data: JSON.stringify({
            "id": orderId,
            "orderName": orderName,
            "orderNumber": orderNumber,
            "shipmentDetails": shipmentDetails,
            "orderTypeId": orderTypeId,
            "vendorId": vendorId,
            "name": name,
             "orderTypeName": orderTypeName,
            
            "orderTypeName": orderTypeName

        }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function () {
            // Handle success
            // Reload or update the order table as needed
            location.reload();
            toastr.success('Order has been updated successfully.');
        },
        error: function (error) {
            console.error('Error updating order:', error);
        }
    });
});





