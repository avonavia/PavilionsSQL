using System.Collections.Generic;
using System.Windows;
using System.Data.SqlClient;
using System.Data;

namespace PavillionsSQL
{
    public partial class PavillionListWindow : Window
    {
        public static string conString { get; set; }
        public static int selectedID { get; set; }
        public static Pavillion selectedPav { get; set; }
        public static int EmpID { get; set; }
        public static bool Changed { get; set; }
        public PavillionListWindow()
        {
            InitializeComponent();
            GetShopNames();
            GetPavStatuses();
            HideStatusChange();
        }

        public void HideStatusChange()
        {
            statusLabel.Visibility = Visibility.Hidden;
            StatusBox.Visibility = Visibility.Hidden;
            status_apply_button.Visibility = Visibility.Hidden;
        }

        public void ShowStatusChange()
        {
            statusLabel.Visibility = Visibility.Visible;
            StatusBox.Visibility = Visibility.Visible;
            status_apply_button.Visibility = Visibility.Visible;
        }

        List<Pavillion> pavlist = new List<Pavillion>();
        List<string> shoplist = new List<string>();
        List<string> statuslist = new List<string>();

        public void GetShopNames()
        {
            pavlist.Clear();
            string cmdString = "GetShopNames";

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(cmdString, con);

                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    shoplist.Add(reader[0].ToString());
                }
                reader.Close();
                ShopBox.ItemsSource = shoplist;
            }
        }

        public int GetShopIDByName()
        {
            pavlist.Clear();
            string cmdString = "GetShopIDByName";

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(cmdString, con);

                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter Param = new SqlParameter
                {
                    ParameterName = "@name",
                    Value = ShopBox.SelectedItem.ToString()
                };
                cmd.Parameters.Add(Param);

                con.Open();

                int result = (int)cmd.ExecuteScalar();

                con.Close();

                return result;
            }
        }

        public int GetPavStatusID()
        {
            pavlist.Clear();
            string cmdString = "GetPavStatusID";

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(cmdString, con);

                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter Param = new SqlParameter
                {
                    ParameterName = "@name",
                    Value = StatusBox.SelectedItem.ToString()
                };
                cmd.Parameters.Add(Param);

                con.Open();

                int result = (int)cmd.ExecuteScalar();

                con.Close();

                return result;
            }
        }

        public void ChangePavStatus()
        {
            string cmdString = "ChangePavStatus";

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(cmdString, con);

                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter Param = new SqlParameter
                {
                    ParameterName = "@pavID",
                    Value = selectedPav.PavID
                };
                cmd.Parameters.Add(Param);

                SqlParameter Param2 = new SqlParameter
                {
                    ParameterName = "@statusID",
                    Value = GetPavStatusID()
                };
                cmd.Parameters.Add(Param2);

                con.Open();

                cmd.ExecuteNonQuery();

                con.Close();
            }
        }
        public void GetPavList()
        {
            PavGrid.ItemsSource = null;
            pavlist.Clear();
            string cmdString = "GetPavList";

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(cmdString, con);

                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter idParam = new SqlParameter
                {
                    ParameterName = "@id",
                    Value = selectedID
                };
                cmd.Parameters.Add(idParam);

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Pavillion st = new Pavillion();
                    st.PavID = (int)reader[0];
                    st.ShopName = reader[1].ToString();
                    st.PavNO = reader[2].ToString();
                    st.Floor = (int)reader[3];
                    st.PavStatus = reader[4].ToString();
                    st.Square = reader[5].ToString();
                    st.Cost = reader[6].ToString();
                    st.Factor = reader[7].ToString();
                    pavlist.Add(st);
                }
                reader.Close();
                PavGrid.ItemsSource = pavlist;
            }
        }

        public void GetPavStatuses()
        {
            StatusBox.ItemsSource = null;
            statuslist.Clear();
            string cmdString = "GetPavStatuses";

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(cmdString, con);

                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    statuslist.Add(reader[0].ToString());
                }
                reader.Close();
                StatusBox.ItemsSource = statuslist;
            }
        }
        private void back_button_Click(object sender, RoutedEventArgs e)
        {
            EmployeeWorkspaceWindow employeeWorkspaceWindow = new EmployeeWorkspaceWindow();
            employeeWorkspaceWindow.Show();
            Close();
        }

        private void ShopBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            selectedID = GetShopIDByName();
            GetPavList();
        }

        private void PavGrid_SelectedCellsChanged(object sender, System.Windows.Controls.SelectedCellsChangedEventArgs e)
        {
            Pavillion item = new Pavillion();
            foreach (var obj in PavGrid.SelectedItems)
            {
                item = obj as Pavillion;
                selectedPav = item;
            }
        }

        private void book_button_Click(object sender, RoutedEventArgs e)
        {
            if (PavGrid.SelectedItem != null)
            {
                if (selectedPav.PavStatus == "Забронирован")
                {
                    MessageBox.Show("Павильон занят");
                }
                else
                {
                    StatusBox.SelectedItem = "Забронирован";
                    BookingRedactAddWindow.ShopID = selectedID;
                    BookingRedactAddWindow.PavID = selectedPav.PavID;
                    BookingRedactAddWindow bookingRedactAddWindow = new BookingRedactAddWindow();
                    bookingRedactAddWindow.Show();
                    Close();
                    if (Changed)
                    {
                        ChangePavStatus();
                    }
                    GetPavList();
                }
            }
            else
            {
                MessageBox.Show("Павильон не выбран");
            }
        }

        private void status_change_button_Click(object sender, RoutedEventArgs e)
        {
            if (PavGrid.SelectedItem != null)
            {
                ShowStatusChange();
            }
            else
            {
                MessageBox.Show("Павильон не выбран");
            }
        }

        private void status_apply_button_Click(object sender, RoutedEventArgs e)
        {
            if (StatusBox.SelectedItem != null)
            {
                ChangePavStatus();
                GetPavList();
                HideStatusChange();
            }
            else
            {
                MessageBox.Show("Статус не выбран");
            }
        }

        private void booklist_button_Click(object sender, RoutedEventArgs e)
        {
            if (ShopBox.SelectedItem != null)
            {
                BookingListWindow.ShopID = GetShopIDByName();
                BookingListWindow bookingListWindow = new BookingListWindow();
                bookingListWindow.Show();
                Close();
            }
            else
            {
                MessageBox.Show("ТЦ не выбран");
            }
        }
    }
}
