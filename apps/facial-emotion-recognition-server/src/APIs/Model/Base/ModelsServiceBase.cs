using FacialEmotionRecognition.APIs;
using FacialEmotionRecognition.APIs.Common;
using FacialEmotionRecognition.APIs.Dtos;
using FacialEmotionRecognition.APIs.Errors;
using FacialEmotionRecognition.APIs.Extensions;
using FacialEmotionRecognition.Infrastructure;
using FacialEmotionRecognition.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FacialEmotionRecognition.APIs;

public abstract class ModelsServiceBase : IModelsService
{
    protected readonly FacialEmotionRecognitionDbContext _context;

    public ModelsServiceBase(FacialEmotionRecognitionDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Model
    /// </summary>
    public async Task<Model> CreateModel(ModelCreateInput createDto)
    {
        var model = new ModelDbModel
        {
            CreatedAt = createDto.CreatedAt,
            Name = createDto.Name,
            TrainedOnDataset = createDto.TrainedOnDataset,
            UpdatedAt = createDto.UpdatedAt,
            Version = createDto.Version
        };

        if (createDto.Id != null)
        {
            model.Id = createDto.Id;
        }

        _context.Models.Add(model);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<ModelDbModel>(model.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Model
    /// </summary>
    public async Task DeleteModel(ModelWhereUniqueInput uniqueId)
    {
        var model = await _context.Models.FindAsync(uniqueId.Id);
        if (model == null)
        {
            throw new NotFoundException();
        }

        _context.Models.Remove(model);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Models
    /// </summary>
    public async Task<List<Model>> Models(ModelFindManyArgs findManyArgs)
    {
        var models = await _context
            .Models.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return models.ConvertAll(model => model.ToDto());
    }

    /// <summary>
    /// Meta data about Model records
    /// </summary>
    public async Task<MetadataDto> ModelsMeta(ModelFindManyArgs findManyArgs)
    {
        var count = await _context.Models.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Model
    /// </summary>
    public async Task<Model> Model(ModelWhereUniqueInput uniqueId)
    {
        var models = await this.Models(
            new ModelFindManyArgs { Where = new ModelWhereInput { Id = uniqueId.Id } }
        );
        var model = models.FirstOrDefault();
        if (model == null)
        {
            throw new NotFoundException();
        }

        return model;
    }

    /// <summary>
    /// Update one Model
    /// </summary>
    public async Task UpdateModel(ModelWhereUniqueInput uniqueId, ModelUpdateInput updateDto)
    {
        var model = updateDto.ToModel(uniqueId);

        _context.Entry(model).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Models.Any(e => e.Id == model.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }
}
