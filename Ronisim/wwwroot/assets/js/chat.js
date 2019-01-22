var tc = "";
var tid = "";
var fid = MyId;
var fc = mycode;
var herimage = "";
var HostUrlContents = "https://contents.vcv.ir";
var lastmessage = 0;//0 is her without avatar - 1 is her with avatar | 2 is you without avatar - 3 is you with avatar
var searchtext = "";

var toallclassid = "";
var toallclasscode = "";

$(document).ready(function () {

    $("#chatform").submit(function () {
        $('#sendmessage').click();
        return false;
    });
    $("#chatform").on('keyup', function (e) {
        if (e.ctrlKey && e.keyCode == 13) {
            $('#sendmessage').click();
            return false;
        }
    });

    $("#historyBtn").click(function () {
        $('#chatdiv').html("");
        $("#divloader").show();
        $.getJSON(HostUrl + "/MyChats/HistoryMessage", {
            fid: fid,
            c1: fc,
            p2: tid,
            c2: tc
        }, function (data) {
            var result = data;
            var now = 0;
            var didv = "";
            lastmessage = -1;
            for (var i = 0; i < result.length; i++) {
                var obj = result[i];
                if (obj.fromCode == fc) {
                    if (lastmessage == -1 || lastmessage == 0 || lastmessage == 1) {
                        didv = " <a class='avatar' data-toggle='tooltip' href='javascript:void(0)' data-placement='right'> <img src='" + myimage + "' alt='...'> </a> ";
                        if (obj.messageType == "text") {
                            $('#chatdiv').append("<div class='chat'><div class='chat-avatar'>" + didv + " </div><div class='chat-body'> <div class='chat-content'> <p dir='auto'>" + obj.messageContent + "</p> " + getseentime(obj.seen, obj.sendTime) + "</div></div></div>");
                        } else if (obj.messageType == "Image") {
                            $('#chatdiv').append("<div class='chat'><div class='chat-avatar'>" + didv + " </div> <div class='chat-body'> <div class='chat-content '> <a target='_blank' href='" + HostUrlContents + "/ChatContent/Image/" + obj.messageContent + "'>  <img class='resimg' src='" + HostUrlContents + "/ChatContent/Image/" + obj.messageContent + "'/></a>" + getseentime(obj.seen, obj.sendTime) + "</div></div></div>");
                        } else if (obj.messageType == "Audio") {
                            $('#chatdiv').append("<div class='chat'> <div class='chat-avatar'>" + didv + " </div><div class='chat-body'> <div class='chat-content'><p><audio controls> <source type='audio/mp3' src='" + HostUrlContents + "/ChatContent/Audio/" + obj.messageContent + "'></audio></p>" + getseentime(obj.seen, obj.sendTime) + "</div></div></div>");
                        }
                        else if (obj.messageType == "Video") {
                            $('#chatdiv').append("<div class='chat'> <div class='chat-avatar'>" + didv + " </div><div class='chat-body'> <div class='chat-content'> <p> <p><video controls> <source type='video/mp4' src='" + HostUrlContents + "/ChatContent/Video/" + obj.messageContent + "'></video><br> <a href='" + HostUrlContents + "/ChatContent/Video/" + obj.messageContent + "' target='_blank'>Download this Video</a> </p></p>" + getseentime(obj.seen, obj.sendTime) + "</div></div></div>");
                        }
                        else if (obj.messageType == "Doc") {
                            $('#chatdiv').append("<div class='chat'> <div class='chat-avatar'>" + didv + " </div><div class='chat-body'> <div class='chat-content'><p><a target='_blank' class='text-white' href='" + HostUrlContents + "/ChatContent/Doc/" + obj.messageContent + "'> <i class='site-menu-icon md-attachment-alt text-white font-size-30' aria-hidden='true'></i> <span class='site-menu-title'>Download attached file</span> </a> </p>" + getseentime(obj.seen, obj.sendTime) + "</div></div></div>");
                        }
                        lastmessage = 3;
                        $("#divloader").hide();
                    } else {
                        if (obj.messageType == "text") {
                            $('#chatdiv > div.chat > div.chat-body:last').append("<div class='chat-content'> <p dir='auto'>" + obj.messageContent + "</p>" + getseentime(obj.seen, obj.sendTime) + "</div>");
                        } else if (obj.messageType == "Image") {
                            $('#chatdiv > div.chat > div.chat-body:last').append("<div class='chat-content '> <a target='_blank' href='" + HostUrlContents + "/ChatContent/Image/" + obj.messageContent + "'>  <img class='resimg' src='" + HostUrlContents + "/ChatContent/Image/" + obj.messageContent + "'/></a>" + getseentime(obj.seen, obj.sendTime) + "</div>");

                        } else if (obj.messageType == "Audio") {
                            $('#chatdiv > div.chat > div.chat-body:last').append("<div class='chat-content'><p><audio controls> <source type='audio/mp3' src='" + HostUrlContents + "/ChatContent/Audio/" + obj.messageContent + "'></audio></p>" + getseentime(obj.seen, obj.sendTime) + "</div>");
                        } else if (obj.messageType == "Video") {
                            $('#chatdiv > div.chat > div.chat-body:last').append("<div class='chat-content'><p><video controls> <source type='video/mp4' src='" + HostUrlContents + "/ChatContent/Video/" + obj.messageContent + "'></video><br> <a href='" + HostUrlContents + "/ChatContent/Video/" + obj.messageContent + "' target='_blank'>Download this Video</a> </p>" + getseentime(obj.seen, obj.sendTime) + "</div>");
                        }
                        else if (obj.messageType == "Doc") {
                            $('#chatdiv > div.chat > div.chat-body:last').append("<div class='chat-content'><p><a target='_blank' class='text-white' href='" + HostUrlContents + "/ChatContent/Doc/" + obj.messageContent + "'> <i class='site-menu-icon md-attachment-alt text-white font-size-30' aria-hidden='true'></i> <span class='site-menu-title'>Download attached file</span> </a> </p>" + getseentime(obj.seen, obj.sendTime) + "</div>");
                        }
                        lastmessage = 2;
                        $("#divloader").hide();
                    }
                } else {
                    if (lastmessage == -1 || lastmessage == 2 || lastmessage == 3) {
                        didv = "<a class='avatar' data-toggle='tooltip' href='javascript:void(0)' data-placement='right'> <img src='" + herimage + "' alt='...'> </a>";
                        if (obj.messageType == "text") {
                            $('#chatdiv').append("<div class='chat chat-left'><div class='chat-avatar'>" + didv + " </div><div class='chat-body'> <div class='chat-content'> <p dir='auto'>" + obj.messageContent + "</p>" + getseentime2(obj.sendTime) + "</div></div></div>");
                        } else if (obj.messageType == "Image") {
                            $('#chatdiv').append("<div class='chat chat-left'><div class='chat-avatar'>" + didv + " </div> <div class='chat-body'> <div class='chat-content '> <a target='_blank' href='" + HostUrlContents + "/ChatContent/Image/" + obj.messageContent + "'>  <img class='resimg' src='" + HostUrlContents + "/ChatContent/Image/" + obj.messageContent + "'/></a>" + getseentime2(obj.sendTime) + "</div></div></div>");
                        } else if (obj.messageType == "Audio") {
                            $('#chatdiv').append("<div class='chat chat-left'> <div class='chat-avatar'>" + didv + " </div><div class='chat-body'> <div class='chat-content'><p><audio controls> <source type='audio/mp3' src='" + HostUrlContents + "/ChatContent/Audio/" + obj.messageContent + "'></audio></p>" + getseentime2(obj.sendTime) + "</div></div></div>");
                        } else if (obj.messageType == "Doc") {
                            $('#chatdiv').append("<div class='chat chat-left'> <div class='chat-avatar'>" + didv + " </div><div class='chat-body'> <div class='chat-content'><p><a target='_blank' class='text-white' href='" + HostUrlContents + "/ChatContent/Doc/" + obj.messageContent + "'> <i class='site-menu-icon md-attachment-alt text-white font-size-30' aria-hidden='true'></i> <span class='site-menu-title'>Download attached file</span> </a> </p>" + getseentime2(obj.sendTime) + "</div></div></div>");
                        }
                        else if (obj.messageType == "Video") {
                            $('#chatdiv').append("<div class='chat chat-left'> <div class='chat-avatar'>" + didv + " </div><div class='chat-body'> <div class='chat-content'><p><video controls> <source type='video/mp4' src='" + HostUrlContents + "/ChatContent/Video/" + obj.messageContent + "'></video><br> <a href='" + HostUrlContents + "/ChatContent/Video/" + obj.messageContent + "' target='_blank'>Download this Video</a> </p>" + getseentime2(obj.sendTime) + "</div></div></div>");
                        }
                        lastmessage = 1;
                        $("#divloader").hide();
                    } else {
                        if (obj.messageType == "text") {
                            $('#chatdiv > div.chat > div.chat-body:last').append("<div class='chat-content'> <p dir='auto'>" + obj.messageContent + "</p>" + getseentime2(obj.sendTime) + "</div>");
                        } else if (obj.messageType == "Image") {
                            $('#chatdiv > div.chat > div.chat-body:last').append("<div class='chat-content '> <a target='_blank' href='" + HostUrlContents + "/ChatContent/Image/" + obj.messageContent + "'>  <img class='resimg' src='" + HostUrlContents + "/ChatContent/Image/" + obj.messageContent + "'/></a>" + getseentime2(obj.sendTime) + "</div>");
                        } else if (obj.messageType == "Audio") {
                            $('#chatdiv > div.chat > div.chat-body:last').append("<div class='chat-content'><p><audio controls> <source type='audio/mp3' src='" + HostUrlContents + "/ChatContent/Audio/" + obj.messageContent + "'></audio></p>" + getseentime2(obj.sendTime) + "</div>");
                        } else if (obj.messageType == "Doc") {
                            $('#chatdiv > div.chat > div.chat-body:last').append("<div class='chat-content'><p><a target='_blank' class='text-white' href='" + HostUrlContents + "/ChatContent/Doc/" + obj.messageContent + "'> <i class='site-menu-icon md-attachment-alt text-white font-size-30' aria-hidden='true'></i> <span class='site-menu-title'>Download attached file</span> </a> </p>" + getseentime2(obj.sendTime) + "</div>");
                        } else if (obj.messageType == "Video") {
                            $('#chatdiv > div.chat > div.chat-body:last').append("<div class='chat-content'><p><video controls> <source type='video/mp4' src='" + HostUrlContents + "/ChatContent/Video/" + obj.messageContent + "'></video><br> <a href='" + HostUrlContents + "/ChatContent/Video/" + obj.messageContent + "' target='_blank'>Download this Video</a> </p>" + getseentime2(obj.sendTime) + "</div>");
                        }
                        lastmessage = 0;
                        $("#divloader").hide();
                    }
                }
            }
        });
        $("#divloader").hide();
        $("#historyBtn").hide();
    });

    $("#docfile").change(function () {
        bootbox.confirm({
            message: "آیا این فایل پی دی اف ارسال شود؟",
            buttons: {
                confirm: {
                    label: 'بله-ارسال',
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
                    var fileInput = $('#docfile')[0];
                    var file = fileInput.files[0];
                    fdata.append("fid", fid);
                    fdata.append("fc", fc);
                    fdata.append("file", file);
                    fdata.append("tocode", tc);
                    fdata.append("toid", tid);
                    fdata.append("__RequestVerificationToken", gettoken());
                    $.ajax({
                        type: 'post',
                        url: HostUrl + "/MyChats/Upload",
                        data: fdata,
                        processData: false,
                        contentType: false,
                    }).done(function (result) {
                        if (result.result == 3) {
                            if (lastmessage == -1 || lastmessage == 0 || lastmessage == 1) {
                                didv = " <a class='avatar' data-toggle='tooltip' href='javascript:void(0)' data-placement='right'> <img src='" + myimage + "' alt='...'> </a> ";
                                $('#chatdiv').append("<div class='chat'> <div class='chat-avatar'>" + didv + "</div> <div class='chat-body'><div class='chat-content'><p><a target='_blank' class='text-white' href='" + HostUrlContents + "/ChatContent/Doc/" + result.filename + "'> <i class='site-menu-icon md-attachment-alt text-white font-size-30' aria-hidden='true'></i> <span class='site-menu-title'>Download attached file</span> </a></p>" + getseentime(false, result.time) + "</div></div></div>");
                                lastmessage = 3;
                            } else {
                                $('#chatdiv > div.chat > div.chat-body:last').append("<div class='chat-content'><p><a target='_blank' class='text-white' href='" + HostUrlContents + "/ChatContent/Doc/" + result.filename + "'><p> <i class='site-menu-icon md-attachment-alt text-white font-size-30' aria-hidden='true'></i> <span class='site-menu-title'>Download attached file</span> </a></p>" + getseentime(false, result.time) + "</div>");
                                lastmessage = 2;
                            }
                            $("#inputmessage").val("");
                            $(".emojionearea-editor").html("");
                            fastscrolldown();

                            $('#docfile').val('');
                            if ($("#nomes").length == 1) {
                                $("#nomes").hide();
                            }
                        } else if (result.result == 2) {
                            bootbox.alert(" فرمت فایل ارسالی مجاز نمی باشد. فقط فایل با پسوند " + "pdf");
                        } else if (result.result == 1) {
                            bootbox.alert("حجم فایل پی دی اف بیشتر از حد مجاز (500 کیلوبایت) میباشد");
                        } else {
                            bootbox.alert("مشکلی در آپلود فایل به وجود آمده است. لطفا دوباره تلاش کنید");
                        }
                        $("#divloader").hide();
                        fastscrolldown();
                    });
                }
            }
        });
    });

    $("#voicefile").change(function () {
        bootbox.confirm({
            message: "آیا این فایل صوتی ارسال شود؟",
            buttons: {
                confirm: {
                    label: 'بله-ارسال',
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
                    var fileInput = $('#voicefile')[0];
                    var file = fileInput.files[0];
                    fdata.append("fid", fid);
                    fdata.append("fc", fc);
                    fdata.append("file", file);
                    fdata.append("tocode", tc);
                    fdata.append("toid", tid);
                    fdata.append("__RequestVerificationToken", gettoken());
                    $.ajax({
                        type: 'post',
                        url: HostUrl + "/MyChats/Upload",
                        data: fdata,
                        processData: false,
                        contentType: false,
                    }).done(function (result) {
                        if (result.result == 3) {
                            if (lastmessage == -1 || lastmessage == 0 || lastmessage == 1) {
                                didv = " <a class='avatar' data-toggle='tooltip' href='javascript:void(0)' data-placement='right'> <img src='" + myimage + "' alt='...'> </a> ";
                                $('#chatdiv').append("<div class='chat'> <div class='chat-avatar'>" + didv + "</div> <div class='chat-body'><div class='chat-content'><p><audio controls> <source type='audio/mp3' src='" + HostUrlContents + "/ChatContent/" + result.type + "/" + result.filename + "'></audio></p>" + getseentime(false, result.time) + "</div></div></div>");
                                lastmessage = 3;
                            } else {
                                $('#chatdiv > div.chat > div.chat-body:last').append("<div class='chat-content'><p><audio controls> <source type='audio/mp3' src='" + HostUrlContents + "/ChatContent/" + result.type + "/" + result.filename + "'></audio></p>" + getseentime(false, result.time) + "</div>");
                                lastmessage = 2;
                            }
                            $("#inputmessage").val("");
                            $(".emojionearea-editor").html("");
                            fastscrolldown();

                            $("#voicefile").val('');
                            if ($("#nomes").length == 1) {
                                $("#nomes").hide();
                            }
                        } else if (result.result == 2) {
                            bootbox.alert(" فرمت فایل ارسالی مجاز نمی باشد. فقط فایل با پسوند " + "mp3, ogg, m4a");
                        } else if (result.result == 1) {
                            bootbox.alert("حجم فایل صوتی بیشتر از حد مجاز (500 کیلوبایت) میباشد");
                        } else {
                            bootbox.alert("مشکلی در آپلود فایل به وجود آمده است. لطفا دوباره تلاش کنید");
                        }
                        $("#divloader").hide();
                        fastscrolldown();
                    });
                }
            }
        });
    });

    $("#picfile").change(function () {
        bootbox.confirm({
            message: "آیا این تصویر ارسال شود؟",
            buttons: {
                confirm: {
                    label: 'بله-ارسال',
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
                    var fileInput = $('#picfile')[0];
                    var file = fileInput.files[0];
                    fdata.append("fid", fid);
                    fdata.append("fc", fc);
                    fdata.append("file", file);
                    fdata.append("tocode", tc);
                    fdata.append("toid", tid);
                    fdata.append("__RequestVerificationToken", gettoken());
                    $.ajax({
                        type: 'post',
                        url: HostUrl + "/MyChats/Upload",
                        data: fdata,
                        processData: false,
                        contentType: false,
                    }).done(function (result) {
                        if (result.result == 3) {
                            if (lastmessage == -1 || lastmessage == 0 || lastmessage == 1) {
                                didv = " <a class='avatar' data-toggle='tooltip' href='javascript:void(0)' data-placement='right'> <img src='" + myimage + "' alt='...'> </a> ";
                                $('#chatdiv').append("<div class='chat'> <div class='chat-avatar'>" + didv + "</div> <div class='chat-body'> <div class='chat-content'> <a target='_blank' href='" + HostUrlContents + "/ChatContent/" + result.type + "/" + result.filename + "'> <img class='resimg' src='" + HostUrlContents + "/ChatContent/" + result.type + "/" + result.filename + "'/><a>" + getseentime(false, result.time) + "</div></div></div>");
                                lastmessage = 3;
                            } else {
                                $('#chatdiv > div.chat > div.chat-body:last').append("<div class='chat-content'> <a target='_blank' href='" + HostUrlContents + "/ChatContent/" + result.type + "/" + result.filename + "'> <img class='resimg' src='" + HostUrlContents + "/ChatContent/" + result.type + "/" + result.filename + "'/><a>" + getseentime(false, result.time) + "</div>");
                                lastmessage = 2;
                            }
                            $("#inputmessage").val("");
                            $(".emojionearea-editor").html("");
                            fastscrolldown();
                            $('#picfile').val('');
                            if ($("#nomes").length == 1) {
                                $("#nomes").hide();
                            }
                        } else if (result.result == 2) {
                            bootbox.alert(" فرمت فایل ارسالی مجاز نمی باشد. فقط فایل با پسوند " + "png, jpg");
                        } else if (result.result == 1) {
                            bootbox.alert("حجم تصویر بیشتر از حد مجاز (500 کیلوبایت) میباشد");
                        } else {
                            bootbox.alert("مشکلی در آپلود فایل به وجود آمده است. لطفا دوباره تلاش کنید");
                        }
                        $("#divloader").hide();
                        fastscrolldown();
                    });
                }
            }
        });
    });

    $('#VCVSupportBtn').click(function () {
        tc = "10";
        if (mycode == "10") {
            tc = "0";
        }
        tid = OtherId;
        herimage = VCVSupportImage;
        resetli();
        $(this).addClass("active");
        renderChat();
        scrolldown();
    });


    function renderChat() {
        $("#divloader").show();
        $('#chatdiv').html("");
        $.getJSON(HostUrl + "/MyChats/LastMessage", {
            fid: fid,
            c1: fc,
            p2: tid,
            c2: tc
        },
            function (data) {
                var result = data;
                var now = 0;
                var didv = "";
                lastmessage = -1;
                if (result.length > 0) {
                    for (var i = 0; i < result.length; i++) {
                        var obj = result[i];
                        if (obj.fromCode == fc) {
                            if (lastmessage == -1 || lastmessage == 0 || lastmessage == 1) {
                                didv = " <a class='avatar' data-toggle='tooltip' href='javascript:void(0)' data-placement='right'> <img src='" + myimage + "' alt='...'> </a> ";
                                if (obj.messageType == "text") {
                                    $('#chatdiv').append("<div class='chat'><div class='chat-avatar'>" + didv + " </div><div class='chat-body'> <div class='chat-content'> <p dir='auto'>" + obj.messageContent + "</p> " + getseentime(obj.seen, obj.sendTime) + "</div></div></div>");
                                } else if (obj.messageType == "Image") {
                                    $('#chatdiv').append("<div class='chat'><div class='chat-avatar'>" + didv + " </div> <div class='chat-body'> <div class='chat-content '> <a target='_blank' href='" + HostUrlContents + "/ChatContent/Image/" + obj.messageContent + "'>  <img class='resimg' src='" + HostUrlContents + "/ChatContent/Image/" + obj.messageContent + "'/></a>" + getseentime(obj.seen, obj.sendTime) + "</div></div></div>");
                                } else if (obj.messageType == "Audio") {
                                    $('#chatdiv').append("<div class='chat'> <div class='chat-avatar'>" + didv + " </div><div class='chat-body'> <div class='chat-content'> <p> <p><audio controls> <source type='audio/mp3' src='" + HostUrlContents + "/ChatContent/Audio/" + obj.messageContent + "'></audio></p></p>" + getseentime(obj.seen, obj.sendTime) + "</div></div></div>");
                                }
                                else if (obj.messageType == "Doc") {
                                    $('#chatdiv').append("<div class='chat'> <div class='chat-avatar'>" + didv + " </div><div class='chat-body'> <div class='chat-content'><p><a target='_blank' class='text-white' href='" + HostUrlContents + "/ChatContent/Doc/" + obj.messageContent + "'> <i class='site-menu-icon md-attachment-alt text-white font-size-30' aria-hidden='true'></i> <span class='site-menu-title'>Download attached file</span> </a> </p>" + getseentime(obj.seen, obj.sendTime) + "</div></div></div>");
                                }
                                lastmessage = 3;
                                $("#divloader").hide();
                            } else {
                                if (obj.messageType == "text") {
                                    $('#chatdiv > div.chat > div.chat-body:last').append("<div class='chat-content'> <p dir='auto'>" + obj.messageContent + "</p>" + getseentime(obj.seen, obj.sendTime) + "</div>");
                                } else if (obj.messageType == "Image") {
                                    $('#chatdiv > div.chat > div.chat-body:last').append("<div class='chat-content '> <a target='_blank' href='" + HostUrlContents + "/ChatContent/Image/" + obj.messageContent + "'>  <img class='resimg' src='" + HostUrlContents + "/ChatContent/Image/" + obj.messageContent + "'/></a>" + getseentime(obj.seen, obj.sendTime) + "</div>");

                                } else if (obj.messageType == "Audio") {
                                    $('#chatdiv > div.chat > div.chat-body:last').append("<div class='chat-content'><p><audio controls> <source type='audio/mp3' src='" + HostUrlContents + "/ChatContent/Audio/" + obj.messageContent + "'></audio></p>" + getseentime(obj.seen, obj.sendTime) + "</div>");
                                }
                                else if (obj.messageType == "Doc") {
                                    $('#chatdiv > div.chat > div.chat-body:last').append("<div class='chat-content'><p><a target='_blank' class='text-white' href='" + HostUrlContents + "/ChatContent/Doc/" + obj.messageContent + "'> <i class='site-menu-icon md-attachment-alt text-white font-size-30' aria-hidden='true'></i> <span class='site-menu-title'>Download attached file</span> </a> </p>" + getseentime(obj.seen, obj.sendTime) + "</div>");
                                }
                                lastmessage = 2;
                                $("#divloader").hide();
                            }
                        } else {
                            if (lastmessage == -1 || lastmessage == 2 || lastmessage == 3) {
                                didv = "<a class='avatar' data-toggle='tooltip' href='javascript:void(0)' data-placement='right'> <img src='" + herimage + "' alt='...'> </a>";
                                if (obj.messageType == "text") {
                                    $('#chatdiv').append("<div class='chat chat-left'><div class='chat-avatar'>" + didv + " </div><div class='chat-body'> <div class='chat-content'> <p dir='auto'>" + obj.messageContent + "</p>" + getseentime2(obj.sendTime) + "</div></div></div>");
                                } else if (obj.messageType == "Image") {
                                    $('#chatdiv').append("<div class='chat chat-left'><div class='chat-avatar'>" + didv + " </div> <div class='chat-body'> <div class='chat-content '> <a target='_blank' href='" + HostUrlContents + "/ChatContent/Image/" + obj.messageContent + "'>  <img class='resimg' src='" + HostUrlContents + "/ChatContent/Image/" + obj.messageContent + "'/></a>" + getseentime2(obj.sendTime) + "</div></div></div>");
                                } else if (obj.messageType == "Audio") {
                                    $('#chatdiv').append("<div class='chat chat-left'> <div class='chat-avatar'>" + didv + " </div><div class='chat-body'> <div class='chat-content'><p><audio controls> <source type='audio/mp3' src='" + HostUrlContents + "/ChatContent/Audio/" + obj.messageContent + "'></audio></p>" + getseentime2(obj.sendTime) + "</div></div></div>");
                                } else if (obj.messageType == "Doc") {
                                    $('#chatdiv').append("<div class='chat chat-left'> <div class='chat-avatar'>" + didv + " </div><div class='chat-body'> <div class='chat-content'><p><a target='_blank' class='text-white' href='" + HostUrlContents + "/ChatContent/Doc/" + obj.messageContent + "'> <i class='site-menu-icon md-attachment-alt text-white font-size-30' aria-hidden='true'></i> <span class='site-menu-title'>Download attached file</span> </a> </p>" + getseentime2(obj.sendTime) + "</div></div></div>");
                                }
                                lastmessage = 1;
                                $("#divloader").hide();
                            } else {
                                if (obj.messageType == "text") {
                                    $('#chatdiv > div.chat > div.chat-body:last').append("<div class='chat-content'> <p dir='auto'>" + obj.messageContent + "</p>" + getseentime2(obj.sendTime) + "</div>");
                                } else if (obj.messageType == "Image") {
                                    $('#chatdiv > div.chat > div.chat-body:last').append("<div class='chat-content '> <a target='_blank' href='" + HostUrlContents + "/ChatContent/Image/" + obj.messageContent + "'>  <img class='resimg' src='" + HostUrlContents + "/ChatContent/Image/" + obj.messageContent + "'/></a>" + getseentime2(obj.sendTime) + "</div>");
                                } else if (obj.messageType == "Audio") {
                                    $('#chatdiv > div.chat > div.chat-body:last').append("<div class='chat-content'><p><audio controls> <source type='audio/mp3' src='" + HostUrlContents + "/ChatContent/Audio/" + obj.messageContent + "'></audio></p>" + getseentime2(obj.sendTime) + "</div>");
                                } else if (obj.messageType == "Doc") {
                                    $('#chatdiv > div.chat > div.chat-body:last').append("<div class='chat-content'><p><a target='_blank' class='text-white' href='" + HostUrlContents + "/ChatContent/Doc/" + obj.messageContent + "'> <i class='site-menu-icon md-attachment-alt text-white font-size-30' aria-hidden='true'></i> <span class='site-menu-title'>Download attached file</span> </a> </p>" + getseentime2(obj.sendTime) + "</div>");
                                }
                                lastmessage = 0;
                                $("#divloader").hide();
                            }
                        }
                    }
                    scrolldown();
                } else {
                    $("#historyBtn").hide();
                    $('#chatdiv').append("<div id='nomes' class='text-center'><p><h2>No messages.</h2><p></div>");
                    $("#divloader").hide();
                }
            });
        $("#historyBtn").show();
    }

    function gettd(a) {
        return "<td>" + a + "</td>";
    }


    $('#sendmessage').click(function () {
        var message = $(".emojionearea-editor").html();
        if (message == null || message.trim() == "") {
            bootbox.alert("لطفا متن پیام را وارد کنید");
            return;
        } else {
            $("#divloader").show();
            var fdata = new FormData();
            fdata.append("__RequestVerificationToken", gettoken());
            fdata.append("fid", fid);
            fdata.append("fc", fc);
            fdata.append("toid", tid);
            fdata.append("tocode", tc);
            fdata.append("mes", message);

            $.ajax({
                type: 'post',
                url: HostUrl + "/MyChats/Send",
                data: fdata,
                processData: false,
                contentType: false,
            }).done(function (data) {
                result = data.result;
                if (result == 1) {
                    if (lastmessage == -1 || lastmessage == 0 || lastmessage == 1) {
                        didv = " <a class='avatar' data-toggle='tooltip' href='javascript:void(0)' data-placement='right'> <img src='" + myimage + "' alt='...'> </a> ";
                        $('#chatdiv').append("<div class='chat'><div class='chat-avatar'>" + didv + " </div><div class='chat-body'> <div class='chat-content'> <p dir='auto'>" + message + "</p>" + getseentime(false, data.time) + "</div></div></div>");
                        lastmessage = 3;
                    } else {
                        $('#chatdiv > div.chat > div.chat-body:last').append("<div class='chat-content'> <p dir='auto'>" + message + "</p>" + getseentime(false, data.time) + "</div>");
                        lastmessage = 2;
                    }
                    $("#inputmessage").val("");
                    $(".emojionearea-editor").html("");
                    fastscrolldown();
                    if ($("#nomes").length == 1) {
                        $("#nomes").hide();
                    }
                } else {
                    alert("Sending the message faild.");
                }
            });
            $("#divloader").hide();
        }
    });
    function resetli() {
        $('#chatdiv').html("");
        $("#TeachersBtn").removeClass("active");
        $("#StudentsBtn").removeClass("active");
        $("#SchoolsBtn").removeClass("active");
        $("#VCVSupportBtn").removeClass("active");
        $("#VCVContestBtn").removeClass("active");
        $("#chatwith").remove();
        $("#chatform").show();
        lastmessage = -1;
    }

    $('#SchoolsBtn').click(function () {
        tc = "20";
        //tid = TeacherId;
        //herimage = TeacherImage;
        resetli();
        $("#chatform").hide();
        $(this).addClass("active");
        renderListSchool();
    });

    $('#TeachersBtn').click(function () {

        tc = "30";
        //tid = TeacherId;
        //herimage = TeacherImage;
        resetli();
        $("#chatform").hide();
        $(this).addClass("active");
        renderListTeacher();
    });

    $('#StudentsBtn').click(function () {
        tc = "40";
        //tid = SchoolId;
        //herimage = SchoolImage;
        resetli();
        $("#chatform").hide();
        $(this).addClass("active");
        renderListStudent();
    });

    $('#VCVContestBtn').click(function () {
        tc = "1";
        herimage = "https://contents.vcv.ir/Movie.png";
        resetli();
        $(this).addClass("active");
        renderListContest();
    });


    function renderListContest() {
        $("#divloader").show();
        $("#historyBtn").hide();
        //$('#chatdiv').html("");
        $.getJSON(HostUrl + "/MyChats/getmycontest", {
            admid: fid,
            mycode: fc
        }, function (data) {
            var result = data;
            if (result.length > 0) {

                var gotchatdiv = "";
                var tbl1 = "<div class='col-lg-9' style='margin:auto'><table class='table table-hover table-bordered table-striped toggle-arrow-tiny' id='TeachersFoo' data-show-toggle='true' data-toggle-column='last' data-filtering='true' data-sorting='true' data-filter-connectors='false'> <thead> <tr>  <th data-name='firstName'>Name</th> <th data-name='schName'>School Name</th> <th data-name='City'>City</th> </tr></thead> <tbody> ";
                //<th data-visible='false' data-filterable='false' data-name='stdid'></th>
                for (var i = 0; i < result.length; i++) {
                    var obj = result[i];
                    if (obj.schname != null) {
                        tbl1 += "<tr onclick='whoclick(this)' id='" + "30_" + obj.stdid + "_" + obj.stdimage + "_" + obj.stdname + "' data-code='" + obj.code + "'>";
                        tbl1 += gettd(obj.stdname + ' <span class="badge badge-pill badge-info">' + getbadge(obj.count) + '</span>');
                        tbl1 += gettd(obj.schname);
                        tbl1 += gettd(obj.city);
                        tbl1 += "</tr>";
                    }
                }

                tbl1 += "</tbody></table></div>";

                gotchatdiv += tbl1;

                $('#chatdiv').html(gotchatdiv);
                $('#TeachersFoo').footable();
                $("input[placeholder='Search']").attr('onchange', 'changesearch2(this)').attr('onpaste', 'changesearch2(this)').attr('onkeyup', 'changesearch2(this)');
                $(".form-inline").append('<button type="button" id="sendmesdyn2" onclick="sendtoalltch()" class="btn btn-primary col-lg-7">Send a message: to all of your teachers</button>');
                $("#divloader").hide();

                //var gotchatdiv = "<div class='page-aside-inner col-lg-7' style='margin:0 auto;display:block'> <div class='input-search' align='center'> <button type='button' onclick='sendtoalltch(this)' class='btn btn-primary'>Send a message to all teachers</button> <h4> <b> List of Teachers </b> </h4> </div><div class='dropdown-divider'></div><div class='app-message-list page-aside-scroll'> <div data-role='container'> <div data-role='content'> <ul class='list-group'>";

                //for (var i = 0; i < result.length; i++) {
                //    var obj = result[i];
                //    gotchatdiv += "<li class='list-group-item' style='cursor:pointer;' onclick='whoclick(this)' id='30_" + obj.id + "_" + obj.imgUrl + "_" + obj.fullname + "'> <div class='media'> <div class='pr-20'> <a class='avatar avatar-online'> <img class='img-fluid' src='" + obj.imgUrl + "' alt='...'> </a> </div><div class='media-body'> <h5 class='mt-0 mb-5'>" + obj.fullname + "</h5> </div></div></li>";
                //}
                //gotchatdiv += "</ul></div></div></div></div>";
                //$('#chatdiv').html(gotchatdiv);
            } else {
                $('#chatdiv').html("<div id='nomes' class='text-center'><p><h2>شما مدرسی که در آموزشگاه شما ثبت نام کرده باشند ندارید</h2><p></div>");
                $("#divloader").hide();
            }
        });
    }

    function renderListTeacher() {
        $("#divloader").show();
        $("#historyBtn").hide();
        //$('#chatdiv').html("");
        $.getJSON(HostUrl + "/MyChats/getmyteachers", {
            admid: fid,
            mycode: fc
        }, function (data) {
            var result = data;
            if (result.length > 0) {

                var gotchatdiv = "";
                var tbl1 = "<div class='col-lg-9' style='margin:auto'><table class='table table-hover table-bordered table-striped toggle-arrow-tiny' id='TeachersFoo' data-show-toggle='true' data-toggle-column='last' data-filtering='true' data-sorting='true' data-filter-connectors='false'> <thead> <tr>  <th data-name='firstName'>Name</th> <th data-name='schName'>School Name</th> <th data-name='City'>City</th> </tr></thead> <tbody> ";
                //<th data-visible='false' data-filterable='false' data-name='stdid'></th>
                for (var i = 0; i < result.length; i++) {
                    var obj = result[i];
                    if (obj.schname != null) {
                        tbl1 += "<tr onclick='whoclick(this)' id='" + "30_" + obj.stdid + "_" + obj.stdimage + "_" + obj.stdname + "'>";
                        tbl1 += gettd(obj.stdname + ' <span class="badge badge-pill badge-danger">' + getbadge(obj.count) + '</span>');
                        tbl1 += gettd(obj.schname);
                        tbl1 += gettd(obj.city);
                        tbl1 += "</tr>";
                    }
                }

                tbl1 += "</tbody></table></div>";

                gotchatdiv += tbl1;

                $('#chatdiv').html(gotchatdiv);
                $('#TeachersFoo').footable();
                $("input[placeholder='Search']").attr('onchange', 'changesearch2(this)').attr('onpaste', 'changesearch2(this)').attr('onkeyup', 'changesearch2(this)');
                $(".form-inline").append('<button type="button" id="sendmesdyn2" onclick="sendtoalltch()" class="btn btn-primary col-lg-7">Send a message: to all of your teachers</button>');
                $("#divloader").hide();

                //var gotchatdiv = "<div class='page-aside-inner col-lg-7' style='margin:0 auto;display:block'> <div class='input-search' align='center'> <button type='button' onclick='sendtoalltch(this)' class='btn btn-primary'>Send a message to all teachers</button> <h4> <b> List of Teachers </b> </h4> </div><div class='dropdown-divider'></div><div class='app-message-list page-aside-scroll'> <div data-role='container'> <div data-role='content'> <ul class='list-group'>";

                //for (var i = 0; i < result.length; i++) {
                //    var obj = result[i];
                //    gotchatdiv += "<li class='list-group-item' style='cursor:pointer;' onclick='whoclick(this)' id='30_" + obj.id + "_" + obj.imgUrl + "_" + obj.fullname + "'> <div class='media'> <div class='pr-20'> <a class='avatar avatar-online'> <img class='img-fluid' src='" + obj.imgUrl + "' alt='...'> </a> </div><div class='media-body'> <h5 class='mt-0 mb-5'>" + obj.fullname + "</h5> </div></div></li>";
                //}
                //gotchatdiv += "</ul></div></div></div></div>";
                //$('#chatdiv').html(gotchatdiv);
            } else {
                $('#chatdiv').html("<div id='nomes' class='text-center'><p><h2>شما مدرسی که در آموزشگاه شما ثبت نام کرده باشند ندارید</h2><p></div>");
                $("#divloader").hide();
            }
        });
    }

    function renderListSchool() {
        $("#divloader").show();
        $("#historyBtn").hide();
        //$('#chatdiv').html("");
        $.getJSON(HostUrl + "/MyChats/getmyschools", {
            admid: fid,
            mycode: fc
        }, function (data) {
            var result = data;
            if (result.length > 0) {

                var gotchatdiv = "";
                var tbl1 = "<div class='col-lg-9' style='margin:auto'><table class='table table-hover table-bordered table-striped toggle-arrow-tiny' id='SchoolsFoo' data-show-toggle='true' data-toggle-column='last' data-filtering='true' data-sorting='true' data-filter-connectors='false'> <thead> <tr>  <th data-name='firstName'>School Name</th> <th data-name='City'>City</th> <th data-name='schName'>Manager Name</th> </tr></thead> <tbody> ";
                for (var i = 0; i < result.length; i++) {
                    var obj = result[i];
                    if (obj.schname != null) {
                        tbl1 += "<tr onclick='whoclick(this)' id='" + "20_" + obj.stdid + "_" + obj.stdimage + "_" + obj.stdname + "'>";
                        tbl1 += gettd(obj.schname + ' <span class="badge badge-pill badge-danger">' + getbadge(obj.count) + '</span>');
                        tbl1 += gettd(obj.city);
                        tbl1 += gettd(obj.stdname);

                        tbl1 += "</tr>";
                    }
                }

                tbl1 += "</tbody></table></div>";

                gotchatdiv += tbl1;

                $('#chatdiv').html(gotchatdiv);
                $('#SchoolsFoo').footable();
                $("input[placeholder='Search']").attr('onchange', 'changesearch3(this)').attr('onpaste', 'changesearch3(this)').attr('onkeyup', 'changesearch3(this)');
                $(".form-inline").append('<button type="button" id="sendmesdyn2" onclick="sendtoallsch()" class="btn btn-primary col-lg-7">Send a message: to all of your Schools</button>');
                $("#divloader").hide();

            } else {
                $('#chatdiv').html("<div id='nomes' class='text-center'><p><h2>شما مدرسی که در آموزشگاه شما ثبت نام کرده باشند ندارید</h2><p></div>");
                $("#divloader").hide();
            }
        });
    }

    function renderListStudent() {
        $("#divloader").show();
        $("#historyBtn").hide();
        //$('#chatdiv').html("");
        $.getJSON(HostUrl + "/MyChats/getallstudent", {
            admid: fid,
            mycode: fc
        },
            function (data) {
                var result = data;
                if (result.length > 0) {
                    var gotchatdiv = "<p style='font-size:0.95rem'>راهنما: ابتدا فرد یا افرادی که می خواهید به آنها پیامی بفرستید را جستجو کنید (مانند یک کد کلاس خاص، یک سطح خاص، و یا یک فرد خاص) سپس روی دکمه کنار جستجو کلیک کنید تا پیام مورد نظرتان را به آن فرد یا افراد جستجو شده بفرستید. در حالت پیش فرض کل زبان آموزان شما در لیست قرار دارند و می توانید به همه آنها پیام بفرستید</p><hr/>";
                    var tbl1 = "<div class='col-lg-9' style='margin:auto'><table class='table table-hover table-bordered table-striped toggle-arrow-tiny' id='StudentsFoo' data-show-toggle='true' data-toggle-column='last' data-filtering='true' data-sorting='true' data-filter-connectors='false'> <thead> <tr>  <th data-name='firstName'>Name</th> <th data-name='schName'>School Name</th> <th data-name='City'>City</th> <th data-name='ClassCode'>Class Code</th><th data-name='TchName'>Teacher Name</th> <th data-name='Level'>Level</th></tr></thead> <tbody> ";
                    //<th data-visible='false' data-filterable='false' data-name='stdid'></th>
                    for (var i = 0; i < result.length; i++) {
                        var obj = result[i];
                        tbl1 += "<tr onclick='whoclick(this)' id='" + "40_" + obj.stdid + "_" + obj.stdimage + "_" + obj.stdname + "'>";
                        //tbl1 += gettd('40_' + obj.stdid + '_' + obj.stdimage + '_' + obj.stdname);
                        tbl1 += gettd(obj.stdname + ' <span class="badge badge-pill badge-danger">' + getbadge(obj.count) + '</span>');
                        //tbl1 += gettd(obj.count);
                        tbl1 += gettd(obj.schname);
                        tbl1 += gettd(obj.city);

                        tbl1 += gettd(obj.stdclasscode);
                        tbl1 += gettd(obj.tchname);
                        tbl1 += gettd(obj.stdlevel);
                        tbl1 += "</tr>";
                    }

                    tbl1 += "</tbody></table></div>";

                    gotchatdiv += tbl1;

                    $('#chatdiv').html(gotchatdiv);
                    $('#StudentsFoo').footable();
                    $("input[placeholder='Search']").attr('onchange', 'changesearch(this)').attr('onpaste', 'changesearch(this)').attr('onkeyup', 'changesearch(this)');
                    $(".form-inline").append('<button type="button" id="sendmesdyn" onclick="sendtoallstd()" class="btn btn-primary col-lg-6">Send a message: to all of your students</button>');
                    $("#divloader").hide();
                } else {
                    $('#chatdiv').html("<div id='nomes' class='text-center'><p dir='auto'><h3>There's no active classes for you at the moment.</h3><p dir='auto'></div>");
                    $("#divloader").hide();
                }
            });

    }

    function getbadge(a) {
        if (a == 0) { return ""; } else { return a; }
    }

    function gettd(a) {
        return "<td>" + a + "</td>";
    }


    refreshnewmes();

    setTimeout(function () {
        setInterval(function () { refreshnewmes(); }, 20000);
    }, 20000);

    $("#inputmessage").emojioneArea({
        attributes: {
            spellcheck: false,
            autocomplete: "off",
            dir: "auto"
        }
    });
    $('#divloader').hide();
    //$('#StudentsBtn').click();
});

