using FacialEmotionRecognition.Infrastructure;

namespace FacialEmotionRecognition.APIs;

public class DatasetsService : DatasetsServiceBase
{
    public DatasetsService(FacialEmotionRecognitionDbContext context)
        : base(context) { }
}
