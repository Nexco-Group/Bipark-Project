$(document).ready(function () {
    $("#divloader").hide();
    //$('input').keypress(function (e) {
    //    if (e.keyCode == 13)
    //        $('#SendVerify').click();
    //});  
});

var verifycode = function (element) {
    myelement = $(element);
    myelement.text("... در حال تایید کد");
    myelement.attr("disabled", true);
    var fdata = new FormData();
    fdata.append("__RequestVerificationToken", gettoken());
    fdata.append("Mobile", $('#Mobile').val());
    fdata.append("NationCode", $('#NationCode').val());
    fdata.append("Captcha", $('#Captcha').val());
    fdata.append("SMSCode", $('#SMS_Code').val());

    $.ajax({
        type: 'post',
        url: HostUrl + "/PasswordRequest/Verify",
        data: fdata,
        processData: false,
        contentType: false,
    }).done(function (result) {
        if (result.result == 1) {
            window.location = HostUrl + "/PasswordRequest";
        }
        else if (result.result == 2) {
            bootbox.alert({
                className: 'my-modal',
                message: "کد وارد شده اشتباه است"
            });
            myelement.attr("disabled", false);
            myelement.text("تایید");
        }
        else {
            bootbox.alert({
                className: 'my-modal',
                message: result.result
            });
            myelement.attr("disabled", false);
            myelement.text("تایید");
        }
    });
};

var resendsms = function (element) {
    myelement = $(element);
    myelement.text("... در حال ارسال");
    myelement.attr("disabled", true);

    var fdata = new FormData();
    fdata.append("__RequestVerificationToken", gettoken());
    fdata.append("Mobile", $('#Mobile').val());

    $.ajax({
        type: 'post',
        url: HostUrl + "/PasswordRequest/ReSend",
        data: fdata,
        processData: false,
        contentType: false,
    }).done(function (result) {
        if (result.result == 1) {
            bootbox.alert({
                className: 'my-modal',
                message: "کد تایید تلفن همراه برای شما ارسال شد"
            });
        }
        else {
            bootbox.alert({
                className: 'my-modal',
                message: result.result
            });
        }
        //myelement.attr("disabled", false);
        myelement.text("کد تایید برای شما ارسال شد");
    });
};

var sendverify = function () {
    $("#divloader").show();
    var fdata = new FormData();
    fdata.append("__RequestVerificationToken", gettoken());
    fdata.append("Mobile", $('#Mobile').val());
    fdata.append("NationCode", $('#NationCode').val());
    fdata.append("Captcha", $('#Captcha').val());
    $.ajax({
        type: 'post',
        url: HostUrl + "/PasswordRequest/Index",
        data: fdata,
        processData: false,
        contentType: false,
    }).done(function (result) {
        if (result.result == 1) {
            var dialog = bootbox.dialog({
                className: 'my-modal',
                message: '<h4 class="text-center font-weight-bold">تایید تلفن همراه</h4><hr/><p class="text-center"> در حال فرستادن پیامک </i></p>',
                closeButton: true
            });
            dialog.init(function () {
                setTimeout(function () {
                    dialog.find('.bootbox-body').html('<h4 class="text-center font-weight-bold">تایید تلفن همراه</h4><hr/><p class="text-center"> .در حال فرستادن پیامک </i></p>');
                }, 1000);

                setTimeout(function () {
                    dialog.find('.bootbox-body').html('<h4 class="text-center font-weight-bold">تایید تلفن همراه</h4><hr/><p class="text-center"> ..در حال فرستادن </i></p>');
                }, 2000);

                setTimeout(function () {
                    dialog.find('.bootbox-body').html('<h4 class="text-center font-weight-bold">تایید تلفن همراه</h4><hr/><p class="text-center"> ...در حال فرستادن پیامک </i></p>');
                }, 3000);

                setTimeout(function () {
                    setTimeout(function () {
                        dialog.find('.bootbox-body').html('<h4 class="text-center font-weight-bold">تایید تلفن همراه</h4><hr/><p class="text-center"> .در حال فرستادن پیامک </i></p>');
                    }, 1000);

                    setTimeout(function () {
                        dialog.find('.bootbox-body').html('<h4 class="text-center font-weight-bold">تایید تلفن همراه</h4><hr/><p class="text-center"> ..در حال فرستادن پیامک </i></p>');
                    }, 2000);

                    setTimeout(function () {
                        dialog.find('.bootbox-body').html('<h4 class="text-center font-weight-bold">تایید تلفن همراه</h4><hr/><p class="text-center"> ...در حال فرستادن پیامک </i></p>');
                    }, 3000);
                }, 3000);

                setTimeout(function () {
                    dialog.find('.bootbox-body').html('<h4 class="text-center font-weight-bold">تایید تلفن همراه</h4><hr/><p class="text-center"> لطفا کد پنج رقمی فرستاده شده را وارد کنید </p><div class="text-center col-lg-5" style="display: block; margin: 0 auto;"><input type="text" class="form-control text-center" style="margin-bottom: 10px" lang="num" id="SMS_Code" maxlength="5"><button type="button" style="margin-bottom: 10px" onclick="verifycode(this)" class="btn btn-primary btn-block"><b>تایید</b></button><div id="timeremain" style="margin-top: 8px"></div></div>');

                    var timeleft = 90;
                    var downloadTimer = setInterval(function () {
                        timeleft--;
                        dialog.find("#timeremain").html(" باید " + timeleft + " ثانیه صبر کنید" + "<br/>" + " برای درخواست مجدد کد ");
                        if (timeleft <= 0) {
                            clearInterval(downloadTimer);
                            dialog.find("#timeremain").html('<hr/> <button type="button" class="btn btn-primary btn-block" id="resendsmsin" onclick="resendsms(this)">ارسال مجدد کد</button> ');
                        }
                    }, 1000);
                }, 7000);
            });
        } else {
            $("#Captcha").val('');
            $("#divloader").hide();
            $("#cimgg").removeAttr("src").attr("src", HostUrl + "/Captcha/PRCaptcha.jpg?" + Math.floor((Math.random() * 10000) + 1));
            bootbox.alert({
                className: 'my-modal',
                message: result.result
            });
        }
    });
};