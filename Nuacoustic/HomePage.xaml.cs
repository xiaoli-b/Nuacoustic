using System;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Collections;

namespace Nuacoustic
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public Hashtable Logins;        
        public HomePage()
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

        private void DisplayEmail()
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.email = txtEmail.Text;
        }

        private void DisplayProjectName()
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.projectname = txtProjectName.Text;
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            DisplayEmail();
            ShareHashtable();
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

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
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

        private void btnCreateNewProject_Click(object sender, RoutedEventArgs e)
        {
            DisplayEmail();
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow.CurrentPage != ApplicationPage.CreateNewProject)
            {
                mainWindow.CurrentPage = ApplicationPage.CreateNewProject;
            }
            else
            {
                mainWindow.MainFrame.NavigationService.GoForward();
            }
        }

        string IsAdmin()
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-G9NKDOKB\SQLEXPRESS; Initial Catalog=NuacousticDB; Integrated Security=True;");
            if (txtEmail.Text == "")
            {
                MessageBox.Show("Please enter your email.");
            }
            if (sqlCon.State == ConnectionState.Closed)
                sqlCon.Open();

            String queryadmin = "SELECT Admin FROM tblUser WHERE Email=@Email";
            SqlCommand sqlCmd4 = new SqlCommand(queryadmin, sqlCon);
            sqlCmd4.CommandType = CommandType.Text;
            sqlCmd4.Parameters.AddWithValue("@Email", txtEmail.Text);
            string admin = Convert.ToString(sqlCmd4.ExecuteScalar());
            return admin;
        }
        private void btnOpenProject_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-G9NKDOKB\SQLEXPRESS; Initial Catalog=NuacousticDB; Integrated Security=True;");
            string admin = IsAdmin();
            if (admin == "Yes")
            {
                try
                {
                    if (sqlCon.State == ConnectionState.Closed)
                        sqlCon.Open();
                    String query = "SELECT COUNT(1) FROM tblProject WHERE ProjectName=@ProjectName";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.AddWithValue("@ProjectName", txtProjectName.Text);
                    int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                    if (count >= 1)
                    {
                        DisplayProjectName();
                        MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
                        if (mainWindow.CurrentPage != ApplicationPage.CurrentProjectSettings)
                        {
                            mainWindow.CurrentPage = ApplicationPage.CurrentProjectSettings;
                        }
                        else
                        {
                            mainWindow.MainFrame.NavigationService.GoForward();
                        }                       
                    }
                    else
                    {
                        MessageBox.Show("This project doesn't exist.");
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
            else
            {
                try
                {
                    if (sqlCon.State == ConnectionState.Closed)
                        sqlCon.Open();
                    String query = "SELECT COUNT(1) FROM tblProject WHERE Email=@Email AND ProjectName=@ProjectName";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    sqlCmd.Parameters.AddWithValue("@ProjectName", txtProjectName.Text);
                    int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                    if (count >= 1)
                    {
                        DisplayEmail();
                        DisplayProjectName();
                        MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
                        if (mainWindow.CurrentPage != ApplicationPage.CurrentProjectSettings)
                        {
                            mainWindow.CurrentPage = ApplicationPage.CurrentProjectSettings;
                        }
                        else
                        {
                            mainWindow.MainFrame.NavigationService.GoForward();
                        }
                    }
                    else
                    {
                        MessageBox.Show("This account did not create this project.");
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
        }

        // Shows details of a given project.
        private void btnOpenDetails_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-G9NKDOKB\SQLEXPRESS; Initial Catalog=NuacousticDB; Integrated Security=True;");
            string admin = IsAdmin();
            if (admin == "Yes")
            {
                try
                {

                    String query = "SELECT COUNT(1) FROM tblProject WHERE ProjectName=@ProjectName";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.AddWithValue("@ProjectName", txtProjectName.Text.Trim());
                    int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                    if (count == 1)
                    {
                        ShowDetails();
                    }
                    else
                    {
                        MessageBox.Show("Please enter the name of an already created project. To create a new project, click 'Create New'.");
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
            else
            {
                try
                {

                    String query = "SELECT COUNT(1) FROM tblProject WHERE ProjectName=@ProjectName AND Email=@Email";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.AddWithValue("@ProjectName", txtProjectName.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                    int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                    if (count == 1)
                    {
                        ShowDetails();
                    }
                    else
                    {
                        MessageBox.Show("Please enter the name of a project that you created. To create a new project, click 'Create New'.");
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
        }

        // Collects data to be shown of project from the database.
        void ShowDetails()
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-G9NKDOKB\SQLEXPRESS; Initial Catalog=NuacousticDB; Integrated Security=True;");
            if (sqlCon.State == ConnectionState.Closed)
                sqlCon.Open();

            String queryLastDate = "SELECT LastEditDate FROM tblProject WHERE tblProject.ProjectName=@ProjectName";
            SqlCommand sqlCmdLastDate = new SqlCommand(queryLastDate, sqlCon);
            sqlCmdLastDate.CommandType = CommandType.Text;
            sqlCmdLastDate.Parameters.AddWithValue("@ProjectName", txtProjectName.Text);

            string LastDate = Convert.ToString(sqlCmdLastDate.ExecuteScalar());

            String querySysRes = "SELECT SystemRes FROM tblProject WHERE tblProject.ProjectName=@ProjectName";
            SqlCommand sqlCmdSysRes = new SqlCommand(querySysRes, sqlCon);
            sqlCmdSysRes.CommandType = CommandType.Text;
            sqlCmdSysRes.Parameters.AddWithValue("@ProjectName", txtProjectName.Text);

            string SysRes = Convert.ToString(sqlCmdSysRes.ExecuteScalar());

            String queryReqAirVolRate = "SELECT ReqAirVolRate FROM tblProject WHERE tblProject.ProjectName=@ProjectName";
            SqlCommand sqlCmdReqAirVolRate = new SqlCommand(queryReqAirVolRate, sqlCon);
            sqlCmdReqAirVolRate.CommandType = CommandType.Text;
            sqlCmdReqAirVolRate.Parameters.AddWithValue("@ProjectName", txtProjectName.Text);

            string ReqAirVolRate = Convert.ToString(sqlCmdReqAirVolRate.ExecuteScalar());

            String queryRoomLength = "SELECT RoomLength FROM tblProject WHERE tblProject.ProjectName=@ProjectName";
            SqlCommand sqlCmdRoomLength = new SqlCommand(queryRoomLength, sqlCon);
            sqlCmdRoomLength.CommandType = CommandType.Text;
            sqlCmdRoomLength.Parameters.AddWithValue("@ProjectName", txtProjectName.Text);

            string RoomLength = Convert.ToString(sqlCmdRoomLength.ExecuteScalar());

            String queryRoomWidth = "SELECT RoomWidth FROM tblProject WHERE tblProject.ProjectName=@ProjectName";
            SqlCommand sqlCmdRoomWidth = new SqlCommand(queryRoomWidth, sqlCon);
            sqlCmdRoomWidth.CommandType = CommandType.Text;
            sqlCmdRoomWidth.Parameters.AddWithValue("@ProjectName", txtProjectName.Text);

            string RoomWidth = Convert.ToString(sqlCmdRoomWidth.ExecuteScalar());

            String queryRoomHeight = "SELECT RoomHeight FROM tblProject WHERE tblProject.ProjectName=@ProjectName";
            SqlCommand sqlCmdRoomHeight = new SqlCommand(queryRoomHeight, sqlCon);
            sqlCmdRoomHeight.CommandType = CommandType.Text;
            sqlCmdRoomHeight.Parameters.AddWithValue("@ProjectName", txtProjectName.Text);

            string RoomHeight = Convert.ToString(sqlCmdRoomHeight.ExecuteScalar());

            String queryFanCode = "SELECT FanCode FROM tblProject WHERE tblProject.ProjectName=@ProjectName";
            SqlCommand sqlCmdFanCode = new SqlCommand(queryFanCode, sqlCon);
            sqlCmdFanCode.CommandType = CommandType.Text;
            sqlCmdFanCode.Parameters.AddWithValue("@ProjectName", txtProjectName.Text);

            string FanCode = Convert.ToString(sqlCmdFanCode.ExecuteScalar());

            String queryTypeOfRoom = "SELECT TypeOfRoom FROM tblProject WHERE tblProject.ProjectName=@ProjectName";
            SqlCommand sqlCmdTypeOfRoom = new SqlCommand(queryTypeOfRoom, sqlCon);
            sqlCmdTypeOfRoom.CommandType = CommandType.Text;
            sqlCmdTypeOfRoom.Parameters.AddWithValue("@ProjectName", txtProjectName.Text);

            string TypeOfRoom = Convert.ToString(sqlCmdTypeOfRoom.ExecuteScalar());

            String queryPeople = "SELECT NumberOfPeople FROM tblProject WHERE tblProject.ProjectName=@ProjectName";
            SqlCommand sqlCmdPeople = new SqlCommand(queryPeople, sqlCon);
            sqlCmdPeople.CommandType = CommandType.Text;
            sqlCmdPeople.Parameters.AddWithValue("@ProjectName", txtProjectName.Text);

            string People = Convert.ToString(sqlCmdPeople.ExecuteScalar());

            String query1 = "SELECT SupplyAttenuatorType FROM tblProject WHERE tblProject.ProjectName=@ProjectName";
            SqlCommand sqlCmd1 = new SqlCommand(query1, sqlCon);
            sqlCmd1.CommandType = CommandType.Text;
            sqlCmd1.Parameters.AddWithValue("@ProjectName", txtProjectName.Text);

            string SupplyType = Convert.ToString(sqlCmd1.ExecuteScalar());

            String query2 = "SELECT ExtractAttenuatorType FROM tblProject WHERE tblProject.ProjectName=@ProjectName";
            SqlCommand sqlCmd2 = new SqlCommand(query2, sqlCon);
            sqlCmd2.CommandType = CommandType.Text;
            sqlCmd2.Parameters.AddWithValue("@ProjectName", txtProjectName.Text);

            string ExtractType = Convert.ToString(sqlCmd2.ExecuteScalar());

            String query3 = "SELECT AcousticBarrierType FROM tblProject WHERE tblProject.ProjectName=@ProjectName";
            SqlCommand sqlCmd3 = new SqlCommand(query3, sqlCon);
            sqlCmd3.CommandType = CommandType.Text;
            sqlCmd3.Parameters.AddWithValue("@ProjectName", txtProjectName.Text);

            string BarrierType = Convert.ToString(sqlCmd3.ExecuteScalar());

            String query4 = "SELECT RoomTerminalSideLength FROM tblProject WHERE tblProject.ProjectName=@ProjectName";
            SqlCommand sqlCmd4 = new SqlCommand(query4, sqlCon);
            sqlCmd4.CommandType = CommandType.Text;
            sqlCmd4.Parameters.AddWithValue("@ProjectName", txtProjectName.Text);

            string RoomTerminalSideLength = Convert.ToString(sqlCmd4.ExecuteScalar());

            String query5 = "SELECT Units FROM tblProject WHERE tblProject.ProjectName=@ProjectName";
            SqlCommand sqlCmd5 = new SqlCommand(query5, sqlCon);
            sqlCmd5.CommandType = CommandType.Text;
            sqlCmd5.Parameters.AddWithValue("@ProjectName", txtProjectName.Text);

            string Units = Convert.ToString(sqlCmd5.ExecuteScalar());

            double roomVolume = Convert.ToDouble(RoomLength) * Convert.ToDouble(RoomWidth) * Convert.ToDouble(RoomHeight);
            string RoomVolume = Convert.ToString(roomVolume);

            // Writes a text file showing details and outputs content into a textbox on screen.
            try
            {
                String path = @"C:\Users\peybi\Downloads\" + txtProjectName.Text + "Details.txt";
                StreamWriter file = new StreamWriter(path);
                file.Write("DETAILS OF PROJECT\r\n");
                file.Write(" \r\n");
                file.Write("Project Name: " + txtProjectName.Text+ "\r\n");
                file.Write("Last edit date: " + LastDate+ "\r\n");
                file.Write("System Resistance: " + SysRes+ "\r\n");
                file.Write("Required Air Volume Flow Rate: " + ReqAirVolRate+ "\r\n");
                file.Write("Room Length: " + RoomLength+ "\r\n");
                file.Write("Room Width: " + RoomWidth+ "\r\n");
                file.Write("Room Height: " + RoomHeight + "\r\n");
                file.Write("Room Volume: " + RoomVolume+ "\r\n");
                file.Write("Fan code: " + FanCode+ "\r\n");
                file.Write("Type of room: " + TypeOfRoom+ "\r\n");
                file.Write("Number of people: " + People+ "\r\n");
                file.Write("Supply attenuator type: " + SupplyType+ "\r\n");
                file.Write("Extract attenuator type: " + ExtractType+ "\r\n");
                file.Write("Acoustic barrier type: " + BarrierType+ "\r\n");
                file.Write("Room terminal side length: " + RoomTerminalSideLength+ "\r\n");
                file.Write("Units: " + Units+ "\r\n");
                file.Close();

                using (StreamReader sr = new StreamReader(path))
                {
                    string filetext = sr.ReadToEnd();
                    txtDetails.Text = filetext;
                }
                
                file.Close();
                File.Delete(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Shows the user what projects they have created; if the user is an admin, they see everyone's project, if not then they just see their own.
        private void btnSeeProjects_Click(object sender, RoutedEventArgs e)
        {
            if(txtEmail.Text == "")
            {
                MessageBox.Show("Please enter your email.");
            }
            SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-G9NKDOKB\SQLEXPRESS; Initial Catalog=NuacousticDB; Integrated Security=True;");
            if (sqlCon.State == ConnectionState.Closed)
                sqlCon.Open();

            String queryadmin = "SELECT Admin FROM tblUser WHERE Email=@Email";
            SqlCommand sqlCmd4 = new SqlCommand(queryadmin, sqlCon);
            sqlCmd4.CommandType = CommandType.Text;
            sqlCmd4.Parameters.AddWithValue("@Email", txtEmail.Text);
            string admin = Convert.ToString(sqlCmd4.ExecuteScalar());
            if (admin == "Yes")
            {
                ArrayList filenames = new ArrayList();
                
                String querynames = "SELECT ProjectName FROM tblProject";
                SqlCommand sqlCmd5 = new SqlCommand(querynames, sqlCon);
                sqlCmd5.CommandType = CommandType.Text;
                SqlDataReader reader = sqlCmd5.ExecuteReader();
                while (reader.Read())
                {
                    filenames.Add(reader[0]);
                }
                int arraylength = filenames.Count;
                try
                {
                    String path = @"C:\Users\peybi\Downloads\" + txtProjectName.Text + "FileNames.txt";
                    StreamWriter file = new StreamWriter(path);                    
                    for(int count = 0;count <= arraylength - 1;count++)
                    {
                        file.Write(filenames[count] + "\r\n");
                    }
                    file.Close();

                    using (StreamReader sr = new StreamReader(path))
                    {
                        string filetext = sr.ReadToEnd();
                        txtProjectFileNames.Text = filetext;
                    }

                    file.Close();
                    File.Delete(path);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                String querynames = "SELECT ProjectName FROM tblProject WHERE Email=@Email";
                SqlCommand sqlCmd5 = new SqlCommand(querynames, sqlCon);
                sqlCmd5.CommandType = CommandType.Text;
                sqlCmd5.Parameters.AddWithValue("@Email", txtEmail.Text);
                string filenames = Convert.ToString(sqlCmd5.ExecuteScalar());
                try
                {
                    String path = @"C:\Users\peybi\Downloads\" + txtProjectName.Text + "FileNames.txt";
                    StreamWriter file = new StreamWriter(path);
                    file.Write(filenames + "\r\n");

                    file.Close();

                    using (StreamReader sr = new StreamReader(path))
                    {
                        string filetext = sr.ReadToEnd();
                        txtProjectFileNames.Text = filetext;
                    }

                    file.Close();
                    File.Delete(path);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        // Deletes project from database.
        private void btnDeleteProject_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-G9NKDOKB\SQLEXPRESS; Initial Catalog=NuacousticDB; Integrated Security=True;");

            if (sqlCon.State == ConnectionState.Closed)
                sqlCon.Open();
            try
            {
                string sql = "DELETE FROM tblProject WHERE Email=@Email AND ProjectName=@ProjectName";
                using (SqlCommand cmd = new SqlCommand(sql, sqlCon))
                {
                    // Create and set the parameters values 
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = txtEmail.Text.Trim();
                    cmd.Parameters.Add("@ProjectName", SqlDbType.NVarChar).Value = txtProjectName.Text.Trim();


                    cmd.ExecuteNonQuery();
                }                

                MessageBox.Show("Your information has been deleted.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(Convert.ToString(ex));
            }
        }
    }
}
