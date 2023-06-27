using System.Windows;
using System.Data.SqlClient;
using System.Data;

namespace PavillionsSQL
{
    public partial class EmployeeWorkspaceWindow : Window
    {
        public static string conString { get; set; }
        public static int EmpID { get; set; }
        public static int RoleID { get; set; }
        public EmployeeWorkspaceWindow()
        {
            InitializeComponent();
            GetEmpNameFromID(EmpID);
            GetEmpRoleFromID(EmpID);
            BookingRedactAddWindow.EmpID = EmpID;
        }

        const string RoleErrorMessage = "Ваша роль не позволяет выполнить данное действие";
        public void GetEmpNameFromID(int id)
        {
            string cmdString = "GetEmpNameFromID";

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(cmdString, con);

                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter idParam = new SqlParameter
                {
                    ParameterName = "@id",
                    Value = id
                };
                cmd.Parameters.Add(idParam);

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    HelloLabel.Content = HelloLabel.Content + reader[0].ToString() + " " + reader[1].ToString() + " " + reader[2].ToString();
                }
                reader.Close();
            }
        }

        public void GetEmpRoleFromID(int id)
        {
            string cmdString = "GetEmpRoleFromID";

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(cmdString, con);

                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter idParam = new SqlParameter
                {
                    ParameterName = "@id",
                    Value = id
                };
                cmd.Parameters.Add(idParam);

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    HelloLabel.Content = HelloLabel.Content + "\n\n" + "Ваша роль: " + reader[1].ToString();
                }
                reader.Close();
            }
        }

        private void shops_button_Click(object sender, RoutedEventArgs e)
        {
            if (RoleID == 1 || RoleID == 3)
            {
                ShopListWindow shopListWindow = new ShopListWindow();
                shopListWindow.Show();
                Close();
            }
            else
            {
                MessageBox.Show(RoleErrorMessage);
            }
        }

        private void tenants_button_Click(object sender, RoutedEventArgs e)
        {
            if (RoleID == 1 || RoleID == 2)
            {
                TenantListWindow tenantListWindow = new TenantListWindow();
                tenantListWindow.Show();
                Close();
            }
            else
            {
                MessageBox.Show(RoleErrorMessage);
            }
        }

        private void pavillions_button_Click(object sender, RoutedEventArgs e)
        {
            if (RoleID == 1 || RoleID == 2)
            {
                PavillionListWindow.EmpID = EmpID;
                PavillionListWindow pavillionListWindow = new PavillionListWindow();
                pavillionListWindow.Show();
                Close();
            }
            else
            {
                MessageBox.Show(RoleErrorMessage);
            }
        }

        private void back_button_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            Close();
        }
    }
}
