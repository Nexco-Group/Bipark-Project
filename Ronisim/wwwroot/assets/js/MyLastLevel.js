var Les = "";
var ex = "";
var whereami = 0;
var lesno = 20;
var exno = 0;
var Level = "";

$(document).ready(function () {
    $("#divloader").hide();
    $("#videocontent").hide();
    $("#pageheader").hide();
    $("#bodycontent").hide();
    $("#ExerciseName").hide();
    var vid = document.getElementById("video");

    $('body').on('click', 'button', function () {
        if ($(this).attr('id') !== null) {
            var btn = $(this).attr('id').split("-");
            console.log($(this).attr('id'));
            Les = btn[1];
            if (btn[0] == "btnles") {
                $("#divloader").show();
                Level = $(this).data("level");
                $.getJSON(HostUrl + "/MyLastLevel/GetExOfLesson", {
                    level: Level,
                    lesson: Les
                },
                    function (data) {
                        var result = data;
                        var divs = "";
                        exno = result.length;
                        for (var i = 0; i < exno; i++) {
                            var obj = result[i];
                            var less = "";
                            if (parseInt(Les) < 10) {
                                less = "0" + Les.toString();
                            } else {
                                less = Les.toString();
                            }
                            var eximageurl = HostUrl + "/ExerciseImage/" + Level + "/" + less + "/V" + Level.charAt(3) + "-L" + Les + "-E" + obj.exercise + ".jpg";
                            divs += "<li> <div class='card card-shadow'> <figure class='overlay'> <img class='overlay-figure' src='" + eximageurl + "' alt='Exercise " + obj.exercise + "'> <figcaption class='overlay-panel overlay-background text-center'> <h3>Exercise " + obj.exercise + "</h3> <p> <button id='exbtn-" + obj.lesson + "-" + obj.exercise + "' type='button' data-level='" + Level + "' class='btn btn-info ladda-button btnc' data-style='zoom-in' data-plugin='ladda'> <span class='ladda-label'><i class='icon md-open-in-new mr-10' aria-hidden='true'></i>برو به این تمرین</span> </button> </p</figcaption> </figure> </div></li>";
                        }
                        $('#workdiv').empty();
                        $('#workdiv').html(divs);
                        $("#divloader").hide();
                        $("#videocontent").hide();
                        $("#divcontent").show();
                        $("#ExerciseName").hide();
                        $("#LessonName").html("Lesson " + Les);
                        $("#LevelName").html(Level);
                        whereami = 1;
                        lessonopen();
                    });
            }
            else if (btn[0] == "exbtn") {
                $("#divloader").show();
                Level = $(this).data("level");
                ex = btn[2];
                $("#exinfo").html("Level: " + Level + " | " + "Lesson: " + Les + " | " + "Exercise: " + ex);
                var src = "https://contents.vcv.ir/media/" + Level + "/" + Les + "/" + Les + "-" + ex + ".mp4";
                var video = $("video");
                video.find("#videosrc").attr("src", src);

                $("#divcontent").hide();
                $("#videocontent").show();
                $("#divloader").hide();
                $("#ExerciseName").show();
                $("#ExerciseName").html("Exercise " + ex);
                $("#LessonName").html("Lesson " + Les);
                $("#LevelName").html(Level);
                exopen();
                whereami = 2;
                video.get(0).load();
                video.get(0).play();
            }
        }
    }); function lessonopen() {
        $("#pageheader").show();
        $("#backltext").text("Back to Lesson's List | بازگشت به لیست درس ها");
        if (Les > 1) {
            $("#pretext").text("Previous Lesson | درس قبلی");
            $("#prebtn").show();
        } else {
            $("#prebtn").hide();
        }
        if (Les < lesno) {
            $("#nextext").text("Next Lesson | درس بعدی");
            $("#nexbtn").show();
        } else {
            $("#nexbtn").hide();
        }

    }
    function exopen() {
        $("#pageheader").show();
        $("#backltext").text("Back to Lesson " + Les + " بازگشت به درس");

        if (ex > 1) {
            $("#pretext").text("Previous Exercise | تمرین قبلی");
            $("#prebtn").show();
        } else {
            $("#prebtn").hide();
        }
        if (ex < exno) {
            $("#nextext").text("Next Exercise | تمرین بعدی");
            $("#nexbtn").show();
        } else {
            $("#nexbtn").hide();
        }
    }

    $("#backl").click(function () {
        if (whereami == 1) {
            $("#divloader").show();
            location.reload();
        } else if (whereami == 2) {
            vid.pause();
            $("#bodycontent").html("<button id='btnles-" + Les + "' data-level='" + Level + "' type='button' style='visibility: hidden'>");
            $("#btnles-" + Les).trigger("click");
        }
    });

    $("#nexbtn").click(function () {
        if (whereami == 1) {
            var nexles = parseInt(Les) + 1;
            $("#bodycontent").html("<button id='btnles-" + nexles + "' data-level='" + Level + "' type='button' style='visibility: hidden'>");
            $("#btnles-" + nexles).trigger("click");
        } else if (whereami == 2) {
            vid.pause();
            var nexex = parseInt(ex) + 1;
            $("#bodycontent").html("<button id='exbtn-" + Les + "-" + nexex + "' data-level='" + Level + "' type='button' style='visibility: hidden'>");
            $("#exbtn-" + Les + "-" + nexex).trigger("click");
        }

    });

    $("#prebtn").click(function () {
        if (whereami == 1) {
            var nexles = parseInt(Les) - 1;
            $("#bodycontent").append("<button id='btnles-" + nexles + "' data-level='" + Level + "' type='button' style='visibility: hidden'>");
            $("#btnles-" + nexles).trigger("click");
        } else if (whereami == 2) {
            vid.pause();
            var nexex = parseInt(ex) - 1;
            $("#bodycontent").append("<button id='exbtn-" + Les + "-" + nexex + "' data-level='" + Level + "' type='button' style='visibility: hidden'>");
            $("#exbtn-" + Les + "-" + nexex).trigger("click");
        }
    });

});