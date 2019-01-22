var foo1;
var foo2;
var foo3;
var foo4;
var foo5;
var foo6;
var foo7;

$(document).ready(function () {
    $('#divloader').hide();
    //get Column JSON from first item in Rows array
    var columnJSON1 = [
        { "name": "id", "title": "ID", "breakpoints": "all" },
        { "name": "title", "title": "Title" },
        { "name": "description", "title": "Description" },
        { "name": "vcv", "title": "VCV", "breakpoints": "xs sm" },
        { "name": "discount", "title": "Discount", "breakpoints": "xs sm" },
        { "name": "price", "title": "Price", "breakpoints": "xs sm" },
        { "name": "stock", "title": "Stock", "breakpoints": "xs sm" },
        { "name": "active", "title": "Active", "breakpoints": "all" },
        { "name": "istock", "title": "Change Stock", "breakpoints": "all" }

    ];
    foo1 = FooTable.init('#foo1', {
        "columns": columnJSON1
    });
    $("a[href='#tab1']").click(function () {
        renderfoo1();
    });
    //----------------
    var columnJSON2 = [
        { "name": "id", "title": "ID", "breakpoints": "all" },
        { "name": "item", "title": "Book" },
        { "name": "number", "title": "Number", "breakpoints": "xs sm" },
        { "name": "description", "title": "Description" },
        { "name": "order_Id", "title": "Order Id(if it was order)", "breakpoints": "xs sm" },
        { "name": "date", "title": "Date", "breakpoints": "xs sm" },
        { "name": "time", "title": "Time", "breakpoints": "xs sm" },
        { "name": "active", "title": "Active", "breakpoints": "all" }
    ];
    foo2 = FooTable.init('#foo2', {
        "columns": columnJSON2
    });
    //$("a[href='#tab2']").click(function () {
    //    renderfoo2();
    //});
    //----------------
    var columnJSON3 = [
        { "name": "id", "title": "ID", "breakpoints": "all" },
        { "name": "schoolName", "title": "School Name" },
        { "name": "orderCode", "title": "Order Code", "breakpoints": "xs sm" },
        { "name": "schooldet", "title": "Billing Info" },
        { "name": "cartdet", "title": "Cart Item's" },
        { "name": "schoolCode", "title": "SchoolCode", "breakpoints": "all" },
        { "name": "paymentInfo", "title": "Payment Info", "breakpoints": "xs sm" },
        { "name": "orderSP", "title": "Order Shipping Print", "breakpoints": "xs sm" },
        { "name": "print", "title": "Print", "breakpoints": "xs sm" },
        { "name": "edit", "title": "Edit this order", "breakpoints": "xs sm" },
        { "name": "orderDate", "title": "Order Date", "breakpoints": "all" },
        { "name": "orderTime", "title": "OrderTime", "breakpoints": "all" },
        { "name": "operation", "title": "Operation", "breakpoints": "xs sm" },
        { "name": "weight", "title": "Weight", "breakpoints": "all"},
        { "name": "orderStep", "title": "Order Step" },
        { "name": "active", "title": "Active", "breakpoints": "all" }
    ];
    foo3 = FooTable.init('#foo3', {
        "columns": columnJSON3
    });
    $("a[href='#tab3']").click(function () {
        renderfoo3();
    });
    //----------------
    var columnJSON4 = [
        { "name": "id", "title": "ID", "breakpoints": "all" },
        { "name": "order_Id", "title": "Order ID" },
        { "name": "type", "title": "Type" },
        { "name": "money", "title": "Amount(Rial)" },
        { "name": "date", "title": "Date", "breakpoints": "xs sm" },
        { "name": "time", "title": "Time", "breakpoints": "xs sm" },
        { "name": "active", "title": "Active", "breakpoints": "all" }
    ];
    foo4 = FooTable.init('#foo4', {
        "columns": columnJSON4
    });
    $("a[href='#tab4']").click(function () {
        renderfoo4();
    });
    //----------------
    var columnJSON5 = [
        { "name": "id", "title": "ID", "breakpoints": "all" },
        { "name": "schoolCode", "title": "School Code" },
        { "name": "schoolNameFa", "title": "SchoolNameFa" },
        { "name": "schoolNameEn", "title": "SchoolNameEn", "breakpoints": "all" },
        { "name": "mobile", "title": "Mobile" },
        { "name": "fnameFa", "title": "FnameFa", "breakpoints": "all" },
        { "name": "lnameFa", "title": "LnameFa", "breakpoints": "all" },
        { "name": "fnameEn", "title": "Province", "breakpoints": "all" },
        { "name": "lnameEn", "title": "City", "breakpoints": "all" },
        { "name": "nationCode", "title": "Nation Code", "breakpoints": "all" },
        { "name": "position", "title": "Position", "breakpoints": "all" },
        { "name": "phone", "title": "Phone", "breakpoints": "all" },
        { "name": "zipCode", "title": "ZipCode", "breakpoints": "all" },
        { "name": "address", "title": "Address", "breakpoints": "all" },
        { "name": "balance", "title": "Balance" },
        { "name": "ibalance", "title": "Change Balance" },
        { "name": "iorder", "title": "Order books" },
        { "name": "joiningDate", "title": "JoiningDate", "breakpoints": "all"  }
    ];
    foo5 = FooTable.init('#foo5', {
        "columns": columnJSON5
    });
    $("a[href='#tab5']").click(function () {
        renderfoo5();
    });
    //----------------
    var columnJSON6 = [
        { "name": "id", "title": "ID", "breakpoints": "all" },
        { "name": "schoolCode", "title": "School Code" },
        { "name": "schoolNameFa", "title": "SchoolNameFa" },
        { "name": "amount", "title": "Amount" },
        { "name": "school_Id", "title": "School_Id", "breakpoints": "all" },
        { "name": "order_Id", "title": "Order_Id", "breakpoints": "all" },
        { "name": "description", "title": "Description" },
        { "name": "active", "title": "Active" },
        { "name": "date", "title": "Date", "breakpoints": "all" },
        { "name": "time", "title": "Time", "breakpoints": "all" }

    ];
    foo6 = FooTable.init('#foo6', {
        "columns": columnJSON6
    });
    $("a[href='#tab6']").click(function () {
        renderfoo6();
    });
});

