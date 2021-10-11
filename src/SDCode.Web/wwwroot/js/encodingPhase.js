function encodingInit(initOptions) {
    document.addEventListener("DOMContentLoaded", function() {
        var loadFailureElement = document.getElementById('unableToLoadImages');
        var loadingElement = document.getElementById('loadingDiv');

        function onSuccess(dataUrls) {
            var imageDataUrls = dataUrls.image;
            var audioDataUrls = dataUrls.audio;
            var images = [];
            var imageElement = document.getElementById('image');
            var numberElement = document.getElementById('number');
            var submittingResultsElement = document.getElementById('submittingResults');
            var submissionErrorElement = document.getElementById('submissionError');
            var spacebarDetected = false;
            var neglectedIndexes = [];
            var obscuredIndexes = [];
            var canShowNumberNext;
            var imageIndex = 0;
            var showNextImage = function() {
                hideBothImageAndNumber();
                var dataUrlKey = images[imageIndex];
                var imageDataUrl = imageDataUrls[dataUrlKey];
                imageElement.src = imageDataUrl;
                imageElement.style.display = 'block';
                imageIndex = imageIndex + 1;
                var whatToDo = imageIndex === images.length ? onLastImageShown : showPlusSign;
                canShowNumberNext = true;
                if (!isInViewport(imageElement)) {
                    obscuredIndexes.push(imageIndex);
                }
                if (initOptions.imageDisplayDurationInMilliseconds >= 4000 && audioDataUrls) {
                    var audioDataUrl = audioDataUrls[dataUrlKey];
                    var snd = new Audio(audioDataUrl);
                    snd.play();
                }
                setTimeout(whatToDo, initOptions.imageDisplayDurationInMilliseconds);
            };
        
            function onLastImageShown() {
                numberElement.style.display = 'none';
                imageElement.style.display = 'none';
                submitResults();
            }
        
            function getRandomInt(max) {
                return Math.floor(Math.random() * Math.floor(max));
            }
        
            var showNextNumber = function() {
                hideBothImageAndNumber();
                var randomNumber = getRandomInt(100);
                numberElement.innerText = randomNumber;
                numberElement.style.display = 'block';
        
                var timeSinceNumberDisplayedInMilliseconds = 0;
                spacebarDetected = false;
                var checkOnTheNumber = function() {
                    timeSinceNumberDisplayedInMilliseconds = timeSinceNumberDisplayedInMilliseconds + initOptions.numberCheckIntervalInMilliseconds;
                    if (timeSinceNumberDisplayedInMilliseconds > initOptions.numberDisplayThresholdInMilliseconds) {
                        neglectedIndexes.push(imageIndex);
                        showPlusSign();
                    } else if (spacebarDetected) {
                        showPlusSign();
                    } else {
                        setTimeout(checkOnTheNumber, initOptions.numberCheckIntervalInMilliseconds);
                    }
                };
                canShowNumberNext = false;
                setTimeout(checkOnTheNumber, initOptions.numberCheckIntervalInMilliseconds);
            };
        
            var hideBothImageAndNumber = function() {
                imageElement.style.display = 'none';
                numberElement.style.display = 'none';
            }
        
            var showNextImageOrNumber = function() {
                var randomNumber = getRandomInt(100);
                var shouldShowNumber = canShowNumberNext && randomNumber <= (initOptions.numberDisplayProbabilityPercentage);
                var whatToDo = shouldShowNumber ? showNextNumber : showNextImage;
                whatToDo();
            };
        
            function keyIsSpacebar(key) {
                return key === ' ';
            }
        
            document.body.onkeyup = function(e){
                if (keyIsSpacebar(e.key)) {
                    spacebarDetected = true;
                }
            }
        
            document.body.onkeydown = function(e){
                if (keyIsSpacebar(e.key)) { // prevent the browser from scrolling the webpage
                    e.preventDefault();
                }
            }
        
            var showPlusSign = function() {
                hideBothImageAndNumber();
                numberElement.innerText = '+';
                numberElement.style.display = 'block';
                setTimeout(showNextImageOrNumber, initOptions.plusSignDisplayDurationInMilliseconds);
            };
        
            document.getElementById('tryAgainButton').addEventListener('click', function() {
                submitResults();
            });
        
            var submitResults = function() {
                submissionErrorElement.style.display = 'none';
                submittingResultsElement.style.display = 'block';
                setTimeout(function(){
                    var xhr = new XMLHttpRequest();
                    xhr.open("POST", initOptions.recordResultsUrl, true);
                    xhr.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
                    xhr.onreadystatechange = function() {
                        submittingResultsElement.style.display = 'none';
                        var submissionFailed;
                        if (this.readyState === XMLHttpRequest.DONE) {
                            if (this.status === 200) {
                                var response = JSON.parse(this.response);
                                if (response.success) {
                                    var nextActionFormElement = document.getElementById('nextActionForm');
                                    nextActionFormElement.action = response.nextAction;
                                    nextActionFormElement.submit();
                                } else {
                                    submissionFailed = true;
                                }
                            } else {
                                submissionFailed = true;
                            }
                        }
                        if (submissionFailed) {
                            submissionErrorElement.style.display = 'block';
                        }
                    }
                    var neglectedIndexesCommaDelimited = neglectedIndexes.join(',');
                    var obscuredIndexesCommaDelimited = obscuredIndexes.join(',');
                    xhr.send(`participantID=${initOptions.participantID}&neglectedIndexesCommaDelimited=${neglectedIndexesCommaDelimited}&obscuredIndexesCommaDelimited=${obscuredIndexesCommaDelimited}`);
                }, 1000);
            };
        
            var xhr = new XMLHttpRequest();
            xhr.open("POST", initOptions.getImageDataUrlsUrl, true);
            xhr.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
            xhr.onreadystatechange = function() {
                if (this.readyState === XMLHttpRequest.DONE) {
                    loadingElement.style.display = 'none';
                    if (this.status === 200) {
                        images = JSON.parse(this.response);
                        showPlusSign();
                    } else {
                        loadFailureElement.style.display = 'block';
                    }
                }
            }
            xhr.send(`participantID=${initOptions.participantID}`);
        }

        function onFailure(err) {
            console.log('getDataUrls_OnError', err);
            loadingElement.style.display = 'none';
            loadFailureElement.style.display = 'block';
        }

        loadImagesInterface(initOptions.imageTypesImageUrlTemplate, initOptions.imageTypesAudioUrlTemplate, initOptions.imageTypesToPreload, 'progressBar', 'loadingPercentageSpan').then(onSuccess).catch(onFailure);
    });
}