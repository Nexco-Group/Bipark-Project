$(document).ready(function () {
    $("#divloader").hide();
});


var backup = "";

$('#combotime').on('change', function () {
    if (backup == "") {
        backup = $("#combopack").html();
    }
    else {
        $("#combopack").html(backup);
    }
    var sa = this.value.split("-");
    var vl = sa[0];
    var tp = sa[1];
    $("#combopack option").each(function () {
        if ($(this).data("date") != vl || $(this).data("type") != tp) {
            $(this).remove();
            console.log(vl + '-' + tp);
            console.log($(this).data("date") + '-' + $(this).data("type"));
        } else {
            console.log(' - ' + $(this).text().split(' - ')[1]);
            var sas = $(this).text();
            sas = sas.replace(' - ' + $(this).text().split(' - ')[1], '');
            console.log(sas);
            $(this).text(sas);
        }
    });
    //$('#combopack').prop('selectedIndex', 0);
});


$("#saverequestbtn").click(function () {
    $("#divloader").show();
    var fdata = new FormData();

    fdata.append("pack", $('#combopack').val());
    fdata.append("pmobile", $('#pmobile').val());
    fdata.append("__RequestVerificationToken", gettoken());

    $.ajax({
        type: 'post',
        url: HostUrl + "/Home/SaveRequest",
        data: fdata,
        processData: false,
        contentType: false
    }).done(function (result) {
        $("#divloader").hide();
        if (result.result == 1) {
            bootbox.hideAll();
            var dialog = bootbox.dialog({                
                message: "<p>لطفا درگاه پرداخت را انتخاب کنید</p>",
                buttons: {
                    cancel: {
                        label: "<img src=\"./mellat.png\" style=\"max-width: 100px;\">",
                        className: 'btn-white',
                        callback: function () {
                            window.location = result.paylink2;
                        }
                    },
                    noclose: {
                        label: "<img src=\"./zarinpal.png\" style=\"max-width: 100px;\">",
                        className: 'btn-white',
                        callback: function () {
                            window.location = result.paylink1;
                        }
                    }
                }
            });
        }
        else {
            bootbox.alert({
                className: 'my-modal',
                message: result.result
            });

            bootbox.hideAll();
            $("#divloader").hide();
        }
    });
});