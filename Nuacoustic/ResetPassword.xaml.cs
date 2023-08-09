using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Collections;
using System.Data;

namespace Nuacoustic
{
    /// <summary>
    /// Interaction logic for ResetPassword.xaml
    /// </summary>
    public partial class ResetPassword : Page
    {
        public string email = ForgotPassword.to;
        public Hashtable Logins;
        public ResetPassword()
        {
            InitializeComponent();
            Logins = (Application.Current.MainWindow as MainWindow).Logins;
        }

        private void ShareHashtable()
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.Logins = Logins;
        }

        // Calculates hashed password.
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

        int GetHashtableTotal()
        {            
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

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            string plainData = Convert.ToString(txtNewPassword.Password.Trim());
            string hashedData = ComputeSha256Hash(plainData);

            // int total = GetHashtableTotal();
            // int hashlocation = total % 1000;
            
            // Updates user's password.
            if (txtNewPassword.Password == txtConfirmNewPass.Password)
            {
                // Logins[hashlocation] = hashedData;
                SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-G9NKDOKB\SQLEXPRESS; Initial Catalog=NuacousticDB; Integrated Security=True;");
                sqlCon.Open();

                string sql = "UPDATE tblUser SET Password=@Password WHERE Email = @Email";

                using (SqlCommand cmd = new SqlCommand(sql, sqlCon))
                {
                    // Create and set the parameters values 
                    cmd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = hashedData;
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = email;
                    cmd.ExecuteNonQuery();
                }
                sqlCon.Close();
                MessageBox.Show("Password has been reset successfully.");
                ShareHashtable();
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
            else
            {
                MessageBox.Show("The two passwords do not match. Please try again.");
            }
        }
    }
}
