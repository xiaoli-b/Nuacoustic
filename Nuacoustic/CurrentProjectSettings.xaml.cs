using System;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using System.Data.SqlClient;

namespace Nuacoustic
{
    /// <summary>
    /// Interaction logic for CurrentProjectSettings.xaml
    /// </summary>
    public partial class CurrentProjectSettings : Page
    {
        public CurrentProjectSettings()
        {
            InitializeComponent();
            txtEmail.Text = (Application.Current.MainWindow as MainWindow).email;
            txtProjectName.Text = (Application.Current.MainWindow as MainWindow).projectname;
            SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-G9NKDOKB\SQLEXPRESS; Initial Catalog=NuacousticDB; Integrated Security=True;");
            if (sqlCon.State == ConnectionState.Closed)
                sqlCon.Open();

            String queryLastDate = "SELECT LastEditDate FROM tblProject WHERE tblProject.ProjectName=@ProjectName AND tblProject.Email=@Email";
            SqlCommand sqlCmdLastDate = new SqlCommand(queryLastDate, sqlCon);
            sqlCmdLastDate.CommandType = CommandType.Text;
            sqlCmdLastDate.Parameters.AddWithValue("@ProjectName", txtProjectName.Text);
            sqlCmdLastDate.Parameters.AddWithValue("@Email", txtEmail.Text);

            txtLastEdit.Text = Convert.ToString(sqlCmdLastDate.ExecuteScalar());

            String querySysRes = "SELECT SystemRes FROM tblProject WHERE tblProject.ProjectName=@ProjectName AND tblProject.Email=@Email";
            SqlCommand sqlCmdSysRes = new SqlCommand(querySysRes, sqlCon);
            sqlCmdSysRes.CommandType = CommandType.Text;
            sqlCmdSysRes.Parameters.AddWithValue("@ProjectName", txtProjectName.Text);
            sqlCmdSysRes.Parameters.AddWithValue("@Email", txtEmail.Text);

            txtSysRes.Text = Convert.ToString(sqlCmdSysRes.ExecuteScalar());

            String queryReqAirVolRate = "SELECT ReqAirVolRate FROM tblProject WHERE tblProject.ProjectName=@ProjectName AND tblProject.Email=@Email";
            SqlCommand sqlCmdReqAirVolRate = new SqlCommand(queryReqAirVolRate, sqlCon);
            sqlCmdReqAirVolRate.CommandType = CommandType.Text;
            sqlCmdReqAirVolRate.Parameters.AddWithValue("@ProjectName", txtProjectName.Text);
            sqlCmdReqAirVolRate.Parameters.AddWithValue("@Email", txtEmail.Text);

            txtAirVolFlowRate.Text = Convert.ToString(sqlCmdReqAirVolRate.ExecuteScalar());

            String queryRoomLength = "SELECT RoomLength FROM tblProject WHERE tblProject.ProjectName=@ProjectName AND tblProject.Email=@Email";
            SqlCommand sqlCmdRoomLength = new SqlCommand(queryRoomLength, sqlCon);
            sqlCmdRoomLength.CommandType = CommandType.Text;
            sqlCmdRoomLength.Parameters.AddWithValue("@ProjectName", txtProjectName.Text);
            sqlCmdRoomLength.Parameters.AddWithValue("@Email", txtEmail.Text);

            txtRoomLength.Text = Convert.ToString(sqlCmdRoomLength.ExecuteScalar());

            String queryRoomWidth = "SELECT RoomWidth FROM tblProject WHERE tblProject.ProjectName=@ProjectName AND tblProject.Email=@Email";
            SqlCommand sqlCmdRoomWidth = new SqlCommand(queryRoomWidth, sqlCon);
            sqlCmdRoomWidth.CommandType = CommandType.Text;
            sqlCmdRoomWidth.Parameters.AddWithValue("@ProjectName", txtProjectName.Text);
            sqlCmdRoomWidth.Parameters.AddWithValue("@Email", txtEmail.Text);

            txtRoomWidth.Text = Convert.ToString(sqlCmdRoomWidth.ExecuteScalar());

            String queryRoomVolume = "SELECT RoomHeight FROM tblProject WHERE tblProject.ProjectName=@ProjectName AND tblProject.Email=@Email";
            SqlCommand sqlCmdRoomVolume = new SqlCommand(queryRoomVolume, sqlCon);
            sqlCmdRoomVolume.CommandType = CommandType.Text;
            sqlCmdRoomVolume.Parameters.AddWithValue("@ProjectName", txtProjectName.Text);
            sqlCmdRoomVolume.Parameters.AddWithValue("@Email", txtEmail.Text);

            txtRoomHeight.Text = Convert.ToString(sqlCmdRoomVolume.ExecuteScalar());

            String queryFanCode = "SELECT FanCode FROM tblProject WHERE tblProject.ProjectName=@ProjectName AND tblProject.Email=@Email";
            SqlCommand sqlCmdFanCode = new SqlCommand(queryFanCode, sqlCon);
            sqlCmdFanCode.CommandType = CommandType.Text;
            sqlCmdFanCode.Parameters.AddWithValue("@ProjectName", txtProjectName.Text);
            sqlCmdFanCode.Parameters.AddWithValue("@Email", txtEmail.Text);

            txtFanCode.Text = Convert.ToString(sqlCmdFanCode.ExecuteScalar());

            String queryTypeOfRoom = "SELECT TypeOfRoom FROM tblProject WHERE tblProject.ProjectName=@ProjectName AND tblProject.Email=@Email";
            SqlCommand sqlCmdTypeOfRoom = new SqlCommand(queryTypeOfRoom, sqlCon);
            sqlCmdTypeOfRoom.CommandType = CommandType.Text;
            sqlCmdTypeOfRoom.Parameters.AddWithValue("@ProjectName", txtProjectName.Text);
            sqlCmdTypeOfRoom.Parameters.AddWithValue("@Email", txtEmail.Text);

            txtTypeOfRoom.Text = Convert.ToString(sqlCmdTypeOfRoom.ExecuteScalar());

            String queryPeople = "SELECT NumberOfPeople FROM tblProject WHERE tblProject.ProjectName=@ProjectName AND tblProject.Email=@Email";
            SqlCommand sqlCmdPeople = new SqlCommand(queryPeople, sqlCon);
            sqlCmdPeople.CommandType = CommandType.Text;
            sqlCmdPeople.Parameters.AddWithValue("@ProjectName", txtProjectName.Text);
            sqlCmdPeople.Parameters.AddWithValue("@Email", txtEmail.Text);

            txtNumberOfPeople.Text = Convert.ToString(sqlCmdPeople.ExecuteScalar());

            String query1 = "SELECT SupplyAttenuatorType FROM tblProject WHERE tblProject.ProjectName=@ProjectName AND tblProject.Email=@Email";
            SqlCommand sqlCmd1 = new SqlCommand(query1, sqlCon);
            sqlCmd1.CommandType = CommandType.Text;
            sqlCmd1.Parameters.AddWithValue("@ProjectName", txtProjectName.Text);
            sqlCmd1.Parameters.AddWithValue("@Email", txtEmail.Text);

            txtAttenuatorSupply.Text = Convert.ToString(sqlCmd1.ExecuteScalar());

            String query2 = "SELECT ExtractAttenuatorType FROM tblProject WHERE tblProject.ProjectName=@ProjectName AND tblProject.Email=@Email";
            SqlCommand sqlCmd2 = new SqlCommand(query2, sqlCon);
            sqlCmd2.CommandType = CommandType.Text;
            sqlCmd2.Parameters.AddWithValue("@ProjectName", txtProjectName.Text);
            sqlCmd2.Parameters.AddWithValue("@Email", txtEmail.Text);

            txtAttenuatorExtract.Text = Convert.ToString(sqlCmd2.ExecuteScalar());

            String query3 = "SELECT AcousticBarrierType FROM tblProject WHERE tblProject.ProjectName=@ProjectName AND tblProject.Email=@Email";
            SqlCommand sqlCmd3 = new SqlCommand(query3, sqlCon);
            sqlCmd3.CommandType = CommandType.Text;
            sqlCmd3.Parameters.AddWithValue("@ProjectName", txtProjectName.Text);
            sqlCmd3.Parameters.AddWithValue("@Email", txtEmail.Text);

            txtBarrierType.Text = Convert.ToString(sqlCmd3.ExecuteScalar());

            String query4 = "SELECT RoomTerminalSideLength FROM tblProject WHERE tblProject.ProjectName=@ProjectName AND tblProject.Email=@Email";
            SqlCommand sqlCmd4 = new SqlCommand(query4, sqlCon);
            sqlCmd4.CommandType = CommandType.Text;
            sqlCmd4.Parameters.AddWithValue("@ProjectName", txtProjectName.Text);
            sqlCmd4.Parameters.AddWithValue("@Email", txtEmail.Text);

            txtRoomTerminalSideLength.Text = Convert.ToString(sqlCmd4.ExecuteScalar());

            String query5 = "SELECT Units FROM tblProject WHERE tblProject.ProjectName=@ProjectName AND tblProject.Email=@Email";
            SqlCommand sqlCmd5 = new SqlCommand(query5, sqlCon);
            sqlCmd5.CommandType = CommandType.Text;
            sqlCmd5.Parameters.AddWithValue("@ProjectName", txtProjectName.Text);
            sqlCmd5.Parameters.AddWithValue("@Email", txtEmail.Text);

            txtUnits.Text = Convert.ToString(sqlCmd5.ExecuteScalar());
        }

