
var selectedTaxTypeId;
var taxtypename;

$(document).ready(function () {


   
   
        // get method to fetch data for supplier dropdown
        $.ajax({
            type: "get",
            url: "https://localhost:44387/api/TaxType",
            contenttype: "application/json",
            datatype: "json",
            success: function (data) {
                var taxtypedropdown = $("#taxTypeDropdown");
                taxtypedropdown.empty();

                taxtypedropdown.append($("<option selected='selected'>").text("select"));

                // loop through each vendor
                $.each(data, function (i, taxtype) {
                    var taxtypeid = taxtype.taxTypeId;
                     taxtypename = taxtype.name;

                    // create option and append to the dropdown
                    var option = $("<option>").text(taxtypename).val(taxtypeid);
                    option.data("tax-type-id", taxtypeid);

                    taxtypedropdown.append(option);
                });

                taxtypedropdown.select2({ width: '100%' });
            },
            error: function (error) {
                console.error("error fetching vendors: ", error);
            }
        });

        // event handler for change in the supplier dropdown
    $("#taxTypeDropdown").on('change', function () {
            // retrieve the selected order id and vendor id
           
        selectedTaxTypeId = $(this).find(':selected').data('tax-type-id');
        });




});
    
  


$("#btnSubmit").on("click", function () {
    submitOrderForm();
});




function submitOrderForm() {




    // Get the selected values from the dropdowns and input fields
    var taxTypeId = selectedTaxTypeId;



    var vendorName = $("input[name='vendorName']").val();
    var mobile = $("input[name='mobile']").val();
    var email = $("input[name='email']").val(); // Convert to ISO string
    var address = $("input[name='address']").val();

    var taxNumber = $("input[name='taxNumber']").val();

    

    // Use the jQuery AJAX function to send a POST request
    $.ajax({
        type: "POST",
        url: "https://localhost:44387/api/Vendors", // Update the URL to your API endpoint
        contentType: "application/json",
        data: JSON.stringify({

            taxTypeMasterId: taxTypeId,
            name: vendorName,
            mobile: mobile,
            address: address,
            email: email,
            taxNumber: taxNumber,
            taxTypeName: taxtypename,
            



        }),
        success: function () {

    
            location.reload();



        },
        error: function (error) {
            // Handle the error response from the server
            alert("Error submitting order: " + error.responseText);
        }
    });
}




