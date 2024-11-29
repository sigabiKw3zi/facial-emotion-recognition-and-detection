using FacialEmotionRecognition.APIs.Dtos;
using FacialEmotionRecognition.Infrastructure.Models;

namespace FacialEmotionRecognition.APIs.Extensions;

public static class ImagesExtensions
{
    public static Image ToDto(this ImageDbModel model)
    {
        return new Image
        {
            CreatedAt = model.CreatedAt,
            Dataset = model.DatasetId,
            FilePath = model.FilePath,
            Height = model.Height,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
            Width = model.Width,
        };
    }

    public static ImageDbModel ToModel(
        this ImageUpdateInput updateDto,
        ImageWhereUniqueInput uniqueId
    )
    {
        var image = new ImageDbModel
        {
            Id = uniqueId.Id,
            FilePath = updateDto.FilePath,
            Height = updateDto.Height,
            Width = updateDto.Width
        };

        if (updateDto.CreatedAt != null)
        {
            image.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.Dataset != null)
        {
            image.DatasetId = updateDto.Dataset;
        }
        if (updateDto.UpdatedAt != null)
        {
            image.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return image;
    }
}