//----------1-----------
function renderfoo1() {
    $('#divloader').show();
    $.getJSON(HostUrl + "/Store/getstoreitem", {
    }, function (data) {
        var a = data.result;
        if (a == 0) {
            bootbox.hideAll();
            bootbox.alert("مشکلی پیش آمده است");
            $("#divloader").hide();
        } else {
            $("#foo1>tbody>tr").each(function (index, elem) {
                $(elem).remove();
            });
            foo1.rows.load(a);

            $.each(foo1.rows.all, function (i, row) {
                v = row.val();
                var m = v.id;
                v.active = '<button data-whochange="' + m + '" data-active="' + v.active + '" onclick="iActiveItem(this)" class="badge badge-table badge-' + ((v.active == "true") ? 'danger' : 'success') + '">' + ((v.active == "true") ? 'InActive' : 'Active') + '</button>';
                v.istock = '<button data-whochange="' + m + '" data-title="' + v.title + '" onclick="istock(this)" class="badge badge-table badge-success">Increase or Decrease Stock</button>';
                row.val(v);
            });
            $("#divloader").hide();
        }
    });
}

function istock(element) {
    var myelm = $(element);

    var dialog = bootbox.dialog({
        className: 'my-modal',
        message: '<h5 class="text-center font-weight-bold">Add Stock for ' + myelm.data("title") + '</h5><hr/><p class="text-center">Attention: The number can be negative or positive</p><div class="text-center col-lg-5" style="display: block; margin: 0 auto;"><input placeholder="Number" type="Number" class="form-control text-center" style="margin-bottom: 10px" id="inum1"><input placeholder="Description" type="text" class="form-control text-center" style="margin-bottom: 10px" id="ides1"><button type="button" style="margin-bottom: 10px" onclick="addstock(this)" data-whochange="' + myelm.data("whochange") + '" class="btn btn-primary btn-block"><b>Save Changes</b></button></div>',
        closeButton: true
    });
}

function addstock(element) {
    var myelm = $(element);
    $("#divloader").show();
    var fdata = new FormData();
    fdata.append("__RequestVerificationToken", gettoken());
    fdata.append("ides", $("#ides1").val());
    fdata.append("inum", $("#inum1").val());
    fdata.append("who", myelm.data("whochange"));

    $.ajax({
        type: 'post',
        url: HostUrl + "/Store/StockItem",
        data: fdata,
        processData: false,
        contentType: false
    }).done(function (result) {
        bootbox.hideAll();
        bootbox.alert(result.result);
        renderfoo1();
        $("#divloader").hide();
    });
}

function iActiveItem(element) {
    var myelm = $(element);
    var activv = myelm.data("active");
    var mmm = "";
    if (activv) {
        mmm = "Are you sure about InActining this Item?";
    } else {
        mmm = "Are you sure about Actining this Item?";
    }
    bootbox.confirm({
        message: mmm,
        buttons: {
            confirm: {
                label: 'Yep',
                className: 'btn-success'
            },
            cancel: {
                label: 'No - Back',
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
                    url: HostUrl + "/Store/CAItem",
                    data: fdata,
                    processData: false,
                    contentType: false
                }).done(function (result) {
                    bootbox.hideAll();
                    bootbox.alert(result.result);
                    renderfoo1();
                    $("#divloader").hide();
                });
            } else {
                $("#divloader").hide();
            }
        }
    });
}

//----------2-----------
function renderfoo2() {
    $('#divloader').show();
    $.getJSON(HostUrl + "/Store/getitemlog", {
    }, function (data) {
        var a = data.result;
        if (a == 0) {
            bootbox.hideAll();
            bootbox.alert("مشکلی پیش آمده است");
            $("#divloader").hide();
        } else {
            $("#foo2>tbody>tr").each(function (index, elem) {
                $(elem).remove();
            });
            foo2.rows.load(a);

            $.each(foo2.rows.all, function (i, row) {
                v = row.val();
                var m = v.id;
                v.active = '<button data-whochange="' + m + '" data-active="' + v.active + '" onclick="iActiveStockLog(this)" class="badge badge-table badge-' + ((v.active != "true") ? 'danger' : 'success') + '">' + ((v.active != "true") ? 'InActive' : 'Active') + '</button>';
                row.val(v);
            });
            $("#divloader").hide();
        }
    });
}
$('a[href^="#tab"]').click(function (event) {
    $('a[href^="#tab"]').removeClass('active');
    $('.tab-pane').removeClass('active');
    $(this).addClass('active');
    $($(this).attr("href")).addClass('active');
});

function iActiveStockLog(element) {
    var myelm = $(element);
    var activv = myelm.data("active");
    var mmm = "";
    if (activv) {
        mmm = "Are you sure about InActining this Log(it will effect on stock number)?";
    } else {
        mmm = "Are you sure about Actining this Log(it will effect on stock number)?";
    }
    bootbox.confirm({
        message: mmm,
        buttons: {
            confirm: {
                label: 'Yep',
                className: 'btn-success'
            },
            cancel: {
                label: 'No - Back',
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
                    url: HostUrl + "/Store/CAStockLog",
                    data: fdata,
                    processData: false,
                    contentType: false
                }).done(function (result) {
                    bootbox.hideAll();
                    bootbox.alert(result.result);
                    renderfoo2();
                    $("#divloader").hide();
                });
            } else {
                $("#divloader").hide();
            }
        }
    });
}

