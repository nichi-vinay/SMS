$(document).ready(function () {

    var baseUrl = 'Sales/Edit/';
    // Function to fetch and display data in the tableSales
    function loadData() {
        $.ajax({
            url: 'https://localhost:7224/api/Sales',
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                console.log(data);
                // Clear existing table rows
                $('#TableBody').empty();

                // Loop through the data and append rows to the table
                $.each(data, function (index, Sales) {
                    var row = '<tr>' +
                        '<td>' + Sales.invoiceNumber + '</td>' +
                        '<td>' + Sales.invoiceDate + '</td>' +
                        '<td>' + Sales.quantity + '</td>' +
                        '<td>' + Sales.totalMRP + '</td>' +
                        '<td>' + Sales.totaDiscount + '</td>' +
                        '<td>' + Sales.totalPaid + '</td>' +
                        '<td><a href="' + baseUrl+Sales.id + '">Edit</a></td>' +

                        '</tr>';
                    $('#TableBody').append(row);
                });

                // Initialize DataTable
                $('#example1').DataTable();
                //$(".chosen-select").chosen();
            },
            error: function (error) {
                console.log('Error fetching data:', error);
            }
        });
    }
    // Call the loadData function when the document is ready
    loadData();
});

// Function to handle edit action (replace with your actual edit logic)
function editSales(id) {
    var baseUrl = 'https://localhost:7224/api/Sales/';
    window.location.href = baseUrl + 'Edit/' + id;
}

var salesId = "";
///fetch the values by ID
$(document).ready(function () {
    // Get the salesId from the URL
    var pathSegments = window.location.pathname.split('/');
    salesId = pathSegments[pathSegments.length - 1];
});

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
function getTaxtypeFromDropdown(dropdowns) {
    return $(dropdowns).find(':selected').data('taxTypeIDs');
}
var ItemValues = [];
var TaxTypeVAlue = [];

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
function PopulateTaxTypeDropdown(row) {
    $.ajax({
        url: 'https://localhost:7224/api/TaxType',
        type: 'GET',
        success: function (bata) {
            TaxTypeVAlue = bata;
            getSalesDataById();
        },
        error: function (error) {
            console.error('Error fetching item data:', error);
        }
        });
}

function populateItemDropdown(row) {
    $.ajax({
        url: 'https://localhost:7224/api/Item',
        type: 'GET',
        success: function (data) {
            // Populate dropdowns with fetched data
            ItemValues = data;
            getSalesDataById();
        },
        error: function (error) {
            console.error('Error fetching item data:', error);
        }
    });
}

function BindTaxtypeValue(taxTypeIDs, ctrl) {
    var taxtype = "";
    if (ctrl == "taxtypppe") {
        $.each(TaxTypeVAlue, function (index, taxtypes) {
            taxtype = taxtype + '<option value="' + taxtypes.taxTypeId + '"> ' + taxtypes.name + '</option> '

        });
    }
    console.log("Taxtypes", taxtype);
    return taxtype;
}
function BindItemValues(itemId, ctrl) {
    var items = "";
    if (ctrl == "name") {
        $.each(ItemValues, function (index, item) {
         
            items = items + '<option value="' + item.id + '"> ' + item.itemName + '</option> '
        });
    }
    else {
        $.each(ItemValues, function (index, item) {
        
            items = items + '<option value="' + item.id + '" >' + item.itemNumber + '</option>'
        });
    }

    console.log("items", items);

    return items;
}
function getSalesDataById() {
    $.ajax({
        url: 'https://localhost:7224/api/Sales/' + salesId,
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            // Populate form fields with the received data
            $('#CustomerDropDown').val(data.customerId);
            $('#CustomerTypeDropDown').val(data.customerTypeMasterId);
            $('#invoiceNumber').val(data.invoiceNumber);
            $('#InvoiceDate').val(data.invoiceDate);
            $('#ShipmentDetails').val(data.shipmentDetails);
            $('#quantity').val(data.quantity);
            $('#totalMRP').val(data.totalMRP);
            $('#TotalDiscount').val(data.totaDiscount);
            $('#totalPaid').val(data.totalPaid);
            $('#ExpextedDate').val(data.expectedDelivery);
            $('#TotalTax').val(data.totaltax);
            $('#totalSum').val(data.totalAmount);
            $('#taxNumber').val(data.taxNumber);
            $('#Check').val(data.cheque);
            $('#Cash').val(data.cash);
            $('#UPIOnline').val(data.online);
            $('#Cards').val(data.cards);

            $('#isCanceled').val(data.isCanceled);
            getSalesItemsData(data.salesItems);
        },
        error: function (error) {
            console.log('Error fetching sales data by ID:', error);
        }
    });
}