        // Collecting values from this page to send forwards so users do not need to keep entering the same details.

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

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            DisplayEmail();
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

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            // Saving new values for a project in the database.
            if (txtProjectName.Text == "" || txtEmail.Text == "")
            {
                MessageBox.Show("Please enter a valid project name and email.");
            }
            
            SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-G9NKDOKB\SQLEXPRESS; Initial Catalog=NuacousticDB; Integrated Security=True;");
            if (sqlCon.State == ConnectionState.Closed)
                sqlCon.Open();

            string sql = "UPDATE tblProject SET SystemRes=@SystemRes, LastEditDate=@LastEditDate, ReqAirVolRate=@ReqAirVolRate," +
                "RoomLength=@RoomLength, RoomWidth=@RoomWidth, RoomHeight=@RoomHeight, TypeOfRoom=@TypeOfRoom, FanCode=@FanCode," +
                "NumberOfPeople=@NumberOfPeople, SupplyAttenuatorType=@SupplyAttenuatorType, ExtractAttenuatorType=@ExtractAttenuatorType," +
                "AcousticBarrierType=@AcousticBarrierType, RoomTerminalSideLength=@RoomTerminalSideLength, Units=@Units WHERE ProjectName = @ProjectName AND Email = @Email";

            using (SqlCommand cmd = new SqlCommand(sql, sqlCon))
            {
                // Create and set the parameters values 
                cmd.Parameters.Add("@ProjectName", SqlDbType.NVarChar).Value = txtProjectName.Text.Trim();
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = txtEmail.Text.Trim();
                cmd.Parameters.Add("@SystemRes", SqlDbType.NVarChar).Value = txtSysRes.Text.Trim();
                cmd.Parameters.Add("@LastEditDate", SqlDbType.NVarChar).Value = DateTime.Now;
                cmd.Parameters.Add("@ReqAirVolRate", SqlDbType.NVarChar).Value = txtAirVolFlowRate.Text.Trim();
                cmd.Parameters.Add("@RoomLength", SqlDbType.NVarChar).Value = txtRoomLength.Text.Trim();
                cmd.Parameters.Add("@RoomWidth", SqlDbType.NVarChar).Value = txtRoomWidth.Text.Trim();
                cmd.Parameters.Add("@RoomHeight", SqlDbType.NVarChar).Value = txtRoomHeight.Text.Trim();
                cmd.Parameters.Add("@TypeOfRoom", SqlDbType.NVarChar).Value = txtTypeOfRoom.Text.Trim();
                cmd.Parameters.Add("@FanCode", SqlDbType.NVarChar).Value = txtFanCode.Text.Trim();
                cmd.Parameters.Add("@NumberOfPeople", SqlDbType.NVarChar).Value = txtNumberOfPeople.Text.Trim();
                cmd.Parameters.Add("@SupplyAttenuatorType", SqlDbType.NVarChar).Value = txtAttenuatorSupply.Text.Trim();
                cmd.Parameters.Add("@ExtractAttenuatorType", SqlDbType.NVarChar).Value = txtAttenuatorExtract.Text.Trim();
                cmd.Parameters.Add("@AcousticBarrierType", SqlDbType.NVarChar).Value = txtBarrierType.Text.Trim();
                cmd.Parameters.Add("@RoomTerminalSideLength", SqlDbType.NVarChar).Value = txtRoomTerminalSideLength.Text.Trim();
                cmd.Parameters.Add("@Units", SqlDbType.NVarChar).Value = txtUnits.Text.Trim();


                cmd.ExecuteNonQuery();
            }
            MessageBox.Show("Your project information has been updated.");
        }

        private void btnProjectPage_Click(object sender, RoutedEventArgs e)
        {
            DisplayProjectName();
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow.CurrentPage != ApplicationPage.Project)
            {
                mainWindow.CurrentPage = ApplicationPage.Project;
            }
            else
            {
                mainWindow.MainFrame.NavigationService.GoForward();
            }
        }
    }
}
