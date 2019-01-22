var foo7;
$(document).ready(function () {
    $("#divloader").hide();
    foo7 = FooTable.init('#foo7');

});


function fitertime() {
    $('#divloader').show();
    $.getJSON(HostUrl + "/iUser/getRequest", {
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

                v.sood = (parseInt(v.money2) - parseInt(v.money)).toLocaleString();
                v.money = parseInt(v.money).toLocaleString();
                v.money2 = parseInt(v.money2).toLocaleString();

                row.val(v);
            });

            foo7.gotoPage(1);
            $("#divloader").hide();
        }
    });
}

function fitertimeexcel() {
    $('#divloader').show();
    $.getJSON(HostUrl + "/iUser/getRequestExcel", {
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
    $.getJSON(HostUrl + "/iUser/getRequestPDF", {
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
            //var win = window.open(a, '_blank');
            //win.focus();
            //window.open("http://www.google.com", '_blank');
            //window.open(a, '_blank');
            $("#divloader").hide();
        }
    });
}



$("#saverequestbtn").click(function () {
    $("#divloader").show();
    var fdata = new FormData();

    fdata.append("pack", $('#combopack').val());
    fdata.append("pmobile", $('#pmobile').val());
    fdata.append("__RequestVerificationToken", gettoken());

    $.ajax({
        type: 'post',
        url: HostUrl + "/iUser/SaveRequest",
        data: fdata,
        processData: false,
        contentType: false
    }).done(function (result) {
        $("#divloader").hide();
        if (result.result == 1) {
            bootbox.alert("درخواست با موفقیت ثبت شد");
            refreshOrderList();
        }
        else {
            bootbox.alert(result.result);
        }
    });
});

//function refreshOrderList() {
//    $.getJSON(HostUrl + "/iUser/getOlist", {
//    }, function (data) {
//        var a = data.result;
//        if (a == 1) {
//            bootbox.hideAll();
//            bootbox.alert("مشکلی پیش آمده است");
//            $("#divloader").hide();
//            $("#orderlist").html("");
//        } else if (a.length == 0) {
//            bootbox.hideAll();
//            $("#orderlist").html('<li class="list-group-item"> <div class="media"> <div class="media-body"> <h6 class="mt-0 mb-5 hover"> رزروی ثبت نشده است </h6> </div></div></li>');
//        }
//        else {
//            $("#orderlist").html("");
//            var retdiv = "";
//            for (var i = 0; i < a.length; i++) {
//                var obj = a[i];
//                var m = obj.id;
//                var pluss = "";
//                if (obj.status == 0) {
//                    pluss = '<button data-whochange="' + m + '" data-nowactive="0" onclick="changerqstatus(this)" class="badge badge-table badge-danger btn-round btn-xs">لغو شده</button>';
//                }
//                else if (obj.status == 1) {
//                    pluss = '<button data-whochange="' + m + '" data-nowactive="1" onclick="changerqstatus(this)" class="badge badge-table badge-warning btn-round btn-xs">ثبت شده - منتظر تایید</button>';
//                } else {
//                    pluss = '<button class="badge badge-table badge-success btn-round btn-xs">تایید شده</button>';
//                }
//                retdiv += '<li class="list-group-item"> <div class="media"> <div class="media-body"> <h6 class="mt-0 mb-5 hover"> ' + obj.who + ' </h6> <small>' + obj.orderDate + ' - ' + obj.orderTime + '</small> </div><div class="pt-10 pb-10 p-0"> ' + pluss + ' </div></div></li>';
//            }
//            $("#orderlist").html(retdiv);
//        }
//    });
//    //pendings
//    $.getJSON(HostUrl + "/iUser/getOPlist", {
//    }, function (data) {
//        var a = data.result;
//        if (a == 1) {
//            bootbox.hideAll();
//            bootbox.alert("مشکلی پیش آمده است");
//            $("#divloader").hide();
//            $("#porderlist").html("");
//        } else if (a.length == 0) {
//            bootbox.hideAll();
//            $("#porderlist").html('<li class="list-group-item"> <div class="media"> <div class="media-body"> <h6 class="mt-0 mb-5 hover"> رزروی ثبت نشده است </h6> </div></div></li>');
//        }
//        else {
//            $("#porderlist").html("");
//            var retdiv = "";
//            for (var i = 0; i < a.length; i++) {
//                var obj = a[i];
//                var m = obj.id;
//                var pluss = "";
//                if (obj.status == 0) {
//                    pluss = '<button data-whochange="' + m + '" data-nowactive="0" onclick="changerqstatus(this)" class="badge badge-table badge-danger btn-round btn-xs">لغو شده</button>';
//                }
//                else if (obj.status == 1) {
//                    pluss = '<button data-whochange="' + m + '" data-nowactive="1" onclick="changerqstatus(this)" class="badge badge-table badge-warning btn-round btn-xs">ثبت شده - منتظر تایید</button>';
//                } else {
//                    pluss = '<button class="badge badge-table badge-success btn-round btn-xs">تایید شده</button>';
//                }
//                retdiv += '<li class="list-group-item"> <div class="media"> <div class="media-body"> <h6 class="mt-0 mb-5 hover"> ' + obj.who + ' </h6> <small>' + obj.orderDate + ' - ' + obj.orderTime + '</small> </div><div class="pt-10 pb-10 p-0"> ' + pluss + ' </div></div></li>';
//            }
//            $("#porderlist").html(retdiv);
//        }

//    });

//}


function changerqstatus(element) {
    var myelm = $(element);
    console.log(myelm.data("nowactive"));
    var activv = myelm.data("nowactive");
    var mmm = "";
    if (activv==0) {
        mmm = "آیا از فعال کردن این درخواست اطمینان دارید؟";
    } else if (activv == 1) {
        mmm = "آیا از لغو این درخواست اطمینان دارید؟";
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
                    url: HostUrl + "/iUser/Changrqstatus",
                    data: fdata,
                    processData: false,
                    contentType: false
                }).done(function (result) {
                    bootbox.alert(result.result);
                    if (activv == 0) {
                        myelm.removeClass("badge-danger");

                        myelm.addClass("badge-warning");

                        myelm.html("ثبت شده - منتظر تایید");
                        myelm.data("nowactive", 1);
                    } else if (activv == 1) {
                        myelm.removeClass("badge-warning");

                        myelm.addClass("badge-danger");
                        myelm.html("لغو شده");
                        myelm.data("nowactive", 0);
                    }
                    $("#divloader").hide();
                    refreshOrderList();
                });
            } else {
                $("#divloader").hide();
            }
        }
    });
}

