function homepageInit(initOptions) {
    document.addEventListener("DOMContentLoaded", function() {
        var loginFormElement = document.getElementById('loginForm');
        var loginButtonElement = document.getElementById('actionBarSubmitButton');
        var participantIdInputElement = document.getElementById('participantId');
        var nextActionAfterScreenCheckElement = document.getElementById('nextActionAfterScreenCheck');
        loginFormElement.addEventListener("submit", function(e){
            e.preventDefault();
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
                            nextActionAfterScreenCheckElement.value = response.nextActionAfterScreenCheck
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
    });
    });
}