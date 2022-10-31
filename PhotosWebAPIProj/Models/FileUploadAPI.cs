using Microsoft.AspNetCore.Http;
using PhotosWebAPIProj.Models;
using System.Collections.Generic;

namespace UploadImageWithData8777
{
    public class FileUploadAPI
    {
        public int ImgID { get; set; }
        public string? Customers { get; set; }
        public IFormFile? files { get; set; }
        public string ImgName { get; set; }
    }
    public class common
    {
        public FileUploadAPI _fileAPI { get; set; }
        public Customer _Customer { get; set; }
        public List<Customer> LstCustomer { get; set; }
    }
}