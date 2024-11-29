using FacialEmotionRecognition.APIs;
using FacialEmotionRecognition.APIs.Common;
using FacialEmotionRecognition.APIs.Dtos;
using FacialEmotionRecognition.APIs.Errors;
using FacialEmotionRecognition.APIs.Extensions;
using FacialEmotionRecognition.Infrastructure;
using FacialEmotionRecognition.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FacialEmotionRecognition.APIs;

public abstract class EmotionsServiceBase : IEmotionsService
{
    protected readonly FacialEmotionRecognitionDbContext _context;

    public EmotionsServiceBase(FacialEmotionRecognitionDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Emotion
    /// </summary>
    public async Task<Emotion> CreateEmotion(EmotionCreateInput createDto)
    {
        var emotion = new EmotionDbModel
        {
            Confidence = createDto.Confidence,
            CreatedAt = createDto.CreatedAt,
            DetectedInImage = createDto.DetectedInImage,
            Name = createDto.Name,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            emotion.Id = createDto.Id;
        }

        _context.Emotions.Add(emotion);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<EmotionDbModel>(emotion.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Emotion
    /// </summary>
    public async Task DeleteEmotion(EmotionWhereUniqueInput uniqueId)
    {
        var emotion = await _context.Emotions.FindAsync(uniqueId.Id);
        if (emotion == null)
        {
            throw new NotFoundException();
        }

        _context.Emotions.Remove(emotion);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Emotions
    /// </summary>
    public async Task<List<Emotion>> Emotions(EmotionFindManyArgs findManyArgs)
    {
        var emotions = await _context
            .Emotions.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return emotions.ConvertAll(emotion => emotion.ToDto());
    }

    /// <summary>
    /// Meta data about Emotion records
    /// </summary>
    public async Task<MetadataDto> EmotionsMeta(EmotionFindManyArgs findManyArgs)
    {
        var count = await _context.Emotions.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Emotion
    /// </summary>
    public async Task<Emotion> Emotion(EmotionWhereUniqueInput uniqueId)
    {
        var emotions = await this.Emotions(
            new EmotionFindManyArgs { Where = new EmotionWhereInput { Id = uniqueId.Id } }
        );
        var emotion = emotions.FirstOrDefault();
        if (emotion == null)
        {
            throw new NotFoundException();
        }

        return emotion;
    }

    /// <summary>
    /// Update one Emotion
    /// </summary>
    public async Task UpdateEmotion(EmotionWhereUniqueInput uniqueId, EmotionUpdateInput updateDto)
    {
        var emotion = updateDto.ToModel(uniqueId);

        _context.Entry(emotion).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Emotions.Any(e => e.Id == emotion.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }
}
