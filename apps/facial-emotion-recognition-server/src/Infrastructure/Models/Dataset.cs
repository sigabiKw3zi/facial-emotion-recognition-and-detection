using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FacialEmotionRecognition.Infrastructure.Models;

[Table("Datasets")]
public class DatasetDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    [StringLength(1000)]
    public string? Description { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    public List<ImageDbModel>? Images { get; set; } = new List<ImageDbModel>();

    [StringLength(1000)]
    public string? Name { get; set; }

    [StringLength(1000)]
    public string? Source { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
