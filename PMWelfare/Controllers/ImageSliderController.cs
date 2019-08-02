using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PMWelfare.Models;

namespace PMWelfare.Controllers
{
    public class ImageSliderController : Controller
    {
        private welfare db = new welfare();
        // GET: ImageSlider
        public ActionResult Pics()
        {
            return View();
        }
       
        public ActionResult UploadSliderImage()
        {
            List<Slider> sliderimages = new List<Slider>();
            string CS = ConfigurationManager.ConnectionStrings["welfare"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("Select TOP 4 ID,Name,FileSize,FilePath from Sliders", con);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Slider slider = new Slider();
                    slider.ID = Convert.ToInt32(rdr["ID"]);
                    slider.Name = rdr["Name"].ToString();
                    slider.FileSize = Convert.ToInt32(rdr["FileSize"]);
                    slider.FilePath = rdr["FilePath"].ToString();

                    sliderimages.Add(slider);
                }
            }
            return View(sliderimages);
        }
        [HttpGet]
        public ActionResult Gallery()
        {
            List<Slider> sliderimages = new List<Slider>();
            string CS = ConfigurationManager.ConnectionStrings["welfare"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("Select * from Sliders", con);
                
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Slider slider = new Slider();
                    slider.ID = Convert.ToInt32(rdr["ID"]);
                    slider.Name = rdr["Name"].ToString();
                    slider.FileSize = Convert.ToInt32(rdr["FileSize"]);
                    slider.FilePath = rdr["FilePath"].ToString();

                    sliderimages.Add(slider);
                }
            }
            return View(sliderimages);
        }
        [HttpPost]
        public ActionResult Gallery(HttpPostedFileBase fileupload)
        {
            try {
                if (fileupload != null)
                {
                    string fileName = Path.GetFileName(fileupload.FileName);
                    int fileSize = fileupload.ContentLength;
                    int Size = fileSize / 1000;
                    fileupload.SaveAs(Server.MapPath("~/ImageSlider/" + fileName));

                    string CS = ConfigurationManager.ConnectionStrings["welfare"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(CS))
                    {
                        SqlCommand cmd = new SqlCommand("insert into Sliders(Name,FileSize,FilePath)values(@Name, @FileSize, @FilePath)  ", con);

                        con.Open();
                        cmd.Parameters.AddWithValue("@Name", fileName);
                        cmd.Parameters.AddWithValue("@FileSize", Size);
                        cmd.Parameters.AddWithValue("FilePath", "~/ImageSlider/" + fileName);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception exp)
            {
                Console.WriteLine(exp);
            }
            return RedirectToAction("Gallery");
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider deposit = db.Sliders.Find(id);
            if (deposit == null)
            {
                return HttpNotFound();
            }
            return View(deposit);
        }

        // POST: Deposits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Slider deposit = db.Sliders.Find(id);
            db.Sliders.Remove(deposit);
            db.SaveChanges();
            return RedirectToAction("Gallery");
        }
    }
}