using FacialEmotionRecognition.APIs;
using FacialEmotionRecognition.APIs.Common;
using FacialEmotionRecognition.APIs.Dtos;
using FacialEmotionRecognition.APIs.Errors;
using FacialEmotionRecognition.APIs.Extensions;
using FacialEmotionRecognition.Infrastructure;
using FacialEmotionRecognition.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FacialEmotionRecognition.APIs;

public abstract class DatasetsServiceBase : IDatasetsService
{
    protected readonly FacialEmotionRecognitionDbContext _context;

    public DatasetsServiceBase(FacialEmotionRecognitionDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Dataset
    /// </summary>
    public async Task<Dataset> CreateDataset(DatasetCreateInput createDto)
    {
        var dataset = new DatasetDbModel
        {
            CreatedAt = createDto.CreatedAt,
            Description = createDto.Description,
            Name = createDto.Name,
            Source = createDto.Source,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            dataset.Id = createDto.Id;
        }
        if (createDto.Images != null)
        {
            dataset.Images = await _context
                .Images.Where(image => createDto.Images.Select(t => t.Id).Contains(image.Id))
                .ToListAsync();
        }

        _context.Datasets.Add(dataset);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<DatasetDbModel>(dataset.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Dataset
    /// </summary>
    public async Task DeleteDataset(DatasetWhereUniqueInput uniqueId)
    {
        var dataset = await _context.Datasets.FindAsync(uniqueId.Id);
        if (dataset == null)
        {
            throw new NotFoundException();
        }

        _context.Datasets.Remove(dataset);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Datasets
    /// </summary>
    public async Task<List<Dataset>> Datasets(DatasetFindManyArgs findManyArgs)
    {
        var datasets = await _context
            .Datasets.Include(x => x.Images)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return datasets.ConvertAll(dataset => dataset.ToDto());
    }

    /// <summary>
    /// Meta data about Dataset records
    /// </summary>
    public async Task<MetadataDto> DatasetsMeta(DatasetFindManyArgs findManyArgs)
    {
        var count = await _context.Datasets.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Dataset
    /// </summary>
    public async Task<Dataset> Dataset(DatasetWhereUniqueInput uniqueId)
    {
        var datasets = await this.Datasets(
            new DatasetFindManyArgs { Where = new DatasetWhereInput { Id = uniqueId.Id } }
        );
        var dataset = datasets.FirstOrDefault();
        if (dataset == null)
        {
            throw new NotFoundException();
        }

        return dataset;
    }

    /// <summary>
    /// Update one Dataset
    /// </summary>
    public async Task UpdateDataset(DatasetWhereUniqueInput uniqueId, DatasetUpdateInput updateDto)
    {
        var dataset = updateDto.ToModel(uniqueId);

        if (updateDto.Images != null)
        {
            dataset.Images = await _context
                .Images.Where(image => updateDto.Images.Select(t => t).Contains(image.Id))
                .ToListAsync();
        }

        _context.Entry(dataset).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Datasets.Any(e => e.Id == dataset.Id))
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
    /// Connect multiple Images records to Dataset
    /// </summary>
    public async Task ConnectImages(
        DatasetWhereUniqueInput uniqueId,
        ImageWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Datasets.Include(x => x.Images)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Images.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.Images);

        foreach (var child in childrenToConnect)
        {
            parent.Images.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Images records from Dataset
    /// </summary>
    public async Task DisconnectImages(
        DatasetWhereUniqueInput uniqueId,
        ImageWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Datasets.Include(x => x.Images)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Images.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.Images?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Images records for Dataset
    /// </summary>
    public async Task<List<Image>> FindImages(
        DatasetWhereUniqueInput uniqueId,
        ImageFindManyArgs datasetFindManyArgs
    )
    {
        var images = await _context
            .Images.Where(m => m.DatasetId == uniqueId.Id)
            .ApplyWhere(datasetFindManyArgs.Where)
            .ApplySkip(datasetFindManyArgs.Skip)
            .ApplyTake(datasetFindManyArgs.Take)
            .ApplyOrderBy(datasetFindManyArgs.SortBy)
            .ToListAsync();

        return images.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Images records for Dataset
    /// </summary>
    public async Task UpdateImages(
        DatasetWhereUniqueInput uniqueId,
        ImageWhereUniqueInput[] childrenIds
    )
    {
        var dataset = await _context
            .Datasets.Include(t => t.Images)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (dataset == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Images.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        dataset.Images = children;
        await _context.SaveChangesAsync();
    }
}
