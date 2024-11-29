using FacialEmotionRecognition.APIs;
using FacialEmotionRecognition.APIs.Common;
using FacialEmotionRecognition.APIs.Dtos;
using FacialEmotionRecognition.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FacialEmotionRecognition.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class ImagesControllerBase : ControllerBase
{
    protected readonly IImagesService _service;

    public ImagesControllerBase(IImagesService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Image
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Image>> CreateImage(ImageCreateInput input)
    {
        var image = await _service.CreateImage(input);

        return CreatedAtAction(nameof(Image), new { id = image.Id }, image);
    }

    /// <summary>
    /// Delete one Image
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteImage([FromRoute()] ImageWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteImage(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Images
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Image>>> Images([FromQuery()] ImageFindManyArgs filter)
    {
        return Ok(await _service.Images(filter));
    }

    /// <summary>
    /// Meta data about Image records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> ImagesMeta([FromQuery()] ImageFindManyArgs filter)
    {
        return Ok(await _service.ImagesMeta(filter));
    }

    /// <summary>
    /// Get one Image
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Image>> Image([FromRoute()] ImageWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Image(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Image
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateImage(
        [FromRoute()] ImageWhereUniqueInput uniqueId,
        [FromQuery()] ImageUpdateInput imageUpdateDto
    )
    {
        try
        {
            await _service.UpdateImage(uniqueId, imageUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a Dataset record for Image
    /// </summary>
    [HttpGet("{Id}/dataset")]
    public async Task<ActionResult<List<Dataset>>> GetDataset(
        [FromRoute()] ImageWhereUniqueInput uniqueId
    )
    {
        var dataset = await _service.GetDataset(uniqueId);
        return Ok(dataset);
    }
}
