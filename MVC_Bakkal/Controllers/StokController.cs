using MVC_Bakkal.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;

namespace MVC_Bakkal.Controllers
{
    public class StokController : Controller
    {
        SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["BakkalDb"].ConnectionString);
        SqlCommand sqlCommand;

       
        Stok stok;

        public StokController()
        {
            stok = new Stok();
           
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }


        //Formdan gelen bilgileri bir satış nesnesinin özelliklerine atayarak satış nesnesini özelliklerin doldurur.
        //Veri tabanında yazılmış ilgili ekle prosedürene parameterleri aracılığıyla gönderilir
        //Daha sonra komut satırları çalışır
        [HttpPost]
        public ActionResult Add(FormCollection form)
        {
            DateTime dateTime = DateTime.Now;
            stok.s_adedi = Convert.ToInt32(form["s_adedi"].ToString());
            stok.stok_id = Convert.ToInt32(form["id"].ToString());


            stok.giris_tarihi = Convert.ToDateTime(form["giris_tarihi"].ToString());

            stok.bitis_tarihi = Convert.ToDateTime(form["bitis_tarihi"].ToString());


            sqlConnection.Open();
            sqlCommand = new SqlCommand("Stok_Ekle", sqlConnection);

            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("Id", stok.stok_id);
            sqlCommand.Parameters.AddWithValue("Stok_Adedi", stok.s_adedi);
            sqlCommand.Parameters.AddWithValue("Giris_Tarihi", stok.giris_tarihi);
            sqlCommand.Parameters.AddWithValue("Bitis_Tarihi", stok.bitis_tarihi);

            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();


            return RedirectToAction("List", "Stok");

        }
        //Veri tabanında yazılmış olan listeleme prosedürü çalıştırılarak veri tabınından veriler çekilir
        public ActionResult List()
        {

            sqlConnection.Open();
            sqlCommand = new SqlCommand("Stok_List", sqlConnection);

            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            ViewBag.table = dataSet.Tables[0];

            return View(dataSet);
        }
        //Root'ta bulunan id değeri çeker ve bu değerini silme prosedüre yollar ve bu komut satırı çalıştırılır.
        public ActionResult Delete(int id)
        {

            sqlConnection.Open();
            sqlCommand = new SqlCommand("Stok_Sil", sqlConnection);

            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("Stok_Id", id);

            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();

            return RedirectToAction("List", "Stok");


        }
        //Güncelleme işlemi yapılırken inputlarda default olarak daha önceki değer bulunması için roottaki id değerini alır ve bu id değerine sahip satırı döndürür.
        public ActionResult Update(int id)
        {
            sqlConnection.Open();
            sqlCommand = new SqlCommand("StokId", sqlConnection);

            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("Stok_Id", id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);
            ViewBag.emprecord = dataSet.Tables[0];
            return View();
           
        }


        //Formdan gelen verileri çeker ilgili güncelleme prosedüreüne gelen verileri atar.
        [HttpPost]

        public ActionResult Update(FormCollection form, int id)
        {

            sqlConnection.Open();
            sqlCommand = new SqlCommand("Stok_Güncelle", sqlConnection);

            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("Stok_Id", id);
            sqlCommand.Parameters.AddWithValue("Stok_Adedi", form["s_adedi"]);
            sqlCommand.Parameters.AddWithValue("Giris_Tarihi", form["s_giris_tarihi"]);
            sqlCommand.Parameters.AddWithValue("Bitis_Tarihi", form["s_bitis_tarihi"]);
            sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();

            sqlConnection.Open();
            sqlCommand = new SqlCommand("StokId", sqlConnection);

            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("Stok_Id", id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);
            ViewBag.emprecord = dataSet.Tables[0];

            sqlConnection.Close();



            return View();
        }
    }
}