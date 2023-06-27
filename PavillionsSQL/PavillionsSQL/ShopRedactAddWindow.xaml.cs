using System.Windows;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;

namespace PavillionsSQL
{
    public partial class ShopRedactAddWindow : Window
    {
        public static string conString { get; set; }
        public static List<Shop> slist { get; set; }
        public static bool IsRedacting { get; set; }
        public static List<string> citylist { get; set; }
        public static List<string> statuslist { get; set; }
        public static Shop redactingShop { get; set; }
        public ShopRedactAddWindow()
        {
            InitializeComponent();
            InitialCheck();
        }
        public void FillBoxes()
        {
            CityBox.ItemsSource = citylist;
            StatusBox.ItemsSource = statuslist;
        }
        public void InitialCheck()
        {
            FillBoxes();
            if (!IsRedacting)
            {
                image.Visibility = Visibility.Hidden;
                set_picture_button.Visibility = Visibility.Hidden;
            }
            else
            {
                image.Visibility = Visibility.Visible;
                set_picture_button.Visibility = Visibility.Visible;
                NameBox.Text = redactingShop.ShopName;
                PavCountBox.Text = redactingShop.PavCount.ToString();
                StatusBox.SelectedItem = redactingShop.ShopStatus;
                CityBox.SelectedItem = redactingShop.City;
                CostBox.Text = redactingShop.ShopCost;
                FactorBox.Text = redactingShop.Factor;
                FloorBox.Text = redactingShop.FloorCount.ToString();
                image.Source = null;
                image.Source = ImageWorker.ShowImage(conString, "Photo", "Shops", "ShopID", redactingShop.ShopID.ToString());
            }
        }

        public int GetStatusID()
        {
            string cmdString = "GetStatusID";

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

        public int GetCityID()
        {
            string cmdString = "GetCityID";

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(cmdString, con);

                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter Param = new SqlParameter
                {
                    ParameterName = "@name",
                    Value = CityBox.SelectedItem.ToString()
                };
                cmd.Parameters.Add(Param);

                con.Open();
                int result = (int)cmd.ExecuteScalar();
                con.Close();
                return result;
            }
        }

        public bool ValidateBoxes()
        {
            bool result = false;
            try
            {
                if (NameBox.Text == "" || PavCountBox.Text == "" || StatusBox.SelectedItem == null || CityBox.SelectedItem == null || CostBox.Text == "" || FactorBox.Text == "" || FloorBox.Text == "")
                {
                    throw new Exception("Не все поля заполнены");
                }
                if (NameBox.Text.Length > 255)
                {
                    throw new Exception("Название не должно превышать 255 символов");
                }
                if (!IsRedacting)
                {
                    foreach (var item in slist)
                    {
                        if (item.ShopName.Contains(NameBox.Text))
                        {
                            throw new Exception("ТЦ с таким названием уже существует");
                        }
                    }
                }
                else
                {
                    foreach (var item in slist)
                    {
                        if (item.ShopName.Contains(NameBox.Text) && item.ShopID != redactingShop.ShopID)
                        {
                            if (item.ShopID == redactingShop.ShopID)
                            {

                            }
                            else
                            {
                                throw new Exception("ТЦ с таким названием уже существует");
                            }
                        }
                    }
                }
                if (PavCountBox.Text.Length > 255 || int.TryParse(PavCountBox.Text, out int r) == false)
                {
                    throw new Exception("Неверный формат количества павильонов");
                }
                if (CostBox.Text.Length > 255 || double.TryParse(CostBox.Text, out double rr) == false || CostBox.Text.Contains("."))
                {
                    throw new Exception("Неверный формат цены (Возможно, вы поставили '.' вместо ',')");
                }
                if (FactorBox.Text.Length > 255 || double.TryParse(FactorBox.Text, out double rrr) == false || FactorBox.Text.Contains("."))
                {
                    throw new Exception("Неверный формат коэффициента (Возможно, вы поставили '.' вместо ',')");
                }
                if (FloorBox.Text.Length > 255 || int.TryParse(FloorBox.Text, out int rrrr) == false)
                {
                    throw new Exception("Неверный формат количества этажей");
                }
                result = true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return result;
        }

        public void AddShop()
        {
            string cmdString = "AddShop";

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(cmdString, con);

                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter Param1 = new SqlParameter
                {
                    ParameterName = "@name",
                    Value = NameBox.Text
                };
                cmd.Parameters.Add(Param1);

                SqlParameter Param2 = new SqlParameter
                {
                    ParameterName = "@status",
                    Value = GetStatusID()
                };
                cmd.Parameters.Add(Param2);

                SqlParameter Param3 = new SqlParameter
                {
                    ParameterName = "@pavCount",
                    Value = Convert.ToInt32(PavCountBox.Text)
                };
                cmd.Parameters.Add(Param3);

                SqlParameter Param4 = new SqlParameter
                {
                    ParameterName = "@cityID",
                    Value = GetCityID()
                };
                cmd.Parameters.Add(Param4);

                SqlParameter Param5 = new SqlParameter
                {
                    ParameterName = "@cost",
                    Value = Convert.ToDouble(CostBox.Text)
                };
                cmd.Parameters.Add(Param5);

                SqlParameter Param6 = new SqlParameter
                {
                    ParameterName = "@factor",
                    Value = Convert.ToDouble(FactorBox.Text)
                };
                cmd.Parameters.Add(Param6);

                SqlParameter Param7 = new SqlParameter
                {
                    ParameterName = "@floorCount",
                    Value = Convert.ToInt32(FloorBox.Text)
                };
                cmd.Parameters.Add(Param7);

                con.Open();

                cmd.ExecuteNonQuery();

                con.Close();
            }
        }

