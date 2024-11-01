using FacialEmotionRecognition.APIs.Dtos;
using FacialEmotionRecognition.Infrastructure.Models;

namespace FacialEmotionRecognition.APIs.Extensions;

public static class EmotionsExtensions
{
    public static Emotion ToDto(this EmotionDbModel model)
    {
        return new Emotion
        {
            Confidence = model.Confidence,
            CreatedAt = model.CreatedAt,
            DetectedInImage = model.DetectedInImage,
            Id = model.Id,
            Name = model.Name,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static EmotionDbModel ToModel(
        this EmotionUpdateInput updateDto,
        EmotionWhereUniqueInput uniqueId
    )
    {
        var emotion = new EmotionDbModel
        {
            Id = uniqueId.Id,
            Confidence = updateDto.Confidence,
            DetectedInImage = updateDto.DetectedInImage,
            Name = updateDto.Name
        };

        if (updateDto.CreatedAt != null)
        {
            emotion.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            emotion.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return emotion;
    }
}
