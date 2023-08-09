using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Security.Cryptography;
using System.Collections;

namespace Nuacoustic
{
    /// <summary>
    /// Interaction logic for LoginScreen.xaml
    /// </summary>

    public partial class LoginScreen : Page
    {
        public Hashtable Logins;
        public LoginScreen()
        {
            InitializeComponent();
            Logins = (Application.Current.MainWindow as MainWindow).Logins;
        }
        
        private void DisplayEmail()
        {            
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.email = txtEmail.Text;
        }

        private void ShareHashtable()
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.Logins = Logins;
        }

        // Calculates the hash of entered password.
        static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        void Temp()
        {
            string temp = ComputeSha256Hash("hello");
            MessageBox.Show(temp);
        }

        // int GetHashtableTotal()
        // {
        //     string email = Convert.ToString(txtEmail.Text);
        //     int countemail = 0;
        //     int arrayLength = email.Length;
        //     int[] emailstr = new int[arrayLength];
        //     int total = 0;
        //     foreach (char c in email)
        //     {
        //         emailstr[countemail] = System.Convert.ToInt32(c);
        // 
        //         total = emailstr[countemail] + total;
        //         countemail++;
        //     }
        // 
        //     return total;
        // }

        // Checks if email and password combination are in the database.
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            string plainData = Convert.ToString(txtPassword.Password.Trim());
            string hashedData = ComputeSha256Hash(plainData);

            // int total = GetHashtableTotal();
            // int hashlocation = total % 997;
            // 
            // if(Logins.ContainsKey(hashlocation) == false)
            // {
            //     MessageBox.Show("Your email or password is incorrect.");
            // }
            // else
            // {
            //     if ((Logins.ContainsKey(hashlocation) == true && (Convert.ToString(Logins[hashlocation]) == hashedData)))
            //     {
            //         DisplayEmail();
            //         ShareHashtable();
            //         MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            //         mainWindow.CurrentPage = ApplicationPage.HomePage;
            //     }
            // }
            
            SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-G9NKDOKB\SQLEXPRESS; Initial Catalog=NuacousticDB; Integrated Security=True;");
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                String query = "SELECT COUNT(1) FROM tblUser WHERE Email=@Email AND Password=@Password";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                sqlCmd.Parameters.AddWithValue("@Password", hashedData);
                int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                if (count >= 1)
                {
                    DisplayEmail();
                    MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
                    mainWindow.CurrentPage = ApplicationPage.HomePage;
                }
                else
                {
                    MessageBox.Show("Email or password is incorrect.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
        }

        private void btnClickToRegister_Click(object sender, RoutedEventArgs e)
        {
            ShareHashtable();
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow.CurrentPage != ApplicationPage.Register)
            {
                mainWindow.CurrentPage = ApplicationPage.Register;
            }
            else
            {
                mainWindow.MainFrame.NavigationService.GoForward();
            }
        }

        private void btnForgotPassword_Click(object sender, RoutedEventArgs e)
        {
            DisplayEmail();
            ShareHashtable();
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow.CurrentPage != ApplicationPage.ForgotPassword)
            {
                mainWindow.CurrentPage = ApplicationPage.ForgotPassword;
            }
            else
            {
                mainWindow.MainFrame.NavigationService.GoForward();
            }
        }
    }
}
