function allnumeric(inputtxt) {
    const regex = /^[0-9]+$/;
    const found = inputtxt.match(regex);
    if (found != null) {
        //  alert('Your Registration number has accepted....');
        return true;
    }
    else {


        return false;
    }
}
function ValidateEmail(inputText) {
    // alert("hii");
    var mailformat = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
    if (inputText.value.match(mailformat)) {

        return true;
    }
    else {
        //alert("You have entered an invalid email address!");
        //document.form1.text1.focus();
        return false;
    }
}

function stringlength(inputtxt, minlength, maxlength) {
    var field = inputtxt.value;
    var mnlen = minlength;
    var mxlen = maxlength;

    if (field.length < mnlen || field.length > mxlen) {

        return false;
    }
    else {
        //alert('Your userid have accepted.');
        return true;
    }
}
function required(inputtxt) {

    var empt = inputtxt;
    if (empt == "") {
        //alert("Please input a Value");
        return false;
    }
    else {
        //alert('Code has accepted : you can try another');
        return true;
    }
}