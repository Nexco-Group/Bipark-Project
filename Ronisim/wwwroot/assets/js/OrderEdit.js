var buyitems = [
    //{ id: '1', name: 'vcv1 cls', tozih: '', count: 'm', price: '', discount: '', total: '' }
];
var totalprice = 0;
var shipping = 0;
var totalall = 0;

var maindivel = "";
var $selectParent;
var $copy;
var InfoChanged = false;

$(document).ready(function () {
    $("#laghvinvoicebtn").hide();

    $selectParent = $('#comboitem');
    init($selectParent);

    var items = orderCart.split('_');
    for (var a = 0; a < items.length; a++) {
        var item = items[a];
        var ss = item.split('-')
        var si = $('#comboitem').find('option[value="' + ss[0] + '"]');
        var total = ((ss[1] * si.data("price")) / 100) * (100 - si.data("discount"));
        buyitems.push({ id: si.val(), name: si.text(), tozih: si.data("tozihat"), count: ss[1], price: si.data("price"), discount: si.data("discount"), total: total });
    }

    refreshtable();
    setTimeout(function () {
        changesome(orderCodeExist, iSht, iShc);
    }, 100);

});

function SaveIn() {
    var dialog = bootbox.dialog({
        message: "<b>لطفا روش ارسال را انتخاب کنید</b>",
        buttons: {
            cancel: {
                label: "TPX - تی پی ایکس",
                className: 'btn-primary',
                callback: function () {
                    SaveIn2(3);
                }
            },
            noclose: {
                label: "باربری",
                className: 'btn-info',
                callback: function () {
                    SaveIn2(2);
                    return false;
                }
            },
            ok: {
                label: "پست رزروی",
                className: 'btn-success',
                callback: function () {
                    SaveIn2(1);
                    return false;
                }
            }
        }
    });
}

function SaveIn2(shippingType) {
    $("#divloader").show();

    var fdata = new FormData();
    fdata.append("__RequestVerificationToken", gettoken());
    var itj = "";
    var i;
    for (i = 0; i < buyitems.length; ++i) {
        if (i == 0) {
            itj += buyitems[i].id + "-" + buyitems[i].count;
        } else {
            itj += "_" + buyitems[i].id + "-" + buyitems[i].count;
        }
    }
    if (InfoChanged == true) {
        fdata.append("address", $("#addressinput").val());
        fdata.append("zipcode", $("#zipcodeinput").val());
        fdata.append("mobile", $("#schmobil").val());
        fdata.append("schphone", $("#schphone").val());
    }
    fdata.append("shippingType", shippingType);

    fdata.append("infochange", InfoChanged);
    fdata.append("json", itj);
    $.ajax({
        type: 'post',
        url: HostUrl + "/Orders/SaveInvoice",
        data: fdata,
        processData: false,
        contentType: false
    }).done(function (result) {
        if (result.result == 1) {
            bootbox.alert({
                className: 'my-modal',
                message: "رزرو شما به شماره پیگیری " + result.order + " برای شما به ثبت رسید "
            });
            changesome(result.order, result.shippingType, result.shippingCost);
            $("#editinvoicebtn").show();
            InfoChanged = false;
            orderExist = result.orderid;
        }
        else {
            bootbox.alert({
                className: 'my-modal',
                message: result.result
            });
        }
        bootbox.hideAll();
        $("#divloader").hide();
    });
}


function ChangeIn() {
    var dialog = bootbox.dialog({
        message: "<b>لطفا روش ارسال را انتخاب کنید</b>",
        buttons: {
            cancel: {
                label: "TPX - تی پی ایکس",
                className: 'btn-primary',
                callback: function () {
                    ChangeIn2(3);
                }
            },
            noclose: {
                label: "باربری",
                className: 'btn-info',
                callback: function () {
                    ChangeIn2(2);
                    return false;
                }
            },
            ok: {
                label: "پست رزروی",
                className: 'btn-success',
                callback: function () {
                    ChangeIn2(1);
                    return false;
                }
            }
        }
    });
}


