$('.dropify').dropify();
$('#divloader').hide();
var foo1;

var foo4;
var foo5;

var foo6;
var foo7;
var foo8;
var foo9;
var foo10;



(function ($) {
    $.extend({
        playSound: function () {
            return $(
                '<audio class="sound-player" autoplay="autoplay" style="display:none;">'
                + '<source src="' + arguments[0] + '" />'
                + '<embed src="' + arguments[0] + '" hidden="true" autostart="true" loop="false"/>'
                + '</audio>'
            ).appendTo('body');
        },
        stopSound: function () {
            $(".sound-player").remove();
        }
    });
})(jQuery);

$("#savestd").on('click', function () {
    bootbox.confirm({
        message: "آیا از تغییر اطلاعات این کاربر اطمینان دارید؟",
        buttons: {
            confirm: {
                label: 'بله',
                className: 'btn-success'
            },
            cancel: {
                label: 'خیر-بازگشت',
                className: 'btn-danger'
            }
        },
        callback: function (result) {
            if (result == true) {
                $("#divloader").show();

                var fdata = new FormData();
                fdata.append("Id", $('#mrid').val());
                fdata.append("rfname", $('#rfname').val());
                fdata.append("rlname", $('#rlname').val());
                fdata.append("rmobile", $('#rmobile').val());
                fdata.append("rcode", $('#rcode').val());
                fdata.append("rpass", $('#rpass').val());
                fdata.append("__RequestVerificationToken", gettoken());
                $.ajax({
                    type: 'post',
                    url: HostUrl + "/Management/EditStdInfo",
                    data: fdata,
                    processData: false,
                    contentType: false
                }).done(function (result) {
                    $("#divloader").hide();
                    if (result.result == "1") {
                        bootbox.alert("تغییرات با موفقیت اعمال شد");
                        $("#editor-modal").modal('hide');
                        //row.val(values);
                        renderfoo10();
                    } else if (result.result == "2") {
                        bootbox.alert("تغییرات با موفقیت اعمال شد - رمز کاربر با موفقیت تغییر کرد");
                        $("#editor-modal").modal('hide');
                        //row.val(values);
                        renderfoo10();
                    }
                    else {
                        bootbox.alert(result.result);
                    }
                    $("#divloader").hide();
                });
            } else {
                $("#editor-modal").modal('hide');
            }
        }
    });
});

setTimeout(function () {
    setInterval(function () { refreshnewmes(); }, 30000);
}, 30000);

function refreshnewmes() {
    $.getJSON(HostUrl + "/Management/GetNotification", {
        fid: 1
    }, function (data) {
        if (data.count2 != 0) {
            ialert("توجه: " + data.count2 + " کاربر تایید نشده وجود دارد  ");
            if (data.count1 == 0) {
                $.playSound(HostUrl + "/alert.mp3");
            }
        }
    });
}

(function (document, window, $) {
    'use strict';
    var Site = window.Site;
    $(document).ready(function ($) {
        Site.run();
    });
})(document, window, jQuery);

$(document).ready(function () {
    $("#divloader").hide();

    foo1 = FooTable.init('#foo1');
    foo2 = FooTable.init('#foo2');
    //foo4 = FooTable.init('#foo4');
    //foo5 = FooTable.init('#foo5');

    foo6 = FooTable.init('#foo6');
    foo7 = FooTable.init('#foo7');
    //foo8 = FooTable.init('#foo8');
    foo10 = FooTable.init('#foo10');
    $("#divloader").hide();
    $('#tab0click').click();
    setTimeout(function () {
        refreshnewmes();
    }, 5000);
});

var open = false;

$(document).on('click', '.dropdown-toggle', function () {
    if (open == false) {
        $(".dropdown-menu-right").show();
        open = true;
    } else {
        $(".dropdown-menu-right").hide();
        open = false;
    }
});


$("a[href='#tab1']").click(function () {
    renderfoo1();
});

$("a[href='#tab2']").click(function () {
    renderfoo2();
});

$("a[href='#tab6']").click(function () {
    //renderfoo6();
});

$("a[href='#tab7']").click(function () {
    //renderfoo7();
});

$("a[href='#tab10']").click(function () {
    renderfoo10();
});

//$("a[href='#tab14']").click(function () {
//    renderfoo14();
//});
//----------14-----------
function renderfoo14() {
    $('#divloader').show();
    $.getJSON(HostUrl + "/Management/getadminDet", {
    }, function (data) {
        var tbl = '<table class="table table-hover" dir="rtl"> <thead> <tr> <th class="text-right">فروش کل</th> <th class="text-right">فروش یک ماه اخیر</th> <th class="text-right">فروش روز اخیر</th> </tr></thead> <tbody>';
        tbl += '<tr> <td class="text-right">' + data.a1.toLocaleString() + '</td><td class="text-right" dir="ltr">' + data.a3.toLocaleString() + '</td><td class="text-right">' + data.a5.toLocaleString() + '</td>';
        tbl += '</tbody> </table>';

        tbl += '<hr/><table class="table table-hover" dir="rtl"> <thead> <tr> <th class="text-right">سود کل</th> <th class="text-right">سود یک ماه اخیر</th> <th class="text-right">سود روز اخیر</th> </tr></thead> <tbody>';
        tbl += '<tr> <td class="text-right">' + data.a2.toLocaleString() + '</td><td class="text-right">' + data.a4.toLocaleString() + '</td><td class="text-right">' + data.a7.toLocaleString() + '</td>';
        tbl += '</tbody> </table>';

        tbl += '<hr/><table class="table table-hover" dir="rtl"> <thead> <tr> <th class="text-right">کل حجم فروخته شده - گیگ</th><th class="text-right">حجم فروخته شده یک ماه اخیر - گیگ</th><th class="text-right">حجم فروخته شده روز اخیر - گیگ</th></tr></thead> <tbody>';
        tbl += '<tr> <td class="text-right">' + data.a9.toLocaleString() + '</td><td class="text-right">' + data.a11.toLocaleString() + '</td><td class="text-center">' + data.a10.toLocaleString() + '</td>';
        tbl += '</tbody> </table>';


        $("#admindet").html(tbl);
        $('#divloader').hide();

    });
}

//----------1-----------
function renderfoo1() {
    $('#divloader').show();
    $.getJSON(HostUrl + "/Management/getPRequest", {
    }, function (data) {
        var a = data.result;
        $("#foo1>tbody>tr").each(function (index, elem) {
            $(elem).remove();
        });
        if (a == 0) {
            bootbox.hideAll();
            bootbox.alert("هیچ درخواستی وجود ندارد");
            $("#divloader").hide();
        } else {
            foo1.rows.load(a);

            $.each(foo1.rows.all, function (i, row) {
                v = row.val();
                var m = v.id;
                v.active = '<button data-whochange="' + m + '"  data-foo="1" data-nowactive="1" onclick="changerqstatus(this)" class="badge badge-table badge-success btn-round btn-xs">تایید کردن</button> | <button data-whochange="' + m + '" data-foo="1" data-nowactive="0" onclick="changerqstatus(this)" class="badge badge-table badge-danger btn-round btn-xs">لغو کردن</button>';

                row.val(v);
            });
            foo1.gotoPage(1);
            $("#divloader").hide();
        }
    });
}

//----------2-----------
function renderfoo2() {
    $('#divloader').show();
    $.getJSON(HostUrl + "/Management/getProduct", {
    }, function (data) {
        var a = data.result;
        if (a == 0) {
            bootbox.hideAll();
            bootbox.alert("جای پارک ای وجود ندارد");
            $("#divloader").hide();
        } else {
            $("#foo2>tbody>tr").each(function (index, elem) {
                $(elem).remove();
            });
            foo2.rows.load(a);

            $.each(foo2.rows.all, function (i, row) {
                v = row.val();
                var m = v.id;
                v.price1 = parseInt(v.price1).toLocaleString();
                v.price2 = parseInt(v.price2).toLocaleString();

                v.delete = '<button data-whochange="' + m + '" onclick="deletep(this)" class="badge badge-table badge-danger">حذف</button>';

                v.edit = '<button data-whochange="' + m + '" onclick="editp(this)" class="badge badge-table badge-info">تغییر اطلاعات این جای پارک</button>';

                if (v.active == "true") {
                    v.active = '<button data-whochange="' + m + '" data-nowactive="true" onclick="changepstatus(this)" class="badge badge-table badge-success">فعال</button>';
                }
                else {
                    v.active = '<button data-whochange="' + m + '" data-nowactive="false" onclick="changepstatus(this)" class="badge badge-table badge-warning">غیر فعال</button>';
                }
                row.val(v);
            });
            foo2.gotoPage(1);
            $("#divloader").hide();
        }
    });
}


