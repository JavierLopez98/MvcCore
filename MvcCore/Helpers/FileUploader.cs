using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.Helpers
{
    public class FileUploader
    {
        PathProvider provider;

        public FileUploader(PathProvider provider)
        {
            this.provider = provider;
        }

        public async Task<String> UploadFileAsync(IFormFile fichero,Folders folder)
        {
            String filename = fichero.FileName;
            String ruta = this.provider.MapPath(filename, folder);

            using(var stream=new FileStream(ruta, FileMode.Create))
            {
                await fichero.CopyToAsync(stream);
            }
            return ruta;
        }
    }
}
