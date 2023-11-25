$(function () {
    
    $(document).ready(function () {
        // Initialize Select2 for the initial row
        $('.item-name, .item-number').select2();
    });
    $("#example1").DataTable({
        "responsive": true,
        "lengthChange": false,
        "autoWidth": false,
        "buttons": ["copy", "csv", "excel", "pdf", "print", "colvis"]
    }).buttons().container().appendTo('#example1_wrapper .col-md-6:eq(0)');
    $('#example2').DataTable({
        "paging": true,
        "lengthChange": false,
        "searching": false,
        "ordering": true,
        "info": true,
        "autoWidth": false,
        "responsive": true,
    });
});
function goToIndexPage() {
    window.location.href = "/Purchase"; // Replace with the actual URL of your index page
}
$(function () {
    bsCustomFileInput.init();
});
$(function () {
    // Function to calculate and update the total sum
    function updateTotalSum() {
        var totalSum = 0;
        $("input[name='total_price[]']").each(function () {
            var value = parseFloat($(this).val()) || 0;
            totalSum += value;
        });

        $("#totalSum").val(totalSum.toFixed(2)); // Update the total sum in the input field
    }

    // Attach the input event handler to the "Total Price" column
    $("#addRow tbody").on("input", "input[name='total_price[]']", function () {
        updateTotalSum(); // Update the total sum when the "Total Price" value changes
    });
    function updatePayableAmount() {
        var totalTax = parseFloat($("#TotalTax").val()) || 0;
        var totalSum = parseFloat($("#totalSum").val()) || 0;

        // Update Payable Amount
        var payableAmount = totalSum + totalTax;
        $("#totalSum").val(payableAmount.toFixed(2));

        // Trigger the updateTotalPayment function
        updateTotalPayment();
    }
    $("#TotalTax").on("input", updatePayableAmount);
    function addNewRow() {
        // Check if all input fields in the current row are filled
        var currentRow = $("#addRow tbody tr:last");
        var inputs = currentRow.find("input");
        var allFieldsFilled = true;

        inputs.each(function () {
            if ($(this).val() === "") {
                allFieldsFilled = false;
                return false; // Exit the loop early
            }
        });

        if (allFieldsFilled) {
            var newRow = `
        <tr>
            <td>
                <select class="form-control select2 item-name" style="width: 100%;">
                    <option value="">Select</option>
                </select>
            </td>
            <td>
                <select class="form-control select2 item-number" style="width: 100%;">
                    <option value="">Select</option>
                </select>
            </td>
        
            <td><input type="number" class="form-control" name="barcode[]"></td>
            <td><input type="number" class="form-control" name="quantity[]"></td>
            <td><input type="number" class="form-control" name="mrp[]"></td>          
            <td><input type="number" class="form-control" name="discount_percentage[]"></td>
            <td><input type="number" class="form-control" name="total_price[]" onkeydown="addRowOnEnter(event)"></td>
        </tr>
    `;
            $("#addRow tbody").append(newRow);
            populateItemDropdowns($(newRow));
            updateTotalSum();

        } else {
            alert("Please fill in all the columns in the current row.");
        }
    }

    // Function to remove the last row when the "Total Price" field is empty or Backspace is pressed
    function removeLastRow() {
        var rowCount = $("#addRow tbody tr").length;
        if (rowCount > 1) {
            var lastRow = $("#addRow tbody tr:last");
            var totalPriceInput = lastRow.find("input[name='total_price[]']");
            if (totalPriceInput.val() === "") {
                if (confirm("Do you want to delete this row?")) {
                    lastRow.remove();
                }
            }
        }
    }

    // Attach the keydown event handler to the "Total Price" column
    $("#addRow tbody").on("keydown", "input[name='total_price[]']", function (event) {
        if (event.key === "Enter") {
            event.preventDefault(); // Prevent the default behavior of Enter key
            addNewRow();
        } else if (event.key === "Backspace") {
            var totalPriceInput = $(this);
            if (totalPriceInput.val() === "") {
                removeLastRow();
            }
        }
    });
});


