var selectedOrderId; // Variable to store the selected order ID
var selectedVendorId; // Variable to store the selected vendor ID
var selectedOrderTypeId; // Variable to store the selected order type ID

var selectedItemID; // Declare the variable outside the document.ready function
var quantity;

// Flag to indicate whether it's an edit operation
var isEditOperation = false;

$(document).ready(function () {
    // Retrieve the encoded order data from the URL
    var encodedOrderData = getParameterByName('orderData');

    // Decode the URL-encoded JSON data
    var decodedOrderData = decodeURIComponent(encodedOrderData);

    // Parse the JSON data into a JavaScript object
    var order = JSON.parse(decodedOrderData);

    // Set the flag based on whether it's an edit operation
    isEditOperation = order !== null && order !== undefined;

    // GET Method to fetch data for supplier dropdown
    $.ajax({
        type: "GET",
        url: "https://localhost:7224/api/Vendors",
        contentType: "application/json",
        dataType: "json",
        success: function (data) {
            var supplierDropdown = $("#supplierDropdown");
            supplierDropdown.empty();

            supplierDropdown.append($("<option selected='selected'>").text("Select"));

            // Loop through each vendor
            $.each(data, function (i, vendor) {
                var vendorId = vendor.id;
                var vendorName = vendor.name;

                // Create option and append to the dropdown
                var option = $("<option>").text(vendorName).val(vendorId);

                // Set the selected attribute if it's an edit operation
                if (isEditOperation && vendorId === order.vendorId) {
                    option.attr('selected', 'selected');
                }

                // Set the 'vendor-id' data attribute
                option.data('vendor-id', vendorId);

                supplierDropdown.append(option);
            });

            supplierDropdown.select2({ width: '100%' });

            // Event handler for change in the supplier dropdown
            $("#supplierDropdown").on('change', function () {
                // Retrieve the selected order ID and vendor ID
                selectedOrderId = $(this).find(':selected').val();
                selectedVendorId = $(this).find(':selected').data('vendor-id');
            });

            // Function to fetch data for order type dropdown
            function populateOrderTypeDropdown() {
                $.ajax({
                    type: "GET",
                    url: "https://localhost:7224/api/OrderType",
                    contentType: "application/json",
                    dataType: "json",
                    success: function (data) {
                        var orderTypeDropdown = $("#orderTypeDropdown");
                        orderTypeDropdown.empty();

                        orderTypeDropdown.append($("<option selected='selected'>").text("Select"));

                        // Loop through each order type
                        $.each(data, function (i, orderType) {
                            var orderTypeId = orderType.id;
                            var orderTypeName = orderType.orderTypeName;

                            // Create option and append to the dropdown
                            var option = $("<option>").text(orderTypeName).val(orderTypeId);
                            option.data("order-type-id", orderTypeId);

                            // Set the selected attribute if it's an edit operation
                            if (isEditOperation && orderTypeId === order.orderTypeId) {
                                option.attr('selected', 'selected');
                            }

                            orderTypeDropdown.append(option);
                        });

                        orderTypeDropdown.select2({ width: '100%' });
                        $("#supplierDropdown").on('change', function () {
                            // Retrieve the selected order ID and vendor ID
                            selectedOrderId = $(this).find(':selected').val();
                            selectedVendorId = $(this).find(':selected').data('vendor-id');
                        });
                    },
                    error: function (error) {
                        console.error("Error fetching order types: ", error);
                    }
                });
            }

            // Call the function to populate the order type dropdown
            populateOrderTypeDropdown();

            $("#orderTypeDropdown").on('change', function () {
                // retrieve the selected order type id
                selectedOrderTypeId = $(this).find(':selected').data('order-type-id');
                console.log("selected order type id:", selectedOrderTypeId);
            });

        },



        error: function (error) {
            console.error("Error fetching vendors: ", error);
        }
    });

});







  $(document).ready(function () {
    var itemData = {}; // object to store associated ids
    var completedRequests = 0; // counter for completed AJAX requests

    // Fetch all item master data
    $.ajax({
        type: "GET",
        url: "https://localhost:7224/api/Item", // Update the URL to your item master API endpoint
        contentType: "application/json",
        dataType: "json",
        success: function (data) {
            // Populate itemData object with all items
            $.each(data, function (i, item) {
                var itemName = item.itemName;
                var itemId = item.id;
                var itemNumber = item.itemNumber;
                itemData[itemName] = { id: itemId, itemNumber: itemNumber };
            });

            // Initialize item dropdowns for the first row
            initializeItemDropdowns($("#addRow tbody tr:first"), 'select', 0);

            // Fetch item details for each order item
            order.orderItems.forEach(function (orderItem, index) {
                fetchItemMasterData(orderItem, index === 0); // Pass true for the first row
            });
        },
        failure: function (data) {
            alert(data.responseText);
        },
        error: function (data) {
            alert(data.responseText);
        }
    });


      // function to handle change event for item name dropdown
      function handleItemNameChange() {
          var itemNameDropdown = $(this);
          var selectedItemId = itemNameDropdown.val();

          // set the selected option in the item number dropdown
          itemNameDropdown.closest('tr').find(".item-number").val(selectedItemId).trigger('change');
      }
    // Function to initialize item dropdowns
      // Function to initialize item dropdowns
      function initializeItemDropdowns(row, itemId, quantity) {
          // Clear existing dropdown options
          row.find('.item-name, .item-number').find('option').remove();

          // Add the default "select" option
          var defaultOption = $("<option>").val('select').text('Select');
          row.find('.item-name, .item-number').append(defaultOption);

          // Add options for each item in the data
          $.each(itemData, function (itemName, item) {
              var currentItemId = item.id;
              var itemNumber = item.itemNumber;

              // Add options to item name dropdown
              var itemNameOption = $("<option>").val(currentItemId).text(itemName);
              row.find(".item-name").append(itemNameOption);

              // Add options to item number dropdown
              var itemNumberOption = $("<option>").val(currentItemId).text(itemNumber);
              row.find(".item-number").append(itemNumberOption);
          });

          // Initialize Select2 for the row
          row.find('.item-name, .item-number').select2({ width: '100%' });

          // Set default item name and item number based on the provided itemId
          row.find(".item-name").val(itemId).trigger('change');
          row.find(".item-number").val(itemId).trigger('change');
          row.find(".quantity").val(quantity);

          $(document).on('change', '.item-name', handleItemNameChange);

      }



    // Function to fetch item master data using AJAX
    function fetchItemMasterData(orderItem, isFirstRow) {
        var itemID = orderItem.itemID;
        var quantity = orderItem.quantity;

        $.ajax({
            type: "GET",
            url: "https://localhost:7224/api/Item/" + itemID,
            contentType: "application/json",
            dataType: "json",
            success: function (itemDetails) {
                var itemId = itemDetails.id;
                var itemName = itemDetails.itemName;
                var itemNumber = itemDetails.itemNumber;

                // Add the fetched item details to itemData
                itemData[itemName] = { id: itemId, itemNumber: itemNumber };

                // Increment the completedRequests counter
                completedRequests++;

                // If all requests are completed, add a new row
                if (completedRequests === order.orderItems.length || isFirstRow) {
                    addNewRow(itemId, quantity);
                }

            
            },
            failure: function (data) {
                alert(data.responseText);
            },
            error: function (data) {
                alert(data.responseText);
            }
        });
    }

    // Function to add a new row to the table
    function addNewRow(itemId, quantity) {
        var newRow = `
        <tr>
            <td>
                <select class="form-control select2 item-name" style="width: 100%;">
                    <option selected="selected">Select</option>
                    <!-- Options will be dynamically added here -->
                </select>
            </td>
            <td>
                <select class="form-control select2 item-number" style="width: 100%;">
                    <option selected="selected">Select</option>
                    <!-- Options will be dynamically added here -->
                </select>
            </td>
            <td> <input type="number" class="form-control quantity" name="quantity[]"></td>
        </tr>
    `;

        // Append the new row
        $("#addRow tbody").append(newRow);

        // Initialize item dropdowns for the new row
        var newRowElement = $("#addRow tbody tr:last");
        initializeItemDropdowns(newRowElement, itemId, quantity);
    }

    // function to remove the last row when the "Total Price" field is empty or Backspace is pressed
    function removeLastRow() {
        var rowCount = $("#addRow tbody tr").length;
        if (rowCount > 1) {
            var lastRow = $("#addRow tbody tr:last");
            var totalPriceInput = lastRow.find("input[name='quantity[]']");
            if (totalPriceInput.val() === "") {
                if (confirm("Do you want to delete this row?")) {
                    lastRow.remove();
                }
            }
        }
    }

    // attach the keydown event handler to the "Total Price" column
    $("#addRow tbody").on("keydown", "input[name='quantity[]']", function (event) {
        if (event.key === "Enter") {
            event.preventDefault(); // prevent the default behavior of Enter key
            addNewRow();
        } else if (event.key === "Backspace") {
            var totalPriceInput = $(this);
            if (totalPriceInput.val() === "") {
                removeLastRow();
            }
        }
    });

});