var div = document.getElementById("chatb");
function scrolldown() {
    setTimeout(function () {
        refreshnewmes();
        $('#' + 'chatb').animate({
            scrollTop: div.scrollHeight - div.clientHeight
        }, 1500);
    }, 1500);
}

function fastscrolldown() {
    $('#' + 'chatb').animate({
        scrollTop: div.scrollHeight - div.clientHeight
    }, 500);
}

var mmmname = "";
function whoclick(item) {
    var res = $(item).attr('id').split("_");
    tc = res[0];
    tid = res[1];
    mmmname = res[3];
    herimage = res[2];
    if ($(item).data('code') != null && $(item).data('code') != "") {
        renderChat2("1");
        $("#historyBtn").hide();
    } else {
        renderChat2(mycode);
        $("#historyBtn").show();
    }
}
function renderChat2(code) {
    $("#divloader").show();
    $('#chatdiv').html("");
    $("#chatform").show();
    $.getJSON(HostUrl + "/MyChats/LastMessage", {
        fid: fid,
        c1: code,
        p2: tid,
        c2: tc
    },
        function (data) {
            var result = data;
            var now = 0;
            var didv = "";
            lastmessage == -1;
            if (result.length > 0) {
                for (var i = 0; i < result.length; i++) {
                    var obj = result[i];
                    if (obj.fromCode == fc) {
                        if (lastmessage == -1 || lastmessage == 0 || lastmessage == 1) {
                            didv = " <a class='avatar' data-toggle='tooltip' href='javascript:void(0)' data-placement='right'> <img src='" + myimage + "' alt='...'> </a> ";
                            if (obj.messageType == "text") {
                                $('#chatdiv').append("<div class='chat'><div class='chat-avatar'>" + didv + " </div><div class='chat-body'> <div class='chat-content'> <p dir='auto'>" + obj.messageContent + "</p> " + getseentime(obj.seen, obj.sendTime) + "</div></div></div>");
                            } else if (obj.messageType == "Image") {
                                $('#chatdiv').append("<div class='chat'><div class='chat-avatar'>" + didv + " </div> <div class='chat-body'> <div class='chat-content '> <a target='_blank' href='" + HostUrlContents + "/ChatContent/Image/" + obj.messageContent + "'>  <img class='resimg' src='" + HostUrlContents + "/ChatContent/Image/" + obj.messageContent + "'/></a>" + getseentime(obj.seen, obj.sendTime) + "</div></div></div>");
                            } else if (obj.messageType == "Audio") {
                                $('#chatdiv').append("<div class='chat'> <div class='chat-avatar'>" + didv + " </div><div class='chat-body'> <div class='chat-content'><p><audio controls> <source type='audio/mp3' src='" + HostUrlContents + "/ChatContent/Audio/" + obj.messageContent + "'></audio></p>" + getseentime(obj.seen, obj.sendTime) + "</div></div></div>");
                            }
                            else if (obj.messageType == "Video") {
                                $('#chatdiv').append("<div class='chat'> <div class='chat-avatar'>" + didv + " </div><div class='chat-body'> <div class='chat-content'> <p> <p><video controls> <source type='video/mp4' src='" + HostUrlContents + "/ChatContent/Video/" + obj.messageContent + "'></video><br> <a href='" + HostUrlContents + "/ChatContent/Video/" + obj.messageContent + "' target='_blank'>Download this Video</a> </p></p>" + getseentime(obj.seen, obj.sendTime) + "</div></div></div>");
                            }
                            else if (obj.messageType == "Doc") {
                                $('#chatdiv').append("<div class='chat'> <div class='chat-avatar'>" + didv + " </div><div class='chat-body'> <div class='chat-content'><p><a target='_blank' class='text-white' href='" + HostUrlContents + "/ChatContent/Doc/" + obj.messageContent + "'> <i class='site-menu-icon md-attachment-alt text-white font-size-30' aria-hidden='true'></i> <span class='site-menu-title'>Download attached file</span> </a> </p>" + getseentime(obj.seen, obj.sendTime) + "</div></div></div>");
                            }
                            lastmessage = 3;
                            $("#divloader").hide();
                        } else {
                            if (obj.messageType == "text") {
                                $('#chatdiv > div.chat > div.chat-body:last').append("<div class='chat-content'> <p dir='auto'>" + obj.messageContent + "</p>" + getseentime(obj.seen, obj.sendTime) + "</div>");
                            } else if (obj.messageType == "Image") {
                                $('#chatdiv > div.chat > div.chat-body:last').append("<div class='chat-content '> <a target='_blank' href='" + HostUrlContents + "/ChatContent/Image/" + obj.messageContent + "'>  <img class='resimg' src='" + HostUrlContents + "/ChatContent/Image/" + obj.messageContent + "'/></a>" + getseentime(obj.seen, obj.sendTime) + "</div>");

                            } else if (obj.messageType == "Audio") {
                                $('#chatdiv > div.chat > div.chat-body:last').append("<div class='chat-content'><p><audio controls> <source type='audio/mp3' src='" + HostUrlContents + "/ChatContent/Audio/" + obj.messageContent + "'></audio></p>" + getseentime(obj.seen, obj.sendTime) + "</div>");
                            } else if (obj.messageType == "Video") {
                                $('#chatdiv > div.chat > div.chat-body:last').append("<div class='chat-content'><p><video controls> <source type='video/mp4' src='" + HostUrlContents + "/ChatContent/Video/" + obj.messageContent + "'></video><br> <a href='" + HostUrlContents + "/ChatContent/Video/" + obj.messageContent + "' target='_blank'>Download this Video</a> </p>" + getseentime(obj.seen, obj.sendTime) + "</div>");
                            }
                            else if (obj.messageType == "Doc") {
                                $('#chatdiv > div.chat > div.chat-body:last').append("<div class='chat-content'><p><a target='_blank' class='text-white' href='" + HostUrlContents + "/ChatContent/Doc/" + obj.messageContent + "'> <i class='site-menu-icon md-attachment-alt text-white font-size-30' aria-hidden='true'></i> <span class='site-menu-title'>Download attached file</span> </a> </p>" + getseentime(obj.seen, obj.sendTime) + "</div>");
                            }
                            lastmessage = 2;
                            $("#divloader").hide();
                        }
                    } else {
                        if (lastmessage == -1 || lastmessage == 2 || lastmessage == 3) {
                            didv = "<a class='avatar' data-toggle='tooltip' href='javascript:void(0)' data-placement='right'> <img src='" + herimage + "' alt='...'> </a>";
                            if (obj.messageType == "text") {
                                $('#chatdiv').append("<div class='chat chat-left'><div class='chat-avatar'>" + didv + " </div><div class='chat-body'> <div class='chat-content'> <p dir='auto'>" + obj.messageContent + "</p>" + getseentime2(obj.sendTime) + "</div></div></div>");
                            } else if (obj.messageType == "Image") {
                                $('#chatdiv').append("<div class='chat chat-left'><div class='chat-avatar'>" + didv + " </div> <div class='chat-body'> <div class='chat-content '> <a target='_blank' href='" + HostUrlContents + "/ChatContent/Image/" + obj.messageContent + "'>  <img class='resimg' src='" + HostUrlContents + "/ChatContent/Image/" + obj.messageContent + "'/></a>" + getseentime2(obj.sendTime) + "</div></div></div>");
                            } else if (obj.messageType == "Audio") {
                                $('#chatdiv').append("<div class='chat chat-left'> <div class='chat-avatar'>" + didv + " </div><div class='chat-body'> <div class='chat-content'><p><audio controls> <source type='audio/mp3' src='" + HostUrlContents + "/ChatContent/Audio/" + obj.messageContent + "'></audio></p>" + getseentime2(obj.sendTime) + "</div></div></div>");
                            } else if (obj.messageType == "Doc") {
                                $('#chatdiv').append("<div class='chat chat-left'> <div class='chat-avatar'>" + didv + " </div><div class='chat-body'> <div class='chat-content'><p><a target='_blank' class='text-white' href='" + HostUrlContents + "/ChatContent/Doc/" + obj.messageContent + "'> <i class='site-menu-icon md-attachment-alt text-white font-size-30' aria-hidden='true'></i> <span class='site-menu-title'>Download attached file</span> </a> </p>" + getseentime2(obj.sendTime) + "</div></div></div>");
                            }
                            else if (obj.messageType == "Video") {
                                $('#chatdiv').append("<div class='chat chat-left'> <div class='chat-avatar'>" + didv + " </div><div class='chat-body'> <div class='chat-content'><p><video controls> <source type='video/mp4' src='" + HostUrlContents + "/ChatContent/Video/" + obj.messageContent + "'></video><br> <a href='" + HostUrlContents + "/ChatContent/Video/" + obj.messageContent + "' target='_blank'>Download this Video</a> </p>" + getseentime2(obj.sendTime) + "</div></div></div>");
                            }
                            lastmessage = 1;
                            $("#divloader").hide();
                        } else {
                            if (obj.messageType == "text") {
                                $('#chatdiv > div.chat > div.chat-body:last').append("<div class='chat-content'> <p dir='auto'>" + obj.messageContent + "</p>" + getseentime2(obj.sendTime) + "</div>");
                            } else if (obj.messageType == "Image") {
                                $('#chatdiv > div.chat > div.chat-body:last').append("<div class='chat-content '> <a target='_blank' href='" + HostUrlContents + "/ChatContent/Image/" + obj.messageContent + "'>  <img class='resimg' src='" + HostUrlContents + "/ChatContent/Image/" + obj.messageContent + "'/></a>" + getseentime2(obj.sendTime) + "</div>");
                            } else if (obj.messageType == "Audio") {
                                $('#chatdiv > div.chat > div.chat-body:last').append("<div class='chat-content'><p><audio controls> <source type='audio/mp3' src='" + HostUrlContents + "/ChatContent/Audio/" + obj.messageContent + "'></audio></p>" + getseentime2(obj.sendTime) + "</div>");
                            } else if (obj.messageType == "Doc") {
                                $('#chatdiv > div.chat > div.chat-body:last').append("<div class='chat-content'><p><a target='_blank' class='text-white' href='" + HostUrlContents + "/ChatContent/Doc/" + obj.messageContent + "'> <i class='site-menu-icon md-attachment-alt text-white font-size-30' aria-hidden='true'></i> <span class='site-menu-title'>Download attached file</span> </a> </p>" + getseentime2(obj.sendTime) + "</div>");
                            } else if (obj.messageType == "Video") {
                                $('#chatdiv > div.chat > div.chat-body:last').append("<div class='chat-content'><p><video controls> <source type='video/mp4' src='" + HostUrlContents + "/ChatContent/Video/" + obj.messageContent + "'></video><br> <a href='" + HostUrlContents + "/ChatContent/Video/" + obj.messageContent + "' target='_blank'>Download this Video</a> </p>" + getseentime2(obj.sendTime) + "</div>");
                            }
                            lastmessage = 0;
                            $("#divloader").hide();
                        }
                    }
                }
                scrolldown();
            } else {
                $("#historyBtn").hide();
                $('#chatdiv').append("<div id='nomes' class='text-center'><p dir='auto'><h2>No messages.</h2><p dir='auto'></div>");
                $("#divloader").hide();
            }
        });
    $("#chatb").prepend("<div id='chatwith'><div class='panel-heading' style='background-color:#eee;'> <h3 class='panel-title'>Chat with " + mmmname + "</h3> </div><hr/></div>");
    $("#divloader").hide();
    //$("#historyBtn").show();
}

