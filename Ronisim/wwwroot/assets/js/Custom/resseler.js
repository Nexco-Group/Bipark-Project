$(document).ready(function () {
    $("#divloader").hide();
    refreshOrderList3();
    setTimeout(function () {
        setInterval(function () { refreshOrderList3(); }, 10000);
    }, 10000);
});

var backup = "";

//$('#combotime').on('change', function () {
//    if (backup == "") {
//        backup = $("#combopack").html();
//    }
//    else {
//        $("#combopack").html(backup);
//    }
//    var sa = this.value.split("-");
//    var vl = sa[0];
//    var tp = sa[1];
//    $("#combopack option").each(function () {
//        if ($(this).data("date") != vl || $(this).data("type") != tp) {
//            $(this).remove();
//            console.log($(this).data("date") + '-' + $(this).data("type"));
//        } else {
//            var sas = $(this).text();
//            sas = sas.replace(' - ' + $(this).text().split(' - ')[1], '');
//            $(this).text(sas);
//        }
//    });
//    //$('#combopack').prop('selectedIndex', 0);
//});

$("#saverequestbtn").click(function () {
    $("#divloader").show();
    var fdata = new FormData();

    fdata.append("pack", $('#combopack').val());

    fdata.append("fd", $('#fromDate2').val());
    fdata.append("ft", $('#combotime').val());
    fdata.append("td", $('#toDate2').val());
    fdata.append("tt", $('#combotime2').val());

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
        }
        else {
            bootbox.alert(result.result);
        }
    });
});

function refreshOrderList3() {
    $("#orderlist").html('<div class="media"> <div class="media-body"> <a href="/iUser/Requests"> رفتن به صفحه همه درخواست ها </a> </div></div>');

    //pendings
    $.getJSON(HostUrl + "/iUser/getOPlist", {
    }, function (data) {
        var a = data.result;
        if (a == 1) {
            bootbox.hideAll();
            bootbox.alert("مشکلی پیش آمده است");
            $("#divloader").hide();
            $("#porderlist").html("");
        } else if (a.length == 0) {
            bootbox.hideAll();
            $("#porderlist").html('<li class="list-group-item"> <div class="media"> <div class="media-body"> <h6 class="mt-0 mb-5 hover"> رزروی ثبت نشده است </h6> </div></div></li>');
        }
        else {
            $("#porderlist").html("");
            var retdiv = "";
            for (var i = 0; i < a.length; i++) {
                var obj = a[i];
                var m = obj.id;
                var pluss = "";
                if (obj.status == 0) {
                    pluss = '<button data-whochange="' + m + '" data-nowactive="0" onclick="changerqstatus(this)" class="badge badge-table badge-danger btn-round btn-xs">لغو شده</button>';
                }
                else if (obj.status == 1) {
                    pluss = '<button data-whochange="' + m + '" data-nowactive="1" onclick="changerqstatus(this)" class="badge badge-table badge-warning btn-round btn-xs">ثبت شده - منتظر تایید</button>';
                } else if (obj.status == 2) {
                    pluss = '<button class="badge badge-table badge-success btn-round btn-xs">تایید شده</button>';
                }
                else {
                    pluss = '<button class="badge badge-table badge-danger btn-round btn-xs">لغو شده توسط ادمین</button>';
                }
                retdiv += '<li class="list-group-item"> <div class="media"> <div class="media-body"> <h6 class="mt-0 mb-5 hover"> ' + obj.who + ' </h6> <small>' + obj.orderDate + ' - ' + obj.orderTime + '</small> </div><div class="pt-10 pb-10 p-0"> ' + pluss + ' </div></div></li>';
            }
            $("#porderlist").html(retdiv);
        }

    });
    
}

