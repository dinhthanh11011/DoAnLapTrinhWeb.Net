using DreamTeam.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace DreamTeam.Support
{
    public class support
    {
        public static string UPLOAD_FOLDER_NAME = "/Uploads";

        public const string ACCOUNT_MANAGE_PERMISSION = "account_manager";
        public const string PRODUCT_MANAGE_PERMISSION = "product_manager";
        public const string STORE_MANAGE_PERMISSION = "store_manager";


        private static string removeVietnameseTone(string text)
        {
            string result = text.Trim().ToLower();
            result = Regex.Replace(result, "à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ|/g", "a");
            result = Regex.Replace(result, "è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ|/g", "e");
            result = Regex.Replace(result, "ì|í|ị|ỉ|ĩ|/g", "i");
            result = Regex.Replace(result, "ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ|/g", "o");
            result = Regex.Replace(result, "ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ|/g", "u");
            result = Regex.Replace(result, "ỳ|ý|ỵ|ỷ|ỹ|/g", "y");
            result = Regex.Replace(result, "đ", "d");
            return result;
        }

        public static string parseToCode(string text)
        {
            text = removeVietnameseTone(text) + "_" + DateTime.Now.ToString("ddMMyyyyHHmmss");
            return text.Trim().Replace(" ", "-");
        }

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