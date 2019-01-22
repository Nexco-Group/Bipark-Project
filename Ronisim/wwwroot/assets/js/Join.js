$(document).ready(function () {
    $("#divloader").hide();
});

$("#joinsubmit").click(function () {
    $("#divloader").show();

    var fdata = new FormData();
    fdata.append("__RequestVerificationToken", gettoken());
    fdata.append("FnameFa", $('#FnameFa').val());
    fdata.append("LnameFa", $('#LnameFa').val());
    fdata.append("Mobile", $('#Mobile').val());
    fdata.append("Plate", $('#Plate').val());
    fdata.append("Password", $('#Password').val());
    fdata.append("ConfirmPassword", $('#ConfirmPassword').val());
    fdata.append("GenderInput", $("#inputLabelMale").is(':checked'));

    $.ajax({
        type: 'post',
        url: HostUrl + "/iUser/Register",
        data: fdata,
        processData: false,
        contentType: false,
    }).done(function (result) {
        if (result.result == 1) {
            bootbox.alert({
                className: 'my-modal',
                message: "ثبت نام شما با موفقیت انجام شد. پس از تایید شدن توسط ادمین به شما اطلاع داده خواهد شد و پس از آن قادر به استفاده از امکانات سایت خواهید بود"
            });
            setTimeout(
                function () {
                    window.location = HostUrl + "/Login";
                }, 4000);
        }
        else {
            bootbox.alert({
                className: 'my-modal',
                message: result.result
            });
        }
        $("#divloader").hide();

    });
});
