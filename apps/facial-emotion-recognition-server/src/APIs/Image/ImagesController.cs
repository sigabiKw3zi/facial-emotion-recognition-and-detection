using Microsoft.AspNetCore.Mvc;

namespace FacialEmotionRecognition.APIs;

[ApiController()]
public class ImagesController : ImagesControllerBase
{
    public ImagesController(IImagesService service)
        : base(service) { }
}
