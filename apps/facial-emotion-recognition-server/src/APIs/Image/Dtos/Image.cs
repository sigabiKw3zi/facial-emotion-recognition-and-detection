namespace FacialEmotionRecognition.APIs.Dtos;

public class Image
{
    public DateTime CreatedAt { get; set; }

    public string? Dataset { get; set; }

    public string? FilePath { get; set; }

    public int? Height { get; set; }

    public string Id { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int? Width { get; set; }
}
