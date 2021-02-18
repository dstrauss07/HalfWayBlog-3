using Microsoft.AspNetCore.Http;
using System.IO;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    [DataContract]
    public class FileUploadModel
    {
        [DataMember(Name = "fileName")]
        public string FileName { get; set; }

        [DataMember(Name = "fileBytes")]
        public byte[] FileBytes { get; set; }

        [DataMember(Name = "isMain")]
        public bool isMain { get; set; }

        [DataMember(Name = "imagePostId")]
        public int imagePostId { get; set; }
    }
}
