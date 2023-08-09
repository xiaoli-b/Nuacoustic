using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Collections;

namespace Nuacoustic
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {
        public Hashtable Logins;
        public Settings()
        {
            InitializeComponent();
            txtEmail.Text = (Application.Current.MainWindow as MainWindow).email;
            Logins = (Application.Current.MainWindow as MainWindow).Logins;
        }

        private void ShareHashtable()
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.Logins = Logins;
        }

        private void btnDeleteAccount_Click(object sender, RoutedEventArgs e)
        {
            ShareHashtable();
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow.CurrentPage != ApplicationPage.DeleteAccount)
            {
                mainWindow.CurrentPage = ApplicationPage.DeleteAccount;
            }
            else
            {
                mainWindow.MainFrame.NavigationService.GoForward();
            }
        }

        int GetHashtableTotal()
        {
            string email = Convert.ToString(txtEmail.Text);
            int countemail = 0;
            int arrayLength = email.Length;
            int[] emailstr = new int[arrayLength];
            int total = 0;
            foreach (char c in email)
            {
                emailstr[countemail] = System.Convert.ToInt32(c);
        
                total = emailstr[countemail] + total;
                countemail++;
            }
        
            return total;
        }

        private void btnSaveNewPass_Click(object sender, RoutedEventArgs e)
        {
            string oldHash = ComputeSha256Hash(Convert.ToString(txtOldPassword.Password));
            string hashedData = ComputeSha256Hash(Convert.ToString(txtConfirmNewPass.Password));

            // int total = GetHashtableTotal();
            // int hashlocation = total % 1000;

            if (txtOldPassword.Password == "" || txtConfirmNewPass.Password == "" || txtEmail.Text == "" || txtNewPassword.Password == "")
            {
                MessageBox.Show("Please enter values into all the boxes.");
            }
            // else if (txtNewPassword.Password != txtConfirmNewPass.Password)
            // {
            //     MessageBox.Show("Passwords do not match.");
            // }
            // else if (Logins.ContainsKey(hashlocation) == false || Logins.ContainsValue(oldHash) == false)
            // {
            //     MessageBox.Show("Old email or password is incorrect.");
            // }
            // else
            // {
            //     Logins[hashlocation] = hashedData;
            //     MessageBox.Show("Your password has been changed.");
            //     ShareHashtable();
            //     MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            //     if (mainWindow.CurrentPage != ApplicationPage.LoginScreen)
            //     {
            //         mainWindow.CurrentPage = ApplicationPage.LoginScreen;
            //     }
            //     else
            //     {
            //         mainWindow.MainFrame.NavigationService.GoForward();
            //     }
            // }

            // Checks old email and password combination to verify user.
            SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-G9NKDOKB\SQLEXPRESS; Initial Catalog=NuacousticDB; Integrated Security=True;");
            if (sqlCon.State == ConnectionState.Closed)
                sqlCon.Open();
            String query = "SELECT COUNT(1) FROM tblUser WHERE Email=@Email AND Password=@Password";
            SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Parameters.AddWithValue("@Email", txtEmail.Text);
            sqlCmd.Parameters.AddWithValue("@Password", oldHash);
            int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
            if (count == 0)
            {
                MessageBox.Show("Old email or password is incorrect.");
            }
            else
            {
                if (txtNewPassword.Password != txtConfirmNewPass.Password)
                {
                    MessageBox.Show("Passwords do not match.");
                }
            
                // Updates database with user's new password.
                string sql = "UPDATE tblUser SET Password=@Password WHERE Email = @Email";
            
                using (SqlCommand cmd = new SqlCommand(sql, sqlCon))
                {
                    // Create and set the parameters values 
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = txtEmail.Text.Trim();
                    cmd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = hashedData;
            
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Your password has been changed.");
            
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

        // Hashes entered password with SHA256 hash.
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
    }
}
