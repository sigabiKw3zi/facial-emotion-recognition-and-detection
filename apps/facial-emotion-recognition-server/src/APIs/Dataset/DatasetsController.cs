using Microsoft.AspNetCore.Mvc;

namespace FacialEmotionRecognition.APIs;

[ApiController()]
public class DatasetsController : DatasetsControllerBase
{
    public DatasetsController(IDatasetsService service)
        : base(service) { }
}