//----------3-----------
function renderfoo3() {
    $('#divloader').show();
    $.getJSON(HostUrl + "/Store/getorders", {
    }, function (data) {
        var a = data.result;
        if (a == 0) {
            bootbox.hideAll();
            bootbox.alert("مشکلی پیش آمده است");
            $("#divloader").hide();
        } else {
            $("#foo3>tbody>tr").each(function (index, elem) {
                $(elem).remove();
            });
            foo3.rows.load(a);
            var co = 0;
            $.each(foo3.rows.all, function (i, row) {
                co += 1;
                v = row.val();
                var m = v.id;
                if (v.orderStep == 2) {
                    v.operation = '<button data-whochange="' + m + '" data-name="' + data.names[co] + '" data-code="' + v.orderCode + '" onclick="verifyOrder(this)" class="badge badge-table badge-warning">Verify this order!</button>';
                }
                else if (v.orderStep == 3) {
                    v.operation = '<button data-whochange="' + m + '" data-name="' + data.names[co] + '" data-code="' + v.orderCode + '" onclick="verifyShipping(this)" class="badge badge-table badge-dark">Ship this order!</button>';
                } else {
                    v.operation = '<a class="text-success">Order Completed</a>';
                }
                v.edit = '<button data-whochange="' + m + '" data-sch="' + data.names[co] + '" data-oc="' + v.orderCode + '" onclick="ieorder(this)" class="badge badge-table badge-info">Edit this Order</button>';
                v.schooldet = '<button data-whochange="' + m + '" data-code="' + v.orderCode + '" onclick="iSchoolDet(this)" class="badge badge-table badge-info">Show Information</button>';
                v.cartdet = '<button data-whochange="' + m + '" data-code="' + v.orderCode + '" onclick="iShowCart(this)" class="badge badge-table badge-info">Show Cart Items</button>';
                v.print = '<button data-whochange="' + m + '" data-discount="' + v.discount + '" data-weight="' + v.weight + '" data-orderTime="' + v.orderTime + '" data-orderDate="' + v.orderDate + '" data-shippingCost="' + v.shippingCost + '" data-shippingType="' + v.shippingType + '" data-totalPrice="' + v.totalPrice + '" data-schoolCode="' + v.schoolCode + '" data-orderCode="' + v.orderCode + '" onclick="iShowPrint(this)" class="badge badge-table badge-info">Show Print Dialog</button>';
                v.paymentInfo = '<button data-whochange="' + m + '" onclick="iShowPayment(this)" data-code="' + v.orderCode + '" class="badge badge-table badge-info">Show Payment Info</button>';
                v.orderSP = '<button data-whochange="' + m + '" onclick="iShoworderSP(this)" class="badge badge-table badge-info">Print Address</button>';
                v.active = '<button data-whochange="' + m + '" data-active="' + v.active + '" onclick="iActiveOrder(this)" class="badge badge-table badge-' + ((v.active != "true") ? 'danger' : 'success') + '">' + ((v.active != "true") ? 'InActive' : 'Active') + '</button>';
                v.schoolName = data.names[co];
                row.val(v);
            });
            $("#divloader").hide();
        }
    });
}

function verifyOrder(element) {
    var myelm = $(element);
    var mestxt1 = "وی سی وی\n" + myelm.data("name") + "\n" + "رزرو شما به شماره پیگیری " + myelm.data("code") + " تایید شد و آماده ارسال می باشد.";
    var dialog = bootbox.dialog({
        className: 'my-modal',
        message: '<h5 class="text-center font-weight-bold">Verify Order: ' + myelm.data("code") + '</h5><hr/><p class="text-center">Attention: Input below is auto generated and it can be changed</p><div class="text-center col-lg-5" style="display: block; margin: 0 auto;"><textarea class="form-control" rows="7" id="mestxt1" style="margin-bottom: 10px" dir="rtl">' + mestxt1 + '</textarea><button type="button" style="margin-bottom: 10px" onclick="vorder(this)" data-whochange="' + myelm.data("whochange") + '" class="btn btn-primary btn-block"><b>Verify</b></button></div>',
        closeButton: true
    });
}

function vBT(element) {
    var myelm = $(element);

    bootbox.confirm({
        message: "Are you sure about verifying this transaction?",
        buttons: {
            confirm: {
                label: 'Yep',
                className: 'btn-success'
            },
            cancel: {
                label: 'No - Back',
                className: 'btn-danger'
            }
        },
        callback: function (result) {
            $("#divloader").show();
            if (result == true) {
                var fdata = new FormData();
                fdata.append("__RequestVerificationToken", gettoken());
                fdata.append("mes", $("#mestxt2")[0].value);
                fdata.append("who", myelm.data("whochange"));
                $.ajax({
                    type: 'post',
                    url: HostUrl + "/Store/vBT",
                    data: fdata,
                    processData: false,
                    contentType: false
                }).done(function (result) {
                    bootbox.hideAll();
                    bootbox.alert(result.result);
                    //renderfoo3();
                    $("#divloader").hide();
                });
            } else {
                $("#divloader").hide();
            }
        }
    });
}

