window.recaptchaInterop = {
    render: function (elementId, siteKey) {
        return new Promise((resolve, reject) => {
            if (typeof grecaptcha === "undefined") {
                reject("grecaptcha chưa sẵn sàng");
                return;
            }
            grecaptcha.ready(function () {
                try {
                    var widgetId = grecaptcha.render(elementId, { sitekey: siteKey });
                    document.getElementById(elementId).setAttribute('data-widget-id', widgetId);
                    resolve(widgetId);
                } catch (e) {
                    reject(e.toString());
                }
            });
        });
    },
    getResponse: function (widgetId) {
        return grecaptcha.getResponse(widgetId);
    },
    reset: function (widgetId) {
        grecaptcha.reset(widgetId);
    },
    getWidgetId: function (elementId) {
        var el = document.getElementById(elementId);
        if (el && el.getAttribute('data-widget-id')) {
            return parseInt(el.getAttribute('data-widget-id'));
        }
        return -1;  // Chưa render
    }
};