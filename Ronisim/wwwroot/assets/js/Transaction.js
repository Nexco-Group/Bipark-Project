var foo6;


$(document).ready(function () {
    foo6 = FooTable.init('#foo6');
});



function fitertime2() {
    $('#divloader').show();
    $.getJSON(HostUrl + "/iUser/getAllmoney", {
        fromdate: $("#fromDate2").val(),
        todate: $("#toDate2").val(),
        status: $("#StatusSelect2").val()
    }, function (data) {
        var a = data.result;
        if (a == 0) {
            bootbox.hideAll();
            bootbox.alert("هیچ تراکنشی وجود ندارد");
            $("#divloader").hide();
        } else if (a == 1) {
            bootbox.hideAll();
            bootbox.alert("لطفا در انتخاب تاریخ دقت کنید");
            $("#divloader").hide();
        } else {
            $("#foo6>tbody>tr").each(function (index, elem) {
                $(elem).remove();
            });
            foo6.rows.load(a);
            var money = 0;
            var out = 0;
            var inc = 0;
            $.each(foo6.rows.all, function (i, row) {
                v = row.val();
                v.money = v.amount;
                money += parseInt(v.amount);
                if (v.amount > 0) {
                    inc += parseInt(v.amount);
                    v.amount = '<span class="text-success">' + parseInt(v.amount).toLocaleString() + ' ریال</span>';
                    v.otype = '<span class="text-success">واریز مبلغ - افزایش موجودی</span>';
                } else {
                    out += parseInt(v.amount);
                    v.amount = '<span class="text-danger">' + (parseInt(v.amount).toLocaleString()) + ' ریال</span>';
                    v.otype = '<span class="text-danger">کسر مبلغ - بابت رزرو</span>';
                }
                if (v.consider === "false" || v.active === "false") {
                    v.vaz = '<span class="text-danger">ناموفق - لغو شده - بررسی نشده</span>';
                }
                else {
                    v.vaz = '<span class="text-success">موفق - حساب شده در موجودی</span>';
                }
                row.val(v);
            });

            $("#foo6o").html('<td>ورودی: ' + inc.toLocaleString() + ' ریال</td><td>خروجی: ' + (-out).toLocaleString() + ' ریال</td><td>کل مبلغ: ' + money.toLocaleString() + ' ریال</td>');

            //$("#foo6").find("input[placeholder='Search']").attr('onchange', 'changesearch2(this)').attr('onpaste', 'changesearch2(this)').attr('onkeyup', 'changesearch2(this)');


            foo6.gotoPage(1);
            $("#divloader").hide();
        }
    });
}

function fitertimeexcel2() {
    $('#divloader').show();
    $.getJSON(HostUrl + "/iUser/getAllmoneyExcel", {
        fromdate: $("#fromDate2").val(),
        todate: $("#toDate2").val(),
        status: $("#StatusSelect2").val()
    }, function (data) {
        var a = data.result;
        if (a == 0) {
            bootbox.hideAll();
            bootbox.alert("هیچ تراکنشی وجود ندارد");
            $("#divloader").hide();
        } else if (a == 1) {
            bootbox.hideAll();
            bootbox.alert("لطفا در انتخاب تاریخ دقت کنید");
            $("#divloader").hide();
        } else {
            window.location.href = a;
            //window.open("http://www.google.com", '_blank');
            //window.open(a, '_blank');
            $("#divloader").hide();
        }
    });
}

function fitertimepdf2() {
    $('#divloader').show();
    $.getJSON(HostUrl + "/iUser/getAllmoneyPDF", {
        fromdate: $("#fromDate2").val(),
        todate: $("#toDate2").val(),
        status: $("#StatusSelect2").val()
    }, function (data) {
        var a = data.result;
        if (a == 0) {
            bootbox.hideAll();
            bootbox.alert("هیچ تراکنشی وجود ندارد");
            $("#divloader").hide();
        } else if (a == 1) {
            bootbox.hideAll();
            bootbox.alert("لطفا در انتخاب تاریخ دقت کنید");
            $("#divloader").hide();
        } else {
            window.location.href = a;
            //window.open("http://www.google.com", '_blank');
            //window.open(a, '_blank');
            $("#divloader").hide();
        }
    });
}