function vorder(element) {
    var myelm = $(element);
    var activv = myelm.data("active");

    bootbox.confirm({
        message: "Are you sure about verifying this order?",
        buttons: {
            confirm: {
                label: 'Yep',
                className: 'btn-success'
            },
            cancel: {
                label: 'No - Back',
                className: 'btn-danger'
            }
        },
        callback: function (result) {
            $("#divloader").show();
            if (result == true) {
                var fdata = new FormData();
                fdata.append("__RequestVerificationToken", gettoken());
                fdata.append("mes", $("#mestxt1")[0].value);
                fdata.append("who", myelm.data("whochange"));
                $.ajax({
                    type: 'post',
                    url: HostUrl + "/Store/vorder",
                    data: fdata,
                    processData: false,
                    contentType: false
                }).done(function (result) {
                    bootbox.hideAll();
                    bootbox.alert(result.result);
                    renderfoo3();
                    $("#divloader").hide();
                });
            } else {
                $("#divloader").hide();
            }
        }
    });
}

function verifyShipping(element) {
    var myelm = $(element);
    var mestxt1 = "وی سی وی\n" + myelm.data("name") + "\n" + "رزرو شما به شماره پیگیری " + myelm.data("code") + " به آدرس شما ارسال شد. اطلاعات ارسال: \n";
    var dialog = bootbox.dialog({
        className: 'my-modal',
        message: '<h5 class="text-center font-weight-bold">Send Order: ' + myelm.data("code") + '</h5><hr/><p class="text-center">Attention: Input below is auto generated and it can be changed</p><div class="text-center col-lg-5" style="display: block; margin: 0 auto;"><textarea class="form-control" rows="7" id="mestxt2" style="margin-bottom: 10px" dir="rtl">' + mestxt1 + '</textarea><button type="button" style="margin-bottom: 10px" onclick="vship(this)" data-whochange="' + myelm.data("whochange") + '" class="btn btn-primary btn-block"><b>Send!</b></button></div>',
        closeButton: true
    });
}

function vship(element) {
    var myelm = $(element);
    var activv = myelm.data("active");

    bootbox.confirm({
        message: "Are you sure about Shipping this order?",
        buttons: {
            confirm: {
                label: 'Yep',
                className: 'btn-success'
            },
            cancel: {
                label: 'No - Back',
                className: 'btn-danger'
            }
        },
        callback: function (result) {
            $("#divloader").show();
            if (result == true) {
                var fdata = new FormData();
                fdata.append("__RequestVerificationToken", gettoken());
                fdata.append("mes", $("#mestxt2")[0].value);
                fdata.append("who", myelm.data("whochange"));
                $.ajax({
                    type: 'post',
                    url: HostUrl + "/Store/vship",
                    data: fdata,
                    processData: false,
                    contentType: false
                }).done(function (result) {
                    bootbox.hideAll();
                    bootbox.alert(result.result);
                    renderfoo3();
                    $("#divloader").hide();
                });
            } else {
                $("#divloader").hide();
            }
        }
    });
}


function iActiveOrder(element) {
    var myelm = $(element);
    var activv = myelm.data("active");
    var mmm = "";
    if (activv) {
        mmm = "Are you sure about InActining this Order?";
    } else {
        mmm = "Are you sure about Actining this Order?";
    }
    bootbox.confirm({
        message: mmm,
        buttons: {
            confirm: {
                label: 'Yep',
                className: 'btn-success'
            },
            cancel: {
                label: 'No - Back',
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
                    url: HostUrl + "/Store/CAOrder",
                    data: fdata,
                    processData: false,
                    contentType: false
                }).done(function (result) {
                    bootbox.hideAll();
                    bootbox.alert(result.result);
                    renderfoo3();
                    $("#divloader").hide();
                });
            } else {
                $("#divloader").hide();
            }
        }
    });
}


function iShoworderSP(element) {
    var myelm = $(element);

    $.getJSON(HostUrl + "/Store/getPrint", {
        Oid: myelm.data("whochange")
    }, function (data) {
        var a = data.result;
        if (a == 0) {
            bootbox.hideAll();
            bootbox.alert("مشکلی پیش آمده است");
            $("#divloader").hide();
        } else {
            var adiv = "<div id='printdiv2'>";
            var num = "09204204700";
            adiv += '<div class="text-center" style="font-size: 17px;"> <img src= "vcv.png" style="max-width: 130px" alt="VCV English"/> <br/> <b>گروه آموزشی وی سی وی (تهیه، تدوین و توزیع کتب آموزش انگلیسی)</b> <br/>' + num.toIndiaDigits() + '<br/> <a>www.vcv.ir</a> </div> <div class="text-right" dir="rtl" style="font-size: 25px; font-weight: bold;">' + 'گیرنده: ' + '<div class="card"><div class="card-block">' + a.city + " (" + a.province + ")" + '<br/>' + a.address.toIndiaDigits() + '<br/>' + a.name.toIndiaDigits() + '<br/>' + a.phone.toIndiaDigits() + " - " + a.mobile.toIndiaDigits() + '</div></div></div>';
            adiv += '<hr/><br/>';
            adiv += '<div class="text-center" style="font-size: 17px;"> <img src= "vcv.png" style="max-width: 130px" alt="VCV English"/> <br/> <b>گروه آموزشی وی سی وی (تهیه، تدوین و توزیع کتب آموزش انگلیسی)</b> <br/>' + num.toIndiaDigits() + '<br/> <a>www.vcv.ir</a> </div> <div class="text-right" dir="rtl" style="font-size: 25px; font-weight: bold;">' + 'گیرنده: ' + '<div class="card"><div class="card-block">' + a.city + " (" + a.province + ")" + '<br/>' + a.address.toIndiaDigits() + '<br/>' + a.name.toIndiaDigits() + '<br/>' + a.phone.toIndiaDigits() + " - " + a.mobile.toIndiaDigits() + '</div></div></div>';
            adiv += "</div><hr/><div class='text-center'><button onclick='printthis2()' class='btn btn-info'>Print This</button></div>";

            var dialog = bootbox.dialog({
                title: 'Print Address',
                className: 'my-modal',
                message: adiv,
                closeButton: true,
                size: 'large'
            });

            $("#divloader").hide();

        }
    });
}

