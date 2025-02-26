namespace MVC_E_ticaretWeb.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Tarih { get; set; }
        public string Ozet { get; set; }
        public string Alici { get; set; }
        public decimal Tutar { get; set; }
        public string Durum { get; set; }
        public DateTime TeslimTarihi { get; set; }
        public string ResimUrl { get; set; }
    }
}
