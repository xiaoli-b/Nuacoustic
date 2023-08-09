using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Collections;

namespace Nuacoustic
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Page
    {
        public Hashtable Logins;

        string randomCode;
        public static string to;
        string connectionString = @"Data Source=LAPTOP-G9NKDOKB\SQLEXPRESS; Initial Catalog=NuacousticDB; Integrated Security=True;";

        public Register()
        {
            InitializeComponent();
            Logins = (Application.Current.MainWindow as MainWindow).Logins;
        }

        // Converts entered password to hashed password with SHA256 hash.
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
        private void ShareHashtable()
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.Logins = Logins;
        }

        private void btnClickToLogin_Click(object sender, RoutedEventArgs e)
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
                
        // Checks if email has already been used for an account.
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (txtEmail.Text == "" || txtPassword.Password == "" || txtFirstName.Text == "" || txtConfirmPass.Password == "")
            {
                MessageBox.Show("Please fill in mandatory fields.");
            }
            else if (txtPassword.Password != txtConfirmPass.Password)
            {
                MessageBox.Show("Passwords do not match.");
            }
            else
            {
                // using hashtable to see if email is already registered
                // int total = GetHashtableTotal();
                // int hashlocation = total % 1000;
                // if (Logins.ContainsKey(hashlocation) == true)
                // {
                //     MessageBox.Show("This email has already been registered.");
                // }
                
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    if (sqlCon.State == ConnectionState.Closed)
                        sqlCon.Open();
                    String query = "SELECT COUNT(1) FROM tblUser WHERE Email=@Email";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                    if (count >= 1)
                    {
                        MessageBox.Show("Your email has already been used");
                    }
                    else
                    {
                        MessageBox.Show("Registration is successful. A code will be sent to your email, please enter it in the box and click 'Verify.'");
                        // send email
                        string from, pass, messageBody;
                        Random rand = new Random();
                        randomCode = (rand.Next(999999)).ToString();
                        MailMessage message = new MailMessage();
                        to = (txtEmail.Text).ToString();
                        from = "nuacoustic.nuaire@gmail.com";
                        pass = "!testPass123!";
                        messageBody = "Your verification code is " + randomCode + ".";
                        message.To.Add(to);
                        message.From = new MailAddress(from);
                        message.Body = messageBody;
                        message.Subject = "Email Verification";
                        SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                        smtp.EnableSsl = true;
                        smtp.Port = 587;
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Credentials = new NetworkCredential(from, pass);
                        smtp.Send(message);

                    }
                }
                              
            }
        }

        int GetHashtableTotal()
        {
            // calculates hashtable index
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

        void Clear()
        {
            txtFirstName.Text = txtEmail.Text = txtPassword.Password = txtAdmin.Text = "";
        }

        // Checks if the verify code is correct.
        private void btnVerifyEmail_Click(object sender, RoutedEventArgs e)
        {
            // int total = GetHashtableTotal();
            // int hashlocation = total % 1000;

            string plainData = Convert.ToString(txtPassword.Password.Trim());
            string hashedData = ComputeSha256Hash(plainData);

            // If verification code is correct, the details of the new user are added to the database.
            if (randomCode == (txtVerifyEmail.Text).ToString())
            {
                // Logins.Add(hashlocation, hashedData);
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    if (sqlCon.State == ConnectionState.Closed)
                        sqlCon.Open();

                    string sql = "insert into dbo.tblUser ([FirstName], [Email], [Password], [Admin]) values(@FirstName,@Email,@Password,@Admin)";

                    // Create the connection (and be sure to dispose it at the end)
                    try
                    {
                        // Open the connection to the database. 
                        // This is the first critical step in the process.
                        // If we cannot reach the db then we have connectivity problems

                        // Prepare the command to be executed on the db
                        using (SqlCommand cmd = new SqlCommand(sql, sqlCon))
                        {
                            // Create and set the parameters values 
                            cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = txtFirstName.Text.Trim();
                            cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = txtEmail.Text.Trim();
                            cmd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = hashedData;
                            cmd.Parameters.Add("@Admin", SqlDbType.NVarChar).Value = txtAdmin.Text.Trim();
                            cmd.ExecuteNonQuery();

                        }
                    }
                    catch (Exception ex)
                    {
                        // We should log the error somewhere, 
                        // for this example let's just show a message
                        MessageBox.Show("ERROR:" + ex.Message);
                    }
                    MessageBox.Show("Your email has been verified.");
                    ShareHashtable();
                    MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
                    if (mainWindow.CurrentPage != ApplicationPage.MainPage)
                    {
                        mainWindow.CurrentPage = ApplicationPage.MainPage;
                    }
                    else
                    {
                        mainWindow.MainFrame.NavigationService.GoForward();
                    }
                }
            }
            else
            {
                MessageBox.Show("Wrong code");
            }
        }
    }
}
