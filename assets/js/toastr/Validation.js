
/*****************************************************************************************/
// function to Validate Phone Number Must be 10 Digit and Numeric Only.
/*****************************************************************************************/
function validatePhoneNumber(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    var textBox = document.getElementById('ContentPlaceHolder1_TextBox_phone');
    // Check if the character entered is a numeric digit and if the length is less than 10
    if ((charCode >= 48 && charCode <= 57) && textBox.value.length < 10) {
        return true;
    }
    // Allow backspace (charCode 8) and prevent input if length is already 10
    if (charCode === 8 || textBox.value.length === 9) {
        return true;
    }
    return false;
}

/*****************************************************************************************/
// function to Validate Pin Code Must be 6 Digit and Numeric Only.
/*****************************************************************************************/
function validatePinCode(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    var textBox = document.getElementById('ContentPlaceHolder1_TextBox_pin');
    // Check if the character entered is a numeric digit and if the length is less than 10
    if ((charCode >= 48 && charCode <= 57) && textBox.value.length < 6) {
        return true;
    }
    // Allow backspace (charCode 8) and prevent input if length is already 10
    if (charCode === 8 || textBox.value.length === 5) {
        return true;
    }
    return false;
}

