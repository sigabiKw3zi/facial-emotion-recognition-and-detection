using Microsoft.AspNetCore.Mvc;

namespace FacialEmotionRecognition.APIs;

[ApiController()]
public class ModelsController : ModelsControllerBase
{
    public ModelsController(IModelsService service)
        : base(service) { }
}