function sendtoallstd(element) {
    renderChat3();
}
function renderChat3() {
    tid = "all";
    tc = "40";
    $("#chatform").show();
    $('#chatdiv').html("");
    $("#chatb").prepend("<div id='chatwith'><div class='panel-heading' style='background-color:#eee;'> <h3 class='panel-title'>Here you can send a message to all of your students:</h3> </div><hr/></div>");
}
function sendtoalltch(element) {
    renderChat4();
}
function renderChat4() {
    tid = "all";
    tc = "30";
    $("#chatform").show();
    $('#chatdiv').html("");
    $("#chatb").prepend("<div id='chatwith'><div class='panel-heading' style='background-color:#eee;'> <h3 class='panel-title'>Here you can send a message to all of your teachers:</h3> </div><hr/></div>");
}


function sendtoallsch(element) {
    renderChat8();
}
function renderChat8() {
    tid = "all";
    tc = "20";
    $("#chatform").show();
    $('#chatdiv').html("");
    $("#chatb").prepend("<div id='chatwith'><div class='panel-heading' style='background-color:#eee;'> <h3 class='panel-title'>Here you can send a message to all of your schools:</h3> </div><hr/></div>");
}



function sendtoresultstd() {
    renderChat5();
}

function renderChat5() {
    tid = "allresult";
    var res = 0;
    var names = "";
    var firstss = "";
    $('#StudentsFoo > tbody  > tr').each(function () {
        tid += "_" + this.id.split('_')[1];
        if (this.id != null && this.id != "" && this.id != "_undefined") {
            res = res + 1;
        }
        if (res == 1) {
            firstss = this.id;
            names += this.id.split('_')[3];
        } else {
            names += "، " + this.id.split('_')[3];
        }
    });
    if (res < 1) {
        bootbox.alert("Search result count is Zero!!");
        return;
    }
    if (res == 1) {
        $('#StudentsFoo > tbody').append('<tr onclick="whoclick(this)" class="tempfojs" style="visibility: collapse;" id="' + firstss + '"></tr>');
        $(".tempfojs").click();
        $(".tempfojs").remove();
        return;
    }
    var searchtext = "";
    searchtext = $("#StudentsFoo > thead > tr > th > form > div > div > input[placeholder='Search']").val();

    if (searchtext == "") {
        renderChat3();
        return;
    }

    if ((/\d/.test(searchtext)) == false) {
        searchtext = names;
    } else {
        if (searchtext.startsWith("vcv") || searchtext.startsWith("VCV")) {
            searchtext = "Level: " + searchtext;
        }
        else {
            searchtext = "Class: " + searchtext;
        }
    }


    tc = "40";
    $("#chatform").show();
    $('#chatdiv').html("");
    $("#chatb").prepend("<div id='chatwith'><div class='panel-heading' style='background-color:#eee;'> <h3 class='panel-title'>Send a message to (" + searchtext + ")</h3> </div><hr/></div>");
}

