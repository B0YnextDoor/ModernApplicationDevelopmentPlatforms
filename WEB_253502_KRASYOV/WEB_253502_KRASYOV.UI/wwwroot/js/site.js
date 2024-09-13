// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
$(document).ready(function () {
    $(document).on('click', '.page-link', function (e) {
        e.preventDefault();

        var url = $(this).data('ajax-url');
        console.log(url);
        $('#catalog').load(url);
    })
})
