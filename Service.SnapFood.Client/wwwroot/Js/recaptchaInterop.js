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
    }
};
