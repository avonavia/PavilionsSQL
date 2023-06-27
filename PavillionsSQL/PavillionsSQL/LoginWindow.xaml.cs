using System.Windows;
using System.Data.SqlClient;
using System.Data;
using System;

namespace PavillionsSQL
{
    public partial class LoginWindow : Window
    {
        static string conString = @"Data Source=.\SQLEXPRESS; Initial Catalog=PavillionsDB; Integrated Security=true;";
        public LoginWindow()
        {
            InitializeComponent();
            HideCapcha();
            ShopListWindow.conString = conString;
            EmployeeWorkspaceWindow.conString = conString;
            ShopInfoWindow.conString = conString;
            PavillionsWindow.conString = conString;
            PavillionRedactAddWindow.conString = conString;
            ShopRedactAddWindow.conString = conString;
            PavillionListWindow.conString = conString;
            BookingListWindow.conString = conString;
            BookingRedactAddWindow.conString = conString;
            TenantListWindow.conString = conString;
            TenantRedactAddWindow.conString = conString;
        }

        public int FailCount = 0;
        public bool CapchaShowing = false;
        public void HideCapcha()
        {
            CapchaButton.Visibility = Visibility.Hidden;
            CapchaLable.Visibility = Visibility.Hidden;
            GeneratedCapchaLabel.Visibility = Visibility.Hidden;
            capcha_box.Visibility = Visibility.Hidden;
        }

        public void ShowCapcha()
        {
            CapchaButton.Visibility = Visibility.Visible;
            CapchaLable.Visibility = Visibility.Visible;
            GeneratedCapchaLabel.Visibility = Visibility.Visible;
            capcha_box.Visibility = Visibility.Visible;
        }

        const string wrongLoginPasswordError = "Неверный логин или пароль";
        const string capchaNotSolvedError = "Для продолжения необходимо решить капчу";

        public bool CheckLoginExists()
        {
            bool output = false;
            using (SqlConnection con = new SqlConnection(conString))
            {
                string cmdString = "CheckLoginExists";
                con.Open();

                SqlCommand cmd = new SqlCommand(cmdString, con);

                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter loginParam = new SqlParameter
                {
                    ParameterName = "@login",
                    Value = login_box.Text.ToLower()
                };
                cmd.Parameters.Add(loginParam);

                SqlParameter passwordParam = new SqlParameter
                {
                    ParameterName = "@password",
                    Value = password_box.Password
                };
                cmd.Parameters.Add(passwordParam);

                int result = (int)cmd.ExecuteScalar();

                con.Close();

                if (result == 1)
                {
                    output = true;
                }
                else
                {
                    output = false;
                }
                return output;
            }
        }

        public int GetEmpFromLogin()
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                string cmdString = "GetEmpFromLogin";
                con.Open();

                SqlCommand cmd = new SqlCommand(cmdString, con);

                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter loginParam = new SqlParameter
                {
                    ParameterName = "@login",
                    Value = login_box.Text
                };
                cmd.Parameters.Add(loginParam);

                SqlParameter passwordParam = new SqlParameter
                {
                    ParameterName = "@password",
                    Value = password_box.Password
                };
                cmd.Parameters.Add(passwordParam);

                int result = (int)cmd.ExecuteScalar();

                con.Close();

                return result;
            }
        }

        public int GetEmpRoleFromID(int id)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                string cmdString = "GetEmpRoleFromID";
                con.Open();

                SqlCommand cmd = new SqlCommand(cmdString, con);

                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter idParam = new SqlParameter
                {
                    ParameterName = "@id",
                    Value = id
                };
                cmd.Parameters.Add(idParam);

                int result = (int)cmd.ExecuteScalar();

                con.Close();

                return result;
            }
        }

        public string GenerateCapcha()
        {
            string capcha = string.Empty;
            string letters = "abcdefghijklmnopqrstuvwxyz1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            Random rand = new Random();
            for (var i = 0; i < 4; i++)
            {
                int num = rand.Next(0, letters.Length);
                capcha += letters[num];
            }
            return capcha;
        }
        public void Capcha()
        {
            CapchaShowing = true;
            ShowCapcha();
            GeneratedCapchaLabel.Content = GenerateCapcha();
        }

        private void Enter_button_Click(object sender, RoutedEventArgs e)
        {
            if (CheckLoginExists() && login_box.Text.Length < 255 && password_box.Password.Length < 255 && CapchaShowing == false)
            {
                int EmpID = GetEmpFromLogin();
                int RoleID = GetEmpRoleFromID(EmpID);
                EmployeeWorkspaceWindow.EmpID = EmpID;
                EmployeeWorkspaceWindow.RoleID = RoleID;
                EmployeeWorkspaceWindow employeeWorkspaceWindow = new EmployeeWorkspaceWindow();
                employeeWorkspaceWindow.Show();
                Close();
            }
            else
            {
                if (CapchaShowing)
                {
                    MessageBox.Show(capchaNotSolvedError);
                }
                else
                {
                    MessageBox.Show(wrongLoginPasswordError);
                    FailCount++;
                    if (FailCount > 2)
                    {
                        Capcha();
                    }
                }
            }
        }

        private void CapchaButton_Click(object sender, RoutedEventArgs e)
        {
            if (GeneratedCapchaLabel.Content.ToString() == capcha_box.Text)
            {
                FailCount = 0;
                HideCapcha();
                CapchaShowing = false;
            }
            else
            {
                Capcha();
            }
        }
    }
}
