$(document).ready(function () {
    $("#divloader").hide();
    refreshOrderList();
    setTimeout(function () {
        setInterval(function () { refreshOrderList(); }, 10000);
    }, 10000);
});

function refreshOrderList() {
    //bal
    $.getJSON(HostUrl + "/iUser/getbal", {
    }, function (data) {
        var a = data.result;
        if (a == 1) {
            bootbox.hideAll();
            bootbox.alert("مشکلی پیش آمده است");
            $("#divloader").hide();
        }
        else {
            $("#bal1").html("موجودی: " + a.a1.toLocaleString() + " ریال");
            $("#divloader").hide();
        }
    });
}
function refreshOrderList2() {
    //bal
    $("#divloader").show();

    $.getJSON(HostUrl + "/iUser/getbal", {
    }, function (data) {
        var a = data.result;
        if (a == 1) {
            bootbox.hideAll();
            bootbox.alert("مشکلی پیش آمده است");
            $("#divloader").hide();
        }
        else {
            $("#bal1").html("موجودی: " + a.a1.toLocaleString() + " ریال");

            $("#divloader").hide();

        }

    });
}

function showtransanction() {
    $.getJSON(HostUrl + "/iUser/getRmoney", {
    }, function (data) {
        var buyitems = data.result;
        if (buyitems == 0) {
            bootbox.hideAll();
            bootbox.alert("شما هیچ تراکنشی ندارید");
            $("#divloader").hide();
        } else {
            var tbl = '<table class="table table-hover" dir="rtl"> <thead> <tr> <th class="text-right">ID</th> <th class="text-right">مبلغ</th> <th class="text-right">نوع تراکنش</th> <th class="text-right">توضیح تراکنش</th> <th class="text-right">وضعیت</th> <th class="text-right">تاریخ</th> <th class="text-right">زمان</th> </tr></thead> <tbody id="cartbody2">';
            for (var i = 0; i < data.result.length; i++) {
                var obj = data.result[i];
                tbl += '<tr>';
                tbl += '<td>' + obj.id + '</td>';
                if (obj.amount > 0) {
                    tbl += '<td class="text-success">' + obj.amount.toLocaleString() + ' ریال</td>';
                    tbl += '<td class="text-success">واریز مبلغ - افزایش موجودی</td>';
                } else {
                    tbl += '<td class="text-danger">' + obj.amount.toLocaleString() + ' ریال</td>';
                    tbl += '<td class="text-danger">کسر مبلغ - بابت رزرو</td>';
                }
                tbl += '<td class="text-right" dir="ltr">' + obj.description + '</td>';

                if (obj.consider == false || obj.active == false) {
                    tbl += '<td class="text-danger">ناموفق - لغو شده - بررسی نشده</td>';
                } else {
                    tbl += '<td class="text-success">موفق - حساب شده در موجودی</td>';
                }
                tbl += '<td class="text-right" dir="ltr">' + obj.date + '</td>';
                tbl += '<td class="text-right" dir="ltr">' + obj.time + '</td>';
                tbl += '</tr>';
            }
            tbl += '</tbody> </table>';

            var dialog = bootbox.dialog({
                className: 'my-modal',
                message: tbl,
                closeButton: true,
                size: 'large'
            });

            $("#divloader").hide();
        }
    });
}

function GoToBank() {
    //if (online == true) {
    //    bootbox.alert("متاسفانه پرداخت آنلاین در حال حاظر مقدور نیست");
    //    return;
    //}
    if ($("#pamount").val() == "") {
        bootbox.alert("لطفا مقادیر ورودی را کنترل کنید");
        return;
    }
    if ($("#pamount").val() < 1000) {
        bootbox.alert("کمتر مقدار هزار ریال میباشد");
        return;
    }
    $("#divloader").show();

    var fdata = new FormData();
    fdata.append("__RequestVerificationToken", gettoken());

    fdata.append("pamount", $("#pamount").val());
    fdata.append("pinfo", $("#pinfo").val());
    $.ajax({
        type: 'post',
        url: HostUrl + "/iUser/GoToBank",
        data: fdata,
        processData: false,
        contentType: false
    }).done(function (result) {
        if (result.result == 1) {
            bootbox.hideAll();
            $("#divloader").hide();
            var dialog = bootbox.dialog({
                message: "<p>لطفا درگاه پرداخت را انتخاب کنید</p>",
                buttons: {
                    cancel: {
                        label: "<img src=\"../mellat.png\" style=\"max-width: 100px;\">",
                        className: 'btn-white',
                        callback: function () {
                            window.location = result.paylink2;
                        }
                    },
                    noclose: {
                        label: "<img src=\"../zarinpal.png\" style=\"max-width: 100px;\">",
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

}