using FacialEmotionRecognition.APIs.Common;
using FacialEmotionRecognition.APIs.Dtos;

namespace FacialEmotionRecognition.APIs;

public interface IDatasetsService
{
    /// <summary>
    /// Create one Dataset
    /// </summary>
    public Task<Dataset> CreateDataset(DatasetCreateInput dataset);

    /// <summary>
    /// Delete one Dataset
    /// </summary>
    public Task DeleteDataset(DatasetWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Datasets
    /// </summary>
    public Task<List<Dataset>> Datasets(DatasetFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Dataset records
    /// </summary>
    public Task<MetadataDto> DatasetsMeta(DatasetFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Dataset
    /// </summary>
    public Task<Dataset> Dataset(DatasetWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Dataset
    /// </summary>
    public Task UpdateDataset(DatasetWhereUniqueInput uniqueId, DatasetUpdateInput updateDto);

    /// <summary>
    /// Connect multiple Images records to Dataset
    /// </summary>
    public Task ConnectImages(DatasetWhereUniqueInput uniqueId, ImageWhereUniqueInput[] imagesId);

    /// <summary>
    /// Disconnect multiple Images records from Dataset
    /// </summary>
    public Task DisconnectImages(
        DatasetWhereUniqueInput uniqueId,
        ImageWhereUniqueInput[] imagesId
    );

    /// <summary>
    /// Find multiple Images records for Dataset
    /// </summary>
    public Task<List<Image>> FindImages(
        DatasetWhereUniqueInput uniqueId,
        ImageFindManyArgs ImageFindManyArgs
    );

    /// <summary>
    /// Update multiple Images records for Dataset
    /// </summary>
    public Task UpdateImages(DatasetWhereUniqueInput uniqueId, ImageWhereUniqueInput[] imagesId);
}
