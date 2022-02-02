using MVC_Bakkal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;

namespace MVC_Bakkal.Controllers
{
    public class RegisterController : Controller
    {

        SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["BakkalDb"].ConnectionString);
        SqlCommand sqlCommand;
        Kullanıcı kullanici;

        public RegisterController()
        {
          
            kullanici = new Kullanıcı();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(FormCollection form)
        {


            kullanici.kullaniciadi = form["k_kullaniciadi"].Trim();
            kullanici.parola = form["k_parola"].Trim();
            kullanici.adi = form["k_adi"].Trim();
            kullanici.soyadi = form["k_soyadi"].Trim();
            kullanici.eposta = form["k_eposta"].Trim();
            kullanici.telefon = form["k_telefon"].Trim();

            kullanici.durum = true;
            kullanici.rol_id = Convert.ToInt32(form["rol_id"].Trim());

            sqlConnection.Open();
            sqlCommand = new SqlCommand("Kullanıcı_Ekle", sqlConnection);

            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("Kullanıcı_adi", kullanici.kullaniciadi);
            sqlCommand.Parameters.AddWithValue("Parola", kullanici.parola);
            sqlCommand.Parameters.AddWithValue("Ad", kullanici.adi);
            sqlCommand.Parameters.AddWithValue("Soyad", kullanici.soyadi);
            sqlCommand.Parameters.AddWithValue("E_posta", kullanici.eposta);
            sqlCommand.Parameters.AddWithValue("Telefon", kullanici.telefon);
            sqlCommand.Parameters.AddWithValue("Durum", kullanici.durum);
            sqlCommand.Parameters.AddWithValue("rol_Id", kullanici.rol_id);

            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();

            return RedirectToAction("Login", "Login");
        }



    }
}