using System.Windows;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;

namespace PavillionsSQL
{
    public partial class TenantListWindow : Window
    {
        public static string conString { get; set; }
        public static Tenant selectedTenant { get; set; }
        public TenantListWindow()
        {
            InitializeComponent();
            GetTenants();
        }

        List<Tenant> tenantlist = new List<Tenant>();

        public void GetTenants()
        {
            tenantlist.Clear();
            string cmdString = "GetTenants";

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(cmdString, con);

                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Tenant st = new Tenant();
                    st.TenantID = (int)reader[0];
                    st.TenantName = reader[1].ToString();
                    st.Phone = reader[2].ToString();
                    st.Address = reader[3].ToString();
                    st.Field = reader[4].ToString();
                    tenantlist.Add(st);
                }
                reader.Close();
                TenantGrid.ItemsSource = tenantlist;
            }
        }

        public void GetTenantServices()
        {
            List<string> servicelist = new List<string>();
            string cmdString = "GetTenantServices";

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(cmdString, con);

                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter Param = new SqlParameter
                {
                    ParameterName = "@id",
                    Value = selectedTenant.TenantID
                };
                cmd.Parameters.Add(Param);

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    servicelist.Add(reader[0].ToString());
                }
                reader.Close();

                string mes = string.Empty;
                foreach (var item in servicelist)
                {
                    mes += item + "\n";
                }
                MessageBox.Show(mes);
            }
        }

        public void GetTenantLicense()
        {
            string cmdString = "GetTenantLicense";

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(cmdString, con);

                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter Param = new SqlParameter
                {
                    ParameterName = "@id",
                    Value = selectedTenant.TenantID
                };
                cmd.Parameters.Add(Param);

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                License st = new License();
                while (reader.Read())
                {
                    st.Number = reader[0].ToString();
                    st.Date = reader[1].ToString();
                    st.Org = reader[2].ToString();
                }
                reader.Close();
                MessageBox.Show(st.Number + "\n" + st.Date + "\n" + st.Org);
            }
        }
        private void back_button_Click(object sender, RoutedEventArgs e)
        {
            EmployeeWorkspaceWindow employeeWorkspaceWindow = new EmployeeWorkspaceWindow();
            employeeWorkspaceWindow.Show();
            Close();
        }

        private void TenantGrid_SelectedCellsChanged(object sender, System.Windows.Controls.SelectedCellsChangedEventArgs e)
        {
            Tenant item = new Tenant();
            foreach (var obj in TenantGrid.SelectedItems)
            {
                item = obj as Tenant;
                selectedTenant = item;
            }
        }

        private void show_services_button_Click(object sender, RoutedEventArgs e)
        {
            if (TenantGrid.SelectedItem != null)
            {
                GetTenantServices();
            }
            else
            {
                MessageBox.Show("Арендатор не выбран");
            }
        }

        private void show_license_button_Click(object sender, RoutedEventArgs e)
        {
            if (TenantGrid.SelectedItem != null)
            {
                GetTenantLicense();
            }
            else
            {
                MessageBox.Show("Арендатор не выбран");
            }
        }

        private void readct_button_Click(object sender, RoutedEventArgs e)
        {
            if (TenantGrid.SelectedItem != null)
            {
                TenantRedactAddWindow.IsRedacting = true;
                TenantRedactAddWindow.redactingTenant = selectedTenant;
                TenantRedactAddWindow tenantRedactAddWindow = new TenantRedactAddWindow();
                tenantRedactAddWindow.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Арендатор не выбран");
            }
        }

        private void add_button_Click(object sender, RoutedEventArgs e)
        {
            TenantRedactAddWindow.IsRedacting = false;
            TenantRedactAddWindow tenantRedactAddWindow = new TenantRedactAddWindow();
            tenantRedactAddWindow.Show();
            Close();
        }
    }
}
