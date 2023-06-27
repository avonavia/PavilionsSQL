using System.Collections.Generic;
using System.Windows;
using System.Data.SqlClient;
using System.Data;

namespace PavillionsSQL
{
    public partial class PavillionsWindow : Window
    {
        public static string conString { get; set; }
        public static int ShopID { get; set; }
        public static Pavillion selectedPav { get; set; }
        public PavillionsWindow()
        {
            InitializeComponent();
            GetPavList();
        }

        List<Pavillion> pavlist = new List<Pavillion>();

        public void GetPavList()
        {
            pavlist.Clear();
            string cmdString = "GetPavList";

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
        private void back_button_Click(object sender, RoutedEventArgs e)
        {
            ShopInfoWindow shopInfoWindow = new ShopInfoWindow();
            shopInfoWindow.Show();
            Close();
        }

        private void add_button_Click(object sender, RoutedEventArgs e)
        {
            PavillionRedactAddWindow.ShopID = ShopID;
            PavillionRedactAddWindow.plist = pavlist;
            PavillionRedactAddWindow.IsRedacting = false;
            PavillionRedactAddWindow pavillionRedactAddWindow = new PavillionRedactAddWindow();
            pavillionRedactAddWindow.Show();
            Close();
        }

        private void redact_button_Click(object sender, RoutedEventArgs e)
        {
            if (PavGrid.SelectedItem != null)
            {
                PavillionRedactAddWindow.ShopID = ShopID;
                PavillionRedactAddWindow.plist = pavlist;
                PavillionRedactAddWindow.IsRedacting = true;
                PavillionRedactAddWindow.redactingPav = selectedPav;
                PavillionRedactAddWindow pavillionRedactAddWindow = new PavillionRedactAddWindow();
                pavillionRedactAddWindow.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Не выбран павильон для редактирования");
            }
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
    }
}
