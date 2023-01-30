$(document).ready(function () {
    let button = $("#CreateAccountButton")
    button.on('click', (e) => {
        let pass1 = $("#ACCOUNTPASS").val();
        let pass2 = $("#password_2").val();
        if (pass1 != pass2) {
            e.preventDefault();
            alert("hai mật khẩu không trùng nhau")
        }
    })
})