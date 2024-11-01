using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FacialEmotionRecognition.Infrastructure.Models;

[Table("Images")]
public class ImageDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    public string? DatasetId { get; set; }

    [ForeignKey(nameof(DatasetId))]
    public DatasetDbModel? Dataset { get; set; } = null;

    [StringLength(1000)]
    public string? FilePath { get; set; }

    [Range(-999999999, 999999999)]
    public int? Height { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }

    [Range(-999999999, 999999999)]
    public int? Width { get; set; }
}
