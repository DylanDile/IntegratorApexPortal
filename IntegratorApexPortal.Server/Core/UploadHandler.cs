namespace IntegratorApexPortal.Server.Core
{
    public class UploadHandler
    {
        public string Upload(IFormFile formFile)
        {
            List<string> validExtensions = new List<string> { ".txt", ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".png", ".jpg", ".jpeg" };
            if (formFile == null || formFile.Length == 0)
            {
                return "File is empty";
            }

            if (!validExtensions.Contains(Path.GetExtension(formFile.FileName).ToLowerInvariant()))
            {
                return "Invalid file extension";
            }

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName);
            string path = Path.Combine("C:\\Data\\Uploads");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            using FileStream fileStream = new FileStream(Path.Combine(path, fileName), FileMode.Create);
            formFile.CopyTo(fileStream);

            return fileName;
        }
    }
}
