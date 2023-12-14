
var selectedOrderId; // variable to store the selected order id
var selectedVendorId; // variable to store the selected vendor id
var selectedOrderTypeId; // variable to store the selected order type id

var selecteditemid; // declare the variable outside the document.ready function
var quantity;

$(document).ready(function () {
    // get method to fetch data for supplier dropdown
    $.ajax({
        type: "get",
        url: "https://localhost:44387/api/vendors",
        contenttype: "application/json",
        datatype: "json",
        success: function (data) {
            var supplierdropdown = $("#supplierDropdown");
            supplierdropdown.empty();

            supplierdropdown.append($("<option selected='selected'>").text("select"));

            // loop through each vendor
            $.each(data, function (i, vendor) {
                var vendorid = vendor.id;
                var vendorname = vendor.name;

                // create option and append to the dropdown
                var option = $("<option>").text(vendorname).val(vendorid);
                option.data("vendor-id", vendorid);

                supplierdropdown.append(option);
            });

            supplierdropdown.select2({ width: '100%' });
        },
        error: function (error) {
            console.error("error fetching vendors: ", error);
        }
    });

    // event handler for change in the supplier dropdown
    $("#supplierDropdown").on('change', function () {
        // retrieve the selected order id and vendor id
        selectedOrderId = $(this).find(':selected').val();
        selectedVendorId = $(this).find(':selected').data('vendor-id');
    });

    function populateOrderTypeDropdown() {
        $.ajax({
            type: "GET",
            url: "https://localhost:44387/api/OrderType",
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
                 

                    orderTypeDropdown.append(option);
                });

                orderTypeDropdown.select2({ width: '100%' });
            },
            error: function (error) {
                console.error("Error fetching order types: ", error);
            }
        });
    }

    // call the function to populate the order type dropdown
    populateOrderTypeDropdown();

    // event handler for change in the order type dropdown
    $("#orderTypeDropdown").on('change', function () {
        // retrieve the selected order type id
        selectedOrderTypeId = $(this).find(':selected').data('order-type-id');
        console.log("selected order type id:", selectedOrderTypeId);
    });
});








$(document).ready(function () {
    var itemData = {}; // object to store associated ids

    // function to initialize item dropdowns
    function initializeItemDropdowns(row) {
        // clear existing dropdown options
        row.find('.item-name, .item-number').find('option').remove();

        // add the default "select" option
        var defaultOption = $("<option>").val('select').text('Select');
        row.find('.item-name, .item-number').append(defaultOption);

        // add options for each item in the data
        $.each(itemData, function (itemName, item) {
            var itemId = item.id;
            var itemNumber = item.itemNumber;

            // add options to item name dropdown
            var itemNameOption = $("<option>").val(itemId).text(itemName);
            row.find(".item-name").append(itemNameOption);

            // add options to item number dropdown
            var itemNumberOption = $("<option>").val(itemId).text(itemNumber);
            row.find(".item-number").append(itemNumberOption);
        });

        // initialize Select2 for the row
        row.find('.item-name, .item-number').select2({ width: '100%' });

        // set default item name and item number to "select"
        row.find(".item-name").val('select').trigger('change');
        row.find(".item-number").val('select').trigger('change');
    }

    // function to handle change event for item name dropdown
    function handleItemNameChange() {
        var itemNameDropdown = $(this);
        selectedItemId = itemNameDropdown.val();
        var selectedText = itemNameDropdown.find(':selected').text();

        // set the selected option in the item number dropdown
        itemNameDropdown.closest('tr').find(".item-number").val(selectedItemId).trigger('change');

        console.log("Selected item type id:", selectedItemId);
    }

    // function to fetch item master data using AJAX
    function fetchItemMasterData(row) {
        return $.ajax({
            type: "GET",
            url: "https://localhost:44387/api/item", // update the URL to your item master API endpoint
            contentType: "application/json",
            dataType: "json",
            success: function (data) {
                // populate itemData object
                $.each(data, function (i, item) {
                    var itemName = item.itemName;
                    var itemId = item.id;
                    var itemNumber = item.itemNumber;

                    itemData[itemName] = { id: itemId, itemNumber: itemNumber };
                });

                // initialize item dropdowns for the new row
                initializeItemDropdowns(row);

                // handle change event for item name dropdown using event delegation
                row.on('change', '.item-name', handleItemNameChange);
            },
            failure: function (data) {
                alert(data.responseText);
            },
            error: function (data) {
                alert(data.responseText);
            }
        });
    }

    // function to add a new row to the table
    function addNewRow() {
        // check if all input fields in the current row are filled
        var currentRow = $("#addRow tbody tr:last");
        var inputs = currentRow.find("input");
        var allFieldsFilled = true;

        inputs.each(function () {
            if ($(this).val() === "") {
                allFieldsFilled = false;
                return false; // exit the loop early
            }
        });

        if (allFieldsFilled) {
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

            // append the new row
            $("#addRow tbody").append(newRow);

            // initialize item dropdowns and fetch data for the new row
            var newRowElement = $("#addRow tbody tr:last");
            fetchItemMasterData(newRowElement);
        } else {
            alert("Please fill in all the columns in the current row.");
        }
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

    // initial fetch and initialization for the first row
    fetchItemMasterData($("#addRow tbody tr:last"));
});







// event listener for form submission
// Event listener for form submission
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
        type: "POST",
        url: "https://localhost:44387/api/Orders", // Update the URL to your API endpoint
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





// retrieve the encoded order data from the url
var encodedorderdata = getparameterbyname('orderdata');

// decode the url-encoded json data
var decodedorderdata = decodeuricomponent(encodedorderdata);

// parse the json data into a javascript object
var order = json.parse(decodedorderdata);

// now you can use the 'order' object to populate your form fields or perform other operations
console.log(order);

// function to extract query parameters from the url
function getparameterbyname(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, '\\$&');
    var regex = new regexp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeuricomponent(results[2].replace(/\+/g, ' '));
}