// Attach the keydown event handler to the "Total Price" column
$("#addRow tbody").on("keydown", "input[name='total_price[]']", function (event) {
    if (event.key === "Enter") {
        event.preventDefault(); // Prevent the default behavior of Enter key
        addNewRow();
    } else if (event.key === "Backspace") {
        var totalPriceInput = $(this);
        if (totalPriceInput.val() === "") {
            removeLastRow();
        }
    }
});

function handlePaymentSubmit() {
    // Calculate the total sum
    var totalSum = parseFloat($("#totalSum").val()) || 0;
    // Get values from payment modal fields (check, cash, upi, cards)
    var checkAmount = parseFloat($("#paymentModal input[name='check']").val()) || 0;
    var cashAmount = parseFloat($("#paymentModal input[name='cash']").val()) || 0;
    var upiAmount = parseFloat($("#paymentModal input[name='upi']").val()) || 0;
    var cardsAmount = parseFloat($("#paymentModal input[name='cards']").val()) || 0;
    // Calculate the balance
    var totalPaidAmount = (checkAmount + cashAmount + upiAmount + cardsAmount);
    $("#totalPaid").val(totalPaidAmount.toFixed(2));
    // Check if the total paid amount is equal to the total sum
    if (totalPaidAmount === totalSum) {
        // Enable the submit button
        $("#submitButton").prop("disabled", false);
    } else {
        // Disable the submit button
        $("#submitButton").prop("disabled", true);
    }
    // Dismiss the payment modal
    $("#paymentModal").modal("hide");
    // Remove the modal-open class and modal-backdrop
    $('body').removeClass('modal-open');
    $('.modal-backdrop').remove();
}
// Add an event listener to the Submit button in the payment modal
$("#paymentModalSubmitBtn").on("click", handlePaymentSubmit);
// Function to populate item dropdowns
function populateItemDropdowns(row) {
    $.ajax({
        url: 'https://localhost:7224/api/Item',
        type: 'GET',
        success: function (data) {
            // Populate dropdowns with fetched data
            $.each(data, function (index, item) {
                $('.item-name').append('<option value="' + item.id + '">' + item.itemName + '</option>');
                $('.item-number').append('<option value="' + item.id + '">' + item.itemNumber + '</option>');
            });
            $('.item-name').on('input', function () {
                var selectedItemId = $(this).val();
                var itemNumberDropdown = $(this).closest('tr').find('.item-number');
                // Add default "Select" option
                itemNumberDropdown.append('<option value="" selected="selected">Select</option>');
                // Populate item number dropdown based on the selected item name
                $.each(data, function (index, item) {
                    if (item.id == selectedItemId) {
                        itemNumberDropdown.append('<option value="' + item.id + '">' + item.itemNumber + '</option>');
                    }
                });
                         itemNumberDropdown.val(selectedItemId).change();
            });
        },
        error: function (error) {
            console.error('Error fetching item data:', error);
        }
    });
}
// Call the function to populate item dropdowns when the page loads
$(document).ready(function () {
    populateItemDropdowns();
});
// Add this code inside the $(function() {...}) block to ensure it runs after the DOM is ready
$(function () {
    // Function to fetch and populate options for the Supplier dropdown
    function populateSupplierDropdown() {
        $.ajax({
            url: 'https://localhost:7224/api/Vendors',
            type: 'GET',
            success: function (data) {
                // Clear existing options
                $('#supplierDropdown').empty();

                // Add default "Select" option
                $('#supplierDropdown').append('<option value="" selected="selected">Select</option>');

                // Populate dropdown with fetched vendor names and IDs
                $.each(data, function (index, vendor) {
                    $('#supplierDropdown').append('<option value="' + vendor.id + '">' + vendor.name + '</option>');
                });
            },
            error: function (error) {
                console.error('Error fetching vendors:', error);
            }
        });
    }

    // Call the function to populate the Supplier dropdown when the page loads
    populateSupplierDropdown();
});

