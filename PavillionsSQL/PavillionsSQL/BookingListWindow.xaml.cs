using System.Collections.Generic;
using System.Windows;
using System.Data.SqlClient;
using System.Data;

namespace PavillionsSQL
{
    public partial class BookingListWindow : Window
    {
        public static string conString { get; set; }
        public static int ShopID { get; set; }
        public static Booking selectedBook { get; set; }
        public BookingListWindow()
        {
            InitializeComponent();
            GetBookings();
        }

        List<Booking> booklist = new List<Booking>();

        public void GetBookings()
        {
            BookGrid.ItemsSource = null;
            booklist.Clear();
            string cmdString = "GetBookings";

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
                    Booking st = new Booking();
                    st.BookID = (int)reader[0];
                    st.Name = reader[1].ToString();
                    st.EmpID = (int)reader[2];
                    st.ShopName = reader[3].ToString();
                    st.Status = reader[4].ToString();
                    st.Bookstart = reader[5].ToString();
                    st.Bookend = reader[6].ToString();
                    booklist.Add(st);
                }
                reader.Close();
                BookGrid.ItemsSource = booklist;
            }
        }
        private void back_button_Click(object sender, RoutedEventArgs e)
        {
            PavillionListWindow pavillionListWindow = new PavillionListWindow();
            pavillionListWindow.Show();
            Close();
        }

        private void add_button_Click(object sender, RoutedEventArgs e)
        {
            BookingRedactAddWindow bookingRedactAddWindow = new BookingRedactAddWindow();
            bookingRedactAddWindow.Show();
            Close();
        }

        private void BookGrid_SelectedCellsChanged_1(object sender, System.Windows.Controls.SelectedCellsChangedEventArgs e)
        {
            Booking item = new Booking();
            foreach (var obj in BookGrid.SelectedItems)
            {
                item = obj as Booking;
                selectedBook = item;
            }
        }
    }
}