function iShowPrint(element) {
    var myelm = $(element);
    $.getJSON(HostUrl + "/Store/getPrint", {
        Oid: myelm.data("whochange")
    }, function (data) {
        var buyitems = data.result;
        if (buyitems == 0) {
            bootbox.hideAll();
            bootbox.alert("مشکلی پیش آمده است");
            $("#divloader").hide();
        } else {
            var a = data.result;
            var buyitems = a.buy;
            var myelm = $(element);

            var adiv = "<div id='printdiv'>";
            adiv += '<div class="text-center"> <h4>فاکتور فروش</h4> </div> <div class="text-right">' + 'شماره فاکتور: ' + myelm.data("ordercode").toString().toIndiaDigits() + ' - تاریخ ثبت فاکتور: ' + myelm.data("orderdate").toString().toIndiaDigits() + '</div> <div class="text-right" dir="rtl"> شماره مشتری: ' + myelm.data("schoolcode").toString().toIndiaDigits() + '<br>خریدار: ' + a.name + ' <br>آدرس: ' + a.city + "(" + a.province + ")" + ' - ' + a.address.toIndiaDigits() + '    -    تلفن: ' + a.phone.toIndiaDigits() + '</div>';
            var tbl = '<table class="table table-hover" dir="rtl"> <thead> <tr> <th class="text-right">ردیف</th> <th class="text-right">توضیح</th> <th class="text-right">تعداد</th> <th class="text-right">قیمت</th> <th class="text-right">تخفیف</th> <th class="text-center">جمع</th> </tr></thead> <tbody id="cartbody">';
            for (var j = 0; j < buyitems.length; j++) {
                //var btn = '<button data-id="' + j + '" onclick="deleteitem(this)" class="badge badge-table badge-danger">حذف این آیتم</button>';
                tbl += '<tr> <td class="text-right">' + (j + 1) + '</td><td class="text-right" dir="ltr">' + buyitems[j].name + '</td><td class="text-right">' + buyitems[j].count.toString().toIndiaDigits() + '</td><td class="text-right">' + buyitems[j].price.toString().toIndiaDigits() + '</td><td class="text-right">' + buyitems[j].discount.toString().toIndiaDigits() + '%</td><td class="text-center">' + buyitems[j].total.toString().toIndiaDigits() + '</td>';
            }
            tbl += '</tbody> </table>';
            adiv += tbl;
            adiv += '<div class="text-left">' + a.zip.toIndiaDigits() + '</div>';

            adiv += "</div><hr/><div class='text-center'><button onclick='printthis()' class='btn btn-info'>Print This</button></div>";

            var dialog = bootbox.dialog({
                title: 'Print Invoice: ' + myelm.data("ordercode"),
                className: 'my-modal',
                message: adiv,
                closeButton: true,
                size: 'large'
            });

            $("#divloader").hide();
        }
    });
}

String.prototype.toIndiaDigits = function () {
    var id = ['۰', '۱', '۲', '۳', '۴', '۵', '۶', '۷', '۸', '۹'];
    return this.replace(/[0-9]/g, function (w) {
        return id[+w]
    }).toLocaleString();
}

function printthis() {
    $('#printdiv').printThis({
        base: "https://vcv.ir",
        debug: true,
        importCSS: true,        // import parent page css
        importStyle: true,     // import style tags
        printContainer: true,   // print outer container/$.selector
        //pageTitle: "VCV Invoice",          // add title to print page
        printDelay: 50,        // variable print delay
        formValues: false,       // preserve input/form values
        canvas: false,          // copy canvas content (experimental)
        base: false            // preserve the BASE tag, or accept a string for the URL
    });
    //$('#printdiv').printThis({
    //    base: "https://vcv.ir",
    //    debug: false,
    //    importCSS: true,        // import parent page css
    //    importStyle: true,     // import style tags
    //    printContainer: true,   // print outer container/$.selector
    //    pageTitle: "VCV Invoice",          // add title to print page
    //    printDelay: 50,        // variable print delay
    //    formValues: false,       // preserve input/form values
    //    canvas: false,          // copy canvas content (experimental)
    //    base: false            // preserve the BASE tag, or accept a string for the URL
    //});
}


function printthis2() {
    $('#printdiv2').printThis({
        base: "https://vcv.ir",
        debug: true,
        importCSS: true,        // import parent page css
        importStyle: true,     // import style tags
        printContainer: true,   // print outer container/$.selector
        //pageTitle: "VCV Invoice",          // add title to print page
        printDelay: 50,        // variable print delay
        formValues: false,       // preserve input/form values
        canvas: false,          // copy canvas content (experimental)
        base: false            // preserve the BASE tag, or accept a string for the URL
    });
    //$('#printdiv').printThis({
    //    base: "https://vcv.ir",
    //    debug: false,
    //    importCSS: true,        // import parent page css
    //    importStyle: true,     // import style tags
    //    printContainer: true,   // print outer container/$.selector
    //    pageTitle: "VCV Invoice",          // add title to print page
    //    printDelay: 50,        // variable print delay
    //    formValues: false,       // preserve input/form values
    //    canvas: false,          // copy canvas content (experimental)
    //    base: false            // preserve the BASE tag, or accept a string for the URL
    //});
}

