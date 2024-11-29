using FacialEmotionRecognition.APIs;
using FacialEmotionRecognition.APIs.Common;
using FacialEmotionRecognition.APIs.Dtos;
using FacialEmotionRecognition.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FacialEmotionRecognition.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class ModelsControllerBase : ControllerBase
{
    protected readonly IModelsService _service;

    public ModelsControllerBase(IModelsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Model
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Model>> CreateModel(ModelCreateInput input)
    {
        var model = await _service.CreateModel(input);

        return CreatedAtAction(nameof(Model), new { id = model.Id }, model);
    }

    /// <summary>
    /// Delete one Model
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteModel([FromRoute()] ModelWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteModel(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Models
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Model>>> Models([FromQuery()] ModelFindManyArgs filter)
    {
        return Ok(await _service.Models(filter));
    }

    /// <summary>
    /// Meta data about Model records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> ModelsMeta([FromQuery()] ModelFindManyArgs filter)
    {
        return Ok(await _service.ModelsMeta(filter));
    }

    /// <summary>
    /// Get one Model
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Model>> Model([FromRoute()] ModelWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Model(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Model
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateModel(
        [FromRoute()] ModelWhereUniqueInput uniqueId,
        [FromQuery()] ModelUpdateInput modelUpdateDto
    )
    {
        try
        {
            await _service.UpdateModel(uniqueId, modelUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