//var selectedOrderId; // Variable to store the selected order ID
//var selectedVendorId; // Variable to store the selected vendor ID
//var selectedOrderTypeId; // Variable to store the selected order type ID
//var selectedItemID; // Declare the variable outside the document.ready function
//var quantity;

//// Flag to indicate whether it's an edit operation
//var isEditOperation = false;

//$(document).ready(function () {
//    // Retrieve the encoded order data from the URL
//    var encodedOrderData = getParameterByName('orderData');

//    // Decode the URL-encoded JSON data
//    var decodedOrderData = decodeURIComponent(encodedOrderData);

//    // Parse the JSON data into a JavaScript object
//    var order = JSON.parse(decodedOrderData);

//    // Set the flag based on whether it's an edit operation
//    isEditOperation = order !== null && order !== undefined;

//    // GET Method to fetch data for supplier dropdown
//    $.ajax({
//        type: "GET",
//        url: "https://localhost:7224/api/Vendors",
//        contentType: "application/json",
//        dataType: "json",
//        success: function (data) {
//            var supplierDropdown = $("#supplierDropdown");
//            supplierDropdown.empty();

//            supplierDropdown.append($("<option selected='selected'>").text("Select"));

//            // Loop through each vendor
//            $.each(data, function (i, vendor) {
//                var vendorId = vendor.id;
//                var vendorName = vendor.name;

//                // Create option and append to the dropdown
//                var option = $("<option>").text(vendorName).val(vendorId);

//                // Set the selected attribute if it's an edit operation
//                if (isEditOperation && vendorId === order.vendorId) {
//                    option.attr('selected', 'selected');
//                }

//                supplierDropdown.append(option);
//            });

//            supplierDropdown.select2({ width: '100%' });
//        },
//        error: function (error) {
//            console.error("Error fetching vendors: ", error);
//        }
//    });

//    // Event handler for change in the supplier dropdown
//    $("#supplierDropdown").on('change', function () {
//        // Retrieve the selected order ID and vendor ID
//        selectedOrderId = $(this).find(':selected').val();
//        selectedVendorId = $(this).find(':selected').data('vendor-id');
//    });

//    // Function to fetch data for order type dropdown
//    function populateOrderTypeDropdown() {
//        $.ajax({
//            type: "GET",
//            url: "https://localhost:7224/api/OrderType",
//            contentType: "application/json",
//            dataType: "json",
//            success: function (data) {
//                var orderTypeDropdown = $("#orderTypeDropdown");
//                orderTypeDropdown.empty();