function iShowPayment(element) {
    var myelm = $(element);

    $.getJSON(HostUrl + "/Store/getPaymentdet", {
        Oid: myelm.data("whochange")
    }, function (data) {
        if (data.result == 0) {
            bootbox.hideAll();
            bootbox.alert("مشکلی پیش آمده است");
            $("#divloader").hide();
        } else {
            if (data.result.paymentType == "Online Payment") {
                var th = '';
                th += '<th class="text-right">orderTotal</th>';
                th += '<th class="text-right">discount</th>';
                th += '<th class="text-right">shippingCost</th>';
                th += '<th class="text-right">shippingType</th>';
                th += '<th class="text-right">paymentType</th>';
                th += '<th class="text-right">balanceWithdrawal</th>';
                th += '<th class="text-right">totalp</th>';
                th += '<th class="text-right">dargahP</th>';
                th += '<th class="text-right">gate</th>';
                th += '<th class="text-right">description</th>';
                th += '<th class="text-right">authority</th>';
                th += '<th class="text-right">refId</th>';
                th += '<th class="text-right">success</th>';

                var tbl = '<table class="table table-hover" dir="rtl"> <thead> <tr>' + th + '</tr></thead> <tbody id="cartbody">';
                var res = data.result;
                var td = '';
                td += '<td class="text-right">' + res.orderTotal + '</td>';
                td += '<td class="text-right">' + res.discount + '</td>';
                td += '<td class="text-right">' + res.shippingCost + '</td>';
                td += '<td class="text-right">' + res.shippingType + '</td>';
                td += '<td class="text-right">' + res.paymentType + '</td>';
                td += '<td class="text-right">' + res.balanceWithdrawal + '</td>';
                td += '<td class="text-right">' + res.totalp + '</td>';
                td += '<td class="text-right">' + res.dargahP + '</td>';
                td += '<td class="text-right">' + res.gate + '</td>';
                td += '<td class="text-right">' + res.description + '</td>';
                td += '<td class="text-right">' + res.authority + '</td>';
                td += '<td class="text-right">' + res.refId + '</td>';
                td += '<td class="text-right">' + res.success + '</td>';
                tbl += '<tr>' + td + '</tr>';

                tbl += '</tbody> </table>';
                var myelm = $(element);

                var dialog = bootbox.dialog({
                    title: 'Payment Info: ' + myelm.data("code"),
                    className: 'my-modal',
                    message: tbl,
                    closeButton: true,
                    size: 'large'
                });
            }
            else {
                if (data.result.success == true) {
                    var th = '';
                    th += '<th class="text-right">orderTotal</th>';
                    th += '<th class="text-right">discount</th>';
                    th += '<th class="text-right">shippingCost</th>';
                    th += '<th class="text-right">shippingType</th>';
                    th += '<th class="text-right">paymentType</th>';
                    th += '<th class="text-right">description</th>';
                    th += '<th class="text-right">success</th>';

                    var tbl = '<table class="table table-hover" dir="rtl"> <thead> <tr>' + th + '</tr></thead> <tbody id="cartbody">';
                    var res = data.result;
                    var td = '';
                    td += '<td class="text-right">' + res.orderTotal + '</td>';
                    td += '<td class="text-right">' + res.discount + '</td>';
                    td += '<td class="text-right">' + res.shippingCost + '</td>';
                    td += '<td class="text-right">' + res.shippingType + '</td>';
                    td += '<td class="text-right">' + res.paymentType + '</td>';
                    td += '<td class="text-right">' + res.description + '</td>';
                    td += '<td class="text-right">' + res.success + '</td>';
                    tbl += '<tr>' + td + '</tr>';

                    tbl += '</tbody> </table>';
                    var myelm = $(element);

                    var dialog = bootbox.dialog({
                        title: 'Payment Info: ' + myelm.data("code"),
                        className: 'my-modal',
                        message: tbl,
                        closeButton: true
                    });
                } else {
                    var myelm = $(element);
                    var dialog = bootbox.dialog({
                        className: 'my-modal',
                        message: '<h5 class="text-center font-weight-bold">Verify transaction of order: ' + myelm.data("code") + '</h5><hr/><p class="text-center">Attention: if the money transfered is lower or larger than this order amount, do this function with user balance<hr/>Do you confirm this user transfered ' + data.result.orderTotal + ' Rials - please insert descreption of transaction below here</p><div class="text-center col-lg-5" style="display: block; margin: 0 auto;"><textarea class="form-control" rows="7" id="mestxt2" place-holder="transaction descreaption" style="margin-bottom: 10px" dir="rtl"></textarea><button type="button" style="margin-bottom: 10px" onclick="vBT(this)" data-whochange="' + myelm.data("whochange") + '" class="btn btn-primary btn-block"><b>Verify</b></button></div>',
                        closeButton: true
                    });
                }
            }
            $("#divloader").hide();
        }
    });
}

function iShowCart(element) {
    var myelm = $(element);

    $.getJSON(HostUrl + "/Store/getCartdet", {
        Oid: myelm.data("whochange")
    }, function (data) {
        var buyitems = data.result;
        if (buyitems == 0) {
            bootbox.hideAll();
            bootbox.alert("مشکلی پیش آمده است");
            $("#divloader").hide();
        } else {
            var tbl = '<table class="table table-hover" dir="rtl"> <thead> <tr> <th class="text-right">#</th> <th class="text-right">Book</th> <th class="text-right">Number</th> <th class="text-right">Price</th> <th class="text-right">Dicount</th> <th class="text-center">Total</th> </tr></thead> <tbody id="cartbody">';
            for (var j = 0; j < buyitems.length; j++) {
                //var btn = '<button data-id="' + j + '" onclick="deleteitem(this)" class="badge badge-table badge-danger">حذف این آیتم</button>';
                tbl += '<tr> <td class="text-right">' + (j + 1) + '</td><td class="text-right" dir="ltr">' + buyitems[j].name + '</td><td class="text-right">' + buyitems[j].count + '</td><td class="text-right">' + buyitems[j].price + '</td><td class="text-right">' + buyitems[j].discount + '%</td><td class="text-center">' + buyitems[j].total + '</td>';
            }
            tbl += '</tbody> </table>';

            var dialog = bootbox.dialog({
                title: 'Invoice: ' + myelm.data("code"),
                className: 'my-modal',
                message: tbl,
                closeButton: true,
                size: 'large'
            });

            $("#divloader").hide();
        }
    });
}

