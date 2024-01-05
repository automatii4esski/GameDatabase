// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//Alert

const alertEl = document.querySelector('.alert');

const hideAlert = function () {
    alertEl.classList.add('alert-hide');
}

setTimeout(hideAlert, 5000)

alertEl.addEventListener('click', hideAlert);