function sendtoresulttch() {
    renderChat6();
}

function renderChat6() {
    tid = "allresult";
    var res = 0;
    var names = "";
    var firstss = "";
    $('#TeachersFoo > tbody  > tr').each(function () {
        tid += "_" + this.id.split('_')[1];
        console.log("_" + this.id.split('_')[1]);
        if (this.id != null && this.id != "" && this.id != "_undefined") {
            res = res + 1;
        }
        if (res == 1) {
            firstss = this.id;
            names += this.id.split('_')[3];
        } else {
            names += "، " + this.id.split('_')[3];
        }
    });
    if (res < 1) {
        bootbox.alert("Search result count is Zero!!");
        return;
    }
    if (res == 1) {
        $('#TeachersFoo > tbody').append('<tr onclick="whoclick(this)" class="tempfojs" style="visibility: collapse;" id="' + firstss + '"></tr>');
        $(".tempfojs").click();
        $(".tempfojs").remove();
        return;
    }
    searchtext = $("#TeachersFoo > thead > tr > th > form > div > div > input[placeholder='Search']").val();

    if ((/\d/.test(searchtext)) == false) {
        searchtext = names;
    }
    console.log(names);
    console.log(searchtext);
    if (searchtext == "") {
        renderChat4();
        return;
    }

    tc = "30";
    $("#chatform").show();
    $('#chatdiv').html("");
    $("#chatb").prepend("<div id='chatwith'><div class='panel-heading' style='background-color:#eee;'> <h3 class='panel-title'>Send a message to (" + searchtext + ")</h3> </div><hr/></div>");
}


