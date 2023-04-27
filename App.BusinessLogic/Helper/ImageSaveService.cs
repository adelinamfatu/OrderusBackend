using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BusinessLogic.Helper
{
    public static class ImageSaveService
    {
        public static async void SaveImageAsync(string fileName, Stream fileStream)
        {
            var localPath = @"C:/OrderusFiles/" + fileName;

            using (var outputStream = new FileStream(localPath, FileMode.Create))
            {
                await fileStream.CopyToAsync(outputStream);
            }
        }
    }
}
