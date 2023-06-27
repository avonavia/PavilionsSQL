using System.Windows;
using System.Data.SqlClient;
using System.Data;

namespace PavillionsSQL
{
    public partial class ShopInfoWindow : Window
    {
        public static string conString { get; set; }
        public static int ShopID { get; set; }
        public ShopInfoWindow()
        {
            InitializeComponent();
            GetShopByID();
            FillLabels();
        }

        public Shop shop = new Shop();
        public void GetShopByID()
        {
            string cmdString = "GetShopByID";

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(cmdString, con);

                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter idParam = new SqlParameter
                {
                    ParameterName = "@id",
                    Value = ShopID
                };
                cmd.Parameters.Add(idParam);

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Shop st = new Shop();
                    st.ShopID = ShopID;
                    st.ShopName = reader[0].ToString();
                    st.ShopStatus = reader[1].ToString();
                    st.PavCount = (int)reader[2];
                    st.City = reader[3].ToString();
                    st.ShopCost = reader[4].ToString();
                    st.Factor = reader[5].ToString();
                    st.FloorCount = (int)reader[6];
                    st.Photo = ImageWorker.ShowImage(conString, "Photo", "Shops", "ShopID", st.ShopID.ToString());
                    shop = st;
                }
                reader.Close();
            }
        }

        public void FillLabels()
        {
            NameLabel.Content = NameLabel.Content + shop.ShopName;
            image.Source = shop.Photo;
            StatusLabel.Content = StatusLabel.Content + shop.ShopStatus;
            PavCountLabel.Content = PavCountLabel.Content + shop.PavCount.ToString();
            CityLabel.Content = CityLabel.Content + shop.City;
            ShopCostLabel.Content = ShopCostLabel.Content + shop.ShopCost;
            FactorLabel.Content = FactorLabel.Content + shop.Factor;
            FloorCountLabel.Content = FloorCountLabel.Content + shop.FloorCount.ToString();
        }

        private void back_button_Click(object sender, RoutedEventArgs e)
        {
            ShopListWindow shopListWindow = new ShopListWindow();
            shopListWindow.Show();
            Close();
        }

        private void pavillions_button_Click(object sender, RoutedEventArgs e)
        {
            PavillionRedactAddWindow.PavCount = shop.PavCount;
            PavillionRedactAddWindow.FloorCount = shop.FloorCount;
            PavillionsWindow.ShopID = shop.ShopID;
            PavillionsWindow pavillionsWindow = new PavillionsWindow();
            pavillionsWindow.Show();
            Close();
        }

        private void redact_button_Click(object sender, RoutedEventArgs e)
        {
            ShopRedactAddWindow.redactingShop = shop;
            ShopRedactAddWindow.IsRedacting = true;
            ShopRedactAddWindow shopRedactAddWindow = new ShopRedactAddWindow();
            shopRedactAddWindow.Show();
            Close();
        }
    }
}
