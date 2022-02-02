using MVC_Bakkal.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MVC_Bakkal.Controllers
{
    public class ÜrünController : Controller
    {
        SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["BakkalDb"].ConnectionString);
        SqlCommand sqlCommand;

        Ürün ürün;

        public ÜrünController()
        {
            ürün = new Ürün();
           
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
            
            ürün.kategori_id = Convert.ToInt32(form["k_id"]);
            ürün.u_adi = form["u_adi"];
            ürün.u_barkodu = form["u_barkodu"];
            ürün.u_uretim_tarihi = Convert.ToDateTime(form["üretim_tarihi"].ToString());

            ürün.u_tuketim_tarihi = Convert.ToDateTime(form["tüketim_tarihi"].ToString());
            ürün.u_fiyat = Convert.ToUInt16(form["fiyat"]);
            ürün.u_agirlik = Convert.ToUInt16(form["agirlik"]);
            ürün.stok_id = Convert.ToInt32(form["stok_id"]);
            ürün.marka_id = Convert.ToInt32(form["marka_id"]);
            ürün.u_rengi = form["renk"];

            sqlConnection.Open();
            sqlCommand = new SqlCommand("Ürün_Ekle", sqlConnection);

            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("Ürün_Adı", ürün.u_adi);
            sqlCommand.Parameters.AddWithValue("Ürün_Barkodu", ürün.u_barkodu);
            sqlCommand.Parameters.AddWithValue("Üretim_Tarihi", ürün.u_uretim_tarihi);
            sqlCommand.Parameters.AddWithValue("Tüketim_Tarihi", ürün.u_tuketim_tarihi);
            sqlCommand.Parameters.AddWithValue("Fiyat", ürün.u_fiyat);
            sqlCommand.Parameters.AddWithValue("Ağırlık", ürün.u_agirlik);
            sqlCommand.Parameters.AddWithValue("Renk", ürün.u_rengi);
            sqlCommand.Parameters.AddWithValue("Stok_Id", ürün.stok_id);
            sqlCommand.Parameters.AddWithValue("Kategori_Id", ürün.kategori_id);
            sqlCommand.Parameters.AddWithValue("Marka_Id", ürün.marka_id);

            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();



            return RedirectToAction("List", "Ürün");

        }
        //Veri tabanında yazılmış olan listeleme prosedürü çalıştırılarak veri tabınından veriler çekilir
    
        public ActionResult List()
        {
            sqlConnection.Open();
            sqlCommand = new SqlCommand("Ürün_List", sqlConnection);

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
            sqlCommand = new SqlCommand("Ürün_Sil", sqlConnection);

            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("Ürün_Id", id);
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();

            return RedirectToAction("List", "Ürün");


        }

        //Güncelleme işlemi yapılırken inputlarda default olarak daha önceki değer bulunması için roottaki id değerini alır ve bu id değerine sahip satırı döndürür.
        public ActionResult Update(int id)
        {
            sqlConnection.Open();
            sqlCommand = new SqlCommand("ÜrünId", sqlConnection);

            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("UrunId", id);
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
            sqlCommand = new SqlCommand("Ürün_Güncelle", sqlConnection);

            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("Ürün_Id", id);
            sqlCommand.Parameters.AddWithValue("Ürün_adı", form["u_adi"]);
            sqlCommand.Parameters.AddWithValue("Ürün_Barkodu", form["u_barkodu"]);
            sqlCommand.Parameters.AddWithValue("Üretim_Tarihi", form["üretim_tarihi"]);
            sqlCommand.Parameters.AddWithValue("Tüketim_Tarihi", form["tüketim_tarihi"]);
            sqlCommand.Parameters.AddWithValue("Fiyat", form["fiyat"]);
            sqlCommand.Parameters.AddWithValue("Ağırlık", form["agirlik"]);
            sqlCommand.Parameters.AddWithValue("Renk", form["renk"]);
            sqlCommand.Parameters.AddWithValue("Stok_Id", form["stok_id"]);
            sqlCommand.Parameters.AddWithValue("Kategori_Id", form["k_id"]);
            sqlCommand.Parameters.AddWithValue("Marka_Id", form["marka_id"]);
            sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();

            sqlConnection.Open();
            sqlCommand = new SqlCommand("ÜrünId", sqlConnection);

            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("UrunId", id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);
            ViewBag.emprecord = dataSet.Tables[0];
            sqlConnection.Close();
            return View();
        }
    }
}