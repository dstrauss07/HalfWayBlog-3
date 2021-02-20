﻿using DataAccessLayer.Interfaces;
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
using BlogLibrary;

namespace WebApplication.Controllers
{
    [Route("api/images")]
    [ApiController]
    public class ImageController : Controller
    {
        private readonly IBlogPostRepository _blogPostRepository;
        public ImageController(IBlogPostRepository blogPostRepository)
        {
            _blogPostRepository = blogPostRepository;
        }

            [HttpPost]
        public async Task<ActionResult> UploadImage(FileUploadModel upload)
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

                    System.IO.File.WriteAllBytes(imgPath, upload.FileBytes);
                    string imageUrl = "https://localhost:44381/images/"+ upload.imagePostId.ToString() + "/" + fileName;
                    //if (upload.isMain)
                    //{
                    //    BlogPost blogPostToUpdate = await _blogPostRepository.GetByIdAsync(upload.imagePostId);
                    //    blogPostToUpdate.ImageUrl = imageUrl;
                    //    await _blogPostRepository.UpdateAsync(blogPostToUpdate);
                    //}
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