//                orderTypeDropdown.append($("<option selected='selected'>").text("Select"));

//                // Loop through each order type
//                $.each(data, function (i, orderType) {
//                    var orderTypeId = orderType.id;
//                    var orderTypeName = orderType.orderTypeName;

//                    // Create option and append to the dropdown
//                    var option = $("<option>").text(orderTypeName).val(orderTypeId);

//                    // Set the selected attribute if it's an edit operation
//                    if (isEditOperation && orderTypeId === order.orderTypeId) {
//                        option.attr('selected', 'selected');
//                    }

//                    orderTypeDropdown.append(option);
//                });

//                orderTypeDropdown.select2({ width: '100%' });
//            },
//            error: function (error) {
//                console.error("Error fetching order types: ", error);
//            }
//        });
//    }

//    // Call the function to populate the order type dropdown
//    populateOrderTypeDropdown();

//    // Event handler for change in the order type dropdown
//    $("#orderTypeDropdown").on('change', function () {
//        // Retrieve the selected order type ID
//        selectedOrderTypeId = $(this).find(':selected').data('order-type-id');
//        console.log("Selected Order Type ID:", selectedOrderTypeId);
//    });

//    // Function to initialize item dropdowns
//    function initializeItemDropdowns(row) {
//        // Clear existing dropdown options
//        row.find('.item-name, .item-number').find('option').remove();

//        // Add the default "Select" option
//        var defaultOption = $("<option>").val('Select').text('Select');
//        row.find('.item-name, .item-number').append(defaultOption);

//        // Add options for each item in the data
//        $.each(itemData, function (itemName, item) {
//            var itemId = item.id;
//            var itemNumber = item.itemNumber;

//            // Add options to item name dropdown
//            var itemNameOption = $("<option>").val(itemId).text(itemName);
//            row.find(".item-name").append(itemNameOption);

//            // Add options to item number dropdown
//            var itemNumberOption = $("<option>").val(itemId).text(itemNumber);
//            row.find(".item-number").append(itemNumberOption);
//        });

//        // Initialize Select2 for the row
//        row.find('.item-name, .item-number').select2({ height: '100%' });

//        // Set default item name and item number to "Select"
//        row.find(".item-name").val('Select').trigger('change');
//        row.find(".item-number").val('Select').trigger('change');
//    }

//    // Call initializeItemDropdowns for the first row
//    initializeItemDropdowns($("#addRow tbody tr:first"));

//    // Function to handle change event for item name dropdown
//    function handleItemNameChange() {
//        var itemNameDropdown = $(this);
//        selectedItemID = itemNameDropdown.val();
//        var selectedText = itemNameDropdown.find(':selected').text();

//        // Set the selected option in the item number dropdown
//        itemNameDropdown.closest('tr').find(".item-number").val(selectedItemID).trigger('change');

//        console.log("Selected item Type ID:", selectedItemID);
//    }

//    // Function to fetch item master data using AJAX
//    function fetchItemMasterData(row) {
//        return $.ajax({
//            type: "GET",
//            url: "https://localhost:7224/api/Item", // Update the URL to your Item Master API endpoint
//            contentType: "application/json",
//            dataType: "json",
//            success: function (data) {
//                // Populate itemData object
//                $.each(data, function (i, item) {
//                    var itemName = item.itemName;
//                    var itemId = item.id;
//                    var itemNumber = item.itemNumber;

//                    itemData[itemName] = { id: itemId, itemNumber: itemNumber };
//                });

//                // Initialize item dropdowns for the new row
//                initializeItemDropdowns(row);

//                // Handle change event for item name dropdown using event delegation
//                row.on('change', '.item-name', handleItemNameChange);
//            },
//            failure: function (data) {
//                alert(data.responseText);
//            },
//            error: function (data) {
//                alert(data.responseText);
//            }
//        });
//    }

//    // Function to add a new row to the table
//    function addNewRow() {
//        // Check if all input fields in the current row are filled
//        var currentRow = $("#addRow tbody tr:last");
//        var inputs = currentRow.find("input");
//        var allFieldsFilled = true;

//        inputs.each(function () {
//            if ($(this).val() === "") {
//                allFieldsFilled = false;
//                return false; // Exit the loop early
//            }
//        });

