using FacialEmotionRecognition.APIs;
using FacialEmotionRecognition.APIs.Common;
using FacialEmotionRecognition.APIs.Dtos;
using FacialEmotionRecognition.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FacialEmotionRecognition.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class EmotionsControllerBase : ControllerBase
{
    protected readonly IEmotionsService _service;

    public EmotionsControllerBase(IEmotionsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Emotion
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Emotion>> CreateEmotion(EmotionCreateInput input)
    {
        var emotion = await _service.CreateEmotion(input);

        return CreatedAtAction(nameof(Emotion), new { id = emotion.Id }, emotion);
    }

    /// <summary>
    /// Delete one Emotion
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteEmotion([FromRoute()] EmotionWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteEmotion(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Emotions
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Emotion>>> Emotions(
        [FromQuery()] EmotionFindManyArgs filter
    )
    {
        return Ok(await _service.Emotions(filter));
    }

    /// <summary>
    /// Meta data about Emotion records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> EmotionsMeta(
        [FromQuery()] EmotionFindManyArgs filter
    )
    {
        return Ok(await _service.EmotionsMeta(filter));
    }

    /// <summary>
    /// Get one Emotion
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Emotion>> Emotion([FromRoute()] EmotionWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Emotion(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Emotion
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateEmotion(
        [FromRoute()] EmotionWhereUniqueInput uniqueId,
        [FromQuery()] EmotionUpdateInput emotionUpdateDto
    )
    {
        try
        {
            await _service.UpdateEmotion(uniqueId, emotionUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
