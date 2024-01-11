namespace WorkSearch.Helpers
{
    public static class FileHelper
    {
      public static string? SavePhoto(IWebHostEnvironment hostingEnvironment, IFormFile? file)
        {
            
                string? photoName = null;

                if (file != null)
                {
                    var photoFolder = Path.Combine(hostingEnvironment.WebRootPath, "photo");
                    photoName = Guid.NewGuid().ToString() + file.FileName;
                    var filePath = Path.Combine(photoFolder, photoName);

                    using (var stream = File.Open(filePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }

                return photoName;
            
        }

        public static void DeletePhoto(IWebHostEnvironment hostingEnvironment, string? photoName)
        {
            try
            {
                if (photoName == null) return;

                var photoFolder = Path.Combine(hostingEnvironment.WebRootPath, "photo");
                var filePath = Path.Combine(photoFolder, photoName);

                File.Delete(filePath);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
