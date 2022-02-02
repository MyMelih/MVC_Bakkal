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
    public class LoginController : Controller
    {
        SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["BakkalDb"].ConnectionString);
        SqlCommand sqlCommand;

       
        Kullanıcı kullanici;

        public LoginController()
        {
         
            kullanici = new Kullanıcı();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        //formdan gelen kullanıcı adı şifre bilgisi kontorl edilmiştir. gelen veriler sistemdeki ile uyuşuyorsa giriş gerçekleşmiştir. Yoksa ana sayfaya gönderilmiştir.
        public ActionResult Login(FormCollection form)
        {
            sqlConnection.Open();
            sqlCommand = new SqlCommand("Kullanıcı_Check", sqlConnection);

            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("Kullanıcı_adi", form["k_adi"]);
            sqlCommand.Parameters.AddWithValue("Parola", form["şifre"]);

            SqlParameter temp = new SqlParameter();
            temp.ParameterName = "@isVaid";
            temp.SqlDbType = System.Data.SqlDbType.Bit;
            temp.Direction = System.Data.ParameterDirection.Output;

            sqlCommand.Parameters.Add(temp);

            sqlCommand.ExecuteNonQuery();

            int sonuc = Convert.ToInt32(temp.Value);
            sqlConnection.Close();
                if (sonuc!=0)
            {
                return RedirectToAction("List", "Ürün");
                
              
            }
            return RedirectToAction("Index", "Home");

        }
    }
}