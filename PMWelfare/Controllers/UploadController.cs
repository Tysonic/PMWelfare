using System;
using OfficeOpenXml; 
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PMWelfare.Models;

namespace PMWelfare.Controllers
{
    public class UploadController : Controller
    {
        // GET: Upload
        public ActionResult ImportExcel()
        {
            List<string> Table = new List<string>()
            {
            };
            //Table.Add("Subscriptions");
            //Table.Add("Members");
            ViewBag.m = Table;
            

            Upload ttt = new Upload() {

            };
            //ttt.TableName = Table;
            
            return View();
        }

      
        [HttpPost]
        public ActionResult ImportExcel(FormCollection formCollection)
        {
            Upload s = new Upload();
            
            //List<string> TableNames = new List<string>();
            //TableNames.Add("Subscriptions");
            //TableNames.Add("Members");



            var usersList = new List<Subscription>();
            var Mb = new List<Member>();
            if (Request != null)
            {
               string Selected = Request.Form["SelectedTable"];

              HttpPostedFileBase file = Request.Files["UploadedFile"];
                if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                {
                    string fileName = file.FileName;
                    string fileContentType = file.ContentType;
                    byte[] fileBytes = new byte[file.ContentLength];
                    var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                    using (var package = new ExcelPackage(file.InputStream))
                    {
                        var currentSheet = package.Workbook.Worksheets;
                        var workSheet = currentSheet.First();
                        var noOfCol = workSheet.Dimension.End.Column;
                        var noOfRow = workSheet.Dimension.End.Row;

                        if (Selected == "Subscriptions")
                        {

                            for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                            {
                                var user = new Subscription();
                                user.SubId = Convert.ToInt32(workSheet.Cells[rowIterator, 1].Value);
                                user.UserName = workSheet.Cells[rowIterator, 2].Value.ToString();
                                user.SubMonth = Convert.ToByte(workSheet.Cells[rowIterator, 3].Value);
                                user.SubYear = Convert.ToInt32(workSheet.Cells[rowIterator, 4].Value);
                                user.Amount = Convert.ToDecimal(workSheet.Cells[rowIterator, 5].Value);

                                usersList.Add(user);
                            }
                            using (welfare db = new welfare())
                            {
                                foreach (var item in usersList)
                                {
                                    db.Subscriptions.Add(item);
                                }
                                db.SaveChanges();
                            }
                        }

                        if (Selected == "Members")
                        {

                            for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                            {
                                var user = new Member();
                                user.UserName = workSheet.Cells[rowIterator, 1].Value.ToString();
                                user.FirstName = workSheet.Cells[rowIterator, 2].Value.ToString();
                                user.OtherName = workSheet.Cells[rowIterator, 3].Value.ToString();
                                user.Email = workSheet.Cells[rowIterator, 4].Value.ToString();
                                user.DOB = Convert.ToDateTime(workSheet.Cells[rowIterator, 5].Value);
                                user.MemberStatus =Convert.ToInt32(workSheet.Cells[rowIterator, 6].Value);
                                //user.IsAdmin = Convert.ToBoolean(workSheet.Cells[rowIterator, 7]);
                                user.CreatedBy = "nicho";
                                user.CreatedAt = DateTime.Now; 

                                Mb.Add(user);
                            }
                            using (welfare db = new welfare())
                            {
                                foreach (var item in Mb)
                                {
                                    db.Members.Add(item);
                                }
                                db.SaveChanges();
                            }
                        }



                        
                    }
                }
            }
            return View();
        }

    }
}