﻿namespace PustokBook.Areas.Admin.Helpers
{
    public static class FileExtension
    {
        public static bool IsValidSize(this IFormFile file, float kb = 20) =>
            file.Length >= kb * 1024000;

        public static bool IsCorrectType(this IFormFile file, string contentType = "image") => file.ContentType.Contains(contentType);

        public static async Task<string> SaveAsync(this IFormFile file, string path)
        {
            string extension = Path.GetExtension(file.FileName);
            string fileName = Path.GetFileNameWithoutExtension(file.FileName).Length > 32 ? file.FileName.Substring(0, 32) :
                Path.GetFileNameWithoutExtension(file.FileName);

            fileName = Path.Combine(path, Path.GetRandomFileName() + fileName + extension);

            using (FileStream fs = File.Create(Path.Combine(PathConstants.RootPath, fileName)))
            {
                await file.CopyToAsync(fs);
            }
            return fileName;
        }

        public static async Task UpdateAsync(this IFormFile file, string lastImageUrl)
        {
            File.Delete(lastImageUrl);
            using (FileStream fs = File.Create(lastImageUrl))
            {
                await file.CopyToAsync(fs);
            }
        }
    }
}
