
var taxtypeName;
$(document).ready(function () {
    // Get the query parameter from the URL
    const urlParams = new URLSearchParams(window.location.search);
    const vendorDataParam = urlParams.get('vendorData');

    if (vendorDataParam) {
        // Parse the JSON data
        const vendorData = JSON.parse(vendorDataParam);

        // Populate the form with the retrieved data
        $('input[name="vendorName"]').val(vendorData.name);
        $('input[name="mobile"]').val(vendorData.mobile);
        $('input[name="email"]').val(vendorData.email);
        $('input[name="address"]').val(vendorData.address);
        $('input[name="taxNumber"]').val(vendorData.taxNumber);
        // Fetch data for enquiry type dropdown
        $.ajax({
            type: "get",
            url: "https://localhost:44387/api/TaxType",
            contentType: "application/json",
            dataType: "json",
            success: function (data) {
                var taxTypeDropdown = $("#taxTypeDropdown");
                taxTypeDropdown.empty();

                // Loop through each enquiry type
                $.each(data, function (i, taxtype) {
                    var taxtypeId = taxtype.taxTypeId;
                     taxtypeName = taxtype.name;

                    // Create option and append to the dropdown
                    var option = $("<option>").text(taxtypeName).val(taxtypeId);
                    option.data("tax-type-id", taxtypeId);

                    // Set the selected attribute if it matches the retrieved enquiryTypeName
                    if (taxtypeName === vendorData.taxTypeName) {
                        option.attr('selected', 'selected');
                    }

                    taxTypeDropdown.append(option);
                });

                taxTypeDropdown.select2({ width: '100%' });
            },
            error: function (error) {
                console.error("Error fetching enquiry types: ", error);
            }
        });

  

        // Store the Enquiry ID in a variable
        var vendorId = vendorData.id;

        // Rest of your code remains the same

        $('#btnSubmit').click(function () {
            // Extract data from the form fields
            var vendorName = $('input[name="vendorName"]').val();
            var mobile = $('input[name="mobile"]').val();
            var email = $('input[name="email"]').val();
            var address = $('input[name="address"]').val();
            var taxTypeId = $('#taxTypeDropdown').val();
            var taxnumber = $('input[name="taxNumber"]').val();
          

            // PUT request
            $.ajax({
                type: "PUT",
                url: "https://localhost:44387/api/Vendors/" + vendorId,

                data: JSON.stringify({
                    id: vendorId,
                    name: vendorName,
                    mobile: mobile,
                    email: email,
                    address: address,
                    taxTypeMasterId: taxTypeId,
                    taxNumber: taxnumber,
                    taxTypeName: taxtypeName,
               
                  
                }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function () {
                    var vendorPageUrl = window.location.origin + "/Supplier";
                    window.location.href = vendorPageUrl;
                   
                },
                error: function (error) {
                    console.error("Error updating enquiry: ", error);
                   
                }
            });
        });
    }
});
