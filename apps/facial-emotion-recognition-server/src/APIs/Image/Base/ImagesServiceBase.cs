using FacialEmotionRecognition.APIs;
using FacialEmotionRecognition.APIs.Common;
using FacialEmotionRecognition.APIs.Dtos;
using FacialEmotionRecognition.APIs.Errors;
using FacialEmotionRecognition.APIs.Extensions;
using FacialEmotionRecognition.Infrastructure;
using FacialEmotionRecognition.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FacialEmotionRecognition.APIs;

public abstract class ImagesServiceBase : IImagesService
{
    protected readonly FacialEmotionRecognitionDbContext _context;

    public ImagesServiceBase(FacialEmotionRecognitionDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Image
    /// </summary>
    public async Task<Image> CreateImage(ImageCreateInput createDto)
    {
        var image = new ImageDbModel
        {
            CreatedAt = createDto.CreatedAt,
            FilePath = createDto.FilePath,
            Height = createDto.Height,
            UpdatedAt = createDto.UpdatedAt,
            Width = createDto.Width
        };

        if (createDto.Id != null)
        {
            image.Id = createDto.Id;
        }
        if (createDto.Dataset != null)
        {
            image.Dataset = await _context
                .Datasets.Where(dataset => createDto.Dataset.Id == dataset.Id)
                .FirstOrDefaultAsync();
        }

        _context.Images.Add(image);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<ImageDbModel>(image.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Image
    /// </summary>
    public async Task DeleteImage(ImageWhereUniqueInput uniqueId)
    {
        var image = await _context.Images.FindAsync(uniqueId.Id);
        if (image == null)
        {
            throw new NotFoundException();
        }

        _context.Images.Remove(image);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Images
    /// </summary>
    public async Task<List<Image>> Images(ImageFindManyArgs findManyArgs)
    {
        var images = await _context
            .Images.Include(x => x.Dataset)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return images.ConvertAll(image => image.ToDto());
    }

    /// <summary>
    /// Meta data about Image records
    /// </summary>
    public async Task<MetadataDto> ImagesMeta(ImageFindManyArgs findManyArgs)
    {
        var count = await _context.Images.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Image
    /// </summary>
    public async Task<Image> Image(ImageWhereUniqueInput uniqueId)
    {
        var images = await this.Images(
            new ImageFindManyArgs { Where = new ImageWhereInput { Id = uniqueId.Id } }
        );
        var image = images.FirstOrDefault();
        if (image == null)
        {
            throw new NotFoundException();
        }

        return image;
    }

    /// <summary>
    /// Update one Image
    /// </summary>
    public async Task UpdateImage(ImageWhereUniqueInput uniqueId, ImageUpdateInput updateDto)
    {
        var image = updateDto.ToModel(uniqueId);

        if (updateDto.Dataset != null)
        {
            image.Dataset = await _context
                .Datasets.Where(dataset => updateDto.Dataset == dataset.Id)
                .FirstOrDefaultAsync();
        }

        _context.Entry(image).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Images.Any(e => e.Id == image.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }

    /// <summary>
    /// Get a Dataset record for Image
    /// </summary>
    public async Task<Dataset> GetDataset(ImageWhereUniqueInput uniqueId)
    {
        var image = await _context
            .Images.Where(image => image.Id == uniqueId.Id)
            .Include(image => image.Dataset)
            .FirstOrDefaultAsync();
        if (image == null)
        {
            throw new NotFoundException();
        }
        return image.Dataset.ToDto();
    }
}
