// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//Validator

jQuery.validator.addMethod("minagedate", function (value, element, params) {
    if (!value) return true; 

    var dateOfBirth = new Date(value);
    var minAge = parseInt(params.minage);
    var today = new Date();
    var age = today.getFullYear() - dateOfBirth.getFullYear();

    if (age < minAge)  return false; 

    return true; 
});

jQuery.validator.unobtrusive.adapters.add("minagedate", ["minage"], function (options) {
    options.rules["minagedate"] = {
        minage: options.params.minage
    };
    options.messages["minagedate"] = options.message;
});

jQuery.validator.addMethod("maxdate", function (value, element, params) {
    if (!value) return true;

    var dateToCheck = new Date(value);
    var maxDate = new Date(params.date);
    if (dateToCheck > maxDate) return false;

    return true;
});

jQuery.validator.unobtrusive.adapters.add("maxdate", ["date"], function (options) {
    options.rules["maxdate"] = {
        date: options.params.date
    };
    options.messages["maxdate"] = options.message;
});