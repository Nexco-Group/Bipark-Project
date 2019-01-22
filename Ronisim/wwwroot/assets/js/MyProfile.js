$(document).ready(function () {
    //$('a[href$="EditInformation"]').click();

    $("#divloader").hide();

    refreshOrderList();

    $("#ChangePasswordbtn").click(function () {
        $("#divloader").show();
        var fdata = new FormData();

        fdata.append("cp", $('#CurrentPassInput').val());
        fdata.append("np", $('#NewPassInput').val());
        fdata.append("renp", $('#ReNewPassInput').val());
        fdata.append("__RequestVerificationToken", gettoken());

        $.ajax({
            type: 'post',
            url: HostUrl + "/iUser/ChangePassword",
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

    $("#cimgfile").hide();
    $("#changeimg").click(function () {
        $("#cimgfile").click();
    });

    $("#cimgfile").change(function () {
        var fileName = $(this).val();
        bootbox.confirm({
            message: "آیا از تغییر لوگوی آموزشگاه خود اطمینان دارید؟",
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
                    var fileInput = $('#cimgfile')[0];
                    var file = fileInput.files[0];
                    fdata.append("uploadimage", file);
                    fdata.append("__RequestVerificationToken", gettoken());

                    $.ajax({
                        type: 'post',
                        url: HostUrl + "/MyProfile/UploadImage",
                        data: fdata,
                        processData: false,
                        contentType: false
                    }).done(function (result) {
                        if (result.result == "1") {
                            $('#profileimage').attr("src", HostUrl + "/ProfileImage/" + result.image);
                            bootbox.alert('عکس شما با موفقیت تغییر کرد');
                        }
                        else {
                            bootbox.alert(result.result);
                        }
                        $("#divloader").hide();
                    });
                } else {
                    $("#divloader").hide();
                }
            }
        });
        img.src = _URL.createObjectURL(element);
    });
});