//        if (allFieldsFilled) {
//            var newRow = `
//                <tr>
//                    <td>
//                        <select class="form-control select2 item-name" style="width: 100%;">
//                            <option selected="selected">Select</option>
//                            <!-- Options will be dynamically added here -->
//                        </select>
//                    </td>
//                    <td>
//                        <select class="form-control select2 item-number" style="width: 100%;">
//                            <option selected="selected">Select</option>
//                            <!-- Options will be dynamically added here -->
//                        </select>
//                    </td>
//                     <td> <input type="number" class="form-control quantity" name="quantity[]"></td>
//                </tr>
//            `;

//            // Append the new row
//            $("#addRow tbody").append(newRow);

//            // Initialize item dropdowns and fetch data for the new row
//            var newRowElement = $("#addRow tbody tr:last");
//            fetchItemMasterData(newRowElement);
//        } else {
//            alert("Please fill in all the columns in the current row.");
//        }
//    }

//    // Function to remove the last row when the "Total Price" field is empty or Backspace is pressed
//    function removeLastRow() {
//        var rowCount = $("#addRow tbody tr").length;
//        if (rowCount > 1) {
//            var lastRow = $("#addRow tbody tr:last");
//            var totalPriceInput = lastRow.find("input[name='quantity[]']");
//            if (totalPriceInput.val() === "") {
//                if (confirm("Do you want to delete this row?")) {
//                    lastRow.remove();
//                }
//            }
//        }
//    }

//    // Attach the keydown event handler to the "Total Price" column
//    $("#addRow tbody").on("keydown", "input[name='quantity[]']", function (event) {
//        if (event.key === "Enter") {
//            event.preventDefault(); // Prevent the default behavior of Enter key
//            addNewRow();
//        } else if (event.key === "Backspace") {
//            var totalPriceInput = $(this);
//            if (totalPriceInput.val() === "") {
//                removeLastRow();
//            }
//        }
//    });

//    // Initial fetch and initialization for the first row
//    fetchItemMasterData($("#addRow tbody tr:last"));
//});

//// Event listener for form submission
//$("#btnSubmit").on("click", function () {
//    submitOrderForm();
//});

//function submitOrderForm() {
//    // Get the selected values from the dropdowns and input fields
//    var vendorId = selectedVendorId;
//    var orderId = selectedOrderId;
//    var orderTypeId = selectedOrderTypeId;

//    var orderName = $("input[name='orderName']").val();
//    var orderNumber = $("input[name='orderNumber']").val();
//    var orderDate = new Date($("input[name='orderDate']").val()).toISOString(); // Convert to ISO string
//    var shipmentDetails = $("input[name='shipmentDetails']").val();

//    var orderTypeName = $("#orderTypeDropdown option:selected").text(); // Get the selected order type name
//    var name = $("#supplierDropdown option:selected").text(); // Get the selected name

//    var orderItems = [];

//    $("#addRow tbody tr").each(function () {
//        var currentItem = {
//            ItemId: $(this).find(".item-name").val(),
//            Quantity: $(this).find("input[name='quantity[]']").val()
//        };

//        orderItems.push(currentItem);
//    });

//    // Use the jQuery AJAX function to send a POST request
//    $.ajax({
//        type: "POST",
//        url: "https://localhost:7224/api/Orders", // Update the URL to your API endpoint
//        contentType: "application/json",
//        data: JSON.stringify({
//            vendorId: vendorId,
//            orderName: orderName,
//            orderNumber: orderNumber,
//            orderTypeId: orderTypeId,
//            orderDate: orderDate,
//            shipmentDetails: shipmentDetails,
//            orderTypeName: orderTypeName,
//            name: name,
//            orderItems: orderItems
//        }),
//        success: function (response) {
//            location.reload();
//        },
//        error: function (error) {
//            // Handle the error response from the server
//            alert("Error submitting order: " + error.responseText);
//        }
//    });
//}

//// Function to extract query parameters from the URL
//function getParameterByName(name, url) {
//    if (!url) url = window.location.href;
//    name = name.replace(/[\[\]]/g, '\\$&');
//    var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
//        results = regex.exec(url);
//    if (!results) return null;
//    if (!results[2]) return '';
//    return decodeURIComponent(results[2].replace(/\+/g, ' '));
//}


















