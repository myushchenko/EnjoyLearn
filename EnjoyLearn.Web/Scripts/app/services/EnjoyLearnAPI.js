define(['appModule'], function (app) {
    app.factory('EnjoyLearnAPI', function ($http, $resource) {
        var urlPrefix = '/api/';
        return {
            User: $resource(urlPrefix + 'Users'),
            Session: $resource(urlPrefix + 'Sessions'),
            RatingLevel: $resource(urlPrefix + 'RatingLevels'),
            Dictionary: $resource(urlPrefix + 'Dictionary',
                { id: '@id' },
                {
                    create: { method: 'POST' },
                    update: { method: 'PUT' }
                }),
            UserDictionary: $resource(urlPrefix + 'UserDictionaries',
                { id: '@id' },
                {
                    create: { method: 'POST', headers: { 'Content-Type': 'application/json' } },
                    update: { method: 'PUT' }
                }),
            UserProfile: $resource(urlPrefix + 'UserProfiles',
               { id: '@id' },
               {
                   create: { method: 'POST' },
                   update: { method: 'PUT' }
               }),
            Password: {
                forgot: function (model) {
                    return $http.post(urlPrefix + 'passwords/forgot', model);
                },
                change: function (model) {
                    return $http.post(urlPrefix + 'passwords/change', model);
                }
            }
        };
    });
});