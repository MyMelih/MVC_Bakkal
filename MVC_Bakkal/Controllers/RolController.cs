﻿using MVC_Bakkal.Models;
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
    public class RolController : Controller
    {
        SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["BakkalDb"].ConnectionString);
        SqlCommand sqlCommand;
        
        Rol rol;

        public RolController()
        {
           
            rol = new Rol();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View();

        }
        //Veri tabanında yazılmış olan listeleme prosedürü çalıştırılarak veri tabınından veriler çekilir
        public ActionResult List()
        {

            sqlConnection.Open();
            sqlCommand = new SqlCommand("Rol_List", sqlConnection);

            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

        
            ViewBag.table = dataSet.Tables[0];

            return View(dataSet);
        }


        //Formdan gelen bilgileri bir satış nesnesinin özelliklerine atayarak satış nesnesini özelliklerin doldurur.
        //Veri tabanında yazılmış ilgili ekle prosedürene parameterleri aracılığıyla gönderilir
        //Daha sonra komut satırları çalışır
        [HttpPost]
        public ActionResult Add(FormCollection form)
        {
            rol.r_adi = form["r_adi"];

            sqlConnection.Open();

            sqlCommand = new SqlCommand("Rol_Ekle", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("Rol_Adı", rol.r_adi);
            sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();

            return RedirectToAction("List", "Rol");

        }
        //Root'ta bulunan id değeri çeker ve bu değerini silme prosedüre yollar ve bu komut satırı çalıştırılır.
        public ActionResult Delete(int id)
        {
            sqlConnection.Open();
            sqlCommand = new SqlCommand("Rol_Sil", sqlConnection);

            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("Rol_Id", id);
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();

            return RedirectToAction("List", "Rol");


        }
        //Güncelleme işlemi yapılırken inputlarda default olarak daha önceki değer bulunması için roottaki id değerini alır ve bu id değerine sahip satırı döndürür.
        public ActionResult Update(int id)
        {
            sqlConnection.Open();
            sqlCommand = new SqlCommand("RolId", sqlConnection);

            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("Rol_Id", id);
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
            sqlCommand = new SqlCommand("Rol_Güncelle", sqlConnection);

            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("Rol_Id", id);
            sqlCommand.Parameters.AddWithValue("Rol_Adı", form["r_adi"]);
            sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();

            sqlConnection.Open();
            sqlCommand = new SqlCommand("RolId", sqlConnection);

            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("Rol_Id", id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);
            ViewBag.emprecord = dataSet.Tables[0];
            
            sqlConnection.Close();


            return View();
        }
    }
}