function getSalesItemsData(salesItems) {
    // Clear existing table rows
    $('#salesItemsTable tbody').empty();

    // Loop through salesItems and append rows to the table
    $.each(salesItems, function (index, salesItem) {
        let itemValueOptions = BindItemValues(salesItem.itemID, "name");
        let itemNumberOptions = BindItemValues(salesItem.itemID, "");
        let taxtypeOptions = BindTaxtypeValue(salesItem.taxTypeID, "taxtypppe");
   
        var row = '<tr>' +
            '<td><select class="form-control select2 itemName item-name-' + salesItem.id+' " style="width: 100%;">' + itemValueOptions + '</select></td>' +
            '<td><select class="form-control select2 itemNumber item-number-' + salesItem.id +'" style="width: 100%;">' + itemNumberOptions + '</select></td>' +
            '<td><input type="number" class="form-control" name="barcode[]" value="' + salesItem.barcode + '"></td>' +
            '<td><input type="number" class="form-control" name="quantity[]" value="' + salesItem.quantity + '"></td>' +
            '<td><input type="number" class="form-control" name="mrp[]" value="' + salesItem.mrp + '"></td>' +
            '<td><select class="form-control select2 taxtypename taxtype-name-' + salesItem.id + ' " style="width: 100%;">' + taxtypeOptions +'</select></td>' +
            '<td><input type="number" class="form-control" name="discount_percentage[]" value="' + salesItem.discountPercentage + '"></td>' +
            '<td><input type="number" class="form-control" name="total_price[]" value="' + salesItem.totalPrice + '"></td>' +
            '</tr>';
        $('#salesItemsTable tbody').append(row);
        $(".item-name-" + salesItem.id + "").val(salesItem.itemID); // Select the option with a value of '1'
        $(".item-name-" + salesItem.id + "").trigger('change');


        $(".taxtype-name-" + salesItem.id + "").val(salesItem.taxTypeID);
        $(".taxtype-name-" + salesItem.id + "").trigger('change');

        $(".item-number-" + salesItem.id + "").val(salesItem.itemID); // Select the option with a value of '1'
        $(".item-number-" + salesItem.id + "").trigger('change');


    
        console.log("Row", row);
    });

    // Initialize Select2 for dynamically added elements
    $('.select2').select2();
    console.log("Slaes", salesItems);

}
$(document).ready(function () {
    populateItemDropdown();
});
$(document).ready(function () {
    PopulateTaxTypeDropdown();
});


$('#salesItemsTable tbody').on('keydown', 'tr:last input[name="total_price[]"]', function (e) {
    if (e.which === 13) { // Check if the pressed key is "Enter"
        e.preventDefault(); // Prevent the default behavior of the Enter key

        // Add a new row to the table
        addNewRowToSalesItemsTable();
    }
});

// Function to add a new row to the salesItemsTable
function addNewRowToSalesItemsTable() {
    var currentRow = $("#salesItemsTable tbody tr:last");
    var inputs = currentRow.find("input");
    var allFieldsFilled = true;

    inputs.each(function () {
        if ($(this).val() === "") {
            allFieldsFilled = false;
            return false; // Exit the loop early
        }
    });
    var length = $("#salesItemsTable tbody tr").length + 1;
    if (allFieldsFilled) {
        let newRow = '<tr>' +
            '<td><select class="form-control select2 itemName" id="item-name-'  + length+'" style = "width: 100%;" ></select></td > ' +
            '<td><select class="form-control select2 itemNumber " id="item-number-' + length +'" style="width: 100%;"></select></td>' +
            '<td><input type="number" class="form-control" name="barcode[]"></td>' +
            '<td><input type="number" class="form-control" name="quantity[]"></td>' +
            '<td><input type="number" class="form-control" name="mrp[]"></td>' +
            '<td><select class="form-control select2 taxtypename" id="taxtype-name-' + length +'" style="width: 100%;"></select></td>' +
            '<td><input type="number" class="form-control" name="discount_percentage[]"></td>' +
            '<td><input type="number" class="form-control" name="total_price[]"></td>' +
            '</tr>';
        $("#salesItemsTable tbody").append(newRow);
        updateTotalSum();
        updateTotalDiscount();
        populateItemDropdowns($(newRow), length);
        populateTaxTypeDropdowns($(newRow), length);
    }
    
    
}

