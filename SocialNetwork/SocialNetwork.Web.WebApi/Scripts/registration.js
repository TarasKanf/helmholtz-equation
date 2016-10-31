$(function () {
    $("form").submit(function (event) {
        event.preventDefault();
        if ($('.form-control').valid()) {

            var sendingData = {
                UserName: $("#login").val(),
                Email: $("#e-mail").val(),
                Password: $("#password").val(),
                ConfirmPassword: $("#password").val(),
                FirstName: $("#first-name").val(),
                LastName: $("#last-name").val()
            }

            $.post("/RegistrationApi/Register",
                sendingData).then
                (function (data) {
                    $("#listreg").text(' ');
                    document.location.href = "/Authentication/Login";
                }, function (error) {

                    var errors = [];
                    for (var key in error.responseJSON.ModelState) {
                        for (var i = 0; i < error.responseJSON.ModelState[key].length; i++) {
                            errors.push(error.responseJSON.ModelState[key][i])
                        }
                    }
                    var message = errors.join(' ');
                    $("#listreg").text(message);
                }
                );
        }
    });
});