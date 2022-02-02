using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Bakkal.Models
{
    public class Ürün
    {
        public int urun_id { get; set; }
        public string u_adi { get; set; }
        public string u_barkodu { get; set; }
        public DateTime u_tuketim_tarihi { get; set; }
        public DateTime u_uretim_tarihi { get; set; }
        public float u_fiyat { get; set; }
        public float u_agirlik { get; set; }
        public string u_rengi { get; set; }
        public int marka_id { get; set; }
        public int stok_id { get; set; }
        public int kategori_id { get; set; }
    }
}