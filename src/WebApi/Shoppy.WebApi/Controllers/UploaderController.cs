using _0_Framework.Application.Extensions;
using _0_Framework.Application.Utilities.ImageRelated;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.IO;

namespace Shoppy.WebApi.Controllers;

[ApiController]
[Route("upload")]
public class UploaderController : ControllerBase
{
    [Route("img-upload"), HttpPost]
    public IActionResult UploadImage(IFormFile upload)
    {
        if (upload.Length <= 0) return null;
        if (!upload.IsImage())
        {
            var notImageMsg = "لطفا یک تصویر آپلود کنید";
            var notImage = JsonConvert.DeserializeObject("{'uploaded':0, 'error': {'message': \" " + notImageMsg + " \"}}");

            return new JsonResult(notImage);
        }

        var day = DateTime.Now.ToString("yyyy/mm/dd").Replace("/", "-");

        var imagePath = DateTime.Now.ToFileName() + Path.GetExtension(upload.FileName);

        upload.AddImageToServer(imagePath, $"wwwroot/editor-upload/{day}/", null, null);

        return new JsonResult(new
        {
            uploaded = true,
            url = $"wwwroot/editor-upload/{day}/{imagePath}"
        });
    }
}
