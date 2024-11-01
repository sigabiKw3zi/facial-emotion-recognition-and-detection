using FacialEmotionRecognition.APIs.Dtos;
using FacialEmotionRecognition.Infrastructure.Models;

namespace FacialEmotionRecognition.APIs.Extensions;

public static class ModelsExtensions
{
    public static Model ToDto(this ModelDbModel model)
    {
        return new Model
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            Name = model.Name,
            TrainedOnDataset = model.TrainedOnDataset,
            UpdatedAt = model.UpdatedAt,
            Version = model.Version,
        };
    }

    public static ModelDbModel ToModel(
        this ModelUpdateInput updateDto,
        ModelWhereUniqueInput uniqueId
    )
    {
        var model = new ModelDbModel
        {
            Id = uniqueId.Id,
            Name = updateDto.Name,
            TrainedOnDataset = updateDto.TrainedOnDataset,
            Version = updateDto.Version
        };

        if (updateDto.CreatedAt != null)
        {
            model.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            model.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return model;
    }
}
