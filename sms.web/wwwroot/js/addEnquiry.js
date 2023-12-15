
var selectedEnquiryTypeId;
var enquirytypename;

$(document).ready(function () {


   
   
        // get method to fetch data for supplier dropdown
        $.ajax({
            type: "get",
            url: "https://localhost:7224/api/EnquiryType",
            contenttype: "application/json",
            datatype: "json",
            success: function (data) {
                var enquirytypedropdown = $("#enquiryTypeDropdown");
                enquirytypedropdown.empty();

                enquirytypedropdown.append($("<option selected='selected'>").text("select"));

                // loop through each vendor
                $.each(data, function (i, enquiry) {
                    var enquiryid = enquiry.id;
                     enquirytypename = enquiry.enquiryTypeName;

                    // create option and append to the dropdown
                    var option = $("<option>").text(enquirytypename).val(enquiryid);
                    option.data("enquiry-type-id", enquiryid);

                    enquirytypedropdown.append(option);
                });

                enquirytypedropdown.select2({ width: '100%' });
            },
            error: function (error) {
                console.error("error fetching vendors: ", error);
            }
        });

        // event handler for change in the supplier dropdown
        $("#enquiryTypeDropdown").on('change', function () {
            // retrieve the selected order id and vendor id
           
            selectedEnquiryTypeId = $(this).find(':selected').data('enquiry-type-id');
        });




});
    
  


$("#btnSubmit").on("click", function () {
    submitOrderForm();
});




function submitOrderForm() {



    let isWhatsAppValue = true;

    // Add an event listener to the checkbox
    const isWhatsAppCheckbox = document.getElementById('isWhatsAppCheckbox');

    // Add an event listener to the checkbox
    isWhatsAppCheckbox.addEventListener('change', function () {
        // Update the global variable with the value of the checkbox (true if checked, false if unchecked)
        isWhatsAppValue = this.checked;
    });

    // Get the selected values from the dropdowns and input fields
    var enquiryTypeId = selectedEnquiryTypeId;



    var customerName = $("input[name='customerName']").val();
    var mobile = $("input[name='mobile']").val();
    var email = $("input[name='email']").val(); // Convert to ISO string
    var address = $("input[name='address']").val();

    var followUpDateInput = $("input[name='followUpDate']").val();
    var followUpDate = new Date(followUpDateInput);

 





    var comments = $("textarea[name='comments']").val();
    






    // Use the jQuery AJAX function to send a POST request
    $.ajax({
        type: "POST",
        url: "https://localhost:7224/api/Customers", // Update the URL to your API endpoint
        contentType: "application/json",
        data: JSON.stringify({

            enquiryTypeId: enquiryTypeId,
            enquiryTypeName: enquirytypename,
            name: customerName,
            mobile: mobile,
            address: address,
            email: email,
            comments: comments,
            followUpdate: followUpDate,
            isWhatsapp: isWhatsAppValue



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