function updatePrice(row) {
    var quantity = parseFloat(row.find("input[name='quantity[]']").val()) || 0;
    var mrp = parseFloat(row.find("input[name='mrp[]']").val()) || 0;
    var discountPercentage = parseFloat(row.find("input[name='discount_percentage[]']").val()) || 0;
    var prices = parseFloat(row.find("input[name ='Price[]']").val()) || 0;

    var MRPS = mrp * quantity;
    // Calculate the discount amount
    var discountAmount = MRPS * (discountPercentage / 100);

    // Calculate the discounted price
    var discountedPrice = MRPS - discountAmount;

    // Calculate the total price
    var totalPrice = quantity * discountedPrice;

    row.find("input[name='total_price[]']").val(discountedPrice.toFixed(2));
}

// Attach the input event handler to the quantity, MRP, and discount percentage columns
$("#addRow tbody").on("input", "input[name='quantity[]'], input[name='mrp[]'], input[name='discount_percentage[]']", function () {
    var currentRow = $(this).closest("tr");
    updatePrice(currentRow);
});
// Call the updatePrice function initially to set the initial price and grand total price
updatePrice($("#addRow tbody tr"));
var totalPriceArray = [];
// Function to serialize the form data
function openPaymentModal() {
    // Show the payment modal
    $("#paymentModal").modal("show");

    // Assuming there's a button in the modal to confirm payment
    $('#paymentModalSubmitBtn').on('click', function () {
        // Retrieve payment values from modal fields
        var check = parseFloat($("#paymentModal input[name='check']").val()) || 0;
        var cash = parseFloat($("#paymentModal input[name='cash']").val()) || 0;
        var upiOnline = parseFloat($("#paymentModal input[name='upi']").val()) || 0;
        var cards = parseFloat($("#paymentModal input[name='cards']").val()) || 0;

        // Calculate the total payment
        var totalPayment = check + cash + upiOnline + cards;

        // Update the Total Paid field
        $("#totalPaid").val(totalPayment.toFixed(2));

        // Close the modal (hide modal logic)
        $("#paymentModal").modal("hide");

        // After closing the modal, trigger the updateTotalPayment function
        updateTotalPayment();
    });
}
function updateTotalPayment() {
    var totalPaid = parseFloat($("#totalPaid").val()) || 0;
    var totalSum = parseFloat($("#totalSum").val()) || 0;

    // Check if total payment is equal to payable amount
    if (totalPaid === totalSum) {
        // Enable the submit button
        $("#submitBtn").prop("disabled", false);
    } else {
        // Disable the submit button
        $("#submitBtn").prop("disabled", true);
    }
}

function serializeForm() {
    var formData = {
        vendorId: $("#supplierDropdown").val(),
        supplierName: $("#supplierDropdown option:selected").text(),
        invoiceNumber: $("#invoiceNumber").val(),
        invoiceDate: new Date($("#InvoiceDate").val()).toISOString().split("T")[0],
        shipmentDetails: $("#ShipmentDetails").val(),
        cheque: $("#Check").val(),
        cash: $("#Cash").val(),
        online: $("#UPIOnline").val(),
        cards: $("#Cards").val(),
        totalDiscount: $("#TotalDiscount").val(),
        totalTax: $("#TotalTax").val(),
        totalAmount: $("#totalSum").val(),
        totalPaid: $("#totalPaid").val(),
        purchaseItems: []
    };

    // Variable to store the sum of mrp values
   
    totalPriceArray = [];
    // Iterate over each row in the table
    $("#addRow tbody tr").each(function () {
        var item = {
            itemID: $(this).find(".item-name").val(),
            itemName: $(this).find(".item-name").val(),
            itemNumber: $(this).find(".item-number").val(),
            barcode: $(this).find("[name='barcode[]']").val(),
            quantity: $(this).find("[name='quantity[]']").val(),
            mrp: $(this).find("[name='mrp[]']").val(),
            discountPercentage: $(this).find("[name='discount_percentage[]']").val(),
            totalPrice: $(this).find("[name='total_price[]']").val()
        };
        // Add the item to the purchaseItems array
        formData.purchaseItems.push(item);
        // Calculate the sum of mrp values
        totalPriceArray.push(parseFloat(item.totalPrice) || 0);
      
    });

    formData.totalDiscount = $("#TotalDiscount").val();
    // Set payable amount in the form
    //$("#totalSum").val(payableAmount);

    formData.totalPaid = $("#totalPaid").val();
    console.log("Form Data:", formData);
    return JSON.stringify(formData);
}