// Function to populate item dropdowns in a row
function populateItemDropdowns(row, length) {
    $.ajax({
        url: 'https://localhost:7224/api/Item',
        type: 'GET',
        success: function (data) {
            console.log("Item Data:", data);
            // Populate dropdowns with fetched data
            $.each(data, function (index, item) {
                $('#item-name-' + length).append('<option value="' + item.id + '">' + item.itemName + '</option>');
                $('#item-number-' + length).append('<option value="' + item.id + '">' + item.itemNumber + '</option>');
            });
            $('.itemName').on('input', function () {
                var selectedItemId = $(this).val();
                var itemNumberDropdown = $(this).closest('tr').find('.itemNumber');
               
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
function populateTaxTypeDropdowns(row, length) {
    $.ajax({
        url: 'https://localhost:7224/api/TaxType',
        type: 'GET',
        success: function (dataaaa) {
            // Populate dropdowns with fetched data
            $.each(dataaaa, function (index, taxtype) {
                $('#taxtype-name-' + length).append('<option value="' + taxtype.taxTypeId + '">' + taxtype.name + '</option>');
            });
            $('.taxtype-name-').on('input', function () {
                var selectedItemIds = $(this).val();             
                selectedItemIds.find('option:selected').val(selectedItemIds);
            });
        },
        error: function (error) {
            console.error('Error fetching TaxType data:', error);
        }
    });
}

$(document).ready(function () {

    $(document).on('change', '.itemName', function ()  {
        var selectedItemId = $(this).val();
        var itemNumberDropdown = $(this).closest('tr').find('.itemNumber');
        itemNumberDropdown.val(selectedItemId).change();
    })

   
    populateTaxTypeDropdowns();

});


function updateTotalSum() {

    var totalSum = 0;
    $("input[name='total_price[]']").each(function () {
        var value = parseFloat($(this).val()) || 0;
        totalSum += value;
    });
    $("#totalSum").val(totalSum.toFixed(2)); // Update the total sum in the input field


}
$("#salesItemsTable tbody").on("input", "input[name='total_price[]']", function () {
    updateTotalSum(); // Update the total sum when the "Total Price" value changes
});

function updateTotalDiscount() {
    var totalDiscount = 0;

    // Iterate through each row and sum up the discounted_price values
    $("#salesItemsTable tbody tr").each(function () {
        var discountedPrice = parseFloat($(this).find("input[name='discount_percentage[]']").val()) || 0;
        totalDiscount += discountedPrice;
    });

    // Update the TotalDiscount input field with the calculated total
    $("#TotalDiscount").val(totalDiscount.toFixed(2));
}

// Attach the input event handler to the discounted_price columns
$("#salesItemsTable tbody").on("input", "input[name='discounted_price[]']", function () {
    updateTotalDiscount();
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

function removeLastRow() {
    var rowCount = $("#salesItemsTable tbody tr").length;
    if (rowCount > 1) {
        var lastRow = $("#salesItemsTable tbody tr:last");
        var totalPriceInput = lastRow.find("input[name='total_price[]']");
        if (totalPriceInput.val() === "") {
            if (confirm("Do you want to delete this row?")) {
                lastRow.remove();
            }
        }
    }
}

// Attach the keydown event handler to the "Total Price" column
$("#salesItemsTable tbody").on("keydown", "input[name='total_price[]']", function (event) {
    if (event.key === "Enter") {
        event.preventDefault(); // Prevent the default behavior of Enter key
        addNewRowToSalesItemsTable();
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

function updatePrice(row) {
    var quantity = parseFloat(row.find("input[name='quantity[]']").val()) || 0;
    var mrp = parseFloat(row.find("input[name='mrp[]']").val()) || 0;
    var discountPercentage = parseFloat(row.find("input[name='discount_percentage[]']").val()) || 0;
    //var prices = parseFloat(row.find("input[name ='Price[]']").val()) || 0;
    var MRPS = mrp * quantity;
    // Calculate the discount amount
    var discountAmount = MRPS * (discountPercentage / 100);

    // Calculate the discounted price
    var discountedPrice = MRPS - discountAmount;


    // Calculate the total price
    row.find("input[name='total_price[]']").val(MRPS.toFixed(2));
    row.find("input[name='total_price[]']").val(discountAmount.toFixed(2));
    row.find("input[name='total_price[]']").val(discountedPrice.toFixed(2));
}
// Attach the input event handler to the quantity, MRP, and discount percentage columns
$("#salesItemsTable tbody").on("input", "input[name='quantity[]'], input[name='mrp[]'], input[name='discount_percentage[]']", function () {
    var currentRow = $(this).closest("tr");
    updatePrice(currentRow);
});
// Call the updatePrice function initially to set the initial price and grand total price
$("#salesItemsTable tbody tr").each(function () {
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
    var formData = {
        id: salesId,
        customerId: $("#CustomerDropDown").val(),
        customerTypeMasterId: $("#CustomerTypeDropDown").val(),
        invoiceNumber: $("#invoiceNumber").val(),
        invoiceDate: $("#InvoiceDate").val(),
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
        salesItems: []
    };
    var totalMRPArray = []; // Array to store MRP values
    totalPriceArray = [];
    $("#salesItemsTable tbody tr").each(function () {
        var item = {
            itemID: $(this).find(".itemName").val(),
            barcode: $(this).find("[name='barcode[]']").val(),
            quantity: $(this).find("[name='quantity[]']").val(),
            taxTypeID: $(this).find(".taxtypename").val(),
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
        type: "PUT",
        url: 'https://localhost:7224/api/Sales/' + salesId,
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