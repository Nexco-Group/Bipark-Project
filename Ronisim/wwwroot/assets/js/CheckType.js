$('input').keypress(function (event) {
    if (event.which == 13) {
        var closest = $(this).closest(':has(button)');
        event.preventDefault();
        if ($(this).parentsUntil(closest).nextAll('button').length >= 1) {
            //alert('[' + $(this).parentsUntil(closest).nextAll('button').first().text() + '] inside [' + $(closest).prop('tagName') + ' ' +  $(closest).attr('name') + ']');
            $(this).parentsUntil(closest).nextAll('button').first().click();
        } else {
            //alert('[' + $(this).parentsUntil(closest).nextAll().find('button').first().text() + '] inside [' + $(closest).prop('tagName') + ' ' +  $(closest).attr('name') + ']');
            $(this).parentsUntil(closest).nextAll().find('button').first().click();
        }
    }
});
//$('input').autocomplete();

$("input[lang='fa']").keypress(function (event) {
    var ew = event.which;
    return OnlyFaAlf(ew);
});
$("input[lang='en']").keypress(function (event) {
    var ew = event.which;
    return OnlyEnAlf(ew);
});
$("input[lang='enc']").keypress(function (event) {
    var ew = event.which;
    return OnlyEnChar(ew);
});
$("input[lang='num']").keypress(function (event) {
    var ew = event.which;
    return OnlyEnNum(ew);
});

$("input[lang='pnum']").keypress(function (event) {
    var ew = event.which;
    //if ($(this).val() != "") {
    //    var n = parseInt($(this).val().replace(/\D/g, ''), 10);
    //    $(this).val(n.toLocaleString());
    //}
    return OnlyEnpNum(ew);
});

$("input[lang='pnum']").on('keyup', function () {
    if ($(this).val() != "") {
        var n = parseInt($(this).val().replace(/\D/g, ''), 10);
        $(this).val(n.toLocaleString());
    }
});

$(document).on('keyup', '#inum2', function () {
    if ($(this).val() != "") {
        var n = parseInt($(this).val().replace(/\D/g, ''), 10);
        $(this).val(n.toLocaleString());
    }
});

$(document).on('keyup', '#inum2', function () {
    console.log("sa");
    if ($(this).val() != "") {
        $("#inum2o").html(wordifyRials(parseInt($(this).val().replace(/\D/g, ''), 10)));
    }
});

//$("#inum2").on("keyup", function () {
//    console.log("sa");
//    if ($(this).val() != "") {
//        $("#inum2o").html(wordifyRials(parseInt($(this).val().replace(/\D/g, ''), 10)));
//    }
//});

$("#pamount").on("keyup", function () {
    if ($(this).val() != "") {
        $("#pwamount").html(wordifyRials(parseInt($(this).val().replace(/\D/g, ''), 10)));
    }
});
$("#Price1").on("keyup", function () {
    if ($(this).val() != "") {
        $("#Price1o").html(wordifyRials(parseInt($(this).val().replace(/\D/g, ''), 10)));
    }
});
$("#Price2").on("keyup", function () {
    if ($(this).val() != "") {
        $("#Price2o").html(wordifyRials(parseInt($(this).val().replace(/\D/g, ''), 10)));
    }
});

function OnlyEnpNum(ew) {
    if (ew >= 0 && ew <= 32)
        return true;
    if (48 <= ew && ew <= 57)
        return true;
    if (ew == 17 && ew == 67 && ew == 86)
        return true;
    alert("فقط عدد و به انگلیسی تایپ کنید");
    return false;
}

function OnlyEnChar(ew) {
    console.log(ew);
    if (ew == 189)//-_
        return true;
    if (ew >= 0 && ew <= 32)
        return true;
    if (45 <= ew && ew <= 57)
        return true;
    if (65 <= ew && ew <= 95)
        return true;
    if (97 <= ew && ew <= 122)
        return true;
    if (ew == 17 && ew == 67 && ew == 86)
        return true;
    alert("تنها حروف انگلیسی و کارکتر مجاز میباشد");
    return false;
}

function OnlyEnAlf(ew) {
    if (ew >= 0 && ew <= 32)
        return true;
    if (48 <= ew && ew <= 57)
        return true;
    if (65 <= ew && ew <= 90)
        return true;
    if (97 <= ew && ew <= 122)
        return true;
    if (ew == 17 && ew == 67 && ew == 86)
        return true;
    alert("به انگلیسی تایپ کنید");
    return false;
}

