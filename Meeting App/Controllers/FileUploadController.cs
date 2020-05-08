
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
namespace Meeting_App
{
    public class FileUploadController : ApiController
    {
        [HttpPost]
        [Route("api/FileUpload")]
        public HttpResponseMessage UploadJsonFile(string MeetingID)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var httpRequest = HttpContext.Current.Request;
            var dirPath = HttpContext.Current.Server.MapPath("~/UploadFile/"+ MeetingID);
           DirectoryInfo directoryInfo= Directory.CreateDirectory(dirPath);
            if (httpRequest.Files.Count > 0)
            {
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    var filePath = HttpContext.Current.Server.MapPath("~/UploadFile/" + directoryInfo +"\\" + postedFile.FileName);
                    postedFile.SaveAs(filePath);
                }
            }
            return response;
        }
        // This will download the file from the server
        // Pass in the filepath of where file is stored and a new file name
        [HttpGet]
        [Route("api/GetFile")]
        public HttpResponseMessage GetFile(string fileName,string MeetingID)
        {
            //Create HTTP Response.
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

            //Set the File Path.
            string filePath = HttpContext.Current.Server.MapPath("~/UploadFile/")+ MeetingID +"\\"+ fileName;

            //Check whether File exists.
            if (!File.Exists(filePath))
            {
                //Throw 404 (Not Found) exception if File not found.
                response.StatusCode = HttpStatusCode.NotFound;
                response.ReasonPhrase = string.Format("File not found: {0} .", fileName);
                throw new HttpResponseException(response);
            }

            //Read the File into a Byte Array.
            byte[] bytes = File.ReadAllBytes(filePath);

            //Set the Response Content.
            response.Content = new ByteArrayContent(bytes);

            //Set the Response Content Length.
            response.Content.Headers.ContentLength = bytes.LongLength;

            //Set the Content Disposition Header Value and FileName.
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = fileName;

            //Set the File Content Type.
            response.Content.Headers.ContentType = new MediaTypeHeaderValue(MimeMapping.GetMimeMapping(fileName));
            return response;
        }
        //[HttpGet]
        //[Route("api/Download")]
        //public HttpResponse GetFileFromServer(string FileName)
        //{
        //    string currentFilePath = HttpContext.Current.Server.MapPath("~/UploadFile/" + FileName);
        //    string newFileName = FileName + DateTime.Now.Date.ToString();
        //    HttpResponse httpresponse = HttpContext.Current.Response;
        //    httpresponse.Clear();
        //    httpresponse.Charset = "utf-8";
        //    httpresponse.ContentType = "application/json"; // mime type to suit file type
        //    httpresponse.AddHeader("content-disposition", string.Format("attachment; filename={0}", newFileName));
        //    httpresponse.BinaryWrite(File.ReadAllBytes(currentFilePath));
        //    httpresponse.End();
        //    return httpresponse;
        //}
        [HttpGet]
        [Route("api/GetFileList")]
        public string [] GetAllFiles(string MeetingID)
        {
           
           string [] folderpath = Directory.GetFiles(HttpContext.Current.Server.MapPath("~/UploadFile/"+ MeetingID));
            
            // finalResult.Add(ResultAarry);
            return folderpath;
        }

        [HttpGet]
        [Route("api/GetFileAll")]
        public List<string> GetAll(string MeetingID)
        {
            string[] folderpath;
            // bool IsfileExists = false;
            string ResultAarry = string.Empty;
            List<string> finalResult = new  List<string>();
            folderpath = Directory.GetFiles(HttpContext.Current.Server.MapPath("~/UploadFile/"+ MeetingID +"\\"));
            foreach (string path in folderpath)
            {
                

                if (File.Exists(path))
                {
                    // This path is a file
                    ResultAarry= Splitfile(path);
                    finalResult.Add(ResultAarry);
                    
                }
            }
            
           
            return finalResult;
        }

        public static string Splitfile(string path)
        {
            string result= string.Empty;
            string [] splitArray= path.Split('\\');
            foreach (var item in splitArray)
            {
                bool s = item.Contains(".");
                if (s==true)
                {
                     result = item;
                }
            }
            return result;
        }
        [HttpGet]
        [Route("api/Search")]
        public bool Search(string FileName,string MeetingID)
        {
            bool IsfileExists = false;
            string[] folderpath = Directory.GetFiles(HttpContext.Current.Server.MapPath("~/UploadFile/")+ MeetingID+"/");

            string fileExists = HttpContext.Current.Server.MapPath("~/UploadFile/") + FileName;
            try
            {
                foreach (string path in folderpath)
                {
                    if (path == fileExists)
                    {
                        if (File.Exists(path))
                        {
                            // This path is a file
                            ProcessFile(path);
                            IsfileExists = true;
                        }
                        else if (Directory.Exists(path))
                        {
                            // This path is a directory
                            ProcessDirectory(path);
                            IsfileExists = true;
                        }
                        else
                        {
                            IsfileExists = false;

                        }
                    }
                }
                               
            }
            catch (Exception ex)
            {

                return false;
            }


            if (IsfileExists == true)
            {
                return true;
            }
            //  GetFiles(path, FileName);
            return false;
        }
    
    // Process all files in the directory passed in, recurse on any directories
    // that are found, and process the files they contain.
    public static void ProcessDirectory(string targetDirectory)
    {
        // Process the list of files found in the directory.
        string[] fileEntries = Directory.GetFiles(targetDirectory);
        foreach (string fileName in fileEntries)
            ProcessFile(fileName);

        // Recurse into subdirectories of this directory.
        string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
        foreach (string subdirectory in subdirectoryEntries)
            ProcessDirectory(subdirectory);
    }

    // Insert logic for processing found files here.
    public static void ProcessFile(string path)
    {
        Console.WriteLine("Processed file '{0}'.", path);
    }
}
}


