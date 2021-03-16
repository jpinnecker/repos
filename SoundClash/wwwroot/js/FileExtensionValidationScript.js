$.validator.addMethod('fileextension', function (value, element) {
    return this.optional(element) || value.includes('.mp3');
});

$.validator.unobtrusive.adapters.add('fileextension', function (options) {
    var element = $(options.form).find('input#FormFile')[0];

    options.rules['fileextension'] = [element];
    options.messages['fileextension'] = options.message;
});