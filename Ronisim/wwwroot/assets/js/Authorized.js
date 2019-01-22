$("#divloader").hide();
$('#comboprovince').on('change', function () {
    $("#divloader").show();
    var vl = this.value;
    $.getJSON(HostUrl + "/api/getcity", { provinveid: vl },
        function (data) {
            result = data.result;
            $('#combocity').html(result);
            $('#loadinfo').html("");
            $("#divloader").hide();
        }
    );
});

$("#comboprovince").select2();
$("#combocity").select2();

$('#combocity').on('change', function () {
    $("#divloader").show();
    var vl = this.value;
    $.getJSON(HostUrl + "/api/getschoolinfo", { cityId: vl },
        function (data) {
            result = data.result;
            $('#loadinfo').html(result);
            $("#footable").footable();
            $("#divloader").hide();

        }
    );
});