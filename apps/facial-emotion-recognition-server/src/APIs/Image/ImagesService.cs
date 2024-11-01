using FacialEmotionRecognition.Infrastructure;

namespace FacialEmotionRecognition.APIs;

public class ImagesService : ImagesServiceBase
{
    public ImagesService(FacialEmotionRecognitionDbContext context)
        : base(context) { }
}
