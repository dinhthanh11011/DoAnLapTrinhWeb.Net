using DreamTeam.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace DreamTeam.Support
{
    public class support
    {
        public static string UPLOAD_FOLDER_NAME = "/Uploads";

        public const string ACCOUNT_MANAGE_PERMISSION = "account_manager";
        public const string PRODUCT_MANAGE_PERMISSION = "product_manager";
        public const string STORE_MANAGE_PERMISSION = "store_manager";

        public static UploadViewModel uploadFile(dynamic file)
        {
            UploadViewModel fileUp = new UploadViewModel();

            fileUp.fileName = DateTime.Now.ToString("ddMMyyyyHHmmss") + Path.GetFileName(file.FileName);

            fileUp.path = Path.Combine(
                HttpContext.Current.Server.MapPath(UPLOAD_FOLDER_NAME),
                fileUp.fileName
            );
            return fileUp;
        }

        public static bool deleteImg(string fileName)
        {
            try
            {
                string path = Path.Combine(
                    HttpContext.Current.Server.MapPath(UPLOAD_FOLDER_NAME),
                    fileName
                );
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static HttpFileCollection checkFileUpLoad(HttpFileCollection files)
        {
            if (files.Count > 0 && files[0].ContentLength > 0)
                return files;
            return null;
        }
    }
}