function iSchoolDet(element) {
    var myelm = $(element);

    $.getJSON(HostUrl + "/Store/getSchooldet", {
        Oid: myelm.data("whochange")
    }, function (data) {
        var buyitems = data.result;
        if (buyitems == 0) {
            bootbox.hideAll();
            bootbox.alert("مشکلی پیش آمده است");
            $("#divloader").hide();
        } else {
            var tbl = '<table class="table table-hover" dir="rtl"> <thead> <tr> <th class="text-right">School Name</th> <th class="text-right">Province</th> <th class="text-right">City</th> <th class="text-right">Address</th> <th class="text-center">ZipCode</th> <th class="text-center">Phone</th> <th class="text-center">Mobile</th> </tr></thead> <tbody id="cartbody2">';
            //var btn = '<button data-id="' + j + '" onclick="deleteitem(this)" class="badge badge-table badge-danger">حذف این آیتم</button>';
            tbl += '<tr> <td class="text-right">' + buyitems.name + '</td><td class="text-right" dir="ltr">' + buyitems.province + '</td><td class="text-right">' + buyitems.city + '</td><td class="text-right">' + buyitems.address + '</td><td class="text-right">' + buyitems.zip + '</td><td class="text-center">' + buyitems.phone + '</td><td class="text-center">' + buyitems.mobile + '</td>';
            tbl += '</tbody> </table>';

            var dialog = bootbox.dialog({
                title: 'Information of School Code: ' + myelm.data("code"),
                className: 'my-modal',
                message: tbl,
                closeButton: true,
                size: 'large'
            });

            $("#divloader").hide();
        }
    });
}

//----------4-----------
function renderfoo4() {
    $('#divloader').show();
    $.getJSON(HostUrl + "/Store/getPayment", {
    }, function (data) {
        var a = data.result;
        if (a == 0) {
            bootbox.hideAll();
            bootbox.alert("مشکلی پیش آمده است");
            $("#divloader").hide();
        } else {
            $("#foo4>tbody>tr").each(function (index, elem) {
                $(elem).remove();
            });
            foo4.rows.load(a);
            $.each(foo4.rows.all, function (i, row) {
                v = row.val();
                var m = v.id;
                v.active = '<button data-whochange="' + m + '" data-active="' + v.active + '" onclick="iActivePayment(this)" class="badge badge-table badge-' + ((v.active == "true") ? 'danger' : 'success') + '">' + ((v.active == "true") ? 'InActive' : 'Active') + '</button>';
                row.val(v);
            });
            $("#divloader").hide();
        }
    });
}

function iActivePayment(element) {
    var myelm = $(element);
    var activv = myelm.data("active");
    var mmm = "";
    if (activv) {
        mmm = "Are you sure about InActining this Payment record?";
    } else {
        mmm = "Are you sure about Actining this Payment record?";
    }
    bootbox.confirm({
        message: mmm,
        buttons: {
            confirm: {
                label: 'Yep',
                className: 'btn-success'
            },
            cancel: {
                label: 'No - Back',
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
                    url: HostUrl + "/Store/CAPayment",
                    data: fdata,
                    processData: false,
                    contentType: false
                }).done(function (result) {
                    bootbox.hideAll();
                    bootbox.alert(result.result);
                    renderfoo4();
                    $("#divloader").hide();
                });
            } else {
                $("#divloader").hide();
            }
        }
    });
}
//----------5-----------
function renderfoo5() {
    $('#divloader').show();
    $.getJSON(HostUrl + "/Store/getSchool", {
    }, function (data) {
        var a = data.result;
        if (a == 0) {
            bootbox.hideAll();
            bootbox.alert("مشکلی پیش آمده است");
            $("#divloader").hide();
        } else {
            $("#foo5>tbody>tr").each(function (index, elem) {
                $(elem).remove();
            });
            foo5.rows.load(a);
            $.each(foo5.rows.all, function (i, row) {
                v = row.val();
                var m = v.id;
                v.ibalance = '<button data-whochange="' + m + '" data-sch="' + v.schoolNameFa + '" data-nowbal="' + v.balance + '" onclick="icbalance(this)" class="badge badge-table badge-info">increase of decrease balance</button>';

                v.iorder = '<button data-whochange="' + m + '" data-sch="' + v.schoolNameFa + '" onclick="icorder(this)" class="badge badge-table badge-info">Order for school</button>';

                row.val(v);
            });
            $("#divloader").hide();
        }
    });
}

function icbalance(element) {
    var myelm = $(element);

    var dialog = bootbox.dialog({
        className: 'my-modal',
        message: '<h5 class="text-center font-weight-bold">Add Balance for: ' + myelm.data("sch") + '</h5><hr/><p class="text-center">Attention: The number can be negative or positive</p><div class="text-center col-lg-5" style="display: block; margin: 0 auto;"><input placeholder="Number" type="Number" class="form-control text-center" style="margin-bottom: 10px" id="inum2"><input placeholder="Description" type="text" class="form-control text-center" style="margin-bottom: 10px" id="ides2"><button type="button" style="margin-bottom: 10px" onclick="addbalance(this)" data-whochange="' + myelm.data("whochange") + '" class="btn btn-primary btn-block"><b>Save Changes</b></button></div>',
        closeButton: true
    });
}

