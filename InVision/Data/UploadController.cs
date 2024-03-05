using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.Diagnostics;

namespace InVision.Data
{
    public class UploadController : Controller
    {
        private readonly IWebHostEnvironment environment;

        public UploadController(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }

        [HttpPost("upload/image")]
        public IActionResult Image(IFormFile file)
        {
            Console.WriteLine("Checkpoint 1");
            try
            {
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

                using (var stream = new FileStream(Path.Combine(environment.WebRootPath, fileName), FileMode.Create))
                {
                    // Save the file
                    file.CopyTo(stream);

                    // Return the URL of the file
                    var url = Url.Content($"~/{fileName}");
                    Console.WriteLine("images saved");
                    return Ok(new { Url = url });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Images not saved");
                return StatusCode(500, ex.Message);
            }
        }
    }
}