        public void RedactShop()
        {
            try
            {
            string cmdString = "RedactShop";

                using (SqlConnection con = new SqlConnection(conString))
                {
                    SqlCommand cmd = new SqlCommand(cmdString, con);

                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter Param = new SqlParameter
                    {
                        ParameterName = "@shopID",
                        Value = redactingShop.ShopID
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
                        ParameterName = "@status",
                        Value = GetStatusID()
                    };
                    cmd.Parameters.Add(Param2);

                    SqlParameter Param3 = new SqlParameter
                    {
                        ParameterName = "@pavCount",
                        Value = Convert.ToInt32(PavCountBox.Text)
                    };
                    cmd.Parameters.Add(Param3);

                    SqlParameter Param4 = new SqlParameter
                    {
                        ParameterName = "@cityID",
                        Value = GetCityID()
                    };
                    cmd.Parameters.Add(Param4);

                    SqlParameter Param5 = new SqlParameter
                    {
                        ParameterName = "@cost",
                        Value = Convert.ToDouble(CostBox.Text)
                    };
                    cmd.Parameters.Add(Param5);

                    SqlParameter Param6 = new SqlParameter
                    {
                        ParameterName = "@factor",
                        Value = Convert.ToDouble(FactorBox.Text)
                    };
                    cmd.Parameters.Add(Param6);

                    SqlParameter Param7 = new SqlParameter
                    {
                        ParameterName = "@floorCount",
                        Value = Convert.ToInt32(FloorBox.Text)
                    };
                    cmd.Parameters.Add(Param7);

                    con.Open();

                    cmd.ExecuteNonQuery();

                    con.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        const string successMessage = "Успешно";
        private void enter_button_Click(object sender, RoutedEventArgs e)
        {
            if (!IsRedacting)
            {
                if (ValidateBoxes())
                {
                    AddShop();
                    MessageBox.Show(successMessage);
                    ShopListWindow shopListWindow = new ShopListWindow();
                    shopListWindow.Show();
                    Close();
                }
            }
            else
            {
                if (ValidateBoxes())
                {
                    RedactShop();
                    MessageBox.Show(successMessage);
                    ShopListWindow shopListWindow = new ShopListWindow();
                    shopListWindow.Show();
                    Close();
                }
            }
        }

        private void back_button_Click(object sender, RoutedEventArgs e)
        {
            if (IsRedacting)
            {
                ShopInfoWindow shopInfoWindow = new ShopInfoWindow();
                shopInfoWindow.Show();
                Close();
            }
            else
            {
                ShopListWindow shopListWindow = new ShopListWindow();
                shopListWindow.Show();
                Close();
            }
        }

            private void set_picture_button_Click(object sender, RoutedEventArgs e)
        {
            ImageWorker.LoadImage(conString, "Shops", "Photo", "ShopID", redactingShop.ShopID.ToString());
            image.Source = null;
            image.Source = ImageWorker.ShowImage(conString, "Photo", "Shops", "ShopID", redactingShop.ShopID.ToString());
        }
    }
}
