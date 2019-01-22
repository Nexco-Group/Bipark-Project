(function (document, window, $) {
    'use strict';
    var Site = window.Site;
    $(document).ready(function () {
        Site.run();
    });
})(document, window, jQuery);

function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}

$(document).ready(function () {
    $("#divloader").hide();

    if (necap == "") {
        $("#capsec").hide();
    }

    $("#loginbtn").click(function () {
        $("#divloader").show();
        var fdata = new FormData();
        fdata.append("__RequestVerificationToken", gettoken());
        fdata.append("Mobile", $('#Mobile').val());
        fdata.append("Password", $('#Password').val());
        fdata.append("Captcha", $('#Captcha').val());
        fdata.append("RememberMe", $('#RememberMe').is(':checked'));
        $.ajax({
            type: 'post',
            url: HostUrl + "/Login/Index",
            data: fdata,
            processData: false,
            contentType: false,
        }).done(function (result) {
            if (result.result == 1) {
                var ss = getParameterByName('ReturnUrl');
                if (ss != null && ss != "" && ss != "/Logout") {
                    window.location = HostUrl + ss;
                } else {
                    window.location = HostUrl + "/Management";
                }
            } else {
                $("#capsec").show();
                $("#Captcha").val('');
                $("#divloader").hide();
                $("#cimgg").removeAttr("src").attr("src", HostUrl + "/Captcha/LoginCaptcha.jpg?" + Math.floor((Math.random() * 10000) + 1));
                bootbox.alert({
                    className: 'my-modal',
                    message: result.result
                });
            }
        });
    });
});