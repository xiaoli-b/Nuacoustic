using System;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using System.Data.SqlClient;

namespace Nuacoustic
{
    /// <summary>
    /// Interaction logic for CreateNewProject.xaml
    /// </summary>
    public partial class CreateNewProject : Page
    {
        public CreateNewProject()
        {
            InitializeComponent();
            txtEmail.Text = (Application.Current.MainWindow as MainWindow).email;
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

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-G9NKDOKB\SQLEXPRESS; Initial Catalog=NuacousticDB; Integrated Security=True;");

            if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
            String query = "SELECT COUNT(1) FROM tblUser WHERE Email=@Email";
            SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Parameters.AddWithValue("@Email", txtEmail.Text);
            int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
            if (count != 1)
            {
                MessageBox.Show("Email is not found. Please try again.");
            }         
            
            // Inserting details of the project into the database.
            using (sqlCon)
            {

                string sql = "insert into dbo.tblProject ([ProjectName], [LastEditDate], [SystemRes], [ReqAirVolRate], " +
                    "[RoomLength], [RoomWidth], [RoomHeight], [FanCode], [TypeOfRoom], [NumberOfPeople], " +
                    "[SupplyAttenuatorType], [ExtractAttenuatorType], [AcousticBarrierType], [RoomTerminalSideLength], [Units]," +
                    " [XCoordListener], [YCoordListener], [XCoordFan], [YCoordFan], [Email], [XCoordSupply], [YCoordSupply]," +
                    "[XCoordExtract], [YCoordExtract]) values(@ProjectName,@LastEditDate,@SystemRes,@ReqAirVolRate," +
                    "@RoomLength,@RoomWidth,@RoomHeight,@FanCode,@TypeOfRoom,@NumberOfPeople,@SupplyAttenuatorType," +
                    "@ExtractAttenuatorType,@AcousticBarrierType,@RoomTerminalSideLength,@Units,@XCoordListener,@YCoordListener," +
                    "@XCoordFan,@YCoordFan,@Email,@XCoordSupply,@YCoordSupply,@XCoordExtract,@YCoordExtract)";

                // Create the connection
                try
                {                    
                    // Open the connection to the database.
                    // This is the first critical step in the process.
                    // If we cannot reach the db then we have connectivity problems


                    // Prepare the command to be executed on the db
                    using (SqlCommand cmd = new SqlCommand(sql, sqlCon))
                    {
                        // Create and set the parameters values 
                        cmd.Parameters.Add("@ProjectName", SqlDbType.NVarChar).Value = txtProjectName.Text.Trim();
                        cmd.Parameters.Add("@LastEditDate", SqlDbType.NVarChar).Value = DateTime.Now.ToString("yyyy-dd-mm");
                        cmd.Parameters.Add("@SystemRes", SqlDbType.NVarChar).Value = txtSysRes.Text.Trim();
                        cmd.Parameters.Add("@ReqAirVolRate", SqlDbType.NVarChar).Value = txtAirVolFlowRate.Text.Trim();
                        cmd.Parameters.Add("@RoomLength", SqlDbType.NVarChar).Value = txtRoomLength.Text.Trim();
                        cmd.Parameters.Add("@RoomWidth", SqlDbType.NVarChar).Value = txtRoomWidth.Text.Trim();
                        cmd.Parameters.Add("@RoomHeight", SqlDbType.NVarChar).Value = txtRoomHeight.Text.Trim();
                        cmd.Parameters.Add("@FanCode", SqlDbType.NVarChar).Value = txtFanCode.Text.Trim();
                        cmd.Parameters.Add("@TypeOfRoom", SqlDbType.NVarChar).Value = txtTypeOfRoom.Text.Trim();
                        cmd.Parameters.Add("@NumberOfPeople", SqlDbType.NVarChar).Value = txtNumberOfPeople.Text.Trim();
                        cmd.Parameters.Add("@SupplyAttenuatorType", SqlDbType.NVarChar).Value = txtAttenuatorSupply.Text.Trim();
                        cmd.Parameters.Add("@ExtractAttenuatorType", SqlDbType.NVarChar).Value = txtAttenuatorExtract.Text.Trim();
                        cmd.Parameters.Add("@AcousticBarrierType", SqlDbType.NVarChar).Value = txtBarrierType.Text.Trim();
                        cmd.Parameters.Add("@RoomTerminalSideLength", SqlDbType.NVarChar).Value = txtRoomTerminalSideLength.Text.Trim();
                        cmd.Parameters.Add("@Units", SqlDbType.NVarChar).Value = txtUnits.Text.Trim();
                        cmd.Parameters.Add("@XCoordListener", SqlDbType.NVarChar).Value = "0";
                        cmd.Parameters.Add("@YCoordListener", SqlDbType.NVarChar).Value = "0";
                        cmd.Parameters.Add("@XCoordFan", SqlDbType.NVarChar).Value = "0";
                        cmd.Parameters.Add("@YCoordFan", SqlDbType.NVarChar).Value = "0";
                        cmd.Parameters.Add("@XCoordSupply", SqlDbType.NVarChar).Value = "0";
                        cmd.Parameters.Add("@YCoordSupply", SqlDbType.NVarChar).Value = "0";
                        cmd.Parameters.Add("@XCoordExtract", SqlDbType.NVarChar).Value = "0";
                        cmd.Parameters.Add("@YCoordExtract", SqlDbType.NVarChar).Value = "0";
                        cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = txtEmail.Text.Trim();

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    // We should log the error somewhere, 
                    // for this example let's just show a message
                    MessageBox.Show("ERROR:" + ex.Message);
                }
            }
            MessageBox.Show("The information has been saved.");
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
    }
}
