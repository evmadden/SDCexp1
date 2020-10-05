function screenCheckInit(initOptions) {
    document.addEventListener("DOMContentLoaded", function() {
        var alertTimeoutId;
        var loginAlertElement = document.getElementById('loginAlert');
        var screenCheckFormElement = document.getElementById('screenCheckForm');
        screenCheckFormElement.addEventListener("submit", function(e){
            if (isFullScreen()) {
	            clearTimeout(alertTimeoutId);
            } else {
                e.preventDefault();
                loginAlertElement.classList.remove('alert-warning');
                loginAlertElement.classList.add('alert-danger');
                alertTimeoutId = setTimeout(function() {
                    loginAlertElement.classList.remove('alert-danger');
                    loginAlertElement.classList.add('alert-warning');
                }, 1000);
                var entireScreenGuidanceElement = document.getElementById('entireScreenGuidance');
                if (entireScreenGuidanceElement) {
                    entireScreenGuidanceElement.style.fontWeight = 'bold';
                }
            }
        });
    });
}