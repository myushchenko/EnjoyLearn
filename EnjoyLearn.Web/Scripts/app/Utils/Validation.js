define(['appModule'], function (app) {
    app.service('Validation', function () {
        this.hasModelErrors = function (response) {
            return response.status === 400;
        };

        this.getModelErrors = function (response) {
            var errors = [];
            //alert(JSON.stringify(response.data));
            if (response.data.ModelState) {
                try {
                    angular.forEach(response.data.ModelState, function (messages) {
                        angular.forEach(messages, function (message) {
                        errors.push(message);
                    });
                });
                } catch (e) {
                    alert(e);
                }
            }
            return errors.length ? errors : void (0);
        };
    });
});