using System.Windows;
using System.Windows.Controls;
using System.Collections;

namespace Nuacoustic
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public Hashtable hashtable;
        public MainPage()
        {
            InitializeComponent();
            hashtable = (Application.Current.MainWindow as MainWindow).Logins;
        }

        private void ShareHashtable()
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.Logins = hashtable;
        }

        private void btnClickToLogin_Click(object sender, RoutedEventArgs e)
        {
            ShareHashtable();
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
