using FacialEmotionRecognition.APIs.Dtos;
using FacialEmotionRecognition.Infrastructure.Models;

namespace FacialEmotionRecognition.APIs.Extensions;

public static class DatasetsExtensions
{
    public static Dataset ToDto(this DatasetDbModel model)
    {
        return new Dataset
        {
            CreatedAt = model.CreatedAt,
            Description = model.Description,
            Id = model.Id,
            Images = model.Images?.Select(x => x.Id).ToList(),
            Name = model.Name,
            Source = model.Source,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static DatasetDbModel ToModel(
        this DatasetUpdateInput updateDto,
        DatasetWhereUniqueInput uniqueId
    )
    {
        var dataset = new DatasetDbModel
        {
            Id = uniqueId.Id,
            Description = updateDto.Description,
            Name = updateDto.Name,
            Source = updateDto.Source
        };

        if (updateDto.CreatedAt != null)
        {
            dataset.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            dataset.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return dataset;
    }
}
