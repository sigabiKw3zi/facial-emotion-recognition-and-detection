using FacialEmotionRecognition.Infrastructure;

namespace FacialEmotionRecognition.APIs;

public class ModelsService : ModelsServiceBase
{
    public ModelsService(FacialEmotionRecognitionDbContext context)
        : base(context) { }
}
