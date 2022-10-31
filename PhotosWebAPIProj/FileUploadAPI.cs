using Microsoft.AspNetCore.Http;
using PhotosWebAPIProj.Models;
using System.Collections.Generic;

namespace PhotosWebAPIProj
{
    public class FileUploadAPI
    {
        public IFormFile? files { get; set; }
        public string fileName { get; set; }
        public string base64ContentFile { get; set; }
    }
    public class common
    {
        public List<FileUploadAPI> _fileAPI { get; set; }
    }


}
