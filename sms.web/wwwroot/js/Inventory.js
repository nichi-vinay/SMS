//$(document).ready(function () {
//    $.ajax({
//        type: "GET",
//        url: "https://localhost:7224/api/PurchaseItem",
//        contentType: "text/plain; charset=utf-8",
//        dataType: "json",
//        success: function (data) {
//            console.log("Original Data:", data);
//            var aggregatedData = aggregateQuantities(data);
//            console.log("Aggregated Data:", aggregatedData);

//            // Your existing code to populate the table with filteredData
//            var tbody = $('#TableBody');
//            tbody.empty(); // clear table body before adding rows
//            $.each(aggregatedData, function (i, item) {
//                var rows = "<tr>" +
//                    "<td class='customer-id' style='display:none;'>" + item.id + "</td>" +
//                    "<td class='customer-id' style='display:none;'>" + item.itemID + "</td>" +
//                    "<td class='customer-name'>" + item.itemName + "</td>" +
//                    "<td class='customer-number'>" + item.itemNumber + "</td>" +
//                    "<td class='customer-number'>" + item.quantity + "</td>" +
//                    "<td class='customer-number'>" + item.mrp + "</td>" +
//                    "<td class='customer-number'>" + item.totalprice + "</td>" +

//                    "</tr>";
//                tbody.append(rows);
//            });
//        },
//        failure: function (data) {
//            alert(data.responseText);
//        },
//        error: function (data) {
//            alert(data.responseText);
//        }
//    });

//    function aggregateQuantities(data) {
//        var aggregatedData = {};


//        data.forEach(function (item) {
//            var itemId = item.itemID; // Use itemID instead of id
//            var itemNumber = item.itemNumber;
//            if (aggregatedData[itemId]) {
//                // If item ID already exists, aggregate the quantity and totalprice
//                aggregatedData[itemId].quantity += item.quantity;

//            } else {
//                // If item ID does not exist, add the item
//                aggregatedData[itemId] = {
//                    id: item.id,
//                    itemID: item.itemID,
//                    itemName: item.itemName,
//                    itemNumber: item.itemNumber,
//                    quantity: item.quantity,
//                    mrp: item.mrp,
//                    totalprice: item.totalprice
//                };

//            }
//        });

//        return Object.values(aggregatedData);
//    }

//    var aggregatedData; // Declare the variable outside the AJAX success function

//    $.ajax({
//        type: "GET",
//        url: "https://localhost:7224/api/SalesItem",
//        contentType: "text/plain; charset=utf-8",
//        dataType: "json",
//        success: function (data) {
//            console.log("Original Data:", data);
//            aggregatedData = aggregateQuantities(data);
//            console.log("Aggregated Data:", aggregatedData);
//        },
//        failure: function (data) {
//            alert(data.responseText);
//        },
//        error: function (data) {
//            alert(data.responseText);
//        }
//    });

//    // Now you can use the aggregatedData variable outside the AJAX request
//    // For example, you can use it in another function or wherever needed
//    function someOtherFunction() {
//        console.log("Aggregated Data (outside AJAX):", aggregatedData);
//    }

//    function aggregateQuantities(data) {
//        var aggregatedData = {};

//        data.forEach(function (item) {
//            var itemId = item.itemID;
//            if (aggregatedData[itemId]) {
//                // If item ID already exists, aggregate the quantity
//                aggregatedData[itemId].quantity += item.quantity;
//            } else {
//                // If item ID does not exist, add the item
//                aggregatedData[itemId] = {
//                    id: item.id,
//                    itemID: item.itemID,
//                    quantity: item.quantity
//                };
//            }
//        });

//        return Object.values(aggregatedData);
//    }






//});













//$(document).ready(function () {
//    var purchaseData;
//    var salesData;

//    // First AJAX request for PurchaseItemMaster
//    $.ajax({
//        type: "GET",
//        url: "https://localhost:44387/api/Purchase",
//        contentType: "text/plain; charset=utf-8",
//        dataType: "json",
//        success: function (data) {
//            console.log("Purchase Data:", data);
//            // Filter only those purchases where isSubmitted is true
//            var filteredPurchaseData = data.filter(function (purchase) {
//                return purchase.isSubmitted === true;
//            });

//            purchaseData = aggregateQuantities(filteredPurchaseData, 'purchase');

//            // Now make the second AJAX request for SalesItemMaster
//            $.ajax({
//                type: "GET",
//                url: "https://localhost:44387/api/SalesItem",
//                contentType: "text/plain; charset=utf-8",
//                dataType: "json",
//                success: function (salesDataResponse) {
//                    console.log("Sales Item Data:", salesDataResponse);
//                    salesData = aggregateQuantities(salesDataResponse, 'sales');