// Retrieve the encoded order data from the URL
var encodedOrderData = getParameterByName('orderData');

// Decode the URL-encoded JSON data
var decodedOrderData = decodeURIComponent(encodedOrderData);

// Parse the JSON data into a JavaScript object
var order = JSON.parse(decodedOrderData);

// Now you can use the 'order' object to populate your form fields or perform other operations
order.orderItems.forEach(function (item) { 
    console.log(item.itemID);
});

// Function to extract query parameters from the URL
function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, '\\$&');
    var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, ' '));
}


//// Populate supplier dropdown
var vendorNameFromQueryString = getParameterByName('vendorName');
$('#supplierDropdown').append($("<option selected='selected'>").text(vendorNameFromQueryString));
$('input[name="orderName"]').val(order.orderName);
$('input[name="orderNumber"]').val(order.orderNumber);
$('#orderTypeDropdown').val(order.orderTypeId); // Assuming orderTypeId corresponds to the order type dropdown
$('input[name="orderDate"]').val(order.orderDate);
$('input[name="shipmentDetails"]').val(order.shipmentDetails);


$("#btnSubmit").on("click", function () {
    submitOrderForm();
});
function submitOrderForm() {

    // Get the selected values from the dropdowns and input fields
    var vendorId = selectedVendorId;
    var orderId = selectedOrderId;
    var orderTypeId = selectedOrderTypeId;


    var orderName = $("input[name='orderName']").val();
    var orderNumber = $("input[name='orderNumber']").val();
    var orderDate = new Date($("input[name='orderDate']").val()).toISOString(); // Convert to ISO string
    var shipmentDetails = $("input[name='shipmentDetails']").val();

    var orderTypeName = $("#orderTypeDropdown option:selected").text(); // Get the selected order type name
    var name = $("#supplierDropdown option:selected").text(); // Get the selected name


    var orderItems = [];

    $("#addRow tbody tr").each(function () {
        var currentItem = {
            ItemId: $(this).find(".item-name").val(),
            Quantity: $(this).find("input[name='quantity[]']").val()
        };

        orderItems.push(currentItem);
    });



    // Use the jQuery AJAX function to send a POST request
    $.ajax({
        type: "PUT",
        url: "https://localhost:7224/api/Orders/" + orderId, // Update the URL to your API endpoint
        contentType: "application/json",
        data: JSON.stringify({

            vendorId: vendorId,
            orderName: orderName,
            orderNumber: orderNumber,
            orderTypeId: orderTypeId,
            orderDate: orderDate,
            shipmentDetails: shipmentDetails,
            orderTypeName: orderTypeName,
            name: name,
            orderItems: orderItems

        }),
        success: function (response) {

            location.reload();



        },
        error: function (error) {
            // Handle the error response from the server
            alert("Error submitting order: " + error.responseText);
        }
    });
}