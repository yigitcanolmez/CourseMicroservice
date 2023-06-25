using FreeCourse.Services.PhotoStock.DTOs;
using FreeCourse.Shared.ControllerBases;
using FreeCourse.Shared.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Services.PhotoStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : CustomBaseController
    {
        [HttpPost]
        public async Task<IActionResult> PhotoSave(IFormFile photo, CancellationToken cancellationToken)
        {
            if (photo != null && photo.Length > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photo.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await photo.CopyToAsync(stream, cancellationToken);

                    var returnPath = "photo/" + photo.FileName;

                    PhotoDto photoDto = new()
                    {
                        Url = returnPath
                    };
                    return CreateActionResultInstance(Response<PhotoDto>.Success(photoDto, StatusCodes.Status200OK));
                }
            }

            return CreateActionResultInstance(Response<PhotoDto>.Fail("url is empty", StatusCodes.Status400BadRequest));

        }

        [HttpDelete]
        public IActionResult PhotoDelete(string photoUrl)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photoUrl);
            if (!System.IO.File.Exists(path))
            {
                return CreateActionResultInstance(Response<NoContent>.Fail("url is empty", StatusCodes.Status404NotFound));

            }
            System.IO.File.Delete(path);
            return CreateActionResultInstance(Response<NoContent>.Success(StatusCodes.Status204NoContent));


        }
    }
}
