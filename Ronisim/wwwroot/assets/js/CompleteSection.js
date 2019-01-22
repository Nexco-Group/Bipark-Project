$(document).ready(function () {

    $("#divloader").hide();

    $('#comboprovince').on('change', function () {
        $("#divloader").show();
        var vl = this.value;
        $.getJSON(HostUrl + "/api/getcity", { provinveid: vl },
            function (data) {
                $('#combocity').html(data.result);
                $("#divloader").hide();
            }
        );
    });

    $("#step3btn").click(function () {
        $("#divloader").show();

        var fdata = new FormData();
        var fileInput = $('#uploadimage')[0];
        var file = fileInput.files[0];
        fdata.append("uploadimage", file);
        fdata.append("__RequestVerificationToken", gettoken());
        fdata.append("schnamefa", $('#schnamefa').val());
        fdata.append("schnameen", $('#schnameen').val());
        fdata.append("schphone", $('#schphone').val());
        fdata.append("position", $('#position').val());
        fdata.append("combocity", $('#combocity').val());
        fdata.append("address", $('#address').val());
        fdata.append("zipcode", $('#zipcode').val());

        $.ajax({
            type: 'post',
            url: HostUrl + "/MyProfile/Step3",
            data: fdata,
            processData: false,
            contentType: false
        }).done(function (result) {
            if (result.result == 1) {
                bootbox.alert('درخواست عضویت شما با موفقیت ثبت شد و منتظر تایید وی سی وی می باشد');
                setTimeout(
                    function () {
                        location.reload();
                    }, 2000);
            }
            else {
                bootbox.alert(result.result);
            }
            $("#divloader").hide();
        });
    });

    $('.dropify').dropify({

    });
});