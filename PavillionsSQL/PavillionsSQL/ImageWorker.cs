using Microsoft.Win32;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Media.Imaging;

namespace PavillionsSQL
{
    public class ImageWorker
    {
        public static void LoadImage(string conString, string tableName, string columnName, string IDColumnName, string id)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.ShowDialog();
                byte[] image_bytes = File.ReadAllBytes(openFileDialog.FileName);

                SqlConnection con = new SqlConnection(conString);
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "UPDATE " + tableName + " SET " + columnName + "= (@ImageData) WHERE " + IDColumnName + "= " + id;
                cmd.Parameters.Add("@ImageData", SqlDbType.Image, 1000000);
                cmd.Parameters["@ImageData"].Value = image_bytes;
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch
            {
               
            }
        }
        public static BitmapImage ShowImage(string conString, string columnName, string tableName, string IDColumnName, string id)
        {
                DataTable dataTable = new DataTable("database");
                SqlConnection con = new SqlConnection(conString);
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT " + columnName + " FROM " + tableName + " WHERE " + IDColumnName + " = " + id;
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                sqlDataAdapter.Fill(dataTable);
                var image = ByteImage.Convert(ByteImage.GetImageFromByteArray((byte[])dataTable.Rows[dataTable.Rows.Count - 1][0]));
                con.Close();
                return image;
        }
    }
}
