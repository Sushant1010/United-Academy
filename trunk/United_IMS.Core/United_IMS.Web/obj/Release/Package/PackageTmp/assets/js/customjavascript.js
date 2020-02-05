function AD2BSNepaliText(fromelement, toelement) {
    var input = $('#' + fromelement).val();
    var inputelement = input.split('/');
    var fulldate = getNepaliDateInWords(AD2BS(inputelement[2].toString() + '-' + inputelement[1].toString() + '-' + inputelement[0].toString())) + " " + getNepaliDayFromDate(inputelement[2].toString() + '-' + inputelement[1].toString() + '-' + inputelement[0].toString());
    $('#'+toelement).text(fulldate);
};
function AD2BSDate(fromelement, toelement, outputformat) {
    var input = $('#' + fromelement).val();
    alert(input);
    var inputelement = input.split('/');
    var output = AD2BS(inputelement[2].toString() + '-' + inputelement[1].toString() + '-' + inputelement[0].toString());
    var outputelement = output.split('-');
    if (outputformat == "Y-M-D") {
        $('#' + toelement).val(output);
    }
    else if (outputformat == "D-M-Y") {
        $('#' + toelement).val(outputelement[2] + '-' + outputelement[1] + '-' + outputelement[0]);
    } else if (outputformat == "M-D-Y") {
        $('#' + toelement).val(outputelement[1] + '-' + outputelement[2] + '-' + outputelement[0]);
    }
}

function BS2ADDate(fromelement, toelement,outputformat) {
    //$('#' + toelement).text();
    var output = BS2AD($('#' + fromelement).val());

    var outputelement = output.split('-');
    if (outputformat == "Y-M-D") {
        $('#' + toelement).val(outputelement[0] + '/' + outputelement[1] + '/' + outputelement[2]);
    }
    else if (outputformat == "D-M-Y") {
        $('#' + toelement).val(outputelement[2] + '/' + outputelement[1] + '/' + outputelement[0]);
    } else if (outputformat == "M-D-Y") {
        $('#' + toelement).val(outputelement[1] + '/' + outputelement[2] + '/' + outputelement[0]);
    }
}
function BS2ADDate1(fromelement, toelement, outputformat) {
    //$('#' + toelement).text();
    var output = BS2AD(fromelement);

    var outputelement = output.split('-');
    if (outputformat == "Y-M-D") {
        $('#' + toelement).text(outputelement[0] + '/' + outputelement[1] + '/' + outputelement[2]);
    }
    else if (outputformat == "D-M-Y") {
        $('#' + toelement).text(outputelement[2] + '/' + outputelement[1] + '/' + outputelement[0]);
    } else if (outputformat == "M-D-Y") {
        $('#' + toelement).text(outputelement[1] + '/' + outputelement[2] + '/' + outputelement[0]);
    }
}