function OnlyEnNum(ew) {
    if (ew >= 0 && ew <= 32)
        return true;
    if (48 <= ew && ew <= 57)
        return true;
    if (ew == 17 && ew == 67 && ew == 86)
        return true;
    alert("فقط عدد و به انگلیسی تایپ کنید");
    return false;
}
var farsiKey = [
    32, 33, 34, 35, 36, 37, 1548, 1711,
    41, 40, 215, 43, 1608, 45, 46, 47,
    1705, 44, 61, 46, 1567,
    64, 1616, 1584, 125, 1609, 1615, 1609, 1604,
    1570, 247, 1600, 1548, 47, 8217, 1583, 215,
    1563, 1614, 1569, 1613, 1601, 8216, 123, 1611,
    1618, 1573, 126, 1580, 1688, 1670, 94, 95,
    1662, 1588, 1584, 1586, 1740, 1579, 1576, 1604,
    1575, 1607, 1578, 1606, 1605, 1574, 1583, 1582,
    1581, 1590, 1602, 1587, 1601, 1593, 1585, 1589,
    1591, 1594, 1592, 60, 124, 62, 1617
];

function OnlyFaAlf(ew) {
    if (ew >= 0 && ew <= 32)
        return true;
    if (farsiKey.indexOf(ew) > -1)
        return true;
    if (ew == 17 && ew == 67 && ew == 86)
        return true;
    alert("به فارسی تایپ کنید");
    return false;
}

var wordifyfa = function (num, level) {
    'use strict';
    if (num === null) {
        return "";
    }
    // convert negative number to positive and get wordify value
    if (num < 0) {
        num = num * -1;
        return "منفی " + wordifyfa(num, level);
    }
    if (num === 0) {
        if (level === 0) {
            return "صفر";
        } else {
            return "";
        }
    }
    var result = "",
        yekan = [" یک ", " دو ", " سه ", " چهار ", " پنج ", " شش ", " هفت ", " هشت ", " نه "],
        dahgan = [" بیست ", " سی ", " چهل ", " پنجاه ", " شصت ", " هفتاد ", " هشتاد ", " نود "],
        sadgan = [" یکصد ", " دویست ", " سیصد ", " چهارصد ", " پانصد ", " ششصد ", " هفتصد ", " هشتصد ", " نهصد "],
        dah = [" ده ", " یازده ", " دوازده ", " سیزده ", " چهارده ", " پانزده ", " شانزده ", " هفده ", " هیجده ", " نوزده "];
    if (level > 0) {
        result += " و ";
        level -= 1;
    }

    if (num < 10) {
        result += yekan[num - 1];
    } else if (num < 20) {
        result += dah[num - 10];
    } else if (num < 100) {
        result += dahgan[parseInt(num / 10, 10) - 2] + wordifyfa(num % 10, level + 1);
    } else if (num < 1000) {
        result += sadgan[parseInt(num / 100, 10) - 1] + wordifyfa(num % 100, level + 1);
    } else if (num < 1000000) {
        result += wordifyfa(parseInt(num / 1000, 10), level) + " هزار " + wordifyfa(num % 1000, level + 1);
    } else if (num < 1000000000) {
        result += wordifyfa(parseInt(num / 1000000, 10), level) + " میلیون " + wordifyfa(num % 1000000, level + 1);
    } else if (num < 1000000000000) {
        result += wordifyfa(parseInt(num / 1000000000, 10), level) + " میلیارد " + wordifyfa(num % 1000000000, level + 1);
    } else if (num < 1000000000000000) {
        result += wordifyfa(parseInt(num / 1000000000000, 10), level) + " تریلیارد " + wordifyfa(num % 1000000000000, level + 1);
    }
    return result;

};

var wordifyRials = function (num) {
    'use strict';
    return wordifyfa(num, 0) + " ریال";
};

var wordifyRialsInTomans = function (num) {
    'use strict';
    if (num >= 10) {
        num = parseInt(num / 10, 10);
    } else if (num <= -10) {
        num = parseInt(num / 10, 10);
    } else {
        num = 0;
    }

    return wordifyfa(num, 0) + " تومان";
};

if (typeof module !== 'undefined' && module.exports) {
    module.exports.wordifyfa = wordifyfa;
    module.exports.wordifyRials = wordifyRials;
    module.exports.wordifyRialsInTomans = wordifyRialsInTomans;
}