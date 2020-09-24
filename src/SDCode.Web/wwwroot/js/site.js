// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

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
    var style = window.getComputedStyle(element);
    return (style.display === 'none')
}