function ChangeIn2(shippingType) {
    $("#divloader").show();

    var fdata = new FormData();
    fdata.append("__RequestVerificationToken", gettoken());
    var itj = "";
    var i;
    for (i = 0; i < buyitems.length; ++i) {
        if (i == 0) {
            itj += buyitems[i].id + "-" + buyitems[i].count;
        } else {
            itj += "_" + buyitems[i].id + "-" + buyitems[i].count;
        }
    }

    if (InfoChanged == true) {
        fdata.append("address", $("#addressinput").val());
        fdata.append("zipcode", $("#zipcodeinput").val());
        fdata.append("mobile", $("#schmobil").val());
        fdata.append("schphone", $("#schphone").val());
    }
    fdata.append("shippingType", shippingType);

    fdata.append("infochange", InfoChanged);
    fdata.append("order_id", orderExist);
    fdata.append("json", itj);
    $.ajax({
        type: 'post',
        url: HostUrl + "/Orders/ChangeInvoice",
        data: fdata,
        processData: false,
        contentType: false
    }).done(function (result) {
        if (result.result == 1) {
            //bootbox.alert({
            //    className: 'my-modal',
            //    message: "رزرو شما به شماره پیگیری " + result.order + " برای شما به ثبت رسید و به زودی همکاران ما برای اقدامات آتی با شما تماس خواهند گرفت. برای اطمینان بیشتر از فاکتور خود پرینت تهیه بفرمایید "
            //});
            changesome(result.order, result.shippingType, result.shippingCost);
            $("#editinvoicebtn").show();
            InfoChanged = false;
        }
        else {
            bootbox.alert({
                className: 'my-modal',
                message: result.result
            });
        }
        bootbox.hideAll();
        $("#divloader").hide();

    });
}

function EditInvoice() {
    $("#changinfo").show();
    $("#editinvoicebtn").hide();
    $("#maindiv").html(maindivel);
    $("#printbtn").html('<i class="icon md-print" aria-hidden="true"></i> پرینت این پیش فاکتور');
    $("#iribbondiv").removeClass("ribbon-success");
    $("#iribbondiv").addClass("ribbon-primary");
    $("#iribbon").html("پیش فاکتور");
    $(".deleteable").show();
    shipping = 1;
    refreshtotal();
    $("#printupbtn").attr('onclick', 'InvoicePrint();');
    $("#PaymentBtn").attr("onclick", "ChangeIn();");
    $("#paymenttext").html('<i class="icon md-shopping-cart" aria-hidden="true"></i> ثبت فاکتور');
    //$(".select2").select2();
    var $copy = $selectParent.clone();
    $("#addselect").html("");
    $("#addselect").append($copy);
    init($copy);
}

function init($elem) {
    $elem.select2({
        width: '100%'
    });
}

function ChangInfoBtn() {
    InfoChanged = true;
    $("#ipro").html($("#addressinput").val());
    $("#izip").html($("#zipcodeinput").val());
    $("#iphone").html($("#schmobil").val());
    $("#imob").html($("#schphone").val());
}

function LaghvSefaresh() {
    bootbox.confirm({
        message: "آیا از لغو این رزرو اطمینان دارید؟",
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
                $.ajax({
                    type: 'post',
                    url: HostUrl + "/Orders/LaghvInvoice",
                    data: fdata,
                    processData: false,
                    contentType: false
                }).done(function (result) {
                    if (result.result == 1) {
                        bootbox.alert({
                            className: 'my-modal',
                            message: "رزرو شما با موفقیت  لغو شد، اکنون میتوانید رزرو جدید ثبت کنید "
                        });
                        window.location.replace("/Orders");
                    }
                    else {
                        bootbox.alert({
                            className: 'my-modal',
                            message: result.result
                        });
                    }
                    bootbox.hideAll();
                    $("#divloader").hide();
                });
            }
        }
    });

}
$('select').on('select2:open', function (e) {
    $('.custom-dropdown').parent().css('z-index', 99999);
});

function payment() {
    var dialog = bootbox.dialog({
        message: "<b>لطفا روش پرداخت را انتخاب کنید</b>",
        buttons: {
            noclose: {
                label: "Transfer to bank - واریز به حساب",
                className: 'btn-info',
                callback: function () {
                    Paymentb(false);
                    return false;
                }
            }
        }
    });
}

function repayment() {
    var dialog = bootbox.dialog({
        message: "<b>لطفا روش پرداخت را انتخاب کنید</b>",
        buttons: {
            noclose: {
                label: "Transfer to bank - واریز به حساب",
                className: 'btn-info',
                callback: function () {
                    rePaymentb(false);
                    return false;
                }
            }
        }
    });
}

