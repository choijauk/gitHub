using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetNote.MVC.Controllers
{
    public class UploadController : Controller
    {
        private readonly IHostingEnvironment _environment;

        public UploadController(IHostingEnvironment environment)
        {
            _environment = environment;
        }

        // default 주소 : http://www.example.com/Upload/ImageUpload
        // Route 지정시 : http://www.example.com/api/upload
        [HttpPost, Route("api/upload")]
        //public async Task<IActionResult> ImageUpload(IFormFile file)
        public IActionResult ImageUpload(IFormFile file)
        {
            // # 이미지나 파일을 업로드 할 때 필요한 구성
            // 1. path(경로) - 어디에다 저장할지 결정
            var path = Path.Combine(_environment.WebRootPath, @"images\upload");
            // 2. Name(이름) - DateTime, GUID, GUID + GUID
            // 파일이름 image.jpg
            //var fileFullName = file.FileName.Split('.');
            //var fileName = $"{Guid.NewGuid()}.{fileFullName[0]}";
            var fileName = file.FileName;
            // 3. Extension(확장자) - jpg, png... txt

            using (var fileStream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
            {
                file.CopyTo(fileStream);
                //await file.CopyToAsync(fileStream);
            }

            return Ok(new { file="/images/upload/" + fileName, success = true});

            // URL 접근 방식
            // ASP.NET - 호스트명/ + api/upload
            // JavaScript - 호스트명 + api/upload => http://www.example.compi/upload 잘못됨
            // JavaScript - 호스트명 + / + api/upload => http://www.example.com/pi/upload /를 붙여줘야함
        }
    }
}
