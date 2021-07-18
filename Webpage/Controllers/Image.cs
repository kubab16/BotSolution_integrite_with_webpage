using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Webpage.Models;

namespace Webpage.Controllers
{
    public class Image : Controller
    {
        private readonly Context _context;
        public Image(Context context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet("avatar")]
        public async Task<ActionResult> UserAvatarAsync(ulong id)
        {
            var item = await _context.users.FirstOrDefaultAsync(x => x.id == id);
            byte[] buffer = item.avatar;
            return File(buffer, "image/jpg", string.Format("{0}.jpg", id));
        }

        [AllowAnonymous]
        [HttpGet("SeriesImage")]
        public async Task<ActionResult> SeriesImageAsync(ulong id)
        {
            var item = await _context.users.FirstOrDefaultAsync(x => x.id == id);
            byte[] buffer = item.avatar;
            return File(buffer, "image/jpg", string.Format("{0}.jpg", id));
        }

        [HttpPost]
        async Task<ActionResult> UploadAvatar(ulong userId, IFormFile files)
        {
            if (files != null)
            {
                if (files.Length > 0)
                {
                    //Getting FileName
                    var fileName = Path.GetFileName(files.FileName);
                    //Getting file Extension
                    var fileExtension = Path.GetExtension(fileName);
                    // concatenating  FileName + FileExtension
                    var newFileName = String.Concat(Convert.ToString(Guid.NewGuid()), fileExtension);

                    var objfiles = new Files()
                    { 
                        DocumentId = 0,
                        Name = newFileName,
                        FileType = fileExtension,
                        CreatedOn = DateTime.Now
                    };

                    using (var target = new MemoryStream())
                    {
                        files.CopyTo(target);
                        objfiles.DataFiles = target.ToArray();
                    }

                    var item = await _context.users.FirstOrDefaultAsync(x => x.id == userId);
                    _context.SaveChanges();
                    
                }
            }
            return View();
        }
    }
}