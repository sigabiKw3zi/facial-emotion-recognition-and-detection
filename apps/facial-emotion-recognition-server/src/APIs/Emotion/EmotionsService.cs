using FacialEmotionRecognition.Infrastructure;

namespace FacialEmotionRecognition.APIs;

public class EmotionsService : EmotionsServiceBase
{
    public EmotionsService(FacialEmotionRecognitionDbContext context)
        : base(context) { }
}