//                    // Calculate current stock and display the data in the table
//                    displayDataWithStock();
//                },
//                failure: function (data) {
//                    alert(data.responseText);
//                },
//                error: function (data) {
//                    alert(data.responseText);
//                }
//            });
//        },
//        failure: function (data) {
//            alert(data.responseText);
//        },
//        error: function (data) {
//            alert(data.responseText);
//        }
//    });

//    // Function to calculate quantities and stock
//    function aggregateQuantities(data, type) {
//        var aggregatedData = {};

//        data.forEach(function (purchase) {
//            purchase.purchaseItems.forEach(function (item) {
//                var itemId = item.itemID;

//                if (item.quantity > 0) {
//                    if (aggregatedData[itemId]) {
//                        if (item.mrp > aggregatedData[itemId].mrp) {
//                            aggregatedData[itemId].mrp = item.mrp;
//                        }
//                        aggregatedData[itemId].quantity += item.quantity;
//                    } else {
//                        aggregatedData[itemId] = {
//                            id: item.id,
//                            itemID: item.itemID,
//                            itemName: item.itemName,
//                            itemNumber: item.itemNumber,
//                            quantity: item.quantity,
//                            mrp: item.mrp,
//                            totalprice: 0,
//                            currentStock: 0
//                        };
//                    }
//                }
//            });
//        });

//        if (type === 'sales') {
//            for (var itemId in aggregatedData) {
//                if (aggregatedData.hasOwnProperty(itemId)) {
//                    var salesItem = aggregatedData[itemId];
//                    var purchaseItem = purchaseData[itemId];

//                    if (purchaseItem) {
//                        purchaseItem.currentStock = purchaseItem.quantity - (salesItem ? salesItem.quantity : 0);
//                        purchaseItem.totalprice = purchaseItem.mrp * purchaseItem.currentStock;
//                    }
//                }
//            }
//        }

//        return aggregatedData;
//    }


//    function displayDataWithStock() {
//        var tbody = $('#TableBody');
//        tbody.empty(); // clear table body before adding rows

//        for (var itemId in purchaseData) {
//            if (purchaseData.hasOwnProperty(itemId)) {
//                var item = purchaseData[itemId];

//                // Set current stock to total stock if not available
//                if (item.currentStock === undefined) {
//                    item.currentStock = item.quantity;
//                } else {
//                    // Update current stock if it is available
//                    item.currentStock = item.quantity - (salesData[itemId] ? salesData[itemId].quantity : 0);
//                }

//                // Calculate totalprice as mrp * currentStock
//                item.totalprice = item.mrp * item.currentStock;

//                var rows = "<tr>" +
//                    "<td class='customer-id' style='display:none;'>" + item.id + "</td>" +
//                    "<td class='customer-id' style='display:none;'>" + item.itemID + "</td>" +
//                    "<td class='customer-name'>" + item.itemName + "</td>" +
//                    "<td class='customer-number'>" + item.itemNumber + "</td>" +
//                    "<td class='customer-number'>" + (item.quantity || 0) + "</td>" + // Total Stock
//                    "<td class='customer-number'>" + (item.currentStock || 0) + "</td>" + // Current Stock
//                    "<td class='customer-number' style='display:none;'>" + item.quantity + "</td>" +
//                    "<td class='customer-number'>" + item.mrp + "</td>" +
//                    "<td class='customer-number'>" + item.totalprice + "</td>" +
//                    "</tr>";
//                tbody.append(rows);
//            }
//        }
//    }

//});


