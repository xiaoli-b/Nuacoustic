using System;
using System.Windows;
using System.Windows.Controls;
using System.Net;
using System.Net.Mail;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace Nuacoustic
{
    /// <summary>
    /// Interaction logic for ForgotPassword.xaml
    /// </summary>
    public partial class ForgotPassword : Page
    {
        string randomCode;
        public static string to;

        public Hashtable Logins;
        
        public ForgotPassword()
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

        // Sends code to users email in order to verify it is them.
        private void btnSendCode_Click(object sender, RoutedEventArgs e)
        {
            string from, pass, messageBody;
            Random rand = new Random();
            randomCode = (rand.Next(999999)).ToString();
            MailMessage message = new MailMessage();
            to = (txtEmail.Text).ToString();
            from = "nuacoustic.nuaire@gmail.com";
            pass = "!testPass123!";
            messageBody = "Your reset code is " + randomCode + ".";
            message.To.Add(to);
            message.From = new MailAddress(from);
            message.Body = messageBody;
            message.Subject = "Password reset";
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential(from, pass);

            // checking if the email the user enters is registered in the database
            int total = GetHashtableTotal();
            int hashlocation = total % 1000;

            // if (Logins.ContainsKey(hashlocation) == true)
            // {
            //     try
            //     {
            //         smtp.Send(message);
            //         MessageBox.Show("Code sent successfully.");
            //     }
            //     catch (Exception ex)
            //     {
            //         MessageBox.Show(ex.Message);
            //     }
            // }
            // else
            // {
            //     MessageBox.Show("Please enter an already registered email.");
            // }

            SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-G9NKDOKB\SQLEXPRESS; Initial Catalog=NuacousticDB; Integrated Security=True;");
            String query = "select count(Email) as Email  from tblUser where Email = '" + txtEmail.Text + "'";
            if (sqlCon.State == ConnectionState.Closed)
                sqlCon.Open();           
            SqlCommand cmd = new SqlCommand(query, sqlCon);
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string iemail= dr["Email"].ToString();
                if (iemail != "0")
                {
                    try
                    {
                        smtp.Send(message);
                        MessageBox.Show("Code sent successfully.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }                                        
                }
                else
                {
                    MessageBox.Show("Please enter an already registered email.");
                }
            
            }
            
        }

        private void btnVerify_Click(object sender, RoutedEventArgs e)
        {
            if(randomCode == (txtVerificationCode.Text).ToString())
            {
                to = txtEmail.Text;
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
            else
            {
                MessageBox.Show("Wrong code");
            }            
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.MainFrame.NavigationService.GoBack();
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
    }
}
