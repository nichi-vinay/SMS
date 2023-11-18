$(document).ready(function () {
    $.ajax({
        type: "GET",
        url: "https://localhost:7224/api/Purchase",
        dataType: "json",

        success: function (data) {
            var tbody = $('#TableBody');
            tbody.empty();
            $.each(data, function (i, item) {
                var rows = "<tr>" +
                           "<td>" + item.id + "</td>" +
                           "<td>" + item.vendorId + "</td>" +
                           "<td>" + item.invoiceNumber + "</td>" +
                           "<td>" + item.invoiceDate + "</td>" +
                           "<td>" + item.shipmentDetails + "</td>" +
                           "<td>" + item.isSubmitted + "</td>" +

                          "</tr>";
                tbody.append(rows);
            });
        },
    });

});