$(document).ready(function () {
    var purchaseData;
    var salesData;


    // Initialize DataTable
    var dataTable = $("#example1").DataTable({
        "responsive": true,
        "lengthChange": false,
        "autoWidth": false,
        "buttons": [
            'copy', 'csv', 'excel', 'pdf', 'print', 'colvis'
        ]
    });

    // Initialize DataTables Buttons
    new $.fn.dataTable.Buttons(dataTable, {
        buttons: [
            'copy', 'csv', 'excel', 'pdf', 'print', 'colvis'
        ]
    });

    // Append Buttons container to the DataTable wrapper
    dataTable.buttons().container().appendTo('#example1_wrapper .col-md-6:eq(0)');

    // First AJAX request for PurchaseItemMaster
    $.ajax({
        type: "GET",
        url: "https://localhost:44387/api/Purchase",
        contentType: "text/plain; charset=utf-8",
        dataType: "json",
        success: function (data) {
            console.log("Purchase Data:", data);
            // Filter only those purchases where isSubmitted is true
            var filteredPurchaseData = data.filter(function (purchase) {
                return purchase.isSubmitted === true;
            });

            purchaseData = aggregateQuantities(filteredPurchaseData, 'purchase');
           

            // Now make the second AJAX request for SalesItemMaster
            $.ajax({
                type: "GET",
                url: "https://localhost:44387/api/SalesItem",
                contentType: "text/plain; charset=utf-8",
                dataType: "json",
                success: function (salesDataResponse) {
                    console.log("Sales Item Data:", salesDataResponse);
                    salesData = aggregateQuantities(salesDataResponse, 'sales');


                    console.log("Purchase Data:", purchaseData);
                    console.log("Sales Data:", salesData);
                    // Calculate current stock and display the data in the table
                    displayDataWithStock();
                },
                failure: function (data) {
                    alert(data.responseText);
                },
                error: function (data) {
                    alert(data.responseText);
                }
            });
        },
        failure: function (data) {
            alert(data.responseText);
        },
        error: function (data) {
            alert(data.responseText);
        }
    });

    // Function to calculate quantities and stock
    // Function to calculate quantities and stock
    function aggregateQuantities(data, type) {
        console.log("Inside aggregateQuantities function for type:", type);

        var aggregatedData = {};

        if (data && data.length) {
            data.forEach(function (purchase) {
                if (purchase.purchaseItems && purchase.purchaseItems.length) {
                    purchase.purchaseItems.forEach(function (item) {
                        var itemId = item.itemID;

                        if (item.quantity > 0) {
                            if (aggregatedData[itemId]) {
                                if (item.mrp > aggregatedData[itemId].mrp) {
                                    aggregatedData[itemId].mrp = item.mrp;
                                }
                                aggregatedData[itemId].quantity += item.quantity;
                            } else {
                                aggregatedData[itemId] = {
                                    id: item.id,
                                    itemID: item.itemID,
                                    itemName: item.itemName,
                                    itemNumber: item.itemNumber,
                                    quantity: item.quantity,
                                    mrp: item.mrp,
                                    totalprice: 0,
                                    currentStock: 0
                                };
                            }
                        }
                    });
                }
            });

            console.log("Aggregated Data:", aggregatedData);

            if (type === 'sales') {
                for (var itemId in aggregatedData) {
                    if (aggregatedData.hasOwnProperty(itemId)) {
                        var salesItem = aggregatedData[itemId];
                        var purchaseItem = purchaseData[itemId];

                        if (purchaseItem) {
                            purchaseItem.currentStock = purchaseItem.quantity - (salesItem ? salesItem.quantity : 0);
                            purchaseItem.totalprice = purchaseItem.mrp * purchaseItem.currentStock;
                        }
                    }
                }
            }
        } else {
            console.error("Data is undefined or empty.");
        }

        return aggregatedData;
    }


    function displayDataWithStock() {
        var tbody = $('#TableBody');
        tbody.empty(); // clear table body before adding rows

        for (var itemId in purchaseData) {
            if (purchaseData.hasOwnProperty(itemId)) {
                var item = purchaseData[itemId];


                if (item.currentStock === undefined) {
                    item.currentStock = item.quantity;
                } else {
                    // Update current stock if it is available
                    item.currentStock = item.quantity - (salesData[itemId] ? salesData[itemId].quantity : 0);
                }

                // Calculate totalprice as mrp * currentStock
                item.totalprice = item.mrp * item.currentStock;

                var rows = "<tr>" +
                    "<td class='customer-id' style='display:none;'>" + item.id + "</td>" +
                    "<td class='customer-id' style='display:none;'>" + item.itemID + "</td>" +
                    "<td class='customer-name'>" + item.itemName + "</td>" +
                    "<td class='customer-number'>" + item.itemNumber + "</td>" +
                    "<td class='customer-number'>" + (item.quantity || 0) + "</td>" + // Total Stock
                    "<td class='customer-number'>" + (item.currentStock || 0) + "</td>" + // Current Stock
                    "<td class='customer-number' style='display:none;'>" + item.quantity + "</td>" +
                    "<td class='customer-number'>" + item.mrp + "</td>" +
                    "<td class='customer-number'>" + item.totalprice + "</td>" +
                    "</tr>";
                tbody.append(rows);

            }

        }
        dataTable.clear().rows.add(tbody.find('tr')).draw();

    }
   


});




