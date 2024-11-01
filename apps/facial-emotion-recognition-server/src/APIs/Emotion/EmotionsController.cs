using Microsoft.AspNetCore.Mvc;

namespace FacialEmotionRecognition.APIs;

[ApiController()]
public class EmotionsController : EmotionsControllerBase
{
    public EmotionsController(IEmotionsService service)
        : base(service) { }
}
