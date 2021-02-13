using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Controllers
{

    [Route("api/images")]
    [ApiController]
    public class ImageController : Controller
    {

        [HttpPost]
        public ActionResult UploadImage(FileUploadModel upload)
        {

            if (upload == null) return null;
            if (new FileExtensionContentTypeProvider().TryGetContentType(upload.FileName, out var contentType))
            {
                if (contentType == "image/gif" || contentType == "image/jpeg" || contentType == "image/png")
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                    if (!System.IO.Directory.Exists(path))
                    {
                        System.IO.Directory.CreateDirectory(path); //Create directory if it doesn't exist
                    }
                    var fileName = Path.GetFileName(upload.FileName).ToLower();
                    string imgPath = Path.Combine(path, fileName);

                    System.IO.File.WriteAllBytes(imgPath, upload.FileBytes);
                    string imageUrl = "https://localhost:44381/images/" + fileName;
                    return Ok(new { imageUrl });
                }
                else
                {
                    var NotImageMessage = "please choose a picture";
                    dynamic NotImage = JsonConvert.DeserializeObject("{ 'uploaded': 0, 'error': { 'message': \"" + NotImageMessage + "\"}}");
                    return Json(NotImage);
                }
            }
            return UnprocessableEntity(new { Message = $"Cannot determine content type for file '{upload.FileName}'." });
        }
    }
}