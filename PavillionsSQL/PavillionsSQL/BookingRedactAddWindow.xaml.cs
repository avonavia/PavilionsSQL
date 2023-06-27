using System.Collections.Generic;
using System.Windows;
using System.Data.SqlClient;
using System.Data;
using System;

namespace PavillionsSQL
{
    public partial class BookingRedactAddWindow : Window
    {
        public static string conString { get; set; }
        public static int EmpID { get; set; }
        public static int ShopID { get; set; }
        public static int PavID { get; set; }
        public BookingRedactAddWindow()
        {
            InitializeComponent();
            GetTenantList();
        }

        List<string> tenantlist = new List<string>();
        public void GetTenantList()
        {
            TenantBox.ItemsSource = null;
            tenantlist.Clear();
            string cmdString = "GetTenantList";

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(cmdString, con);

                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    tenantlist.Add(reader[0].ToString());
                }
                reader.Close();
                TenantBox.ItemsSource = tenantlist;
            }
        }

        public int GetTenantID()
        {
            string cmdString = "GetTenantID";

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(cmdString, con);

                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter Param = new SqlParameter
                {
                    ParameterName = "@name",
                    Value = TenantBox.SelectedItem.ToString()
                };
                cmd.Parameters.Add(Param);

                con.Open();

                int result = (int)cmd.ExecuteScalar();

                con.Close();

                return result;
            }
        }


        public void BookPavillion()
        {
            string cmdString = "BookPavillion";

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(cmdString, con);

                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter Param = new SqlParameter
                {
                    ParameterName = "@tenantID",
                    Value = GetTenantID()
                };
                cmd.Parameters.Add(Param);

                SqlParameter Param2 = new SqlParameter
                {
                    ParameterName = "@empID",
                    Value = EmpID
                };
                cmd.Parameters.Add(Param2);

                SqlParameter Param3 = new SqlParameter
                {
                    ParameterName = "@shopID",
                    Value = ShopID
                };
                cmd.Parameters.Add(Param3);

                SqlParameter Param4 = new SqlParameter
                {
                    ParameterName = "@pavID",
                    Value = PavID
                };
                cmd.Parameters.Add(Param4);

                SqlParameter Param5 = new SqlParameter
                {
                    ParameterName = "@bookstart",
                    Value = StartBox.Text
                };
                cmd.Parameters.Add(Param5);

                SqlParameter Param6 = new SqlParameter
                {
                    ParameterName = "@bookend",
                    Value = EndBox.Text
                };
                cmd.Parameters.Add(Param6);

                con.Open();

                cmd.ExecuteNonQuery();

                con.Close();
            }
        }

        public bool ValidateBoxes()
        {
            bool result = false;
            try
            {
                if (TenantBox.SelectedItem == null || StartBox.Text == "" || EndBox.Text == "")
                {
                    throw new Exception("Не все поля заполнены");
                }
                if (StartBox.Text.Length > 255 || EndBox.Text.Length > 255)
                {
                    throw new Exception("Слишком много символов");
                }
                if (!DateTime.TryParse(StartBox.Text, out DateTime r) || !DateTime.TryParse(EndBox.Text, out DateTime rr))
                {
                    throw new Exception("Неверный формат даты");
                }
                if (Convert.ToDateTime(StartBox.Text) > Convert.ToDateTime(EndBox.Text))
                {
                    throw new Exception("Дата конца раньше даты начала");
                }
                result = true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return result;
        }

        private void enter_button_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateBoxes())
            {
                BookPavillion();
                MessageBox.Show("Успешно");
                PavillionListWindow.Changed = true;
                PavillionListWindow pavillionListWindow = new PavillionListWindow();
                pavillionListWindow.Show();
                Close();
            }
        }

        private void back_button_Click(object sender, RoutedEventArgs e)
        {
            PavillionListWindow.Changed = false;
            PavillionListWindow pavillionListWindow = new PavillionListWindow();
            pavillionListWindow.Show();
            Close();
        }
    }
}