function rePaymentb(online, pay) {
    $("#divloader").show();
    var fdata = new FormData();
    fdata.append("__RequestVerificationToken", gettoken());
    fdata.append("order_id", orderFinish);
    fdata.append("online", online);
    $.ajax({
        type: 'post',
        url: HostUrl + "/Orders/RePayment",
        data: fdata,
        processData: false,
        contentType: false
    }).done(function (result) {
        if (result.result == 1) {
            if (online) {
                bootbox.alert({
                    className: 'my-modal',
                    message: "رزرو شما به شماره پیگیری " + result.order + " برای شما به ثبت رسید و شما در حال انتقال به درگاه پرداخت میباشید"
                });
                bootbox.hideAll();
                $("#divloader").hide();
                //$("html").load(result.paylink);  // this will change the content of current page with comment.php content
                window.location = result.paylink;
            } else {
                bootbox.alert({
                    className: 'my-modal',
                    message: "رزرو شما به شماره پیگیری " + result.order + " برای شما به ثبت رسید و به زودی همکاران ما برای اقدامات آتی با شما تماس خواهند گرفت. برای اطمینان بیشتر از فاکتور خود پرینت تهیه بفرمایید "
                });
                hidesome(result.order, result.pt, result.sht);

                bootbox.hideAll();
                $("#divloader").hide();
            }
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

function Paymentb(online) {
    //if (online == true) {
    //    bootbox.alert("متاسفانه پرداخت آنلاین در حال حاظر مقدور نیست");
    //    return;
    //}
    $("#divloader").show();

    var fdata = new FormData();
    fdata.append("__RequestVerificationToken", gettoken());

    fdata.append("order_id", orderExist);
    fdata.append("online", online);
    $.ajax({
        type: 'post',
        url: HostUrl + "/Orders/Payment",
        data: fdata,
        processData: false,
        contentType: false
    }).done(function (result) {
        if (result.result == 1) {
            if (online) {
                bootbox.alert({
                    className: 'my-modal',
                    message: "رزرو شما به شماره پیگیری " + result.order + " برای شما به ثبت رسید و شما در حال انتقال به درگاه پرداخت میباشید"
                });
                bootbox.hideAll();
                $("#divloader").hide();
                //$("html").load(result.paylink);  // this will change the content of current page with comment.php content
                window.location = result.paylink;
            } else {
                bootbox.alert({
                    className: 'my-modal',
                    message: "رزرو شما به شماره پیگیری " + result.order + " برای شما به ثبت رسید و به زودی همکاران ما برای اقدامات آتی با شما تماس خواهند گرفت. برای اطمینان بیشتر از فاکتور خود پرینت تهیه بفرمایید "
                });
                hidesome(result.order);

                bootbox.hideAll();
                $("#divloader").hide();
            }
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

function finish(code, mes, online, success, sht, shc) {
    var stype = "";
    if (sht == 1) {
        stype = "پست رزروی";
    } else if (sht == 2) {
        stype = "باربری";
    } else {
        stype = "TPX - تی پی ایکس";
    }
    shipping = shc;
    refreshtotal();

    if (success == "true") {
        $("#PaymentBtn").hide();
        $("#maindiv").html('<div class="card card-block p-30 bg-green-600" dir="rtl"> <div class="card-watermark darker font-size-60 m-15" style="margin-top: 40px;"><i aria-hidden="true" class="icon md-shopping-cart"></i></div><div class="counter counter-md counter-inverse text-left"> <div class="counter-number-group"> <span class="counter-number-related text-capitalize">' + mes + '</span> </div><div class="counter-label text-capitalize font-size-18">کد فاکتور: ' + code + ' - نحوه ارسال: ' + stype + '</div></div></div>');
        $("#iribbondiv").addClass("ribbon-success");
        $("#maindiv").append('<p>برای اقدامات بعدی همکاران ما با شما تماس حواهند گرفت</p>');
        $("#laghvinvoicebtn").hide();

    } else {
        $("#PaymentBtn").attr("onclick", "repayment();");
        $("#paymenttext").html('<i class="icon md-shopping-cart" aria-hidden="true"></i> پرداخت دوباره فاکتور');
        $("#maindiv").html('<div class="card card-block p-30 bg-red-600" dir="rtl"> <div class="card-watermark darker font-size-60 m-15" style="margin-top: 40px;"><i aria-hidden="true" class="icon md-shopping-cart"></i></div><div class="counter counter-md counter-inverse text-left"> <div class="counter-number-group"> <span class="counter-number-related text-capitalize">' + mes + '</span> </div><div class="counter-label text-capitalize font-size-18">کد فاکتور: ' + code + ' - نحوه ارسال: ' + stype + '</div></div></div>');
        $("#iribbondiv").addClass("ribbon-danger");
        $("#maindiv").append('<p>پرداخت شما ناموفق بوده است لطفا دوباره تلاش کنید</p>');
        $("#laghvinvoicebtn").show();
    }

    $("#editinvoicebtn").hide();
    $("#changinfo").hide();
    $("#printbtn").html('<i class="icon md-print" aria-hidden="true"></i> پرینت این فاکتور');
    $("#iribbondiv").removeClass("ribbon-primary");
    $("#iribbondiv").addClass("ribbon-success");
    $("#iribbon").html("فاکتور فروش");
    $(".deleteable").hide();
    $("#printupbtn").attr('onclick', 'InvoicePrint2();');
}

function changesome(code, sht, shc) {
    $("#laghvinvoicebtn").show();
    $("#PaymentBtn").attr("onclick", "payment();");
    $("#paymenttext").html('<i class="icon md-shopping-cart" aria-hidden="true"></i> پرداخت فاکتور');
    $("#changinfo").hide();
    maindivel = $("#maindiv").html();
    var stype = "";
    if (sht == 1) {
        stype = "پست رزروی";
    } else if (sht == 2) {
        stype = "باربری";
    } else {
        stype = "TPX - تی پی ایکس";
    }
    shipping = shc;
    refreshtotal();
    $("#maindiv").html('<div class="card card-block p-30 bg-blue-600" dir="rtl"> <div class="card-watermark darker font-size-60 m-15" style="margin-top: 40px;"><i aria-hidden="true" class="icon md-shopping-cart"></i></div><div class="counter counter-md counter-inverse text-left"> <div class="counter-number-group"> <span class="counter-number-related text-capitalize">فاکتور شما با موفقیت به ثبت رسید(عدم پرداخت)</span> </div><div class="counter-label text-capitalize font-size-18">کد فاکتور: ' + code + ' - نحوه ارسال: ' + stype + '</div></div></div>');
    $("#maindiv").append('<p>برای تکمیل رزرو نحوه پرداخت و ارسال را انتخاب کنید</p>');
    $("#printbtn").html('<i class="icon md-print" aria-hidden="true"></i> پرینت این فاکتور');
    $("#iribbondiv").removeClass("ribbon-primary");
    $("#iribbondiv").addClass("ribbon-success");
    $("#iribbon").html("فاکتور فروش");
    $(".deleteable").hide();
    $("#printupbtn").attr('onclick', 'InvoicePrint2();');
}

function hidesome(code, pt, iSht) {
    $("#PaymentBtn").hide();
    $("#changinfo").hide();
    //$selectParent.select2('destroy');
    $copy = $selectParent.clone();
    $("#maindiv").html('<div class="card card-block p-30 bg-green-600" dir="rtl"> <div class="card-watermark darker font-size-60 m-15" style="margin-top: 40px;"><i aria-hidden="true" class="icon md-shopping-cart"></i></div><div class="counter counter-md counter-inverse text-left"> <div class="counter-number-group"> <span class="counter-number-related text-capitalize">رزرو شما با موفقیت به ثبت رسید(واریز به حساب)</span> </div><div class="counter-label text-capitalize font-size-18">کد پیگیری: ' + code + '</div></div></div>');
    $("#maindiv").append('<p>در صورت وجود هرگونه سوال با پشتیبانی سایت در ارتباط باشید</p>');
    $("#printbtn").html('<i class="icon md-print" aria-hidden="true"></i> پرینت این فاکتور');
    $("#iribbondiv").removeClass("ribbon-primary");
    $("#iribbondiv").addClass("ribbon-success");
    $("#iribbon").html("فاکتور فروش");
    $(".deleteable").hide();
    $("#printupbtn").attr('onclick', 'InvoicePrint2();');

    $("#editinvoicebtn").hide();
    $("#laghvinvoicebtn").hide();
    $("#changinfo").hide();
    $("#laghvinvoicebtn").show();
}

function combochange() {
    $("#divloader").show();
    var si = $('#comboitem').find(":selected");
    var vl = si.value;
    $("#bookinfo").html("آیتم: " + "<span dir='ltr'>" + si.text() + "</span>" + " <br/> توضیح: " + si.data("tozihat") + " <br/> قیمت: " + si.data("price") + " ریال " + " <br/> تخفیف: " + si.data("discount") + " درصد ");
    $("#bookimg").attr("src", "https://contents.vcv.ir/BookImage/" + si.data("img"));

    $("#divloader").hide();
}

function additem() {
    if ($("#countitem").val() > 0) {
        $("#divloader").show();
        var si = $('#comboitem').find(":selected");
        if (si.data("tozihat")) {
            var total = (($("#countitem").val() * si.data("price")) / 100) * (100 - si.data("discount"));
            buyitems.push({ id: si.val(), name: si.text(), tozih: si.data("tozihat"), count: $("#countitem").val(), price: si.data("price"), discount: si.data("discount"), total: total });
            //console.log(JSON.stringify(buyitems, null, ' '));
            refreshtable();
        } else {
            $('#comboitem').focus();
        }
    }
}

function refreshtable() {
    $("#cartbody").html("");
    totalprice = 0;
    totalall = 0;
    for (var j = 0; j < buyitems.length; j++) {
        totalprice += buyitems[j].total;
        var btn = '<button data-id="' + j + '" onclick="deleteitem(this)" class="badge badge-table badge-danger">حذف این آیتم</button>';
        $("#cartbody").append('<tr> <td class="text-right">' + (j + 1) + '</td><td class="text-right" dir="ltr">' + buyitems[j].name + '</td><td class="text-right">' + buyitems[j].tozih + '</td><td class="text-right">' + buyitems[j].count + '</td><td class="text-right">' + buyitems[j].price.toLocaleString() + '</td><td class="text-right">' + buyitems[j].discount + '%</td><td class="text-center">' + buyitems[j].total.toLocaleString() + '</td><td class="text-center deleteable">' + btn + '</td></tr>');
    }
    refreshtotal();
}

function deleteitem(btn) {
    $("#divloader").show();
    buyitems.splice($(btn).data("id"), 1);
    refreshtable();
}

function refreshtotal() {
    if (shipping == 0) {
        totalall = totalprice;
        $("#shippingprice").html("هنگام دریافت جای پارک");
    } else if (shipping == 1) {
        totalall = totalprice;
        $("#shippingprice").html("0 ریال");
    } else {
        totalall = shipping + totalprice;
        $("#shippingprice").html(shipping.toLocaleString() + " ریال");
    }

    $("#totalprice").html(totalprice.toLocaleString() + " ریال");
    $("#totalall").html(totalall.toLocaleString() + " ریال");
    if (balance == 0) {
        $("#total22").hide();
        $("#balance").hide();
    } else {
        if (balance > 0) {
            $("#balance").html("بستانکاری: <span>" + balance.toLocaleString() + " ریال</span>");
        } else {
            Math.abs(balance);
            $("#balance").html("بدهکاری:<span>" + Math.abs(balance).toLocaleString() + " ریال</span>");
        }
        if (totalall == 0) {
            $("#totalall2").html("0" + " ریال");
        } else {
            $("#totalall2").html((totalall - balance).toLocaleString() + " ریال");
        }
    }
    $("#divloader").hide();
}

function InvoicePrint() {
    $('#print4, #print1, #print2, #print3').printThis({
        base: "https://vcv.ir",
        debug: true,
        importCSS: true,        // import parent page css
        importStyle: true,     // import style tags
        printContainer: true,   // print outer container/$.selector
        pageTitle: "VCV Invoice",          // add title to print page
        printDelay: 50,        // variable print delay
        formValues: false,       // preserve input/form values
        canvas: false,          // copy canvas content (experimental)
        base: false            // preserve the BASE tag, or accept a string for the URL
    });
}

function InvoicePrint2() {
    $('#printall').printThis({
        base: "https://vcv.ir",
        debug: true,
        importCSS: true,        // import parent page css
        importStyle: true,     // import style tags
        printContainer: true,   // print outer container/$.selector
        pageTitle: "VCV Invoice",          // add title to print page
        printDelay: 50,        // variable print delay
        formValues: false,       // preserve input/form values
        canvas: false,          // copy canvas content (experimental)
        base: false            // preserve the BASE tag, or accept a string for the URL
    });
}