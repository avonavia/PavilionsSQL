using System.Windows.Media.Imaging;

namespace PavillionsSQL
{
    public class Shop
    {
        public int ShopID { get; set; }
        public string ShopName { get; set; }
        public string ShopStatus { get; set; }
        public int PavCount { get; set; }
        public string City { get; set; }
        public string ShopCost { get; set; }
        public string Factor { get; set; }
        public int FloorCount { get; set; }
        public BitmapImage Photo { get; set; }
    }
}
