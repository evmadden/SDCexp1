function homepageInit(initOptions) {
    var alertTimeoutId;
    document.addEventListener("DOMContentLoaded", function() {
        var loginFormElement = document.getElementById('loginForm');
        var loginButtonElement = document.getElementById('actionBarSubmitButton');
        var participantIdInputElement = document.getElementById('participantId');
        var loginAlertElement = document.getElementById('loginAlert');
        loginFormElement.addEventListener("submit", function(e){
            e.preventDefault();
            clearTimeout(alertTimeoutId);
            if (isFullScreen()) {
                var participantID = participantIdInputElement.value;
                loginButtonElement.setAttribute('disabled', 'disabled');
                var originalLoginButtonText = loginButtonElement.value;
                loginButtonElement.value = 'Wait...';
                var xhr = new XMLHttpRequest();
                xhr.open("POST", initOptions.loginUrl, true);
                xhr.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
                xhr.onreadystatechange = function() {
                    if (this.readyState === XMLHttpRequest.DONE) {
                        var errorMessage;
                        if (this.status === 200) {
                            var response = JSON.parse(this.response);
                            if (response.success) {
                                var nextFormElement = document.getElementById('nextForm');
                                var nextParticipantIdElement = document.getElementById('nextParticipantID');
                                var nextWhenToReturnElement = document.getElementById('nextWhenToReturn');
                                nextFormElement.action = response.action;
                                nextParticipantIdElement.value = response.participantID;
                                nextWhenToReturnElement.value = response.whenToReturn;
                                nextFormElement.submit();
                            } else {
                                errorMessage = "There was an unexpected error."
                            }
                        } else {
                            errorMessage = "There was an unexpected error."
                        }
                        if (errorMessage) {
                            setTimeout(function() {
                                alert(errorMessage);
                            },25);
                            loginButtonElement.value = originalLoginButtonText;
                            loginButtonElement.removeAttribute('disabled');
                        }
                    }
                }
                xhr.send(`participantID=${participantID}`);
            } else {
                loginAlertElement.classList.remove('alert-warning');
                loginAlertElement.classList.add('alert-danger');
                alertTimeoutId = setTimeout(function() {
                    loginAlertElement.classList.remove('alert-danger');
                    loginAlertElement.classList.add('alert-warning');
                }, 1000);
            }
        });
    });
}