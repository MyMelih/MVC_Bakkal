using MVC_Bakkal.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;

namespace Bakkal.Controllers
{
    public class SatisController : Controller
    {
        SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["BakkalDb"].ConnectionString);
        SqlCommand sqlCommand;

       
        Satis satis;

        public SatisController()
        {
            satis = new Satis();
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
            satis.Kullanici_id = Convert.ToInt32(form["k_id"]);
            satis.Miktar = Convert.ToUInt16(form["miktar"]);
            satis.Stais_Tarihi = Convert.ToDateTime(form["s_tarih"]);
            satis.Durum = form["durum"].Trim();
            satis.Fiyat = Convert.ToUInt16(form["fiyat"]);
            satis.İskonto = Convert.ToUInt16(form["iskonto"]);
            satis.Urun_Id = Convert.ToUInt16(form["ürün_id"]);

            sqlConnection.Open();
            sqlCommand = new SqlCommand("Satis_Ekle", sqlConnection);

            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("Satis_Tarihi", satis.Stais_Tarihi);
            sqlCommand.Parameters.AddWithValue("Durum", satis.Durum);
            sqlCommand.Parameters.AddWithValue("Kullanici_id", satis.Kullanici_id);

            sqlCommand.Parameters.AddWithValue("Miktar", satis.Miktar);
            sqlCommand.Parameters.AddWithValue("Fiyat", satis.Fiyat);
            sqlCommand.Parameters.AddWithValue("İskonto", satis.İskonto);
            sqlCommand.Parameters.AddWithValue("Urun_Id", satis.Urun_Id);

            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();

            return RedirectToAction("List", "Satis");
        }
        //Veri tabanında yazılmış olan listeleme prosedürü çalıştırılarak veri tabınından veriler çekilir
        public ActionResult List()
        {
            sqlConnection.Open();
            sqlCommand = new SqlCommand("Satis_List", sqlConnection);

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
            sqlCommand = new SqlCommand("Satis_Sil", sqlConnection);

            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("Satis_Id", id);
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();

            return RedirectToAction("List", "Satis");


        }
       
        public ActionResult Update(int id)
        {
            sqlConnection.Open();
            sqlCommand = new SqlCommand("SatisId", sqlConnection);

            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("Satis_Id", id);
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
            satis.Kullanici_id = Convert.ToInt32(form["k_id"]);
            satis.Miktar = Convert.ToUInt16(form["miktar"]);
            satis.Stais_Tarihi = Convert.ToDateTime(form["s_tarih"]);
            satis.Durum = form["durum"].Trim();
            satis.Fiyat = Convert.ToUInt16(form["fiyat"]);
            satis.İskonto = Convert.ToUInt16(form["iskonto"]);
            satis.Urun_Id = Convert.ToUInt16(form["ürün_id"]);



            sqlCommand = new SqlCommand("Satis_Güncelle", sqlConnection);

            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("Satis_Tarihi", satis.Stais_Tarihi);
            sqlCommand.Parameters.AddWithValue("Durum", satis.Durum);
            sqlCommand.Parameters.AddWithValue("Kullanici_id", satis.Kullanici_id);

            sqlCommand.Parameters.AddWithValue("Miktar", satis.Miktar);
            sqlCommand.Parameters.AddWithValue("Fiyat", satis.Fiyat);
            sqlCommand.Parameters.AddWithValue("İskonto", satis.İskonto);
            sqlCommand.Parameters.AddWithValue("Urun_Id", satis.Urun_Id);
            sqlCommand.Parameters.AddWithValue("Satis_Id", id);


            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();


            sqlConnection.Open();
            sqlCommand = new SqlCommand("SatisId", sqlConnection);

            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("Satis_Id", id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);
            ViewBag.emprecord = dataSet.Tables[0];

            sqlConnection.Close();
            return View();
        }
    }
}