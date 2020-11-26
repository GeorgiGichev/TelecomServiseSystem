// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$(function () {
    var PlaceHolderElement = $('#PlaceHolder');
    const customer = document.querySelector('#customer')
    $('button[data-toggle="ajax-modal"]').click(function (event) {
        var url = $(this).data('url') + '?customerId=' + customer.value;
        $.get(url).done(function (data) {
            PlaceHolderElement.html(data);
            PlaceHolderElement.find('.modal').modal('show');
        })
    })

    PlaceHolderElement.on('click', '[data-save="modal"]', function (event) {
        var form = $(this).parents('.modal').find('form');
        var actionUrl = form.attr('action');
        var sendData = form.serialize() + '&CustomerId=' + customer.value;
        $.post(actionUrl, sendData).done(function (data) {
            PlaceHolderElement.find('.modal').modal('hide');
            GetAddresses();
        });

    })
});

function GetFreeSlots() {
    const element = document.getElementById("address");
    const selectedAddress = element.options[element.selectedIndex].value;
    console.log(selectedAddress);
    const slotsSelect = document.getElementById("slots");
    var i, L = slotsSelect.options.length - 1;
    for (i = L; i >= 0; i--) {
        slotsSelect.remove(i);
    };
    if (selectedAddress != null && selectedAddress != '') {
        $.ajax({
            type: "GET",
            url: "/Tasks/GetFreeSlotsAsJson?addressId=" + selectedAddress,
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            data: selectedAddress,
            contentType: "json; charset=utf-8",
            success: function (slots) {
                if (slots != null && !jQuery.isEmptyObject(slots)) {
                    document.createElement("option");
                    document.createTextNode("");
                    $.each(slots, function (index, slot) {
                        var option = document.createElement("option");
                        option.setAttribute("value", slot.id);
                        var text = document.createTextNode(slot.fullInfo);
                        option.appendChild(text);
                        slotsSelect.appendChild(option);
                    });
                };
            },
            failure: function (response) {
                alert(response);
            }
        });
    }
};

function GetFreeNumbers() {
    const element = document.getElementById("serviceName");
    const selectedService = element.options[element.selectedIndex].text;
    const numbersSelect = document.getElementById("numbers");
    var i, L = numbersSelect.options.length - 1;
    for (i = L; i >= 0; i--) {
        numbersSelect.remove(i);
    };
    if (selectedService != null && selectedService != '') {
        $.ajax({
            type: "GET",
            url: "/Orders/GetFreeNumbersAsJson?serviceName=" + selectedService,
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            data: selectedService,
            contentType: "json; charset=utf-8",
            success: function (numbers) {
                if (numbers != null && !jQuery.isEmptyObject(numbers)) {
                    document.createElement("option");
                    document.createTextNode("");
                    $.each(numbers, function (index, number) {
                        var option = document.createElement("option");
                        option.setAttribute("value", number.id);
                        var text = document.createTextNode(number.number);
                        option.appendChild(text);
                        numbersSelect.appendChild(option);
                    });
                };
            },
            failure: function (response) {
                alert(response);
            }
        });
    }
};

function GetAddresses() {
    const element = document.getElementById("address");
    var i, L = element.options.length - 1;
    for (i = L; i >= 0; i--) {
        element.remove(i);
    };
    const customerId = document.getElementById("customer").value;
    if (customerId != null || customerId != '') {
        $.ajax({
            type: "GET",
            url: "/Orders/AddressesAsJson?customerId=" + customerId,
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            data: customerId,
            contentType: "json; charset=utf-8",
            success: function (addreses) {
                if (addreses != null && !jQuery.isEmptyObject(addreses)) {
                    document.createElement("option");
                    document.createTextNode("");
                    $.each(addreses, function (index, address) {
                        var option = document.createElement("option");
                        option.setAttribute("value", address.id);
                        var text = document.createTextNode(address.fullAddress);
                        option.appendChild(text);
                        element.appendChild(option);
                    });
                };
                GetFreeSlots();
            },
            failure: function (response) {
                alert(response);
            }
        });
    }
};