//----------6-----------
function renderfoo6() {
    $('#divloader').show();
    $.getJSON(HostUrl + "/Management/getAllmoney", {
    }, function (data) {
        var a = data.result;
        if (a == 0) {
            bootbox.hideAll();
            bootbox.alert("هیچ تراکنشی وجود ندارد");
            $("#divloader").hide();
        } else {
            $("#foo6>tbody>tr").each(function (index, elem) {
                $(elem).remove();
            });
            foo6.rows.load(a);
            var moneyout = 0;
            var moneyin = 0;
            $.each(foo6.rows.all, function (i, row) {
                v = row.val();
                var hh = v.who.split('|');
                v.money = v.amount;
                var sss = parseInt(v.amount);
                if (sss >= 0) {
                    moneyin += sss;
                } else {
                    moneyout += sss;
                }
                v.who = hh[0];
                if (v.amount > 0) {
                    v.amount = '<span class="text-success">' + hh[1] + ' ریال</span>';
                    v.otype = '<span class="text-success">واریز مبلغ - افزایش موجودی</span>';
                } else {
                    v.amount = '<span class="text-danger">' + hh[1] + ' ریال</span>';
                    v.otype = '<span class="text-danger">کسر مبلغ - بابت رزرو</span>';
                }
                row.val(v);
            });

            $("#foo6o").html('<td>کل خرج شده: ' + (moneyout).toLocaleString() + ' ریال</td><td>کل واریز شده: ' + (moneyin).toLocaleString() + ' ریال</td><td>جمع مبلغ: ' + (moneyout + moneyin).toLocaleString() + ' ریال</td>');

            $("#foo6").find("input[placeholder='Search']").attr('onchange', 'changesearch2()').attr('onpaste', 'changesearch2()').attr('onkeyup', 'changesearch2()');


            foo6.gotoPage(1);
            $("#divloader").hide();
        }
    });
}

function changesearch2() {
    var money = 0;
    setTimeout(function () {
        $("#divloader").show();
        setTimeout(function () {
            $('#foo6 > tbody  > tr').each(function () {
                if ($(this).find("td:nth-child(9)") != null) {
                    money += parseInt($(this).find("td:nth-child(9)").html());
                }
            });

            $("#foo6o").html('<td>کل مبلغ: ' + money.toLocaleString() + ' ریال</td>');
            $("#divloader").hide();
        }, 1500);
    }, 5500);
}
//function changesearchnew2() {
//    var money1 = 0;
//    var gig1 = 0;
//    $("#divloader").show();
//    setTimeout(function () {
//        $('#foo7 > tbody  > tr').each(function () {
//            if ($(this).find("td:nth-child(11)") != null) {
//                gig1 += parseFloat($(this).find("td:nth-child(11)").html());
//                money1 += parseInt($(this).find("td:nth-child(12)").html());
//            }
//        });

//        $("#foo7o").html('<td>کل فروش: ' + money1.toLocaleString() + ' ریال</td><td>کل حجم فروخته شده: ' + gig1 + ' گیگابایت</td>');
//        $("#divloader").hide();
//    }, 1500);
//}

