$.validator.addMethod('filesize', function (value, element) {
    return this.optional(element) || element.files[0].size < 2097152;
});

$.validator.unobtrusive.adapters.add('filesize', function (options) {
    var element = $(options.form).find('input#FormFile')[0];

    options.rules['filesize'] = [element];
    options.messages['filesize'] = options.message;
});