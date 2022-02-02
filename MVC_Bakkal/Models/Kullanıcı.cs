using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Bakkal.Models
{
    public class Kullanıcı
    {
        public int kullanici_id { get; set; }
        public string kullaniciadi { get; set; }
        public string parola { get; set; }
        public string adi { get; set; }
        public string soyadi { get; set; }
        public string eposta { get; set; }
        public string telefon { get; set; }
        public bool durum { get; set; }
        public int rol_id { get; set; }

    }
}