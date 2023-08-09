using System.ComponentModel;
using PropertyChanged;
using System.Collections;

namespace Nuacoustic
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    public partial class MainWindow : INotifyPropertyChanged
    {
        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
        }
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
        public ApplicationPage CurrentPage { get; set; }        
        public string email { get; set; }
        public string projectname { get; set; }
        public Hashtable Logins { get; set; }
    }
}