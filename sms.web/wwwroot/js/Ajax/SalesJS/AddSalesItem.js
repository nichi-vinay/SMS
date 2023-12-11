$(function () {
    // Initialize the date picker
    $('#datepicker').datepicker({
        format: 'yyyy-mm-dd', // You can change the date format as needed
        autoclose: true
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


$(function () {
    bsCustomFileInput.init();
});
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

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
  
    function updateTotalDiscount() {
        var totalDiscount = 0;

        // Iterate through each row and sum up the discounted_price values
        $("#addRow tbody tr").each(function () {
            var discountedPrice = parseFloat($(this).find("input[name='discount_percentage[]']").val()) || 0;
            totalDiscount += discountedPrice;
        });

        // Update the TotalDiscount input field with the calculated total
        $("#TotalDiscount").val(totalDiscount.toFixed(2));
    }

    // Attach the input event handler to the discounted_price columns
    $("#addRow tbody").on("input", "input[name='discounted_price[]']", function () {
        updateTotalDiscount();
    });
  
    function updatePayableAmount() {
        var totalTax = parseFloat($("#TotalTax").val()) || 0;
        var totalSum = parseFloat($("#totalSum").val()) || 0;



        // Update Payable Amount
        var payableAmount = totalSum - totalTax;
        $("#totalSum").val(payableAmount.toFixed(2));

        // Trigger the updateTotalPayment function
        updateTotalPayment();
    }
    $("#TotalTax").on("input", updatePayableAmount);

    // Function to add a new row to the table
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
        var length = $("#addRow tbody tr").length + 1;
        if (allFieldsFilled) {
            var newRow = `
        <tr>
            <td>
                <select class="form-control select2 item-name" style="width: 100%;"></select>
            </td>
            <td>
                <select class="form-control select2 item-number" style="width: 100%;"></select>
            </td>
        
            <td><input type="number" class="form-control" name="barcode[]"></td>
            <td><input type="number" class="form-control" name="quantity[]"></td>
             <td><input type="number" class="form-control" name="mrp[]"></td> 
            <td>
                                        <select id="taxTypeColumn" class="form-control select2 taxtype-name" style="width: 100%;">
                                            
                                        </select>
                                    </td>
                  
            <td><input type="number" class="form-control" name="discount_percentage[]"></td>
            <td><input type="number" class="form-control" name="total_price[]" onkeydown="addRowOnEnter(event)"></td>
        </tr>
    `;
            $("#addRow tbody").append(newRow);
            updateTotalSum();
            updateTotalDiscount();
            populateItemDropdown($(newRow));
            populateTaxTypeDropdowns($(newRow));
           

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


///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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

/////////////////////////////////////////////////////////////////ItemDropDown//////////////////////////////////////////////////////////////////////////////////////
function populateItemDropdown(row) {
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
$(document).ready(function () {
    populateItemDropdown();
});
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    function populateTaxTypeDropdowns(row) {
        $.ajax({
            url: 'https://localhost:7224/api/TaxType',
            type: 'GET',
            success: function (data) {
                // Populate dropdowns with fetched data
                $.each(data, function (index, taxtype) {
                    $('.taxtype-name').append('<option value="' + taxtype.taxTypeId + '">' + taxtype.name + '</option>');

                });
                $('.taxtype-name').on('input', function () {
                    var selectedItemId = $(this).val();
                     var itemNumberDropdown = $(this).closest('tr').find('.taxtype-name');

                    $.each(data, function (index, item) {
                        if (item.id == selectedItemId) {
                            itemNumberDropdown.append('<option value="' + item.taxTypeId + '">' + item.name + '</option>');
                        }
                    });
                     //itemNumberDropdown.val(selectedItemId).change();
                });
            },
            error: function (error) {
                console.error('Error fetching TaxType data:', error);
            }
        });
    }
    
$(document).ready(function () {
    populateTaxTypeDropdowns();
});
/////////////////////////////////////////////////////////////////CustomerName//////////////////////////////////////////////////////////////////////////////////////


$(function () {


    function populateCustomerDropDown() {

        $.ajax({

            url: 'https://localhost:7224/api/Customers',
            type: 'GET',
            success: function (data) {           
                $.each(data, function (index, company) {
                    $('#CustomerDropDown').append('<option value="' + company.id + '">' + company.name + '</option>')
                });
            },
            error: function (error) {
                console.error('Error fetching vendors:', error);
            }
        });
    }
    populateCustomerDropDown();
});


/////////////////////////////////////////////////////////////////CustomerType//////////////////////////////////////////////////////////////////////////////////////
$(function () {
    function populateCustomerTypeDropDown() {
        $.ajax({
            url: 'https://localhost:7224/api/CustomerType',
            type: 'GET',
            success: function (data) {
                $.each(data, function (index, companyType) {
                    $('#CustomerTypeDropDown').append('<option value="' + companyType.id + '">' + companyType.customerTypeName + '</option>')
                    
                });
            },

            error: function (error) {
                console.error('Error fetching vendors:', error);
            }
        });
    }
    populateCustomerTypeDropDown();
});
function getItemIdFromDropdown(dropdown) {
    return $(dropdown).find(':selected').data('itemid');
}

function updatePrice(row) {
    var quantity = parseFloat(row.find("input[name='quantity[]']").val()) || 0;
    var mrp = parseFloat(row.find("input[name='mrp[]']").val()) || 0;
    var discountPercentage = parseFloat(row.find("input[name='discount_percentage[]']").val()) || 0;
    //var prices = parseFloat(row.find("input[name ='Price[]']").val()) || 0;
    var MRPS = mrp * quantity;
    // Calculate the discount amount
    var discountAmount = MRPS * (discountPercentage / 100);

    // Calculate the discounted price
    var discountedPrice = MRPS + discountAmount;


    // Calculate the total price
    row.find("input[name='total_price[]']").val(MRPS.toFixed(2));
    row.find("input[name='total_price[]']").val(discountAmount.toFixed(2));
    row.find("input[name='total_price[]']").val(discountedPrice.toFixed(2));
}
// Attach the input event handler to the quantity, MRP, and discount percentage columns
$("#addRow tbody").on("input", "input[name='quantity[]'], input[name='mrp[]'], input[name='discount_percentage[]']", function () {
    var currentRow = $(this).closest("tr");
    updatePrice(currentRow);
});
// Call the updatePrice function initially to set the initial price and grand total price
$("#addRow tbody tr").each(function () {
    updatePrice($(this));
});
var totalPriceArray = [];

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

function serializeForm(isCanceled) {
    var currentDate = new Date();

    // Format the date as a DateTime string (adjust the format as needed)
    var formattedDate = currentDate.toLocaleString('en-US', { year: 'numeric', month: '2-digit', day: '2-digit'});
    var formData = {
        customerId: $("#CustomerDropDown").val(),
        customerTypeMasterId: $("#CustomerTypeDropDown").val(),
        invoiceNumber: $("#invoiceNumber").val(),
        invoiceDate: currentDate,
        shipmentDetails: $("#ShipmentDetails").val(),
        totaltax: $("#TotalTax").val(),
        isCanceled: isCanceled,
        totaDiscount: $("#TotalDiscount").val(),
        totalPaid: $("#totalPaid").val(),
        //invoiceCopy: $("").val(),
        expectedDelivery: $("#ExpextedDate").val(),
        taxNumber: $("#CustomerTypeDropDown").val(),
        totalAmount: $("#totalSum").val(),
        cheque: $("#Check").val(),
        cash: $("#Cash").val(),
        online: $("#UPIOnline").val(),
        cards: $("#Cards").val(),
        salesItems:[]
    };
    var totalMRPArray = []; // Array to store MRP values
    totalPriceArray = [];
    $("#addRow tbody tr").each(function () {
        var item = {
            itemID: $(this).find(".item-name").val(),
            barcode: $(this).find("[name='barcode[]']").val(),
            quantity: $(this).find("[name='quantity[]']").val(),
            taxTypeID: $(this).find(".taxtype-name").val(),
            mrp: $(this).find("[name='mrp[]']").val(),
            discountPercentage: $(this).find("[name='discount_percentage[]']").val(),
            totalPrice: $(this).find("[name='total_price[]']").val()
        };
        var mrpValue = parseFloat(item.mrp) || 0;
        var quantity = parseInt(item.quantity) || 1; // Default to 1 if quantity is not provided
        totalMRPArray.push(mrpValue * quantity); // Multiply by quantity

        formData.salesItems.push(item);
        totalPriceArray.push(parseFloat(item.totalPrice) || 0);
    });
    formData.totalMRP = totalMRPArray.reduce(function (a, b) {
        return a + b;
    }, 0);

    formData.totalDiscount = $("#TotalDiscount").val();
    formData.totalPaid = $("#totalPaid").val();
    console.log("Form Data:", formData);
    return JSON.stringify(formData);
}

function submitForm(isCanceled) {
    console.log("Submitting form...");
   
    var Data = serializeForm(isCanceled);
    console.log("Form Data:", Data);
    // AJAX request
    $.ajax({
        type: "POST",
        url: "https://localhost:7224/api/Sales",
        timeout: 10000000000,
        contentType: "application/json",
        data: Data,
        success: function (response) {
            // Handle the success response
            console.log("Success:", response);
           /* toastr.success('Purchase added successfully');*/
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
           /* toastr.error('Error adding purchase');*/
        }
    });
}
$("#saveButton").on("click", function (event) {
    event.preventDefault(); // Prevent the default form submission
    submitForm(true); // 1 indicates isCanceled: 1 for the save button
});
$("#submitBtn").on("click", function (event) {
    event.preventDefault(); // Prevent the default form submission
    submitForm(false);
});

