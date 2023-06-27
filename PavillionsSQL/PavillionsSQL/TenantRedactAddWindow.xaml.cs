using System.Windows;
using System.Data.SqlClient;
using System.Data;
using System;

namespace PavillionsSQL
{
    public partial class TenantRedactAddWindow : Window
    {
        public static string conString { get; set; }
        public static Tenant redactingTenant { get; set; }
        public static bool IsRedacting { get; set; }
        public TenantRedactAddWindow()
        {
            InitializeComponent();
            InitialCheck();
        }

        public void InitialCheck()
        {
            if (IsRedacting)
            {
                NameBox.Text = redactingTenant.TenantName;
                PhoneBox.Text = redactingTenant.Phone;
                AddressBox.Text = redactingTenant.Address;
                FieldBox.Text = redactingTenant.Field;
            }
        }
        public bool ValidateBoxes()
        {
            bool result = false;
            try
            {
                if (NameBox.Text == "" || PhoneBox.Text == "" || AddressBox.Text == "" || FieldBox.Text == "")
                {
                    throw new Exception("Не все поля заполнены");
                }
                if (NameBox.Text.Length > 255 || PhoneBox.Text.Length > 255 || AddressBox.Text.Length > 255 || FieldBox.Text.Length > 255)
                {
                    throw new Exception("Превышено количество символов");
                }
                result = true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return result;
        }

        public void AddTenant()
        {
            string cmdString = "AddTenant";

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(cmdString, con);

                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter Param = new SqlParameter
                {
                    ParameterName = "@name",
                    Value = NameBox.Text
                };
                cmd.Parameters.Add(Param);

                SqlParameter Param2 = new SqlParameter
                {
                    ParameterName = "@phone",
                    Value = PhoneBox.Text
                };
                cmd.Parameters.Add(Param2);

                SqlParameter Param3 = new SqlParameter
                {
                    ParameterName = "@address",
                    Value = AddressBox.Text
                };
                cmd.Parameters.Add(Param3);

                SqlParameter Param4 = new SqlParameter
                {
                    ParameterName = "@field",
                    Value = FieldBox.Text
                };
                cmd.Parameters.Add(Param4);

                con.Open();

                cmd.ExecuteNonQuery();

                con.Close();
            }
        }

        public void RedactTenant()
        {
            string cmdString = "RedactTenant";

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(cmdString, con);

                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter Param = new SqlParameter
                {
                    ParameterName = "@id",
                    Value = redactingTenant.TenantID
                };
                cmd.Parameters.Add(Param);

                SqlParameter Param1 = new SqlParameter
                {
                    ParameterName = "@name",
                    Value = NameBox.Text
                };
                cmd.Parameters.Add(Param1);

                SqlParameter Param2 = new SqlParameter
                {
                    ParameterName = "@phone",
                    Value = PhoneBox.Text
                };
                cmd.Parameters.Add(Param2);

                SqlParameter Param3 = new SqlParameter
                {
                    ParameterName = "@address",
                    Value = AddressBox.Text
                };
                cmd.Parameters.Add(Param3);

                SqlParameter Param4 = new SqlParameter
                {
                    ParameterName = "@field",
                    Value = FieldBox.Text
                };
                cmd.Parameters.Add(Param4);

                con.Open();

                cmd.ExecuteNonQuery();

                con.Close();
            }
        }
        private void back_button_Click(object sender, RoutedEventArgs e)
        {
            TenantListWindow tenantListWindow = new TenantListWindow();
            tenantListWindow.Show();
            Close();
        }

        private void enter_button_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateBoxes())
            {
                if (IsRedacting)
                {
                    RedactTenant();
                }
                else
                {
                    AddTenant();
                }
                MessageBox.Show("Успешно");
                TenantListWindow tenantListWindow = new TenantListWindow();
                tenantListWindow.Show();
                Close();
            }
        }
    }
}
