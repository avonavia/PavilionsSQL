using System.Windows;
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;

namespace PavillionsSQL
{
    public partial class PavillionRedactAddWindow : Window
    {
        public static string conString { get; set; }
        public static int ShopID { get; set; }
        public static int PavCount { get; set; }
        public static int FloorCount { get; set; }
        public static List<Pavillion> plist { get; set; }
        public static bool IsRedacting { get; set; }
        public static Pavillion redactingPav { get; set; }
        public PavillionRedactAddWindow()
        {
            InitializeComponent();
            InitialCheck();
        }

        public void InitialCheck()
        {
            FillFloorBox();
            if (IsRedacting)
            {
                NameBox.Text = redactingPav.PavNO;
                FloorBox.SelectedItem = redactingPav.Floor;
                SquareBox.Text = redactingPav.Square;
                CostBox.Text = redactingPav.Cost;
                FactorBox.Text = redactingPav.Factor;
            }
        }

        Regex validateNameRegex = new Regex("^[0-9]{1,2}[А-Я]$");

        List<int> floors = new List<int>();

        public void FillFloorBox()
        {
            floors.Clear();
            FloorBox.ItemsSource = null;
            for (var i = 0; i < FloorCount; i ++)
            {
                floors.Add(i+1);
            }
            FloorBox.ItemsSource = floors;
        }

        public bool ValidateBoxes()
        {
            bool result = false;
            try
            {
                if (NameBox.Text == "" || FloorBox.SelectedItem == null || SquareBox.Text == "" || CostBox.Text == "" || FactorBox.Text == "")
                {
                    throw new Exception("Не все поля заполнены");
                }
                if (!validateNameRegex.IsMatch(NameBox.Text))
                {
                    throw new Exception("Неверный формат названия");
                }
                if (!IsRedacting)
                {
                    foreach (var item in plist)
                    {
                        if (item.PavNO.Contains(NameBox.Text))
                        {
                            throw new Exception("Павильон с таким названием уже существует");
                        }
                    }
                }
                else
                {
                    foreach (var item in plist)
                    {
                        if (item.PavNO.Contains(NameBox.Text) && item.PavID != redactingPav.PavID)
                        {
                            throw new Exception("Павильон с таким названием уже существует");
                        }
                    }
                }
                if (SquareBox.Text.Length > 255 || double.TryParse(SquareBox.Text, out double r) == false || SquareBox.Text.Contains("."))
                {
                    throw new Exception("Неверный формат площади (Возможно, вы поставили '.' вместо ',')");
                }
                if (CostBox.Text.Length > 255 || double.TryParse(CostBox.Text, out double rr) == false || CostBox.Text.Contains("."))
                {
                    throw new Exception("Неверный формат цены (Возможно, вы поставили '.' вместо ',')");
                }
                if (FactorBox.Text.Length > 255 || double.TryParse(FactorBox.Text, out double rrr) == false || FactorBox.Text.Contains("."))
                {
                    throw new Exception("Неверный формат коэффициента (Возможно, вы поставили '.' вместо ',')");
                }
                result = true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return result;
        }

        public int GetPavQuantity()
        {
            string cmdString = "GetPavQuantity";

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(cmdString, con);

                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter Param1 = new SqlParameter
                {
                    ParameterName = "@id",
                    Value = ShopID
                };
                cmd.Parameters.Add(Param1);

                con.Open();

                int result = (int)cmd.ExecuteScalar();

                con.Close();

                return result;
            }
        }

        public void RedactPavillion()
        {
            try
            {
                string cmdString = "RedactPavillion";

                using (SqlConnection con = new SqlConnection(conString))
                {
                    SqlCommand cmd = new SqlCommand(cmdString, con);

                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter Param = new SqlParameter
                    {
                        ParameterName = "@PavID",
                        Value = redactingPav.PavID
                    };
                    cmd.Parameters.Add(Param);

                    SqlParameter Param1 = new SqlParameter
                    {
                        ParameterName = "@shopID",
                        Value = ShopID
                    };
                    cmd.Parameters.Add(Param1);

                    SqlParameter Param2 = new SqlParameter
                    {
                        ParameterName = "@pavNO",
                        Value = NameBox.Text
                    };
                    cmd.Parameters.Add(Param2);

                    SqlParameter Param3 = new SqlParameter
                    {
                        ParameterName = "@floor",
                        Value = Convert.ToInt32(FloorBox.Text)
                    };
                    cmd.Parameters.Add(Param3);

                    SqlParameter Param4 = new SqlParameter
                    {
                        ParameterName = "@square",
                        Value = Convert.ToDouble(SquareBox.Text)
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
        public void AddPavillion()
        {
            string cmdString = "AddPavillion";

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(cmdString, con);

                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter Param1 = new SqlParameter
                {
                    ParameterName = "@shopID",
                    Value = ShopID
                };
                cmd.Parameters.Add(Param1);

                SqlParameter Param2 = new SqlParameter
                {
                    ParameterName = "@pavNO",
                    Value = NameBox.Text
                };
                cmd.Parameters.Add(Param2);

                SqlParameter Param3 = new SqlParameter
                {
                    ParameterName = "@floor",
                    Value = Convert.ToInt32(FloorBox.Text)
                };
                cmd.Parameters.Add(Param3);

                SqlParameter Param4 = new SqlParameter
                {
                    ParameterName = "@square",
                    Value = Convert.ToDouble(SquareBox.Text)
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

                con.Open();

                cmd.ExecuteNonQuery();

                con.Close();
            }
        }
        private void back_button_Click(object sender, RoutedEventArgs e)
        {
            PavillionsWindow pavillionsWindow = new PavillionsWindow();
            pavillionsWindow.Show();
            Close();
        }

        const string tooManyPavillionsError = "Превышено количество павильонов";
        const string successMessage = "Успешно";
        private void enter_button_Click(object sender, RoutedEventArgs e)
        {
            if (!IsRedacting)
            {
                if (ValidateBoxes())
                {
                    if (GetPavQuantity() < PavCount)
                    {
                        AddPavillion();
                        MessageBox.Show(successMessage);
                        PavillionsWindow pavillionsWindow = new PavillionsWindow();
                        pavillionsWindow.Show();
                        Close();
                    }
                    else
                    {
                        MessageBox.Show(tooManyPavillionsError);
                    }
                }
            }
            else
            {
                if (ValidateBoxes())
                {
                    RedactPavillion();
                    MessageBox.Show(successMessage);
                    PavillionsWindow pavillionsWindow = new PavillionsWindow();
                    pavillionsWindow.Show();
                    Close();
                }
            }
        }
    }
}
