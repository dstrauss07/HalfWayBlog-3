using DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Newtonsoft.Json;
using System;
using System.IO;
using WebApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace WebApplication.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Route("api/images")]
    [ApiController]
    public class ImageController : Controller
    {
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly IOptions<AppSettings> _appSettings;

        public ImageController(IBlogPostRepository blogPostRepository, IOptions<AppSettings> appSettings)
        {
            _blogPostRepository = blogPostRepository;
            _appSettings = appSettings;
        }

        [HttpPost]
        public ActionResult UploadImage(FileUploadModel upload)
        {
            if (upload == null) return null;
            if (new FileExtensionContentTypeProvider().TryGetContentType(upload.FileName, out var contentType))
            {
                if (contentType == "image/gif" || contentType == "image/jpeg" || contentType == "image/png")
                {
                    String imgUrl = "wwwroot/images/" + upload.imagePostId.ToString() + "/";
                    var path = Path.Combine(Directory.GetCurrentDirectory(), imgUrl);
                    if (!System.IO.Directory.Exists(path))
                    {
                        System.IO.Directory.CreateDirectory(path); //Create directory if it doesn't exist
                    }
                    var fileName = Path.GetFileName(upload.FileName).ToLower();
                    if (upload.isMain)
                    {
                        string extension = fileName.Substring(fileName.IndexOf(@".") + 1);
                        fileName = "Main." + extension;
                    }
                    string imgPath = Path.Combine(path, fileName);
                    using var imageUpload = Image.Load(upload.FileBytes);
                    if (upload.isMain)
                    {                   
                         imageUpload.Mutate(x => x.Resize(1200, 800));
                    }
                    else
                    {
                        if (imageUpload.Width > 2000 || imageUpload.Height > 2000)
                        {
                            var origWidth = imageUpload.Width;
                            var origHeight = imageUpload.Height;
                            if (origWidth >= origHeight)
                            {
                                var updatedHeight = (2000 / origWidth) * origHeight;
                                imageUpload.Mutate(x => x.Resize(2000, updatedHeight));
                            }
                            else
                            {
                                var updatedWidth = (2000 / origHeight) * origWidth;
                                imageUpload.Mutate(x => x.Resize(updatedWidth, 2000));
                            }
                        }
                    }

                    imageUpload.Save(imgPath);
                    //   System.IO.File.WriteAllBytes(imgPath, imageUpload.Metadata);
                    string imageUrl = _appSettings.Value.SiteUrl + "/images/" + upload.imagePostId.ToString() + "/" + fileName;
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