using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PhotosWebAPIProj.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Linq;

namespace PhotosWebAPIProj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageUploadController : ControllerBase
    {
        public static IWebHostEnvironment _environment;
        public ImageUploadController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        [HttpPost]
        public Task<common> Post([FromForm] FileUploadAPI objFile)
        {
            common obj = new common();
            obj._fileAPI = new List<FileUploadAPI>();
            try
            {
                if (objFile.files.Length > 0)
                {
                    if (!Directory.Exists(_environment.WebRootPath + "\\Upload"))
                    {
                        Directory.CreateDirectory(_environment.WebRootPath + "\\Upload\\");
                    }
                    using (FileStream filestream = System.IO.File.Create(_environment.WebRootPath + "\\Upload\\" + objFile.files.FileName))
                    {
                        objFile.files.CopyTo(filestream);
                        filestream.Flush();
                        //  return "\\Upload\\" + objFile.files.FileName;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
                throw;
            }
            return Task.FromResult(obj);
        }

        [HttpGet]
        [Route("AllFiles")]

        public Task<common> GetAllFiles()
        {
            common obj = new common();
            obj._fileAPI = new List<FileUploadAPI>();
            try
            {
                string filePath = _environment.WebRootPath + "\\Upload\\";
                string[] files = Directory.GetFiles(filePath);
                if (files.Length > 0)
                {
                    foreach (var fileName in files)
                    {
                        FileUploadAPI fileUploadAPI = new FileUploadAPI();
                        filePath = Path.Combine(filePath, fileName);
                        Byte[] bytes = System.IO.File.ReadAllBytes(filePath);
                        String file = Convert.ToBase64String(bytes);
                        fileUploadAPI.base64ContentFile = file;
                        fileUploadAPI.fileName = fileName;
                        obj._fileAPI.Add(fileUploadAPI);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
                throw;
            }
            return Task.FromResult(obj);
        }

        [HttpGet]
        [Route("AllPhotos")]
        public Task<common> GetAllPhotos()
        {
            common obj = new common();
            obj._fileAPI = new List<FileUploadAPI>();
            try
            {
                string filePath = _environment.WebRootPath + "\\Upload\\";
                List<string> imageFiles = new List<string>();
                string[] files = Directory.GetFiles(filePath);
                foreach (string filename in files)
                {
                    if (Regex.IsMatch(filename, @"\.jpg$|\.png$|\.gif$"))
                        imageFiles.Add(filename);
                }
                if (imageFiles.Count > 0)
                {
                    foreach (var fileName in imageFiles)
                    {
                        FileUploadAPI fileUploadAPI = new FileUploadAPI();
                        filePath = Path.Combine(filePath, fileName);
                        Byte[] bytes = System.IO.File.ReadAllBytes(filePath);
                        String file = Convert.ToBase64String(bytes);
                        fileUploadAPI.base64ContentFile = file;
                        fileUploadAPI.fileName = fileName;
                        obj._fileAPI.Add(fileUploadAPI);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
                throw;
            }
            return Task.FromResult(obj);
        }


        [HttpDelete]
        [Route("DeleteFile")]
        public Task<common> RemoveFile(string fileName)
        {
            common obj = new common();
            obj._fileAPI = new List<FileUploadAPI>();
            try
            {
                FileUploadAPI fileUploadAPI = new FileUploadAPI();
                string filePath = _environment.WebRootPath + "\\Upload\\"+ fileName;

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                    fileUploadAPI.fileName = fileName;
                    obj._fileAPI.Add(fileUploadAPI);
                }
                return Task.FromResult(obj);
            }
            catch (Exception ext)
            {
                Debug.Write(ext.Message);
                throw ext;
            }
        }
    }
}
