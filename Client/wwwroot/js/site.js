// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const navbar = document.querySelector(".navbar");

window.addEventListener('scroll', () => {
    navbar.classList.toggle("opacity-75", window.scrollY > 1);
    navbar.classList.toggle("fixed-top", window.scrollY > 1);
});