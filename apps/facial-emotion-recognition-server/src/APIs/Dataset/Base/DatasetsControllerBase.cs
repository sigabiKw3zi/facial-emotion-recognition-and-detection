using FacialEmotionRecognition.APIs;
using FacialEmotionRecognition.APIs.Common;
using FacialEmotionRecognition.APIs.Dtos;
using FacialEmotionRecognition.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FacialEmotionRecognition.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class DatasetsControllerBase : ControllerBase
{
    protected readonly IDatasetsService _service;

    public DatasetsControllerBase(IDatasetsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Dataset
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Dataset>> CreateDataset(DatasetCreateInput input)
    {
        var dataset = await _service.CreateDataset(input);

        return CreatedAtAction(nameof(Dataset), new { id = dataset.Id }, dataset);
    }

    /// <summary>
    /// Delete one Dataset
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteDataset([FromRoute()] DatasetWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteDataset(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Datasets
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Dataset>>> Datasets(
        [FromQuery()] DatasetFindManyArgs filter
    )
    {
        return Ok(await _service.Datasets(filter));
    }

    /// <summary>
    /// Meta data about Dataset records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> DatasetsMeta(
        [FromQuery()] DatasetFindManyArgs filter
    )
    {
        return Ok(await _service.DatasetsMeta(filter));
    }

    /// <summary>
    /// Get one Dataset
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Dataset>> Dataset([FromRoute()] DatasetWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Dataset(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Dataset
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateDataset(
        [FromRoute()] DatasetWhereUniqueInput uniqueId,
        [FromQuery()] DatasetUpdateInput datasetUpdateDto
    )
    {
        try
        {
            await _service.UpdateDataset(uniqueId, datasetUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple Images records to Dataset
    /// </summary>
    [HttpPost("{Id}/images")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> ConnectImages(
        [FromRoute()] DatasetWhereUniqueInput uniqueId,
        [FromQuery()] ImageWhereUniqueInput[] imagesId
    )
    {
        try
        {
            await _service.ConnectImages(uniqueId, imagesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Images records from Dataset
    /// </summary>
    [HttpDelete("{Id}/images")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DisconnectImages(
        [FromRoute()] DatasetWhereUniqueInput uniqueId,
        [FromBody()] ImageWhereUniqueInput[] imagesId
    )
    {
        try
        {
            await _service.DisconnectImages(uniqueId, imagesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Images records for Dataset
    /// </summary>
    [HttpGet("{Id}/images")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Image>>> FindImages(
        [FromRoute()] DatasetWhereUniqueInput uniqueId,
        [FromQuery()] ImageFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindImages(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple Images records for Dataset
    /// </summary>
    [HttpPatch("{Id}/images")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateImages(
        [FromRoute()] DatasetWhereUniqueInput uniqueId,
        [FromBody()] ImageWhereUniqueInput[] imagesId
    )
    {
        try
        {
            await _service.UpdateImages(uniqueId, imagesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
