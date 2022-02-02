using MVC_Bakkal.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVC_Bakkal.Controllers
{
    public class KategoriController : Controller
    {
        SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["BakkalDb"].ConnectionString);
        SqlCommand sqlCommand;

        Kategori category;

        public KategoriController()
        {

            category = new Kategori();
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
            category.k_adi = form["k_adi"];

            sqlConnection.Open();
            sqlCommand = new SqlCommand("Kategori_Ekle", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("Kategori_Adı", category.k_adi);

            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
           
            
            return RedirectToAction("List", "Kategori");

        }
        //Veri tabanında yazılmış olan listeleme prosedürü çalıştırılarak veri tabınından veriler çekilir
        public ActionResult List()
        {
            sqlConnection.Open();
            sqlCommand = new SqlCommand("Kategori_List", sqlConnection);
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
            sqlCommand = new SqlCommand("Kategori_Sil", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("Kategori_Id", id);
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();

            return RedirectToAction("List", "Kategori");


        }
        //Güncelleme işlemi yapılırken inputlarda default olarak daha önceki değer bulunması için roottaki id değerini alır ve bu id değerine sahip satırı döndürür.
        public ActionResult Update(int id)
        {
            sqlConnection.Open();
            sqlCommand = new SqlCommand("KategoriId", sqlConnection);

            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("id", id);
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

            sqlCommand = new SqlCommand("Kategori_Güncelle", sqlConnection);

            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("Kategori_Id", id);
            sqlCommand.Parameters.AddWithValue("Kategori_Adı", form["k_adi"]);
            sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();

            sqlConnection.Open();
            sqlCommand = new SqlCommand("KategoriId", sqlConnection);

            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("id", id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);
            ViewBag.emprecord = dataSet.Tables[0];

            sqlConnection.Close();
            return View();
        }



    }
}