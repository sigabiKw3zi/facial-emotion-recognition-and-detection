using FacialEmotionRecognition.APIs.Common;
using FacialEmotionRecognition.APIs.Dtos;

namespace FacialEmotionRecognition.APIs;

public interface IModelsService
{
    /// <summary>
    /// Create one Model
    /// </summary>
    public Task<Model> CreateModel(ModelCreateInput model);

    /// <summary>
    /// Delete one Model
    /// </summary>
    public Task DeleteModel(ModelWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Models
    /// </summary>
    public Task<List<Model>> Models(ModelFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Model records
    /// </summary>
    public Task<MetadataDto> ModelsMeta(ModelFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Model
    /// </summary>
    public Task<Model> Model(ModelWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Model
    /// </summary>
    public Task UpdateModel(ModelWhereUniqueInput uniqueId, ModelUpdateInput updateDto);
}
