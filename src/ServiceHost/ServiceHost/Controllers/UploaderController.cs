using _0_Framework.Application.Utilities.ImageRelated;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ServiceHost.Controllers;

[ApiController]
[Route("upload")]
public class UploaderController : ControllerBase
{
    [Route("img-upload")]
    [HttpPost]
    public IActionResult UploadImage(IFormFile upload)
    {
        if (upload.Length <= 0) return null;
        if (!upload.IsImage())
        {
            string notImageMsg = "لطفا یک تصویر آپلود کنید";
            object notImage =
                JsonConvert.DeserializeObject("{'uploaded':0, 'error': {'message': \" " + notImageMsg + " \"}}");

            return new JsonResult(notImage);
        }

        string day = DateTime.Now.ToString("yyyy/mm/dd").Replace("/", "-");

        string imagePath = upload.GenerateImagePath();

        upload.AddImageToServer(imagePath, $"wwwroot/editor-upload/{day}/", 150, 150);

        return new JsonResult(new
        {
            uploaded = true,
            url = $"wwwroot/editor-upload/{day}/{imagePath}"
        });
    }
}