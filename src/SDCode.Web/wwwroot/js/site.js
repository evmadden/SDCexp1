﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function isFullScreen() { // https://stackoverflow.com/a/52160506/116895
    const windowWidth = window.innerWidth * window.devicePixelRatio;
    const windowHeight = window.innerHeight * window.devicePixelRatio;
    const screenWidth = window.screen.width * window.devicePixelRatio;
    const screenHeight = window.screen.height * window.devicePixelRatio;
    const widthUsed = windowWidth/screenWidth;
    const heightUsed = windowHeight/screenHeight;
    var result = widthUsed>=0.95 && heightUsed>=0.95;
    return result;
}

function enforceFullscreen() {
    var deviceIsLargeEnough = !isVisible(document.getElementById('deviceTooSmallContainer'));
    if (deviceIsLargeEnough) {
        var mustBeInFullscreen = document.querySelectorAll('[data-fullscreenexempt]').length == 0;
        var loginAlertElement = document.getElementById('loginAlert');
        var loginConfirmElement = document.getElementById('loginConfirm');
        var browserIsFullscreen = isFullScreen();
        if (loginAlertElement && loginConfirmElement) {
            loginAlertElement.style.display = browserIsFullscreen ? 'none' : 'block';
            loginConfirmElement.style.display = browserIsFullscreen ? 'block' : 'none';
        }
        if (mustBeInFullscreen) {
            document.getElementById('container').style.display = browserIsFullscreen ? 'block' : 'none';
            document.getElementById('mustBeFullscreenContainer').style.display = browserIsFullscreen ? 'none' : 'block';
        } else {
            document.getElementById('container').style.display = 'block';
            document.getElementById('mustBeFullscreenContainer').style.display = 'none';
            var fullscreenGuidanceElement = document.getElementById('fullscreenGuidance');
            if (fullscreenGuidanceElement) {
                fullscreenGuidanceElement.style.display = browserIsFullscreen ? 'none' : 'block';
            }
        }
    } else {
        document.getElementById('container').style.display = 'none';
        document.getElementById('mustBeFullscreenContainer').style.display = 'none';
    }
    setTimeout(enforceFullscreen, 100);
}
document.addEventListener("DOMContentLoaded", enforceFullscreen);

function isInViewport(element) { // https://www.javascripttutorial.net/dom/css/check-if-an-element-is-visible-in-the-viewport
    const rect = element.getBoundingClientRect();
    return (
        rect.top >= 0 &&
        rect.left >= 0 &&
        rect.bottom <= (window.innerHeight || document.documentElement.clientHeight) &&
        rect.right <= (window.innerWidth || document.documentElement.clientWidth)
    );
}

function isVisible(element) {
    return !isHidden(element);
}

function isHidden(element) {
    var result = (element.offsetParent === null); // https://stackoverflow.com/a/21696585/116895
    return result;
}

function allProgress(proms, progress_cb) { // <3 https://stackoverflow.com/a/42342373/116895
    let d = 0;
    progress_cb(0);
    for (const p of proms) {
      p.then(()=> {    
        d ++;
        progress_cb( (d * 100) / proms.length );
      });
    }
    return Promise.all(proms);
  }


function getDataUrls(imageFetchUrls, audioFetchUrls, progressCallback) {
    audioFetchUrls = audioFetchUrls || [];
    return new Promise(function(resolve, reject) {
        (function (imageFetchUrls, audioFetchUrls, progressCallback, completedCallback, errorCallback) {
            var operationsCompleted = 0;
            var operationsTotal = (imageFetchUrls.length * 2) + (audioFetchUrls.length * 2);
            async function getHashes(urls) {
                var hashes = [];
                function advanceProgress() {
                    operationsCompleted = operationsCompleted + 1;
                    var percentComplete = (operationsCompleted / operationsTotal) * 100;
                    progressCallback(percentComplete);
                }
                for (let url of urls) {
                    advanceProgress();
                    let response = await fetch(url);
                    if (response.ok) {
                        let hash = await response.json();
                        hashes.push(hash);
                        advanceProgress();
                    } else {
                        throw `Unable to fetch image data (url:'${url}';responseStatus: '${response.status}').`;
                    }
                }
                return hashes;
            }
            getHashes(imageFetchUrls).then(imageHashes => {
                var dataUrls = {};
                dataUrls.image = {};
                dataUrls.audio = {};
                imageHashes.forEach(imageHash=>{ Object.assign(dataUrls.image, imageHash); });
                if (audioFetchUrls) {
                    getHashes(audioFetchUrls).then(audioHashes => {
                        audioHashes.forEach(audioHash=>{ Object.assign(dataUrls.audio, audioHash); });
                        completedCallback(dataUrls);
                    }).catch(err => {
                        errorCallback(err);
                    });
                } else {
                    completedCallback(dataUrls);
                }
            }).catch(err => {
                errorCallback(err);
            });
        })(imageFetchUrls, audioFetchUrls, progressCallback, resolve, reject);
    });
}

function getImageTypesToFetch(imageTypes) {
    var result = [];
    imageTypes.forEach(imageType=>{
        if (imageType.startsWith('N')) { // N files are split to avoid data download files being too large
            [1,2,3].forEach(splitNumber=>{
                result.push(`${imageType}${splitNumber}`);
            });
        } else {
            result.push(imageType);
        }
    });
    return result;
}

function loadImagesInterface(imageTypesImageUrlTemplate, imageTypesAudioUrlTemplate, imageTypes, progressBarElementId, loadingPercentageElementId) {
    return new Promise(function(resolve, reject) {
        (function (imageTypes, progressBarElementId, loadingPercentageElementId, completedCallback, errorCallback) {
            var progressBarElement = document.getElementById(progressBarElementId);
            var loadingPercentageElement = document.getElementById(loadingPercentageElementId);
            var imageTypesToFetch = getImageTypesToFetch(imageTypes);
            var imageFetchUrls = imageTypesToFetch.map(x=>imageTypesImageUrlTemplate.replace('{0}', x));
            var audioFetchUrls = imageTypesAudioUrlTemplate ? imageTypesToFetch.map(x=>imageTypesAudioUrlTemplate.replace('{0}', x)) : null;
            getDataUrls(imageFetchUrls, audioFetchUrls, function(percentComplete) {
                progressBarElement.value = percentComplete;
                loadingPercentageElement.innerHTML = parseInt(percentComplete);
            }).then(completedCallback).catch(errorCallback);
        })(imageTypes, progressBarElementId, loadingPercentageElementId, resolve, reject);
    });
}