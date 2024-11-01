using FacialEmotionRecognition.APIs.Common;
using FacialEmotionRecognition.APIs.Dtos;

namespace FacialEmotionRecognition.APIs;

public interface IEmotionsService
{
    /// <summary>
    /// Create one Emotion
    /// </summary>
    public Task<Emotion> CreateEmotion(EmotionCreateInput emotion);

    /// <summary>
    /// Delete one Emotion
    /// </summary>
    public Task DeleteEmotion(EmotionWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Emotions
    /// </summary>
    public Task<List<Emotion>> Emotions(EmotionFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Emotion records
    /// </summary>
    public Task<MetadataDto> EmotionsMeta(EmotionFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Emotion
    /// </summary>
    public Task<Emotion> Emotion(EmotionWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Emotion
    /// </summary>
    public Task UpdateEmotion(EmotionWhereUniqueInput uniqueId, EmotionUpdateInput updateDto);
}
