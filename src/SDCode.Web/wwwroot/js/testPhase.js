function testInit(initOptions) {
    var obscuredIndexes = [];
    var imageIndex = 0;
    document.addEventListener("DOMContentLoaded", function(){
        var loadingElement = document.getElementById('loadingDiv');
        var unableToLoadImagesElement = document.getElementById('unableToLoadImages');
        var snd;

        function onSuccess(dataUrls) {
            loadingElement.style.display = 'none';
            var imageElement = document.getElementById('image');
            var imageContainerElement = document.getElementById('imageContainer');
            var confidenceElement = document.getElementById('confidence');
            var feedbackElement = document.getElementById('feedback');
            var feedbackMessageElement = document.getElementById('feedbackMessage');
            var waitElement = document.getElementById('wait');
            var retryFirstImageContainerElement = document.getElementById('retryFirstImageContainer');
            var retryFirstImageButtonElement = document.getElementById('retryFirstImageButton');
            var imageJudgement;
            var imageConfidence;
            var imageShownAt;
            var showImage = function(url, progress) {
                feedbackElement.style.display = 'none';
                imageElement.setAttribute('data-progress', progress);
                imageElement.src = dataUrls.image[url];
                imageJudgement = null;
                imageConfidence = null;
                imageShownAt = new Date().getTime();
                imageReactionTime = null;
                if (dataUrls.audio) {
                    var audioDataUrl = dataUrls.audio[url];
                    if (audioDataUrl) {
                        snd = new Audio(audioDataUrl);
                        snd.play();
                    }
                }
            }
            var sendResponse = function() {
                var waitId = setTimeout(function() {
                    waitElement.style.display = 'table';
                }, 500);
                var xhr = new XMLHttpRequest();
                xhr.open("POST", initOptions.responseDataUrl, true);
                xhr.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
                xhr.onreadystatechange = function() {
                    if (this.readyState === XMLHttpRequest.DONE) {
                        if (this.status === 200) {
                            clearTimeout(waitId);
                            waitElement.style.display = 'none';
                            var response = JSON.parse(this.response);
                            if (response.testEnded) {
                                document.getElementById('imageViewer').style.display = 'none';
                                document.getElementById('obscuredIndexesCommaDelimited').value = obscuredIndexes.join(',');
                                var nextAction = obscuredIndexes.length > 0 ? initOptions.questionsUrl : initOptions.endUrl;
                                var nextActionForm = document.getElementById('nextActionForm');
                                nextActionForm.action = nextAction;
                                nextActionForm.submit();
                            } else {
                                feedbackMessageElement.innerText = `${response.feedback ? initOptions.feedbacksCorrectDisplay : initOptions.feedbacksIncorrectDisplay}`;
                                feedbackElement.style.display = 'table';
                                setTimeout(function () {
                                    showImage(response.viewModel.imageToDisplay, response.viewModel.progress);
                                }, initOptions.feedbackDisplayDurationInMilliseconds);
                            }
                        }
                    }
                }
                var progress = imageElement.getAttribute('data-progress');
                xhr.send(`participantID=${initOptions.participantID}&progress=${progress}&sessionID=${initOptions.sessionID}&judgement=${imageJudgement}&confidence=${imageConfidence}&reactionTime=${imageReactionTime}`);
            };
            imageElement.onload = function() { 
                imageContainerElement.style.display = 'block';
                if (!isInViewport(imageElement)) {
                    obscuredIndexes.push(imageIndex);
                    imageIndex = imageIndex + 1
                }
                if (initOptions.shouldAutomate) {
                    var possibleJudgementActions = [()=>onArrowLeft(),()=>onArrowRight()];
                    var possibleConfidenceActions = [()=>onNumber1(),()=>onNumber2(),()=>onNumber3(),()=>onNumber4(),];
                    var judgementAction = possibleJudgementActions[Math.floor(Math.random() * possibleJudgementActions.length)];
                    var confidenceAction = possibleConfidenceActions[Math.floor(Math.random() * possibleConfidenceActions.length)];
                    setTimeout(function(){
                        judgementAction();
                        setTimeout(confidenceAction, initOptions.automationDelayInMilliseconds);
                    }, initOptions.automationDelayInMilliseconds);
                }
            };
            var onJudgement = function(judgement) {
                if (isVisible(imageElement)) {
                    snd?.pause();
                    imageJudgement = judgement;
                    imageReactionTime = new Date().getTime() - imageShownAt;
                    imageContainerElement.style.display = 'none';
                    confidenceElement.style.display = 'table';
                }
            };
            var onConfidence = function(confidence) {
                if (isVisible(confidenceElement)) {
                    confidenceElement.style.display = 'none';
                    imageConfidence = confidence;
                    sendResponse();
                }
            };
            function onArrowLeft() { onJudgement(initOptions.judgementsOld); }
            function onArrowRight() { onJudgement(initOptions.judgementsNew); }
            function onNumber1() { onConfidence(initOptions.confidencesNotConfident); }
            function onNumber2() { onConfidence(initOptions.confidencesSomewhatConfident); }
            function onNumber3() { onConfidence(initOptions.confidencesConfident); }
            function onNumber4() { onConfidence(initOptions.confidencesVeryConfident); }
            document.addEventListener('keyup', function(e) {
                if (!(e.ctrlKey || e.altKey || e.shiftKey || e.metaKey)) {
                    const callback = {
                        "ArrowLeft"  : onArrowLeft,
                        "ArrowRight" : onArrowRight,
                        "1" : onNumber1,
                        "2" : onNumber2,
                        "3" : onNumber3,
                        "4" : onNumber4
                    }[e.key];
                    callback?.();
                }
            });
            var showFirstImage = function() {
                var waitId = setTimeout(function() {
                    waitElement.style.display = 'table';
                }, 500);
                var xhr = new XMLHttpRequest();
                xhr.open("POST", initOptions.getImageUrl, true);
                xhr.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
                xhr.onreadystatechange = function() {
                    if (this.readyState === XMLHttpRequest.DONE) {
                        clearTimeout(waitId);
                        waitElement.style.display = 'none';
                        if (this.status === 200) {
                            retryFirstImageContainerElement.style.display = 'none';
                            var response = JSON.parse(this.response);
                            showImage(response.viewModel.imageToDisplay, response.viewModel.progress);
                        } else {
                            retryFirstImageContainerElement.style.display = 'table';
                        }
                    }
                }
                xhr.send(`participantID=${initOptions.participantID}&progress=${initOptions.progress}`);            
            };

            retryFirstImageButtonElement.addEventListener("click", function(){
                retryFirstImageContainerElement.style.display = 'none';
                setTimeout(showFirstImage, 500);
            });

            showFirstImage();            
        }

        function onFailure(err) {
            console.log('getDataUrls_OnError', err);
            loadingElement.style.display = 'none';
            unableToLoadImagesElement.style.display = 'table';
        }

        loadImagesInterface(initOptions.imageTypesImageUrlTemplate, initOptions.imageTypesAudioUrlTemplate, initOptions.imageTypesToPreload, 'progressBar', 'loadingPercentageSpan').then(onSuccess).catch(onFailure);
    });
}