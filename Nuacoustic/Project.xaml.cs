using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Nuacoustic
{
    /// <summary>
    /// Interaction logic for Project.xaml
    /// </summary>
    public partial class Project : Page
    {
        // Initiating required variables.
        public string newFanCode = "0";
        public double FanXCoord;
        public double FanYCoord;
        public double ListenerXCoord;
        public double ListenerYCoord;
        public double SupplyXCoord;
        public double SupplyYCoord;
        public double ExtractXCoord;
        public double ExtractYCoord;

       
        public Project()
        {
            InitializeComponent();
            txtProjectName.Text = (Application.Current.MainWindow as MainWindow).projectname;
        }

        public string message;
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

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
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

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
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

        private Image draggedImage;
        private Point mousePosition;

        // Moves picture with mouse.
        private void CanvasMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            canvas.MaxHeight = 320;
            canvas.MaxWidth = 600;
            var image = e.Source as Image;
            canvas.ClipToBounds = true;
            if (image != null && canvas.CaptureMouse())
            {
                mousePosition = e.GetPosition(canvas);
                draggedImage = image;
                Panel.SetZIndex(draggedImage, 4); // in case of multiple images
            }
        }

        // Reads where picture is located pixel-wise, and puts this into the database.
        void CanvasMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-G9NKDOKB\SQLEXPRESS; Initial Catalog=NuacousticDB; Integrated Security=True;");
            if (sqlCon.State == ConnectionState.Closed)
                sqlCon.Open();

            if (draggedImage != null)
            {
                if(txtProjectName.Text == "")
                {
                    MessageBox.Show("Please enter a project name.");
                }
                canvas.ReleaseMouseCapture();
                Panel.SetZIndex(draggedImage, 0);

                if (draggedImage.Name == "FanSymbol")
                {
                    FanXCoord = Canvas.GetLeft(draggedImage);
                    FanYCoord = Canvas.GetTop(draggedImage);
                    var FanXCoordstr = Convert.ToString(FanXCoord);
                    var FanYCoordstr = Convert.ToString(FanYCoord);
                    using (sqlCon)
                    {
                        string sql = "UPDATE tblProject SET XCoordFan = @XCoordFan WHERE ProjectName = @ProjectName";

                        using (SqlCommand cmd = new SqlCommand(sql, sqlCon))
                        {
                            // Create and set the parameters values 
                            cmd.Parameters.Add("@ProjectName", SqlDbType.NVarChar).Value = txtProjectName.Text.Trim();
                            cmd.Parameters.Add("@XCoordFan", SqlDbType.NVarChar).Value = FanXCoordstr;

                            cmd.ExecuteNonQuery();
                        }
                        string sql1 = "UPDATE tblProject SET YCoordFan = @YCoordFan WHERE ProjectName = @ProjectName";
                        
                        using (SqlCommand cmd = new SqlCommand(sql1, sqlCon))
                        {
                            // Create and set the parameters values 
                            cmd.Parameters.Add("@ProjectName", SqlDbType.NVarChar).Value = txtProjectName.Text.Trim();
                            cmd.Parameters.Add("@YCoordFan", SqlDbType.NVarChar).Value = FanYCoordstr;

                            cmd.ExecuteNonQuery();
                        }

                    }


                }
                else if (draggedImage.Name == "ListenerSymbol")
                {
                    if (txtProjectName.Text == "")
                    {
                        MessageBox.Show("Please enter a project name.");
                    }
                    ListenerXCoord = Canvas.GetLeft(draggedImage);
                    ListenerYCoord = Canvas.GetTop(draggedImage);
                    var ListenerXCoordstr = Convert.ToString(ListenerXCoord);
                    var ListenerYCoordstr = Convert.ToString(ListenerYCoord);
                    
                    using (sqlCon)
                    {
                        string sql = "UPDATE tblProject SET XCoordListener = @XCoordListener WHERE ProjectName = @ProjectName";

                        using (SqlCommand cmd = new SqlCommand(sql, sqlCon))
                        {
                            // Create and set the parameters values 
                            cmd.Parameters.Add("@ProjectName", SqlDbType.NVarChar).Value = txtProjectName.Text.Trim();
                            cmd.Parameters.Add("@XCoordListener", SqlDbType.NVarChar).Value = ListenerXCoordstr;

                            cmd.ExecuteNonQuery();
                        }
                        string sql1 = "UPDATE tblProject SET YCoordListener = @YCoordListener WHERE ProjectName = @ProjectName";

                        using (SqlCommand cmd = new SqlCommand(sql1, sqlCon))
                        {
                            // Create and set the parameters values 
                            cmd.Parameters.Add("@ProjectName", SqlDbType.NVarChar).Value = txtProjectName.Text.Trim();
                            cmd.Parameters.Add("@YCoordListener", SqlDbType.NVarChar).Value = ListenerYCoordstr;

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                else if (draggedImage.Name == "SupplySymbol")
                {
                    if (txtProjectName.Text == "")
                    {
                        MessageBox.Show("Please enter a project name.");
                    }
                    SupplyXCoord = Canvas.GetLeft(draggedImage);
                    SupplyYCoord = Canvas.GetTop(draggedImage);
                    var SupplyXCoordstr = Convert.ToString(SupplyXCoord);
                    var SupplyYCoordstr = Convert.ToString(SupplyYCoord);

                    using (sqlCon)
                    {
                        string sql = "UPDATE tblProject SET XCoordSupply = @XCoordSupply WHERE ProjectName = @ProjectName";

                        using (SqlCommand cmd = new SqlCommand(sql, sqlCon))
                        {
                            // Create and set the parameters values 
                            cmd.Parameters.Add("@ProjectName", SqlDbType.NVarChar).Value = txtProjectName.Text.Trim();
                            cmd.Parameters.Add("@XCoordSupply", SqlDbType.NVarChar).Value = SupplyXCoordstr;

                            cmd.ExecuteNonQuery();
                        }
                        string sql1 = "UPDATE tblProject SET YCoordSupply = @YCoordSupply WHERE ProjectName = @ProjectName";

                        using (SqlCommand cmd = new SqlCommand(sql1, sqlCon))
                        {
                            // Create and set the parameters values 
                            cmd.Parameters.Add("@ProjectName", SqlDbType.NVarChar).Value = txtProjectName.Text.Trim();
                            cmd.Parameters.Add("@YCoordSupply", SqlDbType.NVarChar).Value = SupplyYCoordstr;

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                else if (draggedImage.Name == "ExtractSymbol")
                {
                    if (txtProjectName.Text == "")
                    {
                        MessageBox.Show("Please enter a project name.");
                    }
                    ExtractXCoord = Canvas.GetLeft(draggedImage);
                    ExtractYCoord = Canvas.GetTop(draggedImage);
                    var ExtractXCoordstr = Convert.ToString(ExtractXCoord);
                    var ExtractYCoordstr = Convert.ToString(ExtractYCoord);

                    using (sqlCon)
                    {
                        string sql = "UPDATE tblProject SET XCoordExtract = @XCoordExtract WHERE ProjectName = @ProjectName";

                        using (SqlCommand cmd = new SqlCommand(sql, sqlCon))
                        {
                            // Create and set the parameters values 
                            cmd.Parameters.Add("@ProjectName", SqlDbType.NVarChar).Value = txtProjectName.Text.Trim();
                            cmd.Parameters.Add("@XCoordExtract", SqlDbType.NVarChar).Value = ExtractXCoordstr;

                            cmd.ExecuteNonQuery();
                        }
                        string sql1 = "UPDATE tblProject SET YCoordExtract = @YCoordExtract WHERE ProjectName = @ProjectName";

                        using (SqlCommand cmd = new SqlCommand(sql1, sqlCon))
                        {
                            // Create and set the parameters values 
                            cmd.Parameters.Add("@ProjectName", SqlDbType.NVarChar).Value = txtProjectName.Text.Trim();
                            cmd.Parameters.Add("@YCoordExtract", SqlDbType.NVarChar).Value = ExtractYCoordstr;

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                draggedImage = null;                
            }                      
        }

        // Calculates the graduations of each square on the grid.
        string Graduations()
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-G9NKDOKB\SQLEXPRESS; Initial Catalog=NuacousticDB; Integrated Security=True;");
            if (sqlCon.State == ConnectionState.Closed)
                sqlCon.Open();

            String queryRoomLength = "SELECT RoomLength FROM tblProject WHERE ProjectName=@ProjectName";
            SqlCommand sqlCmd8 = new SqlCommand(queryRoomLength, sqlCon);
            sqlCmd8.CommandType = CommandType.Text;
            sqlCmd8.Parameters.AddWithValue("@ProjectName", txtProjectName.Text);
            string RoomLengthstr = Convert.ToString(sqlCmd8.ExecuteScalar());
            decimal RoomLength = Convert.ToDecimal(RoomLengthstr);

            String queryRoomWidth = "SELECT RoomWidth FROM tblProject WHERE ProjectName=@ProjectName";
            SqlCommand sqlCmd9 = new SqlCommand(queryRoomWidth, sqlCon);
            sqlCmd9.CommandType = CommandType.Text;
            sqlCmd9.Parameters.AddWithValue("@ProjectName", txtProjectName.Text);
            string RoomWidthstr = Convert.ToString(sqlCmd9.ExecuteScalar());
            decimal RoomWidth = Convert.ToDecimal(RoomWidthstr);

            decimal horizontalGraduationfull = RoomLength / 15;
            decimal horizontalGraduation = Decimal.Round(horizontalGraduationfull, 2);
            decimal verticalGraduationfull = RoomWidth / 10;
            decimal verticalGraduation = Decimal.Round(verticalGraduationfull, 2);
            string message = "Horizontal: starting from 0 on the left, each graduation is " + horizontalGraduation + "m. Vertical: starting from 0 at the bottom, each graduation is " + verticalGraduation + "m.";
            if (txtUnits.Text == "Feet")
            {
                decimal convertToFeet = Convert.ToDecimal(3.28);
                horizontalGraduation = Decimal.Round((horizontalGraduationfull * convertToFeet), 2);
                verticalGraduation = Decimal.Round((verticalGraduationfull * convertToFeet), 2);
                message = "Horizontal: starting from 0 on the left, each graduation is " + horizontalGraduation + "ft. Vertical: starting from 0 at the bottom, each graduation is " + verticalGraduation + "ft.";

            }
            return message;
        }
        private void CanvasMouseMove(object sender, MouseEventArgs e)
        {
            if (draggedImage != null)
            {
                var position = e.GetPosition(canvas);
                var offset = position - mousePosition;
                mousePosition = position;
                Canvas.SetLeft(draggedImage, Canvas.GetLeft(draggedImage) + offset.X);
                Canvas.SetTop(draggedImage, Canvas.GetTop(draggedImage) + offset.Y);
            }
        }

        private void btnSummary_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-G9NKDOKB\SQLEXPRESS; Initial Catalog=NuacousticDB; Integrated Security=True;");
            if (sqlCon.State == ConnectionState.Closed)
                sqlCon.Open();

            // get units
            String queryUnits = "SELECT Units FROM tblProject WHERE ProjectName=@ProjectName";
            SqlCommand sqlCmdUnits = new SqlCommand(queryUnits, sqlCon);
            sqlCmdUnits.CommandType = CommandType.Text;
            sqlCmdUnits.Parameters.AddWithValue("@ProjectName", txtProjectName.Text);
            string Units = Convert.ToString(sqlCmdUnits.ExecuteScalar());

            // calculating the total air flow of the room
            String query = "SELECT NumberOfPeople FROM tblProject WHERE ProjectName=@ProjectName";
            SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Parameters.AddWithValue("@ProjectName", txtProjectName.Text);
            int NumberOfPeople = Convert.ToInt32(sqlCmd.ExecuteScalar());
            
            // get supply flow rate
            double GetSupplyFlowRate()
            {
                String query1 = "SELECT SupplyFlowRate FROM tblTypeOfRoom INNER JOIN tblProject ON tblProject.TypeOfRoom = tblTypeOfRoom.TypeName WHERE tblProject.ProjectName=@ProjectName";

                SqlCommand sqlCmd1 = new SqlCommand(query1, sqlCon);
                sqlCmd1.CommandType = CommandType.Text;
                sqlCmd1.Parameters.AddWithValue("@ProjectName", txtProjectName.Text);
                string SupplyFlowRatestr = Convert.ToString(sqlCmd1.ExecuteScalar());
                double SupplyFlowRate = Convert.ToDouble(SupplyFlowRatestr);

                return SupplyFlowRate;
            }

            // calculating total air flow
            double totalAirFlow = NumberOfPeople * GetSupplyFlowRate();

            // calculating constant a in formula y=ax^2
            double x = totalAirFlow;

            String query2 = "SELECT SystemRes FROM tblProject WHERE ProjectName=@ProjectName";
            SqlCommand sqlCmd2 = new SqlCommand(query2, sqlCon);
            sqlCmd2.CommandType = CommandType.Text;
            sqlCmd2.Parameters.AddWithValue("@ProjectName", txtProjectName.Text);
            double SystemRes = Convert.ToInt32(sqlCmd2.ExecuteScalar());
            double y = SystemRes;

            double a = y / Math.Pow(x, 2);

            // find the types of attenuators and acoustic barrier that were inputted by the user from database

            string FindSupplyAttenuationType()
            {
                String query5 = "SELECT SupplyAttenuatorType FROM tblProject WHERE ProjectName=@ProjectName";
                SqlCommand sqlCmd5 = new SqlCommand(query5, sqlCon);
                sqlCmd5.CommandType = CommandType.Text;
                sqlCmd5.Parameters.AddWithValue("@ProjectName", txtProjectName.Text);
                string SupplyAttenuation = Convert.ToString(sqlCmd5.ExecuteScalar());
                return SupplyAttenuation;
            }

            string FindExtractAttenuationType()
            {
                String query6 = "SELECT ExtractAttenuatorType FROM tblProject WHERE ProjectName=@ProjectName";
                SqlCommand sqlCmdextract = new SqlCommand(query6, sqlCon);
                sqlCmdextract.CommandType = CommandType.Text;
                sqlCmdextract.Parameters.AddWithValue("@ProjectName", txtProjectName.Text);
                string ExtractAttenuation = Convert.ToString(sqlCmdextract.ExecuteScalar());
                return ExtractAttenuation;
            }

            string FindBreakoutBarrierType()
            {
                String query6 = "SELECT AcousticBarrierType FROM tblProject WHERE ProjectName=@ProjectName";
                SqlCommand sqlCmdbreakout = new SqlCommand(query6, sqlCon);
                sqlCmdbreakout.CommandType = CommandType.Text;
                sqlCmdbreakout.Parameters.AddWithValue("@ProjectName", txtProjectName.Text);
                string BarrierType = Convert.ToString(sqlCmdbreakout.ExecuteScalar());
                return BarrierType;
            }

            // each 'SuitabilityXBC'x'' subroutine calculates the corrected supply, extract and breakout values for each of 8 octave bands.
            // These are then sent to another subroutine called 'calculatingTheSuitability'.
            // The different 'x' values are the fan codes.
            #region findingSuitability
            string SuitabilityXBC15()
            {
                // y = 9677.894325825380000x3 - 6494.877163984340000x2 - 3796.919390602680000x + 900.172738777446000

                double A = 9677.894325825380000;
                double B = -6494.877163984340000 - a;
                double C = -3796.919390602680000;
                double D = 900.172738777446000;
                double FanFlowRate = SolvingACubic(A, B, C, D);
                double DeductionIndB = FindDeductionIndB(FanFlowRate);
                // for 63Hz octave band
                double correctedSupply63 = 75 - DeductionIndB;
                double correctedExtract63 = 69 - DeductionIndB;
                double correctedBreakout63 = 61 - DeductionIndB;
                // for 125Hz octave band
                double correctedSupply125 = 72 - DeductionIndB;
                double correctedExtract125 = 59 - DeductionIndB;
                double correctedBreakout125 = 57 - DeductionIndB;
                // for 250Hz octave band
                double correctedSupply250 = 65 - DeductionIndB;
                double correctedExtract250 = 55 - DeductionIndB;
                double correctedBreakout250 = 42 - DeductionIndB;
                // for 500Hz octave band
                double correctedSupply500 = 66 - DeductionIndB;
                double correctedExtract500 = 55 - DeductionIndB;
                double correctedBreakout500 = 43 - DeductionIndB;
                // for 1000Hz octave band
                double correctedSupply1000 = 68 - DeductionIndB;
                double correctedExtract1000 = 61 - DeductionIndB;
                double correctedBreakout1000 = 41 - DeductionIndB;
                // for 2000Hz octave band
                double correctedSupply2000 = 64 - DeductionIndB;
                double correctedExtract2000 = 55 - DeductionIndB;
                double correctedBreakout2000 = 37 - DeductionIndB;
                // for 4000Hz octave band
                double correctedSupply4000 = 59 - DeductionIndB;
                double correctedExtract4000 = 45 - DeductionIndB;
                double correctedBreakout4000 = 34 - DeductionIndB;
                // for 8000Hz octave band
                double correctedSupply8000 = 57 - DeductionIndB;
                double correctedExtract8000 = 41 - DeductionIndB;
                double correctedBreakout8000 = 23 - DeductionIndB;

                string suitable = calculatingTheSuitability(correctedSupply63, correctedExtract63, correctedBreakout63, correctedSupply125, correctedExtract125, correctedBreakout125,
                correctedSupply250, correctedExtract250, correctedBreakout250, correctedSupply500, correctedExtract500, correctedBreakout500,
                correctedSupply1000, correctedExtract1000, correctedBreakout1000, correctedSupply2000, correctedExtract2000, correctedBreakout2000,
                correctedSupply4000, correctedExtract4000, correctedBreakout4000, correctedSupply8000, correctedExtract8000, correctedBreakout8000);

                return suitable;
            }
            string SuitabilityXBC25()
            {
                // y = -22,748.652434210700000x3 + 7,641.356938960030000x2 - 1,808.489952946460000x + 965.580779882142000

                double A = -22748.652434210700000;
                double B = 7641.356938960030000 - a;
                double C = -1808.489952946460000;
                double D = 965.580779882142000;
                double FanFlowRate = SolvingACubic(A, B, C, D);
                double DeductionIndB = FindDeductionIndB(FanFlowRate);
                // for 63Hz octave band
                double correctedSupply63 = 85 - DeductionIndB;
                double correctedExtract63 = 79 - DeductionIndB;
                double correctedBreakout63 = 72 - DeductionIndB;
                // for 125Hz octave band
                double correctedSupply125 = 86 - DeductionIndB;
                double correctedExtract125 = 73 - DeductionIndB;
                double correctedBreakout125 = 71 - DeductionIndB;
                // for 250Hz octave band
                double correctedSupply250 = 81 - DeductionIndB;
                double correctedExtract250 = 71 - DeductionIndB;
                double correctedBreakout250 = 58 - DeductionIndB;
                // for 500Hz octave band
                double correctedSupply500 = 85 - DeductionIndB;
                double correctedExtract500 = 74 - DeductionIndB;
                double correctedBreakout500 = 61 - DeductionIndB;
                // for 1000Hz octave band
                double correctedSupply1000 = 75 - DeductionIndB;
                double correctedExtract1000 = 68 - DeductionIndB;
                double correctedBreakout1000 = 48 - DeductionIndB;
                // for 2000Hz octave band
                double correctedSupply2000 = 75 - DeductionIndB;
                double correctedExtract2000 = 65 - DeductionIndB;
                double correctedBreakout2000 = 47 - DeductionIndB;
                // for 4000Hz octave band
                double correctedSupply4000 = 71 - DeductionIndB;
                double correctedExtract4000 = 57 - DeductionIndB;
                double correctedBreakout4000 = 47 - DeductionIndB;
                // for 8000Hz octave band
                double correctedSupply8000 = 73 - DeductionIndB;
                double correctedExtract8000 = 57 - DeductionIndB;
                double correctedBreakout8000 = 39 - DeductionIndB;

                string suitable = calculatingTheSuitability(correctedSupply63, correctedExtract63, correctedBreakout63, correctedSupply125, correctedExtract125, correctedBreakout125,
                correctedSupply250, correctedExtract250, correctedBreakout250, correctedSupply500, correctedExtract500, correctedBreakout500,
                correctedSupply1000, correctedExtract1000, correctedBreakout1000, correctedSupply2000, correctedExtract2000, correctedBreakout2000,
                correctedSupply4000, correctedExtract4000, correctedBreakout4000, correctedSupply8000, correctedExtract8000, correctedBreakout8000);
                return suitable;
            }
            string SuitabilityXBC45()
            {
                // y = -225.400901870103000x3 - 1,336.437266669990000x2 - 770.006574013649000x + 811.711703606484000
                double A = -225.400901870103000;
                double B = -1336.437266669990000 - a;
                double C = -770.006574013649000;
                double D = 811.711703606484000;
                double FanFlowRate = SolvingACubic(A, B, C, D);
                double DeductionIndB = FindDeductionIndB(FanFlowRate);
                // for 63Hz octave band
                double correctedSupply63 = 87 - DeductionIndB;
                double correctedExtract63 = 84 - DeductionIndB;
                double correctedBreakout63 = 74 - DeductionIndB;
                // for 125Hz octave band
                double correctedSupply125 = 80 - DeductionIndB;
                double correctedExtract125 = 75 - DeductionIndB;
                double correctedBreakout125 = 65 - DeductionIndB;
                // for 250Hz octave band
                double correctedSupply250 = 85 - DeductionIndB;
                double correctedExtract250 = 76 - DeductionIndB;
                double correctedBreakout250 = 62 - DeductionIndB;
                // for 500Hz octave band
                double correctedSupply500 = 71 - DeductionIndB;
                double correctedExtract500 = 63 - DeductionIndB;
                double correctedBreakout500 = 47 - DeductionIndB;
                // for 1000Hz octave band
                double correctedSupply1000 = 72 - DeductionIndB;
                double correctedExtract1000 = 64 - DeductionIndB;
                double correctedBreakout1000 = 45 - DeductionIndB;
                // for 2000Hz octave band
                double correctedSupply2000 = 71 - DeductionIndB;
                double correctedExtract2000 = 63 - DeductionIndB;
                double correctedBreakout2000 = 44 - DeductionIndB;
                // for 4000Hz octave band
                double correctedSupply4000 = 66 - DeductionIndB;
                double correctedExtract4000 = 53 - DeductionIndB;
                double correctedBreakout4000 = 40 - DeductionIndB;
                // for 8000Hz octave band
                double correctedSupply8000 = 62 - DeductionIndB;
                double correctedExtract8000 = 44 - DeductionIndB;
                double correctedBreakout8000 = 29 - DeductionIndB;

                string suitable = calculatingTheSuitability(correctedSupply63, correctedExtract63, correctedBreakout63, correctedSupply125, correctedExtract125, correctedBreakout125,
                correctedSupply250, correctedExtract250, correctedBreakout250, correctedSupply500, correctedExtract500, correctedBreakout500,
                correctedSupply1000, correctedExtract1000, correctedBreakout1000, correctedSupply2000, correctedExtract2000, correctedBreakout2000,
                correctedSupply4000, correctedExtract4000, correctedBreakout4000, correctedSupply8000, correctedExtract8000, correctedBreakout8000);
                return suitable;
            }
            string SuitabilityXBC55()
            {
                // y = -1,254.877471604040000x3 + 9.914530366717370x2 - 865.732222992418000x + 811.032050316677000
                double A = -1254.877471604040000;
                double B = 9.914530366717370 - a;
                double C = -865.732222992418000;
                double D = 811.032050316677000;
                double FanFlowRate = SolvingACubic(A, B, C, D);
                double DeductionIndB = FindDeductionIndB(FanFlowRate);
                // for 63Hz octave band
                double correctedSupply63 = 85 - DeductionIndB;
                double correctedExtract63 = 82 - DeductionIndB;
                double correctedBreakout63 = 72 - DeductionIndB;
                // for 125Hz octave band
                double correctedSupply125 = 80 - DeductionIndB;
                double correctedExtract125 = 75 - DeductionIndB;
                double correctedBreakout125 = 65 - DeductionIndB;
                // for 250Hz octave band
                double correctedSupply250 = 84 - DeductionIndB;
                double correctedExtract250 = 75 - DeductionIndB;
                double correctedBreakout250 = 61 - DeductionIndB;
                // for 500Hz octave band
                double correctedSupply500 = 71 - DeductionIndB;
                double correctedExtract500 = 63 - DeductionIndB;
                double correctedBreakout500 = 47 - DeductionIndB;
                // for 1000Hz octave band
                double correctedSupply1000 = 72 - DeductionIndB;
                double correctedExtract1000 = 64 - DeductionIndB;
                double correctedBreakout1000 = 45 - DeductionIndB;
                // for 2000Hz octave band
                double correctedSupply2000 = 70 - DeductionIndB;
                double correctedExtract2000 = 62 - DeductionIndB;
                double correctedBreakout2000 = 43 - DeductionIndB;
                // for 4000Hz octave band
                double correctedSupply4000 = 66 - DeductionIndB;
                double correctedExtract4000 = 53 - DeductionIndB;
                double correctedBreakout4000 = 40 - DeductionIndB;
                // for 8000Hz octave band
                double correctedSupply8000 = 61 - DeductionIndB;
                double correctedExtract8000 = 43 - DeductionIndB;
                double correctedBreakout8000 = 28 - DeductionIndB;

                string suitable = calculatingTheSuitability(correctedSupply63, correctedExtract63, correctedBreakout63, correctedSupply125, correctedExtract125, correctedBreakout125,
                correctedSupply250, correctedExtract250, correctedBreakout250, correctedSupply500, correctedExtract500, correctedBreakout500,
                correctedSupply1000, correctedExtract1000, correctedBreakout1000, correctedSupply2000, correctedExtract2000, correctedBreakout2000,
                correctedSupply4000, correctedExtract4000, correctedBreakout4000, correctedSupply8000, correctedExtract8000, correctedBreakout8000);
                return suitable;
            }
            string SuitabilityXBC65()
            {
                // y = -165.000155283749000x3 - 315.762639557594000x2 - 427.811504641693000x + 616.069251925225000
                double A = -165.000155283749000;
                double B = -315.762639557594000 - a;
                double C = -427.811504641693000;
                double D = 616.069251925225000;
                double FanFlowRate = SolvingACubic(A, B, C, D);
                double DeductionIndB = FindDeductionIndB(FanFlowRate);
                // for 63Hz octave band
                double correctedSupply63 = 83 - DeductionIndB;
                double correctedExtract63 = 81 - DeductionIndB;
                double correctedBreakout63 = 71 - DeductionIndB;
                // for 125Hz octave band
                double correctedSupply125 = 85 - DeductionIndB;
                double correctedExtract125 = 79 - DeductionIndB;
                double correctedBreakout125 = 69 - DeductionIndB;
                // for 250Hz octave band
                double correctedSupply250 = 79 - DeductionIndB;
                double correctedExtract250 = 70 - DeductionIndB;
                double correctedBreakout250 = 56 - DeductionIndB;
                // for 500Hz octave band
                double correctedSupply500 = 74 - DeductionIndB;
                double correctedExtract500 = 67 - DeductionIndB;
                double correctedBreakout500 = 51 - DeductionIndB;
                // for 1000Hz octave band
                double correctedSupply1000 = 72 - DeductionIndB;
                double correctedExtract1000 = 64 - DeductionIndB;
                double correctedBreakout1000 = 45 - DeductionIndB;
                // for 2000Hz octave band
                double correctedSupply2000 = 68 - DeductionIndB;
                double correctedExtract2000 = 60 - DeductionIndB;
                double correctedBreakout2000 = 41 - DeductionIndB;
                // for 4000Hz octave band
                double correctedSupply4000 = 61 - DeductionIndB;
                double correctedExtract4000 = 48 - DeductionIndB;
                double correctedBreakout4000 = 35 - DeductionIndB;
                // for 8000Hz octave band
                double correctedSupply8000 = 54 - DeductionIndB;
                double correctedExtract8000 = 35 - DeductionIndB;
                double correctedBreakout8000 = 20 - DeductionIndB;

                string suitable = calculatingTheSuitability(correctedSupply63, correctedExtract63, correctedBreakout63, correctedSupply125, correctedExtract125, correctedBreakout125,
                correctedSupply250, correctedExtract250, correctedBreakout250, correctedSupply500, correctedExtract500, correctedBreakout500,
                correctedSupply1000, correctedExtract1000, correctedBreakout1000, correctedSupply2000, correctedExtract2000, correctedBreakout2000,
                correctedSupply4000, correctedExtract4000, correctedBreakout4000, correctedSupply8000, correctedExtract8000, correctedBreakout8000);
                return suitable;
            }
            double SolvingACubic(double A, double B, double C, double D)
            {
                // solving the equation with Cardano's method to solve a cubic equation
                double Q = ((3 * A * C) - (B * B)) / (9 * (A * A));
                double R = ((9 * A * B * C) - (27 * A * A * D) - (2 * B * B * B)) / (54 * A * A * A);
                // M is S^3
                double M = R + Math.Pow((Q * Q * Q) + (R * R), 1 / 2);
                double S = Math.Pow(M, 1 / 3);
                // N is T^3
                double N = R - Math.Pow((Q * Q * Q) + (R * R), 1 / 2);
                double T = Math.Pow(N, 1 / 3);
                double FanFlowRate = S + T - (B / (3 * A));
                return FanFlowRate;
            }

            double FindDeductionIndB(double FanFlowRate)
            {
                // finding the deduction in decibels as a result of the speed
                String query4 = "SELECT ReqAirVolRate FROM tblProject WHERE ProjectName=@ProjectName";
                SqlCommand sqlCmd4 = new SqlCommand(query4, sqlCon);
                sqlCmd4.CommandType = CommandType.Text;
                sqlCmd4.Parameters.AddWithValue("@ProjectName", txtProjectName.Text);
                double ReqAirVolRate = Convert.ToDouble(sqlCmd4.ExecuteScalar());
                double speedRatio = ReqAirVolRate / FanFlowRate;
                double DeductionIndB = 50 * Math.Log10(speedRatio);
                return DeductionIndB;
            }
            #endregion

            #region calculatingSuitability

            double findingSPL(double SWL, double distancevariable, double volumevariable, int octaveband)
            {
                double SPL = SWL - distancevariable - volumevariable - (3 * Math.Log10(octaveband)) + 12;
                return SPL;
            }

            double combineToCalcSPL(double Supply, double Extract, double Breakout)
            {
                double SPL = 10 * Math.Log10(Math.Pow(10, (Supply / 10)) + Math.Pow(10, (Extract / 10)) + Math.Pow(10, (Breakout / 10)));
                return SPL;
            }

            string calculatingTheSuitability(double correctedSupply63, double correctedExtract63, double correctedBreakout63, double correctedSupply125, double correctedExtract125, double correctedBreakout125,
                double correctedSupply250, double correctedExtract250, double correctedBreakout250, double correctedSupply500, double correctedExtract500, double correctedBreakout500,
                double correctedSupply1000, double correctedExtract1000, double correctedBreakout1000, double correctedSupply2000, double correctedExtract2000, double correctedBreakout2000,
                double correctedSupply4000, double correctedExtract4000, double correctedBreakout4000, double correctedSupply8000, double correctedExtract8000, double correctedBreakout8000)
            {
                // working out the attenuation
                string SupplyAttenuationType = FindSupplyAttenuationType();
                string ExtractAttenuationType = FindExtractAttenuationType();
                string BreakoutBarrierType = FindBreakoutBarrierType();

                // The subroutines used in lines 683 - 690 are called from lines 923 - 1213.
                // They each calculated the new 'attenuated' value, i.e. the new sound level after attenuators are added.
                // Attenuators are devices that absorb sound.

                double finalSupply63 = FindAttenuatedSupply63(SupplyAttenuationType, correctedSupply63);
                double finalSupply125 = FindAttenuatedSupply125(SupplyAttenuationType, correctedSupply125);
                double finalSupply250 = FindAttenuatedSupply250(SupplyAttenuationType, correctedSupply250);
                double finalSupply500 = FindAttenuatedSupply500(SupplyAttenuationType, correctedSupply500);
                double finalSupply1000 = FindAttenuatedSupply1000(SupplyAttenuationType, correctedSupply1000);
                double finalSupply2000 = FindAttenuatedSupply2000(SupplyAttenuationType, correctedSupply2000);
                double finalSupply4000 = FindAttenuatedSupply4000(SupplyAttenuationType, correctedSupply4000);
                double finalSupply8000 = FindAttenuatedSupply8000(SupplyAttenuationType, correctedSupply8000);

                double finalExtract63 = FindAttenuatedExtract63(ExtractAttenuationType, correctedExtract63);
                double finalExtract125 = FindAttenuatedExtract125(ExtractAttenuationType, correctedExtract125);
                double finalExtract250 = FindAttenuatedExtract250(ExtractAttenuationType, correctedExtract250);
                double finalExtract500 = FindAttenuatedExtract500(ExtractAttenuationType, correctedExtract500);
                double finalExtract1000 = FindAttenuatedExtract1000(ExtractAttenuationType, correctedExtract1000);
                double finalExtract2000 = FindAttenuatedExtract2000(ExtractAttenuationType, correctedExtract2000);
                double finalExtract4000 = FindAttenuatedExtract4000(ExtractAttenuationType, correctedExtract4000);
                double finalExtract8000 = FindAttenuatedExtract8000(ExtractAttenuationType, correctedExtract8000);

                double finalBreakout63 = FindAttenuatedBreakout63(BreakoutBarrierType, correctedBreakout63);
                double finalBreakout125 = FindAttenuatedBreakout125(BreakoutBarrierType, correctedBreakout125);
                double finalBreakout250 = FindAttenuatedBreakout250(BreakoutBarrierType, correctedBreakout250);
                double finalBreakout500 = FindAttenuatedBreakout500(BreakoutBarrierType, correctedBreakout500);
                double finalBreakout1000 = FindAttenuatedBreakout1000(BreakoutBarrierType, correctedBreakout1000);
                double finalBreakout2000 = FindAttenuatedBreakout2000(BreakoutBarrierType, correctedBreakout2000);
                double finalBreakout4000 = FindAttenuatedBreakout4000(BreakoutBarrierType, correctedBreakout4000);
                double finalBreakout8000 = FindAttenuatedBreakout8000(BreakoutBarrierType, correctedBreakout8000);

                // using Schultz's formula to calculate the sound pressure level from the sound power level at each octave band
                double RoomVolume = FindRoomVolume();
                double volumevariable = 5 * Math.Log10(RoomVolume);

                double FanDistanceToListener = FindDistancefromFantoListener();
                double fandistancevariable = 10 * Math.Log10(FanDistanceToListener);

                double SupplyDistanceToListener = FindDistancefromSupplytoListener();
                double supplydistancevariable = 10 * Math.Log10(SupplyDistanceToListener);

                double ExtractDistanceToListener = FindDistancefromExtracttoListener();
                double extractdistancevariable = 10 * Math.Log10(ExtractDistanceToListener);

                // the subroutine findingSPL is on line 657. It uses Schultz's formula to determine the SPL for each of the labeled values.

                double Supply63 = findingSPL(finalSupply63, supplydistancevariable, volumevariable, 63);
                double Supply125 = findingSPL(finalSupply125, supplydistancevariable, volumevariable, 125);
                double Supply250 = findingSPL(finalSupply250, supplydistancevariable, volumevariable, 250);
                double Supply500 = findingSPL(finalSupply500, supplydistancevariable, volumevariable, 500);
                double Supply1000 = findingSPL(finalSupply1000, supplydistancevariable, volumevariable, 1000);
                double Supply2000 = findingSPL(finalSupply2000, supplydistancevariable, volumevariable, 2000);
                double Supply4000 = findingSPL(finalSupply4000, supplydistancevariable, volumevariable, 4000);
                double Supply8000 = findingSPL(finalSupply8000, supplydistancevariable, volumevariable, 8000);

                double Extract63 = findingSPL(finalExtract63, extractdistancevariable, volumevariable, 63);
                double Extract125 = findingSPL(finalExtract125, extractdistancevariable, volumevariable, 125);
                double Extract250 = findingSPL(finalExtract250, extractdistancevariable, volumevariable, 250);
                double Extract500 = findingSPL(finalExtract500, extractdistancevariable, volumevariable, 500);
                double Extract1000 = findingSPL(finalExtract1000, extractdistancevariable, volumevariable, 1000);
                double Extract2000 = findingSPL(finalExtract2000, extractdistancevariable, volumevariable, 2000);
                double Extract4000 = findingSPL(finalExtract4000, extractdistancevariable, volumevariable, 4000);
                double Extract8000 = findingSPL(finalExtract8000, extractdistancevariable, volumevariable, 8000);

                double Breakout63 = findingSPL(finalBreakout63, fandistancevariable, volumevariable, 63);
                double Breakout125 = findingSPL(finalBreakout125, fandistancevariable, volumevariable, 125);
                double Breakout250 = findingSPL(finalBreakout250, fandistancevariable, volumevariable, 250);
                double Breakout500 = findingSPL(finalBreakout500, fandistancevariable, volumevariable, 500);
                double Breakout1000 = findingSPL(finalBreakout1000, fandistancevariable, volumevariable, 1000);
                double Breakout2000 = findingSPL(finalBreakout2000, fandistancevariable, volumevariable, 2000);
                double Breakout4000 = findingSPL(finalBreakout4000, fandistancevariable, volumevariable, 4000);
                double Breakout8000 = findingSPL(finalBreakout8000, fandistancevariable, volumevariable, 8000);

                // combining to give SPL values
                // the subroutine combineToCalcSPL is on line 663. This uses logarithmic addition to combine the 3 parameters.
                double SPL63 = combineToCalcSPL(Supply63, Extract63, Breakout63);
                double SPL125 = combineToCalcSPL(Supply125, Extract125, Breakout125);
                double SPL250 = combineToCalcSPL(Supply250, Extract250, Breakout250);
                double SPL500 = combineToCalcSPL(Supply500, Extract500, Breakout500);
                double SPL1000 = combineToCalcSPL(Supply1000, Extract1000, Breakout1000);
                double SPL2000 = combineToCalcSPL(Supply2000, Extract2000, Breakout2000);
                double SPL4000 = combineToCalcSPL(Supply4000, Extract4000, Breakout4000);
                double SPL8000 = combineToCalcSPL(Supply8000, Extract8000, Breakout8000);

                string overallsuitability;
                // comparing the SPL values to the NR values
                double NRVal = FindingNRValueOfRoom(); // this subroutine finds the NR value stored in the database.
                if (NRVal == 25)
                {
                    double SWL63 = 55;                    
                    double SWL125 = 44;                    
                    double SWL250 = 35;                    
                    double SWL500 = 29;                    
                    double SWL1000 = 25;                    
                    double SWL2000 = 22;                    
                    double SWL4000 = 20;                    
                    double SWL8000 = 18;
                    
                    bool suitable63 = FindIfSuitable(SPL63, SWL63);
                    bool suitable125 = FindIfSuitable(SPL125, SWL125);
                    bool suitable250 = FindIfSuitable(SPL250, SWL250);
                    bool suitable500 = FindIfSuitable(SPL500, SWL500);
                    bool suitable1000 = FindIfSuitable(SPL1000, SWL1000);
                    bool suitable2000 = FindIfSuitable(SPL2000, SWL2000);
                    bool suitable4000 = FindIfSuitable(SPL4000, SWL4000);
                    bool suitable8000 = FindIfSuitable(SPL8000, SWL8000);

                    overallsuitability = FindOverallSuitability(suitable63, suitable125, suitable250, suitable500, suitable1000, suitable2000, suitable4000, suitable8000);
                    return overallsuitability;
                }
                else if(NRVal == 30)
                {
                    double SWL63 = 59;
                    double SWL125 = 48;
                    double SWL250 = 40;
                    double SWL500 = 34;
                    double SWL1000 = 30;
                    double SWL2000 = 27;
                    double SWL4000 = 25;
                    double SWL8000 = 23;

                    bool suitable63 = FindIfSuitable(SPL63, SWL63);
                    bool suitable125 = FindIfSuitable(SPL125, SWL125);
                    bool suitable250 = FindIfSuitable(SPL250, SWL250);
                    bool suitable500 = FindIfSuitable(SPL500, SWL500);
                    bool suitable1000 = FindIfSuitable(SPL1000, SWL1000);
                    bool suitable2000 = FindIfSuitable(SPL2000, SWL2000);
                    bool suitable4000 = FindIfSuitable(SPL4000, SWL4000);
                    bool suitable8000 = FindIfSuitable(SPL8000, SWL8000);

                    overallsuitability = FindOverallSuitability(suitable63, suitable125, suitable250, suitable500, suitable1000, suitable2000, suitable4000, suitable8000);
                    return overallsuitability;
                }
                else if(NRVal == 35)
                {
                    double SWL63 = 63;
                    double SWL125 = 52;
                    double SWL250 = 45;
                    double SWL500 = 39;
                    double SWL1000 = 35;
                    double SWL2000 = 32;
                    double SWL4000 = 30;
                    double SWL8000 = 28;

                    bool suitable63 = FindIfSuitable(SPL63, SWL63);
                    bool suitable125 = FindIfSuitable(SPL125, SWL125);
                    bool suitable250 = FindIfSuitable(SPL250, SWL250);
                    bool suitable500 = FindIfSuitable(SPL500, SWL500);
                    bool suitable1000 = FindIfSuitable(SPL1000, SWL1000);
                    bool suitable2000 = FindIfSuitable(SPL2000, SWL2000);
                    bool suitable4000 = FindIfSuitable(SPL4000, SWL4000);
                    bool suitable8000 = FindIfSuitable(SPL8000, SWL8000);

                    overallsuitability = FindOverallSuitability(suitable63, suitable125, suitable250, suitable500, suitable1000, suitable2000, suitable4000, suitable8000);
                    return overallsuitability;
                }
                else if (NRVal == 40)
                {
                    double SWL63 = 67;
                    double SWL125 = 57;
                    double SWL250 = 49;
                    double SWL500 = 44;
                    double SWL1000 = 40;
                    double SWL2000 = 37;
                    double SWL4000 = 35;
                    double SWL8000 = 33;

                    bool suitable63 = FindIfSuitable(SPL63, SWL63);
                    bool suitable125 = FindIfSuitable(SPL125, SWL125);
                    bool suitable250 = FindIfSuitable(SPL250, SWL250);
                    bool suitable500 = FindIfSuitable(SPL500, SWL500);
                    bool suitable1000 = FindIfSuitable(SPL1000, SWL1000);
                    bool suitable2000 = FindIfSuitable(SPL2000, SWL2000);
                    bool suitable4000 = FindIfSuitable(SPL4000, SWL4000);
                    bool suitable8000 = FindIfSuitable(SPL8000, SWL8000);

                    overallsuitability = FindOverallSuitability(suitable63, suitable125, suitable250, suitable500, suitable1000, suitable2000, suitable4000, suitable8000);
                    return overallsuitability;
                }
                else if (NRVal == 45)
                {
                    double SWL63 = 71;
                    double SWL125 = 61;
                    double SWL250 = 54;
                    double SWL500 = 48;
                    double SWL1000 = 45;
                    double SWL2000 = 42;
                    double SWL4000 = 40;
                    double SWL8000 = 38;

                    bool suitable63 = FindIfSuitable(SPL63, SWL63);
                    bool suitable125 = FindIfSuitable(SPL125, SWL125);
                    bool suitable250 = FindIfSuitable(SPL250, SWL250);
                    bool suitable500 = FindIfSuitable(SPL500, SWL500);
                    bool suitable1000 = FindIfSuitable(SPL1000, SWL1000);
                    bool suitable2000 = FindIfSuitable(SPL2000, SWL2000);
                    bool suitable4000 = FindIfSuitable(SPL4000, SWL4000);
                    bool suitable8000 = FindIfSuitable(SPL8000, SWL8000);

                    overallsuitability = FindOverallSuitability(suitable63, suitable125, suitable250, suitable500, suitable1000, suitable2000, suitable4000, suitable8000);
                    return overallsuitability;
                }
                else
                {
                    overallsuitability = "No";
                    return overallsuitability;
                }                
            }

            // Compares the SPL and SWL to see if the fan is suitable at each octave band.
            bool FindIfSuitable(double SPLCalc, double NRSPL)
            {
                if (SPLCalc >= NRSPL)
                {
                    bool suitable = true;
                    return suitable;
                }
                else
                {
                    bool suitable = false;
                    return suitable;
                }
            }

            // If the fan is suitable at all the octave bands, then the fan is suitable overall.
            // Otherwise, it is not suitable.
            string FindOverallSuitability(bool suitable63, bool suitable125, bool suitable250, bool suitable500, bool suitable1000, bool suitable2000, bool suitable4000, bool suitable8000)
            {
                if ((suitable63 == true) && (suitable125 == true) && (suitable250 == true) && (suitable500 == true) && (suitable1000 == true) && (suitable2000 == true) && (suitable4000 == true) && (suitable8000 == true))
                {
                    string overallsuitability = "Yes";
                    return overallsuitability;
                }
                else
                {
                    string overallsuitability = "No";
                    return overallsuitability;
                }
            }
            #endregion

            #region finalSupply
            // find the corrected values for the attenuators and acoustic barrier

            double FindAttenuatedSupply63(string SupplyAttenuationType, double correctedSupply63)
            {
                if(SupplyAttenuationType == "Short")
                {
                    double attenuatedSupply63 = correctedSupply63 - 5;
                    double hydraulicArea = FindHydraulicArea();
                    double endReflection63 = 10 * (Math.Log10(1 + Math.Pow(((0.8 * 344) / (3.142 * hydraulicArea * 63)), 1.88)));
                    double finalSupply63 = FindFinal63(attenuatedSupply63, endReflection63);
                    return finalSupply63;
                }
                else if (SupplyAttenuationType == "Medium")
                {
                    double attenuatedSupply63 = correctedSupply63 - 7;
                    double hydraulicArea = FindHydraulicArea();
                    double endReflection63 = 10 * (Math.Log10(1 + Math.Pow(((0.8 * 344) / (3.142 * hydraulicArea * 63)), 1.88)));
                    double finalSupply63 = FindFinal63(attenuatedSupply63, endReflection63);
                    return finalSupply63;
                }
                else if (SupplyAttenuationType == "Long")
                {
                    double attenuatedSupply63 = correctedSupply63 - 9;
                    double hydraulicArea = FindHydraulicArea();
                    double endReflection63 = 10 * (Math.Log10(1 + Math.Pow(((0.8 * 344) / (3.142 * hydraulicArea * 63)), 1.88)));
                    double finalSupply63 = FindFinal63(attenuatedSupply63, endReflection63);
                    return finalSupply63;
                }
                else
                {
                    double number = 1;
                    return number;
                }
                
            }

            double FindAttenuatedSupply125(string SupplyAttenuationType, double correctedSupply125)
            {
                if (SupplyAttenuationType == "Short")
                {
                    double attenuatedSupply125 = correctedSupply125 - 8;
                    double hydraulicArea = FindHydraulicArea();
                    double endReflection125 = 10 * (Math.Log10(1 + Math.Pow(((0.8 * 344) / (3.142 * hydraulicArea * 125)), 1.88)));
                    double finalSupply125 = FindFinal125(attenuatedSupply125, endReflection125);
                    return finalSupply125;
                }
                else if (SupplyAttenuationType == "Medium")
                {
                    double attenuatedSupply125 = correctedSupply125 - 10;
                    double hydraulicArea = FindHydraulicArea();
                    double endReflection125 = 10 * (Math.Log10(1 + Math.Pow(((0.8 * 344) / (3.142 * hydraulicArea * 125)), 1.88)));
                    double finalSupply125 = FindFinal125(attenuatedSupply125, endReflection125);
                    return finalSupply125;

                }
                else if (SupplyAttenuationType == "Long")
                {
                    double attenuatedSupply125 = correctedSupply125 - 13;
                    double hydraulicArea = FindHydraulicArea();
                    double endReflection125 = 10 * (Math.Log10(1 + Math.Pow(((0.8 * 344) / (3.142 * hydraulicArea * 125)), 1.88)));
                    double finalSupply125 = FindFinal125(attenuatedSupply125, endReflection125);
                    return finalSupply125;

                }
                else
                {
                    double number = 1;
                    return number;
                }

            }

            double FindAttenuatedSupply250(string SupplyAttenuationType, double correctedSupply250)
            {
                if (SupplyAttenuationType == "Short")
                {
                    double attenuatedSupply250 = correctedSupply250 - 15;
                    double hydraulicArea = FindHydraulicArea();
                    double endReflection250 = 10 * (Math.Log10(1 + Math.Pow(((0.8 * 344) / (3.142 * hydraulicArea * 250)), 1.88)));
                    double finalSupply250 = FindFinal250(attenuatedSupply250, endReflection250);
                    return finalSupply250;

                }
                else if (SupplyAttenuationType == "Medium")
                {
                    double attenuatedSupply250 = correctedSupply250 - 18;
                    double hydraulicArea = FindHydraulicArea();
                    double endReflection250 = 10 * (Math.Log10(1 + Math.Pow(((0.8 * 344) / (3.142 * hydraulicArea * 250)), 1.88)));
                    double finalSupply250 = FindFinal250(attenuatedSupply250, endReflection250);
                    return finalSupply250;

                }
                else if (SupplyAttenuationType == "Long")
                {
                    double attenuatedSupply250 = correctedSupply250 - 23;
                    double hydraulicArea = FindHydraulicArea();
                    double endReflection250 = 10 * (Math.Log10(1 + Math.Pow(((0.8 * 344) / (3.142 * hydraulicArea * 250)), 1.88)));
                    double finalSupply250 = FindFinal250(attenuatedSupply250, endReflection250);
                    return finalSupply250;

                }
                else
                {
                    double number = 1;
                    return number;
                }

            }

            double FindAttenuatedSupply500(string SupplyAttenuationType, double correctedSupply500)
            {
                if (SupplyAttenuationType == "Short")
                {
                    double attenuatedSupply500 = correctedSupply500 - 30;
                    double hydraulicArea = FindHydraulicArea();
                    double endReflection500 = 10 * (Math.Log10(1 + Math.Pow(((0.8 * 344) / (3.142 * hydraulicArea * 500)), 1.88)));
                    double finalSupply500 = FindFinal500(attenuatedSupply500, endReflection500);
                    return finalSupply500;

                }
                else if (SupplyAttenuationType == "Medium")
                {
                    double attenuatedSupply500 = correctedSupply500 - 36;
                    double hydraulicArea = FindHydraulicArea();
                    double endReflection500 = 10 * (Math.Log10(1 + Math.Pow(((0.8 * 344) / (3.142 * hydraulicArea * 500)), 1.88)));
                    double finalSupply500 = FindFinal500(attenuatedSupply500, endReflection500);
                    return finalSupply500;

                }
                else if (SupplyAttenuationType == "Long")
                {
                    double attenuatedSupply500 = correctedSupply500 - 42;
                    double hydraulicArea = FindHydraulicArea();
                    double endReflection500 = 10 * (Math.Log10(1 + Math.Pow(((0.8 * 344) / (3.142 * hydraulicArea * 500)), 1.88)));
                    double finalSupply500 = FindFinal500(attenuatedSupply500, endReflection500);
                    return finalSupply500;

                }
                else
                {
                    double number = 1;
                    return number;
                }
            }

            double FindAttenuatedSupply1000(string SupplyAttenuationType, double correctedSupply1000)
            {
                if (SupplyAttenuationType == "Short")
                {
                    double attenuatedSupply1000 = correctedSupply1000 - 41;
                    double hydraulicArea = FindHydraulicArea();
                    double endReflection1000 = 10 * (Math.Log10(1 + Math.Pow(((0.8 * 344) / (3.142 * hydraulicArea * 1000)), 1.88)));
                    double finalSupply1000 = FindFinal1000(attenuatedSupply1000, endReflection1000);
                    return finalSupply1000;

                }
                else if (SupplyAttenuationType == "Medium")
                {
                    double attenuatedSupply1000 = correctedSupply1000 - 51;
                    double hydraulicArea = FindHydraulicArea();
                    double endReflection1000 = 10 * (Math.Log10(1 + Math.Pow(((0.8 * 344) / (3.142 * hydraulicArea * 1000)), 1.88)));
                    double finalSupply1000 = FindFinal1000(attenuatedSupply1000, endReflection1000);
                    return finalSupply1000;

                }
                else if (SupplyAttenuationType == "Long")
                {
                    double attenuatedSupply1000 = correctedSupply1000 - 55;
                    double hydraulicArea = FindHydraulicArea();
                    double endReflection1000 = 10 * (Math.Log10(1 + Math.Pow(((0.8 * 344) / (3.142 * hydraulicArea * 1000)), 1.88)));
                    double finalSupply1000 = FindFinal1000(attenuatedSupply1000, endReflection1000);
                    return finalSupply1000;

                }
                else
                {
                    double number = 1;
                    return number;
                }
            }

            double FindAttenuatedSupply2000(string SupplyAttenuationType, double correctedSupply2000)
            {
                if (SupplyAttenuationType == "Short")
                {
                    double attenuatedSupply2000 = correctedSupply2000 - 31;
                    double hydraulicArea = FindHydraulicArea();
                    double endReflection2000 = 10 * (Math.Log10(1 + Math.Pow(((0.8 * 344) / (3.142 * hydraulicArea * 2000)), 1.88)));
                    double finalSupply2000 = FindFinal2000(attenuatedSupply2000, endReflection2000);
                    return finalSupply2000;

                }
                else if (SupplyAttenuationType == "Medium")
                {
                    double attenuatedSupply2000 = correctedSupply2000 - 39;
                    double hydraulicArea = FindHydraulicArea();
                    double endReflection2000 = 10 * (Math.Log10(1 + Math.Pow(((0.8 * 344) / (3.142 * hydraulicArea * 2000)), 1.88)));
                    double finalSupply2000 = FindFinal2000(attenuatedSupply2000, endReflection2000);
                    return finalSupply2000;

                }
                else if (SupplyAttenuationType == "Long")
                {
                    double attenuatedSupply2000 = correctedSupply2000 - 49;
                    double hydraulicArea = FindHydraulicArea();
                    double endReflection2000 = 10 * (Math.Log10(1 + Math.Pow(((0.8 * 344) / (3.142 * hydraulicArea * 2000)), 1.88)));
                    double finalSupply2000 = FindFinal2000(attenuatedSupply2000, endReflection2000);
                    return finalSupply2000;

                }
                else
                {
                    double number = 1;
                    return number;
                }
            }

            double FindAttenuatedSupply4000(string SupplyAttenuationType, double correctedSupply4000)
            {
                if (SupplyAttenuationType == "Short")
                {
                    double attenuatedSupply4000 = correctedSupply4000 - 21;
                    double hydraulicArea = FindHydraulicArea();
                    double endReflection4000 = 10 * (Math.Log10(1 + Math.Pow(((0.8 * 344) / (3.142 * hydraulicArea * 4000)), 1.88)));
                    double finalSupply4000 = FindFinal4000(attenuatedSupply4000, endReflection4000);
                    return finalSupply4000;

                }
                else if (SupplyAttenuationType == "Medium")
                {
                    double attenuatedSupply4000 = correctedSupply4000 - 26;
                    double hydraulicArea = FindHydraulicArea();
                    double endReflection4000 = 10 * (Math.Log10(1 + Math.Pow(((0.8 * 344) / (3.142 * hydraulicArea * 4000)), 1.88)));
                    double finalSupply4000 = FindFinal4000(attenuatedSupply4000, endReflection4000);
                    return finalSupply4000;

                }
                else if (SupplyAttenuationType == "Long")
                {
                    double attenuatedSupply4000 = correctedSupply4000 - 32;
                    double hydraulicArea = FindHydraulicArea();
                    double endReflection4000 = 10 * (Math.Log10(1 + Math.Pow(((0.8 * 344) / (3.142 * hydraulicArea * 4000)), 1.88)));
                    double finalSupply4000 = FindFinal4000(attenuatedSupply4000, endReflection4000);
                    return finalSupply4000;

                }
                else
                {
                    double number = 1;
                    return number;
                }
            }

            double FindAttenuatedSupply8000(string SupplyAttenuationType, double correctedSupply8000)
            {
                if (SupplyAttenuationType == "Short")
                {                                       
                    double attenuatedSupply8000 = correctedSupply8000 - 16;

                    double hydraulicArea = FindHydraulicArea(); // called from line 1723.

                    // calculating end reflection - calculated with set formula.

                    double endReflection8000 = 10 * (Math.Log10(1 + Math.Pow(((0.8 * 344) / (3.142 * hydraulicArea * 8000)), 1.88)));
                    double finalSupply8000 = FindFinal8000(attenuatedSupply8000, endReflection8000);
                    return finalSupply8000;

                }
                else if (SupplyAttenuationType == "Medium")
                {                    
                    double attenuatedSupply8000 = correctedSupply8000 - 20;
                    double hydraulicArea = FindHydraulicArea();

                    // calculating end reflection
                    double endReflection8000 = 10 * (Math.Log10(1 + Math.Pow(((0.8 * 344) / (3.142 * hydraulicArea * 8000)), 1.88)));
                    double finalSupply8000 = FindFinal8000(attenuatedSupply8000, endReflection8000);
                    return finalSupply8000;
                }
                else if (SupplyAttenuationType == "Long")
                {                   
                    double attenuatedSupply8000 = correctedSupply8000 - 25;
                    double hydraulicArea = FindHydraulicArea();
                    double endReflection8000 = 10 * (Math.Log10(1 + Math.Pow(((0.8 * 344) / (3.142 * hydraulicArea * 8000)), 1.88)));

                    double finalSupply8000 = FindFinal8000(attenuatedSupply8000, endReflection8000);
                    return finalSupply8000;
                }
                else
                {
                    double number = 1;
                    return number;
                }
            }
            #endregion

            #region finalExtract

            double FindAttenuatedExtract63(string ExtractAttenuationType, double correctedExtract63)
            {
                if (ExtractAttenuationType == "Short")
                {
                    double attenuatedExtract63 = correctedExtract63 - 4;
                    double hydraulicArea = FindHydraulicArea();
                    double endReflection63 = 10 * (Math.Log10(1 + Math.Pow(((0.8 * 344) / (3.142 * hydraulicArea * 63)), 1.88)));
                    double finalExtract63 = FindFinal63(attenuatedExtract63, endReflection63);
                    return finalExtract63;
                }
                else if (ExtractAttenuationType == "Medium")
                {
                    double attenuatedExtract63 = correctedExtract63 - 5;
                    double hydraulicArea = FindHydraulicArea();
                    double endReflection63 = 10 * (Math.Log10(1 + Math.Pow(((0.8 * 344) / (3.142 * hydraulicArea * 63)), 1.88)));
                    double finalExtract63 = FindFinal63(attenuatedExtract63, endReflection63);
                    return finalExtract63;
                }
                else if (ExtractAttenuationType == "Long")
                {
                    double attenuatedExtract63 = correctedExtract63 - 6;
                    double hydraulicArea = FindHydraulicArea();
                    double endReflection63 = 10 * (Math.Log10(1 + Math.Pow(((0.8 * 344) / (3.142 * hydraulicArea * 63)), 1.88)));
                    double finalExtract63 = FindFinal63(attenuatedExtract63, endReflection63);
                    return finalExtract63;
                }
                else
                {
                    double number = 1;
                    return number;
                }

            }

            double FindAttenuatedExtract125(string ExtractAttenuationType, double correctedExtract125)
            {
                if (ExtractAttenuationType == "Short")
                {
                    double attenuatedExtract125 = correctedExtract125 - 4;
                    double hydraulicArea = FindHydraulicArea();
                    double endReflection125 = 10 * (Math.Log10(1 + Math.Pow(((0.8 * 344) / (3.142 * hydraulicArea * 125)), 1.88)));
                    double finalExtract125 = FindFinal125(attenuatedExtract125, endReflection125);
                    return finalExtract125;
                }
                else if (ExtractAttenuationType == "Medium")
                {
                    double attenuatedExtract125 = correctedExtract125 - 6;
                    double hydraulicArea = FindHydraulicArea();
                    double endReflection125 = 10 * (Math.Log10(1 + Math.Pow(((0.8 * 344) / (3.142 * hydraulicArea * 125)), 1.88)));
                    double finalExtract125 = FindFinal125(attenuatedExtract125, endReflection125);
                    return finalExtract125;
                }
                else if (ExtractAttenuationType == "Long")
                {
                    double attenuatedExtract125 = correctedExtract125 - 8;
                    double hydraulicArea = FindHydraulicArea();
                    double endReflection125 = 10 * (Math.Log10(1 + Math.Pow(((0.8 * 344) / (3.142 * hydraulicArea * 125)), 1.88)));
                    double finalExtract125 = FindFinal125(attenuatedExtract125, endReflection125);
                    return finalExtract125;
                }
                else
                {
                    double number = 1;
                    return number;
                }
            }

            double FindAttenuatedExtract250(string ExtractAttenuationType, double correctedExtract250)
            {
                if (ExtractAttenuationType == "Short")
                {
                    double attenuatedExtract250 = correctedExtract250 - 10;
                    double hydraulicArea = FindHydraulicArea();
                    double endReflection250 = 10 * (Math.Log10(1 + Math.Pow(((0.8 * 344) / (3.142 * hydraulicArea * 250)), 1.88)));
                    double finalExtract250 = FindFinal250(attenuatedExtract250, endReflection250);
                    return finalExtract250;
                }
                else if (ExtractAttenuationType == "Medium")
                {
                    double attenuatedExtract250 = correctedExtract250 - 12;
                    double hydraulicArea = FindHydraulicArea();
                    double endReflection250 = 10 * (Math.Log10(1 + Math.Pow(((0.8 * 344) / (3.142 * hydraulicArea * 250)), 1.88)));
                    double finalExtract250 = FindFinal250(attenuatedExtract250, endReflection250);
                    return finalExtract250;
                }
                else if (ExtractAttenuationType == "Long")
                {
                    double attenuatedExtract250 = correctedExtract250 - 15;
                    double hydraulicArea = FindHydraulicArea();
                    double endReflection250 = 10 * (Math.Log10(1 + Math.Pow(((0.8 * 344) / (3.142 * hydraulicArea * 250)), 1.88)));
                    double finalExtract250 = FindFinal250(attenuatedExtract250, endReflection250);
                    return finalExtract250;
                }
                else
                {
                    double number = 1;
                    return number;
                }
            }

            double FindAttenuatedExtract500(string ExtractAttenuationType, double correctedExtract500)
            {
                if (ExtractAttenuationType == "Short")
                {
                    double attenuatedExtract500 = correctedExtract500 - 22;
                    double hydraulicArea = FindHydraulicArea();
                    double endReflection500 = 10 * (Math.Log10(1 + Math.Pow(((0.8 * 344) / (3.142 * hydraulicArea * 500)), 1.88)));
                    double finalExtract500 = FindFinal500(attenuatedExtract500, endReflection500);
                    return finalExtract500;
                }
                else if (ExtractAttenuationType == "Medium")
                {
                    double attenuatedExtract500 = correctedExtract500 - 27;
                    double hydraulicArea = FindHydraulicArea();
                    double endReflection500 = 10 * (Math.Log10(1 + Math.Pow(((0.8 * 344) / (3.142 * hydraulicArea * 500)), 1.88)));
                    double finalExtract500 = FindFinal500(attenuatedExtract500, endReflection500);
                    return finalExtract500;
                }
                else if (ExtractAttenuationType == "Long")
                {
                    double attenuatedExtract500 = correctedExtract500 - 33;
                    double hydraulicArea = FindHydraulicArea();
                    double endReflection500 = 10 * (Math.Log10(1 + Math.Pow(((0.8 * 344) / (3.142 * hydraulicArea * 500)), 1.88)));
                    double finalExtract500 = FindFinal500(attenuatedExtract500, endReflection500);
                    return finalExtract500;
                }
                else
                {
                    double number = 1;
                    return number;
                }
            }

            double FindAttenuatedExtract1000(string ExtractAttenuationType, double correctedExtract1000)
            {
                if (ExtractAttenuationType == "Short")
                {
                    double attenuatedExtract1000 = correctedExtract1000 - 26;
                    double hydraulicArea = FindHydraulicArea();
                    double endReflection1000 = 10 * (Math.Log10(1 + Math.Pow(((0.8 * 344) / (3.142 * hydraulicArea * 1000)), 1.88)));
                    double finalExtract1000 = FindFinal1000(attenuatedExtract1000, endReflection1000);
                    return finalExtract1000;
                }
                else if (ExtractAttenuationType == "Medium")
                {
                    double attenuatedExtract1000 = correctedExtract1000 - 34;
                    double hydraulicArea = FindHydraulicArea();
                    double endReflection1000 = 10 * (Math.Log10(1 + Math.Pow(((0.8 * 344) / (3.142 * hydraulicArea * 1000)), 1.88)));
                    double finalExtract1000 = FindFinal1000(attenuatedExtract1000, endReflection1000);
                    return finalExtract1000;
                }
                else if (ExtractAttenuationType == "Long")
                {
                    double attenuatedExtract1000 = correctedExtract1000 - 43;
                    double hydraulicArea = FindHydraulicArea();
                    double endReflection1000 = 10 * (Math.Log10(1 + Math.Pow(((0.8 * 344) / (3.142 * hydraulicArea * 1000)), 1.88)));
                    double finalExtract1000 = FindFinal1000(attenuatedExtract1000, endReflection1000);
                    return finalExtract1000;
                }
                else
                {
                    double number = 1;
                    return number;
                }
            }

            double FindAttenuatedExtract2000(string ExtractAttenuationType, double correctedExtract2000)
            {
                if (ExtractAttenuationType == "Short")
                {
                    double attenuatedExtract2000 = correctedExtract2000 - 15;
                    double hydraulicArea = FindHydraulicArea();
                    double endReflection2000 = 10 * (Math.Log10(1 + Math.Pow(((0.8 * 344) / (3.142 * hydraulicArea * 2000)), 1.88)));
                    double finalExtract2000 = FindFinal2000(attenuatedExtract2000, endReflection2000);
                    return finalExtract2000;
                }
                else if (ExtractAttenuationType == "Medium")
                {
                    double attenuatedExtract2000 = correctedExtract2000 - 20;
                    double hydraulicArea = FindHydraulicArea();
                    double endReflection2000 = 10 * (Math.Log10(1 + Math.Pow(((0.8 * 344) / (3.142 * hydraulicArea * 2000)), 1.88)));
                    double finalExtract2000 = FindFinal2000(attenuatedExtract2000, endReflection2000);
                    return finalExtract2000;
                }
                else if (ExtractAttenuationType == "Long")
                {
                    double attenuatedExtract2000 = correctedExtract2000 - 25;
                    double hydraulicArea = FindHydraulicArea();
                    double endReflection2000 = 10 * (Math.Log10(1 + Math.Pow(((0.8 * 344) / (3.142 * hydraulicArea * 2000)), 1.88)));
                    double finalExtract2000 = FindFinal2000(attenuatedExtract2000, endReflection2000);
                    return finalExtract2000;
                }
                else
                {
                    double number = 1;
                    return number;
                }
            }

            double FindAttenuatedExtract4000(string ExtractAttenuationType, double correctedExtract4000)
            {
                if (ExtractAttenuationType == "Short")
                {
                    double attenuatedExtract4000 = correctedExtract4000 - 10;
                    double hydraulicArea = FindHydraulicArea();
                    double endReflection4000 = 10 * (Math.Log10(1 + Math.Pow(((0.8 * 344) / (3.142 * hydraulicArea * 4000)), 1.88)));
                    double finalExtract4000 = FindFinal4000(attenuatedExtract4000, endReflection4000);
                    return finalExtract4000;
                }
                else if (ExtractAttenuationType == "Medium")
                {
                    double attenuatedExtract4000 = correctedExtract4000 - 13;
                    double hydraulicArea = FindHydraulicArea();
                    double endReflection4000 = 10 * (Math.Log10(1 + Math.Pow(((0.8 * 344) / (3.142 * hydraulicArea * 4000)), 1.88)));
                    double finalExtract4000 = FindFinal4000(attenuatedExtract4000, endReflection4000);
                    return finalExtract4000;
                }
                else if (ExtractAttenuationType == "Long")
                {
                    double attenuatedExtract4000 = correctedExtract4000 - 15;
                    double hydraulicArea = FindHydraulicArea();
                    double endReflection4000 = 10 * (Math.Log10(1 + Math.Pow(((0.8 * 344) / (3.142 * hydraulicArea * 4000)), 1.88)));
                    double finalExtract4000 = FindFinal4000(attenuatedExtract4000, endReflection4000);
                    return finalExtract4000;
                }
                else
                {
                    double number = 1;
                    return number;
                }
            }

            double FindAttenuatedExtract8000(string ExtractAttenuationType, double correctedExtract8000)
            {
                if (ExtractAttenuationType == "Short")
                {
                    double attenuatedExtract8000 = correctedExtract8000 - 8;

                    double hydraulicArea = FindHydraulicArea();

                    // calculating end reflection

                    double endReflection8000 = 10 * (Math.Log10(1 + Math.Pow(((0.8 * 344) / (3.142 * hydraulicArea * 8000)), 1.88)));

                    double finalExtract8000 = FindFinal8000(attenuatedExtract8000, endReflection8000);
                    return finalExtract8000;
                }
                else if (ExtractAttenuationType == "Medium")
                {
                    double attenuatedExtract8000 = correctedExtract8000 - 9;

                    double hydraulicArea = FindHydraulicArea();

                    // calculating end reflection
                    double endReflection8000 = 10 * (Math.Log10(1 + Math.Pow(((0.8 * 344) / (3.142 * hydraulicArea * 8000)), 1.88)));

                    double finalExtract8000 = FindFinal8000(attenuatedExtract8000, endReflection8000);
                    return finalExtract8000;

                }
                else if (ExtractAttenuationType == "Long")
                {
                    double attenuatedExtract8000 = correctedExtract8000 - 11;

                    double hydraulicArea = FindHydraulicArea();

                    // calculating end reflection

                    double endReflection8000 = 10 * (Math.Log10(1 + Math.Pow(((0.8 * 344) / (3.142 * hydraulicArea * 8000)), 1.88)));

                    double finalExtract8000 = FindFinal8000(attenuatedExtract8000, endReflection8000);
                    return finalExtract8000;

                }
                else
                {
                    double number = 1;
                    return number;
                }
            }
            #endregion
                       
            #region finalBreakout
            double FindAttenuatedBreakout63(string BreakoutBarrierType, double correctedBreakout63)
            {
                if (BreakoutBarrierType == "Basic")
                {
                    double finalBreakout63 = correctedBreakout63 - 2;
                    return finalBreakout63;
                }
                else if (BreakoutBarrierType == "Acoustic Grade")
                {
                    double finalBreakout63 = correctedBreakout63 - 7;
                    return finalBreakout63;
                }
                else
                {
                    double number = 1;
                    return number;
                }
            }

            double FindAttenuatedBreakout125(string BreakoutBarrierType, double correctedBreakout125)
            {
                if (BreakoutBarrierType == "Basic")
                {
                    double finalBreakout125 = correctedBreakout125 - 3;
                    return finalBreakout125;
                }
                else if (BreakoutBarrierType == "Acoustic Grade")
                {
                    double finalBreakout125 = correctedBreakout125 - 9;
                    return finalBreakout125;
                }
                else
                {
                    double number = 1;
                    return number;
                }
            }

            double FindAttenuatedBreakout250(string BreakoutBarrierType, double correctedBreakout250)
            {
                if (BreakoutBarrierType == "Basic")
                {
                    double finalBreakout250 = correctedBreakout250 - 3;
                    return finalBreakout250;
                }
                else if (BreakoutBarrierType == "Acoustic Grade")
                {
                    double finalBreakout250 = correctedBreakout250 - 10;
                    return finalBreakout250;
                }
                else
                {
                    double number = 1;
                    return number;
                }
            }

            double FindAttenuatedBreakout500(string BreakoutBarrierType, double correctedBreakout500)
            {
                if (BreakoutBarrierType == "Basic")
                {
                    double finalBreakout500 = correctedBreakout500 - 5;
                    return finalBreakout500;
                }
                else if (BreakoutBarrierType == "Acoustic Grade")
                {
                    double finalBreakout500 = correctedBreakout500 - 15;
                    return finalBreakout500;
                }
                else
                {
                    double number = 1;
                    return number;
                }
            }

            double FindAttenuatedBreakout1000(string BreakoutBarrierType, double correctedBreakout1000)
            {
                if (BreakoutBarrierType == "Basic")
                {
                    double finalBreakout1000 = correctedBreakout1000 - 5;
                    return finalBreakout1000;
                }
                else if (BreakoutBarrierType == "Acoustic Grade")
                {
                    double finalBreakout1000 = correctedBreakout1000 - 20;
                    return finalBreakout1000;
                }
                else
                {
                    double number = 1;
                    return number;
                }
            }

            double FindAttenuatedBreakout2000(string BreakoutBarrierType, double correctedBreakout2000)
            {
                if (BreakoutBarrierType == "Basic")
                {
                    double finalBreakout2000 = correctedBreakout2000 - 8;
                    return finalBreakout2000;
                }
                else if (BreakoutBarrierType == "Acoustic Grade")
                {
                    double finalBreakout2000 = correctedBreakout2000 - 24;
                    return finalBreakout2000;
                }
                else
                {
                    double number = 1;
                    return number;
                }
            }

            double FindAttenuatedBreakout4000(string BreakoutBarrierType, double correctedBreakout4000)
            {
                if (BreakoutBarrierType == "Basic")
                {
                    double finalBreakout4000 = correctedBreakout4000 - 9;
                    return finalBreakout4000;
                }
                else if (BreakoutBarrierType == "Acoustic Grade")
                {
                    double finalBreakout4000 = correctedBreakout4000 - 27;
                    return finalBreakout4000;
                }
                else
                {
                    double number = 1;
                    return number;
                }
            }

            double FindAttenuatedBreakout8000(string BreakoutBarrierType, double correctedBreakout8000)
            {
                if (BreakoutBarrierType == "Basic")
                {
                    double finalBreakout8000 = correctedBreakout8000 - 8;
                    return finalBreakout8000;
                }
                else if (BreakoutBarrierType == "Acoustic Grade")
                {
                    double finalBreakout8000 = correctedBreakout8000 - 24;
                    return finalBreakout8000;
                }
                else
                {
                    double number = 1;
                    return number;
                }
            }

            #endregion

            #region findFinal

            double FindFinal63(double attenuatedExtract63, double endReflection63)
            {
                double finalExtract63 = attenuatedExtract63 - endReflection63;
                return finalExtract63;
            }
            double FindFinal125(double attenuatedExtract63, double endReflection63)
            {
                double finalExtract63 = attenuatedExtract63 - endReflection63;
                return finalExtract63;
            }
            double FindFinal250(double attenuatedExtract63, double endReflection63)
            {
                double finalExtract63 = attenuatedExtract63 - endReflection63;
                return finalExtract63;
            }
            double FindFinal500(double attenuatedExtract63, double endReflection63)
            {
                double finalExtract63 = attenuatedExtract63 - endReflection63;
                return finalExtract63;
            }
            double FindFinal1000(double attenuatedExtract63, double endReflection63)
            {
                double finalExtract63 = attenuatedExtract63 - endReflection63;
                return finalExtract63;
            }
            double FindFinal2000(double attenuatedExtract63, double endReflection63)
            {
                double finalExtract63 = attenuatedExtract63 - endReflection63;
                return finalExtract63;
            }
            double FindFinal4000(double attenuatedExtract63, double endReflection63)
            {
                double finalExtract63 = attenuatedExtract63 - endReflection63;
                return finalExtract63;
            }
            double FindFinal8000(double attenuatedExtract63, double endReflection63)
            {
                double finalExtract63 = attenuatedExtract63 - endReflection63;
                return finalExtract63;
            }

            #endregion


            // finding the 'flush' or end reflection from the room terminal side length

            double FindHydraulicArea()
            {
                // finding a side length of the room terminal
                String query4 = "SELECT RoomTerminalSideLength FROM tblProject WHERE ProjectName=@ProjectName";
                SqlCommand sqlCmd4 = new SqlCommand(query4, sqlCon);
                sqlCmd4.CommandType = CommandType.Text;
                sqlCmd4.Parameters.AddWithValue("@ProjectName", txtProjectName.Text);
                double RoomTerminalSideLength = Convert.ToDouble(sqlCmd4.ExecuteScalar());
                if(Units == "Feet")
                {
                    RoomTerminalSideLength = RoomTerminalSideLength * 3.28;
                }
                // calculating the hydraulic area of the terminal
                double hydraulicArea = Math.Pow(((4 * RoomTerminalSideLength * RoomTerminalSideLength) / 3.142), 1 / 2);
                return hydraulicArea;
            }

            #region findingDistances

            // finding commonly used distances from database

            String queryXCoordListener = "SELECT XCoordListener FROM tblProject WHERE ProjectName=@ProjectName";
            SqlCommand sqlCmd6 = new SqlCommand(queryXCoordListener, sqlCon);
            sqlCmd6.CommandType = CommandType.Text;
            sqlCmd6.Parameters.AddWithValue("@ProjectName", txtProjectName.Text);
            string XCoordListenerstr = Convert.ToString(sqlCmd6.ExecuteScalar());
            double XCoordListener = Convert.ToDouble(XCoordListenerstr);

            String queryYCoordListener = "SELECT YCoordListener FROM tblProject WHERE ProjectName=@ProjectName";
            SqlCommand sqlCmd7 = new SqlCommand(queryYCoordListener, sqlCon);
            sqlCmd7.CommandType = CommandType.Text;
            sqlCmd7.Parameters.AddWithValue("@ProjectName", txtProjectName.Text);
            string YCoordListenerstr = Convert.ToString(sqlCmd7.ExecuteScalar());
            double YCoordListener = Convert.ToDouble(YCoordListenerstr);

            String queryRoomLength = "SELECT RoomLength FROM tblProject WHERE ProjectName=@ProjectName";
            SqlCommand sqlCmd8 = new SqlCommand(queryRoomLength, sqlCon);
            sqlCmd8.CommandType = CommandType.Text;
            sqlCmd8.Parameters.AddWithValue("@ProjectName", txtProjectName.Text);
            double RoomLength = Convert.ToDouble(sqlCmd8.ExecuteScalar());

            String queryRoomWidth = "SELECT RoomWidth FROM tblProject WHERE ProjectName=@ProjectName";
            SqlCommand sqlCmd9 = new SqlCommand(queryRoomWidth, sqlCon);
            sqlCmd9.CommandType = CommandType.Text;
            sqlCmd9.Parameters.AddWithValue("@ProjectName", txtProjectName.Text);
            double RoomWidth = Convert.ToDouble(sqlCmd9.ExecuteScalar());

            double FindRoomVolume()
            {     
                String queryRoomHeight = "SELECT RoomHeight FROM tblProject WHERE ProjectName=@ProjectName";
                SqlCommand sqlCmd10 = new SqlCommand(queryRoomHeight, sqlCon);
                sqlCmd10.CommandType = CommandType.Text;
                sqlCmd10.Parameters.AddWithValue("@ProjectName", txtProjectName.Text);
                double RoomHeight = Convert.ToDouble(sqlCmd10.ExecuteScalar());

                // converting units if specified by user
                if (Units == "Feet")
                {
                    RoomLength = RoomLength * 3.28;
                    RoomWidth = RoomWidth * 3.28;
                }
                double RoomVolume = RoomLength * RoomWidth * RoomHeight;
                return RoomVolume;
            }

            double FindDistancefromFantoListener()
            {
                // finding necessary variables from the database
                String queryXCoordFan = "SELECT XCoordFan FROM tblProject WHERE ProjectName=@ProjectName";
                SqlCommand sqlCmd4 = new SqlCommand(queryXCoordFan, sqlCon);
                sqlCmd4.CommandType = CommandType.Text;
                sqlCmd4.Parameters.AddWithValue("@ProjectName", txtProjectName.Text);
                string XCoordFanstr = Convert.ToString(sqlCmd4.ExecuteScalar());
                double XCoordFan = Convert.ToDouble(XCoordFanstr);

                String queryYCoordFan = "SELECT YCoordFan FROM tblProject WHERE ProjectName=@ProjectName";
                SqlCommand sqlCmd5 = new SqlCommand(queryYCoordFan, sqlCon);
                sqlCmd5.CommandType = CommandType.Text;
                sqlCmd5.Parameters.AddWithValue("@ProjectName", txtProjectName.Text);
                string YCoordFanstr = Convert.ToString(sqlCmd5.ExecuteScalar());
                double YCoordFan = Convert.ToDouble(YCoordFanstr);                

                if (Units == "Feet")
                {
                    RoomLength = RoomLength * 3.28;
                    RoomWidth = RoomWidth * 3.28;
                }

                double FanToListener = calculatingDistancestoListener(XCoordFan, YCoordFan);
                return FanToListener;
            }

            double FindDistancefromSupplytoListener()
            {
                // finding necessary variables from the database
                String queryXCoordS = "SELECT XCoordSupply FROM tblProject WHERE ProjectName=@ProjectName";
                SqlCommand sqlCmd4 = new SqlCommand(queryXCoordS, sqlCon);
                sqlCmd4.CommandType = CommandType.Text;
                sqlCmd4.Parameters.AddWithValue("@ProjectName", txtProjectName.Text);
                string XCoordSstr = Convert.ToString(sqlCmd4.ExecuteScalar());
                double XCoordS = Convert.ToDouble(XCoordSstr);

                String queryYCoordS = "SELECT YCoordSupply FROM tblProject WHERE ProjectName=@ProjectName";
                SqlCommand sqlCmd5 = new SqlCommand(queryYCoordS, sqlCon);
                sqlCmd5.CommandType = CommandType.Text;
                sqlCmd5.Parameters.AddWithValue("@ProjectName", txtProjectName.Text);
                string YCoordSstr = Convert.ToString(sqlCmd5.ExecuteScalar());
                double YCoordS = Convert.ToDouble(YCoordSstr);

                if (Units == "Feet")
                {
                    RoomLength = RoomLength * 3.28;
                    RoomWidth = RoomWidth * 3.28;
                }

                double SupplyToListener = calculatingDistancestoListener(XCoordS, YCoordS);
                return SupplyToListener;
            }

            double FindDistancefromExtracttoListener()
            {
                // finding necessary variables from the database
                String queryXCoordE = "SELECT XCoordExtract FROM tblProject WHERE ProjectName=@ProjectName";
                SqlCommand sqlCmd4 = new SqlCommand(queryXCoordE, sqlCon);
                sqlCmd4.CommandType = CommandType.Text;
                sqlCmd4.Parameters.AddWithValue("@ProjectName", txtProjectName.Text);
                string XCoordEstr = Convert.ToString(sqlCmd4.ExecuteScalar());
                double XCoordE = Convert.ToDouble(XCoordEstr);

                String queryYCoordE = "SELECT YCoordExtract FROM tblProject WHERE ProjectName=@ProjectName";
                SqlCommand sqlCmd5 = new SqlCommand(queryYCoordE, sqlCon);
                sqlCmd5.CommandType = CommandType.Text;
                sqlCmd5.Parameters.AddWithValue("@ProjectName", txtProjectName.Text);
                string YCoordEstr = Convert.ToString(sqlCmd5.ExecuteScalar());
                double YCoordE = Convert.ToDouble(YCoordEstr);

                if (Units == "Feet")
                {
                    RoomLength = RoomLength * 3.28;
                    RoomWidth = RoomWidth * 3.28;
                }

                double ExtractToListener = calculatingDistancestoListener(XCoordE, YCoordE);
                return ExtractToListener;
            }

            double calculatingDistancestoListener(double XCoord, double YCoord)
            {
                // calculating difference between x and y coordinates of the fan and the listener
                double Xdifference = XCoord - XCoordListener;
                double AbsXdifference = Math.Abs(Xdifference);
                double Ydifference = YCoord - YCoordListener;
                double AbsYdifference = Math.Abs(Ydifference);

                // calculating distance in metres represented by a pixel on the canvas
                double Xpixel = RoomLength / 600;
                double Ypixel = RoomWidth / 320;

                // calculating horizontal and vertical distances in metres
                double horizontalDistance = Xpixel * AbsXdifference;
                double verticalDistance = Ypixel * AbsYdifference;

                // using Pythagorus' theorem to calculate diagonal distance between fan and listener
                double totalDistancesquared = (horizontalDistance * horizontalDistance) + (verticalDistance * verticalDistance);
                double totalDistance = Math.Pow(totalDistancesquared, 0.5);
                return totalDistance;
            }
            #endregion

            double FindingNRValueOfRoom()
            {
                String queryNRVal = "SELECT NRVal FROM tblTypeOfRoom INNER JOIN tblProject ON tblProject.TypeOfRoom=tblTypeOfRoom.TypeName WHERE tblProject.ProjectName=@ProjectName";
                SqlCommand sqlCmd10 = new SqlCommand(queryNRVal, sqlCon);
                sqlCmd10.CommandType = CommandType.Text;
                sqlCmd10.Parameters.AddWithValue("@ProjectName", txtProjectName.Text);
                double NRVal = Convert.ToDouble(sqlCmd10.ExecuteScalar());
                return NRVal;
            }

            #region findingNewFan
            String query3 = "SELECT FanCode FROM tblProject WHERE ProjectName=@ProjectName";
            SqlCommand sqlCmd3 = new SqlCommand(query3, sqlCon);
            sqlCmd3.CommandType = CommandType.Text;
            sqlCmd3.Parameters.AddWithValue("@ProjectName", txtProjectName.Text);
            string FanCode = Convert.ToString(sqlCmd3.ExecuteScalar());

            // This determines whether or not each individual fan is suitable for the given project.
            // This is regardless of which fan the user intends to use.
            string suitable15 = SuitabilityXBC15();
            string suitable25 = SuitabilityXBC25();
            string suitable45 = SuitabilityXBC45();
            string suitable55 = SuitabilityXBC55();
            string suitable65 = SuitabilityXBC65();

            string actualSuitability = actualsuitability();

            string actualnewfancode = actualnewFanCode();

            // This finds the suitability of the fan that the user originally chose.
            string actualsuitability()
            {
                string suitability;
                if ((FanCode == "XBC15") && (suitable15 == "Yes"))
                {
                    suitability = "Yes";
                    return suitability;
                }
                else if ((FanCode == "XBC25") && (suitable25 == "Yes"))
                {
                    suitability = "Yes";
                    return suitability;
                }
                else if ((FanCode == "XBC45") && (suitable45 == "Yes"))
                {
                    suitability = "Yes";
                    return suitability;
                }
                else if ((FanCode == "XBC55") && (suitable55 == "Yes"))
                {
                    suitability = "Yes";
                    return suitability;
                }
                else if ((FanCode == "XBC65") && (suitable65 == "Yes"))
                {
                    suitability = "Yes";
                    return suitability;
                }
                else
                {
                    suitability = "No";
                    return suitability;
                }
            }

            // If the fan the user chose is not suitable, then find the 'new fan code/codes' which would be suitabile.
            string actualnewFanCode()
            {
                if (suitable15 == "No" && suitable25 == "No" && suitable45 == "No" && suitable55 == "No" && suitable65 == "No")
                {
                    newFanCode = "There is no fan in the XBC range that is suitable for your room.";
                }
                else if ((FanCode == "XBC15") && (actualSuitability == "No"))
                {
                    newFanCode = FindNewFanCode15();
                }
                else if ((FanCode == "XBC25") && (actualSuitability == "No"))
                {
                    newFanCode = FindNewFanCode25();
                }
                else if ((FanCode == "XBC45") && (actualSuitability == "No"))
                {
                    newFanCode = FindNewFanCode45();
                }
                else if ((FanCode == "XBC55") && (actualSuitability == "No"))
                {
                    newFanCode = findNewFanCode55();
                }
                else if ((FanCode == "XBC65") && (actualSuitability == "No"))
                {
                    newFanCode = findNewFanCode65();
                }
                else
                {
                    newFanCode = "N/A";
                }
                return newFanCode;
            }
            
            string FindNewFanCode15()
            {
                if (suitable25 == "Yes" && suitable45 == "Yes" && suitable55 == "Yes" && suitable65 == "Yes")
                {
                    newFanCode = "XBC25, XBC45, XBC55, XBC65";
                }
                else if (suitable25 == "No" && suitable45 == "Yes" && suitable55 == "Yes" && suitable65 == "Yes")
                {
                    newFanCode = "XBC45, XBC55, XBC65";
                }
                else if (suitable25 == "Yes" && suitable45 == "No" && suitable55 == "Yes" && suitable65 == "Yes")
                {
                    newFanCode = "XBC25, XBC55, XBC65";
                }
                else if (suitable25 == "Yes" && suitable45 == "Yes" && suitable55 == "No" && suitable65 == "Yes")
                {
                    newFanCode = "XBC25, XBC45, XBC65";
                }
                else if (suitable25 == "Yes" && suitable45 == "Yes" && suitable55 == "Yes" && suitable65 == "No")
                {
                    newFanCode = "XBC25, XBC45, XBC55";
                }
                else if (suitable25 == "Yes" && suitable45 == "Yes" && suitable55 == "No" && suitable65 == "No")
                {
                    newFanCode = "XBC25, XBC45";
                }
                else if (suitable25 == "Yes" && suitable45 == "No" && suitable55 == "Yes" && suitable65 == "No")
                {
                    newFanCode = "XBC25, XBC55";
                }
                else if (suitable25 == "Yes" && suitable45 == "No" && suitable55 == "No" && suitable65 == "Yes")
                {
                    newFanCode = "XBC25, XBC65";
                }
                else if (suitable25 == "No" && suitable45 == "Yes" && suitable55 == "No" && suitable65 == "Yes")
                {
                    newFanCode = "XBC45, XBC65";
                }
                else if (suitable25 == "No" && suitable45 == "No" && suitable55 == "Yes" && suitable65 == "Yes")
                {
                    newFanCode = "XBC55, XBC65";
                }
                else if (suitable25 == "Yes" && suitable45 == "No" && suitable55 == "No" && suitable65 == "No")
                {
                    newFanCode = "XBC25";
                }
                else if (suitable25 == "No" && suitable45 == "Yes" && suitable55 == "No" && suitable65 == "No")
                {
                    newFanCode = "XBC45";
                }
                else if (suitable25 == "No" && suitable45 == "No" && suitable55 == "Yes" && suitable65 == "No")
                {
                    newFanCode = "XBC55";
                }
                else if (suitable25 == "No" && suitable45 == "No" && suitable55 == "No" && suitable65 == "Yes")
                {
                    newFanCode = "XBC65";
                }
                return newFanCode;
            }
            string FindNewFanCode25()
            {
                if (suitable15 == "Yes" && suitable45 == "Yes" && suitable55 == "Yes" && suitable65 == "Yes")
                {
                    newFanCode = "XBC15, XBC45, XBC55, XBC65";
                }
                else if (suitable15 == "No" && suitable45 == "Yes" && suitable55 == "Yes" && suitable65 == "Yes")
                {
                    newFanCode = "XBC45, XBC55, XBC65";
                }
                else if (suitable15 == "Yes" && suitable45 == "No" && suitable55 == "Yes" && suitable65 == "Yes")
                {
                    newFanCode = "XBC15, XBC55, XBC65";
                }
                else if (suitable15 == "Yes" && suitable45 == "Yes" && suitable55 == "No" && suitable65 == "Yes")
                {
                    newFanCode = "XBC15, XBC45, XBC65";
                }
                else if (suitable15 == "Yes" && suitable45 == "Yes" && suitable55 == "Yes" && suitable65 == "No")
                {
                    newFanCode = "XBC15, XBC45, XBC55";
                }
                else if (suitable15 == "Yes" && suitable45 == "Yes" && suitable55 == "No" && suitable65 == "No")
                {
                    newFanCode = "XBC15, XBC45";
                }
                else if (suitable15 == "Yes" && suitable45 == "No" && suitable55 == "Yes" && suitable65 == "No")
                {
                    newFanCode = "XBC15, XBC55";
                }
                else if (suitable15 == "Yes" && suitable45 == "No" && suitable55 == "No" && suitable65 == "Yes")
                {
                    newFanCode = "XBC15, XBC65";
                }
                else if (suitable15 == "No" && suitable45 == "Yes" && suitable55 == "No" && suitable65 == "Yes")
                {
                    newFanCode = "XBC45, XBC65";
                }
                else if (suitable15 == "No" && suitable45 == "No" && suitable55 == "Yes" && suitable65 == "Yes")
                {
                    newFanCode = "XBC55, XBC65";
                }
                else if (suitable15 == "Yes" && suitable45 == "No" && suitable55 == "No" && suitable65 == "No")
                {
                    newFanCode = "XBC15";
                }
                else if (suitable15 == "No" && suitable45 == "Yes" && suitable55 == "No" && suitable65 == "No")
                {
                    newFanCode = "XBC45";
                }
                else if (suitable15 == "No" && suitable45 == "No" && suitable55 == "Yes" && suitable65 == "No")
                {
                    newFanCode = "XBC55";
                }
                else if (suitable15 == "No" && suitable45 == "No" && suitable55 == "No" && suitable65 == "Yes")
                {
                    newFanCode = "XBC65";
                }
                return newFanCode;
            }
            string FindNewFanCode45()
            {
                if (suitable15 == "Yes" && suitable25 == "Yes" && suitable55 == "Yes" && suitable65 == "Yes")
                {
                    newFanCode = "XBC15, XBC25, XBC55, XBC65";
                }
                else if (suitable15 == "No" && suitable25 == "Yes" && suitable55 == "Yes" && suitable65 == "Yes")
                {
                    newFanCode = "XBC25, XBC55, XBC65";
                }
                else if (suitable15 == "Yes" && suitable25 == "No" && suitable55 == "Yes" && suitable65 == "Yes")
                {
                    newFanCode = "XBC15, XBC55, XBC65";
                }
                else if (suitable15 == "Yes" && suitable25 == "Yes" && suitable55 == "No" && suitable65 == "Yes")
                {
                    newFanCode = "XBC15, XBC25, XBC65";
                }
                else if (suitable15 == "Yes" && suitable25 == "Yes" && suitable55 == "Yes" && suitable65 == "No")
                {
                    newFanCode = "XBC15, XBC25, XBC55";
                }
                else if (suitable15 == "Yes" && suitable25 == "Yes" && suitable55 == "No" && suitable65 == "No")
                {
                    newFanCode = "XBC15, XBC25";
                }
                else if (suitable15 == "Yes" && suitable25 == "No" && suitable55 == "Yes" && suitable65 == "No")
                {
                    newFanCode = "XBC15, XBC55";
                }
                else if (suitable15 == "Yes" && suitable25 == "No" && suitable55 == "No" && suitable65 == "Yes")
                {
                    newFanCode = "XBC15, XBC65";
                }
                else if (suitable15 == "No" && suitable25 == "Yes" && suitable55 == "No" && suitable65 == "Yes")
                {
                    newFanCode = "XBC25, XBC65";
                }
                else if (suitable15 == "No" && suitable25 == "No" && suitable55 == "Yes" && suitable65 == "Yes")
                {
                    newFanCode = "XBC55, XBC65";
                }
                else if (suitable15 == "Yes" && suitable25 == "No" && suitable55 == "No" && suitable65 == "No")
                {
                    newFanCode = "XBC15";
                }
                else if (suitable15 == "No" && suitable25 == "Yes" && suitable55 == "No" && suitable65 == "No")
                {
                    newFanCode = "XBC25";
                }
                else if (suitable15 == "No" && suitable25 == "No" && suitable55 == "Yes" && suitable65 == "No")
                {
                    newFanCode = "XBC55";
                }
                else if (suitable15 == "No" && suitable25 == "No" && suitable55 == "No" && suitable65 == "Yes")
                {
                    newFanCode = "XBC65";
                }
                return newFanCode;
            }
            string findNewFanCode55()
            {
                if (suitable15 == "Yes" && suitable25 == "Yes" && suitable45 == "Yes" && suitable65 == "Yes")
                {
                    newFanCode = "XBC15, XBC25, XBC45, XBC65";
                }
                else if (suitable15 == "No" && suitable25 == "Yes" && suitable45 == "Yes" && suitable65 == "Yes")
                {
                    newFanCode = "XBC25, XBC45, XBC65";
                }
                else if (suitable15 == "Yes" && suitable25 == "No" && suitable45 == "Yes" && suitable65 == "Yes")
                {
                    newFanCode = "XBC15, XBC45, XBC65";
                }
                else if (suitable15 == "Yes" && suitable25 == "Yes" && suitable45 == "No" && suitable65 == "Yes")
                {
                    newFanCode = "XBC15, XBC25, XBC65";
                }
                else if (suitable15 == "Yes" && suitable25 == "Yes" && suitable45 == "Yes" && suitable65 == "No")
                {
                    newFanCode = "XBC15, XBC25, XBC45";
                }
                else if (suitable15 == "Yes" && suitable25 == "Yes" && suitable45 == "No" && suitable65 == "No")
                {
                    newFanCode = "XBC15, XBC25";
                }
                else if (suitable15 == "Yes" && suitable25 == "No" && suitable45 == "Yes" && suitable65 == "No")
                {
                    newFanCode = "XBC15, XBC45";
                }
                else if (suitable15 == "Yes" && suitable25 == "No" && suitable45 == "No" && suitable65 == "Yes")
                {
                    newFanCode = "XBC15, XBC65";
                }
                else if (suitable15 == "No" && suitable25 == "Yes" && suitable45 == "No" && suitable65 == "Yes")
                {
                    newFanCode = "XBC25, XBC65";
                }
                else if (suitable15 == "No" && suitable25 == "No" && suitable45 == "Yes" && suitable65 == "Yes")
                {
                    newFanCode = "XBC45, XBC65";
                }
                else if (suitable15 == "Yes" && suitable25 == "No" && suitable45 == "No" && suitable65 == "No")
                {
                    newFanCode = "XBC15";
                }
                else if (suitable15 == "No" && suitable25 == "Yes" && suitable45 == "No" && suitable65 == "No")
                {
                    newFanCode = "XBC25";
                }
                else if (suitable15 == "No" && suitable25 == "No" && suitable45 == "Yes" && suitable65 == "No")
                {
                    newFanCode = "XBC45";
                }
                else if (suitable15 == "No" && suitable25 == "No" && suitable45 == "No" && suitable65 == "Yes")
                {
                    newFanCode = "XBC65";
                }
                return newFanCode;
            }
            string findNewFanCode65()
            {
                if (suitable15 == "Yes" && suitable25 == "Yes" && suitable45 == "Yes" && suitable55 == "Yes")
                {
                    newFanCode = "XBC15, XBC25, XBC45, XBC55";
                }
                else if (suitable15 == "No" && suitable25 == "Yes" && suitable45 == "Yes" && suitable55 == "Yes")
                {
                    newFanCode = "XBC25, XBC45, XBC55";
                }
                else if (suitable15 == "Yes" && suitable25 == "No" && suitable45 == "Yes" && suitable55 == "Yes")
                {
                    newFanCode = "XBC15, XBC45, XBC55";
                }
                else if (suitable15 == "Yes" && suitable25 == "Yes" && suitable45 == "No" && suitable55 == "Yes")
                {
                    newFanCode = "XBC15, XBC25, XBC55";
                }
                else if (suitable15 == "Yes" && suitable25 == "Yes" && suitable45 == "Yes" && suitable55 == "No")
                {
                    newFanCode = "XBC15, XBC25, XBC45";
                }
                else if (suitable15 == "Yes" && suitable25 == "Yes" && suitable45 == "No" && suitable55 == "No")
                {
                    newFanCode = "XBC15, XBC25";
                }
                else if (suitable15 == "Yes" && suitable25 == "No" && suitable45 == "Yes" && suitable55 == "No")
                {
                    newFanCode = "XBC15, XBC45";
                }
                else if (suitable15 == "Yes" && suitable25 == "No" && suitable45 == "No" && suitable55 == "Yes")
                {
                    newFanCode = "XBC15, XBC55";
                }
                else if (suitable15 == "No" && suitable25 == "Yes" && suitable45 == "No" && suitable55 == "Yes")
                {
                    newFanCode = "XBC25, XBC55";
                }
                else if (suitable15 == "No" && suitable25 == "No" && suitable45 == "Yes" && suitable55 == "Yes")
                {
                    newFanCode = "XBC45, XBC55";
                }
                else if (suitable15 == "Yes" && suitable25 == "No" && suitable45 == "No" && suitable55 == "No")
                {
                    newFanCode = "XBC15";
                }
                else if (suitable15 == "No" && suitable25 == "Yes" && suitable45 == "No" && suitable55 == "No")
                {
                    newFanCode = "XBC25";
                }
                else if (suitable15 == "No" && suitable25 == "No" && suitable45 == "Yes" && suitable55 == "No")
                {
                    newFanCode = "XBC45";
                }
                else if (suitable15 == "No" && suitable25 == "No" && suitable45 == "No" && suitable55 == "Yes")
                {
                    newFanCode = "XBC55";
                }
                return newFanCode;
            }

            #endregion

            // Finds possible new attenuation for the project.
            #region findingNewAttenuation
            string newSupply = "0";
            string newExtract = "0";
            string newBarrier = "0";
            if (actualSuitability == "No")
            {
                newSupply = findNewSupply();
                newExtract = findNewExtract();
                newBarrier = findNewBarrier();
            }
            string findNewSupply()
            {
                string newsupply;
                
                string supply = FindSupplyAttenuationType();
                if (supply == "Short")
                {
                    newsupply = "Medium, Long";
                }
                else if (supply == "Medium")
                {
                    newsupply = "Long";
                }
                else
                {
                    newsupply = "There is no supply attenuator type suitable.";
                }                
                return newsupply;
            }
            string findNewExtract()
            {
                string newextract;

                string extract = FindExtractAttenuationType();
                if (extract == "Short")
                {
                    newextract = "Medium, Long";
                }
                else if (extract == "Medium")
                {
                    newextract = "Long";
                }
                else
                {
                    newextract = "There is no supply attenuator type suitable.";
                }
                return newextract;
            }
            string findNewBarrier()
            {
                string newbarrier;

                string barrier = FindBreakoutBarrierType();
                if (barrier == "Basic")
                {
                    newbarrier = "Acoustic";
                }                
                else
                {
                    newbarrier = "There is no supply attenuator type suitable.";
                }
                return newbarrier;
            }

            #endregion

            #region textfile
            
            // creating, writing to and reading from a summary text file of the model
            if (actualSuitability == "No")
            {
                try
                {
                    String path = @"C:\Users\peybi\Downloads\" + txtProjectName.Text + "Summary.txt";
                    StreamWriter file = new StreamWriter(path);
                    file.Write("SUMMARY FAN DATA SHEET\r\n");
                    file.Write(" \r\n");
                    file.Write("Technical Data\r\n");
                    file.Write("Fan code: " + FanCode + "\r\n");
                    file.Write("Suitable? No" + "\r\n");
                    file.Write(" \r\n");
                    file.Write("Changes:" + "\r\n");
                    file.Write("Suggested new fan code: " + actualnewfancode + "\r\n");
                    file.Write("Supply attenuator type: " + newSupply + "\r\n");
                    file.Write("Extract attenuator type: " + newExtract + "\r\n");
                    file.Write("Acoustic barrier type: " + newBarrier + "\r\n");

                    file.Close();

                    using (StreamReader sr = new StreamReader(path))
                    {
                        string filetext = sr.ReadToEnd();
                        txtSummary.Text = filetext;
                    }

                    file.Close();
                    File.Delete(path);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else if (actualSuitability == "Yes")
            {
                try
                {
                    String path = @"C:\Users\peybi\Downloads\" + txtProjectName.Text + "Summary.txt";
                    StreamWriter file = new StreamWriter(path);
                    file.Write("SUMMARY FAN DATA SHEET\r\n");
                    file.Write(" \r\n");
                    file.Write("Technical Data\r\n");
                    file.Write("Fan code: " + FanCode + "\r\n");
                    file.Write("Suitable? Yes" + "\r\n");
                    file.Write(" \r\n");
                    file.Write("Changes: N/A" + "\r\n");
                    file.Write("Suggested new fan code: N/A" + "\r\n");
                    file.Write("Supply attenuator type: N/A" + "\r\n");
                    file.Write("Extract attenuator type: N/A" + "\r\n");
                    file.Write("Acoustic barrier type: N/A" + "\r\n");

                    file.Close();

                    using (StreamReader sr = new StreamReader(path))
                    {
                        string filetext = sr.ReadToEnd();
                        txtSummary.Text = filetext;
                    }

                    file.Close();
                    File.Delete(path);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            #endregion
        }

        private void btnGraduations_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Graduations());
        }
    }
}
