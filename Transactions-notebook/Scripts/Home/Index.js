$(document).ready(function () {
    initButtonListeners();
    getAllTransactions();
});

function getAllTransactions() {
    $.ajax({
        type: 'GET',
        url: '/Home/GetAllTransactions',
        contentType: 'application/json; charset=utf-8',
        success: function (response) {
            showAllTransactions(response);
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}

function showAllTransactions(data) {
    data = JSON.parse(data);
    for (var index = 0; index < data.length; index++) {
        $('#table > tbody').append(
            '<tr>' +
                '<td><input id="' + data[index].Id + '" class="selectRow" type="checkbox"></td>' +
                '<td class="operationDate">' + data[index].OperationDate.substring(0, 10) + '</td>' +
                '<td class="quoteName">' + data[index].Name + '</td>' +
                '<td class="comment">' + data[index].Comment + '</td>' +
                '<td class="customerName">' + data[index].Customer.Name + '</td>' +
                '<td class="customerId">' + data[index].Customer.Id + '</td>' +
                '<td class="totalAmount">' + data[index].TotalAmount + '</td>' +
            '</tr>');
    }
}

function initButtonListeners() {
    $('#deleteTransaction').click(function () {
        validateDelete();
    });
    $('#addTransaction').click(function () {
        validateAdd();
    });
    $('#editTransaction').click(function () {
        getDataForEdit();
    });
    $('#saveEdit').click(function () {
        saveEdit();
    });
}

var validateAdd = function () {
    var validJson;
    var dateInput = $("#dateInput").val();
    var nameInput = $("#nameInput").val();
    var commentInput = $("#commentInput").val();
    var customerNameInput = $("#customerNameInput").val();
    var customerIdInput = $("#customerIdInput").val();
    var totalAmountInput = $("#totalAmountInput").val().replace(",", ".");

    var totalAmount = parseFloat(totalAmountInput);
    var date = Date.parse(dateInput);
    var incorrectInputs = 0;

    if (isNaN(totalAmount)) {
        alert('Total amount is invalid.');
        incorrectInputs++;
    }
    if (isNaN(customerIdInput)) {
        alert('Customer Number is invalid.');
        incorrectInputs++;
    }
    if (date === null) {
        alert('Date is invalid.');
        incorrectInputs++;
    }

    totalAmount = JSON.stringify(totalAmount);
    var transactionFields = [date, nameInput, commentInput, customerNameInput, customerIdInput, totalAmount];
    var errorInputs = "";

    for (var index = 0; index < transactionFields.length; index++) {
        if (transactionFields[index] === "" || transactionFields[index] === null || transactionFields[index] === undefined) {
            errorInputs = errorInputs + "Incorrect input number: " + (Number(index) + Number(1)) + ", ";
            incorrectInputs++;
        }
    }
    if (incorrectInputs === 0) {
        validJson = getValidJson(null, date, nameInput, commentInput, customerNameInput, customerIdInput, totalAmount);
        addTransaction(validJson);
    } else {
        alert(errorInputs);
    }
}

function getValidJson(id, date, nameInput, commentInput, customerNameInput, customerIdInput, totalAmount) {
    var json = {
        Id: id,
        Date: date,
        Name: nameInput,
        Comment: commentInput,
        CustomerName: customerNameInput,
        CustomerId: customerIdInput,
        Price: totalAmount
    };
    return json;
}


function addTransaction(transactionFields) {
    $.ajax({
        type: 'POST',
        url: '/Home/AddTransaction',
        data: JSON.stringify(transactionFields),
        contentType: 'application/json; charset=utf-8',
        success: function (response) {
            location.reload();
        },
        error: function (xhr, status, error) {
            location.reload();
            console.log(error);
        }
    });
}

function validateDelete() {
    var selectedItems = $('.selectRow:checked');
    if (selectedItems.length === 0) {
        alert('Select transaction with checkbox first');
    }
    var selected = [];
    selectedItems.each(function() {
        selected.push($(this).attr('id'));
    });
    deleteTransaction(selected);
}

function deleteTransaction(transactionIds) {
    $.ajax({
        type: 'POST',
        url: '/Home/DeleteTransaction',
        data: JSON.stringify(transactionIds),
        contentType: 'application/json; charset=utf-8',
        success: function (response) {
            location.reload();
        },
        error: function (xhr, status, error) {
            location.reload();
            console.log(error);
        }
    });
}

function getDataForEdit() {
    var parentRow = $('.selectRow:checked').first().closest('tr');
    if ($('.selectRow:checked').length === 0) {
        alert('Select transaction with checkbox first');
    } else {
        $('#saveEdit').fadeIn(500);
    }
    var id = parentRow.find('.selectRow').first().attr('id');
    var date = parentRow.find('.operationDate').first().html();
    var quoteName = parentRow.find('.quoteName').first().html();
    var comment = parentRow.find('.comment').first().html();
    var customerName = parentRow.find('.customerName').first().html();
    var customerId = parentRow.find('.customerId').first().html();
    var totalAmount = parentRow.find('.totalAmount').first().html();

    $('#idHolder').val(id);
    $('#dateInput').val(date);
    $('#nameInput').val(quoteName);
    $('#commentInput').val(comment);
    $('#customerNameInput').val(customerName);
    $('#customerIdInput').val(customerId);
    $('#totalAmountInput').val(totalAmount);
}

function saveEdit() {
    var data = getInputs();
    $('#saveEdit').fadeOut(500);
    clearInputs();
    $.ajax({
        type: 'POST',
        url: '/Home/UpdateTransaction',
        data: JSON.stringify(data),
        contentType: 'application/json; charset=utf-8',
        success: function (response) {
            location.reload();
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}

function getInputs() {
    var validJson;
    validJson = getValidJson($('#idHolder').val(),
        $('#dateInput').val(),
        $('#nameInput').val(),
        $('#commentInput').val(),
        $('#customerNameInput').val(),
        $('#customerIdInput').val(),
        $('#totalAmountInput').val());
    return validJson;
}

function clearInputs() {
    $('#dateInput').val("");
    $('#nameInput').val("");
    $('#commentInput').val("");
    $('#customerNameInput').val("");
    $('#customerIdInput').val("");
    $('#totalAmountInput').val("");
}