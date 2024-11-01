using Microsoft.EntityFrameworkCore;

namespace FacialEmotionRecognition.Infrastructure;

public class FacialEmotionRecognitionDbContext : DbContext
{
    public FacialEmotionRecognitionDbContext(
        DbContextOptions<FacialEmotionRecognitionDbContext> options
    )
        : base(options) { }
}
