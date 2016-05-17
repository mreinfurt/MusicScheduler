System.register(['angular2/platform/browser', 'angular2/http', './main'], function(exports_1, context_1) {
    "use strict";
    var __moduleName = context_1 && context_1.id;
    var browser_1, http_1, main_1;
    return {
        setters:[
            function (browser_1_1) {
                browser_1 = browser_1_1;
            },
            function (http_1_1) {
                http_1 = http_1_1;
            },
            function (main_1_1) {
                main_1 = main_1_1;
            }],
        execute: function() {
            browser_1.bootstrap(main_1.App, [http_1.HTTP_PROVIDERS])
                .then(success => console.log(`Bootstrap success`))
                .catch(error => console.log(error));
        }
    }
});
//# sourceMappingURL=bootstrap.js.map