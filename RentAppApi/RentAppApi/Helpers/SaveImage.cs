using System;
using System.Collections;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;

namespace RentAppApi.Helpers
{
    public class SaveImage
    {
        //[ForeignKey("ProductId")]
        public static string Save(byte[]  image, TypeImage type)
        {
            try
            {
                var folderName = (type == TypeImage.User) ? Path.Combine("User") : Path.Combine("Products");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                var fileName = Guid.NewGuid() + ".png";
                var fullPath = Path.Combine(pathToSave, fileName);
                var dbPath = Path.Combine(folderName, fileName);
                Stream stm = new MemoryStream(image);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    stm.CopyTo(stream);
                }
                return fileName;
            }
            catch (Exception ex)
            {
                return null;
            }           
        }

        public enum TypeImage
        {
            User,
            Product
        }
    }
}

