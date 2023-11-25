$(document).ready(function () {
   
   
    // Function to fetch and display data in the table
    function loadData() {
        $.ajax({
            url: 'https://localhost:7224/api/Purchase',
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                console.log(data);
                // Clear existing table rows
                $('#TableBody').empty();

                // Loop through the data and append rows to the table
                $.each(data, function (index, purchase) {
                    var row = '<tr>' +
                        '<td>' + purchase.invoiceNumber + '</td>' +
                        '<td>' + purchase.invoiceDate + '</td>' +
                        '<td>' + purchase.supplierName + '</td>' +
                        '<td>' + purchase.totalAmount + '</td>' +
                        '<td>' + purchase.discountPercentage + '</td>' +
                        '<td>' + calculateTotalPrice(purchase.totalAmount, purchase.discountPercentage) + '</td>' +
                        '<td><a href="#" onclick="editPurchase(' + purchase.id + ')">Edit</a></td>' +
                        '</tr>';
                    $('#TableBody').append(row);
                });

                // Initialize DataTable
                $('#example1').DataTable();
                $(".chosen-select").chosen();
            },
            error: function (error) {
                console.log('Error fetching data:', error);
            }
        });
    }

    // Format date as per your requirement
    

    // Calculate total price based on MRP and discount percentage
    function calculateTotalPrice(totalAmount, discountPercentage) {
        var discount = (totalAmount * discountPercentage) / 100;
        var totalPrice = totalAmount - discount;
        return totalPrice.toFixed(2); // Adjust decimal places as needed
    }

    // Call the loadData function when the document is ready
    loadData();
});

// Function to handle edit action (replace with your actual edit logic)
function editPurchase(id) {
    alert('Edit Purchase with ID: ' + id);
}


