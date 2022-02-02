using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Bakkal.Models
{
    public class Stok
    {
        public int stok_id { get; set; }
        public int s_adedi { get; set; }

        public DateTime giris_tarihi { get; set; }
        public DateTime bitis_tarihi { get; set; }
    }
}