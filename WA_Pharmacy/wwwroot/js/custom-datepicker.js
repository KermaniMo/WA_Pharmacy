function initPersianDatepicker(selector) {
    if (typeof jQuery !== 'undefined' && $.fn.persianDatepicker) {
        var $elements = $(selector);
        if ($elements.length > 0) {
            $elements.persianDatepicker({
                format: 'YYYY/MM/DD',      // فرمت ذخیره‌سازی
                initialValue: false,       // مقدار پیش‌فرض نذاره (مگر اینکه ولیو داشته باشه)
                autoClose: true,           // بعد از انتخاب تاریخ بسته بشه
                calendar: {
                    persian: {
                        locale: 'fa'
                    }
                }
            });
            console.log('Persian Datepicker initialized on selector: ' + selector);
        } else {
            console.warn('No elements found for selector: ' + selector);
        }
    } else {
        console.error('jQuery or persianDatepicker plugin is not loaded.');
    }
}
