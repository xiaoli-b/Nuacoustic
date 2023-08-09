using System.Windows;
using System.Windows.Controls;

namespace Nuacoustic
{
    /// <summary>
    /// Interaction logic for HowTo.xaml
    /// </summary>
    public partial class HowTo : Page
    {
        public HowTo()
        {
            InitializeComponent();
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow.CurrentPage != ApplicationPage.HomePage)
            {
                mainWindow.CurrentPage = ApplicationPage.HomePage;
            }
            else
            {
                mainWindow.MainFrame.NavigationService.GoForward();
            }
        }

        private void btnHowTo_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow.CurrentPage != ApplicationPage.HowTo)
            {
                mainWindow.CurrentPage = ApplicationPage.HowTo;
            }
            else
            {
                mainWindow.MainFrame.NavigationService.GoForward();
            }
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow.CurrentPage != ApplicationPage.Settings)
            {
                mainWindow.CurrentPage = ApplicationPage.Settings;
            }
            else
            {
                mainWindow.MainFrame.NavigationService.GoForward();
            }
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {            
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow.CurrentPage != ApplicationPage.LoginScreen)
            {
                mainWindow.CurrentPage = ApplicationPage.LoginScreen;
            }
            else
            {
                mainWindow.MainFrame.NavigationService.GoForward();
            }
        }
    }
}
