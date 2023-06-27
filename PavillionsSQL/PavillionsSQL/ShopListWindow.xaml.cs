using System.Windows;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace PavillionsSQL
{
    public partial class ShopListWindow : Window
    {
        public static string conString { get; set; }
        public static string selectedCity { get; set; }
        public static string selectedStatus { get; set; }
        public ShopListWindow()
        {
            InitializeComponent();
            GetShops();
            GetStatusList();
            GetCityList();
            ShopRedactAddWindow.slist = shoplist;
            ShopRedactAddWindow.citylist = citylist;
            ShopRedactAddWindow.statuslist = statuslist;
        }

        List<Shop> shoplist = new List<Shop>();
        List<Shop> searchlist = new List<Shop>();
        List<Shop> sortlist = new List<Shop>();

        List<string> citylist = new List<string>();
        List<string> statuslist = new List<string>();

        public void GetStatusList()
        {
            statuslist.Clear();
            StatusBox.ItemsSource = null;

            string cmdString = "GetStatusList";

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

        public void GetCityList()
        {
            citylist.Clear();
            CityBox.ItemsSource = null;

            string cmdString = "GetCityList";

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(cmdString, con);

                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    citylist.Add(reader[0].ToString());
                }
                reader.Close();
                CityBox.ItemsSource = citylist;
            }
        }

        private void SearchBar_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (sortlist.Count == 0)
            {
                if (SearchBar.Text != null)
                {
                    searchlist.Clear();
                    foreach (var item in shoplist)
                    {
                        if (item.ShopName.ToLower().Contains(SearchBar.Text.ToLower()))
                        {
                            searchlist.Add(item);
                        }
                        ShopListBox.ItemsSource = null;
                        ShopListBox.ItemsSource = searchlist;
                    }
                }
                else
                {
                    searchlist.Clear();
                    ShopListBox.ItemsSource = null;
                    ShopListBox.ItemsSource = shoplist;
                }
            }
            else
            {
                if (SearchBar.Text != null)
                {
                    searchlist.Clear();
                    foreach (var item in sortlist)
                    {
                        if (item.ShopName.ToLower().Contains(SearchBar.Text.ToLower()))
                        {
                            searchlist.Add(item);
                        }
                        ShopListBox.ItemsSource = null;
                        ShopListBox.ItemsSource = searchlist;
                    }
                }
                else
                {
                    searchlist.Clear();
                    ShopListBox.ItemsSource = null;
                    ShopListBox.ItemsSource = sortlist;
                }
            }
        }

        public void GetShops()
        {
            shoplist.Clear();
            string cmdString = "GetShops";

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(cmdString, con);

                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Shop st = new Shop();
                    st.ShopID = (int)reader[0];
                    st.ShopName = reader[1].ToString();
                    st.ShopStatus = reader[2].ToString();
                    st.PavCount = (int)reader[3];
                    st.City = reader[4].ToString();
                    st.ShopCost = reader[5].ToString();
                    st.Factor = reader[6].ToString();
                    st.FloorCount = (int)reader[7];
                    st.Photo = ImageWorker.ShowImage(conString, "Photo", "Shops", "ShopID", st.ShopID.ToString());
                    shoplist.Add(st);
                }
                reader.Close();
                ShopListBox.ItemsSource = shoplist;
            }
        }

        private void back_button_Click(object sender, RoutedEventArgs e)
        {
            EmployeeWorkspaceWindow employeeWorkspaceWindow = new EmployeeWorkspaceWindow();
            employeeWorkspaceWindow.Show();
            Close();
        }

        public void Sort()
        {
            if (CityBox.SelectedItem != null && StatusBox.SelectedItem == null)
            {
                sortlist.Clear();
                foreach (var item in shoplist)
                {
                    if (item.City.Contains(selectedCity))
                    {
                        sortlist.Add(item);
                    }
                    ShopListBox.ItemsSource = null;
                    ShopListBox.ItemsSource = sortlist;
                }
            }
            if (CityBox.SelectedItem == null && StatusBox.SelectedItem != null)
            {
                sortlist.Clear();
                foreach (var item in shoplist)
                {
                    if (item.ShopStatus.Contains(selectedStatus))
                    {
                        sortlist.Add(item);
                    }
                    ShopListBox.ItemsSource = null;
                    ShopListBox.ItemsSource = sortlist;
                }
            }
            if (CityBox.SelectedItem != null && StatusBox.SelectedItem != null)
            {
                sortlist.Clear();
                foreach (var item in shoplist)
                {
                    if (item.City.Contains(selectedCity) && item.ShopStatus.Contains(selectedStatus))
                    {
                        sortlist.Add(item);
                    }
                    ShopListBox.ItemsSource = null;
                    ShopListBox.ItemsSource = sortlist;
                }
            }
        }

        private void CityBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (CityBox.SelectedItem != null)
            {
                selectedCity = CityBox.SelectedItem.ToString();
                Sort();
            }
        }

        private void StatusBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (StatusBox.SelectedItem != null)
            {
                selectedStatus = StatusBox.SelectedItem.ToString();
                Sort();
            }
        }

        private void RemoveFiltersButton_Click(object sender, RoutedEventArgs e)
        {
            sortlist.Clear();
            CityBox.SelectedItem = null;
            StatusBox.SelectedItem = null;
            selectedStatus = string.Empty;
            selectedCity = string.Empty;
            ShopListBox.ItemsSource = null;
            ShopListBox.ItemsSource = shoplist;
        }

        private void ShopListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            foreach (var item in ShopListBox.SelectedItems)
            {
                var obj = item as Shop;
                ShopInfoWindow.ShopID = obj.ShopID;
            }
            ShopInfoWindow shopInfoWindow = new ShopInfoWindow();
            shopInfoWindow.Show();
            Close();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            ShopRedactAddWindow.IsRedacting = false;
            ShopRedactAddWindow shopRedactAddWindow = new ShopRedactAddWindow();
            shopRedactAddWindow.Show();
            Close();
        }
    }
}
