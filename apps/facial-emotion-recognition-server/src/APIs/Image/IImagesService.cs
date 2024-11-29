using FacialEmotionRecognition.APIs.Common;
using FacialEmotionRecognition.APIs.Dtos;

namespace FacialEmotionRecognition.APIs;

public interface IImagesService
{
    /// <summary>
    /// Create one Image
    /// </summary>
    public Task<Image> CreateImage(ImageCreateInput image);

    /// <summary>
    /// Delete one Image
    /// </summary>
    public Task DeleteImage(ImageWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Images
    /// </summary>
    public Task<List<Image>> Images(ImageFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Image records
    /// </summary>
    public Task<MetadataDto> ImagesMeta(ImageFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Image
    /// </summary>
    public Task<Image> Image(ImageWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Image
    /// </summary>
    public Task UpdateImage(ImageWhereUniqueInput uniqueId, ImageUpdateInput updateDto);

    /// <summary>
    /// Get a Dataset record for Image
    /// </summary>
    public Task<Dataset> GetDataset(ImageWhereUniqueInput uniqueId);
}
