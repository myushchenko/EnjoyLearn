require.config({
    baseUrl: '/Scripts/app/',
    paths: {
        //"bootstrap": "../ui-bootstrap-tpls-0.3.0"      
        //angular: '../../Scripts/angular/angular'
    }
});

require
(
    [     
        "appModule",
        "appRoutes",
        
        'services/EnjoyLearnAPI',
        'services/EnjoyLearnContext',
        
        'utils/Events',
        'utils/Resource',
        'utils/Validation',
        'utils/RouteResolverServices',
        
        'directives/FormValidation',
        'directives/Smart-Table',
        
        "controllers/maintains/LanguageController",
        "controllers/maintains/FlashMessageController",
        "controllers/account/SignInController",
        "controllers/account/SignUpController",
        "controllers/account/ChangePasswordController",
        "controllers/account/ForgotPasswordController",
        "controllers/account/LoginController",
        "controllers/account/ProfileController",
        "controllers/account/RatingController",
        "controllers/dialogs/LoginDialogController",
        "controllers/dialogs/ProfileDialogController",
        "controllers/dialogs/AddWordDialogController",
        "controllers/dialogs/EditWordDialogController",
        "controllers/dialogs/NewLevelController",
        
        "controllers/extensions/SmartTableExtController"
    ],
    function ( ) {
        angular.bootstrap(document, ['EnjoyLearnApp']);       
    }
);