var enquiryTypeName;


$(document).ready(function () {
    // Get the query parameter from the URL
    const urlParams = new URLSearchParams(window.location.search);
    const enquiryDataParam = urlParams.get('enquiryData');

    if (enquiryDataParam) {
        // Parse the JSON data
        const enquiryData = JSON.parse(enquiryDataParam);

        // Populate the form with the retrieved data
        $('input[name="customerName"]').val(enquiryData.name);
        $('input[name="mobile"]').val(enquiryData.mobile);
        $('input[name="email"]').val(enquiryData.email);
        $('input[name="address"]').val(enquiryData.address);

        // Fetch data for enquiry type dropdown
        $.ajax({
            type: "get",
            url: "https://localhost:7224/api/EnquiryType",
            contentType: "application/json",
            dataType: "json",
            success: function (data) {
                var enquiryTypeDropdown = $("#enquiryTypeDropdown");
                enquiryTypeDropdown.empty();

                // Loop through each enquiry type
                $.each(data, function (i, enquiry) {
                    var enquiryId = enquiry.id;
                     enquiryTypeName = enquiry.enquiryTypeName;

                    // Create option and append to the dropdown
                    var option = $("<option>").text(enquiryTypeName).val(enquiryId);
                    option.data("enquiry-type-id", enquiryId);

                    // Set the selected attribute if it matches the retrieved enquiryTypeName
                    if (enquiryTypeName === enquiryData.enquiryTypeName) {
                        option.attr('selected', 'selected');
                    }

                    enquiryTypeDropdown.append(option);
                });

                enquiryTypeDropdown.select2({ width: '100%' });
            },
            error: function (error) {
                console.error("Error fetching enquiry types: ", error);
            }
        });

        $('input[name="followUpDate"]').val(enquiryData.followUpdate);
        $('textarea[name="comments"]').val(enquiryData.comments);
        $('#isWhatsAppCheckbox').prop('checked', enquiryData.isWhatsApp);

        // Store the Enquiry ID in a variable
        var enquiryId = enquiryData.id;

        // Rest of your code remains the same

        $('#btnSubmit').click(function () {
            // Extract data from the form fields
            var customerName = $('input[name="customerName"]').val();
            var mobile = $('input[name="mobile"]').val();
            var email = $('input[name="email"]').val();
            var address = $('input[name="address"]').val();
            var enquiryTypeId = $('#enquiryTypeDropdown').val();
            var followUpDate = $('input[name="followUpDate"]').val();
            var comments = $('textarea[name="comments"]').val();
            var isWhatsApp = $('#isWhatsAppCheckbox').prop('checked');

            // PUT request
            $.ajax({
                type: "PUT",
                url: "https://localhost:7224/api/Customers/" + enquiryId,

                data: JSON.stringify({
                    id: enquiryId,
                    name: customerName,
                    enquiryTypeName: enquiryTypeName,
                    mobile: mobile,
                    email: email,
                    address: address,
                    enquiryTypeId: enquiryTypeId,
                    followUpDate: followUpDate,
                    comments: comments,
                    isWhatsApp: isWhatsApp
                }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function () {
                   
                    toastr.success('Enquiry has been updated successfully.');
                },
                error: function (error) {
                    console.error("Error updating enquiry: ", error);
                    toastr.error('Error updating enquiry. Please try again.');
                }
            });
        });
    }
});