function addbalance(element) {
    var myelm = $(element);
    $("#divloader").show();
    var fdata = new FormData();
    fdata.append("__RequestVerificationToken", gettoken());
    fdata.append("ides", $("#ides2").val());
    fdata.append("inum", $("#inum2").val());
    fdata.append("who", myelm.data("whochange"));

    $.ajax({
        type: 'post',
        url: HostUrl + "/Store/addbal",
        data: fdata,
        processData: false,
        contentType: false
    }).done(function (result) {
        if (result.result == 0) {
            bootbox.hideAll();
            bootbox.alert("مشکلی رخ داده است");
            $("#divloader").hide();
        } else {
            renderfoo5();
            bootbox.hideAll();
            bootbox.alert(result.result);
            $("#divloader").hide();
        }
    });
}


function icorder(element) {
    var myelm = $(element);
    var dialog = bootbox.dialog({
        className: 'my-modal',
        message: '<h5 class="text-center font-weight-bold">Order books for: ' + myelm.data("sch") + '</h5><hr/><div class="text-center col-lg-5" style="display: block; margin: 0 auto;"><button type="button" style="margin-bottom: 10px" onclick="gotoopage(this)" data-whochange="' + myelm.data("whochange") + '" class="btn btn-primary btn-block"><b>Go to order page</b></button></div>',
        closeButton: true
    });
}

function ieorder(element) {
    var myelm = $(element);
    var dialog = bootbox.dialog({
        className: 'my-modal',
        message: '<h5 class="text-center font-weight-bold">Edit Order ' + myelm.data("oc")+' for: ' + myelm.data("sch") + '</h5><hr/><div class="text-center col-lg-5" style="display: block; margin: 0 auto;"><button type="button" style="margin-bottom: 10px" onclick="gotooeditpage(this)" data-whochange="' + myelm.data("whochange") + '" class="btn btn-primary btn-block"><b>Go to edit order page</b></button></div>',
        closeButton: true
    });
}

function gotooeditpage(element) {
    var myelm = $(element);
    var fdata = new FormData();
    fdata.append("__RequestVerificationToken", gettoken());
    fdata.append("order", myelm.data("whochange"));

    $.ajax({
        type: 'post',
        url: HostUrl + "/Store/genosessid2",
        data: fdata,
        processData: false,
        contentType: false
    }).done(function (result) {
        if (result.result == 0) {
            bootbox.hideAll();
            bootbox.alert("مشکلی رخ داده است");
            $("#divloader").hide();
        } else {
            var win = window.open('/OrderEdit', '_blank');
            if (win) {
                //Browser has allowed it to be opened
                win.focus();
            } else {
                //Browser has blocked it
                alert('Please allow popups for this website or go to /OrderEdit');
            }
        }
    });
}

function gotoopage(element) {
    var myelm = $(element);
    var fdata = new FormData();
    fdata.append("__RequestVerificationToken", gettoken());
    fdata.append("sch", myelm.data("whochange"));

    $.ajax({
        type: 'post',
        url: HostUrl + "/Store/genosessid",
        data: fdata,
        processData: false,
        contentType: false
    }).done(function (result) {
        if (result.result == 0) {
            bootbox.hideAll();
            bootbox.alert("مشکلی رخ داده است");
            $("#divloader").hide();
        } else {
            var win = window.open('/Orders', '_blank');
            if (win) {
                //Browser has allowed it to be opened
                win.focus();
            } else {
                //Browser has blocked it
                alert('Please allow popups for this website or go to /Orders');
            }
        }
    });
}

//----------6-----------
function renderfoo6() {
    $('#divloader').show();
    $.getJSON(HostUrl + "/Store/getballog", {
    }, function (data) {
        var a = data.result;
        if (a == 0) {
            bootbox.hideAll();
            bootbox.alert("مشکلی پیش آمده است");
            $("#divloader").hide();
        } else {
            $("#foo6>tbody>tr").each(function (index, elem) {
                $(elem).remove();
            });
            foo6.rows.load(a);

            $.each(foo6.rows.all, function (i, row) {
                v = row.val();
                var m = v.id;
                v.active = '<button data-whochange="' + m + '" data-active="' + v.active + '" onclick="iActiveBalLog(this)" class="badge badge-table badge-' + ((v.active != "true") ? 'danger' : 'success') + '">' + ((v.active != "true") ? 'InActive' : 'Active') + '</button>';
                row.val(v);
            });
            $("#divloader").hide();
        }
    });
}

function iActiveBalLog(element) {
    var myelm = $(element);
    var activv = myelm.data("active");
    var mmm = "";
    if (activv) {
        mmm = "Are you sure about InActining this Log(it will effect on Balance)?";
    } else {
        mmm = "Are you sure about Actining this Log(it will effect on Balance)?";
    }
    bootbox.confirm({
        message: mmm,
        buttons: {
            confirm: {
                label: 'Yep',
                className: 'btn-success'
            },
            cancel: {
                label: 'No - Back',
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
                    url: HostUrl + "/Store/CABalLog",
                    data: fdata,
                    processData: false,
                    contentType: false
                }).done(function (result) {
                    bootbox.hideAll();
                    bootbox.alert(result.result);
                    renderfoo6();
                    $("#divloader").hide();
                });
            } else {
                $("#divloader").hide();
            }
        }
    });
}
