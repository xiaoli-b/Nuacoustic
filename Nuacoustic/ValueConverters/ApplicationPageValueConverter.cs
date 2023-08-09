using System;
using System.Globalization;
using System.Diagnostics;

namespace Nuacoustic.ValueConverters
{
    
     class ApplicationPageValueConverter : BaseValueConverter<ApplicationPageValueConverter>
     {
         public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
         {
             switch ((ApplicationPage)value)
             {
                case ApplicationPage.MainPage:
                     return new MainPage();

                case ApplicationPage.LoginScreen:
                     return new LoginScreen();

                case ApplicationPage.Register:
                     return new Register();

                case ApplicationPage.ForgotPassword:
                    return new ForgotPassword();

                case ApplicationPage.ResetPassword:
                    return new ResetPassword();

                case ApplicationPage.HomePage:
                    return new HomePage();

                case ApplicationPage.Settings:
                    return new Settings();

                case ApplicationPage.ExitPage:
                    return new ExitPage();

                case ApplicationPage.DeleteAccount:
                    return new DeleteAccount();

                case ApplicationPage.Project:
                    return new Project();

                case ApplicationPage.CreateNewProject:
                    return new CreateNewProject();

                case ApplicationPage.HowTo:
                    return new HowTo();

                case ApplicationPage.CurrentProjectSettings:
                    return new CurrentProjectSettings();


                default:
                     Debugger.Break();
                     return null;
             }
         }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

     }
}