function sendtoresultsch() {
    renderChat7();
}

function renderChat7() {
    tid = "allresult";
    var res = 0;
    var names = "";
    var firstss = "";
    $('#SchoolsFoo > tbody  > tr').each(function () {
        tid += "_" + this.id.split('_')[1];
        console.log("_" + this.id.split('_')[1]);
        if (this.id != null && this.id != "" && this.id != "_undefined") {
            res = res + 1;
        }
        if (res == 1) {
            firstss = this.id;
            names += this.id.split('_')[3];
        } else {
            names += "، " + this.id.split('_')[3];
        }
    });
    if (res < 1) {
        bootbox.alert("Search result count is Zero!!");
        return;
    }
    if (res == 1) {
        $('#SchoolsFoo > tbody').append('<tr onclick="whoclick(this)" class="tempfojs" style="visibility: collapse;" id="' + firstss + '"></tr>');
        $(".tempfojs").click();
        $(".tempfojs").remove();
        return;
    }
    searchtext = $("#SchoolsFoo > thead > tr > th > form > div > div > input[placeholder='Search']").val();

    if ((/\d/.test(searchtext)) == false) {
        searchtext = names;
    }
    console.log(names);
    console.log(searchtext);
    if (searchtext == "") {
        renderChat8();
        return;
    }

    tc = "20";
    $("#chatform").show();
    $('#chatdiv').html("");
    $("#chatb").prepend("<div id='chatwith'><div class='panel-heading' style='background-color:#eee;'> <h3 class='panel-title'>Send a message to (" + searchtext + ")</h3> </div><hr/></div>");
}