// Function to handle the form submission
function submitForm() {
    var formData = serializeForm();
    // AJAX request
    $.ajax({
        type: "POST",
        url: "https://localhost:7224/api/Purchase",
        timeout: 10000000000,
        contentType: "application/json",
        data: formData,
        success: function (response) {
            // Handle the success response
            console.log("Success:", response);
            toastr.success('Purchase added successfully');
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
            toastr.error('Error adding purchase');
        }
    });
}

$("#submitBtn").on("click", function () {

    submitForm();
});

function getItemIdFromDropdown(dropdown) {
    return $(dropdown).find(':selected').data('itemid');
}

// Function to get the selected vendor ID from the dropdown
function getVendorIdFromDropdown(dropdown) {
    // Assuming the vendor ID is directly set as the value of the option
    return $(dropdown).find(':selected').data('vendorID');
}



// BARCODE GENRATION
function printBarcode() {
    // Make an AJAX request to the specified URL
    $.ajax({
        type: 'POST',
        url: 'https://localhost:7224/api/Purchase/barcode',
        success: function () {
            // Handle success (optional)
            console.log('Barcode printed successfully');
           
            toastr.success('Barcode Generated successfully');
            $('#barcodeModal').modal('close');
        },
        error: function () {
            // Handle error (optional)
            console.error('Error printing barcode');
            toastr.error('Error printing barcode');

        }
    });
}



//////////////////////////////////////////////////
//GETMETHOD OF UNITTYPE


function populateUnitDropdowns(row) {
    $.ajax({
        url: 'https://localhost:7224/api/UnitType',
        type: 'GET',
        success: function (data) {
            // Populate dropdowns with fetched data
            $.each(data, function (index, item) {
                $('.unit-name').append('<option value="' + item.id + '">' + item.unitTypeName + '</option>');
            });
            $('.unit-name').on('input', function () {
                var selectedItemId = $(this).val();
                var itemNumberDropdown = $(this).closest('tr').find('.unit-name');
                // Add default "Select" option
                itemNumberDropdown.append('<option value="" selected="selected">Select</option>');
            
                itemNumberDropdown.val(selectedItemId).change();
            });
        },
        error: function (error) {
            console.error('Error fetching item data:', error);
        }
    });
}

$(document).ready(function () {
    populateUnitDropdowns();
});

////////////////////POST METHOD OF ITEM/////////////////////////

function submitItemForm() {
    // Get form data
    var formData = {
        id: 0,
        itemName: $('#ItemNames').val(),
        itemNumber: $('#ItemNumbers').val(),
        unitTypeMasterId: $('#UnitType').val(),
        isActive: true
    };

    // Make an AJAX request to the specified URL
    $.ajax({
        type: 'POST',
        url: 'https://localhost:7224/api/Item',
        contentType: 'application/json',
        data: JSON.stringify(formData),
        success: function (response) {
            // Handle success (optional)
            console.log('Item added successfully');
            // You can perform additional actions here if needed
            $('#itemModal').modal('hide');
            toastr.success('Item added successfully');
        },
        error: function () {
            // Handle error (optional)
            console.error('Error adding item');
            toastr.error('Error adding item');
        }
    });
}

