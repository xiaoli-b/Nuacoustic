using System.Windows;
using System.Windows.Controls;

namespace Nuacoustic
{
    /// <summary>
    /// Interaction logic for ExitPage.xaml
    /// </summary>
    public partial class ExitPage : Page
    {
        public ExitPage()
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
    }
}
