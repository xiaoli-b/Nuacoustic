using System;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using System.Data.SqlClient;

namespace Nuacoustic
{
    /// <summary>
    /// Interaction logic for DeleteAccount.xaml
    /// </summary>
    public partial class DeleteAccount : Page
    {
        public DeleteAccount()
        {
            InitializeComponent();
        }

        // Deletes both user account and the projects they created
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-G9NKDOKB\SQLEXPRESS; Initial Catalog=NuacousticDB; Integrated Security=True;");

            if (sqlCon.State == ConnectionState.Closed)
                sqlCon.Open();
            try
            {
                string sql = "DELETE FROM tblUser WHERE Email=@Email";
                using (SqlCommand cmd = new SqlCommand(sql, sqlCon))
                {
                    // Create and set the parameters values 
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = txtEmail.Text.Trim();
                    
                    cmd.ExecuteNonQuery();
                }

                string sql1 = "DELETE FROM tblProject WHERE Email=@Email";
                using (SqlCommand cmd = new SqlCommand(sql1, sqlCon))
                {
                    // Create and set the parameters values 
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = txtEmail.Text.Trim();

                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Your information has been deleted.");
            }
            catch(Exception ex)
            {
                MessageBox.Show(Convert.ToString(ex));
            }

            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow.CurrentPage != ApplicationPage.ExitPage)
            {
                mainWindow.CurrentPage = ApplicationPage.ExitPage;
            }
            else
            {
                mainWindow.MainFrame.NavigationService.GoForward();
            }
            MessageBox.Show("Your account has been deleted.");
            sqlCon.Close();
        }
    }
}
