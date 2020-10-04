// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function isFullScreen() { // https://stackoverflow.com/a/58210211/116895
    var result = window.matchMedia('(display-mode: fullscreen)').matches || window.document.fullscreenElement;
    return result;
}

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


function getDataUrls(fetchUrls, progressCallback) {
    return new Promise(function(resolve, reject) {
        (function (fetchUrls, progressCallback, completedCallback, errorCallback) {
            var dataUrls = {};
            async function getHashes(urls) {
                var hashes = [];
                var operationsCompleted = 0;
                var operationsTotal = fetchUrls.length * 2;
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
            getHashes(fetchUrls).then(hashes => {
                hashes.forEach(hash=>{ Object.assign(dataUrls, hash); });
                completedCallback(dataUrls);
            }).catch(err => {
                errorCallback(err);
            });
        })(fetchUrls, progressCallback, resolve, reject);
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

function loadImagesInterface(imageTypesUrlTemplate, imageTypes, progressBarElementId, loadingPercentageElementId) {
    return new Promise(function(resolve, reject) {
        (function (imageTypes, progressBarElementId, loadingPercentageElementId, completedCallback, errorCallback) {
            var progressBarElement = document.getElementById(progressBarElementId);
            var loadingPercentageElement = document.getElementById(loadingPercentageElementId);
            var imageTypesToFetch = getImageTypesToFetch(imageTypes);
            var fetchUrls = imageTypesToFetch.map(x=>imageTypesUrlTemplate.replace('{0}', x));
            getDataUrls(fetchUrls, function(percentComplete) {
                progressBarElement.value = percentComplete;
                loadingPercentageElement.innerHTML = parseInt(percentComplete);
            }).then(completedCallback).catch(errorCallback);
        })(imageTypes, progressBarElementId, loadingPercentageElementId, resolve, reject);
    });
}