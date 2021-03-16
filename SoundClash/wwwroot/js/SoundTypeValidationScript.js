$.validator.addMethod('soundtype', function (value, element) {
    return this.optional(element) || value != 0;
});

$.validator.unobtrusive.adapters.add('soundtype', function (options) {
    var element = $(options.form).find('select#SoundUpload_Type')[0];

    options.rules['soundtype'] = [element];
    options.messages['soundtype'] = options.message;
});