function fitertime2() {
    $('#divloader').show();
    $.getJSON(HostUrl + "/Management/getAllmoney", {
        fromdate: $("#fromDate2").val(),
        todate: $("#toDate2").val(),
        status: $("#StatusSelect2").val(),
        whoid: $("#ResellerSelect2").val(),
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
                var hh = v.who.split('|');
                v.money = v.amount;
                money += parseInt(v.amount);
                v.who = hh[0];
                if (v.amount > 0) {
                    inc += parseInt(v.amount);
                    v.amount = '<span class="text-success">' + hh[1] + ' ریال</span>';
                    v.otype = '<span class="text-success">واریز مبلغ - افزایش موجودی</span>';
                } else {
                    out += parseInt(v.amount);
                    v.amount = '<span class="text-danger">' + hh[1] + ' ریال</span>';
                    v.otype = '<span class="text-danger">کسر مبلغ - بابت رزرو</span>';
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
    $.getJSON(HostUrl + "/Management/getAllmoneyExcel", {
        fromdate: $("#fromDate2").val(),
        todate: $("#toDate2").val(),
        status: $("#StatusSelect2").val(),
        whoid: $("#ResellerSelect2").val(),
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
    $.getJSON(HostUrl + "/Management/getAllmoneyPDF", {
        fromdate: $("#fromDate2").val(),
        todate: $("#toDate2").val(),
        status: $("#StatusSelect2").val(),
        whoid: $("#ResellerSelect2").val(),
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

//----------7-----------
function renderfoo7() {
    $('#divloader').show();
    $.getJSON(HostUrl + "/Management/getRequest", {
    }, function (data) {
        var a = data.result;
        if (a == 0) {
            bootbox.hideAll();
            bootbox.alert("هیچ درخواستی وجود ندارد");
            $("#divloader").hide();
        } else {
            $("#foo7>tbody>tr").each(function (index, elem) {
                $(elem).remove();
            });
            foo7.rows.load(a);
            var gig = 0;
            var money = 0;
            var money2 = 0;
            var sood = 0;
            $.each(foo7.rows.all, function (i, row) {
                v = row.val();
                var m = v.id;

                gig += parseFloat(v.gig);
                money += parseInt(v.money);
                money2 += parseInt(v.money2);
                sood += (parseInt(v.money2) - parseInt(v.money));

                v.sood = (parseInt(v.money2) - parseInt(v.money)).toLocaleString();
                v.money = parseInt(v.money).toLocaleString();
                v.money2 = parseInt(v.money2).toLocaleString();

                if (v.st == 4) {
                    v.active = '<button data-whochange="' + m + '" data-foo="7" data-nowactive="1" onclick="changerqstatus(this)" class="badge badge-table badge-success btn-round btn-xs">تایید کردن</button>';
                } else if (v.st == 2) {
                    v.active = '<button data-whochange="' + m + '" data-foo="7" data-nowactive="0" onclick="changerqstatus(this)" class="badge badge-table badge-danger btn-round btn-xs">لغو کردن</button>';
                } else {
                    v.active = '<button data-whochange="' + m + '" data-foo="7" data-nowactive="1" onclick="changerqstatus(this)" class="badge badge-table badge-success btn-round btn-xs">تایید کردن</button> | <button data-whochange="' + m + '" data-foo="7" data-nowactive="0" onclick="changerqstatus(this)" class="badge badge-table badge-danger btn-round btn-xs">لغو کردن</button>';
                }
                v.money = parseInt(v.money).toLocaleString() + " ریال";
                row.val(v);
            });
            $("#foo7o").html('<td>کل فروش: ' + money.toLocaleString() + ' ریال</td><td>کل فروش - قیمت مصرف کننده: ' + money2.toLocaleString() + ' ریال</td><td>کل سود: ' + sood.toLocaleString() + ' ریال</td><td>کل حجم فروخته شده: ' + gig + ' گیگابایت</td>');

            //$("#foo7o").html('<td>کل فروش: ' + money.toLocaleString() + ' ریال</td><td>کل حجم فروخته شده: ' + gig + ' گیگابایت</td>');

            $("#foo7").find("input[placeholder='Search']").attr('onchange', 'changesearch()').attr('onpaste', 'changesearch()').attr('onkeyup', 'changesearch()');

            foo7.gotoPage(1);
            $("#divloader").hide();
        }
    });
}


function changesearch() {
    var gig = 0;
    var money = 0;
    setTimeout(function () {
        $("#divloader").show();
        setTimeout(function () {
            $('#foo7 > tbody  > tr').each(function () {
                if ($(this).find("td:nth-child(11)") != null) {
                    gig += parseFloat($(this).find("td:nth-child(11)").html());
                    money += parseInt($(this).find("td:nth-child(12)").html());
                }
            });

            $("#foo7o").html('<td>کل فروش: ' + money.toLocaleString() + ' ریال</td><td>کل حجم فروخته شده: ' + gig + ' گیگابایت</td>');
            $("#divloader").hide();
        }, 1500);

    }, 2500);
    //if ($(el).val() == null || $(el).val() == "") {
    //    renderfoo7();
    //} else {
    //    $.each(foo7.rows.all, function (i, row) {
    //        v = row.val();
    //        gig += parseInt(v.gig);
    //        money += parseInt(v.money);
    //    });
    //    $("#foo7o").html('<td>کل فروش: ' + money.toLocaleString() + ' ریال</td><td>کل حجم فروخته شده: ' + gig + ' گیگابایت</td>');
    //}
}

function fitertime() {
    $('#divloader').show();
    $.getJSON(HostUrl + "/Management/getRequest", {
        fromdate: $("#fromDate1").val(),
        todate: $("#toDate1").val(),
        status: $("#StatusSelect").val(),
        formobile: $("#formobile").val(),
        size: $("#Psize2").val(),
        sizetype: $("#SizeType2").val(),
        whoid: $("#ResellerSelect").val(),
    }, function (data) {
        var a = data.result;
        if (a == 0) {
            bootbox.hideAll();
            bootbox.alert("هیچ درخواستی وجود ندارد");
            $("#divloader").hide();
        } else if (a == 1) {
            bootbox.hideAll();
            bootbox.alert("لطفا در انتخاب تاریخ دقت کنید");
            $("#divloader").hide();
        } else {
            $("#foo7>tbody>tr").each(function (index, elem) {
                $(elem).remove();
            });
            foo7.rows.load(a);
            var gig = 0;
            var money = 0;
            var money2 = 0;
            var sood = 0;
            $.each(foo7.rows.all, function (i, row) {
                v = row.val();
                var m = v.id;

                gig += parseFloat(v.gig);
                money += parseInt(v.money);
                money2 += parseInt(v.money2);
                sood += (parseInt(v.money2) - parseInt(v.money));

                v.sood = (parseInt(v.money2) - parseInt(v.money)).toLocaleString();
                v.money = parseInt(v.money).toLocaleString();
                v.money2 = parseInt(v.money2).toLocaleString();

                if (v.st == 4) {
                    v.active = '<button data-whochange="' + m + '" data-foo="7" data-nowactive="1" onclick="changerqstatus(this)" class="badge badge-table badge-success btn-round btn-xs">تایید کردن</button>';
                } else if (v.st == 2) {
                    v.active = '<button data-whochange="' + m + '" data-foo="7" data-nowactive="0" onclick="changerqstatus(this)" class="badge badge-table badge-danger btn-round btn-xs">لغو کردن</button>';
                } else {
                    v.active = '<button data-whochange="' + m + '" data-foo="7" data-nowactive="1" onclick="changerqstatus(this)" class="badge badge-table badge-success btn-round btn-xs">تایید کردن</button> | <button data-whochange="' + m + '" data-foo="7" data-nowactive="0" onclick="changerqstatus(this)" class="badge badge-table badge-danger btn-round btn-xs">لغو کردن</button>';
                }
                row.val(v);
            });

            $("#foo7o").html('<td>کل فروش: ' + money.toLocaleString() + ' ریال</td><td>کل فروش - قیمت مصرف کننده: ' + money2.toLocaleString() + ' ریال</td><td>کل سود: ' + sood.toLocaleString() + ' ریال</td><td>کل حجم فروخته شده: ' + gig + ' گیگابایت</td>');

            //$("#foo7o").html('<td>کل فروش: ' + money.toLocaleString() + ' ریال</td><td>کل حجم فروخته شده: ' + gig + ' گیگابایت</td>');

            foo7.gotoPage(1);
            $("#divloader").hide();
        }
    });
}

function fitertimeexcel() {
    $('#divloader').show();
    $.getJSON(HostUrl + "/Management/getRequestExcel", {
        fromdate: $("#fromDate1").val(),
        todate: $("#toDate1").val(),
        status: $("#StatusSelect").val(),
        formobile: $("#formobile").val(),
        size: $("#Psize2").val(),
        sizetype: $("#SizeType2").val(),
        whoid: $("#ResellerSelect").val(),
    }, function (data) {
        var a = data.result;
        if (a == 0) {
            bootbox.hideAll();
            bootbox.alert("هیچ درخواستی وجود ندارد");
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

function fitertimepdf() {
    $('#divloader').show();
    $.getJSON(HostUrl + "/Management/getRequestPDF", {
        fromdate: $("#fromDate1").val(),
        todate: $("#toDate1").val(),
        status: $("#StatusSelect").val(),
        formobile: $("#formobile").val(),
        size: $("#Psize2").val(),
        sizetype: $("#SizeType2").val(),
        whoid: $("#ResellerSelect").val(),
    }, function (data) {
        var a = data.result;
        if (a == 0) {
            bootbox.hideAll();
            bootbox.alert("هیچ درخواستی وجود ندارد");
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


//----------new---------

function fitertimeexcel3() {
    $('#divloader').show();
    $.getJSON(HostUrl + "/Management/getPRequestExcel", {

    }, function (data) {
        var a = data.result;
        if (a == 0) {
            bootbox.hideAll();
            bootbox.alert("هیچ درخواستی وجود ندارد");
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


function fitertimepdf3() {
    $('#divloader').show();
    $.getJSON(HostUrl + "/Management/getPRequestPDF", {
    }, function (data) {
        var a = data.result;
        if (a == 0) {
            bootbox.hideAll();
            bootbox.alert("هیچ درخواستی وجود ندارد");
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


//----------10-----------
function renderfoo10() {
    $('#divloader').show();
    $.getJSON(HostUrl + "/Management/getRSLS", {
    }, function (data) {
        var a = data.result;
        if (a == 0) {
            bootbox.hideAll();
            bootbox.alert("هیچ کاربر ای وجود ندارد");
            $("#divloader").hide();
        } else {
            $("#foo10>tbody>tr").each(function (index, elem) {
                $(elem).remove();
            });
            foo10.rows.load(a);

            $.each(foo10.rows.all, function (i, row) {
                v = row.val();
                var m = v.id;
                if (v.verify == "true") {
                    v.verify = '<b class="text-success">این کاربر توسط شما تایید شده است</b>';
                } else {
                    v.verify = '<button data-whochange="' + m + '" onclick="verifytch(this)" class="badge badge-table badge-success">تایید کردن این کاربر</button>';
                }
                if (v.active == "true") {
                    v.active = '<button data-whochange="' + m + '" data-nowactive="true" onclick="changerslstatus(this)" class="badge badge-table badge-success">فعال</button>';
                }
                else {
                    v.active = '<button data-whochange="' + m + '" data-nowactive="false" onclick="changerslstatus(this)" class="badge badge-table badge-warning">غیر فعال</button>';
                }
                v.summery = '<button data-whochange="' + m + '" onclick="showsum(this)" class="badge badge-table badge-default">فعالیت</button>';
                v.money = '<button data-whochange="' + m + '" onclick="showmoney(this)" class="badge badge-table badge-default">تراکنش های مالی</button>';
                v.addmoney = '<button data-whochange="' + m + '"  data-sch="' + v.fname + '" onclick="icbalance(this)" class="badge badge-table badge-default">تغییر موجودی</button>';

                v.delete = '<button data-whochange="' + m + '" onclick="deleter(this)" class="badge badge-table badge-danger">حذف</button>';

                v.edit = '<button data-whochange="' + m + '" onclick="editr(this)" class="badge badge-table badge-info">تغییر اطلاعات این کاربر</button>';

                row.val(v);
            });
            foo10.gotoPage(1);
            $("#divloader").hide();
        }
    });
}

function changerslstatus(element) {
    var myelm = $(element);
    console.log(myelm.data("whochange"));
    var activv = myelm.data("nowactive");
    var mmm = "";
    if (activv) {
        mmm = "آیا از غیر فعال کردن این کاربر اطمینان دارید؟";
    } else {
        mmm = "آیا از فعال کردن این کاربر اطمینان دارید؟";
    }
    bootbox.confirm({
        message: mmm,
        buttons: {
            confirm: {
                label: 'بله',
                className: 'btn-success'
            },
            cancel: {
                label: 'خیر - بازگشت',
                className: 'btn-danger'
            }
        },
        callback: function (result) {
            $("#divloader").show();
            if (result == true) {
                var fdata = new FormData();
                fdata.append("__RequestVerificationToken", gettoken());
                fdata.append("nowst", activv);
                fdata.append("who", myelm.data("whochange"));

                $.ajax({
                    type: 'post',
                    url: HostUrl + "/Management/Changrslstatus",
                    data: fdata,
                    processData: false,
                    contentType: false
                }).done(function (result) {

                    bootbox.alert(result.result);
                    renderfoo10();
                    $("#divloader").hide();
                });
            } else {
                $("#divloader").hide();
            }
        }
    });
}

function icbalance(element) {
    var myelm = $(element);

    var dialog = bootbox.dialog({
        className: 'my-modal',
        message: '<h5 class="text-center font-weight-bold">تغییر موجودی برای ' + myelm.data("sch") + '</h5><hr/><p class="text-center">توجه داشته باشید عدد وارد شده به ریال میباشد و میتواند مثبت یا منفی باشد. در صورت مثبت بودن به موجودی کاربر اضافه میشود و در صورت منفی بود از موجودی وی کسر خواهد شد (باید چک باکس منفی بودن را تیک بزنید) - در قسمت توضیحات حتما علت ذکر شود</p><div class="text-center col-lg-5" style="display: block; margin: 0 auto;"><div class="text-center" style="display: block; margin: 0 auto;"><div style=" direction: rtl;"><input type="checkbox" id="manfie" value="Bike"><span style=" margin-right: 5px;">مبلغ منفی - از موجودی کسر شود<span></span></span></div></div><input placeholder="مبلغ - به ریال" type="text" class="form-control text-center" style="margin-bottom: 10px" id="inum2" lang="pnum"><span style="font-weight: normal;" id="inum2o"></span><input placeholder="توضیحات" type="text" class="form-control text-center" style="margin-bottom: 10px;margin-top: 5px;" id="ides2"><button type="button" style="margin-bottom: 10px" onclick="addbalance(this)" data-whochange="' + myelm.data("whochange") + '" class="btn btn-primary btn-block"><b>ثبت</b></button></div>',
        closeButton: true
    });
}

function addbalance(element) {
    var myelm = $(element);
    $("#divloader").show();
    if ($("#inum2").val() == "") {
        bootbox.alert("لطفا مقادیر ورودی را کنترل کنید");
        return;
    }
    var money = $("#inum2").val();

    var money1 = parseInt(money.replace(/,/g, ''))
    if ($("#manfie").is(':checked')) {
        money1 = -1 * money1;
    }
    var fdata = new FormData();
    fdata.append("__RequestVerificationToken", gettoken());
    fdata.append("ides", $("#ides2").val());
    fdata.append("inum", money1);
    fdata.append("who", myelm.data("whochange"));

    $.ajax({
        type: 'post',
        url: HostUrl + "/Management/addbal",
        data: fdata,
        processData: false,
        contentType: false
    }).done(function (result) {
        if (result.result == 0) {
            bootbox.hideAll();
            bootbox.alert("مشکلی رخ داده است");
            $("#divloader").hide();
        } else {
            bootbox.hideAll();
            bootbox.alert(result.result);
            $("#divloader").hide();
        }
    });
}

function showmoney(element) {
    var myelm = $(element);

    $.getJSON(HostUrl + "/Management/getRmoney", {
        Oid: myelm.data("whochange")
    }, function (data) {
        var buyitems = data.result;
        if (buyitems == 0) {
            bootbox.hideAll();
            bootbox.alert("این کاربر هیچ تراکنشی ندارد");
            $("#divloader").hide();
        } else {
            var tbl = '<table class="table table-hover" dir="rtl"> <thead> <tr> <th class="text-right">ID</th> <th class="text-right">مبلغ</th> <th class="text-right">نوع تراکنش</th> <th class="text-right">توضیح تراکنش</th> <th class="text-right">تاریخ</th> <th>وضعیت</th> <th class="text-right">زمان</th> </tr></thead> <tbody id="cartbody2">';
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


function showsum(element) {
    var myelm = $(element);

    $.getJSON(HostUrl + "/Management/getRdet", {
        Oid: myelm.data("whochange")
    }, function (data) {
        var buyitems = data.result;
        if (buyitems == 0) {
            bootbox.hideAll();
            bootbox.alert("مشکلی پیش آمده است");
            $("#divloader").hide();
        } else {
            var tbl = '<table class="table table-hover" dir="rtl"> <thead> <tr> <th class="text-right">فروش کل</th> <th class="text-right">فروش یک ماه اخیر</th> <th class="text-right">فروش روز اخیر</th> <th class="text-right">سود کل</th> <th class="text-right">سود یک ماه اخیر</th> <th class="text-right">سود روز اخیر</th> <th class="text-right">کل حجم فروخته شده</th><th class="text-right">حجم فروخته شده یک ماه اخیر</th><th class="text-right">حجم فروخته شده روز اخیر</th> <th class="text-right">موجودی</th> <th class="text-center">تاریخ آخرین رزرو</th> </tr></thead> <tbody id="cartbody2">';
            tbl += '<tr> <td class="text-right">' + data.a1.toLocaleString() + '</td><td class="text-right" dir="ltr">' + data.a3.toLocaleString() + '</td><td class="text-right">' + data.a7.toLocaleString() + '</td><td class="text-right">' + data.a2.toLocaleString() + '</td><td class="text-right">' + data.a4.toLocaleString() + '</td><td class="text-right">' + data.a8.toLocaleString() + '</td><td class="text-right">' + data.a9.toLocaleString() + '</td><td class="text-right">' + data.a11.toLocaleString() + '</td><td class="text-center">' + data.a10.toLocaleString() + '</td><td class="text-center">' + data.a5.toLocaleString() + '</td><td class="text-center">' + data.a6 + '</td></tr>';
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


function exportGrid(gridID, filename) {
    var html = $('#' + gridID).html();
    var a = document.createElement('a');
    a.id = 'tempLink';
    a.href = 'data:application/vnd.ms-excel,' + html;
    a.download = filename + ".xls";
    document.body.appendChild(a);
    a.click(); // Downloads the excel document
    document.getElementById('tempLink').remove();
}

function exportpdf(elm) {
    //exportPDF2('#foo' + $(elm).data('what'));
    $('#foo' + $(elm).data('what')).printThis({
        base: "https://ronisim.ir",
        debug: true,
        importCSS: true,        // import parent page css
        importStyle: true,     // import style tags
        printContainer: true,   // print outer container/$.selector
        pageTitle: "ronisim export",          // add title to print page
        printDelay: 50,        // variable print delay
        formValues: false,       // preserve input/form values
        canvas: false,          // copy canvas content (experimental)
        base: false            // preserve the BASE tag, or accept a string for the URL
    });
}


//function exportPDF2(elm) {
//    var csv = JSON.parse(JSON.stringify(FooTable.get(elm).toJSON(true)).replace(/<\/?[^>]+(>|$)/g, ""));
//    console.log(csv);
//    var columnss = [];
//    var rowss = [];

//    for (var a = 0; a < csv.columns.length; a++) {
//        var obj = csv.columns[a];
//        columnss.push(obj.title);
//    }
//    console.log(columnss);

//    //var columns2 = [];
//    //for (var a1 = 0; a1 < columns.length; a1++) {
//    //    var obj21 = columns[a1];
//    //    columns2.push(obj21);
//    //}
//    //console.log(columns2);

//    for (var b = 0; b < csv.rows.length; b++) {
//        var obj2 = csv.rows[b];


//        var result = '';
//        console.log(obj2);

//        var co = 0;
//        for (x in obj2) {
//            result += (obj2[x]) + ',';
//            co = co + 1;
//        }
//        var sa = result.split(',');
//        console.log(result);
//        console.log(sa);

//        rowss.push(sa);
//    }
//    console.log(rowss);


//    //var rows2 = [];
//    //for (var a2 = 0; a2 < rows.length; a2++) {
//    //    var obj22 = rows[a2];
//    //    rows2.push(obj22);
//    //}
//    //console.log(rows2);

//    var nep = [];
//    nep.push(columnss);
//    for (var c = 0; c < rowss.length; c++) {
//        nep.push(rowss[c]);
//    }

//    var dasas = JSON.parse(JSON.stringify(rowss));

//    console.log(dasas);
//    var docDefinition = {
//        content: [
//            {
//                layout: 'lightHorizontalLines', // optional
//                table: {
//                    // headers are automatically repeated if the table spans over multiple pages
//                    // you can declare how many rows should be treated as headers
//                    headerRows: 1,
//                    widths: ['auto', 'auto', 100, 'auto'],
//                    //pageSize: 'A0',//A0 is the largest A5 smallest(A0,A1,A2,A3,legal,A4,A5,letter))
//                    body: dasas
//                }
//            }
//        ]
//    };
//    console.log(columnss);

//    var sdas = "[
//    ['First', 'Second', 'Third', 'The last one'], ['Value 1', 'Value 2', 'Value 3', 'Value 4'] 
//    ]";

//    console.log(sdas);

//    var docDefinition = {
//        content: [
//            {
//                layout: 'lightHorizontalLines', // optional
//                table: {
//                    // headers are automatically repeated if the table spans over multiple pages
//                    // you can declare how many rows should be treated as headers
//                    headerRows: 1,
//                    widths: ['auto', 'auto', 100, 'auto'],
//                    //pageSize: 'A0',//A0 is the largest A5 smallest(A0,A1,A2,A3,legal,A4,A5,letter))
//                    body: JSON.parse(sdas)
//                }
//            }
//        ]
//    };
//    pdfMake.createPdf(docDefinition).download();

//    //console.log(columns);

//    //console.log(rows);+

//    //var printDoc = new jsPDF();
//    //printDoc.fromHTML($(elm).get(0), 10, 10, { 'width': 180 }, {lang: 'ar'});
//    //printDoc.save('table.pdf');

//    //var doc = new jsPDF('p', 'pt');
//    //doc.autoTable(columns, rows);
//    //doc.save('table.pdf');

//}


function exportimg(elm) {
    var node = document.getElementById('foo' + $(elm).data('what'));

    console.log('foo' + $(elm).data('what'));
    domtoimage.toPng(node)
        .then(function (dataUrl) {
            //var img = new Image();
            //img.src = dataUrl;
            ////document.appendChild(img);
            //window.document.body.appendChild(img);
            //img.click();

            var a = window.document.createElement('a');
            a.setAttribute('href', dataUrl);
            a.setAttribute('download', 'ronisim.png');
            window.document.body.appendChild(a);
            a.click();
        })
        .catch(function (error) {
            console.error('oops, something went wrong!', error);
        });
}

function exportexcel(elm) {
    var sa = '#foo' + $(elm).data('what');

    var csv = FooTable.get(sa).toCSV(false).replace(/<\/?[^>]+(>|$)/g, "");
    //var data = FooTable.get(sa).toJSON(false).replace(/<\/?[^>]+(>|$)/g, "");;


    console.log(csv);
    var filename = 'export.csv';

    var universalBOM = "\uFEFF";
    var a = window.document.createElement('a');
    a.setAttribute('href', 'data:text/csv; charset=utf-8,' + encodeURIComponent(universalBOM + csv));
    a.setAttribute('download', 'example.csv');
    window.document.body.appendChild(a);
    a.click();

    //var hiddenElement = document.createElement('a');
    //hiddenElement.href = 'data:text/csv;charset=utf-8,' + encodeURI(csv);
    //hiddenElement.target = '_blank';
    //hiddenElement.download = 'SDTB_DM_010_LICENSES_DONGLES_ACT.csv';
    //hiddenElement.click();
    //if (false && window.navigator.msSaveBlob) {

    //    var blob = new Blob([decodeURIComponent(csv)], {
    //        type: 'text/csv;charset=utf8'
    //    });

    //    // Crashes in IE 10, IE 11 and Microsoft Edge
    //    // See MS Edge Issue #10396033: https://goo.gl/AEiSjJ
    //    // Hence, the deliberate 'false'
    //    // This is here just for completeness
    //    // Remove the 'false' at your own risk
    //    window.navigator.msSaveBlob(blob, filename);

    //} else if (window.Blob && window.URL) {
    // HTML5 Blob        
    //var blob = new Blob([csv], { type: 'text/csv;charset=utf8' });
    //var csvUrl = URL.createObjectURL(blob);

    //$(this)
    //    .attr({
    //        'download': filename,
    //        'href': csvUrl
    //    });
    //} else {
    //    // Data URI
    //    var csvData = 'data:application/csv;charset=utf-8,' + encodeURIComponent(csv);

    //    $(this)
    //        .attr({
    //            'download': filename,
    //            'href': csvData,
    //            'target': '_blank'
    //        });
    //}
    //console.log ("csv: " + csv);
    //var hiddenElement = document.createElement('a');
    //hiddenElement.href = 'data:text/csv;charset=utf-8,' + encodeURI(csv);
    //hiddenElement.target = '_blank';
    //hiddenElement.download = 'SDTB_DM_010_LICENSES_DONGLES_ACT.csv';
    //hiddenElement.click();
}


$('a[href*="tab"]').on("click", function () {
    $('.exportpdf').hide();
    //$('#exportpdf').hide();
    var nowtab = $(this).attr("href").split("tab");

    $(".dropdown-menu-right").hide();
    open = false;



    for (var a = 1; a < 13; a++) {
        if (a != nowtab[1]) {
            $("#tab" + a).hide();
        } else {
            $("#tab" + a).show();
        }
    }

    if (nowtab[1] != "7" && nowtab[1] != "6" && nowtab[1] != "1") {
        setTimeout(function () {
            $('#foo' + nowtab[1]).find('.form-inline').append('<button type="button" onclick="exportpdf(this)" data-what="' + nowtab[1] + '" class="btn btn-info exportpdf" style="margin-right: 20px;">خروجی PDF</button><button type="button" style="margin-right: 20px; margin-lefts: 20px;" onclick="exportexcel(this)" data-what="' + nowtab[1] + '" class="btn btn-info exportpdf">خروجی EXCEL</button><button type="button" style="margin-right: 20px; margin-lefts: 20px;" onclick="exportimg(this)" data-what="' + nowtab[1] + '" class="btn btn-info exportpdf">خروجی عکس</button>');
        }, 500);
    }

});

$("#AddNewsbtn").click(function () {
    $("#divloader").show();
    var fdata = new FormData();
    fdata.append("__RequestVerificationToken", gettoken());
    fdata.append("Newstitle", $('#Newstitle').val());
    //fdata.append("Newsurl", $('#Newsurl').val());
    var fileInput = $('#Newsimage')[0];
    var file = fileInput.files[0];
    fdata.append("Newsimage", file);
    fdata.append("Resellerchecked", $('#schoolchecked').is(':checked'));
    fdata.append("Ruserchecked", $('#teacherchecked').is(':checked'));
    $.ajax({
        type: 'post',
        url: HostUrl + "/Management/AddNews",
        data: fdata,
        processData: false,
        contentType: false
    }).done(function (result) {
        if (result.result == 1) {
            bootbox.alert({
                className: 'my-modal',
                message: "اطلاعیه ثبت شد"
            });
            setTimeout(function () {
                location.reload();
            }, 2000);
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

$("#saveprd").click(function () {
    $("#divloader").show();
    var money = $("#Price1").val();
    var Price1 = parseInt(money.replace(/,/g, ''));

    var fdata = new FormData();
    fdata.append("__RequestVerificationToken", gettoken());
    fdata.append("Name", $('#Pname').val());
    fdata.append("Price1", Price1);
    fdata.append("Notes", $('#Pnote').val());
    $.ajax({
        type: 'post',
        url: HostUrl + "/Management/AddProduct",
        data: fdata,
        processData: false,
        contentType: false
    }).done(function (result) {
        if (result.result == 1) {
            bootbox.alert({
                className: 'my-modal',
                message: "جای پارک با موفقیت ثبت شد"
            });
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

function showmodal(elm) {
    $("#inwstext").val($(elm).data('ntext'));
    $("#inwsId").val($(elm).data('id'));
    if ($(elm).data('res') == "true" || $(elm).data('res') == "True") {
        $('#ischoolchecked').prop('checked', true);
    } else {
        $('#ischoolchecked').prop('checked', false);
    }

    if ($(elm).data('user') == "true" || $(elm).data('user') == "True") {
        $('#iteacherchecked').prop('checked', true);
    } else {
        $('#iteacherchecked').prop('checked', false);
    }

    $('#editor-modal2').modal('show');
}

$("#savenws").click(function () {
    $("#divloader").show();
    var fdata = new FormData();
    fdata.append("__RequestVerificationToken", gettoken());
    fdata.append("Id", $('#inwsId').val());
    fdata.append("Newstitle", $('#inwstext').val());
    fdata.append("schoolchecked", $('#ischoolchecked').is(':checked'));
    fdata.append("teacherchecked", $('#iteacherchecked').is(':checked'));

    $.ajax({
        type: 'post',
        url: HostUrl + "/Management/EditNews",
        data: fdata,
        processData: false,
        contentType: false
    }).done(function (result) {
        if (result.result == 1) {
            bootbox.alert({
                className: 'my-modal',
                message: "اطلاعیه با موفقیت آپدیت شد - توجه: برای دیدن تغییرات باید پیج را رفرش کنید"
            });
            setTimeout(function () {
                location.reload();
            }, 2000);
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

function editr(element) {
    var myelm = $(element);
    $("#divloader").show();
    var fdata = new FormData();
    fdata.append("__RequestVerificationToken", gettoken());
    fdata.append("who", myelm.data("whochange"));

    $.ajax({
        type: 'post',
        url: HostUrl + "/Management/getiUser",
        data: fdata,
        processData: false,
        contentType: false
    }).done(function (result) {
        if (result.result == 1) {
            $('#mrid').val(myelm.data("whochange"));
            $('#rfname').val(result.fname);
            $('#rlname').val(result.lname);
            $('#rmobile').val(result.mobile);
            $('#rcode').val(result.code);

            $("#editor-modal").modal('show');
            $("#divloader").hide();

        } else {
            bootbox.alert(result.result);
            $("#divloader").hide();
        }
    });
}

function verifytch(element) {
    var myelm = $(element);
    var mmm = "آیا از تایید این کاربر اطمینان دارید؟";

    bootbox.confirm({
        message: mmm,
        buttons: {
            confirm: {
                label: 'بله',
                className: 'btn-success'
            },
            cancel: {
                label: 'خیر - بازگشت',
                className: 'btn-danger'
            }
        },
        callback: function (result) {
            $("#divloader").show();
            if (result == true) {
                var fdata = new FormData();
                fdata.append("__RequestVerificationToken", gettoken());
                fdata.append("who", myelm.data("whochange"));

                $.ajax({
                    type: 'post',
                    url: HostUrl + "/Management/verifyrsl",
                    data: fdata,
                    processData: false,
                    contentType: false
                }).done(function (result) {
                    bootbox.alert(result.result);
                    renderfoo10();
                    $("#divloader").hide();
                });
            } else {
                $("#divloader").hide();
            }
        }
    });
}


function changerqstatus(element) {
    var myelm = $(element);
    console.log(myelm.data("whochange"));
    var activv = myelm.data("nowactive");
    var mmm = "";
    if (activv == 0) {
        mmm = "آیا از لغو این درخواست اطمینان دارید؟ در اینصورت مبلغ این رزرو نیز به کاربر برگشت خواهد شد";
    } else {
        mmm = "آیا از تایید کردن این درخواست اطمینان دارید؟";
    }
    var options = '';

    for (var a = 1; a < 30; a++) {
        options += '<option value="' + a + '">' + a + '</option>';
    }
    var dialog = bootbox.dialog({
        className: 'my-modal',
        message: '<h5 class="text-center font-weight-bold">' + mmm + '</h5><hr/><p class="text-center">توضیحات</p><div class="text-center col-lg-5" style="display: block; margin: 0 auto;"><textarea rows="4" class="form-control text-center" style="margin-bottom: 10px" id="icode"></textarea> <p>انتخاب اپراتور</p> <select Id="vrf"> ' + options + '</select> <br><br><button type="button" style="margin-bottom: 10px" onclick="changerqstatusend(this)" data-foo="' + myelm.data("foo") + '" data-whochange="' + myelm.data("whochange") + '" data-nowactive="' + myelm.data("nowactive") + '" class="btn btn-primary btn-block"><b>انجام عملیات</b></button></div>',
        closeButton: true
    });
}

function changerqstatusend(element) {
    var myelm = $(element);
    console.log(myelm.data("whochange"));
    var activv = myelm.data("nowactive");

    var fdata = new FormData();
    fdata.append("__RequestVerificationToken", gettoken());
    fdata.append("nowst", activv);
    fdata.append("who", myelm.data("whochange"));
    fdata.append("note", $("#icode").val());
    fdata.append("vrf", $("#vrf").val());

    $.ajax({
        type: 'post',
        url: HostUrl + "/Management/Changrqstatus",
        data: fdata,
        processData: false,
        contentType: false
    }).done(function (result) {
        bootbox.hideAll();
        bootbox.alert(result.result);
        //setTimeout(function () {
        //    location.reload();
        //}, 2000);
        if (myelm.data("foo") == 1) {
            renderfoo1();
        } else {
            renderfoo7();
        }
        $("#divloader").hide();
    });

}

function deleter(element) {
    var myelm = $(element);

    mmm = "آیا از حذف این کاربر اطمینان دارید؟";

    bootbox.confirm({
        message: mmm,
        buttons: {
            confirm: {
                label: 'بله',
                className: 'btn-success'
            },
            cancel: {
                label: 'خیر - بازگشت',
                className: 'btn-danger'
            }
        },
        callback: function (result) {
            $("#divloader").show();
            if (result == true) {
                var fdata = new FormData();
                fdata.append("__RequestVerificationToken", gettoken());
                fdata.append("who", myelm.data("whochange"));

                $.ajax({
                    type: 'post',
                    url: HostUrl + "/Management/deleter",
                    data: fdata,
                    processData: false,
                    contentType: false
                }).done(function (result) {

                    bootbox.alert(result.result);
                    //setTimeout(function () {
                    //    location.reload();
                    //}, 2000);
                    $("#divloader").hide();
                    renderfoo10();
                });
            } else {
                $("#divloader").hide();
            }
        }
    });
}

function deletep(element) {
    var myelm = $(element);

    mmm = "آیا از حذف این جای پارک اطمینان دارید؟";

    bootbox.confirm({
        message: mmm,
        buttons: {
            confirm: {
                label: 'بله',
                className: 'btn-success'
            },
            cancel: {
                label: 'خیر - بازگشت',
                className: 'btn-danger'
            }
        },
        callback: function (result) {
            $("#divloader").show();
            if (result == true) {
                var fdata = new FormData();
                fdata.append("__RequestVerificationToken", gettoken());
                fdata.append("who", myelm.data("whochange"));

                $.ajax({
                    type: 'post',
                    url: HostUrl + "/Management/deletep",
                    data: fdata,
                    processData: false,
                    contentType: false
                }).done(function (result) {

                    bootbox.alert(result.result);
                    //setTimeout(function () {
                    //    location.reload();
                    //}, 2000);
                    $("#divloader").hide();
                    renderfoo2();
                });
            } else {
                $("#divloader").hide();
            }
        }
    });
}

function changepstatus(element) {
    var myelm = $(element);
    console.log(myelm.data("whochange"));
    var activv = myelm.data("nowactive");
    var mmm = "";
    if (activv == 0) {
        mmm = "آیا از فعال کردن این جای پارک اطمینان دارید؟";

    } else {
        mmm = "آیا از غیر فعال کردن این جای پارک اطمینان دارید؟";

    }
    bootbox.confirm({
        message: mmm,
        buttons: {
            confirm: {
                label: 'بله',
                className: 'btn-success'
            },
            cancel: {
                label: 'خیر - بازگشت',
                className: 'btn-danger'
            }
        },
        callback: function (result) {
            $("#divloader").show();
            if (result == true) {
                var fdata = new FormData();
                fdata.append("__RequestVerificationToken", gettoken());
                fdata.append("nowst", activv);
                fdata.append("who", myelm.data("whochange"));

                $.ajax({
                    type: 'post',
                    url: HostUrl + "/Management/Changepstatus",
                    data: fdata,
                    processData: false,
                    contentType: false
                }).done(function (result) {

                    bootbox.alert(result.result);
                    //setTimeout(function () {
                    //    location.reload();
                    //}, 2000);
                    $("#divloader").hide();
                    renderfoo2();
                });
            } else {
                $("#divloader").hide();
            }
        }
    });
}

function changeschstatus(element) {
    var myelm = $(element);
    console.log(myelm.data("whochange"));
    var activv = myelm.data("nowactive");
    var mmm = "";
    if (activv) {
        mmm = "آیا از غیر فعال کردن این آموزشگاه اطمینان دارید؟ با اینکار دسترسی این آموزشگاه و مدرسان و تمامی کلاس های این آموزشگاه متوقف خواهد شد";
    } else {
        mmm = "آیا از فعال کردن این آموزشگاه اطمینان دارید؟";
    }
    bootbox.confirm({
        message: mmm,
        buttons: {
            confirm: {
                label: 'بله',
                className: 'btn-success'
            },
            cancel: {
                label: 'خیر - بازگشت',
                className: 'btn-danger'
            }
        },
        callback: function (result) {
            $("#divloader").show();
            if (result == true) {
                var fdata = new FormData();
                fdata.append("__RequestVerificationToken", gettoken());
                fdata.append("nowst", activv);
                fdata.append("who", myelm.data("whochange"));

                $.ajax({
                    type: 'post',
                    url: HostUrl + "/Management/Changschstatus",
                    data: fdata,
                    processData: false,
                    contentType: false
                }).done(function (result) {

                    bootbox.alert(result.result);
                    setTimeout(function () {
                        location.reload();
                    }, 2000);
                    $("#divloader").hide();
                });
            } else {
                $("#divloader").hide();
            }
        }
    });
}


function changesrlstatus(element) {
    var myelm = $(element);
    console.log(myelm.data("whochange"));
    var activv = myelm.data("nowactive");
    var mmm = "";
    if (activv) {
        mmm = "آیا از آزاد کردن این کد اطمینان دارید؟";
    } else {
        mmm = "آیا از غیر فعال کردن این کد اطمینان دارید؟";
    }
    bootbox.confirm({
        message: mmm,
        buttons: {
            confirm: {
                label: 'بله',
                className: 'btn-success'
            },
            cancel: {
                label: 'خیر - بازگشت',
                className: 'btn-danger'
            }
        },
        callback: function (result) {
            $("#divloader").show();
            if (result == true) {
                var fdata = new FormData();
                fdata.append("__RequestVerificationToken", gettoken());
                fdata.append("nowst", activv);
                fdata.append("who", myelm.data("whochange"));

                $.ajax({
                    type: 'post',
                    url: HostUrl + "/Management/Changsrlstatus",
                    data: fdata,
                    processData: false,
                    contentType: false
                }).done(function (result) {
                    bootbox.alert(result.result);
                    setTimeout(function () {
                        location.reload();
                    }, 2000);
                    $("#divloader").hide();
                });
            } else {
                $("#divloader").hide();
            }
        }
    });
}

function deletesch(element) {
    var myelm = $(element);
    var mmm = "";
    mmm = "آیا از حذف کردن این آموزشگاه اطمینان دارید؟";

    bootbox.confirm({
        message: mmm,
        buttons: {
            confirm: {
                label: 'بله',
                className: 'btn-success'
            },
            cancel: {
                label: 'خیر - بازگشت',
                className: 'btn-danger'
            }
        },
        callback: function (result) {
            $("#divloader").show();
            if (result == true) {
                var fdata = new FormData();
                fdata.append("__RequestVerificationToken", gettoken());
                fdata.append("who", myelm.data("whochange"));

                $.ajax({
                    type: 'post',
                    url: HostUrl + "/Management/Deletesch",
                    data: fdata,
                    processData: false,
                    contentType: false
                }).done(function (result) {
                    bootbox.alert(result.result);
                    setTimeout(function () {
                        location.reload();
                    }, 2000);
                    $("#divloader").hide();
                });
            } else {
                $("#divloader").hide();
            }
        }
    });
}

function deletetch(element) {
    var myelm = $(element);
    var mmm = "";
    mmm = "آیا از حذف کردن این مدرس اطمینان دارید؟";

    bootbox.confirm({
        message: mmm,
        buttons: {
            confirm: {
                label: 'بله',
                className: 'btn-success'
            },
            cancel: {
                label: 'خیر - بازگشت',
                className: 'btn-danger'
            }
        },
        callback: function (result) {
            $("#divloader").show();
            if (result == true) {
                var fdata = new FormData();
                fdata.append("__RequestVerificationToken", gettoken());
                fdata.append("who", myelm.data("whochange"));

                $.ajax({
                    type: 'post',
                    url: HostUrl + "/Management/Deletetch",
                    data: fdata,
                    processData: false,
                    contentType: false
                }).done(function (result) {
                    bootbox.alert(result.result);
                    setTimeout(function () {
                        location.reload();
                    }, 2000);
                    $("#divloader").hide();
                });
            } else {
                $("#divloader").hide();
            }
        }
    });
}


function verifysch(element) {
    var myelm = $(element);
    var mmm = "";
    mmm = "آیا از تایید کردن این آموزشگاه اطمینان دارید؟";

    bootbox.confirm({
        message: mmm,
        buttons: {
            confirm: {
                label: 'بله',
                className: 'btn-success'
            },
            cancel: {
                label: 'خیر - بازگشت',
                className: 'btn-danger'
            }
        },
        callback: function (result) {
            $("#divloader").show();
            if (result == true) {
                var fdata = new FormData();
                fdata.append("__RequestVerificationToken", gettoken());
                fdata.append("who", myelm.data("whochange"));

                $.ajax({
                    type: 'post',
                    url: HostUrl + "/Management/Verifysch",
                    data: fdata,
                    processData: false,
                    contentType: false
                }).done(function (result) {
                    bootbox.alert(result.result);
                    setTimeout(function () {
                        location.reload();
                    }, 2000);
                    $("#divloader").hide();
                });
            } else {
                $("#divloader").hide();
            }
        }
    });
}

function changestdpass(element) {
    var myelm = $(element);
    console.log(myelm.data("whochange"));

    var dialog = bootbox.dialog({
        className: 'my-modal',
        message: '<h5 class="text-center font-weight-bold">تغییر رمز عبور برای ' + myelm.data("whoname") + '</h5><hr/><p class="text-center"> رمز عبور جدید وارد کنید</p><div class="text-center col-lg-5" style="display: block; margin: 0 auto;"><input type="password" placeholder="New Password" lang="en" class="form-control text-center" style="margin-bottom: 10px" id="ipass"><input type="password" lang="en" placeholder="Confirm New Password" class="form-control text-center" style="margin-bottom: 10px" id="irepass"><button type="button" style="margin-bottom: 10px" onclick="changepass(this)" data-whochange="' + myelm.data("whochange") + '" class="btn btn-primary btn-block"><b>Save Changes</b></button></div>',
        closeButton: true
    });
}

function changenewsstatus(element) {
    var myelm = $(element);
    console.log(myelm.data("whochange"));
    var activv = myelm.data("nowactive");
    var mmm = "";
    if (activv) {
        mmm = "آیا از غیر فعال کردن این خبر اطمینان دارید؟";
    } else {
        mmm = "آیا از فعال کردن این خبر اطمینان دارید؟";
    }
    bootbox.confirm({
        message: mmm,
        buttons: {
            confirm: {
                label: 'بله',
                className: 'btn-success'
            },
            cancel: {
                label: 'خیر - بازگشت',
                className: 'btn-danger'
            }
        },
        callback: function (result) {
            $("#divloader").show();
            if (result == true) {
                var fdata = new FormData();
                fdata.append("__RequestVerificationToken", gettoken());
                fdata.append("nowst", activv);
                fdata.append("who", myelm.data("whochange"));

                $.ajax({
                    type: 'post',
                    url: HostUrl + "/Management/Changnewsstatus",
                    data: fdata,
                    processData: false,
                    contentType: false
                }).done(function (result) {

                    bootbox.alert(result.result);
                    setTimeout(function () {
                        location.reload();
                    }, 2000);
                    $("#divloader").hide();
                });
            } else {
                $("#divloader").hide();
            }
        }
    });
}



function getstdcls(element) {
    $("#divloader").show();
    var myelm = $(element);
    var whh = myelm.data("whosee");
    $.getJSON(HostUrl + "/Management/getStdofcls", {
        clsid: whh,
        schid: meid
    }, function (data) {
        var a = data.result;
        if (a == 0) {
            bootbox.alert("مشکلی پیش آمده است");
        } else {
            var dialog = bootbox.dialog({
                className: 'my-modal',
                message: data.result,
                closeButton: true
            });
        }
        $("#divloader").hide();
    });
}


var $modal = $('#editor-modal'),
    $editor = $('#editor'),
    $editorTitle = $('#editor-title'),
    ft = FooTable.init('#exampleFootableFiltering1', {
        editing: {
            enabled: true,
            editRow: function (row) {
                var values = row.val();
                $editor.find('#istdId').val(values.Id);
                $editor.find('#istdfnamefa').val(values.fnamefa);
                $editor.find('#istdlnamefa').val(values.lnamefa);
                $editor.find('#istdfnameen').val(values.fnameen);
                $editor.find('#istdlnameen').val(values.lnameen);
                $editor.find('#istdfathername').val(values.fathername);
                $editor.find('#istdnation').val(values.nation);
                $editor.find('#istdmobile').val(values.mobile);
                $editor.find('#istdemail').val(values.email);

                $modal.data('row', row);
                $editorTitle.text(values.fnamefa + " " + values.lnamefa + " تغییر دادن اطلاعات ");
                $modal.modal('show');
            },
            "hideText": "",
            "showText": ''
        },
        "paging": {
            "enabled": true
        }
    });

//var $modal = $('#editor-modal'),
//    $editor = $('#editor'),
//    $editorTitle = $('#editor-title'),
//    ft = FooTable.init('#exampleFootableFiltering4', {
//        editing: {
//            enabled: true,
//            editRow: function (row) {
//                var values = row.val();
//                $editor.find('#istdId').val(values.Id);
//                $editor.find('#istdfnamefa').val(values.fnamefa);
//                $editor.find('#istdlnamefa').val(values.lnamefa);
//                $editor.find('#istdfnameen').val(values.fnameen);
//                $editor.find('#istdlnameen').val(values.lnameen);
//                $editor.find('#istdfathername').val(values.fathername);
//                $editor.find('#istdnation').val(values.nation);
//                $editor.find('#istdmobile').val(values.mobile);
//                $editor.find('#istdemail').val(values.email);

//                $modal.data('row', row);
//                $editorTitle.text(values.fnamefa + " " + values.lnamefa + " تغییر دادن اطلاعات ");
//                $modal.modal('show');
//            },
//            "hideText": "",
//            "showText": ''
//        },
//        "paging": {
//            "enabled": true
//        }
//    });


//$("#savestd").on('click', function () {
//    bootbox.confirm({
//        message: "آیا از تغییر اطلاعات این زبان آموز اطمینان دارید؟",
//        buttons: {
//            confirm: {
//                label: 'بله',
//                className: 'btn-success'
//            },
//            cancel: {
//                label: 'خیر-بازگشت',
//                className: 'btn-danger'
//            }
//        },
//        callback: function (result) {
//            if (result == true) {
//                $("#divloader").show();
//                var row = $modal.data('row'), // get any previously stored row object
//                    values = {
//                        namefa: $editor.find('#istdfnamefa').val() + " " + $editor.find('#istdlnamefa').val(),
//                        nameen: $editor.find('#istdfnameen').val() + " " + $editor.find('#istdlnameen').val(),
//                        fathername: $editor.find('#istdfathername').val(),
//                        nation: $editor.find('#istdnation').val(),
//                        mobile: $editor.find('#istdmobile').val(),
//                        email: $editor.find('#istdemail').val()
//                    };
//                if (row instanceof FooTable.Row) {
//                    var fdata = new FormData();
//                    fdata.append("Id", $('#istdId').val());
//                    fdata.append("cfnamefa", $('#istdfnamefa').val());
//                    fdata.append("elnamefa", $('#istdlnamefa').val());
//                    fdata.append("efnameen", $('#istdfnameen').val());
//                    fdata.append("elnameen", $('#istdlnameen').val());
//                    fdata.append("efaname", $('#istdfathername').val());
//                    fdata.append("nationcode", $('#istdnation').val());
//                    fdata.append("mobile", $('#istdmobile').val());
//                    fdata.append("eemail", $('#istdemail').val());
//                    fdata.append("__RequestVerificationToken", gettoken());
//                    $.ajax({
//                        type: 'post',
//                        url: HostUrl + "/Management/EditStdInfo",
//                        data: fdata,
//                        processData: false,
//                        contentType: false
//                    }).done(function (result) {
//                        $("#divloader").hide();
//                        if (result.result == "1") {
//                            bootbox.alert("تغییرات با موفقیت اعمال شد");
//                            $modal.modal('hide');
//                            row.val(values);
//                        }
//                        else {
//                            bootbox.alert(result.result);
//                        }
//                        $("#divloader").hide();

//                    });
//                }

//            } else {
//                $modal.modal('hide');
//            }
//        }
//    });
//});

var $modal2 = $('#editor-modal2'),
    $editor2 = $('#editor2'),
    $editorTitle2 = $('#editor-title2'),
    ft = FooTable.init('#exampleFootableFiltering2', {
        editing: {
            enabled: true,
            editRow: function (row) {
                var values = row.val();
                $editor2.find('#iclscode').val(values.ClassCode);
                $editor2.find('#iclsId').val(values.clsid);
                $editor2.find('#iclstch').val(values.tchid);
                $editor2.find('#iclslevel1').val(values.lvl1);
                $editor2.find('#iclsles1').val(values.les1);
                $editor2.find('#iclstoles1').val(values.toles1);
                if (values.lvl2 != "") {
                    $("#inputchecked2")[0].checked = true;

                    $("#showlvl2").show();
                    $("#showles2").show();
                    $("#showtoles2").show();
                } else {
                    $("#inputchecked2")[0].checked = false;

                    $("#showlvl2").hide();
                    $("#showles2").hide();
                    $("#showtoles2").hide();
                }
                $editor2.find('#iclslevel2').val(values.lvl2);
                $editor2.find('#iclsles2').val(values.les2);
                $editor2.find('#iclstoles2').val(values.toles2);
                $editor2.find('#iclscapacity').val(values.Capacity);
                $editor2.find('#iclsstartDate').val(values.StartDate);
                $editor2.find('#iclsfinalDate').val(values.FinalDate);

                $modal2.data('row', row);
                $editorTitle2.text("(" + values.ClassCode + ") تغییر دادن اطلاعات کلاس ");
                $modal2.modal('show');
            },
            "hideText": "",
            "showText": '',
            "column": {
                "classes": "footable-editing",
                "name": "",
                "title": "",
                "filterable": false,
                "sortable": false
            }
        }
    });

$("#savecls").on('click', function () {
    bootbox.confirm({
        message: "آیا از تغییر اطلاعات این کلاس اطمینان دارید؟",
        buttons: {
            confirm: {
                label: 'بله',
                className: 'btn-success'
            },
            cancel: {
                label: 'خیر-بازگشت',
                className: 'btn-danger'
            }
        },
        callback: function (result) {
            if (result == true) {
                $("#divloader").show();
                //$editor2 = $('#editor2');
                var lvvl = $editor2.find('#iclslevel1').val() + " [" + $editor2.find('#iclsles1').val() + "-" + $editor2.find('#iclstoles1').val() + "]";
                if ($editor2.find('#iclslevel2').val() != "") {
                    lvvl += " " + $editor2.find('#iclslevel2').val() + " [" + $editor2.find('#iclsles2').val() + "-" + $editor2.find('#iclstoles2').val() + "]";
                }
                var row = $modal2.data('row'), // get any previously stored row object
                    values = {
                        Teacher: $editor2.find('#iclstch option:selected').text(),
                        Level: lvvl,
                        ClassCode: $editor2.find('#iclscode').val(),
                        Capacity: $editor2.find('#iclscapacity').val(),
                        StartDate: $editor2.find('#iclsstartDate').val(),
                        FinalDate: $editor2.find('#iclsfinalDate').val()
                    };

                if (row instanceof FooTable.Row) {
                    var fdata = new FormData();
                    fdata.append("iclsId", $('#iclsId').val());
                    fdata.append("iclscode", $('#iclscode').val());
                    fdata.append("iclstch", $('#iclstch').val());
                    fdata.append("iclslevel1", $('#iclslevel1').val());
                    fdata.append("iclsles1", $('#iclsles1').val() + "-" + $('#iclstoles1').val());
                    //fdata.append("iclstoles1", $('#iclstoles1').val());
                    fdata.append("iclslevel2", $('#iclslevel2').val());
                    fdata.append("iclsles2", $('#iclsles2').val() + "-" + $('#iclstoles2').val());
                    //fdata.append("iclstoles2", $('#iclstoles2').val());
                    fdata.append("iclscapacity", $('#iclscapacity').val());
                    fdata.append("iclsstartDate", $('#iclsstartDate').val());
                    fdata.append("iclsfinalDate", $('#iclsfinalDate').val());

                    fdata.append("__RequestVerificationToken", gettoken());
                    $.ajax({
                        type: 'post',
                        url: HostUrl + "/Management/EditClsInfo",
                        data: fdata,
                        processData: false,
                        contentType: false
                    }).done(function (result) {
                        $("#divloader").hide();
                        if (result.result == "1") {
                            bootbox.alert("تغییرات با موفقیت اعمال شد");
                            $modal2.modal('hide');
                            row.val(values);
                        }
                        else {
                            bootbox.alert(result.result);
                        }
                        $("#divloader").hide();

                    });
                }
            } else {
                $modal.modal('hide');
            }
        }
    });
});

$("#ChangePasswordbtn").click(function () {
    $("#divloader").show();
    var fdata = new FormData();

    fdata.append("cp", $('#CurrentPassInput').val());
    fdata.append("np", $('#NewPassInput').val());
    fdata.append("renp", $('#ReNewPassInput').val());
    fdata.append("__RequestVerificationToken", gettoken());

    $.ajax({
        type: 'post',
        url: HostUrl + "/Management/ChangePassword",
        data: fdata,
        processData: false,
        contentType: false
    }).done(function (result) {
        $("#divloader").hide();
        if (result.result == 1) {
            bootbox.alert("رمز عبور با موفقیت تغییر کرد");
            setTimeout(function () {
                document.location = HostUrl + "/Logout";
            }, 2000);
        } else if (result.result == 2) {
            bootbox.alert("رمز عبور فعلی درست نمی باشد");
        }
        else if (result.result == 3) {
            bootbox.alert("رمز عبور باید شامل حداقل 6 کاراکتر باشد");
        }
        else if (result.result == 4) {
            bootbox.alert("رمز عبور جدید با تکرار آن مطابقت ندارد");
        }
        else {
            bootbox.alert("مشکلی پیش آمده لطفا دوباره تلاش کنید");
        }
    });
});