function changesearch(el) {
    if ($(el).val() == null || $(el).val() == "") {
        $("#sendmesdyn").html('Send a message to: all of your students');
        $("#sendmesdyn").attr('onclick', 'sendtoallstd()');
    } else {
        $("#sendmesdyn").html('Send a message to: ' + $(el).val());
        $("#sendmesdyn").attr('onclick', 'sendtoresultstd()');
    }
}

function changesearch2(el) {
    if ($(el).val() == null || $(el).val() == "") {
        $("#sendmesdyn2").html('Send a message to: all of your teachers');
        $("#sendmesdyn2").attr('onclick', 'sendtoalltch()');
    } else {
        $("#sendmesdyn2").html('Send a message to: ' + $(el).val());
        $("#sendmesdyn2").attr('onclick', 'sendtoresulttch()');
    }
}

function changesearch3(el) {
    if ($(el).val() == null || $(el).val() == "") {
        $("#sendmesdyn2").html('Send a message to: all of your Schools');
        $("#sendmesdyn2").attr('onclick', 'sendtoallsch()');
    } else {
        $("#sendmesdyn2").html('Send a message to: ' + $(el).val());
        $("#sendmesdyn2").attr('onclick', 'sendtoresultsch()');
    }
}

function refreshnewmes() {
    $.getJSON(HostUrl + "/MyChats/GetNotification", {
        fid: fid,
        mycode: fc
    }, function (data) {
        if (data.count1 == 0) {
            $("#studentsbadge").html("");
        } else {
            $("#studentsbadge").html(data.count1);
        }
        if (data.count2 == 0) {
            $("#teachersbadge").html("");
        } else {
            $("#teachersbadge").html(data.count2);
        }
        if (data.count3 == 0) {
            $("#schoolsbadge").html("");
        } else {
            $("#schoolsbadge").html(data.count3);
        }
        if (data.count4 == 0) {
            $("#vcvsupportbadge").html("");
        } else {
            $("#vcvsupportbadge").html(data.count4);
        }
    });
}

function getseen(iseen) {
    if (iseen == true) {
        return "check-all";
    } else {
        return "check";
    }
}

function getseentime(seen, sendTime) {
    return "<i class='icon md-" + getseen(seen) + " chat-checks' aria-hidden='true'></i> <time class='chat-time'>" + sendTime + "</time>";
}

function getseentime2(sendTime) {
    return "<time class='chat-time'>" + sendTime + "</time>";
}
