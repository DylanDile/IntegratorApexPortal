using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IntegratorApexPortal.Server.Core;
using Microsoft.AspNetCore.StaticFiles;

namespace IntegratorApexPortal.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileManagerController : ControllerBase
    {
        [HttpPost("UploadFile")]
        public IActionResult UploadFile(IFormFile file, int generatedReturmId)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File is empty");
            }
            return Ok(new UploadHandler().Upload(file));
        }

        [HttpGet("DownloadFile")]
        public async Task<IActionResult> DownloadFile(string fileName, int generatedReturmId)
        {
            var path = Path.Combine("C:\\Data\\Uploads", fileName);      

            var provider  = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(path, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            var bytes = await System.IO.File.ReadAllBytesAsync(path);

            return File(bytes, contentType, Path.GetFileName(path));
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats officedocument.spreadsheetml.sheet"}
            };
        }
    }
}
