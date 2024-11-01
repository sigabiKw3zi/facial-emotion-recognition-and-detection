using FacialEmotionRecognition.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FacialEmotionRecognition.Infrastructure;

public class FacialEmotionRecognitionDbContext : IdentityDbContext<IdentityUser>
{
    public FacialEmotionRecognitionDbContext(
        DbContextOptions<FacialEmotionRecognitionDbContext> options
    )
        : base(options) { }

    public DbSet<ImageDbModel> Images { get; set; }

    public DbSet<DatasetDbModel> Datasets { get; set; }

    public DbSet<EmotionDbModel> Emotions { get; set; }

    public DbSet<ModelDbModel> Models { get; set; }
}
