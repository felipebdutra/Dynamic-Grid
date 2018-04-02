using FileGenerator.Constants;
using FileGenerator.Enum;
using FileGenerator.Facade;
using FileGenerator.Helper;
using FileGenerator.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FileGenerator.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public string UpdateGrid(string aTable)
        {
            try
            {
                return new GridFacade().UpdateGrid(aTable);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string UpdatePoint(string aTable, int aQntPoint, int aIndex)
        {
            try
            {
                return new GridFacade().UpdatePoint(aTable, aQntPoint, aIndex);

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void PrepareFile(string aTable, string fileType)
        {
            try
            {
                new GridFacade().PrepareFile(aTable, fileType);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string DownloadFile()
        {
            try
            {
                Response.Headers.Add("Content-type", FileHelper.File.ContentType);
                Response.Headers.Add("Content-disposition", FileHelper.File.ContentDisposition);
                Response.Headers.Add("Pragma.", "no-cache");
                Response.Cache.SetNoStore();
                return FileHelper.File.StrFile;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                try
                {
                    if (!FileHelper.IsExtensionValid(file.FileName))
                        ViewBag.ErrorMessage = ErrorMessagesConstant.ExtensionInvalid;
                    
                    ViewBag.UploadedTable = new GridFacade().GetFileBytes(file);

                    return View("Index");
                }
                catch (Exception)
                {
                    ViewBag.ErrorMessage = ErrorMessagesConstant.WrongFileUpload;
                }
            }
            else
            {
                ViewBag.ErrorMessage = ErrorMessagesConstant.NotSpecifiedFile;
            }

            return View("Index");
        }
    }
}